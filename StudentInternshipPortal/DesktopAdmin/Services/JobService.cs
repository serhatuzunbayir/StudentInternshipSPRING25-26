using Microsoft.Data.Sqlite;
using DesktopAdmin.ViewModels;
using Shared.Data;
using Shared.Enums;
using Shared.Models;
using Shared.Services;

namespace DesktopAdmin.Services;

// This service handles all database operations (CRUD) for Jobs on the desktop admin side.
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

    // Fetches all jobs stored in the database, newest first.
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

    // Finds and returns a single job profile by its ID.
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

    // Saves a new job record to the database, gets the new auto-incremented ID, and notifies matched students.
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
        
        // ExecuteScalar executes query and returns the newly generated primary key ID.
        job.Id = Convert.ToInt32(command.ExecuteScalar());

        // Notify matching students about this new job
        NotifyMatchingStudents(job);
    }

    // Updates an existing job record and re-evaluates matches to notify potential new students.
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

        // Notify any students whose profiles match the updated job details
        NotifyMatchingStudents(job);
    }

    // Deletes a job listing and automatically purges all applications submitted to that job.
    public void DeleteJob(int jobId)
    {
        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        // 1. Delete associated applications first to respect foreign keys
        using var deleteApplications = connection.CreateCommand();
        deleteApplications.CommandText = "DELETE FROM Applications WHERE JobId = $jobId;";
        deleteApplications.Parameters.AddWithValue("$jobId", jobId);
        deleteApplications.ExecuteNonQuery();

        // 2. Delete the actual job record
        using var deleteJob = connection.CreateCommand();
        deleteJob.CommandText = "DELETE FROM Jobs WHERE Id = $jobId;";
        deleteJob.Parameters.AddWithValue("$jobId", jobId);
        deleteJob.ExecuteNonQuery();
    }

    // Helper method to add parameters to SQL commands, preventing injection attacks.
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

    // Scans all registered students and sends them alerts if their skills match this job.
    private void NotifyMatchingStudents(Job job)
    {
        // Only notify if the job posting is currently Active
        if (!job.IsActive)
        {
            return;
        }

        // Loop through all candidate students
        foreach (var candidate in GetStudentCandidates())
        {
            // Compute match percentage
            var matchPercentage = _matchingService.CalculateSkillMatchPercentage(candidate.Skills, job.RequiredSkills);
            if (matchPercentage <= 0)
            {
                continue; // No matching skills
            }

            var referenceKey = $"job:{job.Id}";
            // Skip if we already sent an alert for this job to this student
            if (_notificationManager.HasNotification(candidate.UserId, JobMatchNotificationType, referenceKey))
            {
                continue;
            }

            var message =
                $"A new job matches your profile: '{job.Title}' in {job.Location}. " +
                $"Your current skill match is {matchPercentage}%.";

            // Save the notification alert
            _notificationManager.CreateNotification(
                candidate.UserId,
                "New Job Match",
                message,
                notificationType: JobMatchNotificationType,
                referenceKey: referenceKey);
        }
    }

    // Fetches student user IDs and skills list from the DB to run matching scans.
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

    // Nested private class to hold candidate profiles for the notifier matching checks.
    private sealed class StudentMatchCandidate
    {
        public int UserId { get; set; }
        public string Skills { get; set; } = string.Empty;
    }
}

