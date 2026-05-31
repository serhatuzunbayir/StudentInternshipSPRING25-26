using Shared.Data;
using Shared.Enums;
using Shared.Models;
using Shared.Services;
using StudentWeb.Models;

namespace StudentWeb.Services;

public class JobBrowseService
{
    private readonly DatabaseHelper _databaseHelper;
    private readonly MatchingService _matchingService = new();

    public JobBrowseService(DatabaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
    }

    public List<JobSearchResultViewModel> SearchJobs(int userId, JobSearchViewModel filters)
    {
        var studentSkills = GetStudentSkills(userId);
        var jobs = GetActiveJobs();
        IEnumerable<JobSearchResultViewModel> query = jobs.Select(job => new JobSearchResultViewModel
        {
            Id = job.Id,
            Title = job.Title,
            Description = job.Description,
            RequiredSkills = job.RequiredSkills,
            Location = job.Location,
            JobType = job.JobType.ToString(),
            MatchPercentage = _matchingService.CalculateSkillMatchPercentage(studentSkills, job.RequiredSkills)
        });

        if (!string.IsNullOrWhiteSpace(filters.Skill))
        {
            query = query.Where(job =>
                job.RequiredSkills.Contains(filters.Skill, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filters.Location))
        {
            query = query.Where(job =>
                job.Location.Contains(filters.Location, StringComparison.OrdinalIgnoreCase));
        }

        if (Enum.TryParse<JobType>(filters.JobType, true, out var selectedJobType))
        {
            query = query.Where(job =>
                job.JobType.Equals(selectedJobType.ToString(), StringComparison.OrdinalIgnoreCase));
        }

        return query
            .OrderByDescending(job => job.MatchPercentage)
            .ThenBy(job => job.Title)
            .ToList();
    }

    private string GetStudentSkills(int userId)
    {
        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT Skills
            FROM StudentProfiles
            WHERE UserId = $userId;
            """;
        command.Parameters.AddWithValue("$userId", userId);

        return command.ExecuteScalar()?.ToString() ?? string.Empty;
    }

    private List<Job> GetActiveJobs()
    {
        var jobs = new List<Job>();

        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT Id, Title, Description, RequiredSkills, Location, JobType, IsActive, CreatedAt
            FROM Jobs
            WHERE IsActive = 1
            ORDER BY CreatedAt DESC;
            """;

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            jobs.Add(new Job
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                Description = reader.GetString(2),
                RequiredSkills = reader.GetString(3),
                Location = reader.GetString(4),
                JobType = (JobType)reader.GetInt32(5),
                IsActive = reader.GetBoolean(6),
                CreatedAt = DateTime.TryParse(reader.GetString(7), out var createdAt)
                    ? createdAt
                    : DateTime.UtcNow
            });
        }

        return jobs;
    }
}
