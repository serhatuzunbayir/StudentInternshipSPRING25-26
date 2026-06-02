namespace Shared.Services;

// This service computes how well a student's skills match the skills required for a job.
public class MatchingService
{
    // Calculates the percentage of required skills that a student has.
    // E.g., if a job requires 4 skills and student has 2 of them, returns 50.
    public int CalculateSkillMatchPercentage(string studentSkills, string requiredSkills)
    {
        // Parse skills from comma-separated strings into hash sets
        var studentSkillSet = ParseSkills(studentSkills);
        var requiredSkillSet = ParseSkills(requiredSkills);

        // If the job requires no skills at all, matching percentage is 0.
        if (requiredSkillSet.Count == 0)
        {
            return 0;
        }

        // Use LINQ Count to see how many required skills exist in the student's skills set.
        var matchingSkillCount = requiredSkillSet.Count(skill => studentSkillSet.Contains(skill));
        // Round to nearest integer: matching skills count / total required skills count * 100
        return (int)Math.Round(matchingSkillCount * 100.0 / requiredSkillSet.Count, MidpointRounding.AwayFromZero);
    }

    // Helper method to split, clean up, and normalize a skill list string.
    private static HashSet<string> ParseSkills(string input)
    {
        // Split string by commas, remove empty elements, trim spaces, and lowercase them.
        // We use ToHashSet for fast search lookups using .Contains().
        return input
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Where(value => !string.IsNullOrWhiteSpace(value)) // Filter out empty lines/spaces using LINQ
            .Select(value => value.ToLowerInvariant()) // Convert each skill to lowercase using LINQ
            .ToHashSet(); // Convert to hash set
    }
}

