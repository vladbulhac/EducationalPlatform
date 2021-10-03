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

        public async Task AddAsync(Event eventEntity, CancellationToken cancellationToken = default) => await context.AddAsync(eventEntity, cancellationToken);

        public async Task<Event> GetByIdAsync(string eventId, CancellationToken cancellationToken = default)
                => await context.Events.Include(e => e.Recipients)
                                       .SingleOrDefaultAsync(e => e.Id == eventId, cancellationToken);

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await context.SaveChangesAsync(cancellationToken);

        public async Task<ICollection<GetEventDetails>> GetUnseenEventsForRecipientAsync(string recipientID, CancellationToken cancellationToken = default)
        {
            return await context.Recipients.Include(r => r.Event)
                                           .Where(r => r.Id == recipientID && !r.Seen)
                                           .Select(e => new GetEventDetails()
                                           {
                                               Message = e.Event.Message,
                                               TimeIssued = e.Event.TriggerDetails.TimeIssued,
                                               Url = e.Event.Uri
                                           })
                                           .ToListAsync(cancellationToken);
        }
    }
}