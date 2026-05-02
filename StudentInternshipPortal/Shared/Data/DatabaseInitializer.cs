using StudentInternshipJobPortal.Shared.Constants;
using StudentInternshipJobPortal.Shared.Models;

namespace StudentInternshipJobPortal.Shared.Data;

public static class DatabaseInitializer
{
    public static void Initialize()
    {
        using var db = new AppDbContext();
        db.Database.EnsureCreated();

        bool hasAdmin = db.Users.Any(x => x.Role == RoleNames.Admin);
        if (hasAdmin)
        {
            return;
        }

        db.Users.Add(new User
        {
            NameSurname = "System Admin",
            Username = "admin",
            Password = "password123",
            Role = RoleNames.Admin
        });
        db.SaveChanges();
    }
}
