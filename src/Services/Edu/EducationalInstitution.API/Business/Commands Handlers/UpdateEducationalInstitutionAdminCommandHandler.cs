using EducationalInstitutionAPI.Business.IntegrationEvents_Handlers;
using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Events_Definitions;
using EducationalInstitutionAPI.Data.Repository_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Repositories.Command_Repository;
using EducationalInstitutionAPI.Unit_of_Work.Command_Unit_of_Work;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQEventBus.Abstractions;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Business.Commands_Handlers
{
    public class UpdateEducationalInstitutionAdminCommandHandler : HandlerBase<UpdateEducationalInstitutionAdminCommandHandler>,
                                                                   IRequestHandler<DTOEducationalInstitutionAdminUpdateCommand, Response>
    {
        private readonly IUnitOfWorkForCommands unitOfWork;
        private readonly IEventBus eventBus;

        /// <inheritdoc cref="HandlerBase{THandler}.HandlerBase"/>
        public UpdateEducationalInstitutionAdminCommandHandler(IUnitOfWorkForCommands unitOfWork, IEventBus eventBus, ILogger<UpdateEducationalInstitutionAdminCommandHandler> logger) : base(logger)
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
        public async Task<Response> Handle(DTOEducationalInstitutionAdminUpdateCommand request, CancellationToken cancellationToken = default)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                using (unitOfWork)
                {
                    var updateResult = await unitOfWork.UsingEducationalInstitutionCommandRepository()
                                                              .UpdateAdminsAsync(request.EducationalInstitutionID,
                                                                                 request.AddAdminsIDs,
                                                                                 request.RemoveAdminsIDs,
                                                                                 cancellationToken);

                    if (updateResult == default)
                        return new()
                        {
                            OperationStatus = false,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = $"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!"
                        };

                    await unitOfWork.SaveChangesAsync(cancellationToken);
                    PublishNotificationEventsToAdmins(updateResult, request.EducationalInstitutionID);
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
                    nameof(IEducationalInstitutionCommandRepository.UpdateAdminsAsync),
                    e.Message);
            }

            return new()
            {
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent
            };
        }

        private void PublishNotificationEventsToAdmins(UpdateAdminsCommandRepositoryResult notificationData, Guid educationalInstitutionID)
        {
            AssignedAdminsToEducationalInstitutionIntegrationEvent @newAdminsEvent = new()
            {
                Message = "Admin rights granted for an Educational Institution!",
                ToNotify = notificationData.NewAdminsToNotify,
                Url = $"/edu/{educationalInstitutionID}",
                TriggeredBy = new()
                {
                    ServiceName = this.GetType().Namespace.Split('.')[0],
                    Action = "Update"
                }
            };
            eventBus.Publish(@newAdminsEvent);

            NotifyAdminsOfEducationalInstitutionUpdateIntegrationEvent @existingAdminsEvent = new()
            {
                Message = "Educational Institution's admins were updated!",
                ToNotify = notificationData.ExistingAdminsToNotify,
                Url = $"/edu/{educationalInstitutionID}",
                TriggeredBy = new()
                {
                    ServiceName = this.GetType().Namespace.Split('.')[0],
                    Action = "Update"
                }
            };
            eventBus.Publish(existingAdminsEvent);

            NotifyAdminsOfEducationalInstitutionUpdateIntegrationEvent @removedAdminsEvent = new()
            {
                Message = "Admin rights revoked for an Educational Institution!",
                ToNotify = notificationData.RemovedAdminsToNotify,
                Url = $"/edu/{educationalInstitutionID}",
                TriggeredBy = new()
                {
                    ServiceName = this.GetType().Namespace.Split('.')[0],
                    Action = "Update"
                }
            };
            eventBus.Publish(removedAdminsEvent);
        }
    }
}