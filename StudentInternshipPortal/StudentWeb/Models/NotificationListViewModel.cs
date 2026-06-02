namespace StudentWeb.Models;

public class NotificationListViewModel
{
    public List<NotificationListItemViewModel> Notifications { get; set; } = [];
    public int UnreadCount { get; set; }
}

public class NotificationListItemViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public string CreatedAt { get; set; } = string.Empty;
}
