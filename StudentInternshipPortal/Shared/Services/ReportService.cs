using StudentInternshipJobPortal.Shared.Constants;
using StudentInternshipJobPortal.Shared.Data;
using StudentInternshipJobPortal.Shared.Models;

namespace StudentInternshipJobPortal.Shared.Services;

public class ReportService
{
    public ReportSummary GetSummary()
    {
        using var db = new AppDbContext();
        return new ReportSummary
        {
            TotalStudents = db.Users.Count(x => x.Role == RoleNames.Student),
            TotalActiveJobs = db.Jobs.Count(x => x.IsActive),
            TotalApplications = db.Applications.Count(),
            PendingApplications = db.Applications.Count(x => x.Status == ApplicationStatuses.Pending),
            AcceptedApplications = db.Applications.Count(x => x.Status == ApplicationStatuses.Accepted),
            RejectedApplications = db.Applications.Count(x => x.Status == ApplicationStatuses.Rejected)
        };
    }
}
