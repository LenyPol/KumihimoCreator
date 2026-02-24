using Kumihimo.Domain.Entities.UserPatterns;

namespace Kumihimo.Application.Abstractions;

public interface IPatternRepository
{
    Task<UserPattern?> GetByIdAsync(Guid id, string userId, CancellationToken ct);
    Task<IReadOnlyList<UserPattern>> GetMineAsync(string userId, int page, int pageSize, CancellationToken ct);
    Task<int> CountMineAsync(string userId, CancellationToken ct);
    Task AddAsync(UserPattern pattern, CancellationToken ct);          
    Task UpdateAsync(UserPattern pattern, CancellationToken ct);       
    Task DeleteAsync(Guid id, string userId, CancellationToken ct);   
    Task SaveChangesAsync(CancellationToken ct);
}