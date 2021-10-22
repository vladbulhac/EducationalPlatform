using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using RabbitMQEventBus.Transactional_Outbox.Infrastructure;
using RabbitMQEventBus.Transactional_Outbox.Services.MessageRelay;
using RabbitMQEventBus.Transactional_Outbox.Services.Outbox_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQEventBus.Transactional_Outbox.Services.Transaction
{
    public class TransactionService : ITransactionService
    {
        private bool disposed;

        private readonly DbContext context;
        private readonly ILogger<TransactionService> logger;
        private readonly ILoggerFactory loggerFactory;

        private readonly IMessageRelayService messageRelay;

        public TransactionService(DbContext context, IMessageRelayService messageRelay, ILoggerFactory loggerFactory)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.messageRelay = messageRelay ?? throw new ArgumentNullException(nameof(messageRelay));
            this.loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));

            logger = loggerFactory.CreateLogger<TransactionService>();
        }

        public async Task<TResponse> ExecuteTransactionAsync<TRequest, TResponse>(Func<IDbContextTransaction, IIntegrationEventOutboxService, TRequest, Task<TResponse>> transactionOperations, TRequest request)
        {
            var executionStrategy = context.Database.CreateExecutionStrategy();

            TResponse response = default;

            await executionStrategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();
                using var eventOutboxService = new IntegrationEventOutboxService(context.Database.GetDbConnection(), loggerFactory.CreateLogger<IntegrationEventOutboxService>());

                try
                {
                    logger.LogInformation($"[Transaction]: Beginning transaction {transaction.TransactionId}.");

                    response = await transactionOperations(transaction, eventOutboxService, request);

                    // Transaction will auto-rollback when disposed if any command fails.
                    await transaction.CommitAsync();

                    await messageRelay.OnTransactionFinished(transaction.TransactionId);
                    logger.LogInformation($"[Transaction]: Transaction {transaction.TransactionId} finished and all the pending integration events had been sent to the eventbus!");
                }
                catch (Exception e)
                {
                    logger.LogInformation($"[Transaction]: Transaction {transaction.TransactionId} failed! All changes will be discarded! Error details => {e.Message}");
                }
            });

            return response;
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
                {
                    loggerFactory.Dispose();
                }
            }

            disposed = true;
        }
    }
}