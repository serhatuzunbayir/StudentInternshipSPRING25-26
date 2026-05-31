using Shared.Data;
using Shared.Services;
using StudentWeb.Models;
using System.Globalization;

namespace StudentWeb.Services;

public class NotificationQueryService
{
    private readonly NotificationManager _notificationManager;

    public NotificationQueryService(DatabaseHelper databaseHelper)
    {
        _notificationManager = new NotificationManager(databaseHelper);
    }

    public NotificationListViewModel GetNotificationsForUser(int userId)
    {
        var notifications = _notificationManager.GetNotificationsForUser(userId);

        return new NotificationListViewModel
        {
            UnreadCount = notifications.Count(notification => !notification.IsRead),
            Notifications = notifications.Select(notification => new NotificationListItemViewModel
            {
                Id = notification.Id,
                Title = notification.Title,
                Message = notification.Message,
                IsRead = notification.IsRead,
                CreatedAt = notification.CreatedAt.ToLocalTime().ToString("dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture)
            }).ToList()
        };
    }

    public int GetUnreadCount(int userId)
    {
        return _notificationManager.GetNotificationsForUser(userId).Count(notification => !notification.IsRead);
    }

    public void MarkAsRead(int userId, int notificationId)
    {
        _notificationManager.MarkAsRead(userId, notificationId);
    }

    public void MarkAllAsRead(int userId)
    {
        _notificationManager.MarkAllAsRead(userId);
    }
}
