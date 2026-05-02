namespace Shared.Services;

public class MatchingService
{
    public int CalculateSkillMatchPercentage(string studentSkills, string requiredSkills)
    {
        var studentSkillSet = ParseSkills(studentSkills);
        var requiredSkillSet = ParseSkills(requiredSkills);

        if (requiredSkillSet.Count == 0)
        {
            return 0;
        }

        var matchingSkillCount = requiredSkillSet.Count(skill => studentSkillSet.Contains(skill));
        return (int)Math.Round(matchingSkillCount * 100.0 / requiredSkillSet.Count, MidpointRounding.AwayFromZero);
    }

    private static HashSet<string> ParseSkills(string input)
    {
        return input
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .Select(value => value.ToLowerInvariant())
            .ToHashSet();
    }
}
