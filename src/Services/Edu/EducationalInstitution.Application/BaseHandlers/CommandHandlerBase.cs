using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using RabbitMQEventBus.IntegrationEvents;
using RabbitMQEventBus.Transactional_Outbox.Services.Outbox_Services;

namespace EducationalInstitution.Application.BaseHandlers;

public abstract class CommandHandlerBase<THandler, TRequest, TResponse> : HandlerBase<THandler>, IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse> where THandler : class
{
    protected CommandHandlerBase(ILogger<THandler> logger) : base(logger)
    { }

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    protected abstract Task<TResponse> TransactionOperations(IDbContextTransaction transaction, IIntegrationEventOutboxService eventOutboxService, TRequest request);

    protected async Task PublishIntegrationEventsAsync(IDbContextTransaction transaction, IIntegrationEventOutboxService eventOutboxService, IEnumerable<IntegrationEvent> events)
    {
        if (transaction is null) throw new ArgumentNullException(nameof(transaction));
        if (eventOutboxService is null) throw new ArgumentNullException(nameof(eventOutboxService));
        if (events is null || !events.Any()) throw new ArgumentNullException(nameof(events));

        await eventOutboxService.SaveMultipleEventsToDatabaseAsync(events, transaction);
    }

    protected async Task PublishIntegrationEventAsync(IDbContextTransaction transaction, IIntegrationEventOutboxService eventOutboxService, IntegrationEvent @event)
    {
        if (transaction is null) throw new ArgumentNullException(nameof(transaction));
        if (eventOutboxService is null) throw new ArgumentNullException(nameof(eventOutboxService));
        if (@event is null) throw new ArgumentNullException(nameof(@event));

        await eventOutboxService.SaveEventToDatabaseAsync(@event, transaction);
    }
}