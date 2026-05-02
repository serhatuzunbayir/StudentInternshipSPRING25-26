using System.Reflection;

namespace StudentInternshipJobPortal.Shared.Data;

public static class DbPathProvider
{
    public static string GetDatabasePath()
    {
        string? current = AppContext.BaseDirectory;
        DirectoryInfo? directory = new(current);

        while (directory is not null)
        {
            string solutionFile = Path.Combine(directory.FullName, "StudentInternshipJobPortal.sln");
            if (File.Exists(solutionFile))
            {
                return Path.Combine(directory.FullName, "Database", "student_portal.db");
            }

            directory = directory.Parent;
        }

        return Path.Combine(AppContext.BaseDirectory, "student_portal.db");
    }
}
