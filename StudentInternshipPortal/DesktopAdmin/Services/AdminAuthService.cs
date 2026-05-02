using Microsoft.Data.Sqlite;
using Shared.Data;
using Shared.Enums;
using Shared.Models;
using Shared.Services;

namespace DesktopAdmin.Services;

public class AdminAuthService
{
    private readonly DatabaseHelper _databaseHelper;

    public AdminAuthService(DatabaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
    }

    public User? Authenticate(string username, string password)
    {
        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT Id, Username, PasswordHash, Role, CreatedAt
            FROM Users
            WHERE Username = $username AND Role = $role;
            """;
        command.Parameters.AddWithValue("$username", username);
        command.Parameters.AddWithValue("$role", (int)UserRole.Admin);

        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            return null;
        }

        var user = new User
        {
            Id = reader.GetInt32(0),
            Username = reader.GetString(1),
            PasswordHash = reader.GetString(2),
            Role = (UserRole)reader.GetInt32(3),
            CreatedAt = DateTime.Parse(reader.GetString(4))
        };

        return PasswordHasher.Verify(password, user.PasswordHash) ? user : null;
    }
}
