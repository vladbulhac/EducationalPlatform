namespace RabbitMQEventBus.IntegrationEvents;

public interface IIntegrationEventHandler<in TIntegrationEvent> where TIntegrationEvent : IntegrationEvent
{
    public Task HandleEvent(TIntegrationEvent @event);
}