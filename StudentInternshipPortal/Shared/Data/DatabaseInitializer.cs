using Microsoft.Data.Sqlite;
using Shared.Enums;
using Shared.Services;

namespace Shared.Data;

public static class DatabaseInitializer
{
    public static void Initialize(DatabaseHelper databaseHelper)
    {
        using var connection = databaseHelper.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Username TEXT NOT NULL UNIQUE,
                PasswordHash TEXT NOT NULL,
                Role INTEGER NOT NULL,
                CreatedAt TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS StudentProfiles (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                UserId INTEGER NOT NULL UNIQUE,
                FullName TEXT NOT NULL,
                Skills TEXT NOT NULL,
                Education TEXT NOT NULL,
                Experience TEXT NOT NULL,
                Phone TEXT NOT NULL,
                AboutMe TEXT NOT NULL,
                FOREIGN KEY(UserId) REFERENCES Users(Id)
            );

            CREATE TABLE IF NOT EXISTS Jobs (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Title TEXT NOT NULL,
                Description TEXT NOT NULL,
                RequiredSkills TEXT NOT NULL,
                Location TEXT NOT NULL,
                JobType INTEGER NOT NULL,
                IsActive INTEGER NOT NULL,
                CreatedAt TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Applications (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                StudentProfileId INTEGER NOT NULL,
                JobId INTEGER NOT NULL,
                Status INTEGER NOT NULL,
                AppliedAt TEXT NOT NULL,
                UpdatedAt TEXT NOT NULL,
                FOREIGN KEY(StudentProfileId) REFERENCES StudentProfiles(Id),
                FOREIGN KEY(JobId) REFERENCES Jobs(Id)
            );

            CREATE TABLE IF NOT EXISTS Notifications (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                UserId INTEGER NOT NULL,
                Title TEXT NOT NULL,
                Message TEXT NOT NULL,
                IsRead INTEGER NOT NULL,
                CreatedAt TEXT NOT NULL,
                FOREIGN KEY(UserId) REFERENCES Users(Id)
            );
            """;
        command.ExecuteNonQuery();

        SeedAdmin(connection);
    }

    private static void SeedAdmin(SqliteConnection connection)
    {
        using var checkCommand = connection.CreateCommand();
        checkCommand.CommandText = "SELECT COUNT(1) FROM Users WHERE Role = $role;";
        checkCommand.Parameters.AddWithValue("$role", (int)UserRole.Admin);
        var adminCount = Convert.ToInt32(checkCommand.ExecuteScalar());

        if (adminCount > 0)
        {
            return;
        }

        using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText =
            """
            INSERT INTO Users (Username, PasswordHash, Role, CreatedAt)
            VALUES ($username, $passwordHash, $role, $createdAt);
            """;
        insertCommand.Parameters.AddWithValue("$username", "admin");
        insertCommand.Parameters.AddWithValue("$passwordHash", PasswordHasher.Hash("admin123"));
        insertCommand.Parameters.AddWithValue("$role", (int)UserRole.Admin);
        insertCommand.Parameters.AddWithValue("$createdAt", DateTime.UtcNow.ToString("O"));
        insertCommand.ExecuteNonQuery();
    }
}
