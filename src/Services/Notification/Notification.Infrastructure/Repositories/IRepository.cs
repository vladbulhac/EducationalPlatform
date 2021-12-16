using Notification.Domain.Building_Blocks;
using Notification.Domain.Models.Aggregates;

namespace Notification.Infrastructure.Repositories;

/// <summary>
/// Encapsulates the logic required to access data sources.
/// </summary>
public interface IRepository<T> : IDisposable where T : IAggregateRoot
{
    public Task AddAsync(T entity, CancellationToken cancellationToken = default);

    public Task<T> GetByIdAsync(string eventId, string recipientId, CancellationToken cancellationToken = default);

    public Task<ICollection<Event>> GetAsync(string recipientId, int offset = 0, int resultsCount = 50, CancellationToken cancellationToken = default);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default);
}