using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RabbitMQEventBus.Abstractions;
using RabbitMQEventBus.Transactional_Outbox.Infrastructure;
using RabbitMQEventBus.Transactional_Outbox.Models;
using System.Reflection;

namespace RabbitMQEventBus.Transactional_Outbox.Services.MessageRelay;

/// <summary>
/// <inheritdoc cref="IMessageRelayService"/>
/// </summary>
public class MessageRelayService : IMessageRelayService
{
    private readonly ILogger<MessageRelayService> logger;

    private readonly IEventBus eventBus;
    private readonly List<Type> eventTypes;
    private readonly TransactionalOutboxContext context;

    public MessageRelayService(ILogger<MessageRelayService> logger, TransactionalOutboxContext context, IEventBus eventBus, Assembly assemblyWithIntegrationEvents)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.context = context ?? throw new ArgumentNullException(nameof(context));
        this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));

        eventTypes = assemblyWithIntegrationEvents.GetTypes()
                                                  .Where(t => t.Name.EndsWith("IntegrationEvent"))
                                                  .ToList();
    }

    public async Task OnTransactionFinished(Guid transactionId)
    {
        if (transactionId == Guid.Empty) throw new ArgumentException(nameof(transactionId));

        var pendingEvents = await context.Outbox.Where(o => o.PublishStatus == PublishStatus.Pending && o.TransactionId == transactionId)
                                                .OrderBy(o => o.CreatedDate)
                                                .ToListAsync();

        foreach (var pendingEvent in pendingEvents)
        {
            try
            {
                var eventType = eventTypes.Find(e => e.Name == pendingEvent.EventName);
                var @event = pendingEvent.RestoreEvent(eventType);

                eventBus.Publish(@event, publisherConfirms: true);
            }
            catch (Exception e)
            {
                logger.LogInformation($"[MessageRelay][Error]: Could not publish the event {pendingEvent.EventName} with id:{pendingEvent.EventId} from transaction {pendingEvent.TransactionId}! Error details => {e.Message}.");
            }
        }
    }
}