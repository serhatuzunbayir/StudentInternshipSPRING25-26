namespace StudentInternshipJobPortal.DesktopAdmin.Forms;

partial class ManageApplicationsForm
{
    private System.ComponentModel.IContainer components = null!;
    private Label lblTitle = null!;
    private DataGridView dgvApplications = null!;
    private Label lblStatus = null!;
    private ComboBox cboStatus = null!;
    private Button btnUpdateStatus = null!;
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
        dgvApplications = new DataGridView();
        lblStatus = new Label();
        cboStatus = new ComboBox();
        btnUpdateStatus = new Button();
        btnRefresh = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvApplications).BeginInit();
        SuspendLayout();
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitle.Location = new Point(27, 22);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(255, 37);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Manage Applications";
        dgvApplications.AllowUserToAddRows = false;
        dgvApplications.AllowUserToDeleteRows = false;
        dgvApplications.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvApplications.BackgroundColor = Color.White;
        dgvApplications.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvApplications.Location = new Point(31, 79);
        dgvApplications.MultiSelect = false;
        dgvApplications.Name = "dgvApplications";
        dgvApplications.ReadOnly = true;
        dgvApplications.RowHeadersWidth = 51;
        dgvApplications.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvApplications.Size = new Size(861, 462);
        dgvApplications.TabIndex = 1;
        lblStatus.AutoSize = true;
        lblStatus.Location = new Point(31, 560);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(81, 20);
        lblStatus.TabIndex = 2;
        lblStatus.Text = "New Status";
        cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cboStatus.FormattingEnabled = true;
        cboStatus.Location = new Point(118, 557);
        cboStatus.Name = "cboStatus";
        cboStatus.Size = new Size(190, 28);
        cboStatus.TabIndex = 3;
        btnUpdateStatus.BackColor = Color.FromArgb(13, 110, 253);
        btnUpdateStatus.FlatAppearance.BorderSize = 0;
        btnUpdateStatus.FlatStyle = FlatStyle.Flat;
        btnUpdateStatus.ForeColor = Color.White;
        btnUpdateStatus.Location = new Point(324, 554);
        btnUpdateStatus.Name = "btnUpdateStatus";
        btnUpdateStatus.Size = new Size(141, 34);
        btnUpdateStatus.TabIndex = 4;
        btnUpdateStatus.Text = "Update Status";
        btnUpdateStatus.UseVisualStyleBackColor = false;
        btnUpdateStatus.Click += BtnUpdateStatus_Click;
        btnRefresh.Location = new Point(471, 554);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(100, 34);
        btnRefresh.TabIndex = 5;
        btnRefresh.Text = "Refresh";
        btnRefresh.UseVisualStyleBackColor = true;
        btnRefresh.Click += BtnRefresh_Click;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(924, 619);
        Controls.Add(btnRefresh);
        Controls.Add(btnUpdateStatus);
        Controls.Add(cboStatus);
        Controls.Add(lblStatus);
        Controls.Add(dgvApplications);
        Controls.Add(lblTitle);
        Name = "ManageApplicationsForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Manage Applications";
        ((System.ComponentModel.ISupportInitialize)dgvApplications).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
