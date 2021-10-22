using Microsoft.EntityFrameworkCore.Storage;
using RabbitMQEventBus.IntegrationEvents;
using RabbitMQEventBus.Transactional_Outbox.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQEventBus.Transactional_Outbox.Services.Outbox_Services
{
    /// <summary>
    /// A service that uses a relational database inserts <see cref="IntegrationEvent">events</see> into an <see cref="Outbox"/> table as part of the local <see cref="IDbContextTransaction">transaction</see>.
    /// </summary>
    public interface IIntegrationEventOutboxService : IOutboxEventPublishingStatus, IDisposable
    {
        public Task SaveEventToDatabaseAsync(IntegrationEvent @event, IDbContextTransaction transaction, CancellationToken cancellationToken = default);

        public Task SaveMultipleEventsToDatabaseAsync(List<IntegrationEvent> events, IDbContextTransaction transaction, CancellationToken cancellationToken = default);
    }
}