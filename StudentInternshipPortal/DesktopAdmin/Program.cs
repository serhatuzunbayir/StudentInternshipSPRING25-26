using DesktopAdmin.Forms;
using Shared.Data;
using System.Text.Json;

namespace DesktopAdmin;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        var databaseHelper = new DatabaseHelper(databasePath: LoadDatabasePath());
        DatabaseInitializer.Initialize(databaseHelper);

        Application.Run(new AdminLoginForm(databaseHelper));
    }

    private static string? LoadDatabasePath()
    {
        var settingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        if (!File.Exists(settingsPath))
        {
            return null;
        }

        using var stream = File.OpenRead(settingsPath);
        using var document = JsonDocument.Parse(stream);

        if (document.RootElement.TryGetProperty("Database", out var databaseSection)
            && databaseSection.TryGetProperty("FileName", out var fileNameElement))
        {
            return fileNameElement.GetString();
        }

        return null;
    }
}
