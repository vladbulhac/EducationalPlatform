using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using RabbitMQEventBus.IntegrationEvents;
using RabbitMQEventBus.Transactional_Outbox.Services.Outbox_Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitution.Application.BaseHandlers
{
    public abstract class CommandRequestHandlerBase<THandler, TRequest, TResponse> : HandlerBase<THandler>, IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse> where THandler : class
    {
        protected CommandRequestHandlerBase(ILogger<THandler> logger) : base(logger)
        {
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

        protected abstract Task<TResponse> TransactionOperations(IDbContextTransaction transaction, IIntegrationEventOutboxService eventOutboxService, TRequest request);

        protected async Task PublishIntegrationEventsAsync(IDbContextTransaction transaction, IIntegrationEventOutboxService eventOutboxService, List<IntegrationEvent> @events)
        {
            await eventOutboxService.SaveMultipleEventsToDatabaseAsync(@events, transaction);
        }

        protected async Task PublishIntegrationEventAsync(IDbContextTransaction transaction, IIntegrationEventOutboxService eventOutboxService, IntegrationEvent @event)
        {
            await eventOutboxService.SaveEventToDatabaseAsync(@event, transaction);
        }
    }
}