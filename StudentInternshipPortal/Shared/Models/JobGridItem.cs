namespace StudentInternshipJobPortal.Shared.Models;

public class JobGridItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string JobType { get; set; } = string.Empty;
    public string RequiredSkills { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
