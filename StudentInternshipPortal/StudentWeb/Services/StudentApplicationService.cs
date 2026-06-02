using Shared.Data;
using Shared.Enums;
using StudentWeb.Models;

namespace StudentWeb.Services;

public class StudentApplicationService
{
    private readonly DatabaseHelper _databaseHelper;

    public StudentApplicationService(DatabaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
    }

    public List<ApplicationListItemViewModel> GetApplicationsForStudent(int userId)
    {
        var items = new List<ApplicationListItemViewModel>();

        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT a.Id,
                   a.JobId,
                   j.Title,
                   j.Location,
                   j.JobType,
                   j.RequiredSkills,
                   a.Status,
                   a.AppliedAt
            FROM Applications a
            INNER JOIN StudentProfiles sp ON sp.Id = a.StudentProfileId
            INNER JOIN Jobs j ON j.Id = a.JobId
            WHERE sp.UserId = $userId
            ORDER BY a.AppliedAt DESC, a.Id DESC;
            """;
        command.Parameters.AddWithValue("$userId", userId);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            items.Add(new ApplicationListItemViewModel
            {
                Id = reader.GetInt32(0),
                JobId = reader.GetInt32(1),
                JobTitle = reader.GetString(2),
                Location = reader.GetString(3),
                JobType = ((JobType)reader.GetInt32(4)).ToString(),
                RequiredSkills = reader.GetString(5),
                Status = ((ApplicationStatus)reader.GetInt32(6)).ToString(),
                AppliedAt = DateTime.Parse(reader.GetString(7)).ToLocalTime().ToString("yyyy-MM-dd HH:mm")
            });
        }

        return items;
    }

    public int GetApplicationCountForStudent(int userId)
    {
        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COUNT(1)
            FROM Applications a
            INNER JOIN StudentProfiles sp ON sp.Id = a.StudentProfileId
            WHERE sp.UserId = $userId;
            """;
        command.Parameters.AddWithValue("$userId", userId);

        return Convert.ToInt32(command.ExecuteScalar());
    }

    public SubmitApplicationResult SubmitApplication(int userId, int jobId, string? resumeFileName)
    {
        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        using var profileCommand = connection.CreateCommand();
        profileCommand.CommandText =
            """
            SELECT Id
            FROM StudentProfiles
            WHERE UserId = $userId;
            """;
        profileCommand.Parameters.AddWithValue("$userId", userId);
        var studentProfileId = profileCommand.ExecuteScalar() as long?;

        if (studentProfileId is null)
        {
            return SubmitApplicationResult.ProfileMissing;
        }

        using var jobCommand = connection.CreateCommand();
        jobCommand.CommandText =
            """
            SELECT COUNT(1)
            FROM Jobs
            WHERE Id = $jobId AND IsActive = 1;
            """;
        jobCommand.Parameters.AddWithValue("$jobId", jobId);
        if (Convert.ToInt32(jobCommand.ExecuteScalar()) == 0)
        {
            return SubmitApplicationResult.JobUnavailable;
        }

        using var existingCommand = connection.CreateCommand();
        existingCommand.CommandText =
            """
            SELECT COUNT(1)
            FROM Applications
            WHERE StudentProfileId = $studentProfileId AND JobId = $jobId;
            """;
        existingCommand.Parameters.AddWithValue("$studentProfileId", (int)studentProfileId);
        existingCommand.Parameters.AddWithValue("$jobId", jobId);

        if (Convert.ToInt32(existingCommand.ExecuteScalar()) > 0)
        {
            return SubmitApplicationResult.AlreadyApplied;
        }

        var now = DateTime.UtcNow.ToString("O");

        using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText =
            """
            INSERT INTO Applications (StudentProfileId, JobId, Status, AppliedAt, UpdatedAt, ResumeFileName)
            VALUES ($studentProfileId, $jobId, $status, $appliedAt, $updatedAt, $resumeFileName);
            """;
        insertCommand.Parameters.AddWithValue("$studentProfileId", (int)studentProfileId);
        insertCommand.Parameters.AddWithValue("$jobId", jobId);
        insertCommand.Parameters.AddWithValue("$status", (int)ApplicationStatus.Pending);
        insertCommand.Parameters.AddWithValue("$appliedAt", now);
        insertCommand.Parameters.AddWithValue("$updatedAt", now);
        insertCommand.Parameters.AddWithValue("$resumeFileName", (object?)resumeFileName ?? DBNull.Value);
        insertCommand.ExecuteNonQuery();

        return SubmitApplicationResult.Success;
    }
}

public enum SubmitApplicationResult
{
    Success = 1,
    AlreadyApplied = 2,
    JobUnavailable = 3,
    ProfileMissing = 4
}
