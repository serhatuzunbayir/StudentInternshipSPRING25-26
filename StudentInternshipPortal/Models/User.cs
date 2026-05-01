using System;

namespace StudentInternshipPortal.Models
{
    /// <summary>
    /// Represents a user in the system. Can be either an Admin or a Student.
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "Student"; // "Admin" or "Student"
    }
}
