using StudentInternshipJobPortal.DesktopAdmin.Forms;
using StudentInternshipJobPortal.Shared.Data;

namespace StudentInternshipJobPortal.DesktopAdmin;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        DatabaseInitializer.Initialize();
        Application.Run(new LoginForm());
    }
}
