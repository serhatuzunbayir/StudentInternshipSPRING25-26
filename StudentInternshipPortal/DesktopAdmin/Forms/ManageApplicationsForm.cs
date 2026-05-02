using StudentInternshipJobPortal.Shared.Constants;
using StudentInternshipJobPortal.Shared.Helpers;
using StudentInternshipJobPortal.Shared.Models;
using StudentInternshipJobPortal.Shared.Services;

namespace StudentInternshipJobPortal.DesktopAdmin.Forms;

public partial class ManageApplicationsForm : Form
{
    private readonly ApplicationService _applicationService = new();

    public ManageApplicationsForm()
    {
        InitializeComponent();
        cboStatus.Items.AddRange(ApplicationStatuses.All);
        NotificationManager.ApplicationNotificationRaised += NotificationManager_ApplicationNotificationRaised;
        LoadApplications();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        NotificationManager.ApplicationNotificationRaised -= NotificationManager_ApplicationNotificationRaised;
        base.OnFormClosed(e);
    }

    private void NotificationManager_ApplicationNotificationRaised(object sender, string message)
    {
        MessageBox.Show(message, "Application Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void LoadApplications()
    {
        dgvApplications.DataSource = _applicationService.GetAllForAdmin();
        dgvApplications.ClearSelection();
    }

    private void BtnRefresh_Click(object? sender, EventArgs e)
    {
        LoadApplications();
    }

    private void BtnUpdateStatus_Click(object? sender, EventArgs e)
    {
        if (dgvApplications.SelectedRows.Count == 0)
        {
            MessageBox.Show("Select an application first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (cboStatus.SelectedItem is null)
        {
            MessageBox.Show("Choose a new status.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (dgvApplications.SelectedRows[0].DataBoundItem is not AdminApplicationListItem item)
        {
            return;
        }

        _applicationService.UpdateStatus(item.Id, cboStatus.SelectedItem.ToString()!);
        LoadApplications();
    }
}
