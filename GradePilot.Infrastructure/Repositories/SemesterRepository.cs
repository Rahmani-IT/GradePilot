using GradePilot.Application.Interfaces;
using GradePilot.Domain.Entities;
using GradePilot.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GradePilot.Infrastructure.Repositories;

public class SemesterRepository : ISemesterRepository
{
    private readonly GradePilotDbContext _context;

    public SemesterRepository(GradePilotDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Semester semester,
        CancellationToken cancellationToken = default)
    {
        await _context.Semesters.AddAsync(semester, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Semester>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Semesters
            .Include(s => s.Courses)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Semester?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Semesters
            .Include(s => s.Courses)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var semester = await _context.Semesters
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        if (semester is null)
            return;

        _context.Semesters.Remove(semester);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task UpdateAsync(
    Semester semester,
    CancellationToken cancellationToken = default)
{
    _context.Semesters.Update(semester);

    await _context.SaveChangesAsync(cancellationToken);
}
}