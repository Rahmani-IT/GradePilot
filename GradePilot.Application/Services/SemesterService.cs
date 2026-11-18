using GradePilot.Application.Interfaces;
using GradePilot.Domain.Common;
using GradePilot.Domain.Entities;

namespace GradePilot.Application.Services;

public class SemesterService
{
    private readonly ISemesterRepository _repository;

    public SemesterService(ISemesterRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Unit>> CreateSemesterAsync(
        string name,
        int academicYear,
        int semesterNumber,
        CancellationToken cancellationToken = default)
    {
        var creation = Semester.Create(name, academicYear, semesterNumber);

        return await creation.Match<Task<Result<Unit>>>(
            async semester =>
            {
                await _repository.AddAsync(semester, cancellationToken);
                return Result.Ok();
            },
            error => Task.FromResult(Result.Fail<Unit>(error)));
    }

    public async Task<IReadOnlyList<Semester>> GetAllSemestersAsync(
        CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }

    public async Task<Semester?> GetSemesterByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _repository.GetByIdAsync(id, cancellationToken);
    }

    public async Task DeleteSemesterAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        await _repository.DeleteAsync(id, cancellationToken);
    }
}
