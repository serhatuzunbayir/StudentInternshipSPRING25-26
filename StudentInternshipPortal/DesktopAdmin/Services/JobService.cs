using Microsoft.Data.Sqlite;
using DesktopAdmin.ViewModels;
using Shared.Data;
using Shared.Enums;
using Shared.Models;
using Shared.Services;

namespace DesktopAdmin.Services;

public class JobService
{
    private const string JobMatchNotificationType = "JobMatch";
    private readonly DatabaseHelper _databaseHelper;
    private readonly MatchingService _matchingService = new();
    private readonly NotificationManager _notificationManager;

    public JobService(DatabaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
        _notificationManager = new NotificationManager(databaseHelper);
    }

    public List<JobListItemViewModel> GetAllJobs()
    {
        var items = new List<JobListItemViewModel>();

        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT Id, Title, Location, JobType, RequiredSkills, IsActive
            FROM Jobs
            ORDER BY CreatedAt DESC, Id DESC;
            """;

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            items.Add(new JobListItemViewModel
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                Location = reader.GetString(2),
                JobType = ((JobType)reader.GetInt32(3)).ToString(),
                RequiredSkills = reader.GetString(4),
                ActiveStatus = reader.GetInt32(5) == 1 ? "Active" : "Passive"
            });
        }

        return items;
    }

    public Job? GetJobById(int jobId)
    {
        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT Id, Title, Description, RequiredSkills, Location, JobType, IsActive, CreatedAt
            FROM Jobs
            WHERE Id = $jobId;
            """;
        command.Parameters.AddWithValue("$jobId", jobId);

        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            return null;
        }

        return new Job
        {
            Id = reader.GetInt32(0),
            Title = reader.GetString(1),
            Description = reader.GetString(2),
            RequiredSkills = reader.GetString(3),
            Location = reader.GetString(4),
            JobType = (JobType)reader.GetInt32(5),
            IsActive = reader.GetInt32(6) == 1,
            CreatedAt = DateTime.Parse(reader.GetString(7))
        };
    }

    public void AddJob(Job job)
    {
        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            INSERT INTO Jobs (Title, Description, RequiredSkills, Location, JobType, IsActive, CreatedAt)
            VALUES ($title, $description, $requiredSkills, $location, $jobType, $isActive, $createdAt);
            SELECT last_insert_rowid();
            """;
        FillJobParameters(command, job);
        job.Id = Convert.ToInt32(command.ExecuteScalar());

        NotifyMatchingStudents(job);
    }

    public void UpdateJob(Job job)
    {
        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            UPDATE Jobs
            SET Title = $title,
                Description = $description,
                RequiredSkills = $requiredSkills,
                Location = $location,
                JobType = $jobType,
                IsActive = $isActive
            WHERE Id = $id;
            """;
        FillJobParameters(command, job);
        command.Parameters.AddWithValue("$id", job.Id);
        command.ExecuteNonQuery();

        NotifyMatchingStudents(job);
    }

    public void DeleteJob(int jobId)
    {
        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        using var deleteApplications = connection.CreateCommand();
        deleteApplications.CommandText = "DELETE FROM Applications WHERE JobId = $jobId;";
        deleteApplications.Parameters.AddWithValue("$jobId", jobId);
        deleteApplications.ExecuteNonQuery();

        using var deleteJob = connection.CreateCommand();
        deleteJob.CommandText = "DELETE FROM Jobs WHERE Id = $jobId;";
        deleteJob.Parameters.AddWithValue("$jobId", jobId);
        deleteJob.ExecuteNonQuery();
    }

    private static void FillJobParameters(SqliteCommand command, Job job)
    {
        command.Parameters.AddWithValue("$title", job.Title);
        command.Parameters.AddWithValue("$description", job.Description);
        command.Parameters.AddWithValue("$requiredSkills", job.RequiredSkills);
        command.Parameters.AddWithValue("$location", job.Location);
        command.Parameters.AddWithValue("$jobType", (int)job.JobType);
        command.Parameters.AddWithValue("$isActive", job.IsActive ? 1 : 0);
        command.Parameters.AddWithValue("$createdAt", job.CreatedAt.ToString("O"));
    }

    private void NotifyMatchingStudents(Job job)
    {
        if (!job.IsActive)
        {
            return;
        }

        foreach (var candidate in GetStudentCandidates())
        {
            var matchPercentage = _matchingService.CalculateSkillMatchPercentage(candidate.Skills, job.RequiredSkills);
            if (matchPercentage <= 0)
            {
                continue;
            }

            var referenceKey = $"job:{job.Id}";
            if (_notificationManager.HasNotification(candidate.UserId, JobMatchNotificationType, referenceKey))
            {
                continue;
            }

            var message =
                $"A new job matches your profile: '{job.Title}' in {job.Location}. " +
                $"Your current skill match is {matchPercentage}%.";

            _notificationManager.CreateNotification(
                candidate.UserId,
                "New Job Match",
                message,
                notificationType: JobMatchNotificationType,
                referenceKey: referenceKey);
        }
    }

    private List<StudentMatchCandidate> GetStudentCandidates()
    {
        var candidates = new List<StudentMatchCandidate>();

        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT UserId, Skills
            FROM StudentProfiles;
            """;

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            candidates.Add(new StudentMatchCandidate
            {
                UserId = reader.GetInt32(0),
                Skills = reader.IsDBNull(1) ? string.Empty : reader.GetString(1)
            });
        }

        return candidates;
    }

    private sealed class StudentMatchCandidate
    {
        public int UserId { get; set; }
        public string Skills { get; set; } = string.Empty;
    }
}
