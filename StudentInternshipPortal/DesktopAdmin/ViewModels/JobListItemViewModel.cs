namespace DesktopAdmin.ViewModels;

public class JobListItemViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string JobType { get; set; } = string.Empty;
    public string RequiredSkills { get; set; } = string.Empty;
    public string ActiveStatus { get; set; } = string.Empty;
}
