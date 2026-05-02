using StudentInternshipJobPortal.Shared.Data;
using StudentInternshipJobPortal.Shared.Helpers;
using StudentInternshipJobPortal.Shared.Models;

namespace StudentInternshipJobPortal.Shared.Services;

public class JobService
{
    public List<JobGridItem> GetAllJobs()
    {
        using var db = new AppDbContext();
        return db.Jobs
            .OrderByDescending(x => x.Id)
            .Select(x => new JobGridItem
            {
                Id = x.Id,
                Title = x.Title,
                CompanyName = x.CompanyName,
                Location = x.Location,
                JobType = x.JobType,
                RequiredSkills = x.RequiredSkills,
                IsActive = x.IsActive
            })
            .ToList();
    }

    public Job? GetById(int id)
    {
        using var db = new AppDbContext();
        return db.Jobs.FirstOrDefault(x => x.Id == id);
    }

    public void Add(Job job)
    {
        using var db = new AppDbContext();
        db.Jobs.Add(job);
        db.SaveChanges();
        NotificationManager.RaiseJobNotification(this, $"Job '{job.Title}' was added successfully.");
    }

    public void Update(Job job)
    {
        using var db = new AppDbContext();
        var existing = db.Jobs.First(x => x.Id == job.Id);
        existing.Title = job.Title;
        existing.CompanyName = job.CompanyName;
        existing.Location = job.Location;
        existing.JobType = job.JobType;
        existing.RequiredSkills = job.RequiredSkills;
        existing.Description = job.Description;
        existing.IsActive = job.IsActive;
        db.SaveChanges();
        NotificationManager.RaiseJobNotification(this, $"Job '{job.Title}' was updated.");
    }

    public void Delete(int id)
    {
        using var db = new AppDbContext();
        var job = db.Jobs.First(x => x.Id == id);
        string title = job.Title;
        db.Jobs.Remove(job);
        db.SaveChanges();
        NotificationManager.RaiseJobNotification(this, $"Job '{title}' was deleted.");
    }
}
