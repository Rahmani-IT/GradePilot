using GradePilot.Domain.Common;
using GradePilot.Domain.Entities;
using GradePilot.Domain.Grading;

namespace GradePilot.Domain.Services;

public static class GpaCalculator
{
    private readonly record struct WeightedPoints(decimal GradePoints, decimal Credits);

    public static Result<decimal> Calculate(IEnumerable<GradedCourse> courses)
    {
        ArgumentNullException.ThrowIfNull(courses);

        var completedCourses = courses
            .Where(course => course.IsCompleted)
            .ToList();

        if (completedCourses.Count == 0)
            return Result.Ok(0m);

        return completedCourses
            .Select(ToWeightedPoints)
            .Aggregate(
                Result.Ok(new WeightedPoints(0m, 0m)),
                (accumulated, next) => accumulated.Bind(total =>
                    next.Map(points => new WeightedPoints(
                        total.GradePoints + points.GradePoints,
                        total.Credits + points.Credits))))
            .Map(total => Math.Round(
                total.GradePoints / total.Credits,
                2,
                MidpointRounding.AwayFromZero));
    }

    public static Result<decimal> CalculateSemesterGpa(Semester semester)
    {
        ArgumentNullException.ThrowIfNull(semester);
        return Calculate(semester.Courses);
    }

    public static Result<decimal> CalculateCgpa(IEnumerable<Semester> semesters)
    {
        ArgumentNullException.ThrowIfNull(semesters);

        var allCourses = semesters
            .SelectMany(s => s.Courses)
            .ToList();

        return Calculate(allCourses);
    }

    private static Result<WeightedPoints> ToWeightedPoints(GradedCourse course)
    {
        if (string.IsNullOrWhiteSpace(course.CourseName))
            return Result.Fail<WeightedPoints>("Course name is required.");

        if (course.Credits <= 0)
            return Result.Fail<WeightedPoints>("Credits must be greater than zero.");

        if (course.Percentage is not decimal percentage)
            return Result.Fail<WeightedPoints>("A completed course must have a percentage.");

        return AituGradingScale.GetGrade(percentage)
            .Map(grade => new WeightedPoints(
                grade.GradePoint * course.Credits,
                course.Credits));
    }
}
