using GradePilot.Domain.Entities;
using GradePilot.Domain.Services;

namespace GradePilot.Web.Helpers;

public static class GpaFormatter
{
    public static string Format(IEnumerable<GradedCourse> courses)
    {
        var courseList = courses.ToList();

        if (!courseList.Any(course => course.IsCompleted))
            return "—";

        return GpaCalculator.Calculate(courseList).Match(
            gpa => gpa.ToString("F2"),
            _ => "—");
    }
}
