using Shared.Data;
using Shared.Models;

namespace Shared.Services;

// A custom delegate signature. E.g. when a new notification is generated, this specifies who receives the alert.
public delegate void NotificationCreatedEventHandler(object? sender, Notification notification);

// This class manages user notifications in the database and fires events when they are created.
public class NotificationManager
{
    private readonly DatabaseHelper _databaseHelper;

    // Inject the database helper class
    public NotificationManager(DatabaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
    }

    // This event notifies subscribers (like the admin panel popup) when a new notification is created.
    public event NotificationCreatedEventHandler? NotificationCreated;

    // Creates a new notification record in the DB and raises the NotificationCreated event.
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

        // Open a new database connection
        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        // Insert notification details into database and select the auto-generated row ID
        command.CommandText =
            """
            INSERT INTO Notifications (UserId, Title, Message, NotificationType, ReferenceKey, IsRead, CreatedAt)
            VALUES ($userId, $title, $message, $notificationType, $referenceKey, $isRead, $createdAt);
            SELECT last_insert_rowid();
            """;
        // Use parameters to block SQL injections
        command.Parameters.AddWithValue("$userId", notification.UserId);
        command.Parameters.AddWithValue("$title", notification.Title);
        command.Parameters.AddWithValue("$message", notification.Message);
        command.Parameters.AddWithValue("$notificationType", notification.NotificationType);
        command.Parameters.AddWithValue("$referenceKey", notification.ReferenceKey);
        command.Parameters.AddWithValue("$isRead", notification.IsRead ? 1 : 0);
        command.Parameters.AddWithValue("$createdAt", notification.CreatedAt.ToString("O"));
        
        // Execute and set the newly generated ID
        notification.Id = Convert.ToInt32(command.ExecuteScalar());

        // Invoke the NotificationCreated event to let any listening interfaces (like admin dashboard) show popup
        NotificationCreated?.Invoke(this, notification);
        return notification;
    }

    // Fetches all notifications belonging to a specific user, sorted from newest to oldest.
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

        // Execute reader to read rows sequentially
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
                // Parse date string back into standard DateTime format
                CreatedAt = DateTime.Parse(reader.GetString(7), null, System.Globalization.DateTimeStyles.RoundtripKind)
            });
        }

        return items;
    }

    // Checks if a specific notification already exists in the database.
    // We use this to avoid sending duplicate alerts for the same job matching.
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

        // If count is greater than 0, it exists
        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    // Marks a specific notification as read by setting the IsRead flag to 1.
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

    // Marks all unread notifications of a user as read.
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

