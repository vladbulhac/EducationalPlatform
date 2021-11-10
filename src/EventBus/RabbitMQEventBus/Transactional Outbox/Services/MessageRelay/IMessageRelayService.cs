using RabbitMQEventBus.IntegrationEvents;

namespace RabbitMQEventBus.Transactional_Outbox.Services.MessageRelay;

/// <summary>
/// A service that publishes the <see cref="IntegrationEvent">events</see> from a database to a message broker.
/// </summary>
public interface IMessageRelayService
{
    /// <summary>
    /// Gets all <see cref="IntegrationEvent">events</see> with the same transaction id from the database
    /// and tries to publish them to the event bus.
    /// </summary>
    public Task OnTransactionFinished(Guid transactionId);
}