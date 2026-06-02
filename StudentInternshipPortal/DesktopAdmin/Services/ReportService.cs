using DesktopAdmin.ViewModels;
using Shared.Data;
using Shared.Enums;

namespace DesktopAdmin.Services;

// This service builds reports and counts database tables to show general dashboard statistics.
public class ReportService
{
    private readonly DatabaseHelper _databaseHelper;

    public ReportService(DatabaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
    }

    // Runs SELECT COUNT queries across tables to populate the dashboard summary model.
    public ReportSummaryViewModel GetSummary()
    {
        using var connection = _databaseHelper.CreateConnection();
        connection.Open();

        // Count totals using ExecuteScalar helper
        return new ReportSummaryViewModel
        {
            TotalStudents = ExecuteScalar(connection, "SELECT COUNT(1) FROM StudentProfiles;"),
            ActiveJobs = ExecuteScalar(connection, "SELECT COUNT(1) FROM Jobs WHERE IsActive = 1;"),
            TotalApplications = ExecuteScalar(connection, "SELECT COUNT(1) FROM Applications;"),
            AcceptedApplications = ExecuteScalar(connection, $"SELECT COUNT(1) FROM Applications WHERE Status = {(int)ApplicationStatus.Accepted};"),
            RejectedApplications = ExecuteScalar(connection, $"SELECT COUNT(1) FROM Applications WHERE Status = {(int)ApplicationStatus.Rejected};"),
            PendingApplications = ExecuteScalar(connection, $"SELECT COUNT(1) FROM Applications WHERE Status = {(int)ApplicationStatus.Pending};")
        };
    }

    // Helper method to execute a query that returns a single numerical value (scalar).
    private static int ExecuteScalar(Microsoft.Data.Sqlite.SqliteConnection connection, string sql)
    {
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        return Convert.ToInt32(command.ExecuteScalar());
    }
}

