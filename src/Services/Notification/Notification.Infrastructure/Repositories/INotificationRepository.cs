﻿using Notification.Domain.Models.Aggregates;
using Notification.Infrastructure.Repositories.Results;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Notification.Infrastructure.Repositories
{
    public interface INotificationRepository : IRepository<Event>
    {
        public Task AddEventAsync(Event entity, CancellationToken cancellationToken = default);

        public Task<ICollection<GetEventDetails>> GetUnseenEventsForRecipientAsync(Guid recipientID, CancellationToken cancellationToken = default);

        public Task<bool> RecipientSawTheEventAsync(Guid eventId, Guid recipientID, CancellationToken cancellationToken = default);
    }
}