using Microsoft.Data.Sqlite;
using Shared.Data;
using Shared.Models;

namespace StudentWeb.Services;

public class ProfileService
{
    private readonly DatabaseHelper _databaseHelper;

    public ProfileService(DatabaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
    }

    public StudentProfile? GetProfileByUserId(int userId)
    {
        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        var sql = @"SELECT Id, UserId, FullName, Skills, Education, Experience, Phone, AboutMe
                    FROM StudentProfiles
                    WHERE UserId = @UserId";

        using var cmd = new SqliteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@UserId", userId);

        using var reader = cmd.ExecuteReader();

        if (!reader.Read()) return null;

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

    public void UpsertProfile(StudentProfile profile)
    {
        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

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