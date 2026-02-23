using Kumihimo.Domain.Models;

namespace Kumihimo.Application.Abstractions;

public interface IPatternRepository
{
    Task<UserPattern?> GetByIdAsync(Guid id, string userId, CancellationToken ct);
    Task<IReadOnlyList<UserPattern>> GetMineAsync(string userId, CancellationToken ct);
    Task AddAsync(UserPattern pattern, string userId, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}