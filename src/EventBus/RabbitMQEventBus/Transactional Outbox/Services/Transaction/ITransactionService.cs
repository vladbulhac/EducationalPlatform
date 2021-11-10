using Microsoft.EntityFrameworkCore.Storage;
using RabbitMQEventBus.Transactional_Outbox.Models;
using RabbitMQEventBus.Transactional_Outbox.Services.Outbox_Services;

namespace RabbitMQEventBus.Transactional_Outbox.Services.Transaction;

public interface ITransactionService : IDisposable
{
    /// <summary>
    /// Within an <see cref="IExecutionStrategy"/> a shared <see cref="IDbContextTransaction"/> is used to atomically execute <paramref name="transactionOperations"/> and publish integration events.
    /// </summary>
    /// <remarks>Ensures that if any operation from <paramref name="transactionOperations"/> or from inserting the integration events into <see cref="Outbox"/> table fails, all changes will be discarded.</remarks>
    public Task<TResponse> ExecuteTransactionAsync<TRequest, TResponse>(Func<IDbContextTransaction, IIntegrationEventOutboxService, TRequest, Task<TResponse>> transactionOperations, TRequest request);
}