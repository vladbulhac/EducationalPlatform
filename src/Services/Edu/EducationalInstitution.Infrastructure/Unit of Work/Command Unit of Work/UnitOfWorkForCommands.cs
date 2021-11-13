using EducationalInstitution.Infrastructure.Repositories.Command_Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using RabbitMQEventBus.Transactional_Outbox.Services.MessageRelay;
using RabbitMQEventBus.Transactional_Outbox.Services.Outbox_Services;
using RabbitMQEventBus.Transactional_Outbox.Services.Transaction;

namespace EducationalInstitution.Infrastructure.Unit_of_Work.Command_Unit_of_Work;

public class UnitOfWorkForCommands : IUnitOfWorkForCommands
{
    private bool disposed;

    private readonly DataContext context;
    private readonly ITransactionService transactionService;

    public IEducationalInstitutionCommandRepository EducationalInstitutionRepository { get; private set; }

    public UnitOfWorkForCommands(ILoggerFactory loggerFactory, IMessageRelayService messageRelay, DbContextOptions<DataContext> options)
    {
        context = new(options);
        transactionService = new TransactionService(context, messageRelay, loggerFactory);
    }

    public IEducationalInstitutionCommandRepository UsingEducationalInstitutionCommandRepository()
    {
        if (EducationalInstitutionRepository is null)
            EducationalInstitutionRepository = new EducationalInstitutionCommandRepository(context);

        return EducationalInstitutionRepository;
    }

    public async Task<TResponse> ExecuteTransactionAsync<TRequest, TResponse>(Func<IDbContextTransaction, IIntegrationEventOutboxService, TRequest, Task<TResponse>> transactionOperations, TRequest request)
    {
        return await transactionService.ExecuteTransactionAsync(transactionOperations, request);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await context.SaveChangesAsync(cancellationToken);

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
                context.Dispose();
                transactionService.Dispose();
            }
        }

        disposed = true;
    }
}