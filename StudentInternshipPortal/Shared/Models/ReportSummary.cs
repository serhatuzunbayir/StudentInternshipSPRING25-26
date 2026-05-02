namespace StudentInternshipJobPortal.Shared.Models;

public class ReportSummary
{
    public int TotalStudents { get; set; }
    public int TotalActiveJobs { get; set; }
    public int TotalApplications { get; set; }
    public int PendingApplications { get; set; }
    public int AcceptedApplications { get; set; }
    public int RejectedApplications { get; set; }
}
