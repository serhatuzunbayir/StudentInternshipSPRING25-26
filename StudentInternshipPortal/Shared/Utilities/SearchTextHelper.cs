using System.Globalization;
using System.Text;

namespace Shared.Utilities;

public static class SearchTextHelper
{
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

        return Normalize(source).Contains(Normalize(value), StringComparison.Ordinal);
    }

    private static string Normalize(string value)
    {
        var normalized = value
            .Trim()
            .Replace('I', 'i')
            .Replace('İ', 'i')
            .Replace('ı', 'i')
            .ToLowerInvariant()
            .Normalize(NormalizationForm.FormD);
        var builder = new StringBuilder(normalized.Length);

        foreach (var character in normalized)
        {
            var category = CharUnicodeInfo.GetUnicodeCategory(character);
            if (category != UnicodeCategory.NonSpacingMark)
            {
                builder.Append(character);
            }
        }

        return builder
            .ToString()
            .Normalize(NormalizationForm.FormC);
    }
}
