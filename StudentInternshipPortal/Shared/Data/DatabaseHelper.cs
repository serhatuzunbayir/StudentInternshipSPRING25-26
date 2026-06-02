using Microsoft.Data.Sqlite;

namespace Shared.Data;

// This helper class makes it easy to open SQLite database connections and locate the DB file.
public class DatabaseHelper
{
    // The relative folder and file name where the SQLite database is stored.
    private const string DefaultRelativeDatabasePath = "Database/student_portal.db";
    private readonly string _connectionString;

    // Constructor that sets up the database connection string.
    // If no custom string or path is given, it resolves the default path automatically.
    public DatabaseHelper(string? connectionString = null, string? databasePath = null)
    {
        _connectionString = connectionString ?? $"Data Source={ResolveDatabasePath(databasePath)}";
    }

    // Helper method to create a new SqliteConnection object.
    // The caller needs to open and dispose this connection themselves.
    public SqliteConnection CreateConnection()
    {
        return new SqliteConnection(_connectionString);
    }

    // Finds the full absolute file system path of the database file.
    // It climbs up the folder directory tree to find where the Database folder actually lives.
    public static string ResolveDatabasePath(string? configuredPath = null)
    {
        var relativePath = string.IsNullOrWhiteSpace(configuredPath)
            ? DefaultRelativeDatabasePath
            : configuredPath;

        // If the path is already rooted (like E:\something\file.db), we just create the directory and return it.
        if (Path.IsPathRooted(relativePath))
        {
            var rootedDirectory = Path.GetDirectoryName(relativePath);
            if (!string.IsNullOrWhiteSpace(rootedDirectory))
            {
                Directory.CreateDirectory(rootedDirectory);
            }

            return relativePath;
        }

        // Search parent folders up the directory tree to find our database directory.
        var current = new DirectoryInfo(AppContext.BaseDirectory);

        while (current is not null)
        {
            var candidatePath = Path.GetFullPath(Path.Combine(current.FullName, relativePath));
            var candidateDirectory = Path.GetDirectoryName(candidatePath);

            // If the database file exists, or the folder exists, we use it.
            if (File.Exists(candidatePath) || (!string.IsNullOrWhiteSpace(candidateDirectory) && Directory.Exists(candidateDirectory)))
            {
                return candidatePath;
            }

            // Climb up to the parent directory.
            current = current.Parent;
        }

        // Fallback: If we couldn't find it anywhere, create it directly in the app execution directory.
        var fallbackPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, relativePath));
        var fallbackDirectory = Path.GetDirectoryName(fallbackPath);
        if (!string.IsNullOrWhiteSpace(fallbackDirectory))
        {
            Directory.CreateDirectory(fallbackDirectory);
        }

        return fallbackPath;
    }
}

