using Microsoft.Data.Sqlite;

namespace Shared.Data;

public class DatabaseHelper
{
    private const string DefaultRelativeDatabasePath = "Database/student_portal.db";
    private readonly string _connectionString;

    public DatabaseHelper(string? connectionString = null, string? databasePath = null)
    {
        _connectionString = connectionString ?? $"Data Source={ResolveDatabasePath(databasePath)}";
    }

    public SqliteConnection CreateConnection()
    {
        return new SqliteConnection(_connectionString);
    }

    public static string ResolveDatabasePath(string? configuredPath = null)
    {
        var relativePath = string.IsNullOrWhiteSpace(configuredPath)
            ? DefaultRelativeDatabasePath
            : configuredPath;

        if (Path.IsPathRooted(relativePath))
        {
            var rootedDirectory = Path.GetDirectoryName(relativePath);
            if (!string.IsNullOrWhiteSpace(rootedDirectory))
            {
                Directory.CreateDirectory(rootedDirectory);
            }

            return relativePath;
        }

        var current = new DirectoryInfo(AppContext.BaseDirectory);

        while (current is not null)
        {
            var candidatePath = Path.GetFullPath(Path.Combine(current.FullName, relativePath));
            var candidateDirectory = Path.GetDirectoryName(candidatePath);

            if (File.Exists(candidatePath) || (!string.IsNullOrWhiteSpace(candidateDirectory) && Directory.Exists(candidateDirectory)))
            {
                return candidatePath;
            }

            current = current.Parent;
        }

        var fallbackPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, relativePath));
        var fallbackDirectory = Path.GetDirectoryName(fallbackPath);
        if (!string.IsNullOrWhiteSpace(fallbackDirectory))
        {
            Directory.CreateDirectory(fallbackDirectory);
        }

        return fallbackPath;
    }
}
