using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using StudentInternshipPortal.Data;
using StudentInternshipPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace StudentInternshipPortal
{
   
    public delegate void ApplicationStatusChangedHandler(object sender, string jobTitle, string newStatus);

   
    /// StudentDashboardForm: Main dashboard for logged-in students.
    /// Provides tabs for profile management, job browsing with LINQ filtering,
    /// job application, and application tracking.
    /// Implements delegate/event pattern for notification system.
    public class StudentDashboardForm : Form
    {
        private int userId;

        // Event for application status change notification (Delegate/Event Requirement)
        public event ApplicationStatusChangedHandler? OnApplicationStatusChanged;

        // UI Controls
        private TabControl tabControl = null!;

        // Tab 1: Profile
        private TextBox txtFullName = null!, txtSkills = null!, txtEducation = null!;
        private Button btnSaveProfile = null!;

        // Tab 2: Browse Jobs
        private DataGridView dgvJobs = null!;
        private TextBox txtSearchSkill = null!, txtSearchLocation = null!;
        private Button btnSearch = null!, btnClearSearch = null!, btnApply = null!;

        // Tab 3: My Applications
        private DataGridView dgvMyApplications = null!;
        private Button btnRefreshApps = null!;

        public StudentDashboardForm(int userId)
        {
            this.userId = userId;
            InitializeComponent();
            LoadProfile();
            LoadJobs();
            LoadMyApplications();
            CheckForStatusUpdates();

            this.OnApplicationStatusChanged += StudentDashboardForm_OnApplicationStatusChanged;
        }

        private void InitializeComponent()
        {
            this.Text = "Student Dashboard - Student Internship Portal";
            this.Size = new Size(950, 680);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);

            tabControl = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10) };

            // TAB 1: My Profile
            TabPage tabProfile = new TabPage("My Profile");
            tabProfile.BackColor = Color.White;
            tabProfile.Padding = new Padding(20);

            Label lblProfileTitle = new Label
            {
                Text = "📋 Student Profile Management",
                Left = 20, Top = 15, Width = 400,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41)
            };

            Label lblName = new Label { Text = "Full Name:", Left = 20, Top = 65, Width = 100, Font = new Font("Segoe UI", 10) };
            txtFullName = new TextBox { Left = 130, Top = 63, Width = 300, Font = new Font("Segoe UI", 10) };

            Label lblSkills = new Label { Text = "Skills:", Left = 20, Top = 105, Width = 100, Font = new Font("Segoe UI", 10) };
            txtSkills = new TextBox
            {
                Left = 130, Top = 103, Width = 300, Height = 60,
                Multiline = true, Font = new Font("Segoe UI", 10),
                PlaceholderText = "e.g. C#, Python, SQL, JavaScript"
            };

            Label lblEdu = new Label { Text = "Education:", Left = 20, Top = 175, Width = 100, Font = new Font("Segoe UI", 10) };
            txtEducation = new TextBox
            {
                Left = 130, Top = 173, Width = 300, Height = 60,
                Multiline = true, Font = new Font("Segoe UI", 10),
                PlaceholderText = "e.g. Computer Science, 3rd Year"
            };

            btnSaveProfile = new Button
            {
                Text = "💾 Save Profile",
                Left = 130, Top = 250, Width = 180, Height = 40,
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSaveProfile.FlatAppearance.BorderSize = 0;
            btnSaveProfile.Click += BtnSaveProfile_Click;

            tabProfile.Controls.AddRange(new Control[] { lblProfileTitle, lblName, txtFullName, lblSkills, txtSkills, lblEdu, txtEducation, btnSaveProfile });

  
            // TAB 2: Browse Jobs
            TabPage tabJobs = new TabPage("Browse Jobs");
            tabJobs.BackColor = Color.White;

            Label lblJobsTitle = new Label
            {
                Text = "🔍 Search & Apply for Internships",
                Left = 20, Top = 15, Width = 400,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41)
            };

            // Search/Filter area
            Label lblFilterSkill = new Label { Text = "Filter by Skill:", Left = 20, Top = 55, Width = 100, Font = new Font("Segoe UI", 9) };
            txtSearchSkill = new TextBox { Left = 125, Top = 53, Width = 160, Font = new Font("Segoe UI", 9), PlaceholderText = "e.g. C#" };

            Label lblFilterLoc = new Label { Text = "Location:", Left = 300, Top = 55, Width = 65, Font = new Font("Segoe UI", 9) };
            txtSearchLocation = new TextBox { Left = 370, Top = 53, Width = 140, Font = new Font("Segoe UI", 9), PlaceholderText = "e.g. Istanbul" };

            btnSearch = new Button
            {
                Text = "🔎 Search",
                Left = 530, Top = 50, Width = 100, Height = 30,
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Click += BtnSearch_Click;

            btnClearSearch = new Button
            {
                Text = "Clear",
                Left = 640, Top = 50, Width = 70, Height = 30,
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9),
                Cursor = Cursors.Hand
            };
            btnClearSearch.FlatAppearance.BorderSize = 0;
            btnClearSearch.Click += BtnClearSearch_Click;

            dgvJobs = new DataGridView
            {
                Left = 20, Top = 90, Width = 700, Height = 450,
                AllowUserToAddRows = false, ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                Font = new Font("Segoe UI", 9),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            btnApply = new Button
            {
                Text = "📨 Apply for Selected Job",
                Left = 740, Top = 90, Width = 160, Height = 45,
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnApply.FlatAppearance.BorderSize = 0;
            btnApply.Click += BtnApply_Click;

            tabJobs.Controls.AddRange(new Control[] { lblJobsTitle, lblFilterSkill, txtSearchSkill, lblFilterLoc, txtSearchLocation, btnSearch, btnClearSearch, dgvJobs, btnApply });

            // TAB 3: My Applications
            TabPage tabApps = new TabPage("My Applications");
            tabApps.BackColor = Color.White;

            Label lblAppsTitle = new Label
            {
                Text = "My Application Tracking",
                Left = 20, Top = 15, Width = 400,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41)
            };

            dgvMyApplications = new DataGridView
            {
                Left = 20, Top = 55, Width = 750, Height = 500,
                AllowUserToAddRows = false, ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                Font = new Font("Segoe UI", 9),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            btnRefreshApps = new Button
            {
                Text = "Refresh",
                Left = 790, Top = 55, Width = 110, Height = 40,
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRefreshApps.FlatAppearance.BorderSize = 0;
            btnRefreshApps.Click += (s, e) => { LoadMyApplications(); CheckForStatusUpdates(); };

            tabApps.Controls.AddRange(new Control[] { lblAppsTitle, dgvMyApplications, btnRefreshApps });

            // Add tabs
            tabControl.TabPages.Add(tabProfile);
            tabControl.TabPages.Add(tabJobs);
            tabControl.TabPages.Add(tabApps);

            this.Controls.Add(tabControl);
            this.FormClosed += (s, e) => Application.Exit();
        }

        // PROFILE MANAGEMENT 
        /// Loads existing student profile from database using LINQ.
        /// If profile exists, populates the form fields.
        private void LoadProfile()
        {
            using (var db = new AppDbContext())
            {
                // LINQ: Find student profile by UserId
                var profile = db.StudentProfiles.FirstOrDefault(p => p.UserId == userId);
                if (profile != null)
                {
                    txtFullName.Text = profile.FullName;
                    txtSkills.Text = profile.Skills;
                    txtEducation.Text = profile.Education;
                }
            }
        }

        /// Saves or updates the student profile.
        /// Uses LINQ to check if profile exists, then performs INSERT or UPDATE.
        private void BtnSaveProfile_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Full name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var db = new AppDbContext())
            {
                // LINQ: Check if profile already exists for this user
                var profile = db.StudentProfiles.FirstOrDefault(p => p.UserId == userId);

                if (profile != null)
                {
                    // UPDATE existing profile
                    profile.FullName = txtFullName.Text.Trim();
                    profile.Skills = txtSkills.Text.Trim();
                    profile.Education = txtEducation.Text.Trim();
                }
                else
                {
                    // INSERT new profile
                    profile = new StudentProfile
                    {
                        UserId = userId,
                        FullName = txtFullName.Text.Trim(),
                        Skills = txtSkills.Text.Trim(),
                        Education = txtEducation.Text.Trim()
                    };
                    db.StudentProfiles.Add(profile);
                }

                db.SaveChanges();
                MessageBox.Show("Profile saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // BROWSE & FILTER JOBS
        /// Loads all active job postings into the DataGridView.
        /// Uses LINQ Where clause to filter only active jobs.
        private void LoadJobs(string? skillFilter = null, string? locationFilter = null)
        {
            using (var db = new AppDbContext())
            {
                // LINQ Filtering Requirement: Filter jobs by skill and/or location
                var query = db.JobPostings.Where(j => j.IsActive);

                if (!string.IsNullOrWhiteSpace(skillFilter))
                {
                    // LINQ: Case-insensitive skill filter using Contains
                    query = query.Where(j => j.RequiredSkills.ToLower().Contains(skillFilter.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(locationFilter))
                {
                    // LINQ: Case-insensitive location filter using Contains
                    query = query.Where(j => j.Location.ToLower().Contains(locationFilter.ToLower()));
                }

                var jobs = query.Select(j => new
                {
                    j.Id,
                    j.Title,
                    j.Description,
                    Skills = j.RequiredSkills,
                    j.Location
                }).ToList();

                dgvJobs.DataSource = jobs;
            }
        }

        /// Search button handler: Applies LINQ filters based on user input.
        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            LoadJobs(txtSearchSkill.Text.Trim(), txtSearchLocation.Text.Trim());
        }

        /// Clears search filters and reloads all active jobs.
        private void BtnClearSearch_Click(object? sender, EventArgs e)
        {
            txtSearchSkill.Clear();
            txtSearchLocation.Clear();
            LoadJobs();
        }

        // APPLY FOR JOBS

        /// Applies for the selected job posting.
        /// Creates a new JobApplication with Status="Pending" and current date.
        /// Uses LINQ to check for duplicate applications.
        private void BtnApply_Click(object? sender, EventArgs e)
        {
            if (dgvJobs.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a job to apply for.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var cellValue = dgvJobs.SelectedRows[0].Cells["Id"].Value;
            if (cellValue == null) return;
            int jobId = (int)cellValue;

            using (var db = new AppDbContext())
            {
                // Ensure the student has a profile
                var profile = db.StudentProfiles.FirstOrDefault(p => p.UserId == userId);
                if (profile == null)
                {
                    MessageBox.Show("Please create your profile first (My Profile tab).", "Profile Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabControl.SelectedIndex = 0; // Switch to profile tab
                    return;
                }

                // LINQ: Check if already applied to this job
                bool alreadyApplied = db.Applications.Any(a => a.StudentId == profile.Id && a.JobId == jobId);
                if (alreadyApplied)
                {
                    MessageBox.Show("You have already applied for this job!", "Duplicate Application", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var application = new JobApplication
                {
                    StudentId = profile.Id,
                    JobId = jobId,
                    Status = "Pending",
                    ApplicationDate = DateTime.Now
                };

                db.Applications.Add(application);
                db.SaveChanges();

                string jobTitle = dgvJobs.SelectedRows[0].Cells["Title"].Value?.ToString() ?? "Unknown";
                MessageBox.Show($"Successfully applied for '{jobTitle}'!", "Application Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LoadMyApplications();
        }

        // MY APPLICATIONS
        /// Loads the student's applications with job details using LINQ joins.
        private void LoadMyApplications()
        {
            using (var db = new AppDbContext())
            {
                var profile = db.StudentProfiles.FirstOrDefault(p => p.UserId == userId);
                if (profile == null)
                {
                    dgvMyApplications.DataSource = null;
                    return;
                }

                // LINQ: Join Applications with JobPostings to get full details
                var myApps = db.Applications
                    .Include(a => a.Job)
                    .Where(a => a.StudentId == profile.Id)
                    .Select(a => new
                    {
                        a.Id,
                        JobTitle = a.Job != null ? a.Job.Title : "Unknown",
                        Location = a.Job != null ? a.Job.Location : "-",
                        a.Status,
                        Applied = a.ApplicationDate.ToString("yyyy-MM-dd HH:mm")
                    })
                    .OrderByDescending(a => a.Applied)
                    .ToList();

                dgvMyApplications.DataSource = myApps;
            }
        }

        // DELEGATE/EVENT: Notification System
        /// Checks if any of the student's applications have been accepted.
        /// Fires the OnApplicationStatusChanged event/delegate for each accepted application.
        /// This simulates the notification system requirement.
        private void CheckForStatusUpdates()
        {
            using (var db = new AppDbContext())
            {
                var profile = db.StudentProfiles.FirstOrDefault(p => p.UserId == userId);
                if (profile == null) return;

                // LINQ: Find applications that have been accepted
                var acceptedApps = db.Applications
                    .Include(a => a.Job)
                    .Where(a => a.StudentId == profile.Id && a.Status == "Accepted")
                    .ToList();

                foreach (var app in acceptedApps)
                {
                    string jobTitle = app.Job?.Title ?? "Unknown";
                    // Fire the delegate/event for notification
                    OnApplicationStatusChanged?.Invoke(this, jobTitle, app.Status);
                }
            }
        }

        /// Event handler subscribed via delegate: Shows notification when application status changes.
        /// This demonstrates the delegate/event pattern requirement.
        private void StudentDashboardForm_OnApplicationStatusChanged(object sender, string jobTitle, string newStatus)
        {
            MessageBox.Show(
                $" Congratulations! Your application for '{jobTitle}' has been {newStatus}!",
                "Application Status Notification (via Delegate)",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}
