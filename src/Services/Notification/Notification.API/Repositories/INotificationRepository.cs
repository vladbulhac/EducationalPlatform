using Notification.API.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Notification.API.Repositories
{
    public interface INotificationRepository
    {
        public Task CreateEvent(Event entity, CancellationToken cancellationToken = default);

        public Task<ICollection<PublicEvent>> GetUnseenEvents(Guid recipientID, CancellationToken cancellationToken = default);

        public Task<bool> RecipientSawTheEvent(Guid recipientID, CancellationToken cancellationToken = default);
    }
}