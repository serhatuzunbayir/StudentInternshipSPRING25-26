using DesktopAdmin.Services;
using DesktopAdmin.ViewModels;
using Shared.Data;
using Shared.Enums;
using Shared.Services;
using Shared.Utilities;



namespace DesktopAdmin.Forms;

public partial class AdminDashboardForm : Form
{
    private readonly JobService _jobService;
    private readonly ApplicationService _applicationService;
    private readonly ReportService _reportService;
    private readonly NotificationManager _notificationManager;
    private List<JobListItemViewModel> _jobItems = [];
    private List<ApplicationListItemViewModel> _applicationItems = [];
    private readonly AuditLogger _auditLogger = new();

 


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



        // Wire cell double click on application grid
        dgvApplications.CellDoubleClick += dgvApplications_CellDoubleClick;

        _notificationManager.NotificationCreated += NotificationManager_NotificationCreated;
        _auditLogger.AdminActionPerformed += action =>
        {
            lstAuditLog.Items.Insert(0, $"{DateTime.Now:HH:mm:ss} - {action}");
        };
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

    private static void ConfigureIdColumns(DataGridView grid)
    {
        foreach (DataGridViewColumn column in grid.Columns)
        {
            if (column.Name.Equals("StudentId", StringComparison.OrdinalIgnoreCase) ||
                column.Name.Equals("ResumeFileName", StringComparison.OrdinalIgnoreCase))
            {
                column.Visible = false;
                continue;
            }

            if (column.Name.Equals("MatchPercentage", StringComparison.OrdinalIgnoreCase))
            {
                column.HeaderText = "Match %";
                column.FillWeight = 60;
                column.MinimumWidth = 65;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                continue;
            }

            if (column.Name.Equals("CVSource", StringComparison.OrdinalIgnoreCase))
            {
                column.HeaderText = "CV Source";
                column.FillWeight = 70;
                column.MinimumWidth = 75;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                continue;
            }

            if (column.Name.Contains("Id", StringComparison.OrdinalIgnoreCase))
            {
                column.FillWeight = 48;
                column.MinimumWidth = 58;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                continue;
            }

            if (column.Name.Contains("Name", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(column.Name, "Title", StringComparison.OrdinalIgnoreCase))
            {
                column.FillWeight = 82;
                column.MinimumWidth = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
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
        _auditLogger.Log($"Added job: {form.JobResult.Title}");
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
        _auditLogger.Log($"Edited job: {form.JobResult.Title}");
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
        _auditLogger.Log($"Deleted job: {selectedItem.Title}");

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
                SearchTextHelper.Contains(item.Title, searchText) ||
                SearchTextHelper.Contains(item.Location, searchText) ||
                SearchTextHelper.Contains(item.RequiredSkills, searchText));
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
        ConfigureIdColumns(dgvJobs);
    }

    private void ApplyApplicationFilters()
    {
        IEnumerable<ApplicationListItemViewModel> query = _applicationItems;

        var searchText = txtApplicationSearch.Text.Trim();
        if (!string.IsNullOrWhiteSpace(searchText))
        {
            query = query.Where(item =>
                SearchTextHelper.Contains(item.StudentName, searchText) ||
                SearchTextHelper.Contains(item.JobTitle, searchText));
        }

        var selectedStatus = cmbApplicationStatusFilter.SelectedItem?.ToString();
        if (!string.IsNullOrWhiteSpace(selectedStatus) && selectedStatus != "All Statuses")
        {
            query = query.Where(item => item.Status.Equals(selectedStatus, StringComparison.OrdinalIgnoreCase));
        }

        dgvApplications.AutoGenerateColumns = true;
        dgvApplications.DataSource = query.ToList();
        ConfigureIdColumns(dgvApplications);
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

    private void btnRefreshApplications_Click(object sender, EventArgs e)
    {
        LoadApplications();
        LoadReports();
    }

    private void dgvApplications_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            dgvApplications.Rows[e.RowIndex].Selected = true;
        }
    }


    private void dgvApplications_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            OpenApplicantDetails();
        }
    }

    private void OpenApplicantDetails()
    {
        var selectedApplication = GetSelectedApplicationRow();
        if (selectedApplication is null)
        {
            MessageBox.Show("Select an application first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var profileDetails = _applicationService.GetStudentProfileDetails(selectedApplication.StudentId);
        if (profileDetails is null)
        {
            MessageBox.Show("Could not load student profile details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        using var detailsForm = new ApplicantDetailsForm(
            profileDetails,
            selectedApplication.JobTitle,
            selectedApplication.MatchPercentage,
            selectedApplication.ResumeFileName);
        
        detailsForm.ShowDialog(this);
    }
}
