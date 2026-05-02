namespace StudentInternshipJobPortal.DesktopAdmin.Forms;

partial class LoginForm
{
    private System.ComponentModel.IContainer components = null!;
    private Panel pnlCard = null!;
    private Label lblTitle = null!;
    private Label lblHint = null!;
    private Label lblUsername = null!;
    private Label lblPassword = null!;
    private TextBox txtUsername = null!;
    private TextBox txtPassword = null!;
    private Button btnLogin = null!;
    private Label lblStatus = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        pnlCard = new Panel();
        lblStatus = new Label();
        btnLogin = new Button();
        txtPassword = new TextBox();
        txtUsername = new TextBox();
        lblPassword = new Label();
        lblUsername = new Label();
        lblHint = new Label();
        lblTitle = new Label();
        pnlCard.SuspendLayout();
        SuspendLayout();
        pnlCard.BackColor = Color.White;
        pnlCard.Controls.Add(lblStatus);
        pnlCard.Controls.Add(btnLogin);
        pnlCard.Controls.Add(txtPassword);
        pnlCard.Controls.Add(txtUsername);
        pnlCard.Controls.Add(lblPassword);
        pnlCard.Controls.Add(lblUsername);
        pnlCard.Controls.Add(lblHint);
        pnlCard.Controls.Add(lblTitle);
        pnlCard.Location = new Point(62, 40);
        pnlCard.Name = "pnlCard";
        pnlCard.Size = new Size(430, 320);
        pnlCard.TabIndex = 0;
        lblStatus.ForeColor = Color.FromArgb(176, 42, 55);
        lblStatus.Location = new Point(36, 245);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(358, 40);
        lblStatus.TabIndex = 7;
        lblStatus.TextAlign = ContentAlignment.MiddleCenter;
        btnLogin.BackColor = Color.FromArgb(13, 110, 253);
        btnLogin.FlatAppearance.BorderSize = 0;
        btnLogin.FlatStyle = FlatStyle.Flat;
        btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnLogin.ForeColor = Color.White;
        btnLogin.Location = new Point(36, 201);
        btnLogin.Name = "btnLogin";
        btnLogin.Size = new Size(358, 36);
        btnLogin.TabIndex = 6;
        btnLogin.Text = "Admin Sign In";
        btnLogin.UseVisualStyleBackColor = false;
        btnLogin.Click += BtnLogin_Click;
        txtPassword.Location = new Point(36, 158);
        txtPassword.Name = "txtPassword";
        txtPassword.PasswordChar = '*';
        txtPassword.PlaceholderText = "Enter password";
        txtPassword.Size = new Size(358, 27);
        txtPassword.TabIndex = 5;
        txtUsername.Location = new Point(36, 102);
        txtUsername.Name = "txtUsername";
        txtUsername.PlaceholderText = "Enter admin username";
        txtUsername.Size = new Size(358, 27);
        txtUsername.TabIndex = 3;
        lblPassword.AutoSize = true;
        lblPassword.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblPassword.Location = new Point(36, 134);
        lblPassword.Name = "lblPassword";
        lblPassword.Size = new Size(76, 21);
        lblPassword.TabIndex = 4;
        lblPassword.Text = "Password";
        lblUsername.AutoSize = true;
        lblUsername.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblUsername.Location = new Point(36, 78);
        lblUsername.Name = "lblUsername";
        lblUsername.Size = new Size(84, 21);
        lblUsername.TabIndex = 2;
        lblUsername.Text = "Username";
        lblHint.ForeColor = Color.FromArgb(108, 117, 125);
        lblHint.Location = new Point(36, 47);
        lblHint.Name = "lblHint";
        lblHint.Size = new Size(358, 23);
        lblHint.TabIndex = 1;
        lblHint.Text = "Admin accounts must already exist in the database.";
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitle.Location = new Point(29, 12);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(179, 37);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Desktop Admin";
        AcceptButton = btnLogin;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(238, 242, 247);
        ClientSize = new Size(554, 406);
        Controls.Add(pnlCard);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        Name = "LoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Student Internship Job Portal - Admin Login";
        pnlCard.ResumeLayout(false);
        pnlCard.PerformLayout();
        ResumeLayout(false);
    }
}
