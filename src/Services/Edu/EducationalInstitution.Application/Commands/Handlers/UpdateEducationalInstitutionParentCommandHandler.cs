using EducationalInstitution.Application.BaseHandlers;
using EducationalInstitution.Application.Integration_Events;
using EducationalInstitution.Infrastructure.Repositories.Command_Repository;
using EducationalInstitution.Infrastructure.Unit_of_Work.Command_Unit_of_Work;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQEventBus.IntegrationEvents;
using RabbitMQEventBus.Transactional_Outbox.Services.Outbox_Services;
using System.Net;

namespace EducationalInstitution.Application.Commands.Handlers;

public class UpdateEducationalInstitutionParentCommandHandler : CommandHandlerBase<UpdateEducationalInstitutionParentCommandHandler,
                                                                                   UpdateEducationalInstitutionParentCommand, Response>
{
    private readonly IUnitOfWorkForCommands unitOfWork;

    /// <inheritdoc cref="HandlerBase{THandler}.HandlerBase"/>
    public UpdateEducationalInstitutionParentCommandHandler(IUnitOfWorkForCommands unitOfWork, ILogger<UpdateEducationalInstitutionParentCommandHandler> logger) : base(logger)
    {
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <summary>
    /// Tries to update the ParentInstitution field of an <see cref="EducationalInstitution"/> entity
    /// </summary>
    /// <param name="cancellationToken">Cancels the operation ____________</param>
    /// <returns>
    /// An <see cref="Response">object</see> with HttpStatusCode:
    /// <list type="bullet">
    /// <item><see cref="HttpStatusCode.NoContent">NoContent</see> if operation is successful</item>
    /// <item><see cref="HttpStatusCode.NotFound">NotFound</see> if no <see cref="EducationalInstitution"/> has been found for the provided EducationalInstitutionID or ParentInstitutionID</item>
    /// <item><see cref="HttpStatusCode.InternalServerError">InternalServerError</see> if the entity could not be updated</item>
    /// </list>
    /// </returns>
    /// <exception cref="ArgumentNullException"/>
    public override async Task<Response> Handle(UpdateEducationalInstitutionParentCommand request, CancellationToken cancellationToken = default)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        try
        {
            using (unitOfWork)
            {
                var transactionResult = await unitOfWork.ExecuteTransactionAsync(TransactionOperations, request);

                if (transactionResult == default)
                    return new()
                    {
                        OperationStatus = false,
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = "Could not successfully handle all the required operations for this request!"
                    };

                return transactionResult;
            }
        }
        catch (Exception e)
        {
            return HandleException<Response>(
                    error_message: "Could not update the Educational Institution with the given data: {0}, using {1} with {2}'s method: {3}, error details => {4}",
                    response_message: "An error occurred while updating the parent of the Educational Institution with the given data!",
                    JsonConvert.SerializeObject(request),
                    unitOfWork.GetType(),
                    unitOfWork.UsingEducationalInstitutionCommandRepository().GetType(),
                    nameof(IEducationalInstitutionCommandRepository.GetEducationalInstitutionIncludingAdminsAsync),
                    e.Message);
        }
    }

    protected override async Task<Response> TransactionOperations(IDbContextTransaction transaction, IIntegrationEventOutboxService eventOutboxService, UpdateEducationalInstitutionParentCommand request)
    {
        var parentInstitution = await unitOfWork.UsingEducationalInstitutionCommandRepository()
                                                .GetEducationalInstitutionIncludingAdminsAsync(request.ParentInstitutionID);
        if (parentInstitution == default)
            return new()
            {
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound,
                Message = $"The Parent Educational Institution with the ID: {request.ParentInstitutionID} has not been found!"
            };

        var educationalInstitution = await unitOfWork.UsingEducationalInstitutionCommandRepository()
                                                    .GetEducationalInstitutionIncludingAdminsAsync(request.EducationalInstitutionID);
        if (educationalInstitution == default)
            return new()
            {
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound,
                Message = $"The Educational Institution with the ID: {request.EducationalInstitutionID} has not been found!"
            };

        educationalInstitution.SetParentInstitution(parentInstitution);

        await unitOfWork.SaveChangesAsync();

        await PublishIntegrationEventsAsync(transaction,
                                            eventOutboxService,
                                            CreateNotificationEventsForAdmins(request.EducationalInstitutionID,
                                                                              educationalInstitution.Admins.Select(a => a.Id).ToList(),
                                                                              parentInstitution.Admins.Select(a => a.Id).ToList()));

        return new()
        {
            OperationStatus = true,
            StatusCode = HttpStatusCode.NoContent,
            Message = string.Empty
        };
    }

    private IEnumerable<IntegrationEvent> CreateNotificationEventsForAdmins(Guid educationalInstitutionID, ICollection<string> adminsToNotify, ICollection<string> parentAdminsToNotify)
    {
        if (adminsToNotify is not null && adminsToNotify.Count > 0)
            yield return new NotifyAdminsOfEducationalInstitutionUpdateIntegrationEvent
            {
                Message = "An Educational Institution's parent has been recently updated!",
                TriggeredBy = new()
                {
                    Action = "Update",
                    ServiceName = this.GetType().Namespace.Split('.')[0]
                },
                Uri = $"/edu/{educationalInstitutionID}",
                ToNotify = adminsToNotify,
            };

        if (parentAdminsToNotify is not null && parentAdminsToNotify.Count > 0)
        {
            yield return new NotifyAdminsOfNewEducationalInstitutionChildIntegrationEvent
            {
                Message = "An Educational Institution assigned this institution as a parent!",
                ToNotify = parentAdminsToNotify,
                Uri = $"/edu/{educationalInstitutionID}",
                TriggeredBy = new()
                {
                    ServiceName = this.GetType().Namespace.Split('.')[0],
                    Action = "Create"
                }
            };
        }
    }
}