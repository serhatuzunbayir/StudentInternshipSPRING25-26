namespace StudentInternshipJobPortal.DesktopAdmin.Forms;

public partial class DashboardForm : Form
{
    private readonly int _adminId;
    private readonly string _adminName;

    public DashboardForm(int adminId, string adminName)
    {
        _adminId = adminId;
        _adminName = string.IsNullOrWhiteSpace(adminName) ? "Admin" : adminName;
        InitializeComponent();
        lblWelcome.Text = $"Welcome, {_adminName}";
    }

    private void BtnManageJobs_Click(object? sender, EventArgs e)
    {
        using var form = new ManageJobsForm();
        form.ShowDialog();
    }

    private void BtnManageApplications_Click(object? sender, EventArgs e)
    {
        using var form = new ManageApplicationsForm();
        form.ShowDialog();
    }

    private void BtnReports_Click(object? sender, EventArgs e)
    {
        using var form = new ReportsForm();
        form.ShowDialog();
    }

    private void BtnLogout_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
