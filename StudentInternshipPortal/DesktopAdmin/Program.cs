using DesktopAdmin.Forms;
using Shared.Data;

namespace DesktopAdmin;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        var databaseHelper = new DatabaseHelper();
        DatabaseInitializer.Initialize(databaseHelper);

        Application.Run(new AdminLoginForm(databaseHelper));
    }
}
