using DesktopAdmin.ViewModels;
using Shared.Data;
using Shared.Enums;
using Shared.Services;

namespace DesktopAdmin.Services;

public class ApplicationService
{
    private readonly DatabaseHelper _databaseHelper;
    private readonly NotificationManager _notificationManager;

    public ApplicationService(DatabaseHelper databaseHelper, NotificationManager notificationManager)
    {
        _databaseHelper = databaseHelper;
        _notificationManager = notificationManager;
    }

    public List<ApplicationListItemViewModel> GetAllApplications()
    {
        var items = new List<ApplicationListItemViewModel>();
        var matchingService = new MatchingService();

        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT a.Id,
                   sp.UserId,
                   sp.FullName,
                   j.Title,
                   a.Status,
                   a.AppliedAt,
                   sp.Skills,
                   j.RequiredSkills,
                   a.ResumeFileName
            FROM Applications a
            INNER JOIN StudentProfiles sp ON sp.Id = a.StudentProfileId
            INNER JOIN Jobs j ON j.Id = a.JobId
            ORDER BY a.AppliedAt DESC, a.Id DESC;
            """;

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var studentSkills = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
            var requiredSkills = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
            var matchPercentage = matchingService.CalculateSkillMatchPercentage(studentSkills, requiredSkills);
            var resumeFileName = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);

            items.Add(new ApplicationListItemViewModel
            {
                Id = reader.GetInt32(0),
                StudentId = reader.GetInt32(1),
                StudentName = reader.GetString(2),
                JobTitle = reader.GetString(3),
                Status = ((ApplicationStatus)reader.GetInt32(4)).ToString(),
                AppliedAt = DateTime.Parse(reader.GetString(5)).ToString("yyyy-MM-dd HH:mm"),
                MatchPercentage = matchPercentage,
                ResumeFileName = resumeFileName
            });
        }

        return items;
    }

    public StudentProfileDetailsViewModel? GetStudentProfileDetails(int studentUserId)
    {
        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT u.Username,
                   sp.FullName,
                   sp.Skills,
                   sp.Education,
                   sp.Experience,
                   sp.Phone,
                   sp.AboutMe
            FROM StudentProfiles sp
            INNER JOIN Users u ON u.Id = sp.UserId
            WHERE sp.UserId = $userId;
            """;
        command.Parameters.AddWithValue("$userId", studentUserId);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new StudentProfileDetailsViewModel
            {
                Email = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                FullName = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                Skills = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                Education = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                Experience = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                Phone = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                AboutMe = reader.IsDBNull(6) ? string.Empty : reader.GetString(6)
            };
        }

        return null;
    }

    public void UpdateStatus(int applicationId, ApplicationStatus newStatus)
    {
        var statusChangedAt = DateTime.UtcNow;

        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        using var queryCommand = connection.CreateCommand();
        queryCommand.CommandText =
            """
            SELECT sp.UserId, j.Title
            FROM Applications a
            INNER JOIN StudentProfiles sp ON sp.Id = a.StudentProfileId
            INNER JOIN Jobs j ON j.Id = a.JobId
            WHERE a.Id = $applicationId;
            """;
        queryCommand.Parameters.AddWithValue("$applicationId", applicationId);

        int studentUserId = 0;
        var jobTitle = string.Empty;

        using (var reader = queryCommand.ExecuteReader())
        {
            if (!reader.Read())
            {
                return;
            }

            studentUserId = reader.GetInt32(0);
            jobTitle = reader.GetString(1);
        }

        using var updateCommand = connection.CreateCommand();
        updateCommand.CommandText =
            """
            UPDATE Applications
            SET Status = $status,
                UpdatedAt = $updatedAt
            WHERE Id = $applicationId;
            """;
        updateCommand.Parameters.AddWithValue("$status", (int)newStatus);
        updateCommand.Parameters.AddWithValue("$updatedAt", statusChangedAt.ToString("O"));
        updateCommand.Parameters.AddWithValue("$applicationId", applicationId);
        updateCommand.ExecuteNonQuery();

        var notificationTitle = GetNotificationTitle(newStatus);
        var notificationMessage = GetNotificationMessage(jobTitle, newStatus);

        _notificationManager.CreateNotification(
            studentUserId,
            notificationTitle,
            notificationMessage,
            statusChangedAt);
    }

    private static string GetNotificationTitle(ApplicationStatus status)
    {
        return status switch
        {
            ApplicationStatus.Accepted => "Application Accepted",
            ApplicationStatus.Rejected => "Application Rejected",
            ApplicationStatus.Pending => "Application Back In Review",
            _ => "Application Status Updated"
        };
    }

    private static string GetNotificationMessage(string jobTitle, ApplicationStatus status)
    {
        return status switch
        {
            ApplicationStatus.Accepted => $"Good news. Your application for '{jobTitle}' has been accepted.",
            ApplicationStatus.Rejected => $"Your application for '{jobTitle}' has been rejected.",
            ApplicationStatus.Pending => $"Your application for '{jobTitle}' is currently under review again.",
            _ => $"Your application for '{jobTitle}' has been updated."
        };
    }
}
