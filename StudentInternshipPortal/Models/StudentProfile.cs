using System;

namespace StudentInternshipPortal.Models
{
    /// <summary>
    /// Represents a student's profile containing personal and academic information.
    /// Linked to a User via the UserId foreign key.
    /// </summary>
    public class StudentProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Foreign Key to User
        public string FullName { get; set; } = string.Empty;
        public string Skills { get; set; } = string.Empty; // Comma separated skills
        public string Education { get; set; } = string.Empty;

        // Navigation property
        public User User { get; set; } = null!;
    }
}
