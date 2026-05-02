using Shared.Data;
using Shared.Services;

namespace StudentWeb.Services;

public class NotificationQueryService
{
    private readonly NotificationManager _notificationManager = new(new DatabaseHelper());

    public string Description => $"Placeholder notification query service using {_notificationManager.GetType().Name}.";
}
