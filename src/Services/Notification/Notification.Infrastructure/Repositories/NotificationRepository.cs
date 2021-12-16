using Microsoft.EntityFrameworkCore;
using Notification.Domain.Models.Aggregates;

namespace Notification.Infrastructure.Repositories;

/// <summary>
/// <inheritdoc cref="INotificationRepository"/>
/// </summary>
public class NotificationRepository : INotificationRepository
{
    private bool disposed;

    private readonly NotificationContext context;

    public NotificationRepository(NotificationContext context)
            => this.context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task AddAsync(Event eventEntity, CancellationToken cancellationToken = default)
            => await context.AddAsync(eventEntity, cancellationToken);

    public async Task<Event> GetByIdAsync(string eventId, string recipientId, CancellationToken cancellationToken = default)
            => await context.Events.Include(e => e.Recipients.Where(r => r.Id == recipientId))
                                   .SingleOrDefaultAsync(e => e.Id == eventId &&
                                                              e.Recipients.Select(r => r.Id).Contains(recipientId),
                                                         cancellationToken);

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
            => await context.SaveChangesAsync(cancellationToken);

    public async Task<ICollection<Event>> GetAsync(string recipientId, int offset = 0, int resultsCount = 50, CancellationToken cancellationToken = default)
            => await context.Events.Include(e => e.Recipients.Where(r => r.Id == recipientId))
                                   .Where(e => e.Recipients.Select(r => r.Id).Contains(recipientId))
                                   .OrderBy(r => r.TriggerDetails.TimeIssued)
                                   .Skip(offset)
                                   .Take(resultsCount)
                                   .ToListAsync(cancellationToken);

    public async Task<ICollection<Event>> GetEventsFromCollectionAsync(ICollection<string> eventsIds, string recipientId, CancellationToken cancellationToken = default)
            => await context.Events.Include(e => e.Recipients.Where(r => r.Id == recipientId))
                                   .Where(e => eventsIds.Contains(e.Id) && e.Recipients.Select(r => r.Id).Contains(recipientId))
                                   .OrderBy(e => e.TriggerDetails.TimeIssued)
                                   .ToListAsync(cancellationToken);

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }

            disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}