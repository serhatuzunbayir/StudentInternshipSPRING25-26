namespace StudentInternshipJobPortal.DesktopAdmin.Forms;

partial class ReportsForm
{
    private System.ComponentModel.IContainer components = null!;
    private Label lblTitle = null!;
    private Label lblStudents = null!;
    private Label lblActiveJobs = null!;
    private Label lblApplications = null!;
    private Label lblPending = null!;
    private Label lblAccepted = null!;
    private Label lblRejected = null!;
    private Label lblStudentsValue = null!;
    private Label lblActiveJobsValue = null!;
    private Label lblApplicationsValue = null!;
    private Label lblPendingValue = null!;
    private Label lblAcceptedValue = null!;
    private Label lblRejectedValue = null!;
    private Button btnRefresh = null!;

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
        lblStudents = new Label();
        lblActiveJobs = new Label();
        lblApplications = new Label();
        lblPending = new Label();
        lblAccepted = new Label();
        lblRejected = new Label();
        lblStudentsValue = new Label();
        lblActiveJobsValue = new Label();
        lblApplicationsValue = new Label();
        lblPendingValue = new Label();
        lblAcceptedValue = new Label();
        lblRejectedValue = new Label();
        btnRefresh = new Button();
        SuspendLayout();
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitle.Location = new Point(33, 25);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(109, 37);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Reports";
        lblStudents.AutoSize = true;
        lblStudents.Location = new Point(37, 96);
        lblStudents.Text = "Total Students";
        lblActiveJobs.AutoSize = true;
        lblActiveJobs.Location = new Point(37, 139);
        lblActiveJobs.Text = "Active Jobs";
        lblApplications.AutoSize = true;
        lblApplications.Location = new Point(37, 182);
        lblApplications.Text = "Total Applications";
        lblPending.AutoSize = true;
        lblPending.Location = new Point(37, 225);
        lblPending.Text = "Pending";
        lblAccepted.AutoSize = true;
        lblAccepted.Location = new Point(37, 268);
        lblAccepted.Text = "Accepted";
        lblRejected.AutoSize = true;
        lblRejected.Location = new Point(37, 311);
        lblRejected.Text = "Rejected";
        lblStudentsValue.AutoSize = true;
        lblStudentsValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblStudentsValue.Location = new Point(224, 92);
        lblStudentsValue.Text = "0";
        lblActiveJobsValue.AutoSize = true;
        lblActiveJobsValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblActiveJobsValue.Location = new Point(224, 135);
        lblActiveJobsValue.Text = "0";
        lblApplicationsValue.AutoSize = true;
        lblApplicationsValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblApplicationsValue.Location = new Point(224, 178);
        lblApplicationsValue.Text = "0";
        lblPendingValue.AutoSize = true;
        lblPendingValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblPendingValue.Location = new Point(224, 221);
        lblPendingValue.Text = "0";
        lblAcceptedValue.AutoSize = true;
        lblAcceptedValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblAcceptedValue.Location = new Point(224, 264);
        lblAcceptedValue.Text = "0";
        lblRejectedValue.AutoSize = true;
        lblRejectedValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblRejectedValue.Location = new Point(224, 307);
        lblRejectedValue.Text = "0";
        btnRefresh.Location = new Point(37, 365);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(108, 36);
        btnRefresh.TabIndex = 13;
        btnRefresh.Text = "Refresh";
        btnRefresh.UseVisualStyleBackColor = true;
        btnRefresh.Click += BtnRefresh_Click;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(402, 440);
        Controls.Add(btnRefresh);
        Controls.Add(lblRejectedValue);
        Controls.Add(lblAcceptedValue);
        Controls.Add(lblPendingValue);
        Controls.Add(lblApplicationsValue);
        Controls.Add(lblActiveJobsValue);
        Controls.Add(lblStudentsValue);
        Controls.Add(lblRejected);
        Controls.Add(lblAccepted);
        Controls.Add(lblPending);
        Controls.Add(lblApplications);
        Controls.Add(lblActiveJobs);
        Controls.Add(lblStudents);
        Controls.Add(lblTitle);
        Name = "ReportsForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Reports";
        ResumeLayout(false);
        PerformLayout();
    }
}
