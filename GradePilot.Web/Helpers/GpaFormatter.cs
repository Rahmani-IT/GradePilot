using GradePilot.Domain.Entities;
using GradePilot.Domain.Grading;
using GradePilot.Domain.Services;

namespace GradePilot.Web.Helpers;

public static class GpaFormatter
{
    public static string Format(IEnumerable<GradedCourse> courses)
    {
        var courseList = courses.ToList();

        if (!courseList.Any(course => course.IsCompleted))
            return "—";

        try
        {
            return GpaCalculator.Calculate(courseList).ToString("F2");
        }
        catch (ArgumentException)
        {
            return "—";
        }
    }
}