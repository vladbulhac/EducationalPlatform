using EducationalInstitution.Infrastructure.Repositories.Command_Repository;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using RabbitMQEventBus.Transactional_Outbox.Infrastructure;
using RabbitMQEventBus.Transactional_Outbox.Services.Outbox_Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitution.Infrastructure.Unit_of_Work.Command_Unit_of_Work
{
    public interface IUnitOfWorkForCommands : IDisposable
    {
        /// <returns>Returns an instance of a Repository class that implements <see cref="IEducationalInstitutionCommandRepository"/></returns>
        public IEducationalInstitutionCommandRepository UsingEducationalInstitutionCommandRepository();

        public Task<TResponse> ExecuteTransactionAsync<TRequest, TResponse>(Func<IDbContextTransaction, IIntegrationEventOutboxService, TRequest, Task<TResponse>> transactionOperations, TRequest request);

        public Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}