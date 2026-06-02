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
        BackColor = Color.FromArgb(243, 246, 252);
        Font = new Font("Segoe UI", 9F);
        lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(32, 41, 74);
        lblTitle.Location = new Point(28, 24);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(344, 38);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Desktop Admin Login";
        lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        lblUsername.AutoSize = true;
        lblUsername.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblUsername.ForeColor = Color.FromArgb(86, 96, 120);
        lblUsername.Location = new Point(40, 92);
        lblUsername.Name = "lblUsername";
        lblUsername.Size = new Size(75, 20);
        lblUsername.TabIndex = 1;
        lblUsername.Text = "Username";
        lblPassword.AutoSize = true;
        lblPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblPassword.ForeColor = Color.FromArgb(86, 96, 120);
        lblPassword.Location = new Point(40, 154);
        lblPassword.Name = "lblPassword";
        lblPassword.Size = new Size(70, 20);
        lblPassword.TabIndex = 2;
        lblPassword.Text = "Password";
        txtUsername.BorderStyle = BorderStyle.FixedSingle;
        txtUsername.Font = new Font("Segoe UI", 10F);
        txtUsername.Location = new Point(40, 118);
        txtUsername.Name = "txtUsername";
        txtUsername.Size = new Size(310, 30);
        txtUsername.TabIndex = 3;
        txtPassword.BorderStyle = BorderStyle.FixedSingle;
        txtPassword.Font = new Font("Segoe UI", 10F);
        txtPassword.Location = new Point(40, 180);
        txtPassword.Name = "txtPassword";
        txtPassword.PasswordChar = '*';
        txtPassword.Size = new Size(310, 30);
        txtPassword.TabIndex = 4;
        btnLogin.BackColor = Color.FromArgb(26, 115, 232);
        btnLogin.Cursor = Cursors.Hand;
        btnLogin.FlatAppearance.BorderSize = 0;
        btnLogin.FlatStyle = FlatStyle.Flat;
        btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnLogin.ForeColor = Color.White;
        btnLogin.Location = new Point(40, 232);
        btnLogin.Name = "btnLogin";
        btnLogin.Size = new Size(310, 42);
        btnLogin.TabIndex = 5;
        btnLogin.Text = "Login";
        btnLogin.UseVisualStyleBackColor = false;
        btnLogin.Click += btnLogin_Click;
        lblHint.Font = new Font("Segoe UI", 9F);
        lblHint.ForeColor = Color.FromArgb(110, 118, 138);
        lblHint.Location = new Point(40, 283);
        lblHint.Name = "lblHint";
        lblHint.Size = new Size(310, 23);
        lblHint.TabIndex = 6;
        lblHint.Text = "Default admin: admin / admin123";
        lblHint.TextAlign = ContentAlignment.MiddleCenter;
        lblError.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblError.ForeColor = Color.FromArgb(196, 54, 44);
        lblError.Location = new Point(40, 308);
        lblError.Name = "lblError";
        lblError.Size = new Size(310, 41);
        lblError.TabIndex = 7;
        lblError.TextAlign = ContentAlignment.MiddleCenter;
        AcceptButton = btnLogin;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(397, 377);
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
        MinimizeBox = false;
        Name = "AdminLoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Desktop Admin";
        ResumeLayout(false);
        PerformLayout();
    }
}
#nullable restore
