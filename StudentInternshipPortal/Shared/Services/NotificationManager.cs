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

    public Notification CreateNotification(int userId, string title, string message)
    {
        var notification = new Notification
        {
            UserId = userId,
            Title = title,
            Message = message,
            IsRead = false,
            CreatedAt = DateTime.UtcNow
        };

        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            INSERT INTO Notifications (UserId, Title, Message, IsRead, CreatedAt)
            VALUES ($userId, $title, $message, $isRead, $createdAt);
            SELECT last_insert_rowid();
            """;
        command.Parameters.AddWithValue("$userId", notification.UserId);
        command.Parameters.AddWithValue("$title", notification.Title);
        command.Parameters.AddWithValue("$message", notification.Message);
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
            SELECT Id, UserId, Title, Message, IsRead, CreatedAt
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
                IsRead = reader.GetInt32(4) == 1,
                CreatedAt = DateTime.Parse(reader.GetString(5))
            });
        }

        return items;
    }
}
