namespace StudentInternshipJobPortal.Shared.Models;

public class User
{
    public int Id { get; set; }
    public string NameSurname { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    public StudentProfile? StudentProfile { get; set; }
    public ICollection<JobApplication> Applications { get; set; } = new List<JobApplication>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
