namespace DesktopAdmin.ViewModels;

public class ApplicationListItemViewModel
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string AppliedAt { get; set; } = string.Empty;
    public int MatchPercentage { get; set; }
    public string ResumeFileName { get; set; } = string.Empty;
    public string CVSource => string.IsNullOrEmpty(ResumeFileName) ? "None" : (ResumeFileName == "profile" ? "Profile CV" : "Uploaded CV");
}
