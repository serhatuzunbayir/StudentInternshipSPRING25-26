namespace DesktopAdmin.ViewModels;

public class ReportSummaryViewModel
{
    public int TotalStudents { get; set; }
    public int ActiveJobs { get; set; }
    public int TotalApplications { get; set; }
    public int AcceptedApplications { get; set; }
    public int RejectedApplications { get; set; }
    public int PendingApplications { get; set; }
}
