using Notification.Domain.Models.Aggregates;

namespace Notification.Infrastructure.Repositories;

/// <summary>
/// <inheritdoc cref="IRepository{T}"/>
/// </summary>
public interface INotificationRepository : IRepository<Event>
{
    public Task<ICollection<Event>> GetEventsFromCollection(ICollection<string> eventsIds, string recipientId, CancellationToken cancellationToken = default);
}