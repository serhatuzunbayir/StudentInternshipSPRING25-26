using Microsoft.EntityFrameworkCore;
using StudentInternshipJobPortal.Shared.Data;
using StudentInternshipJobPortal.Shared.Helpers;
using StudentInternshipJobPortal.Shared.Models;

namespace StudentInternshipJobPortal.Shared.Services;

public class ApplicationService
{
    private readonly NotificationService _notificationService = new();

    public List<AdminApplicationListItem> GetAllForAdmin()
    {
        using var db = new AppDbContext();
        return db.Applications
            .Include(x => x.Student)
            .Include(x => x.Job)
            .OrderByDescending(x => x.ApplicationDate)
            .Select(x => new AdminApplicationListItem
            {
                Id = x.Id,
                StudentName = x.Student.NameSurname,
                Username = x.Student.Username,
                JobTitle = x.Job.Title,
                CompanyName = x.Job.CompanyName,
                Status = x.Status,
                ApplicationDate = x.ApplicationDate
            })
            .ToList();
    }

    public void UpdateStatus(int applicationId, string newStatus)
    {
        using var db = new AppDbContext();
        var application = db.Applications
            .Include(x => x.Student)
            .Include(x => x.Job)
            .First(x => x.Id == applicationId);

        application.Status = newStatus;
        db.SaveChanges();

        string message = $"Your application for '{application.Job.Title}' at '{application.Job.CompanyName}' is now {newStatus}.";
        _notificationService.AddNotification(application.StudentId, message);
        NotificationManager.RaiseApplicationNotification(this, $"Application for '{application.Student.NameSurname}' was updated to {newStatus}.");
    }
}
