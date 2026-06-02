using Shared.Enums;
using Shared.Models;

namespace DesktopAdmin.Forms;

// This Windows Form is used to add a new Job or edit an existing Job listing.
public partial class JobEditForm : Form
{
    private readonly Job? _existingJob;

    // Constructor accepts an optional Job object. If provided, we are editing; if null, we are creating.
    public JobEditForm(Job? job = null)
    {
        _existingJob = job;
        InitializeComponent();
        PopulateJobTypes();
        LoadExistingValues();
    }

    // Stores the output job data when the user clicks save.
    public Job? JobResult { get; private set; }

    // Binds the JobType enum options (Internship, FullTime, etc.) to the combobox drop-down.
    private void PopulateJobTypes()
    {
        cmbJobType.DataSource = Enum.GetValues(typeof(JobType));
    }

    // Fills the form fields with existing job data if we are editing.
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

    // Event handler when Save button is clicked. Validates fields and packs data into JobResult.
    private void btnSave_Click(object sender, EventArgs e)
    {
        // Simple input validation checks
        if (string.IsNullOrWhiteSpace(txtTitle.Text) ||
            string.IsNullOrWhiteSpace(txtRequiredSkills.Text) ||
            string.IsNullOrWhiteSpace(txtLocation.Text))
        {
            lblError.Text = "Title, required skills and location are required.";
            return;
        }

        // Map inputs to the Job object
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

        // Set DialogResult to OK to tell parent form that saving succeeded
        DialogResult = DialogResult.OK;
        Close();
    }

    // Cancel editing and close form window.
    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}

