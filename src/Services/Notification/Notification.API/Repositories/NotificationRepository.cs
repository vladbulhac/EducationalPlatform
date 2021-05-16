using Microsoft.EntityFrameworkCore;
using Notification.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Notification.API.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly NotificationContext context;

        public NotificationRepository(NotificationContext context)
            => this.context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<bool> RecipientSawTheEvent(Guid recipientID, CancellationToken cancellationToken)
        {
            var recipient = await context.Recipients.SingleOrDefaultAsync(r => r.RecipientID == recipientID, cancellationToken);
            if (recipient is null) return false;

            recipient.NotificationWasSeen();
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task CreateEvent(Event eventEntity, CancellationToken cancellationToken)
        {
            await context.AddAsync(eventEntity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<ICollection<PublicEvent>> GetUnseenEvents(Guid recipientID, CancellationToken cancellationToken)
        {
            return await context.Recipients.Include(r => r.Event)
                                           .Where(r => r.RecipientID == recipientID && !r.Seen)
                                           .Select(e => new PublicEvent()
                                           {
                                               Message = e.Event.Message,
                                               TimeIssued = e.Event.TimeIssued,
                                               Url = e.Event.Url
                                           })
                                           .ToListAsync(cancellationToken);
        }
    }
}