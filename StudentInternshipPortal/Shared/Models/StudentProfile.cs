namespace StudentInternshipJobPortal.Shared.Models;

public class StudentProfile
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Skills { get; set; } = string.Empty;
    public string Education { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string AboutMe { get; set; } = string.Empty;

    public User User { get; set; } = null!;
}
