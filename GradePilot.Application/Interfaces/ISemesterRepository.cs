using GradePilot.Domain.Entities;

namespace GradePilot.Application.Interfaces;

public interface ISemesterRepository
{
    Task AddAsync(Semester semester, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Semester>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Semester?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateAsync(Semester semester, CancellationToken cancellationToken = default);
}
