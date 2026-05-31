namespace StudentWeb.Models;

public class JobSearchViewModel
{
    public string Skill { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string JobType { get; set; } = string.Empty;
    public List<JobSearchResultViewModel> Results { get; set; } = [];
}

public class JobSearchResultViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string RequiredSkills { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string JobType { get; set; } = string.Empty;
    public int MatchPercentage { get; set; }
    public bool HasApplied { get; set; }
    public string ApplicationStatus { get; set; } = string.Empty;
}
