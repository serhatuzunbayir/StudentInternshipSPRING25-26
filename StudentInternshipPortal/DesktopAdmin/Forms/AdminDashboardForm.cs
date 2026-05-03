using DesktopAdmin.Services;
using DesktopAdmin.ViewModels;
using Shared.Data;
using Shared.Enums;
using Shared.Services;

namespace DesktopAdmin.Forms;

public partial class AdminDashboardForm : Form
{
    private readonly JobService _jobService;
    private readonly ApplicationService _applicationService;
    private readonly ReportService _reportService;
    private readonly NotificationManager _notificationManager;

    public AdminDashboardForm(DatabaseHelper databaseHelper, int adminUserId, string adminUsername)
    {
        _jobService = new JobService(databaseHelper);
        _notificationManager = new NotificationManager(databaseHelper);
        _applicationService = new ApplicationService(databaseHelper, _notificationManager);
        _reportService = new ReportService(databaseHelper);

        InitializeComponent();
        lblWelcome.Text = $"Logged in as: {adminUsername}";
        ApplyGridTheme(dgvJobs);
        ApplyGridTheme(dgvApplications);
        _notificationManager.NotificationCreated += NotificationManager_NotificationCreated;
        LoadDashboardData();
    }

    private static void ApplyGridTheme(DataGridView grid)
    {
        grid.EnableHeadersVisualStyles = false;
        grid.GridColor = Color.FromArgb(228, 234, 244);
        grid.RowTemplate.Height = 36;
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(233, 239, 249);
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(43, 53, 79);
        grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        grid.DefaultCellStyle.BackColor = Color.White;
        grid.DefaultCellStyle.ForeColor = Color.FromArgb(48, 58, 84);
        grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(213, 229, 255);
        grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(27, 38, 66);
        grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(247, 250, 255);
        grid.RowHeadersVisible = false;
    }

    private void LoadDashboardData()
    {
        LoadJobs();
        LoadApplications();
        LoadReports();
    }

    private void LoadJobs()
    {
        dgvJobs.AutoGenerateColumns = true;
        dgvJobs.DataSource = _jobService.GetAllJobs();
    }

    private void LoadApplications()
    {
        dgvApplications.AutoGenerateColumns = true;
        dgvApplications.DataSource = _applicationService.GetAllApplications();
    }

    private void LoadReports()
    {
        var summary = _reportService.GetSummary();
        lblTotalStudentsValue.Text = summary.TotalStudents.ToString();
        lblActiveJobsValue.Text = summary.ActiveJobs.ToString();
        lblTotalApplicationsValue.Text = summary.TotalApplications.ToString();
        lblAcceptedValue.Text = summary.AcceptedApplications.ToString();
        lblRejectedValue.Text = summary.RejectedApplications.ToString();
        lblPendingValue.Text = summary.PendingApplications.ToString();
    }

    private void btnAddJob_Click(object sender, EventArgs e)
    {
        using var form = new JobEditForm();
        if (form.ShowDialog(this) != DialogResult.OK || form.JobResult is null)
        {
            return;
        }

        _jobService.AddJob(form.JobResult);
        LoadJobs();
        LoadReports();
        MessageBox.Show("Job created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnEditJob_Click(object sender, EventArgs e)
    {
        var selectedItem = GetSelectedJobRow();
        if (selectedItem is null)
        {
            MessageBox.Show("Select a job first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var job = _jobService.GetJobById(selectedItem.Id);
        if (job is null)
        {
            MessageBox.Show("Selected job could not be found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        using var form = new JobEditForm(job);
        if (form.ShowDialog(this) != DialogResult.OK || form.JobResult is null)
        {
            return;
        }

        _jobService.UpdateJob(form.JobResult);
        LoadJobs();
        LoadReports();
        MessageBox.Show("Job updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnDeleteJob_Click(object sender, EventArgs e)
    {
        var selectedItem = GetSelectedJobRow();
        if (selectedItem is null)
        {
            MessageBox.Show("Select a job first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var result = MessageBox.Show(
            $"Delete '{selectedItem.Title}' and its related applications?",
            "Confirm Delete",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (result != DialogResult.Yes)
        {
            return;
        }

        _jobService.DeleteJob(selectedItem.Id);
        LoadDashboardData();
    }

    private void btnAccept_Click(object sender, EventArgs e)
    {
        UpdateApplicationStatus(ApplicationStatus.Accepted);
    }

    private void btnReject_Click(object sender, EventArgs e)
    {
        UpdateApplicationStatus(ApplicationStatus.Rejected);
    }

    private void btnMarkPending_Click(object sender, EventArgs e)
    {
        UpdateApplicationStatus(ApplicationStatus.Pending);
    }

    private void btnRefreshReports_Click(object sender, EventArgs e)
    {
        LoadReports();
    }

    private void UpdateApplicationStatus(ApplicationStatus status)
    {
        var selectedApplication = GetSelectedApplicationRow();
        if (selectedApplication is null)
        {
            MessageBox.Show("Select an application first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        _applicationService.UpdateStatus(selectedApplication.Id, status);
        LoadApplications();
        LoadReports();
    }

    private JobListItemViewModel? GetSelectedJobRow()
    {
        return dgvJobs.CurrentRow?.DataBoundItem as JobListItemViewModel;
    }

    private ApplicationListItemViewModel? GetSelectedApplicationRow()
    {
        return dgvApplications.CurrentRow?.DataBoundItem as ApplicationListItemViewModel;
    }

    private void NotificationManager_NotificationCreated(object? sender, Shared.Models.Notification notification)
    {
        MessageBox.Show(notification.Message, notification.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
