using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using StudentInternshipPortal.Data;
using StudentInternshipPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace StudentInternshipPortal
{
    /// Custom delegate for job notification events (Requirement: Delegates).
    /// Fired when a new job posting is successfully added.
    public delegate void JobAddedEventHandler(object sender, string jobTitle);

    /// AdminDashboardForm: Main dashboard for admin users.
    /// Features three tabs:
    ///   Tab 1 - Manage Job Postings (CRUD operations)
    ///   Tab 2 - Manage Applications (Accept/Reject)
    ///   Tab 3 - Reports & Stats (LINQ Aggregation)
    public class AdminDashboardForm : Form
    {
        private int adminId;

        // Event based on the custom delegate (Requirement)
        public event JobAddedEventHandler? OnJobAdded;

        // UI Controls
        private TabControl tabControl = null!;

        // Tab 1: Jobs
        private TextBox txtTitle = null!, txtDesc = null!, txtSkills = null!, txtLocation = null!;
        private DataGridView dgvJobs = null!;

        // Tab 2: Applications
        private DataGridView dgvApplications = null!;

        // Tab 3: Reports
        private Label lblTotalJobs = null!, lblPendingApps = null!, lblAcceptedApps = null!, lblRejectedApps = null!, lblTotalStudents = null!;

        public AdminDashboardForm(int adminId)
        {
            this.adminId = adminId;
            InitializeComponent();
            LoadData();

            // Subscribe to the delegate event (Requirement: Delegates)
            this.OnJobAdded += AdminDashboardForm_OnJobAdded;
        }

        private void InitializeComponent()
        {
            this.Text = "Admin Dashboard - Student Internship Portal";
            this.Size = new Size(950, 680);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);

            tabControl = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10) };

            // TAB 1: Add/Manage Jobs
            TabPage tabJobs = new TabPage("Manage Job Postings");
            tabJobs.BackColor = Color.White;

            Label lblJobTitle = new Label
            {
                Text = "📋 Job Posting Management",
                Left = 20, Top = 10, Width = 300,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41)
            };

            Label lbl1 = new Label { Text = "Title:", Left = 20, Top = 50, Width = 80, Font = new Font("Segoe UI", 10) };
            txtTitle = new TextBox { Left = 100, Top = 48, Width = 210, Font = new Font("Segoe UI", 10) };

            Label lbl2 = new Label { Text = "Description:", Left = 20, Top = 85, Width = 80, Font = new Font("Segoe UI", 10) };
            txtDesc = new TextBox { Left = 100, Top = 83, Width = 210, Multiline = true, Height = 50, Font = new Font("Segoe UI", 9) };

            Label lbl3 = new Label { Text = "Req. Skills:", Left = 20, Top = 145, Width = 80, Font = new Font("Segoe UI", 10) };
            txtSkills = new TextBox { Left = 100, Top = 143, Width = 210, Font = new Font("Segoe UI", 10) };

            Label lbl4 = new Label { Text = "Location:", Left = 20, Top = 180, Width = 80, Font = new Font("Segoe UI", 10) };
            txtLocation = new TextBox { Left = 100, Top = 178, Width = 210, Font = new Font("Segoe UI", 10) };

            Button btnAddJob = new Button
            {
                Text = "Add Job",
                Left = 20, Top = 220, Width = 140, Height = 38,
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnAddJob.FlatAppearance.BorderSize = 0;
            btnAddJob.Click += BtnAddJob_Click;

            Button btnDeleteJob = new Button
            {
                Text = "Delete Selected",
                Left = 170, Top = 220, Width = 140, Height = 38,
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnDeleteJob.FlatAppearance.BorderSize = 0;
            btnDeleteJob.Click += BtnDeleteJob_Click;

            dgvJobs = new DataGridView
            {
                Left = 330, Top = 10, Width = 580, Height = 560,
                AllowUserToAddRows = false, ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                Font = new Font("Segoe UI", 9),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            tabJobs.Controls.AddRange(new Control[] { lblJobTitle, lbl1, txtTitle, lbl2, txtDesc, lbl3, txtSkills, lbl4, txtLocation, btnAddJob, btnDeleteJob, dgvJobs });

            // TAB 2: Manage Applications
            TabPage tabApps = new TabPage("Manage Applications");
            tabApps.BackColor = Color.White;

            Label lblAppsTitle = new Label
            {
                Text = "Application Review",
                Left = 20, Top = 10, Width = 300,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41)
            };

            dgvApplications = new DataGridView
            {
                Left = 20, Top = 50, Width = 680, Height = 530,
                AllowUserToAddRows = false, ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                Font = new Font("Segoe UI", 9),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            Button btnAccept = new Button
            {
                Text = "Accept",
                Left = 720, Top = 50, Width = 170, Height = 45,
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnAccept.FlatAppearance.BorderSize = 0;
            btnAccept.Click += BtnAccept_Click;

            Button btnReject = new Button
            {
                Text = "Reject",
                Left = 720, Top = 110, Width = 170, Height = 45,
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnReject.FlatAppearance.BorderSize = 0;
            btnReject.Click += BtnReject_Click;

            tabApps.Controls.AddRange(new Control[] { lblAppsTitle, dgvApplications, btnAccept, btnReject });

            // TAB 3: Reports & Statistics
            TabPage tabReports = new TabPage("Reports & Stats");
            tabReports.BackColor = Color.White;

            Label lblReportTitle = new Label
            {
                Text = "System Statistics & Reports",
                Left = 30, Top = 20, Width = 400,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41)
            };

            lblTotalJobs = new Label { Left = 30, Top = 80, Width = 350, Font = new Font("Segoe UI", 13), ForeColor = Color.FromArgb(0, 123, 255) };
            lblTotalStudents = new Label { Left = 30, Top = 120, Width = 350, Font = new Font("Segoe UI", 13), ForeColor = Color.FromArgb(108, 117, 125) };
            lblPendingApps = new Label { Left = 30, Top = 160, Width = 350, Font = new Font("Segoe UI", 13), ForeColor = Color.FromArgb(255, 193, 7) };
            lblAcceptedApps = new Label { Left = 30, Top = 200, Width = 350, Font = new Font("Segoe UI", 13), ForeColor = Color.FromArgb(40, 167, 69) };
            lblRejectedApps = new Label { Left = 30, Top = 240, Width = 350, Font = new Font("Segoe UI", 13), ForeColor = Color.FromArgb(220, 53, 69) };

            Button btnRefreshReports = new Button
            {
                Text = "Refresh Reports",
                Left = 30, Top = 300, Width = 180, Height = 40,
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRefreshReports.FlatAppearance.BorderSize = 0;
            btnRefreshReports.Click += (s, e) => UpdateReports();

            tabReports.Controls.AddRange(new Control[] { lblReportTitle, lblTotalJobs, lblTotalStudents, lblPendingApps, lblAcceptedApps, lblRejectedApps, btnRefreshReports });

            tabControl.TabPages.Add(tabJobs);
            tabControl.TabPages.Add(tabApps);
            tabControl.TabPages.Add(tabReports);

            this.Controls.Add(tabControl);
            this.FormClosed += (s, e) => Application.Exit();
        }

        /// Loads all data into DataGridViews and refreshes reports.
        private void LoadData()
        {
            using (var db = new AppDbContext())
            {
                // Load job postings
                dgvJobs.DataSource = db.JobPostings.Select(j => new
                {
                    j.Id,
                    j.Title,
                    j.RequiredSkills,
                    j.Location,
                    Active = j.IsActive ? "Yes" : "No"
                }).ToList();

                // LINQ: Fetch applications with student and job info using Include (joins)
                var apps = db.Applications
                    .Include(a => a.Student)
                    .Include(a => a.Job)
                    .Select(a => new
                    {
                        a.Id,
                        StudentName = a.Student != null ? a.Student.FullName : "Unknown",
                        JobTitle = a.Job != null ? a.Job.Title : "Unknown",
                        a.Status,
                        Applied = a.ApplicationDate.ToString("yyyy-MM-dd")
                    }).ToList();

                dgvApplications.DataSource = apps;
            }
            UpdateReports();
        }

        /// LINQ Aggregation Requirement: Calculates and displays system statistics.
        /// Uses Count, Count with predicate, and other LINQ aggregation methods.
        private void UpdateReports()
        {
            using (var db = new AppDbContext())
            {
                // LINQ Aggregation queries for reporting
                int totalJobs = db.JobPostings.Count();
                int totalStudents = db.StudentProfiles.Count();
                int pending = db.Applications.Count(a => a.Status == "Pending");
                int accepted = db.Applications.Count(a => a.Status == "Accepted");
                int rejected = db.Applications.Count(a => a.Status == "Rejected");

                lblTotalJobs.Text = $"📋 Total Job Postings: {totalJobs}";
                lblTotalStudents.Text = $"👥 Registered Students: {totalStudents}";
                lblPendingApps.Text = $"⏳ Pending Applications: {pending}";
                lblAcceptedApps.Text = $"✅ Accepted Applications: {accepted}";
                lblRejectedApps.Text = $"❌ Rejected Applications: {rejected}";
            }
        }

        /// Adds a new job posting to the database and triggers the delegate event.
        private void BtnAddJob_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtSkills.Text))
            {
                MessageBox.Show("Title and Required Skills cannot be empty.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var db = new AppDbContext())
            {
                var newJob = new JobPosting
                {
                    Title = txtTitle.Text.Trim(),
                    Description = txtDesc.Text.Trim(),
                    RequiredSkills = txtSkills.Text.Trim(),
                    Location = txtLocation.Text.Trim(),
                    IsActive = true
                };

                db.JobPostings.Add(newJob);
                db.SaveChanges();

                // Trigger the delegate event (Requirement: Delegates/Events)
                OnJobAdded?.Invoke(this, newJob.Title);
            }

            txtTitle.Clear(); txtDesc.Clear(); txtSkills.Clear(); txtLocation.Clear();
            LoadData();
        }

        /// Delegate event handler: Displays notification when a new job is added.
        /// This demonstrates the delegate/event pattern requirement.
        private void AdminDashboardForm_OnJobAdded(object sender, string jobTitle)
        {
            MessageBox.Show(
                $"Notification (via Delegate): A new job posting '{jobTitle}' has been successfully added!",
                "System Notification",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        /// Deletes the selected job posting from the database.
        private void BtnDeleteJob_Click(object? sender, EventArgs e)
        {
            if (dgvJobs.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a job to delete.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var jobCellValue = dgvJobs.SelectedRows[0].Cells["Id"].Value;
            if (jobCellValue == null) return;
            int jobId = (int)jobCellValue;
            string jobTitle = dgvJobs.SelectedRows[0].Cells["Title"].Value?.ToString() ?? "Unknown";

            var result = MessageBox.Show(
                $"Are you sure you want to delete '{jobTitle}'?\nThis will also remove all related applications.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                using (var db = new AppDbContext())
                {
                    // First delete related applications (cascade)
                    var relatedApps = db.Applications.Where(a => a.JobId == jobId).ToList();
                    db.Applications.RemoveRange(relatedApps);

                    var job = db.JobPostings.Find(jobId);
                    if (job != null)
                    {
                        db.JobPostings.Remove(job);
                        db.SaveChanges();
                        MessageBox.Show($"Job '{jobTitle}' deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                LoadData();
            }
        }

        private void BtnAccept_Click(object? sender, EventArgs e)
        {
            ChangeApplicationStatus("Accepted");
        }

        private void BtnReject_Click(object? sender, EventArgs e)
        {
            ChangeApplicationStatus("Rejected");
        }

        /// Updates the status of the selected application in the database.
        private void ChangeApplicationStatus(string newStatus)
        {
            if (dgvApplications.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an application first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var appCellValue = dgvApplications.SelectedRows[0].Cells["Id"].Value;
            if (appCellValue == null) return;
            int appId = (int)appCellValue;

            using (var db = new AppDbContext())
            {
                var app = db.Applications.Find(appId);
                if (app != null)
                {
                    app.Status = newStatus;
                    db.SaveChanges();
                    MessageBox.Show($"Application has been marked as '{newStatus}'.", "Status Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            LoadData();
        }
    }
}
