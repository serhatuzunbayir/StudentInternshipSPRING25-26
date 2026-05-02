using Shared.Data;
using Shared.Services;

namespace StudentWeb.Services;

public class JobBrowseService
{
    private readonly DatabaseHelper _databaseHelper = new();
    private readonly MatchingService _matchingService = new();

    public string Description => $"Placeholder job browse service using {_databaseHelper.GetType().Name} and {_matchingService.GetType().Name}.";
}
