namespace StudentWeb.Models;

public class ApplicationListViewModel
{
    public List<ApplicationListItemViewModel> Applications { get; set; } = [];
}

public class ApplicationListItemViewModel
{
    public int Id { get; set; }
    public int JobId { get; set; }
    public string JobTitle { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string JobType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string AppliedAt { get; set; } = string.Empty;
    public string RequiredSkills { get; set; } = string.Empty;
}
