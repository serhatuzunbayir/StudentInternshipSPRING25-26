using Shared.Data;
using Shared.Models;

namespace Shared.Services;

public delegate void NotificationCreatedEventHandler(object? sender, Notification notification);

public class NotificationManager
{
    private readonly DatabaseHelper _databaseHelper;

    public NotificationManager(DatabaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
    }

    public event NotificationCreatedEventHandler? NotificationCreated;

    public Notification CreateNotification(
        int userId,
        string title,
        string message,
        DateTime? createdAt = null,
        string? notificationType = null,
        string? referenceKey = null)
    {
        var notification = new Notification
        {
            UserId = userId,
            Title = title,
            Message = message,
            NotificationType = notificationType ?? string.Empty,
            ReferenceKey = referenceKey ?? string.Empty,
            IsRead = false,
            CreatedAt = createdAt ?? DateTime.UtcNow
        };

        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            INSERT INTO Notifications (UserId, Title, Message, NotificationType, ReferenceKey, IsRead, CreatedAt)
            VALUES ($userId, $title, $message, $notificationType, $referenceKey, $isRead, $createdAt);
            SELECT last_insert_rowid();
            """;
        command.Parameters.AddWithValue("$userId", notification.UserId);
        command.Parameters.AddWithValue("$title", notification.Title);
        command.Parameters.AddWithValue("$message", notification.Message);
        command.Parameters.AddWithValue("$notificationType", notification.NotificationType);
        command.Parameters.AddWithValue("$referenceKey", notification.ReferenceKey);
        command.Parameters.AddWithValue("$isRead", notification.IsRead ? 1 : 0);
        command.Parameters.AddWithValue("$createdAt", notification.CreatedAt.ToString("O"));
        notification.Id = Convert.ToInt32(command.ExecuteScalar());

        NotificationCreated?.Invoke(this, notification);
        return notification;
    }

    public List<Notification> GetNotificationsForUser(int userId)
    {
        var items = new List<Notification>();

        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT Id, UserId, Title, Message, NotificationType, ReferenceKey, IsRead, CreatedAt
            FROM Notifications
            WHERE UserId = $userId
            ORDER BY CreatedAt DESC;
            """;
        command.Parameters.AddWithValue("$userId", userId);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            items.Add(new Notification
            {
                Id = reader.GetInt32(0),
                UserId = reader.GetInt32(1),
                Title = reader.GetString(2),
                Message = reader.GetString(3),
                NotificationType = reader.GetString(4),
                ReferenceKey = reader.GetString(5),
                IsRead = reader.GetInt32(6) == 1,
                CreatedAt = DateTime.Parse(reader.GetString(7), null, System.Globalization.DateTimeStyles.RoundtripKind)
            });
        }

        return items;
    }

    public bool HasNotification(int userId, string notificationType, string referenceKey)
    {
        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COUNT(1)
            FROM Notifications
            WHERE UserId = $userId
              AND NotificationType = $notificationType
              AND ReferenceKey = $referenceKey;
            """;
        command.Parameters.AddWithValue("$userId", userId);
        command.Parameters.AddWithValue("$notificationType", notificationType);
        command.Parameters.AddWithValue("$referenceKey", referenceKey);

        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    public void MarkAsRead(int userId, int notificationId)
    {
        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            UPDATE Notifications
            SET IsRead = 1
            WHERE Id = $notificationId AND UserId = $userId;
            """;
        command.Parameters.AddWithValue("$notificationId", notificationId);
        command.Parameters.AddWithValue("$userId", userId);
        command.ExecuteNonQuery();
    }

    public void MarkAllAsRead(int userId)
    {
        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            UPDATE Notifications
            SET IsRead = 1
            WHERE UserId = $userId AND IsRead = 0;
            """;
        command.Parameters.AddWithValue("$userId", userId);
        command.ExecuteNonQuery();
    }
}
