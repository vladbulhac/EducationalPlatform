using EducationalInstitution.Application.Integration_Events;
using EducationalInstitution.Infrastructure.Repositories.Command_Repository;
using EducationalInstitution.Infrastructure.Unit_of_Work.Command_Unit_of_Work;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQEventBus.Abstractions;
using RabbitMQEventBus.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitution.Application.Commands.Handlers
{
    public class UpdateEducationalInstitutionAdminsCommandHandler : HandlerBase<UpdateEducationalInstitutionAdminsCommandHandler>,
                                                                    IRequestHandler<UpdateEducationalInstitutionAdminsCommand, Response>
    {
        private readonly IUnitOfWorkForCommands unitOfWork;
        private readonly IEventBus eventBus;

        /// <inheritdoc cref="HandlerBase{THandler}.HandlerBase"/>
        public UpdateEducationalInstitutionAdminsCommandHandler(IUnitOfWorkForCommands unitOfWork, IEventBus eventBus, ILogger<UpdateEducationalInstitutionAdminsCommandHandler> logger) : base(logger)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        /// <summary>
        /// Tries to update the Admins of an <see cref="EducationalInstitution"/> entity
        /// </summary>
        /// <param name="cancellationToken">Cancels the operation ____________</param>
        /// <returns>
        /// An <see cref="Response">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.NoContent">NoContent</see> if operation is successful</item>
        /// <item><see cref="HttpStatusCode.NotFound">NotFound</see> if no <see cref="EducationalInstitution"/> has been found for the provided EducationalInstitutionID</item>
        /// <item><see cref="HttpStatusCode.InternalServerError">InternalServerError</see> if the entity could not be updated</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public async Task<Response> Handle(UpdateEducationalInstitutionAdminsCommand request, CancellationToken cancellationToken = default)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                using (unitOfWork)
                {
                    var educationalInstitution = await unitOfWork.UsingEducationalInstitutionCommandRepository()
                                                                 .GetEducationalInstitutionIncludingAdminsAsync(request.EducationalInstitutionID, cancellationToken);

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

                    await unitOfWork.SaveChangesAsync(cancellationToken);

                    eventBus.PublishMultiple(PublishNotificationEventsForAdmins(request.NewAdmins,
                                                                                request.AdminsWithRevokedPermissions,
                                                                                request.AdminsWithNewPermissions,
                                                                                request.EducationalInstitutionID));
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

            return new()
            {
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent
            };
        }

        private IEnumerable<IntegrationEvent> PublishNotificationEventsForAdmins(ICollection<AdminDetails> newAdmins, ICollection<AdminDetails> adminsWithRevokedPermissions, ICollection<AdminDetails> adminsWithNewPermissions, Guid educationalInstitutionID)
        {
            if (newAdmins.Count > 0)
            {
                yield return new AssignedAdminsToEducationalInstitutionIntegrationEvent(BuildDetailedMessage(newAdmins, $"permissions has been granted to you for the Educational Institution accessible at [{educationalInstitutionID}]"),
                                                                                        educationalInstitutionID,
                                                                                        $"/edu/{educationalInstitutionID}",
                                                                                        "Add");
            }

            if (adminsWithRevokedPermissions.Count > 0)
            {
                yield return new UpdatedAdminsPermissionsIntegrationEvent
                {
                    Message = "Admin rights revoked for an Educational Institution!",
                    EducationalInstitutionId = educationalInstitutionID,
                    UpdatedAdmins = BuildDetailedMessage(adminsWithRevokedPermissions, $"permissions have been revoked for the Educational Institution accessible at [{educationalInstitutionID}]"),
                    Uri = $"/edu/{educationalInstitutionID}",
                    TriggeredBy = new()
                    {
                        ServiceName = this.GetType().Namespace.Split('.')[0],
                        Action = "Delete"
                    }
                };
            }

            yield return new UpdatedAdminsPermissionsIntegrationEvent
            {
                Message = "Admin rights updated for an Educational Institution!",
                EducationalInstitutionId = educationalInstitutionID,
                UpdatedAdmins = BuildDetailedMessage(adminsWithNewPermissions, $"permissions have been granted for the Educational Institution accessible at [{educationalInstitutionID}]"),
                Uri = $"/edu/{educationalInstitutionID}",
                TriggeredBy = new()
                {
                    ServiceName = this.GetType().Namespace.Split('.')[0],
                    Action = "Add"
                }
            };
        }

        private AdminDetailsForIntegrationEvent[] BuildDetailedMessage(ICollection<AdminDetails> newAdmins, string appendedMessage)
        {
            var result = new AdminDetailsForIntegrationEvent[newAdmins.Count];
            var index = 0;
            foreach (var admin in newAdmins)
            {
                var permissions = new StringBuilder();

                foreach (var permission in admin.Permissions)
                    permissions.Append(permission.Split('.')[2])
                               .Append(' ');

                permissions.Append(appendedMessage);

                result[index] = new()
                {
                    DetailedMessage = permissions.ToString(),
                    Identity = admin.Identity,
                    Permissions = admin.Permissions
                };

                index++;
            }

            return result;
        }
    }
}