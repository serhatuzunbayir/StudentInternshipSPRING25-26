using StudentInternshipJobPortal.Shared.Constants;
using StudentInternshipJobPortal.Shared.Helpers;
using StudentInternshipJobPortal.Shared.Models;
using StudentInternshipJobPortal.Shared.Services;

namespace StudentInternshipJobPortal.DesktopAdmin.Forms;

public partial class ManageJobsForm : Form
{
    private readonly JobService _jobService = new();
    private int? _selectedJobId;

    public ManageJobsForm()
    {
        InitializeComponent();
        cboJobType.Items.AddRange(JobTypes.All);
        NotificationManager.JobNotificationRaised += NotificationManager_JobNotificationRaised;
        LoadJobs();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        NotificationManager.JobNotificationRaised -= NotificationManager_JobNotificationRaised;
        base.OnFormClosed(e);
    }

    private void NotificationManager_JobNotificationRaised(object sender, string message)
    {
        MessageBox.Show(message, "Job Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void LoadJobs()
    {
        dgvJobs.DataSource = _jobService.GetAllJobs();
        dgvJobs.ClearSelection();
    }

    private void BtnNew_Click(object? sender, EventArgs e)
    {
        ClearForm();
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        if (!ValidateForm())
        {
            return;
        }

        var job = BuildJobFromForm();
        if (_selectedJobId.HasValue)
        {
            job.Id = _selectedJobId.Value;
            _jobService.Update(job);
        }
        else
        {
            _jobService.Add(job);
        }

        LoadJobs();
        ClearForm();
    }

    private void BtnDelete_Click(object? sender, EventArgs e)
    {
        if (!_selectedJobId.HasValue)
        {
            MessageBox.Show("Select a job first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var result = MessageBox.Show("Delete the selected job?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (result != DialogResult.Yes)
        {
            return;
        }

        _jobService.Delete(_selectedJobId.Value);
        LoadJobs();
        ClearForm();
    }

    private void DgvJobs_SelectionChanged(object? sender, EventArgs e)
    {
        if (dgvJobs.SelectedRows.Count == 0)
        {
            return;
        }

        if (dgvJobs.SelectedRows[0].DataBoundItem is not JobGridItem item)
        {
            return;
        }

        var job = _jobService.GetById(item.Id);
        if (job is null)
        {
            return;
        }

        _selectedJobId = job.Id;
        txtTitle.Text = job.Title;
        txtCompanyName.Text = job.CompanyName;
        txtLocation.Text = job.Location;
        cboJobType.SelectedItem = job.JobType;
        txtRequiredSkills.Text = job.RequiredSkills;
        txtDescription.Text = job.Description;
        chkIsActive.Checked = job.IsActive;
    }

    private bool ValidateForm()
    {
        if (string.IsNullOrWhiteSpace(txtTitle.Text) ||
            string.IsNullOrWhiteSpace(txtCompanyName.Text) ||
            string.IsNullOrWhiteSpace(txtLocation.Text) ||
            string.IsNullOrWhiteSpace(txtRequiredSkills.Text) ||
            cboJobType.SelectedItem is null)
        {
            MessageBox.Show("Fill in all required fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        return true;
    }

    private Job BuildJobFromForm()
    {
        return new Job
        {
            Title = txtTitle.Text.Trim(),
            CompanyName = txtCompanyName.Text.Trim(),
            Location = txtLocation.Text.Trim(),
            JobType = cboJobType.SelectedItem?.ToString() ?? string.Empty,
            RequiredSkills = txtRequiredSkills.Text.Trim(),
            Description = txtDescription.Text.Trim(),
            IsActive = chkIsActive.Checked
        };
    }

    private void ClearForm()
    {
        _selectedJobId = null;
        txtTitle.Clear();
        txtCompanyName.Clear();
        txtLocation.Clear();
        txtRequiredSkills.Clear();
        txtDescription.Clear();
        cboJobType.SelectedIndex = -1;
        chkIsActive.Checked = true;
        dgvJobs.ClearSelection();
    }
}
