using EducationalInstitution.Application.BaseHandlers;
using EducationalInstitution.Application.Commands.Results;
using EducationalInstitution.Application.Integration_Events;
using EducationalInstitution.Infrastructure.Repositories.Command_Repository;
using EducationalInstitution.Infrastructure.Unit_of_Work.Command_Unit_of_Work;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using RabbitMQEventBus.IntegrationEvents;
using RabbitMQEventBus.Transactional_Outbox.Services.Outbox_Services;
using System.Net;

namespace EducationalInstitution.Application.Commands.Handlers;

public class DisableEducationalInstitutionCommandHandler : CommandRequestHandlerBase<DisableEducationalInstitutionCommandHandler,
                                                                                     DisableEducationalInstitutionCommand,
                                                                                     Response<DisableEducationalInstitutionCommandResult>>
{
    private readonly IUnitOfWorkForCommands unitOfWorkCommand;

    /// <inheritdoc cref="HandlerBase{THandler}.HandlerBase"/>
    public DisableEducationalInstitutionCommandHandler(IUnitOfWorkForCommands unitOfWorkCommand, ILogger<DisableEducationalInstitutionCommandHandler> logger) : base(logger)
    {
        this.unitOfWorkCommand = unitOfWorkCommand ?? throw new ArgumentNullException(nameof(unitOfWorkCommand));
    }

    /// <summary>
    /// Schedules an <see cref="EducationalInstitution"/> entity for deletion
    /// </summary>
    /// <param name="cancellationToken">Cancels the operation ________</param>
    /// <returns>
    /// An <see cref="Response{TData}">object</see> with HttpStatusCode:
    /// <list type="bullet">
    /// <item><see cref="HttpStatusCode.Accepted">Accepted</see> if the entity has been scheduled for deletion</item>
    /// <item><see cref="HttpStatusCode.NotFound">NotFound</see> if the <see cref="EducationalInstitution"/> has not been found</item>
    /// <item><see cref="HttpStatusCode.InternalServerError">InternalServerError</see> if the schedule for deletion could not be saved in the database</item>
    /// </list>
    /// </returns>
    public override async Task<Response<DisableEducationalInstitutionCommandResult>> Handle(DisableEducationalInstitutionCommand request, CancellationToken cancellationToken = default)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        try
        {
            using (unitOfWorkCommand)
            {
                var transactionResult = await unitOfWorkCommand.ExecuteTransactionAsync(TransactionOperations, request);

                if (transactionResult == default)
                    return new()
                    {
                        Data = null,
                        OperationStatus = false,
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = "Could not successfully handle all the required operations for this request!"
                    };

                return transactionResult;
            }
        }
        catch (Exception e)
        {
            return HandleException<Response<DisableEducationalInstitutionCommandResult>>(
                     error_message: "Could not schedule for deletion the Educational Institution with ID: {0}, using {1} and {2} with {3}, error details => {4}",
                     response_message: "An error occurred while scheduling for deletion the Educational Institution with the following ID: {0}!",
                     request.EducationalInstitutionID,
                     unitOfWorkCommand.GetType(),
                     unitOfWorkCommand.UsingEducationalInstitutionCommandRepository().GetType(),
                     nameof(IEducationalInstitutionCommandRepository.GetEducationalInstitutionIncludingAdminsAsync),
                     e.Message);
        }
    }

    protected override async Task<Response<DisableEducationalInstitutionCommandResult>> TransactionOperations(IDbContextTransaction transaction, IIntegrationEventOutboxService eventOutboxService, DisableEducationalInstitutionCommand request)
    {
        var educationalInstitution = await unitOfWorkCommand.UsingEducationalInstitutionCommandRepository()
                                                            .GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(request.EducationalInstitutionID);

        if (educationalInstitution == default)
            return new()
            {
                Data = null,
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound,
                Message = $"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!"
            };

        educationalInstitution.ScheduleForDeletion();

        await unitOfWorkCommand.SaveChangesAsync();

        await PublishIntegrationEventAsync(transaction,
                                           eventOutboxService,
                                           CreateNotificationEventForAdmins(request.EducationalInstitutionID,
                                                                            educationalInstitution.GetDeletionDate().Value,
                                                                            educationalInstitution.Admins.Select(a => a.Id).ToList()));

        return new()
        {
            Data = new() { DateForPermanentDeletion = educationalInstitution.GetDeletionDate().Value },
            OperationStatus = true,
            StatusCode = HttpStatusCode.Accepted,
            Message = string.Empty
        };
    }

    private IntegrationEvent CreateNotificationEventForAdmins(Guid educationalInstitutionID, DateTime scheduledDateForDeletion, ICollection<string> adminsToNotify)
    {
        return new NotifyAdminsOfEducationalInstitutionScheduledForDeletionIntegrationEvent()
        {
            Message = $"The Educational Institution with ID: {educationalInstitutionID} has been scheduled for deletion on: {scheduledDateForDeletion}!",
            ToNotify = adminsToNotify,
            Uri = string.Empty,
            TriggeredBy = new()
            {
                ServiceName = this.GetType().Namespace.Split('.')[0],
                Action = "Delete"
            }
        };
    }
}