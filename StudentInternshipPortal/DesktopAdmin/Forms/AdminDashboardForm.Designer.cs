#nullable disable
namespace DesktopAdmin.Forms;

partial class AdminDashboardForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Label lblWelcome;
    private System.Windows.Forms.TabControl tabMain;
    private TabPage tabJobs;
    private TabPage tabApplications;
    private TabPage tabReports;
    private System.Windows.Forms.DataGridView dgvJobs;
    private System.Windows.Forms.DataGridView dgvApplications;
    private TextBox txtJobSearch;
    private ComboBox cmbJobTypeFilter;
    private ComboBox cmbJobStatusFilter;
    private System.Windows.Forms.Button btnClearJobFilters;
    private Label lblJobSearch;
    private Label lblJobTypeFilter;
    private Label lblJobStatusFilter;
    private System.Windows.Forms.TextBox txtApplicationSearch;
    private ComboBox cmbApplicationStatusFilter;
    private System.Windows.Forms.Button btnClearApplicationFilters;
    private System.Windows.Forms.Button btnRefreshApplications;
    private Label lblApplicationSearch;
    private Label lblApplicationStatusFilter;
    private System.Windows.Forms.Button btnAddJob;
    private System.Windows.Forms.Button btnEditJob;
    private System.Windows.Forms.Button btnDeleteJob;
    private Button btnAccept;
    private Button btnReject;
    private System.Windows.Forms.Button btnMarkPending;
    private Button btnRefreshReports;
    private Label lblTotalStudents;
    private Label lblActiveJobs;
    private Label lblTotalApplications;
    private Label lblAccepted;
    private Label lblRejected;
    private Label lblPending;
    private Label lblTotalStudentsValue;
    private Label lblActiveJobsValue;
    private Label lblTotalApplicationsValue;
    private Label lblAcceptedValue;
    private Label lblRejectedValue;
    private Label lblPendingValue;
    private System.Windows.Forms.ListBox lstAuditLog;
    private Label lblAuditLog;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components is not null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        lblWelcome = new System.Windows.Forms.Label();
        tabMain = new System.Windows.Forms.TabControl();
        tabJobs = new System.Windows.Forms.TabPage();
        btnDeleteJob = new System.Windows.Forms.Button();
        btnEditJob = new System.Windows.Forms.Button();
        btnAddJob = new System.Windows.Forms.Button();
        txtJobSearch = new System.Windows.Forms.TextBox();
        cmbJobTypeFilter = new System.Windows.Forms.ComboBox();
        cmbJobStatusFilter = new System.Windows.Forms.ComboBox();
        btnClearJobFilters = new System.Windows.Forms.Button();
        lblJobSearch = new System.Windows.Forms.Label();
        lblJobTypeFilter = new System.Windows.Forms.Label();
        lblJobStatusFilter = new System.Windows.Forms.Label();
        dgvJobs = new System.Windows.Forms.DataGridView();
        tabApplications = new System.Windows.Forms.TabPage();
        txtApplicationSearch = new System.Windows.Forms.TextBox();
        cmbApplicationStatusFilter = new System.Windows.Forms.ComboBox();
        btnClearApplicationFilters = new System.Windows.Forms.Button();
        btnRefreshApplications = new System.Windows.Forms.Button();
        lblApplicationSearch = new System.Windows.Forms.Label();
        lblApplicationStatusFilter = new System.Windows.Forms.Label();
        btnMarkPending = new System.Windows.Forms.Button();
        btnReject = new System.Windows.Forms.Button();
        btnAccept = new System.Windows.Forms.Button();
        dgvApplications = new System.Windows.Forms.DataGridView();
        tabReports = new System.Windows.Forms.TabPage();
        btnRefreshReports = new System.Windows.Forms.Button();
        lblPendingValue = new System.Windows.Forms.Label();
        lblRejectedValue = new System.Windows.Forms.Label();
        lblAcceptedValue = new System.Windows.Forms.Label();
        lblTotalApplicationsValue = new System.Windows.Forms.Label();
        lblActiveJobsValue = new System.Windows.Forms.Label();
        lblTotalStudentsValue = new System.Windows.Forms.Label();
        lblPending = new System.Windows.Forms.Label();
        lblRejected = new System.Windows.Forms.Label();
        lblAccepted = new System.Windows.Forms.Label();
        lblTotalApplications = new System.Windows.Forms.Label();
        lblActiveJobs = new System.Windows.Forms.Label();
        lblTotalStudents = new System.Windows.Forms.Label();
        lblAuditLog = new System.Windows.Forms.Label();
        lstAuditLog = new System.Windows.Forms.ListBox();
        tabMain.SuspendLayout();
        tabJobs.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvJobs).BeginInit();
        tabApplications.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvApplications).BeginInit();
        tabReports.SuspendLayout();
        SuspendLayout();
        // 
        // lblWelcome
        // 
        lblWelcome.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
        lblWelcome.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)77)), ((int)((byte)88)), ((int)((byte)110)));
        lblWelcome.Location = new System.Drawing.Point(695, 18);
        lblWelcome.Name = "lblWelcome";
        lblWelcome.Size = new System.Drawing.Size(287, 23);
        lblWelcome.TabIndex = 0;
        lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // tabMain
        // 
        tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
        tabMain.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
        tabMain.Controls.Add(tabJobs);
        tabMain.Controls.Add(tabApplications);
        tabMain.Controls.Add(tabReports);
        tabMain.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        tabMain.ItemSize = new System.Drawing.Size(120, 34);
        tabMain.Location = new System.Drawing.Point(12, 54);
        tabMain.Name = "tabMain";
        tabMain.SelectedIndex = 0;
        tabMain.Size = new System.Drawing.Size(970, 574);
        tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
        tabMain.TabIndex = 1;
        // 
        // tabJobs
        // 
        tabJobs.BackColor = System.Drawing.Color.FromArgb(((int)((byte)250)), ((int)((byte)252)), ((int)((byte)255)));
        tabJobs.Controls.Add(btnDeleteJob);
        tabJobs.Controls.Add(btnEditJob);
        tabJobs.Controls.Add(btnAddJob);
        tabJobs.Controls.Add(txtJobSearch);
        tabJobs.Controls.Add(cmbJobTypeFilter);
        tabJobs.Controls.Add(cmbJobStatusFilter);
        tabJobs.Controls.Add(btnClearJobFilters);
        tabJobs.Controls.Add(lblJobSearch);
        tabJobs.Controls.Add(lblJobTypeFilter);
        tabJobs.Controls.Add(lblJobStatusFilter);
        tabJobs.Controls.Add(dgvJobs);
        tabJobs.Location = new System.Drawing.Point(4, 38);
        tabJobs.Name = "tabJobs";
        tabJobs.Padding = new System.Windows.Forms.Padding(3);
        tabJobs.Size = new System.Drawing.Size(962, 532);
        tabJobs.TabIndex = 0;
        tabJobs.Text = "Jobs";
        // 
        // btnDeleteJob
        // 
        btnDeleteJob.BackColor = System.Drawing.Color.FromArgb(((int)((byte)210)), ((int)((byte)76)), ((int)((byte)70)));
        btnDeleteJob.Cursor = System.Windows.Forms.Cursors.Hand;
        btnDeleteJob.FlatAppearance.BorderSize = 0;
        btnDeleteJob.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnDeleteJob.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        btnDeleteJob.ForeColor = System.Drawing.Color.White;
        btnDeleteJob.Location = new System.Drawing.Point(272, 18);
        btnDeleteJob.Name = "btnDeleteJob";
        btnDeleteJob.Size = new System.Drawing.Size(118, 36);
        btnDeleteJob.TabIndex = 3;
        btnDeleteJob.Text = "Delete";
        btnDeleteJob.UseVisualStyleBackColor = false;
        btnDeleteJob.Click += btnDeleteJob_Click;
        // 
        // btnEditJob
        // 
        btnEditJob.BackColor = System.Drawing.Color.White;
        btnEditJob.Cursor = System.Windows.Forms.Cursors.Hand;
        btnEditJob.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)((byte)186)), ((int)((byte)196)), ((int)((byte)214)));
        btnEditJob.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnEditJob.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        btnEditJob.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)52)), ((int)((byte)64)), ((int)((byte)92)));
        btnEditJob.Location = new System.Drawing.Point(145, 18);
        btnEditJob.Name = "btnEditJob";
        btnEditJob.Size = new System.Drawing.Size(118, 36);
        btnEditJob.TabIndex = 2;
        btnEditJob.Text = "Edit";
        btnEditJob.UseVisualStyleBackColor = false;
        btnEditJob.Click += btnEditJob_Click;
        // 
        // btnAddJob
        // 
        btnAddJob.BackColor = System.Drawing.Color.FromArgb(((int)((byte)31)), ((int)((byte)119)), ((int)((byte)90)));
        btnAddJob.Cursor = System.Windows.Forms.Cursors.Hand;
        btnAddJob.FlatAppearance.BorderSize = 0;
        btnAddJob.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnAddJob.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        btnAddJob.ForeColor = System.Drawing.Color.White;
        btnAddJob.Location = new System.Drawing.Point(18, 18);
        btnAddJob.Name = "btnAddJob";
        btnAddJob.Size = new System.Drawing.Size(118, 36);
        btnAddJob.TabIndex = 1;
        btnAddJob.Text = "Add Job";
        btnAddJob.UseVisualStyleBackColor = false;
        btnAddJob.Click += btnAddJob_Click;
        // 
        // txtJobSearch
        // 
        txtJobSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtJobSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        txtJobSearch.Location = new System.Drawing.Point(418, 28);
        txtJobSearch.Name = "txtJobSearch";
        txtJobSearch.PlaceholderText = "Title, location or skills";
        txtJobSearch.Size = new System.Drawing.Size(180, 29);
        txtJobSearch.TabIndex = 5;
        txtJobSearch.TextChanged += txtJobSearch_TextChanged;
        // 
        // cmbJobTypeFilter
        // 
        cmbJobTypeFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cmbJobTypeFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
        cmbJobTypeFilter.FormattingEnabled = true;
        cmbJobTypeFilter.Items.AddRange(new object[] { "All Types", "Internship", "PartTime", "FullTime" });
        cmbJobTypeFilter.Location = new System.Drawing.Point(613, 28);
        cmbJobTypeFilter.Name = "cmbJobTypeFilter";
        cmbJobTypeFilter.Size = new System.Drawing.Size(104, 28);
        cmbJobTypeFilter.TabIndex = 7;
        cmbJobTypeFilter.SelectedIndexChanged += cmbJobTypeFilter_SelectedIndexChanged;
        // 
        // cmbJobStatusFilter
        // 
        cmbJobStatusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cmbJobStatusFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
        cmbJobStatusFilter.FormattingEnabled = true;
        cmbJobStatusFilter.Items.AddRange(new object[] { "All Statuses", "Active", "Passive" });
        cmbJobStatusFilter.Location = new System.Drawing.Point(732, 28);
        cmbJobStatusFilter.Name = "cmbJobStatusFilter";
        cmbJobStatusFilter.Size = new System.Drawing.Size(95, 28);
        cmbJobStatusFilter.TabIndex = 9;
        cmbJobStatusFilter.SelectedIndexChanged += cmbJobStatusFilter_SelectedIndexChanged;
        // 
        // btnClearJobFilters
        // 
        btnClearJobFilters.BackColor = System.Drawing.Color.White;
        btnClearJobFilters.Cursor = System.Windows.Forms.Cursors.Hand;
        btnClearJobFilters.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)((byte)199)), ((int)((byte)208)), ((int)((byte)225)));
        btnClearJobFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnClearJobFilters.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        btnClearJobFilters.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)87)), ((int)((byte)97)), ((int)((byte)122)));
        btnClearJobFilters.Location = new System.Drawing.Point(833, 28);
        btnClearJobFilters.Name = "btnClearJobFilters";
        btnClearJobFilters.Size = new System.Drawing.Size(93, 28);
        btnClearJobFilters.TabIndex = 10;
        btnClearJobFilters.Text = "Clear";
        btnClearJobFilters.UseVisualStyleBackColor = false;
        btnClearJobFilters.Click += btnClearJobFilters_Click;
        // 
        // lblJobSearch
        // 
        lblJobSearch.AutoSize = true;
        lblJobSearch.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        lblJobSearch.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)77)), ((int)((byte)88)), ((int)((byte)110)));
        lblJobSearch.Location = new System.Drawing.Point(414, 8);
        lblJobSearch.Name = "lblJobSearch";
        lblJobSearch.Size = new System.Drawing.Size(55, 20);
        lblJobSearch.TabIndex = 4;
        lblJobSearch.Text = "Search";
        // 
        // lblJobTypeFilter
        // 
        lblJobTypeFilter.AutoSize = true;
        lblJobTypeFilter.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        lblJobTypeFilter.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)77)), ((int)((byte)88)), ((int)((byte)110)));
        lblJobTypeFilter.Location = new System.Drawing.Point(609, 8);
        lblJobTypeFilter.Name = "lblJobTypeFilter";
        lblJobTypeFilter.Size = new System.Drawing.Size(42, 20);
        lblJobTypeFilter.TabIndex = 6;
        lblJobTypeFilter.Text = "Type";
        // 
        // lblJobStatusFilter
        // 
        lblJobStatusFilter.AutoSize = true;
        lblJobStatusFilter.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        lblJobStatusFilter.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)77)), ((int)((byte)88)), ((int)((byte)110)));
        lblJobStatusFilter.Location = new System.Drawing.Point(728, 8);
        lblJobStatusFilter.Name = "lblJobStatusFilter";
        lblJobStatusFilter.Size = new System.Drawing.Size(53, 20);
        lblJobStatusFilter.TabIndex = 8;
        lblJobStatusFilter.Text = "Status";
        // 
        // dgvJobs
        // 
        dgvJobs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
        dgvJobs.BackgroundColor = System.Drawing.Color.White;
        dgvJobs.BorderStyle = System.Windows.Forms.BorderStyle.None;
        dgvJobs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvJobs.Location = new System.Drawing.Point(18, 117);
        dgvJobs.MultiSelect = false;
        dgvJobs.Name = "dgvJobs";
        dgvJobs.ReadOnly = true;
        dgvJobs.RowHeadersWidth = 51;
        dgvJobs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        dgvJobs.Size = new System.Drawing.Size(908, 398);
        dgvJobs.TabIndex = 0;
        // 
        // tabApplications
        // 
        tabApplications.BackColor = System.Drawing.Color.FromArgb(((int)((byte)250)), ((int)((byte)252)), ((int)((byte)255)));
        tabApplications.Controls.Add(txtApplicationSearch);
        tabApplications.Controls.Add(cmbApplicationStatusFilter);
        tabApplications.Controls.Add(btnClearApplicationFilters);
        tabApplications.Controls.Add(btnRefreshApplications);
        tabApplications.Controls.Add(lblApplicationSearch);
        tabApplications.Controls.Add(lblApplicationStatusFilter);
        tabApplications.Controls.Add(btnMarkPending);
        tabApplications.Controls.Add(btnReject);
        tabApplications.Controls.Add(btnAccept);
        tabApplications.Controls.Add(dgvApplications);
        tabApplications.Location = new System.Drawing.Point(4, 38);
        tabApplications.Name = "tabApplications";
        tabApplications.Padding = new System.Windows.Forms.Padding(3);
        tabApplications.Size = new System.Drawing.Size(962, 532);
        tabApplications.TabIndex = 1;
        tabApplications.Text = "Applications";
        // 
        // txtApplicationSearch
        // 
        txtApplicationSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtApplicationSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        txtApplicationSearch.Location = new System.Drawing.Point(413, 27);
        txtApplicationSearch.Name = "txtApplicationSearch";
        txtApplicationSearch.PlaceholderText = "Student or job";
        txtApplicationSearch.Size = new System.Drawing.Size(220, 29);
        txtApplicationSearch.TabIndex = 5;
        txtApplicationSearch.TextChanged += txtApplicationSearch_TextChanged;
        // 
        // cmbApplicationStatusFilter
        // 
        cmbApplicationStatusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cmbApplicationStatusFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
        cmbApplicationStatusFilter.FormattingEnabled = true;
        cmbApplicationStatusFilter.Items.AddRange(new object[] { "All Statuses", "Pending", "Accepted", "Rejected" });
        cmbApplicationStatusFilter.Location = new System.Drawing.Point(654, 28);
        cmbApplicationStatusFilter.Name = "cmbApplicationStatusFilter";
        cmbApplicationStatusFilter.Size = new System.Drawing.Size(96, 28);
        cmbApplicationStatusFilter.TabIndex = 7;
        cmbApplicationStatusFilter.SelectedIndexChanged += cmbApplicationStatusFilter_SelectedIndexChanged;
        // 
        // btnClearApplicationFilters
        // 
        btnClearApplicationFilters.BackColor = System.Drawing.Color.White;
        btnClearApplicationFilters.Cursor = System.Windows.Forms.Cursors.Hand;
        btnClearApplicationFilters.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)((byte)199)), ((int)((byte)208)), ((int)((byte)225)));
        btnClearApplicationFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnClearApplicationFilters.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        btnClearApplicationFilters.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)87)), ((int)((byte)97)), ((int)((byte)122)));
        btnClearApplicationFilters.Location = new System.Drawing.Point(770, 29);
        btnClearApplicationFilters.Name = "btnClearApplicationFilters";
        btnClearApplicationFilters.Size = new System.Drawing.Size(65, 28);
        btnClearApplicationFilters.TabIndex = 8;
        btnClearApplicationFilters.Text = "Clear";
        btnClearApplicationFilters.UseVisualStyleBackColor = false;
        btnClearApplicationFilters.Click += btnClearApplicationFilters_Click;
        // 
        // btnRefreshApplications
        // 
        btnRefreshApplications.BackColor = System.Drawing.Color.FromArgb(((int)((byte)26)), ((int)((byte)115)), ((int)((byte)232)));
        btnRefreshApplications.Cursor = System.Windows.Forms.Cursors.Hand;
        btnRefreshApplications.FlatAppearance.BorderSize = 0;
        btnRefreshApplications.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnRefreshApplications.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        btnRefreshApplications.ForeColor = System.Drawing.Color.White;
        btnRefreshApplications.Location = new System.Drawing.Point(848, 29);
        btnRefreshApplications.Name = "btnRefreshApplications";
        btnRefreshApplications.Size = new System.Drawing.Size(71, 28);
        btnRefreshApplications.TabIndex = 9;
        btnRefreshApplications.Text = "Refresh";
        btnRefreshApplications.UseVisualStyleBackColor = false;
        btnRefreshApplications.Click += btnRefreshApplications_Click;
        // 
        // lblApplicationSearch
        // 
        lblApplicationSearch.AutoSize = true;
        lblApplicationSearch.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        lblApplicationSearch.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)77)), ((int)((byte)88)), ((int)((byte)110)));
        lblApplicationSearch.Location = new System.Drawing.Point(413, 8);
        lblApplicationSearch.Name = "lblApplicationSearch";
        lblApplicationSearch.Size = new System.Drawing.Size(55, 20);
        lblApplicationSearch.TabIndex = 4;
        lblApplicationSearch.Text = "Search";
        // 
        // lblApplicationStatusFilter
        // 
        lblApplicationStatusFilter.AutoSize = true;
        lblApplicationStatusFilter.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
        lblApplicationStatusFilter.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)77)), ((int)((byte)88)), ((int)((byte)110)));
        lblApplicationStatusFilter.Location = new System.Drawing.Point(650, 8);
        lblApplicationStatusFilter.Name = "lblApplicationStatusFilter";
        lblApplicationStatusFilter.Size = new System.Drawing.Size(53, 20);
        lblApplicationStatusFilter.TabIndex = 6;
        lblApplicationStatusFilter.Text = "Status";
        // 
        // btnMarkPending
        // 
        btnMarkPending.BackColor = System.Drawing.Color.White;
        btnMarkPending.Cursor = System.Windows.Forms.Cursors.Hand;
        btnMarkPending.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)((byte)199)), ((int)((byte)208)), ((int)((byte)225)));
        btnMarkPending.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnMarkPending.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        btnMarkPending.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)87)), ((int)((byte)97)), ((int)((byte)122)));
        btnMarkPending.Location = new System.Drawing.Point(270, 18);
        btnMarkPending.Name = "btnMarkPending";
        btnMarkPending.Size = new System.Drawing.Size(125, 36);
        btnMarkPending.TabIndex = 3;
        btnMarkPending.Text = "Reset Status\r\n";
        btnMarkPending.UseVisualStyleBackColor = false;
        btnMarkPending.Click += btnMarkPending_Click;
        // 
        // btnReject
        // 
        btnReject.BackColor = System.Drawing.Color.FromArgb(((int)((byte)210)), ((int)((byte)76)), ((int)((byte)70)));
        btnReject.Cursor = System.Windows.Forms.Cursors.Hand;
        btnReject.FlatAppearance.BorderSize = 0;
        btnReject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnReject.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        btnReject.ForeColor = System.Drawing.Color.White;
        btnReject.Location = new System.Drawing.Point(141, 18);
        btnReject.Name = "btnReject";
        btnReject.Size = new System.Drawing.Size(123, 36);
        btnReject.TabIndex = 2;
        btnReject.Text = "Reject";
        btnReject.UseVisualStyleBackColor = false;
        btnReject.Click += btnReject_Click;
        // 
        // btnAccept
        // 
        btnAccept.BackColor = System.Drawing.Color.FromArgb(((int)((byte)31)), ((int)((byte)119)), ((int)((byte)90)));
        btnAccept.Cursor = System.Windows.Forms.Cursors.Hand;
        btnAccept.FlatAppearance.BorderSize = 0;
        btnAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnAccept.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        btnAccept.ForeColor = System.Drawing.Color.White;
        btnAccept.Location = new System.Drawing.Point(12, 18);
        btnAccept.Name = "btnAccept";
        btnAccept.Size = new System.Drawing.Size(123, 36);
        btnAccept.TabIndex = 1;
        btnAccept.Text = "Accept";
        btnAccept.UseVisualStyleBackColor = false;
        btnAccept.Click += btnAccept_Click;
        // 
        // dgvApplications
        // 
        dgvApplications.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
        dgvApplications.BackgroundColor = System.Drawing.Color.White;
        dgvApplications.BorderStyle = System.Windows.Forms.BorderStyle.None;
        dgvApplications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvApplications.Location = new System.Drawing.Point(12, 98);
        dgvApplications.MultiSelect = false;
        dgvApplications.Name = "dgvApplications";
        dgvApplications.ReadOnly = true;
        dgvApplications.RowHeadersWidth = 51;
        dgvApplications.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        dgvApplications.Size = new System.Drawing.Size(907, 417);
        dgvApplications.TabIndex = 0;
        // 
        // tabReports
        // 
        tabReports.BackColor = System.Drawing.Color.FromArgb(((int)((byte)250)), ((int)((byte)252)), ((int)((byte)255)));
        tabReports.Controls.Add(btnRefreshReports);
        tabReports.Controls.Add(lblPendingValue);
        tabReports.Controls.Add(lblRejectedValue);
        tabReports.Controls.Add(lblAcceptedValue);
        tabReports.Controls.Add(lblTotalApplicationsValue);
        tabReports.Controls.Add(lblActiveJobsValue);
        tabReports.Controls.Add(lblTotalStudentsValue);
        tabReports.Controls.Add(lblPending);
        tabReports.Controls.Add(lblRejected);
        tabReports.Controls.Add(lblAccepted);
        tabReports.Controls.Add(lblTotalApplications);
        tabReports.Controls.Add(lblActiveJobs);
        tabReports.Controls.Add(lblTotalStudents);
        tabReports.Controls.Add(lblAuditLog);
        tabReports.Controls.Add(lstAuditLog);
        tabReports.Location = new System.Drawing.Point(4, 38);
        tabReports.Name = "tabReports";
        tabReports.Padding = new System.Windows.Forms.Padding(3);
        tabReports.Size = new System.Drawing.Size(962, 532);
        tabReports.TabIndex = 2;
        tabReports.Text = "Reports";
        // 
        // btnRefreshReports
        // 
        btnRefreshReports.BackColor = System.Drawing.Color.FromArgb(((int)((byte)26)), ((int)((byte)115)), ((int)((byte)232)));
        btnRefreshReports.Cursor = System.Windows.Forms.Cursors.Hand;
        btnRefreshReports.FlatAppearance.BorderSize = 0;
        btnRefreshReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnRefreshReports.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        btnRefreshReports.ForeColor = System.Drawing.Color.White;
        btnRefreshReports.Location = new System.Drawing.Point(26, 318);
        btnRefreshReports.Name = "btnRefreshReports";
        btnRefreshReports.Size = new System.Drawing.Size(148, 40);
        btnRefreshReports.TabIndex = 12;
        btnRefreshReports.Text = "Refresh";
        btnRefreshReports.UseVisualStyleBackColor = false;
        btnRefreshReports.Click += btnRefreshReports_Click;
        // 
        // lblPendingValue
        // 
        lblPendingValue.AutoSize = true;
        lblPendingValue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
        lblPendingValue.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)128)), ((int)((byte)90)), ((int)((byte)24)));
        lblPendingValue.Location = new System.Drawing.Point(308, 252);
        lblPendingValue.Name = "lblPendingValue";
        lblPendingValue.Size = new System.Drawing.Size(33, 37);
        lblPendingValue.TabIndex = 11;
        lblPendingValue.Text = "0";
        // 
        // lblRejectedValue
        // 
        lblRejectedValue.AutoSize = true;
        lblRejectedValue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
        lblRejectedValue.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)187)), ((int)((byte)61)), ((int)((byte)52)));
        lblRejectedValue.Location = new System.Drawing.Point(308, 202);
        lblRejectedValue.Name = "lblRejectedValue";
        lblRejectedValue.Size = new System.Drawing.Size(33, 37);
        lblRejectedValue.TabIndex = 10;
        lblRejectedValue.Text = "0";
        // 
        // lblAcceptedValue
        // 
        lblAcceptedValue.AutoSize = true;
        lblAcceptedValue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
        lblAcceptedValue.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)31)), ((int)((byte)119)), ((int)((byte)90)));
        lblAcceptedValue.Location = new System.Drawing.Point(308, 152);
        lblAcceptedValue.Name = "lblAcceptedValue";
        lblAcceptedValue.Size = new System.Drawing.Size(33, 37);
        lblAcceptedValue.TabIndex = 9;
        lblAcceptedValue.Text = "0";
        // 
        // lblTotalApplicationsValue
        // 
        lblTotalApplicationsValue.AutoSize = true;
        lblTotalApplicationsValue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
        lblTotalApplicationsValue.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)26)), ((int)((byte)115)), ((int)((byte)232)));
        lblTotalApplicationsValue.Location = new System.Drawing.Point(308, 102);
        lblTotalApplicationsValue.Name = "lblTotalApplicationsValue";
        lblTotalApplicationsValue.Size = new System.Drawing.Size(33, 37);
        lblTotalApplicationsValue.TabIndex = 8;
        lblTotalApplicationsValue.Text = "0";
        // 
        // lblActiveJobsValue
        // 
        lblActiveJobsValue.AutoSize = true;
        lblActiveJobsValue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
        lblActiveJobsValue.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)32)), ((int)((byte)41)), ((int)((byte)74)));
        lblActiveJobsValue.Location = new System.Drawing.Point(308, 52);
        lblActiveJobsValue.Name = "lblActiveJobsValue";
        lblActiveJobsValue.Size = new System.Drawing.Size(33, 37);
        lblActiveJobsValue.TabIndex = 7;
        lblActiveJobsValue.Text = "0";
        // 
        // lblTotalStudentsValue
        // 
        lblTotalStudentsValue.AutoSize = true;
        lblTotalStudentsValue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
        lblTotalStudentsValue.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)32)), ((int)((byte)41)), ((int)((byte)74)));
        lblTotalStudentsValue.Location = new System.Drawing.Point(308, 2);
        lblTotalStudentsValue.Name = "lblTotalStudentsValue";
        lblTotalStudentsValue.Size = new System.Drawing.Size(33, 37);
        lblTotalStudentsValue.TabIndex = 6;
        lblTotalStudentsValue.Text = "0";
        // 
        // lblPending
        // 
        lblPending.AutoSize = true;
        lblPending.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        lblPending.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)77)), ((int)((byte)88)), ((int)((byte)110)));
        lblPending.Location = new System.Drawing.Point(26, 260);
        lblPending.Name = "lblPending";
        lblPending.Size = new System.Drawing.Size(180, 23);
        lblPending.TabIndex = 5;
        lblPending.Text = "Pending Applications";
        // 
        // lblRejected
        // 
        lblRejected.AutoSize = true;
        lblRejected.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        lblRejected.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)77)), ((int)((byte)88)), ((int)((byte)110)));
        lblRejected.Location = new System.Drawing.Point(26, 210);
        lblRejected.Name = "lblRejected";
        lblRejected.Size = new System.Drawing.Size(184, 23);
        lblRejected.TabIndex = 4;
        lblRejected.Text = "Rejected Applications";
        // 
        // lblAccepted
        // 
        lblAccepted.AutoSize = true;
        lblAccepted.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        lblAccepted.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)77)), ((int)((byte)88)), ((int)((byte)110)));
        lblAccepted.Location = new System.Drawing.Point(26, 160);
        lblAccepted.Name = "lblAccepted";
        lblAccepted.Size = new System.Drawing.Size(190, 23);
        lblAccepted.TabIndex = 3;
        lblAccepted.Text = "Accepted Applications";
        // 
        // lblTotalApplications
        // 
        lblTotalApplications.AutoSize = true;
        lblTotalApplications.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        lblTotalApplications.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)77)), ((int)((byte)88)), ((int)((byte)110)));
        lblTotalApplications.Location = new System.Drawing.Point(26, 110);
        lblTotalApplications.Name = "lblTotalApplications";
        lblTotalApplications.Size = new System.Drawing.Size(154, 23);
        lblTotalApplications.TabIndex = 2;
        lblTotalApplications.Text = "Total Applications";
        // 
        // lblActiveJobs
        // 
        lblActiveJobs.AutoSize = true;
        lblActiveJobs.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        lblActiveJobs.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)77)), ((int)((byte)88)), ((int)((byte)110)));
        lblActiveJobs.Location = new System.Drawing.Point(26, 60);
        lblActiveJobs.Name = "lblActiveJobs";
        lblActiveJobs.Size = new System.Drawing.Size(101, 23);
        lblActiveJobs.TabIndex = 1;
        lblActiveJobs.Text = "Active Jobs";
        // 
        // lblTotalStudents
        // 
        lblTotalStudents.AutoSize = true;
        lblTotalStudents.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        lblTotalStudents.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)77)), ((int)((byte)88)), ((int)((byte)110)));
        lblTotalStudents.Location = new System.Drawing.Point(26, 10);
        lblTotalStudents.Name = "lblTotalStudents";
        lblTotalStudents.Size = new System.Drawing.Size(125, 23);
        lblTotalStudents.TabIndex = 0;
        lblTotalStudents.Text = "Total Students";
        // 
        // lblAuditLog
        // 
        lblAuditLog.AutoSize = true;
        lblAuditLog.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        lblAuditLog.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)77)), ((int)((byte)88)), ((int)((byte)110)));
        lblAuditLog.Location = new System.Drawing.Point(26, 375);
        lblAuditLog.Name = "lblAuditLog";
        lblAuditLog.Size = new System.Drawing.Size(166, 23);
        lblAuditLog.TabIndex = 13;
        lblAuditLog.Text = "Admin Activity Log";
        // 
        // lstAuditLog
        // 
        lstAuditLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        lstAuditLog.Font = new System.Drawing.Font("Segoe UI", 9F);
        lstAuditLog.Location = new System.Drawing.Point(26, 400);
        lstAuditLog.Name = "lstAuditLog";
        lstAuditLog.Size = new System.Drawing.Size(913, 102);
        lstAuditLog.TabIndex = 14;
        // 
        // AdminDashboardForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.FromArgb(((int)((byte)243)), ((int)((byte)246)), ((int)((byte)252)));
        ClientSize = new System.Drawing.Size(994, 639);
        Controls.Add(tabMain);
        Controls.Add(lblWelcome);
        Font = new System.Drawing.Font("Segoe UI", 9F);
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        Text = "Desktop Admin Dashboard";
        Load += AdminDashboardForm_Load;
        tabMain.ResumeLayout(false);
        tabJobs.ResumeLayout(false);
        tabJobs.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvJobs).EndInit();
        tabApplications.ResumeLayout(false);
        tabApplications.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvApplications).EndInit();
        tabReports.ResumeLayout(false);
        tabReports.PerformLayout();
        ResumeLayout(false);
    }
}
#nullable restore
