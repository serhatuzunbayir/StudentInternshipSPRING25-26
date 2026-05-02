#nullable disable
namespace DesktopAdmin.Forms;

partial class JobEditForm
{
    private System.ComponentModel.IContainer components = null;
    private Label lblTitle;
    private Label lblDescription;
    private Label lblRequiredSkills;
    private Label lblLocation;
    private Label lblJobType;
    private TextBox txtTitle;
    private TextBox txtDescription;
    private TextBox txtRequiredSkills;
    private TextBox txtLocation;
    private ComboBox cmbJobType;
    private CheckBox chkIsActive;
    private Button btnSave;
    private Button btnCancel;
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
        lblDescription = new Label();
        lblRequiredSkills = new Label();
        lblLocation = new Label();
        lblJobType = new Label();
        txtTitle = new TextBox();
        txtDescription = new TextBox();
        txtRequiredSkills = new TextBox();
        txtLocation = new TextBox();
        cmbJobType = new ComboBox();
        chkIsActive = new CheckBox();
        btnSave = new Button();
        btnCancel = new Button();
        lblError = new Label();
        SuspendLayout();
        lblTitle.AutoSize = true;
        lblTitle.Location = new Point(24, 24);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(38, 20);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Title";
        txtTitle.Location = new Point(144, 21);
        txtTitle.Name = "txtTitle";
        txtTitle.Size = new Size(261, 27);
        txtTitle.TabIndex = 1;
        lblDescription.AutoSize = true;
        lblDescription.Location = new Point(24, 67);
        lblDescription.Name = "lblDescription";
        lblDescription.Size = new Size(85, 20);
        lblDescription.TabIndex = 2;
        lblDescription.Text = "Description";
        txtDescription.Location = new Point(144, 64);
        txtDescription.Multiline = true;
        txtDescription.Name = "txtDescription";
        txtDescription.Size = new Size(261, 84);
        txtDescription.TabIndex = 3;
        lblRequiredSkills.AutoSize = true;
        lblRequiredSkills.Location = new Point(24, 165);
        lblRequiredSkills.Name = "lblRequiredSkills";
        lblRequiredSkills.Size = new Size(102, 20);
        lblRequiredSkills.TabIndex = 4;
        lblRequiredSkills.Text = "Required Skills";
        txtRequiredSkills.Location = new Point(144, 162);
        txtRequiredSkills.Name = "txtRequiredSkills";
        txtRequiredSkills.Size = new Size(261, 27);
        txtRequiredSkills.TabIndex = 5;
        lblLocation.AutoSize = true;
        lblLocation.Location = new Point(24, 208);
        lblLocation.Name = "lblLocation";
        lblLocation.Size = new Size(64, 20);
        lblLocation.TabIndex = 6;
        lblLocation.Text = "Location";
        txtLocation.Location = new Point(144, 205);
        txtLocation.Name = "txtLocation";
        txtLocation.Size = new Size(261, 27);
        txtLocation.TabIndex = 7;
        lblJobType.AutoSize = true;
        lblJobType.Location = new Point(24, 251);
        lblJobType.Name = "lblJobType";
        lblJobType.Size = new Size(67, 20);
        lblJobType.TabIndex = 8;
        lblJobType.Text = "Job Type";
        cmbJobType.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbJobType.FormattingEnabled = true;
        cmbJobType.Location = new Point(144, 248);
        cmbJobType.Name = "cmbJobType";
        cmbJobType.Size = new Size(261, 28);
        cmbJobType.TabIndex = 9;
        chkIsActive.AutoSize = true;
        chkIsActive.Location = new Point(144, 291);
        chkIsActive.Name = "chkIsActive";
        chkIsActive.Size = new Size(76, 24);
        chkIsActive.TabIndex = 10;
        chkIsActive.Text = "Active";
        chkIsActive.UseVisualStyleBackColor = true;
        btnSave.Location = new Point(144, 335);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(125, 36);
        btnSave.TabIndex = 11;
        btnSave.Text = "Save";
        btnSave.UseVisualStyleBackColor = true;
        btnSave.Click += btnSave_Click;
        btnCancel.Location = new Point(280, 335);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(125, 36);
        btnCancel.TabIndex = 12;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += btnCancel_Click;
        lblError.ForeColor = Color.Firebrick;
        lblError.Location = new Point(24, 384);
        lblError.Name = "lblError";
        lblError.Size = new Size(381, 34);
        lblError.TabIndex = 13;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(432, 427);
        Controls.Add(lblError);
        Controls.Add(btnCancel);
        Controls.Add(btnSave);
        Controls.Add(chkIsActive);
        Controls.Add(cmbJobType);
        Controls.Add(lblJobType);
        Controls.Add(txtLocation);
        Controls.Add(lblLocation);
        Controls.Add(txtRequiredSkills);
        Controls.Add(lblRequiredSkills);
        Controls.Add(txtDescription);
        Controls.Add(lblDescription);
        Controls.Add(txtTitle);
        Controls.Add(lblTitle);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "JobEditForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Job";
        ResumeLayout(false);
        PerformLayout();
    }
}
#nullable restore
