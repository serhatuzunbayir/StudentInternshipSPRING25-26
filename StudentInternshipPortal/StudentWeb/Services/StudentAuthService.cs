using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Shared.Data;
using Shared.Enums;
using Shared.Models;

namespace StudentWeb.Services;

public class StudentAuthService
{
    private readonly DatabaseHelper _databaseHelper;
    private readonly PasswordHasher<string> _passwordHasher = new();

    public StudentAuthService(DatabaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
    }

    public bool IsUsernameTaken(string username)
    {
        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        var commandText = "SELECT COUNT(1) FROM Users WHERE LOWER(Username) = LOWER(@Username);";
        using var command = new SqliteCommand(commandText, connection);
        command.Parameters.AddWithValue("@Username", username.Trim());

        var count = Convert.ToInt32(command.ExecuteScalar());
        return count > 0;
    }

    public bool RegisterStudent(string username, string password)
    {
        if (IsUsernameTaken(username)) return false;

        string hashedPassword = _passwordHasher.HashPassword(username, password);

        using var connection = _databaseHelper.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction();

        try
        {
            // 1. Insert into Users table
            var insertUserSql = @"
                INSERT INTO Users (Username, PasswordHash, Role, CreatedAt) 
                VALUES (@Username, @PasswordHash, @Role, @CreatedAt);
                SELECT last_insert_rowid();";

            long newUserId;
            using (var userCmd = new SqliteCommand(insertUserSql, connection, transaction))
            {
                userCmd.Parameters.AddWithValue("@Username", username.Trim());
                userCmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                userCmd.Parameters.AddWithValue("@Role", (int)UserRole.Student);
                userCmd.Parameters.AddWithValue("@CreatedAt", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                newUserId = (long)userCmd.ExecuteScalar()!;
            }

            // 2. Initialize a default StudentProfile for the user
            var insertProfileSql = @"
                INSERT INTO StudentProfiles (UserId, FullName, Skills, Education, Experience, Phone, AboutMe)
                VALUES (@UserId, @FullName, '', '', '', '', '');";

            using (var profileCmd = new SqliteCommand(insertProfileSql, connection, transaction))
            {
                profileCmd.Parameters.AddWithValue("@UserId", newUserId);
                profileCmd.Parameters.AddWithValue("@FullName", username.Trim()); // Default to username initially
                profileCmd.ExecuteNonQuery();
            }

            transaction.Commit();
            return true;
        }
        catch
        {
            transaction.Rollback();
            return false;
        }
    }

    public User? ValidateStudent(string username, string password)
    {
        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        var selectUserSql = "SELECT Id, Username, PasswordHash, Role FROM Users WHERE LOWER(Username) = LOWER(@Username);";
        using var command = new SqliteCommand(selectUserSql, connection);
        command.Parameters.AddWithValue("@Username", username.Trim());

        using var reader = command.ExecuteReader();
        if (!reader.Read()) return null;

        var user = new User
        {
            Id = reader.GetInt32(0),
            Username = reader.GetString(1),
            PasswordHash = reader.GetString(2),
            Role = (UserRole)reader.GetInt32(3)
        };

        // Guard: Make sure they are a student role attempting to access the student site
        if (user.Role != UserRole.Student) return null;

        var verificationResult = _passwordHasher.VerifyHashedPassword(user.Username, user.PasswordHash, password);
        return verificationResult == PasswordVerificationResult.Success ? user : null;
    }
}