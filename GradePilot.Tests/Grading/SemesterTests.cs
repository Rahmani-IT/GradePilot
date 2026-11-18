using GradePilot.Domain.Common;
using GradePilot.Domain.Entities;
using GradePilot.Domain.Services;

namespace GradePilot.Tests.Grading;

public class SemesterTests
{
    [Fact]
    public void AddCourse_AddsCourseSuccessfully()
    {
        var semester = new Semester("Fall 2026", 2026, 1);

        var course = new GradedCourse("Mathematics", 5, 96, 96, 96);

        var result = semester.AddCourse(course);

        Assert.True(result.IsSuccess);
        Assert.Single(semester.Courses);
    }

    [Fact]
    public void AddCourse_RejectsDuplicateCourse()
    {
        var semester = new Semester("Fall 2026", 2026, 1);

        semester.AddCourse(
            new GradedCourse("Mathematics", 5, 96, 96, 96));

        var result = semester.AddCourse(
            new GradedCourse("mathematics", 5, 90, 90, 90));

        Assert.IsType<Result<Unit>.Failure>(result);
        Assert.Single(semester.Courses);
    }

    [Fact]
    public void CalculateSemesterGpa_ReturnsCorrectResult()
    {
        var semester = new Semester("Fall 2026", 2026, 1);

        semester.AddCourse(
            new GradedCourse("Mathematics", 5, 96, 96, 96));

        semester.AddCourse(
            new GradedCourse("Programming", 4, 87, 87, 87));

        var result = GpaCalculator.CalculateSemesterGpa(semester);

        Assert.Equal(3.70m, Assert.IsType<Result<decimal>.Success>(result).Value);
    }

    [Fact]
    public void CalculateCgpa_ReturnsCorrectWeightedResult()
    {
        var semester1 = new Semester("Fall 2026", 2026, 1);

        semester1.AddCourse(
            new GradedCourse("Mathematics", 5, 96, 96, 96));

        var semester2 = new Semester("Spring 2027", 2027, 2);

        semester2.AddCourse(
            new GradedCourse("Programming", 5, 85, 85, 85));

        var result = GpaCalculator.CalculateCgpa(
            new[] { semester1, semester2 });

        Assert.Equal(3.67m, Assert.IsType<Result<decimal>.Success>(result).Value);
    }

    [Fact]
    public void RemoveCourse_RemovesCourseSuccessfully()
    {
        var semester = new Semester("Fall 2026", 2026, 1);

        semester.AddCourse(
            new GradedCourse("Mathematics", 5, 96, 96, 96));

        var result = semester.RemoveCourse("Mathematics");

        Assert.True(result.IsSuccess);
        Assert.Empty(semester.Courses);
    }

    [Fact]
    public void RemoveCourse_ReturnsFailure_WhenCourseDoesNotExist()
    {
        var semester = new Semester("Fall 2026", 2026, 1);

        var result = semester.RemoveCourse("Physics");

        Assert.IsType<Result<Unit>.Failure>(result);
    }

    [Fact]
    public void Create_RejectsEmptyName()
    {
        var result = Semester.Create("   ", 2026, 1);

        Assert.IsType<Result<Semester>.Failure>(result);
    }
}
