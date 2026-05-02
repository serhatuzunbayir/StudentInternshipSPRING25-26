using StudentInternshipJobPortal.Shared.Data;
using StudentInternshipJobPortal.Shared.Models;

namespace StudentInternshipJobPortal.Shared.Services;

public class NotificationService
{
    public void AddNotification(int userId, string message)
    {
        using var db = new AppDbContext();
        db.Notifications.Add(new Notification
        {
            UserId = userId,
            Message = message,
            IsRead = false,
            CreatedDate = DateTime.UtcNow
        });
        db.SaveChanges();
    }
}
