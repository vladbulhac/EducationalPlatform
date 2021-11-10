using RabbitMQEventBus.IntegrationEvents;

namespace RabbitMQEventBus.Subscription;

/// <summary>
/// Keeps track of which <see cref="IIntegrationEventHandler{TIntegrationEvent}"/> handles which <see cref="IntegrationEvent"/>
/// </summary>
public interface ISubscriptionManager
{
    public void ClearResources();

    public void SaveSubscription(Type eventType, Type handler);

    public Subscription GetSubscriptionDetailsOfEvent(string eventName);
}