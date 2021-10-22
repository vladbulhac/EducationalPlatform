using RabbitMQEventBus.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RabbitMQEventBus.Transactional_Outbox.Models
{
    /// <summary>
    /// Contains <see cref="IntegrationEvent"/> data along with the transaction id and the publish status.
    /// </summary>
    public class Outbox
    {
        public string EventId { get; init; }
        public Guid TransactionId { get; init; }
        public string EventBody { get; set; }
        public string EventName { get; set; }
        public DateTime CreatedDate { get; init; }
        public PublishStatus PublishStatus { get; set; }

        public Outbox()
        {
        }

        public Outbox(IntegrationEvent @event, Guid transactionId)
        {
            TransactionId = transactionId;
            EventId = @event.EventId;
            EventBody = JsonSerializer.Serialize(@event, @event.GetType(), new JsonSerializerOptions { WriteIndented = true });
            EventName = @event.GetType().Name;

            CreatedDate = @event.TimeIssued;
            PublishStatus = PublishStatus.Pending;
        }

        /// <summary>
        /// Get the deserialized <see cref="IntegrationEvent"/> based on the given type.
        /// </summary>
        public IntegrationEvent RestoreEvent(Type eventType) => JsonSerializer.Deserialize(EventBody, eventType, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) as IntegrationEvent;

        public void EventHasBeenPublished() => PublishStatus = PublishStatus.Published;

        public void EventCouldNotBePublished() => PublishStatus = PublishStatus.Failed;
    }

    public enum PublishStatus
    {
        Pending = 0,
        Published = 1,
        Failed = 2
    }
}