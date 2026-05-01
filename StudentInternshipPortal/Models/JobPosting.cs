using System;

namespace StudentInternshipPortal.Models
{
    /// <summary>
    /// Represents an internship or job posting created by an admin.
    /// Contains job details and required skills for matching.
    /// </summary>
    public class JobPosting
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string RequiredSkills { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
