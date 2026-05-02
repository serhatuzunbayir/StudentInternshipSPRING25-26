using Microsoft.Data.Sqlite;

namespace Shared.Data;

public class DatabaseHelper
{
    private readonly string _connectionString;

    public DatabaseHelper(string? connectionString = null)
    {
        _connectionString = connectionString ?? $"Data Source={ResolveDatabasePath()}";
    }

    public SqliteConnection CreateConnection()
    {
        return new SqliteConnection(_connectionString);
    }

    public static string ResolveDatabasePath()
    {
        var current = new DirectoryInfo(AppContext.BaseDirectory);

        while (current is not null)
        {
            var candidateDirectory = Path.Combine(current.FullName, "Database");
            if (Directory.Exists(candidateDirectory))
            {
                return Path.Combine(candidateDirectory, "student_portal.db");
            }

            current = current.Parent;
        }

        var fallbackDirectory = Path.Combine(AppContext.BaseDirectory, "Database");
        Directory.CreateDirectory(fallbackDirectory);
        return Path.Combine(fallbackDirectory, "student_portal.db");
    }
}
