namespace StudentWeb.Models;

public class HomeViewModel
{
    public bool IsAuthenticated { get; set; }
    public int ProfileMatchPercentage { get; set; }
    public int OpenRolesCount { get; set; }
    public int PendingReviewCount { get; set; }
    public int TotalApplicationsCount { get; set; }
    public int TotalNotificationsCount { get; set; }
    public int UnreadNotificationsCount { get; set; }
}
