using RabbitMQEventBus.IntegrationEvents;

namespace Notification.Application.Integration_Events.Events;

public record NotificationIntegrationEvent : IntegrationEvent
{
    public ICollection<string> ToNotify { get; init; }
}