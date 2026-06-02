using Microsoft.Data.Sqlite;
using Shared.Data;
using Shared.Models;

namespace StudentWeb.Services;

// This service manages student profile pages (saving and loading skills, educations, etc.) in the database.
public class ProfileService
{
    private readonly DatabaseHelper _databaseHelper;

    public ProfileService(DatabaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
    }

    // Fetches profile information for a student user ID.
    public StudentProfile? GetProfileByUserId(int userId)
    {
        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        // Parameterized SQL statement to prevent SQL Injection
        var sql = @"SELECT Id, UserId, FullName, Skills, Education, Experience, Phone, AboutMe
                    FROM StudentProfiles
                    WHERE UserId = @UserId";

        using var cmd = new SqliteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@UserId", userId);

        using var reader = cmd.ExecuteReader();

        // If no profile found, return null
        if (!reader.Read()) return null;

        // Map fields to StudentProfile model
        return new StudentProfile
        {
            Id = reader.GetInt32(0),
            UserId = reader.GetInt32(1),
            FullName = reader.GetString(2),
            Skills = reader.GetString(3),
            Education = reader.GetString(4),
            Experience = reader.GetString(5),
            Phone = reader.GetString(6),
            AboutMe = reader.GetString(7)
        };
    }

    // Saves or updates a student profile. Uses ON CONFLICT to overwrite the profile if it exists.
    public void UpsertProfile(StudentProfile profile)
    {
        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        // ON CONFLICT(UserId) DO UPDATE specifies that if the record exists, update its fields instead of throwing an error.
        var sql = @"
INSERT INTO StudentProfiles (UserId, FullName, Skills, Education, Experience, Phone, AboutMe)
VALUES (@UserId, @FullName, @Skills, @Education, @Experience, @Phone, @AboutMe)
ON CONFLICT(UserId) DO UPDATE SET
FullName = excluded.FullName,
Skills = excluded.Skills,
Education = excluded.Education,
Experience = excluded.Experience,
Phone = excluded.Phone,
AboutMe = excluded.AboutMe;
";

        using var cmd = new SqliteCommand(sql, connection);

        // Bind parameters safely
        cmd.Parameters.AddWithValue("@UserId", profile.UserId);
        cmd.Parameters.AddWithValue("@FullName", profile.FullName ?? "");
        cmd.Parameters.AddWithValue("@Skills", profile.Skills ?? "");
        cmd.Parameters.AddWithValue("@Education", profile.Education ?? "");
        cmd.Parameters.AddWithValue("@Experience", profile.Experience ?? "");
        cmd.Parameters.AddWithValue("@Phone", profile.Phone ?? "");
        cmd.Parameters.AddWithValue("@AboutMe", profile.AboutMe ?? "");

        cmd.ExecuteNonQuery();
    }
}