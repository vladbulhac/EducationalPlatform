using Microsoft.EntityFrameworkCore;
using Notification.Domain.Models.Aggregates;
using Notification.Infrastructure.Repositories.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Notification.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly NotificationContext context;

        public NotificationRepository(NotificationContext context)
            => this.context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task AddEventAsync(Event eventEntity, CancellationToken cancellationToken = default)
        {
            await context.AddAsync(eventEntity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> RecipientSawTheEventAsync(Guid eventId, Guid recipientId, CancellationToken cancellationToken = default)
        {
            var eventEntity = await context.Events.Include(e => e.Recipients)
                                                  .SingleOrDefaultAsync(e => e.Id == eventId, cancellationToken);
            if (eventEntity == default) return false;

            eventEntity.RecipientSawEventNotification(recipientId);
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<ICollection<GetEventDetails>> GetUnseenEventsForRecipientAsync(Guid recipientID, CancellationToken cancellationToken = default)
        {
            return await context.Recipients.Include(r => r.Event)
                                           .Where(r => r.Id == recipientID && !r.Seen)
                                           .Select(e => new GetEventDetails()
                                           {
                                               Message = e.Event.Message,
                                               TimeIssued = e.Event.TriggerDetails.TimeIssued,
                                               Url = e.Event.Url
                                           })
                                           .ToListAsync(cancellationToken);
        }
    }
}