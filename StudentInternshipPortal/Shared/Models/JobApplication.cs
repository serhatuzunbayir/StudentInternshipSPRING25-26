namespace StudentInternshipJobPortal.Shared.Models;

public class JobApplication
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int JobId { get; set; }
    public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = string.Empty;

    public User Student { get; set; } = null!;
    public Job Job { get; set; } = null!;
}
