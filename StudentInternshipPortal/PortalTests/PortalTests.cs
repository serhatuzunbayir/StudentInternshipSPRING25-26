using Xunit;
using Shared.Services;
using Shared.Utilities;

namespace PortalTests;

public class MatchingServiceTests
{
    private readonly MatchingService _matchingService = new();

    [Fact]
    public void CalculateSkillMatchPercentage_FullMatch_Returns100()
    {
        // Arrange
        string studentSkills = "C#, SQL, ASP.NET Core";
        string requiredSkills = "C#, SQL";

        // Act
        int result = _matchingService.CalculateSkillMatchPercentage(studentSkills, requiredSkills);

        // Assert
        Assert.Equal(100, result);
    }

    [Fact]
    public void CalculateSkillMatchPercentage_NoMatch_Returns0()
    {
        // Arrange
        string studentSkills = "Java, Python";
        string requiredSkills = "C#, SQL";

        // Act
        int result = _matchingService.CalculateSkillMatchPercentage(studentSkills, requiredSkills);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void CalculateSkillMatchPercentage_PartialMatch_ReturnsCorrectPercentage()
    {
        // Arrange
        string studentSkills = "C#, Python, HTML";
        string requiredSkills = "C#, SQL, CSS, HTML"; // 2 of 4 match (50%)

        // Act
        int result = _matchingService.CalculateSkillMatchPercentage(studentSkills, requiredSkills);

        // Assert
        Assert.Equal(50, result);
    }

    [Fact]
    public void CalculateSkillMatchPercentage_CaseInsensitivity_Returns100()
    {
        // Arrange
        string studentSkills = "c#, sql";
        string requiredSkills = "C#, SQL";

        // Act
        int result = _matchingService.CalculateSkillMatchPercentage(studentSkills, requiredSkills);

        // Assert
        Assert.Equal(100, result);
    }

    [Fact]
    public void CalculateSkillMatchPercentage_EmptyRequiredSkills_Returns0()
    {
        // Arrange
        string studentSkills = "C#, SQL";
        string requiredSkills = "";

        // Act
        int result = _matchingService.CalculateSkillMatchPercentage(studentSkills, requiredSkills);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void CalculateSkillMatchPercentage_ExtraStudentSkills_Returns100()
    {
        // Arrange
        string studentSkills = "C#, SQL, Python, Java, Javascript";
        string requiredSkills = "C#";

        // Act
        int result = _matchingService.CalculateSkillMatchPercentage(studentSkills, requiredSkills);

        // Assert
        Assert.Equal(100, result);
    }

    [Fact]
    public void CalculateSkillMatchPercentage_TrimAndWhitespace_ReturnsCorrectPercentage()
    {
        // Arrange
        string studentSkills = "  C# ,   SQL  ";
        string requiredSkills = "C#,SQL";

        // Act
        int result = _matchingService.CalculateSkillMatchPercentage(studentSkills, requiredSkills);

        // Assert
        Assert.Equal(100, result);
    }
}

public class SearchTextHelperTests
{
    [Fact]
    public void Contains_CaseInsensitiveSearch_ReturnsTrue()
    {
        // Arrange
        string source = "Web Developer Position";
        string value = "DEVELOPER";

        // Act
        bool result = SearchTextHelper.Contains(source, value);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Contains_EmptyValue_ReturnsTrue()
    {
        // Arrange
        string source = "Any text";
        string value = "";

        // Act
        bool result = SearchTextHelper.Contains(source, value);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Contains_Mismatch_ReturnsFalse()
    {
        // Arrange
        string source = "C#, SQL, HTML";
        string value = "Python";

        // Act
        bool result = SearchTextHelper.Contains(source, value);

        // Assert
        Assert.False(result);
    }
}
