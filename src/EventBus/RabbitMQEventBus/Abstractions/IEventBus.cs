namespace RabbitMQEventBus.Abstractions;

public interface IEventBus : IEventBusPublisher, IEventBusSubscriber, IDisposable
{ }