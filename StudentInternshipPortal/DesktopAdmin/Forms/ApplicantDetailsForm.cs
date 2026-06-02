using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DesktopAdmin.ViewModels;
using Shared.Data;

namespace DesktopAdmin.Forms;

// This Windows Form displays a detailed view of a student's profile and job application status.
public class ApplicantDetailsForm : Form
{
    private readonly StudentProfileDetailsViewModel _profile;
    private readonly string _jobTitle;
    private readonly int _matchPercentage;
    private readonly string _resumeFileName;

    // Receive student data, target job, matching calculations, and reference to their CV.
    public ApplicantDetailsForm(
        StudentProfileDetailsViewModel profile,
        string jobTitle,
        int matchPercentage,
        string resumeFileName)
    {
        _profile = profile;
        _jobTitle = jobTitle;
        _matchPercentage = matchPercentage;
        _resumeFileName = resumeFileName;

        InitializeForm();
    }

    // Programmatically builds the entire UI layout of this form window.
    private void InitializeForm()
    {
        // Window general setups
        Text = $"Applicant Profile - {_profile.FullName}";
        Size = new Size(820, 680);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        BackColor = Color.FromArgb(250, 252, 255);
        Font = new Font("Segoe UI", 9F, FontStyle.Regular);

        // Header Panel to show user name and target job
        var pnlHeader = new Panel
        {
            Dock = DockStyle.Top,
            Height = 85,
            BackColor = Color.FromArgb(43, 53, 79)
        };

        var lblHeaderTitle = new Label
        {
            Text = _profile.FullName,
            Font = new Font("Segoe UI", 18F, FontStyle.Bold),
            ForeColor = Color.White,
            Location = new Point(20, 12),
            AutoSize = true
        };

        var lblHeaderSub = new Label
        {
            Text = $"Applicant for position: {_jobTitle}",
            Font = new Font("Segoe UI", 9.5F, FontStyle.Italic),
            ForeColor = Color.FromArgb(200, 210, 230),
            Location = new Point(22, 48),
            AutoSize = true
        };

        pnlHeader.Controls.Add(lblHeaderTitle);
        pnlHeader.Controls.Add(lblHeaderSub);
        Controls.Add(pnlHeader);

        // Left Panel for metadata (Email, phone, match percentage, CV type)
        var pnlLeft = new Panel
        {
            Location = new Point(15, 100),
            Size = new Size(260, 465),
            BackColor = Color.FromArgb(240, 244, 250),
            BorderStyle = BorderStyle.None
        };
        
        // Draw custom borders on Left panel
        pnlLeft.Paint += (s, e) =>
        {
            using var pen = new Pen(Color.FromArgb(220, 228, 240), 1);
            e.Graphics.DrawRectangle(pen, 0, 0, pnlLeft.Width - 1, pnlLeft.Height - 1);
        };

        // Add details to the left sidebar panel
        AddLeftDetail(pnlLeft, "Email", _profile.Email, 20);
        AddLeftDetail(pnlLeft, "Phone", string.IsNullOrWhiteSpace(_profile.Phone) ? "Not Provided" : _profile.Phone, 80);
        AddLeftDetail(pnlLeft, "Skill Match Ratio", $"{_matchPercentage}%", 140);

        // Determine if they uploaded a file or generated a profile CV
        var cvSource = string.IsNullOrEmpty(_resumeFileName) 
            ? "None" 
            : (_resumeFileName == "profile" ? "Profile CV" : "Uploaded CV");

        AddLeftDetail(pnlLeft, "CV Source Option", cvSource, 200);
        AddLeftDetail(pnlLeft, "CV File Reference", string.IsNullOrEmpty(_resumeFileName) ? "N/A" : _resumeFileName, 260);

        Controls.Add(pnlLeft);

        // Right Panel to display long texts (About candidate, education, skills, experience)
        var pnlRight = new Panel
        {
            Location = new Point(290, 100),
            Size = new Size(500, 465),
            BackColor = Color.White
        };
        pnlRight.Paint += (s, e) =>
        {
            using var pen = new Pen(Color.FromArgb(228, 234, 244), 1);
            e.Graphics.DrawRectangle(pen, 0, 0, pnlRight.Width - 1, pnlRight.Height - 1);
        };

        // Populate sections in the right panel
        AddRightSection(pnlRight, "About Candidate", _profile.AboutMe, 15, 80);
        AddRightSection(pnlRight, "Skills & Key Technologies", _profile.Skills, 125, 60);
        AddRightSection(pnlRight, "Education & Academics", _profile.Education, 215, 80);
        AddRightSection(pnlRight, "Professional Experience", _profile.Experience, 325, 110);

        Controls.Add(pnlRight);

        // Bottom panel for action buttons
        var pnlBottom = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 65,
            BackColor = Color.FromArgb(245, 247, 250)
        };
        pnlBottom.Paint += (s, e) =>
        {
            using var pen = new Pen(Color.FromArgb(228, 234, 244), 1);
            e.Graphics.DrawLine(pen, 0, 0, pnlBottom.Width, 0);
        };

