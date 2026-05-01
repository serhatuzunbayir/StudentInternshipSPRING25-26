using Microsoft.EntityFrameworkCore;
using StudentInternshipPortal.Models;
using System;
using System.IO;

namespace StudentInternshipPortal.Data
{
    /// <summary>
    /// Application database context using Entity Framework Core with SQLite.
    /// Manages all database operations for Users, StudentProfiles, JobPostings, and Applications.
    /// Seeds a default admin user on first creation.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>DbSet for User entities (Admin and Student accounts).</summary>
        public DbSet<User> Users { get; set; }

        /// <summary>DbSet for StudentProfile entities.</summary>
        public DbSet<StudentProfile> StudentProfiles { get; set; }

        /// <summary>DbSet for JobPosting entities (internship/job listings).</summary>
        public DbSet<JobPosting> JobPostings { get; set; }

        /// <summary>DbSet for JobApplication entities (student applications).</summary>
        public DbSet<JobApplication> Applications { get; set; }

        /// <summary>
        /// Configures the SQLite database connection.
        /// Database file is created in the application's base directory as 'student_portal.db'.
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "student_portal.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        /// <summary>
        /// Seeds initial data into the database.
        /// Creates a default Admin user (Username: admin, Password: password123).
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial admin user for first-time setup
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", Password = "password123", Role = "Admin" }
            );
        }
    }
}
