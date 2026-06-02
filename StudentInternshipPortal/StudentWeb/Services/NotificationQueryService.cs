using Shared.Data;
using Shared.Services;
using StudentWeb.Models;
using System.Globalization;

namespace StudentWeb.Services;

// This service processes notification queries for students on the web frontend.
public class NotificationQueryService
{
    private readonly NotificationManager _notificationManager;

    public NotificationQueryService(DatabaseHelper databaseHelper)
    {
        _notificationManager = new NotificationManager(databaseHelper);
    }

    // Fetches the notifications list for a student, returning counts and mapped items.
    public NotificationListViewModel GetNotificationsForUser(int userId)
    {
        var notifications = _notificationManager.GetNotificationsForUser(userId);

        return new NotificationListViewModel
        {
            // LINQ Count checks how many notifications are unread (IsRead is false)
            UnreadCount = notifications.Count(notification => !notification.IsRead),
            // LINQ Select translates database models to display view models
            Notifications = notifications.Select(notification => new NotificationListItemViewModel
            {
                Id = notification.Id,
                Title = notification.Title,
                Message = notification.Message,
                IsRead = notification.IsRead,
                // Formats the timestamp nicely for display
                CreatedAt = notification.CreatedAt.ToLocalTime().ToString("dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture)
            }).ToList() // Output as a List
        };
    }

    // Counts unread notifications using LINQ Count.
    public int GetUnreadCount(int userId)
    {
        return _notificationManager.GetNotificationsForUser(userId).Count(notification => !notification.IsRead);
    }

    // Returns total notification count.
    public int GetNotificationCount(int userId)
    {
        return _notificationManager.GetNotificationsForUser(userId).Count;
    }

    // Marks a specific notification as read.
    public void MarkAsRead(int userId, int notificationId)
    {
        _notificationManager.MarkAsRead(userId, notificationId);
    }

    // Marks all notifications as read.
    public void MarkAllAsRead(int userId)
    {
        _notificationManager.MarkAllAsRead(userId);
    }
}

