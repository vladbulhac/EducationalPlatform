using RabbitMQEventBus.IntegrationEvents;
using RabbitMQEventBus.Transactional_Outbox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQEventBus.Transactional_Outbox.Services.Outbox_Services
{
    /// <summary>
    /// Exposes methods for setting the publishing status of <see cref="IntegrationEvent">events</see>.
    /// </summary>
    public interface IOutboxEventPublishingStatus
    {
        /// <summary>
        /// Marks the publishing status of the event with the given id, as Successful.
        /// </summary>
        public Task EventHasBeenPublished(string eventId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Marks the publishing status of the event with the given id, as Failed.
        /// </summary>
        public Task PublishingFailed(string eventId, CancellationToken cancellationToken = default);
    }
}