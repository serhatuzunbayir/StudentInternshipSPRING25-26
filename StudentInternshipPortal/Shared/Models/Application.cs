using Shared.Enums;

namespace Shared.Models;

public class Application
{
    public int Id { get; set; }
    public int StudentProfileId { get; set; }
    public int JobId { get; set; }
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
    public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? ResumeFileName { get; set; }
}
