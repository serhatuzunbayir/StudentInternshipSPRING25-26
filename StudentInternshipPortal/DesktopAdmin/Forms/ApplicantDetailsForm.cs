using DesktopAdmin.ViewModels;

namespace DesktopAdmin.Forms;

public class ApplicantDetailsForm : Form
{
    public ApplicantDetailsForm(
        StudentProfileDetailsViewModel profileDetails,
        string jobTitle,
        int matchPercentage,
        string resumeFileName)
    {
        Text = "Applicant Details";
        StartPosition = FormStartPosition.CenterParent;
        Size = new Size(760, 620);
        MinimumSize = new Size(680, 520);

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 9,
            Padding = new Padding(16),
            AutoScroll = true
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

        AddField(layout, 0, "Full Name", CreateReadOnlyBox(profileDetails.FullName));
        AddField(layout, 1, "Email", CreateReadOnlyBox(profileDetails.Email));
        AddField(layout, 2, "Phone", CreateReadOnlyBox(profileDetails.Phone));
        AddField(layout, 3, "Job Title", CreateReadOnlyBox(jobTitle));
        AddField(layout, 4, "Match %", CreateReadOnlyBox($"{matchPercentage}%"));
        AddField(layout, 5, "CV Source", CreateReadOnlyBox(string.IsNullOrWhiteSpace(resumeFileName) ? "None" : resumeFileName));
        AddField(layout, 6, "Skills", CreateMultilineBox(profileDetails.Skills));
        AddField(layout, 7, "Education", CreateMultilineBox(profileDetails.Education));
        AddField(layout, 8, "Experience / About", CreateMultilineBox(JoinSections(profileDetails.Experience, profileDetails.AboutMe)));

        var closeButton = new Button
        {
            Text = "Close",
            AutoSize = true,
            Anchor = AnchorStyles.Right
        };
        closeButton.Click += (_, _) => Close();

        var buttonPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            FlowDirection = FlowDirection.RightToLeft,
            Padding = new Padding(16, 0, 16, 16),
            Height = 56
        };
        buttonPanel.Controls.Add(closeButton);

        Controls.Add(layout);
        Controls.Add(buttonPanel);
    }

    private static void AddField(TableLayoutPanel layout, int rowIndex, string labelText, Control editor)
    {
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        var label = new Label
        {
            Text = labelText,
            AutoSize = true,
            Margin = new Padding(3, 8, 12, 8),
            Font = new Font("Segoe UI", 9F, FontStyle.Bold)
        };

        editor.Margin = new Padding(3, 4, 3, 12);

        layout.Controls.Add(label, 0, rowIndex);
        layout.Controls.Add(editor, 1, rowIndex);
    }

    private static TextBox CreateReadOnlyBox(string value)
    {
        return new TextBox
        {
            ReadOnly = true,
            Text = value,
            Dock = DockStyle.Top,
            Width = 520
        };
    }

    private static TextBox CreateMultilineBox(string value)
    {
        return new TextBox
        {
            ReadOnly = true,
            Text = value,
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            Dock = DockStyle.Top,
            Height = 96,
            Width = 520
        };
    }

    private static string JoinSections(string experience, string aboutMe)
    {
        if (string.IsNullOrWhiteSpace(experience))
        {
            return aboutMe;
        }

        if (string.IsNullOrWhiteSpace(aboutMe))
        {
            return experience;
        }

        return $"Experience:{Environment.NewLine}{experience}{Environment.NewLine}{Environment.NewLine}About Me:{Environment.NewLine}{aboutMe}";
    }
}
