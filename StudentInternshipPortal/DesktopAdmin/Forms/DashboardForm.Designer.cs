namespace StudentInternshipJobPortal.DesktopAdmin.Forms;

partial class DashboardForm
{
    private System.ComponentModel.IContainer components = null!;
    private Label lblTitle = null!;
    private Label lblWelcome = null!;
    private Button btnManageJobs = null!;
    private Button btnManageApplications = null!;
    private Button btnReports = null!;
    private Button btnLogout = null!;

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
        lblTitle = new Label();
        lblWelcome = new Label();
        btnManageJobs = new Button();
        btnManageApplications = new Button();
        btnReports = new Button();
        btnLogout = new Button();
        SuspendLayout();
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblTitle.Location = new Point(39, 29);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(382, 41);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Student Internship Job Portal";
        lblWelcome.AutoSize = true;
        lblWelcome.ForeColor = Color.FromArgb(108, 117, 125);
        lblWelcome.Location = new Point(45, 80);
        lblWelcome.Name = "lblWelcome";
        lblWelcome.Size = new Size(88, 20);
        lblWelcome.TabIndex = 1;
        lblWelcome.Text = "Welcome, -";
        btnManageJobs.BackColor = Color.FromArgb(13, 110, 253);
        btnManageJobs.FlatAppearance.BorderSize = 0;
        btnManageJobs.FlatStyle = FlatStyle.Flat;
        btnManageJobs.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnManageJobs.ForeColor = Color.White;
        btnManageJobs.Location = new Point(49, 140);
        btnManageJobs.Name = "btnManageJobs";
        btnManageJobs.Size = new Size(235, 50);
        btnManageJobs.TabIndex = 2;
        btnManageJobs.Text = "Manage Jobs";
        btnManageJobs.UseVisualStyleBackColor = false;
        btnManageJobs.Click += BtnManageJobs_Click;
        btnManageApplications.BackColor = Color.FromArgb(25, 135, 84);
        btnManageApplications.FlatAppearance.BorderSize = 0;
        btnManageApplications.FlatStyle = FlatStyle.Flat;
        btnManageApplications.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnManageApplications.ForeColor = Color.White;
        btnManageApplications.Location = new Point(49, 204);
        btnManageApplications.Name = "btnManageApplications";
        btnManageApplications.Size = new Size(235, 50);
        btnManageApplications.TabIndex = 3;
        btnManageApplications.Text = "Manage Applications";
        btnManageApplications.UseVisualStyleBackColor = false;
        btnManageApplications.Click += BtnManageApplications_Click;
        btnReports.BackColor = Color.FromArgb(255, 193, 7);
        btnReports.FlatAppearance.BorderSize = 0;
        btnReports.FlatStyle = FlatStyle.Flat;
        btnReports.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnReports.ForeColor = Color.Black;
        btnReports.Location = new Point(49, 268);
        btnReports.Name = "btnReports";
        btnReports.Size = new Size(235, 50);
        btnReports.TabIndex = 4;
        btnReports.Text = "Reports";
        btnReports.UseVisualStyleBackColor = false;
        btnReports.Click += BtnReports_Click;
        btnLogout.BackColor = Color.FromArgb(108, 117, 125);
        btnLogout.FlatAppearance.BorderSize = 0;
        btnLogout.FlatStyle = FlatStyle.Flat;
        btnLogout.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnLogout.ForeColor = Color.White;
        btnLogout.Location = new Point(49, 332);
        btnLogout.Name = "btnLogout";
        btnLogout.Size = new Size(235, 50);
        btnLogout.TabIndex = 5;
        btnLogout.Text = "Logout";
        btnLogout.UseVisualStyleBackColor = false;
        btnLogout.Click += BtnLogout_Click;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(493, 430);
        Controls.Add(btnLogout);
        Controls.Add(btnReports);
        Controls.Add(btnManageApplications);
        Controls.Add(btnManageJobs);
        Controls.Add(lblWelcome);
        Controls.Add(lblTitle);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        Name = "DashboardForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Admin Dashboard";
        ResumeLayout(false);
        PerformLayout();
    }
}
