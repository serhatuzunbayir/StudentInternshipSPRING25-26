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
    private List<JobListItemViewModel> _jobItems = [];
    private List<ApplicationListItemViewModel> _applicationItems = [];

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

    private void AdminDashboardForm_Load(object sender, EventArgs e)
    {
        cmbJobTypeFilter.SelectedIndex = 0;
        cmbJobStatusFilter.SelectedIndex = 0;
        cmbApplicationStatusFilter.SelectedIndex = 0;
    }

    private void LoadJobs()
    {
        _jobItems = _jobService.GetAllJobs();
        ApplyJobFilters();
    }

    private void LoadApplications()
    {
        _applicationItems = _applicationService.GetAllApplications();
        ApplyApplicationFilters();
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

    private void ApplyJobFilters()
    {
        IEnumerable<JobListItemViewModel> query = _jobItems;

        var searchText = txtJobSearch.Text.Trim();
        if (!string.IsNullOrWhiteSpace(searchText))
        {
            query = query.Where(item =>
                item.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                item.Location.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                item.RequiredSkills.Contains(searchText, StringComparison.OrdinalIgnoreCase));
        }

        var selectedJobType = cmbJobTypeFilter.SelectedItem?.ToString();
        if (!string.IsNullOrWhiteSpace(selectedJobType) && selectedJobType != "All Types")
        {
            query = query.Where(item => item.JobType.Equals(selectedJobType, StringComparison.OrdinalIgnoreCase));
        }

        var selectedActiveStatus = cmbJobStatusFilter.SelectedItem?.ToString();
        if (!string.IsNullOrWhiteSpace(selectedActiveStatus) && selectedActiveStatus != "All Statuses")
        {
            query = query.Where(item => item.ActiveStatus.Equals(selectedActiveStatus, StringComparison.OrdinalIgnoreCase));
        }

        dgvJobs.AutoGenerateColumns = true;
        dgvJobs.DataSource = query.ToList();
    }

    private void ApplyApplicationFilters()
    {
        IEnumerable<ApplicationListItemViewModel> query = _applicationItems;

        var searchText = txtApplicationSearch.Text.Trim();
        if (!string.IsNullOrWhiteSpace(searchText))
        {
            query = query.Where(item =>
                item.StudentName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                item.JobTitle.Contains(searchText, StringComparison.OrdinalIgnoreCase));
        }

        var selectedStatus = cmbApplicationStatusFilter.SelectedItem?.ToString();
        if (!string.IsNullOrWhiteSpace(selectedStatus) && selectedStatus != "All Statuses")
        {
            query = query.Where(item => item.Status.Equals(selectedStatus, StringComparison.OrdinalIgnoreCase));
        }

        dgvApplications.AutoGenerateColumns = true;
        dgvApplications.DataSource = query.ToList();
    }

    private void txtJobSearch_TextChanged(object sender, EventArgs e)
    {
        ApplyJobFilters();
    }

    private void cmbJobTypeFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        ApplyJobFilters();
    }

    private void cmbJobStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        ApplyJobFilters();
    }

    private void btnClearJobFilters_Click(object sender, EventArgs e)
    {
        txtJobSearch.Clear();
        cmbJobTypeFilter.SelectedIndex = 0;
        cmbJobStatusFilter.SelectedIndex = 0;
        ApplyJobFilters();
    }

    private void txtApplicationSearch_TextChanged(object sender, EventArgs e)
    {
        ApplyApplicationFilters();
    }

    private void cmbApplicationStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        ApplyApplicationFilters();
    }

    private void btnClearApplicationFilters_Click(object sender, EventArgs e)
    {
        txtApplicationSearch.Clear();
        cmbApplicationStatusFilter.SelectedIndex = 0;
        ApplyApplicationFilters();
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
