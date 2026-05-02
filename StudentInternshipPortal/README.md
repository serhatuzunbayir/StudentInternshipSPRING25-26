# Student Internship and Job Portal

This solution is split into three projects:

- `DesktopAdmin`: WinForms admin application
- `StudentWeb`: ASP.NET Core MVC student application scaffold
- `Shared`: common models, SQLite helper, notification manager, matching logic

## Current Status

- The old single-project structure was removed.
- `DesktopAdmin` is implemented with designer-friendly forms.
- `StudentWeb` is prepared as a compile-ready scaffold and can be filled later on top of the shared layer.
- Both sides are designed to use the same SQLite database file.

## Default Admin

- Username: `admin`
- Password: `admin123`

## Database

The SQLite file is created automatically at:

- `Database/student_portal.db`

## Notes

- WinForms UI layout is kept in `.Designer.cs` files.
- Form event handling and business logic are kept in `.cs` files.
- Shared database and notification behavior live in the `Shared` project.
