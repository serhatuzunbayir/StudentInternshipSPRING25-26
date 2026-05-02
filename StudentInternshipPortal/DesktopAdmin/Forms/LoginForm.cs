using StudentInternshipJobPortal.Shared.Services;

namespace StudentInternshipJobPortal.DesktopAdmin.Forms;

public partial class LoginForm : Form
{
    private readonly AuthService _authService = new();

    public LoginForm()
    {
        InitializeComponent();
    }

    private void BtnLogin_Click(object? sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;

        string username = txtUsername.Text.Trim();
        string password = txtPassword.Text.Trim();

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            lblStatus.Text = "Please enter username and password.";
            return;
        }

        var admin = _authService.AuthenticateAdmin(username, password);
        if (admin is null)
        {
            lblStatus.Text = "Admin login failed. Check your credentials.";
            return;
        }

        Hide();
        using var dashboard = new DashboardForm(admin.Id, admin.NameSurname);
        dashboard.ShowDialog();
        Show();
        txtPassword.Clear();
    }
}
