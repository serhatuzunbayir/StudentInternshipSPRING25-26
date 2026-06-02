using DesktopAdmin.Forms;
using Shared.Data;
using System.Text.Json;

namespace DesktopAdmin;

// The entry point class of the Windows Forms application.
internal static class Program
{
    // The main entry point for the application.
    [STAThread]
    private static void Main()
    {
        // Initializes WinForms configuration details (like default font, dpi scaling, etc.)
        ApplicationConfiguration.Initialize();

        // Instantiate database helper with loaded path, initialize tables, and run the login form
        var databaseHelper = new DatabaseHelper(databasePath: LoadDatabasePath());
        DatabaseInitializer.Initialize(databaseHelper);

        // Run the main application message loop with the login form
        Application.Run(new AdminLoginForm(databaseHelper));
    }

    // Helper method to read the custom database file location from appsettings.json
    private static string? LoadDatabasePath()
    {
        var settingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        if (!File.Exists(settingsPath))
        {
            return null;
        }

        // Open and parse the appsettings.json configuration file
        using var stream = File.OpenRead(settingsPath);
        using var document = JsonDocument.Parse(stream);

        // Try to read "Database": { "FileName": "some_value" }
        if (document.RootElement.TryGetProperty("Database", out var databaseSection)
            && databaseSection.TryGetProperty("FileName", out var fileNameElement))
        {
            return fileNameElement.GetString();
        }

        return null;
    }
}

