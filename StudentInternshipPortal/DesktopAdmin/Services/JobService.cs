using Microsoft.Data.Sqlite;
using DesktopAdmin.ViewModels;
using Shared.Data;
using Shared.Enums;
using Shared.Models;

namespace DesktopAdmin.Services;

public class JobService
{
    private readonly DatabaseHelper _databaseHelper;

    public JobService(DatabaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
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
            """;
        FillJobParameters(command, job);
        command.ExecuteNonQuery();
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
}
