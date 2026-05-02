using StudentInternshipJobPortal.Shared.Constants;
using StudentInternshipJobPortal.Shared.Data;
using StudentInternshipJobPortal.Shared.Models;

namespace StudentInternshipJobPortal.Shared.Services;

public class AuthService
{
    public User? AuthenticateAdmin(string username, string password)
    {
        using var db = new AppDbContext();
        return db.Users.FirstOrDefault(x =>
            x.Username == username &&
            x.Password == password &&
            x.Role == RoleNames.Admin);
    }
}
