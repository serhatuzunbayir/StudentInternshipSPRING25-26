using Shared.Data;
using Shared.Services;

namespace StudentWeb.Services;

public class NotificationQueryService
{
    private readonly NotificationManager _notificationManager;

    public NotificationQueryService(DatabaseHelper databaseHelper)
    {
        _notificationManager = new NotificationManager(databaseHelper);
    }

    public string Description => $"Placeholder notification query service using {_notificationManager.GetType().Name}.";
}
