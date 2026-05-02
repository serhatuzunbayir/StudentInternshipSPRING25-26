#nullable disable
namespace DesktopAdmin.Forms;

partial class AdminLoginForm
{
    private System.ComponentModel.IContainer components = null;
    private Label lblTitle;
    private Label lblUsername;
    private Label lblPassword;
    private TextBox txtUsername;
    private TextBox txtPassword;
    private Button btnLogin;
    private Label lblHint;
    private Label lblError;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components is not null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        lblTitle = new Label();
        lblUsername = new Label();
        lblPassword = new Label();
        txtUsername = new TextBox();
        txtPassword = new TextBox();
        btnLogin = new Button();
        lblHint = new Label();
        lblError = new Label();
        SuspendLayout();
        lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitle.Location = new Point(28, 21);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(344, 38);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Desktop Admin Login";
        lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        lblUsername.AutoSize = true;
        lblUsername.Location = new Point(39, 94);
        lblUsername.Name = "lblUsername";
        lblUsername.Size = new Size(75, 20);
        lblUsername.TabIndex = 1;
        lblUsername.Text = "Username";
        lblPassword.AutoSize = true;
        lblPassword.Location = new Point(39, 145);
        lblPassword.Name = "lblPassword";
        lblPassword.Size = new Size(70, 20);
        lblPassword.TabIndex = 2;
        lblPassword.Text = "Password";
        txtUsername.Location = new Point(140, 91);
        txtUsername.Name = "txtUsername";
        txtUsername.Size = new Size(210, 27);
        txtUsername.TabIndex = 3;
        txtPassword.Location = new Point(140, 142);
        txtPassword.Name = "txtPassword";
        txtPassword.PasswordChar = '*';
        txtPassword.Size = new Size(210, 27);
        txtPassword.TabIndex = 4;
        btnLogin.Location = new Point(140, 194);
        btnLogin.Name = "btnLogin";
        btnLogin.Size = new Size(210, 37);
        btnLogin.TabIndex = 5;
        btnLogin.Text = "Login";
        btnLogin.UseVisualStyleBackColor = true;
        btnLogin.Click += btnLogin_Click;
        lblHint.Location = new Point(39, 247);
        lblHint.Name = "lblHint";
        lblHint.Size = new Size(311, 23);
        lblHint.TabIndex = 6;
        lblHint.Text = "Default admin: admin / admin123";
        lblHint.TextAlign = ContentAlignment.MiddleCenter;
        lblError.ForeColor = Color.Firebrick;
        lblError.Location = new Point(39, 278);
        lblError.Name = "lblError";
        lblError.Size = new Size(311, 41);
        lblError.TabIndex = 7;
        lblError.TextAlign = ContentAlignment.MiddleCenter;
        AcceptButton = btnLogin;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(397, 337);
        Controls.Add(lblError);
        Controls.Add(lblHint);
        Controls.Add(btnLogin);
        Controls.Add(txtPassword);
        Controls.Add(txtUsername);
        Controls.Add(lblPassword);
        Controls.Add(lblUsername);
        Controls.Add(lblTitle);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        Name = "AdminLoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Desktop Admin";
        ResumeLayout(false);
        PerformLayout();
    }
}
#nullable restore
