using GradePilot.Domain.Common;

namespace GradePilot.Domain.Grading;

public static class AituGradingScale
{
    public static readonly IReadOnlyList<GradeScaleEntry> Grades =
        Array.AsReadOnly(new[]
        {
            new GradeScaleEntry("A",  4.00m, 95, 100, "Excellent"),
            new GradeScaleEntry("A-", 3.67m, 90, 94,  "Excellent"),

            new GradeScaleEntry("B+", 3.33m, 85, 89,  "Good"),
            new GradeScaleEntry("B",  3.00m, 80, 84,  "Good"),
            new GradeScaleEntry("B-", 2.67m, 75, 79,  "Good"),

            new GradeScaleEntry("C+", 2.33m, 70, 74,  "Satisfactory"),
            new GradeScaleEntry("C",  2.00m, 65, 69,  "Satisfactory"),
            new GradeScaleEntry("C-", 1.67m, 60, 64,  "Satisfactory"),
            new GradeScaleEntry("D+", 1.33m, 55, 59,  "Satisfactory"),
            new GradeScaleEntry("D",  1.00m, 50, 54,  "Satisfactory"),

            new GradeScaleEntry("FX", 0.00m, 30, 49,  "Fail"),
            new GradeScaleEntry("F",  0.00m, 0,  29,  "Fail")
        });

    public static Result<GradeScaleEntry> GetGrade(decimal percentage)
    {
        if (percentage < 0 || percentage > 100)
            return Result.Fail<GradeScaleEntry>(
                "Percentage must be between 0 and 100.");

        var roundedPercentage = (int)Math.Round(
            percentage,
            MidpointRounding.AwayFromZero);

        var grade = Grades.FirstOrDefault(g =>
            roundedPercentage >= g.MinimumPercentage &&
            roundedPercentage <= g.MaximumPercentage);

        return grade is null
            ? Result.Fail<GradeScaleEntry>("No AITU grade matches this percentage.")
            : Result.Ok(grade);
    }
}
