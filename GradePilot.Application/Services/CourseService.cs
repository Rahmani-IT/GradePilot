using GradePilot.Application.Interfaces;
using GradePilot.Domain.Entities;

namespace GradePilot.Application.Services;

public class CourseService
{
    private readonly ISemesterRepository _repository;

    public CourseService(ISemesterRepository repository)
    {
        _repository = repository;
    }

    public async Task AddCourseAsync(
        Guid semesterId,
        string courseName,
        decimal credits,
        decimal? midterm,
        decimal? endterm,
        decimal? finalExam,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(courseName))
            throw new ArgumentException("Course name is required.");

        if (credits <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(credits),
                "Credits must be greater than zero.");

        ValidateScore(midterm, nameof(midterm));
        ValidateScore(endterm, nameof(endterm));
        ValidateScore(finalExam, nameof(finalExam));

        var semester = await _repository.GetByIdAsync(
            semesterId,
            cancellationToken);

        if (semester is null)
            throw new InvalidOperationException("Semester not found.");

        var course = new GradedCourse(
            courseName,
            credits,
            midterm,
            endterm,
            finalExam);

        semester.AddCourse(course);

        await _repository.UpdateAsync(
            semester,
            cancellationToken);
    }

    private static void ValidateScore(
        decimal? score,
        string parameterName)
    {
        if (score.HasValue && (score.Value < 0 || score.Value > 100))
        {
            throw new ArgumentOutOfRangeException(
                parameterName,
                "Score must be between 0 and 100.");
        }
    }

    public async Task RemoveCourseAsync(
        Guid semesterId,
        string courseName,
        CancellationToken cancellationToken = default)
    {
        var semester = await _repository.GetByIdAsync(
            semesterId,
            cancellationToken);

        if (semester is null)
            throw new InvalidOperationException("Semester not found.");

        if (!semester.RemoveCourse(courseName))
            throw new InvalidOperationException("Course not found.");

        await _repository.UpdateAsync(
            semester,
            cancellationToken);
    }
}