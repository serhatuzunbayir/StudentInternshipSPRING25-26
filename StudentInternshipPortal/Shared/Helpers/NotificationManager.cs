namespace StudentInternshipJobPortal.Shared.Helpers;

public delegate void AdminNotificationHandler(object sender, string message);

public static class NotificationManager
{
    public static event AdminNotificationHandler? JobNotificationRaised;
    public static event AdminNotificationHandler? ApplicationNotificationRaised;

    public static void RaiseJobNotification(object sender, string message)
    {
        JobNotificationRaised?.Invoke(sender, message);
    }

    public static void RaiseApplicationNotification(object sender, string message)
    {
        ApplicationNotificationRaised?.Invoke(sender, message);
    }
}
