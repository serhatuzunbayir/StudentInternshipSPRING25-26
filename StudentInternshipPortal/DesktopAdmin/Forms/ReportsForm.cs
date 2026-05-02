using StudentInternshipJobPortal.Shared.Services;

namespace StudentInternshipJobPortal.DesktopAdmin.Forms;

public partial class ReportsForm : Form
{
    private readonly ReportService _reportService = new();

    public ReportsForm()
    {
        InitializeComponent();
        LoadSummary();
    }

    private void BtnRefresh_Click(object? sender, EventArgs e)
    {
        LoadSummary();
    }

    private void LoadSummary()
    {
        var summary = _reportService.GetSummary();
        lblStudentsValue.Text = summary.TotalStudents.ToString();
        lblActiveJobsValue.Text = summary.TotalActiveJobs.ToString();
        lblApplicationsValue.Text = summary.TotalApplications.ToString();
        lblPendingValue.Text = summary.PendingApplications.ToString();
        lblAcceptedValue.Text = summary.AcceptedApplications.ToString();
        lblRejectedValue.Text = summary.RejectedApplications.ToString();
    }
}
