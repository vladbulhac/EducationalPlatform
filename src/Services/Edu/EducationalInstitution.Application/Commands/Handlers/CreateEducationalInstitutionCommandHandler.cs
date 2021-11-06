using EducationalInstitution.Application.BaseHandlers;
using EducationalInstitution.Application.Commands.Results;
using EducationalInstitution.Application.Integration_Events;
using EducationalInstitution.Application.Permissions;
using EducationalInstitution.Domain.Models;
using EducationalInstitution.Infrastructure.Repositories.Command_Repository;
using EducationalInstitution.Infrastructure.Unit_of_Work.Command_Unit_of_Work;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQEventBus.Abstractions;
using RabbitMQEventBus.IntegrationEvents;
using RabbitMQEventBus.Transactional_Outbox.Services.Outbox_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Domain = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.Application.Commands.Handlers
{
    public class CreateEducationalInstitutionCommandHandler : CommandRequestHandlerBase<CreateEducationalInstitutionCommandHandler,
                                                                                        CreateEducationalInstitutionCommand,
                                                                                        Response<CreateEducationalInstitutionCommandResult>>
    {
        private readonly IUnitOfWorkForCommands unitOfWork;

        /// <inheritdoc cref="HandlerBase{THandler}.HandlerBase"/>
        public CreateEducationalInstitutionCommandHandler(IUnitOfWorkForCommands unitOfWork, ILogger<CreateEducationalInstitutionCommandHandler> logger) : base(logger)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Tries to create and save to the database a new <see cref="EducationalInstitution"/> entity
        /// </summary>
        /// <param name="cancellationToken">Cancels the operation ________</param>
        /// <returns>
        /// An <see cref="Response{TData}">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.Created">Created</see> if operation is successful</item>
        /// <item><see cref="HttpStatusCode.MultiStatus">MultiStatus</see> if an ID for the <see cref="EducationalInstitution">Parent Institution</see> has provided but has not been found in the database but the <see cref="EducationalInstitution"/> has been saved in the database</item>
        /// <item><see cref="HttpStatusCode.InternalServerError">InternalServerError</see> if the entity could not be inserted into the database</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public override async Task<Response<CreateEducationalInstitutionCommandResult>> Handle(CreateEducationalInstitutionCommand request, CancellationToken cancellationToken = default)
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
                return HandleException<Response<CreateEducationalInstitutionCommandResult>>(
                    error_message: "Could not create an Educational Institution with the request data: {0}, using {1} with {2}'s method: {3}, error details => {4}",
                    response_message: "An error occurred while creating the Educational Institution with the given data!",
                    JsonConvert.SerializeObject(request),
                    unitOfWork.GetType(),
                    unitOfWork.UsingEducationalInstitutionCommandRepository().GetType(),
                    nameof(IEducationalInstitutionCommandRepository.CreateAsync),
                    e.Message);
            }
        }

        protected override async Task<Response<CreateEducationalInstitutionCommandResult>> TransactionOperations(IDbContextTransaction transaction, IIntegrationEventOutboxService eventOutboxService, CreateEducationalInstitutionCommand request)
        {
            Domain::EducationalInstitution parentInstitution = null;
            if (request.ParentInstitutionID != default)
                parentInstitution = await unitOfWork.UsingEducationalInstitutionCommandRepository()
                                                    .GetEducationalInstitutionIncludingAdminsAsync(request.ParentInstitutionID);

            Domain::EducationalInstitution newEducationalInstitution = new(request.Name,
                                                                           request.Description,
                                                                           request.LocationID,
                                                                           request.BuildingsIDs,
                                                                           request.AdminId,
                                                                           parentInstitution);

            await unitOfWork.UsingEducationalInstitutionCommandRepository()
                            .CreateAsync(newEducationalInstitution);
            await unitOfWork.SaveChangesAsync();

            await PublishIntegrationEventsAsync(transaction,
                                                eventOutboxService,
                                                CreateNotificationEventsForAdmins(newEducationalInstitution.Id,
                                                                                   newEducationalInstitution.Admins,
                                                                                   parentInstitution?.Admins.Select(a => a.Id).ToList()));

            if (parentInstitution is null && request.ParentInstitutionID != default)
                return new()
                {
                    Data = new() { EducationalInstitutionID = newEducationalInstitution.Id },
                    OperationStatus = true,
                    StatusCode = HttpStatusCode.MultiStatus,
                    Message = $"The Educational Institution has been successfully created but the Parent Institution with the following ID: {request.ParentInstitutionID} has not been found!"
                };

            return new()
            {
                Data = new() { EducationalInstitutionID = newEducationalInstitution.Id },
                OperationStatus = true,
                StatusCode = HttpStatusCode.Created,
                Message = string.Empty
            };
        }

        private IEnumerable<IntegrationEvent> CreateNotificationEventsForAdmins(Guid newEducationalInstitutionId, ICollection<EducationalInstitutionAdmin> admins, ICollection<string> parentAdmins)
        {
            if (parentAdmins is not null && parentAdmins.Count > 0)
            {
                yield return new NotifyAdminsOfNewEducationalInstitutionChildIntegrationEvent
                {
                    Message = "An Educational Institution assigned this institution as a parent!",
                    ToNotify = parentAdmins,
                    Uri = $"/edu/{newEducationalInstitutionId}",
                    TriggeredBy = new()
                    {
                        ServiceName = this.GetType().Namespace.Split('.')[0],
                        Action = "Create"
                    }
                };
            }

            if (admins is not null && admins.Count > 0)
                yield return new AssignedAdminsToEducationalInstitutionIntegrationEvent
                {
                    Message = "Admin rights granted for a recently created Educational Institution!",
                    EducationalInstitutionId = newEducationalInstitutionId,
                    NewAdmins = GetAdminDetails(admins),
                    Uri = $"/edu/{newEducationalInstitutionId}",
                    TriggeredBy = new()
                    {
                        ServiceName = this.GetType().Namespace.Split('.')[0],
                        Action = "Create"
                    }
                };
        }

        private AdminDetailsForIntegrationEvent[] GetAdminDetails(ICollection<EducationalInstitutionAdmin> admins)
        {
            var adminsDetails = new AdminDetailsForIntegrationEvent[admins.Count];
            var index = 0;
            foreach (var admin in admins)
            {
                adminsDetails[index] = new()
                {
                    DetailedMessage = $"{UserPermissions.All.Split('.')[^1]} permission has been granted to you for the Educational Institution accessible at x",
                    Identity = admin.Id,
                    Permissions = admin.Permissions
                };
            }

            return adminsDetails;
        }
    }
}