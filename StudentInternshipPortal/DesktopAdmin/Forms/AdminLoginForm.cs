using DesktopAdmin.Services;
using Shared.Data;

namespace DesktopAdmin.Forms;

// This form handles the login screen for administrators in the desktop application.
public partial class AdminLoginForm : Form
{
    private readonly DatabaseHelper _databaseHelper;
    private readonly AdminAuthService _authService;

    public AdminLoginForm(DatabaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
        // Instantiate the authentication service
        _authService = new AdminAuthService(databaseHelper);
        InitializeComponent();
    }

    // Event handler triggered when the Login button is clicked.
    private void btnLogin_Click(object sender, EventArgs e)
    {
        // Clear any previous error message text
        lblError.Text = string.Empty;

        var username = txtUsername.Text.Trim();
        var password = txtPassword.Text.Trim();

        // Basic input validation: check if fields are empty
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            lblError.Text = "Username and password are required.";
            return;
        }

        // Validate administrator credentials in the database
        var adminUser = _authService.Authenticate(username, password);
        if (adminUser is null)
        {
            lblError.Text = "Invalid admin credentials.";
            return;
        }

        // If credentials are valid, hide the login screen, open the dashboard dialog,
        // and show the login screen again once dashboard is closed.
        Hide();
        using var dashboard = new AdminDashboardForm(_databaseHelper, adminUser.Id, adminUser.Username);
        dashboard.ShowDialog(this);
        Show();
        txtPassword.Clear();
    }
}

