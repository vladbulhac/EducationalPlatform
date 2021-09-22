using EducationalInstitution.Application.Integration_Events;
using EducationalInstitution.Infrastructure.Repositories.Command_Repository;
using EducationalInstitution.Infrastructure.Repositories.Command_Repository.Results;
using EducationalInstitution.Infrastructure.Unit_of_Work.Command_Unit_of_Work;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQEventBus.Abstractions;
using RabbitMQEventBus.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Net;
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

                    eventBus.PublishMultiple(PublishNotificationEventsForAdmins(updateResult, request.EducationalInstitutionID));
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

        private IEnumerable<IntegrationEvent> PublishNotificationEventsForAdmins(AfterUpdateAdminsCommandChangesDetails notificationData, Guid educationalInstitutionID)
        {
            if (notificationData.NewAdminsToNotify.Count > 0)
            {
                yield return new AssignedAdminsToEducationalInstitutionIntegrationEvent
                {
                    Message = "Admin rights granted for an Educational Institution!",
                    ToNotify = notificationData.NewAdminsToNotify,
                    Uri = $"/edu/{educationalInstitutionID}",
                    TriggeredBy = new()
                    {
                        ServiceName = this.GetType().Namespace.Split('.')[0],
                        Action = "Update"
                    }
                };
            }

            if (notificationData.RemovedAdminsToNotify.Count > 0)
            {
                yield return new NotifyAdminsOfEducationalInstitutionUpdateIntegrationEvent
                {
                    Message = "Admin rights revoked for an Educational Institution!",
                    ToNotify = notificationData.RemovedAdminsToNotify,
                    Uri = $"/edu/{educationalInstitutionID}",
                    TriggeredBy = new()
                    {
                        ServiceName = this.GetType().Namespace.Split('.')[0],
                        Action = "Update"
                    }
                };
            }

            yield return new NotifyAdminsOfEducationalInstitutionUpdateIntegrationEvent
            {
                Message = "Educational Institution's admins were updated!",
                ToNotify = notificationData.AdminsToNotify,
                Uri = $"/edu/{educationalInstitutionID}",
                TriggeredBy = new()
                {
                    ServiceName = this.GetType().Namespace.Split('.')[0],
                    Action = "Update"
                }
            };
        }
    }
}