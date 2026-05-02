using Shared.Data;

namespace StudentWeb.Services;

public class StudentAuthService
{
    private readonly DatabaseHelper _databaseHelper = new();

    public string Description => $"Placeholder auth service using {_databaseHelper.GetType().Name}.";
}
