namespace StudentInternshipJobPortal.Shared.Models;

public class AdminApplicationListItem
{
    public int Id { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime ApplicationDate { get; set; }
}
