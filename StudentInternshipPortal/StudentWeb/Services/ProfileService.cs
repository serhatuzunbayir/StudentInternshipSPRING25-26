using Shared.Data;

namespace StudentWeb.Services;

public class ProfileService
{
    private readonly DatabaseHelper _databaseHelper = new();

    public string Description => $"Placeholder profile service using {_databaseHelper.GetType().Name}.";
}
