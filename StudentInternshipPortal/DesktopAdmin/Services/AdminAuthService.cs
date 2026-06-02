using Microsoft.Data.Sqlite;
using Shared.Data;
using Shared.Enums;
using Shared.Models;
using Shared.Services;

namespace DesktopAdmin.Services;

// This service handles verification of administrator login credentials against the database.
public class AdminAuthService
{
    private readonly DatabaseHelper _databaseHelper;

    public AdminAuthService(DatabaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
    }

    // Authenticates admin user. Returns User details if valid, otherwise null.
    public User? Authenticate(string username, string password)
    {
        // Setup SQLite connection
        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        // SQL query to search for a user matching the username and who has the Admin role (role value 1)
        command.CommandText =
            """
            SELECT Id, Username, PasswordHash, Role, CreatedAt
            FROM Users
            WHERE Username = $username AND Role = $role;
            """;
        // Parameterized bindings to protect against SQL injections
        command.Parameters.AddWithValue("$username", username);
        command.Parameters.AddWithValue("$role", (int)UserRole.Admin);

        // Run reader to extract user details
        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            return null; // No matching user found
        }

        // Map database fields to the User model
        var user = new User
        {
            Id = reader.GetInt32(0),
            Username = reader.GetString(1),
            PasswordHash = reader.GetString(2),
            Role = (UserRole)reader.GetInt32(3),
            CreatedAt = DateTime.Parse(reader.GetString(4))
        };

        // Use custom PasswordHasher utility to verify SHA256 hashed password
        return PasswordHasher.Verify(password, user.PasswordHash) ? user : null;
    }
}

