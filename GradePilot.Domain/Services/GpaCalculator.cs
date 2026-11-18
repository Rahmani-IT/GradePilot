using GradePilot.Domain.Entities;
using GradePilot.Domain.Grading;

namespace GradePilot.Domain.Services;

public static class GpaCalculator
{
    public static decimal Calculate(IEnumerable<GradedCourse> courses)
    {
        ArgumentNullException.ThrowIfNull(courses);

        var courseList = courses.ToList();

        var completedCourses = courseList
            .Where(course => course.IsCompleted)
            .ToList();

        if (completedCourses.Count == 0)
            return 0m;

        decimal totalGradePoints = 0;
        decimal totalCredits = 0;

        foreach (var course in completedCourses)
        {
            if (string.IsNullOrWhiteSpace(course.CourseName))
                throw new ArgumentException("Course name is required.");

            if (course.Credits <= 0)
                throw new ArgumentException(
                    "Credits must be greater than zero.");

            var grade = AituGradingScale.GetGrade(
                course.Percentage!.Value);

            totalGradePoints += grade.GradePoint * course.Credits;
            totalCredits += course.Credits;
        }

        return Math.Round(
            totalGradePoints / totalCredits,
            2,
            MidpointRounding.AwayFromZero);
    }

    public static decimal CalculateSemesterGpa(Semester semester)
    {
        ArgumentNullException.ThrowIfNull(semester);
        return Calculate(semester.Courses);
    }

    public static decimal CalculateCgpa(IEnumerable<Semester> semesters)
    {
        ArgumentNullException.ThrowIfNull(semesters);

        var allCourses = semesters
            .SelectMany(s => s.Courses)
            .ToList();

        return Calculate(allCourses);
    }
}