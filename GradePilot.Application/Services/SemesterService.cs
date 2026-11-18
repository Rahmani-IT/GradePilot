using GradePilot.Application.Interfaces;
using GradePilot.Domain.Entities;

namespace GradePilot.Application.Services;

public class SemesterService
{
    private readonly ISemesterRepository _repository;

    public SemesterService(ISemesterRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateSemesterAsync(
        string name,
        int academicYear,
        int semesterNumber,
        CancellationToken cancellationToken = default)
    {
        var semester = new Semester(
            name,
            academicYear,
            semesterNumber
        );

        await _repository.AddAsync(semester, cancellationToken);
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