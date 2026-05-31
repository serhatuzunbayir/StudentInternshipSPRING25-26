# StudentInternshipSPRING25-26
The system has two parts:

- `DesktopAdmin`: admin panel for managing jobs and applications
- `StudentWeb`: web side for students

## How It Works

Students can register and log in to the system, create their profiles, add skills and experience information, and explore available internship or job opportunities.

Administrators use the desktop application to manage job postings, review applications, update application statuses, and monitor overall system activity through the dashboard.

## Technologies Used

- C#
- .NET 10
- WinForms for the admin panel
- ASP.NET Core MVC for the student web app
- SQLite for the database
- LINQ for filtering in the admin dashboard
- Cookie Authentication for student login

## Packages / Dependencies

- `Microsoft.Data.Sqlite` `10.0.7`
- .NET SDK version in `global.json`: `10.0.0`

There is no Entity Framework in this project. Database operations are written manually with SQLite commands.

## Project Structure

- `DesktopAdmin/`: WinForms admin application
- `StudentWeb/`: ASP.NET Core MVC student application
- `Shared/`: shared models, enums, database helper, matching logic, notification logic
- `Database/`: shared SQLite database file

## Database

The project uses a shared SQLite database located at:

- `Database/student_portal.db`

The database path is resolved using a relative path, making the project portable across different machines.

Important note:

- The database tables and default admin user are created when `DesktopAdmin` starts.
- Because of that, it is best to run `DesktopAdmin` first at least one time.

## Default Admin Account

- Username: `admin`
- Password: `admin123`

## Features

### User Registration and Authentication
Students can register and log in through the web application. Administrators can access the system through the desktop application.

### Student Profile Management
Students can manage personal information, skills, education, and experience details.

### Resume Builder
Provides resume creation and management functionality for student profiles.

### Job Posting Management
Administrators can create, update, delete, and manage job postings.

### Job Search and Filtering
Job and application data can be filtered using LINQ-based operations.

### Job Matching System
The system calculates compatibility between student skills and job requirements.

### Application Submission and Tracking
Students can apply for positions and track application status.

### Notification System
Notifications are generated using delegates and events when important actions occur.

### Admin Dashboard and Reporting
Administrators can view application statistics, reports, and system summaries.

## How to Run

1. Install .NET 10 SDK.
2. Open the solution:

```powershell
dotnet build .\StudentInternshipPortal.sln
```

3. Run the admin project first to create the database and seed the default admin:

```powershell
dotnet run --project .\DesktopAdmin\DesktopAdmin.csproj
```

4. Run the student web project:

```powershell
dotnet run --project .\StudentWeb\StudentWeb.csproj
```

5. Open the local URL shown in the terminal for `StudentWeb`.


