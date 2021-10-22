using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using RabbitMQEventBus.IntegrationEvents;
using RabbitMQEventBus.Transactional_Outbox.Infrastructure;
using RabbitMQEventBus.Transactional_Outbox.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQEventBus.Transactional_Outbox.Services.Outbox_Services
{
    /// <summary>
    /// <inheritdoc cref="IIntegrationEventOutboxService"/>
    /// <inheritdoc cref="IOutboxEventPublishingStatus"/>
    /// </summary>
    public class IntegrationEventOutboxService : IIntegrationEventOutboxService
    {
        private bool disposed;

        private readonly ILogger<IntegrationEventOutboxService> logger;
        private readonly TransactionalOutboxContext context;

        ///<remarks>The same <see cref="DbConnection"/> must be used on the <see cref="DbContext">DbContexts</see> that want to share a transaction</remarks>
        public IntegrationEventOutboxService(DbConnection sharedConnection, ILogger<IntegrationEventOutboxService> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            if (sharedConnection is null) throw new ArgumentNullException(nameof(sharedConnection));

            context = new(new DbContextOptionsBuilder<TransactionalOutboxContext>().UseSqlServer(sharedConnection).Options);
        }

        public async Task SaveEventToDatabaseAsync(IntegrationEvent @event, IDbContextTransaction transaction, CancellationToken cancellationToken = default)
        {
            if (@event is null) throw new ArgumentNullException(nameof(@event));
            if (transaction is null) throw new ArgumentNullException(nameof(transaction));

            await context.Database.UseTransactionAsync(transaction.GetDbTransaction(), cancellationToken);

            await AddEventToOutboxAsync(@event,
                                        transaction.TransactionId,
                                        cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveMultipleEventsToDatabaseAsync(List<IntegrationEvent> events, IDbContextTransaction transaction, CancellationToken cancellationToken = default)
        {
            if (events is null || events.Count == 0) throw new ArgumentException(nameof(events));
            if (transaction is null) throw new ArgumentNullException(nameof(transaction));

            await context.Database.UseTransactionAsync(transaction.GetDbTransaction(), cancellationToken);

            foreach (var @event in events)
            {
                await AddEventToOutboxAsync(@event,
                                            transaction.TransactionId,
                                            cancellationToken);
            }

            await context.SaveChangesAsync(cancellationToken);
        }

        private async Task AddEventToOutboxAsync(IntegrationEvent @event, Guid transactionId, CancellationToken cancellationToken = default)
        {
            if (@event is null) throw new ArgumentNullException(nameof(@event));
            if (transactionId == Guid.Empty) throw new ArgumentException(nameof(transactionId));

            Outbox outbox = new(@event, transactionId);

            await context.AddAsync(outbox, cancellationToken);

            logger.LogDebug("[Outbox]: Using transaction {0}, the event: {1} will be saved to the Outbox table!",
                                  transactionId,
                                  @event.ToString());
        }

        public async Task EventHasBeenPublished(string eventId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(eventId)) throw new ArgumentException(eventId);

            var eventDetails = await context.Outbox.FirstOrDefaultAsync(o => o.EventId == eventId, cancellationToken);

            if (eventDetails is not null)
            {
                eventDetails.EventHasBeenPublished();
                await context.SaveChangesAsync(cancellationToken);

                logger.LogInformation($"[Outbox][Success]: Event {eventDetails.EventName} from transaction {eventDetails.TransactionId} has been successfully published to the event bus!");
            }
        }

        public async Task PublishingFailed(string eventId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(eventId)) throw new ArgumentException(eventId);

            var eventDetails = await context.Outbox.FirstOrDefaultAsync(o => o.EventId == eventId, cancellationToken);

            if (eventDetails is not null)
            {
                eventDetails.EventCouldNotBePublished();
                await context.SaveChangesAsync(cancellationToken);

                logger.LogInformation($"[Outbox][Failure]: Event {eventDetails.EventName} from transaction {eventDetails.TransactionId} has NOT been published to the event bus!");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    context.Dispose();
            }

            disposed = true;
        }
    }
}