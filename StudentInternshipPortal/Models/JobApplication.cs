using System;

namespace StudentInternshipPortal.Models
{
    /// <summary>
    /// Represents a student's application to a job posting.
    /// Tracks the application status (Pending, Accepted, Rejected).
    /// </summary>
    public class JobApplication
    {
        public int Id { get; set; }
        public int StudentId { get; set; } // Foreign Key to StudentProfile
        public int JobId { get; set; } // Foreign Key to JobPosting
        public string Status { get; set; } = "Pending"; // "Pending", "Accepted", "Rejected"
        public DateTime ApplicationDate { get; set; } = DateTime.Now;

        // Navigation properties
        public StudentProfile Student { get; set; } = null!;
        public JobPosting Job { get; set; } = null!;
    }
}