        var btnClose = new Button
        {
            Text = "Close",
            Size = new Size(95, 34),
            Location = new Point(700, 15),
            BackColor = Color.White,
            ForeColor = Color.FromArgb(87, 97, 122),
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            Cursor = Cursors.Hand
        };
        btnClose.FlatAppearance.BorderColor = Color.FromArgb(199, 208, 225);
        btnClose.Click += (s, e) => Close();

        var btnViewCV = new Button
        {
            Text = "View Uploaded CV File",
            Size = new Size(180, 34),
            Location = new Point(505, 15),
            BackColor = Color.FromArgb(31, 119, 90),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            Cursor = Cursors.Hand
        };
        btnViewCV.FlatAppearance.BorderSize = 0;
        
        // If they did not upload a separate file, disable the view button.
        if (string.IsNullOrEmpty(_resumeFileName) || _resumeFileName == "profile")
        {
            btnViewCV.Enabled = false;
            btnViewCV.BackColor = Color.FromArgb(190, 200, 195);
            btnViewCV.ForeColor = Color.FromArgb(130, 140, 135);
            btnViewCV.Text = "No Uploaded CV File";
        }
        else
        {
            btnViewCV.Click += btnViewCV_Click;
        }

        pnlBottom.Controls.Add(btnViewCV);
        pnlBottom.Controls.Add(btnClose);
        Controls.Add(pnlBottom);
    }

    // Helper method to draw structured single-line read-only details on left panel.
    private void AddLeftDetail(Panel parent, string labelText, string valueText, int y)
    {
        var lbl = new Label
        {
            Text = labelText.ToUpper(),
            Font = new Font("Segoe UI", 8F, FontStyle.Bold),
            ForeColor = Color.FromArgb(100, 110, 130),
            Location = new Point(15, y),
            AutoSize = true
        };

        var txt = new TextBox
        {
            Text = valueText,
            ReadOnly = true,
            BorderStyle = BorderStyle.None,
            BackColor = Color.FromArgb(240, 244, 250),
            ForeColor = Color.FromArgb(43, 53, 79),
            Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
            Location = new Point(15, y + 18),
            Size = new Size(230, 22),
            WordWrap = true
        };

        parent.Controls.Add(lbl);
        parent.Controls.Add(txt);
    }

    // Helper method to draw multiline text boxes on right panel.
    private void AddRightSection(Panel parent, string sectionTitle, string contentText, int y, int height)
    {
        var lbl = new Label
        {
            Text = sectionTitle,
            Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
            ForeColor = Color.FromArgb(43, 53, 79),
            Location = new Point(15, y),
            AutoSize = true
        };

        var txt = new TextBox
        {
            Text = string.IsNullOrWhiteSpace(contentText) ? "No information provided." : contentText,
            ReadOnly = true,
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            BorderStyle = BorderStyle.None,
            BackColor = Color.White,
            ForeColor = Color.FromArgb(70, 80, 95),
            Font = new Font("Segoe UI", 9F, FontStyle.Regular),
            Location = new Point(15, y + 20),
            Size = new Size(470, height)
        };

        // Draw visual divider line between blocks
        if (y < 325)
        {
            var linePanel = new Panel
            {
                BackColor = Color.FromArgb(240, 243, 247),
                Location = new Point(15, y + 20 + height + 5),
                Size = new Size(470, 1)
            };
            parent.Controls.Add(linePanel);
        }

        parent.Controls.Add(lbl);
        parent.Controls.Add(txt);
    }

    // Event handler to load and display the uploaded PDF/Doc file.
    private void btnViewCV_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_resumeFileName) || _resumeFileName == "profile")
        {
            MessageBox.Show("This student applied using their system profile CV. All details are displayed on this screen.", "Profile CV", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        try
        {
            // Find absolute path of database and solution folders
            string dbPath = DatabaseHelper.ResolveDatabasePath();
            string solutionDir = Path.GetDirectoryName(Path.GetDirectoryName(dbPath))!;
            string resumePath = Path.Combine(solutionDir, "StudentWeb", "wwwroot", "uploads", "resumes", _resumeFileName);

            if (!File.Exists(resumePath))
            {
                MessageBox.Show($"CV file not found on disk at:\n{resumePath}", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Launch the system default viewer to show the resume file
            Process.Start(new ProcessStartInfo(resumePath) { UseShellExecute = true });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Could not open CV file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

