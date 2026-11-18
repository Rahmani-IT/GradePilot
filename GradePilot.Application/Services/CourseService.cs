using GradePilot.Application.Interfaces;
using GradePilot.Domain.Common;
using GradePilot.Domain.Entities;

namespace GradePilot.Application.Services;

public class CourseService
{
    private readonly ISemesterRepository _repository;

    public CourseService(ISemesterRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Unit>> AddCourseAsync(
        Guid semesterId,
        string courseName,
        decimal credits,
        decimal? midterm,
        decimal? endterm,
        decimal? finalExam,
        CancellationToken cancellationToken = default)
    {
        var validation = ValidateCourseInput(
            courseName,
            credits,
            midterm,
            endterm,
            finalExam);

        if (validation.IsFailure)
            return validation;

        var semester = await _repository.GetByIdAsync(
            semesterId,
            cancellationToken);

        if (semester is null)
            return Result.Fail<Unit>("Semester not found.");

        var course = new GradedCourse(
            courseName,
            credits,
            midterm,
            endterm,
            finalExam);

        var addResult = semester.AddCourse(course);

        if (addResult.IsFailure)
            return addResult;

        await _repository.UpdateAsync(semester, cancellationToken);

        return addResult;
    }

    public async Task<Result<Unit>> RemoveCourseAsync(
        Guid semesterId,
        string courseName,
        CancellationToken cancellationToken = default)
    {
        var semester = await _repository.GetByIdAsync(
            semesterId,
            cancellationToken);

        if (semester is null)
            return Result.Fail<Unit>("Semester not found.");

        var removeResult = semester.RemoveCourse(courseName);

        if (removeResult.IsFailure)
            return removeResult;

        await _repository.UpdateAsync(semester, cancellationToken);

        return removeResult;
    }

    private static Result<Unit> ValidateCourseInput(
        string courseName,
        decimal credits,
        decimal? midterm,
        decimal? endterm,
        decimal? finalExam) =>
        ValidateCourseName(courseName)
            .Bind(_ => ValidateCredits(credits))
            .Bind(_ => ValidateScore(midterm, "Midterm"))
            .Bind(_ => ValidateScore(endterm, "Endterm"))
            .Bind(_ => ValidateScore(finalExam, "Final exam"));

    private static Result<Unit> ValidateCourseName(string courseName) =>
        string.IsNullOrWhiteSpace(courseName)
            ? Result.Fail<Unit>("Course name is required.")
            : Result.Ok();

    private static Result<Unit> ValidateCredits(decimal credits) =>
        credits <= 0
            ? Result.Fail<Unit>("Credits must be greater than zero.")
            : Result.Ok();

    private static Result<Unit> ValidateScore(decimal? score, string label) =>
        score is { } value && (value < 0 || value > 100)
            ? Result.Fail<Unit>($"{label} score must be between 0 and 100.")
            : Result.Ok();
}
