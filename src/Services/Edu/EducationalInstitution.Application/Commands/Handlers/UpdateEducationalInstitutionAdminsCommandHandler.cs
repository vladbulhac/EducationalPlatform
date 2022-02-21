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
using System.Text;

namespace EducationalInstitution.Application.Commands.Handlers;

public class UpdateEducationalInstitutionAdminsCommandHandler : CommandHandlerBase<UpdateEducationalInstitutionAdminsCommandHandler,
                                                                                   UpdateEducationalInstitutionAdminsCommand,
                                                                                   Response>
{
    private readonly IUnitOfWorkForCommands unitOfWork;

    /// <inheritdoc cref="HandlerBase{THandler}.HandlerBase"/>
    public UpdateEducationalInstitutionAdminsCommandHandler(IUnitOfWorkForCommands unitOfWork, ILogger<UpdateEducationalInstitutionAdminsCommandHandler> logger) : base(logger)
    {
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <summary>
    /// Tries to update the Admins of an <see cref="EducationalInstitution"/> entity.
    /// </summary>
    /// <returns>
    /// An <see cref="Response">object</see> with HttpStatusCode:
    /// <list type="bullet">
    /// <item><see cref="HttpStatusCode.NoContent">NoContent</see> if operation is successful</item>
    /// <item><see cref="HttpStatusCode.NotFound">NotFound</see> if no <see cref="EducationalInstitution"/> has been found for the provided EducationalInstitutionID</item>
    /// <item><see cref="HttpStatusCode.InternalServerError">InternalServerError</see> if the entity could not be updated</item>
    /// </list>
    /// </returns>
    /// <exception cref="ArgumentNullException"/>
    public override async Task<Response> Handle(UpdateEducationalInstitutionAdminsCommand request, CancellationToken cancellationToken = default)
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
                response_message: "An error occurred while updating the Educational Institution's admins with the given data!",
                JsonConvert.SerializeObject(request),
                unitOfWork.GetType(),
                unitOfWork.UsingEducationalInstitutionCommandRepository().GetType(),
                nameof(IEducationalInstitutionCommandRepository.GetEducationalInstitutionIncludingAdminsAsync),
                e.Message);
        }
    }

    protected override async Task<Response> TransactionOperations(IDbContextTransaction transaction, IIntegrationEventOutboxService eventOutboxService, UpdateEducationalInstitutionAdminsCommand request)
    {
        var educationalInstitution = await unitOfWork.UsingEducationalInstitutionCommandRepository()
                                                     .GetEducationalInstitutionIncludingAdminsAsync(request.EducationalInstitutionID);

        if (educationalInstitution == default)
            return new()
            {
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound,
                Message = $"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!"
            };

        foreach (var admin in request.NewAdmins)
            educationalInstitution.CreateAndAddAdmin(admin.Identity, admin.Permissions);

        foreach (var admin in request.AdminsWithNewPermissions)
            educationalInstitution.GrantAdminPermissions(admin.Identity, admin.Permissions);

        foreach (var admin in request.AdminsWithRevokedPermissions)
            educationalInstitution.RevokeAdminPermissions(admin.Identity, admin.Permissions);

        await unitOfWork.SaveChangesAsync();

        await PublishIntegrationEventsAsync(transaction,
                                            eventOutboxService,
                                            CreateNotificationEventsForAdmins(request.NewAdmins,
                                                                              request.AdminsWithRevokedPermissions,
                                                                              request.AdminsWithNewPermissions,
                                                                              request.EducationalInstitutionID));
        return new()
        {
            Message = string.Empty,
            OperationStatus = true,
            StatusCode = HttpStatusCode.NoContent
        };
    }

    private IEnumerable<IntegrationEvent> CreateNotificationEventsForAdmins(ICollection<AdminDetails> newAdmins, ICollection<AdminDetails> adminsWithRevokedPermissions, ICollection<AdminDetails> adminsWithNewPermissions, Guid educationalInstitutionID)
    {
        if (newAdmins.Count > 0)
            yield return new AssignedAdminsToEducationalInstitutionIntegrationEvent(BuildDetailedMessage(newAdmins, $"permissions has been granted to you for the Educational Institution accessible at [{educationalInstitutionID}]"),
                                                                                    educationalInstitutionID,
                                                                                    $"/edu/{educationalInstitutionID}",
                                                                                    "Add");

        if (adminsWithRevokedPermissions.Count > 0)
            yield return new UpdatedAdminsPermissionsIntegrationEvent
            {
                Message = "Admin rights revoked for an Educational Institution!",
                EducationalInstitutionId = educationalInstitutionID,
                UpdatedAdmins = BuildDetailedMessage(adminsWithRevokedPermissions, $"permissions have been revoked for the Educational Institution."),
                Uri = $"/edu/{educationalInstitutionID}",
                TriggeredBy = new()
                {
                    ServiceName = this.GetType().Namespace.Split('.')[0],
                    Action = "Delete"
                }
            };

        if (adminsWithNewPermissions.Count > 0)
            yield return new UpdatedAdminsPermissionsIntegrationEvent
            {
                Message = "Admin rights updated for an Educational Institution!",
                EducationalInstitutionId = educationalInstitutionID,
                UpdatedAdmins = BuildDetailedMessage(adminsWithNewPermissions, $"permissions have been granted for the Educational Institution."),
                Uri = $"/edu/{educationalInstitutionID}",
                TriggeredBy = new()
                {
                    ServiceName = this.GetType().Namespace.Split('.')[0],
                    Action = "Add"
                }
            };
    }

    /// <summary>
    /// For each given admin will create an <see cref="AdminDetailsForIntegrationEvent"/>.
    /// </summary>
    /// <remarks>
    /// The constructed detailed messaged will look like: 'Permission1 Permission2 Permission3... PermissionN appendMessage'.
    /// </remarks>
    private AdminDetailsForIntegrationEvent[] BuildDetailedMessage(ICollection<AdminDetails> newAdmins, string appendMessage)
    {
        var result = new AdminDetailsForIntegrationEvent[newAdmins.Count];
        var messageBuilder = new StringBuilder();

        var index = 0;
        foreach (var admin in newAdmins)
        {
            ConstructMessageInBuilder(admin.Permissions, appendMessage, messageBuilder);

            result[index] = new()
            {
                DetailedMessage = messageBuilder.ToString(),
                Identity = admin.Identity,
                Permissions = admin.Permissions
            };
            index++;

            messageBuilder.Clear();
        }

        return result;
    }

    private void ConstructMessageInBuilder(ICollection<string> permissions, string appendMessage, StringBuilder messageBuilder)
    {
        foreach (var permission in permissions)
            messageBuilder.Append(permission.Split('.')[2])
                          .Append(' ');

        messageBuilder.Append(appendMessage);
    }
}