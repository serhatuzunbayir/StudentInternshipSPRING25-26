using DesktopAdmin.Services;
using Shared.Data;

namespace DesktopAdmin.Forms;

public partial class AdminLoginForm : Form
{
    private readonly DatabaseHelper _databaseHelper;
    private readonly AdminAuthService _authService;

    public AdminLoginForm(DatabaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
        _authService = new AdminAuthService(databaseHelper);
        InitializeComponent();
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;

        var username = txtUsername.Text.Trim();
        var password = txtPassword.Text.Trim();

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            lblError.Text = "Username and password are required.";
            return;
        }

        var adminUser = _authService.Authenticate(username, password);
        if (adminUser is null)
        {
            lblError.Text = "Invalid admin credentials.";
            return;
        }

        Hide();
        using var dashboard = new AdminDashboardForm(_databaseHelper, adminUser.Id, adminUser.Username);
        dashboard.ShowDialog(this);
        Show();
        txtPassword.Clear();
    }
}
