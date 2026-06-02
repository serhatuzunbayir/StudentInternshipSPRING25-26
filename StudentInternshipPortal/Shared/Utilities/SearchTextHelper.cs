using System.Globalization;
using System.Text;

namespace Shared.Utilities;

// Helper class to do case-insensitive search queries that ignore special Turkish accents/characters.
public static class SearchTextHelper
{
    // Checks if the source string contains the search value.
    // Handles case-insensitive matches and handles special characters like 'İ' and 'ı'.
    public static bool Contains(string source, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return true;
        }

        if (string.IsNullOrWhiteSpace(source))
        {
            return false;
        }

        // Normalize both strings and check if one contains the other.
        return Normalize(source).Contains(Normalize(value), StringComparison.Ordinal);
    }

    // Normalizes strings by converting to lower case, replacing Turkish-specific characters,
    // and removing accent marks (non-spacing marks like dots, cedillas, etc.).
    private static string Normalize(string value)
    {
        var normalized = value
            .Trim()
            .Replace('I', 'i')
            .Replace('İ', 'i')
            .Replace('ı', 'i')
            .ToLowerInvariant()
            // FormD separates letters from their accent marks (e.g. 'ç' becomes 'c' + cedilla accent mark).
            .Normalize(NormalizationForm.FormD);
        var builder = new StringBuilder(normalized.Length);

        // Loop through each character and ignore accent marks (NonSpacingMark).
        foreach (var character in normalized)
        {
            var category = CharUnicodeInfo.GetUnicodeCategory(character);
            if (category != UnicodeCategory.NonSpacingMark)
            {
                builder.Append(character);
            }
        }

        // Put letters back together in normal composition (FormC).
        return builder
            .ToString()
            .Normalize(NormalizationForm.FormC);
    }
}

