using GradePilot.Domain.Entities;
using GradePilot.Domain.Grading;
using GradePilot.Domain.Services;

namespace GradePilot.Tests.Grading;

public class GpaCalculatorTests
{
    [Theory]
    [InlineData(100, "A")]
    [InlineData(95, "A")]
    [InlineData(94, "A-")]
    [InlineData(90, "A-")]
    [InlineData(89, "B+")]
    [InlineData(85, "B+")]
    [InlineData(84, "B")]
    [InlineData(80, "B")]
    [InlineData(79, "B-")]
    [InlineData(75, "B-")]
    [InlineData(74, "C+")]
    [InlineData(70, "C+")]
    [InlineData(69, "C")]
    [InlineData(65, "C")]
    [InlineData(64, "C-")]
    [InlineData(60, "C-")]
    [InlineData(59, "D+")]
    [InlineData(55, "D+")]
    [InlineData(54, "D")]
    [InlineData(50, "D")]
    [InlineData(49, "FX")]
    [InlineData(30, "FX")]
    [InlineData(29, "F")]
    [InlineData(0, "F")]
    public void GetGrade_ReturnsCorrectGrade(
        int percentage,
        string expectedGrade)
    {
        var result = AituGradingScale.GetGrade(percentage);

        Assert.Equal(expectedGrade, result.LetterGrade);
    }

    [Fact]
    public void Calculate_ReturnsCorrectWeightedGpa()
    {
        var courses = new List<GradedCourse>
        {
            new("Mathematics", 5, 96, 96, 96),
            new("Programming", 4, 87, 87, 87),
            new("Physics", 3, 82, 82, 82)
        };

        var result = GpaCalculator.Calculate(courses);

        Assert.Equal(3.53m, result);
    }

    [Fact]
    public void Calculate_RejectsZeroCredits()
    {
        var courses = new List<GradedCourse>
        {
            new("Mathematics", 0, 96, 96, 96)
        };

        Assert.Throws<ArgumentException>(
            () => GpaCalculator.Calculate(courses));
    }

    [Fact]
    public void GetGrade_RejectsInvalidPercentage()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => AituGradingScale.GetGrade(105));
    }
}