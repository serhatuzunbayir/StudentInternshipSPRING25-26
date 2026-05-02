using Shared.Enums;
using Shared.Models;

namespace DesktopAdmin.Forms;

public partial class JobEditForm : Form
{
    private readonly Job? _existingJob;

    public JobEditForm(Job? job = null)
    {
        _existingJob = job;
        InitializeComponent();
        PopulateJobTypes();
        LoadExistingValues();
    }

    public Job? JobResult { get; private set; }

    private void PopulateJobTypes()
    {
        cmbJobType.DataSource = Enum.GetValues(typeof(JobType));
    }

    private void LoadExistingValues()
    {
        if (_existingJob is null)
        {
            cmbJobType.SelectedItem = JobType.Internship;
            chkIsActive.Checked = true;
            return;
        }

        txtTitle.Text = _existingJob.Title;
        txtDescription.Text = _existingJob.Description;
        txtRequiredSkills.Text = _existingJob.RequiredSkills;
        txtLocation.Text = _existingJob.Location;
        cmbJobType.SelectedItem = _existingJob.JobType;
        chkIsActive.Checked = _existingJob.IsActive;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtTitle.Text) ||
            string.IsNullOrWhiteSpace(txtRequiredSkills.Text) ||
            string.IsNullOrWhiteSpace(txtLocation.Text))
        {
            lblError.Text = "Title, required skills and location are required.";
            return;
        }

        JobResult = new Job
        {
            Id = _existingJob?.Id ?? 0,
            Title = txtTitle.Text.Trim(),
            Description = txtDescription.Text.Trim(),
            RequiredSkills = txtRequiredSkills.Text.Trim(),
            Location = txtLocation.Text.Trim(),
            JobType = cmbJobType.SelectedItem is JobType selectedJobType ? selectedJobType : JobType.Internship,
            IsActive = chkIsActive.Checked,
            CreatedAt = _existingJob?.CreatedAt ?? DateTime.UtcNow
        };

        DialogResult = DialogResult.OK;
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
