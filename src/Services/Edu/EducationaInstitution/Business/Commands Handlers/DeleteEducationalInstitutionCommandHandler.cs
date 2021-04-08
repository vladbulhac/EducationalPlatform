﻿using EducationaInstitutionAPI.Data;
using EducationaInstitutionAPI.Data.Queries_and_Commands_Results.Commands_Results;
using EducationaInstitutionAPI.DTOs.Commands;
using EducationaInstitutionAPI.Unit_of_Work;
using EducationaInstitutionAPI.Utils;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EducationaInstitutionAPI.Business.Commands_Handlers
{
    /// <summary>
    /// Defines a method that handles the deletion of an <see cref="EduInstitution"/> entity
    /// </summary>
    public class DeleteEducationalInstitutionCommandHandler : IRequestHandler<DTOEducationalInstitutionDeleteCommand, Response<DeleteEducationalInstitutionCommandResult>>
    {
        /// <summary>
        /// Outputs to a file information about the state of the machine when an error/exception occurs during an operation
        /// </summary>
        private readonly ILogger<DeleteEducationalInstitutionCommandHandler> logger;

        private readonly IUnitOfWork unitOfWork;

        public DeleteEducationalInstitutionCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteEducationalInstitutionCommandHandler> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Schedules an <see cref="EduInstitution"/> entity for deletion
        /// </summary>
        /// <param name="request">Contains a <see cref="EduInstitution.EduInstitutionID"/></param>
        /// <param name="cancellationToken">Cancels the operation ________</param>
        /// <returns>
        /// An <see cref="Response{ResponseType}">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.Accepted">if the entity has been scheduled for deletion</see></item>
        /// <item><see cref="HttpStatusCode.NotFound">if the <see cref="EduInstitution"/> has not been found</see></item>
        /// <item><see cref="HttpStatusCode.InternalServerError">if the schedule for deletion could not be saved in the database</see></item>
        /// </list>
        /// </returns>
        public async Task<Response<DeleteEducationalInstitutionCommandResult>> Handle(DTOEducationalInstitutionDeleteCommand request, CancellationToken cancellationToken = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                using (unitOfWork)
                {
                    var educationalInstitution = await unitOfWork.UseEducationalInstitutionRepository()
                                                                 .GetEntityByIDAsync(request.EduInstitutionID, cancellationToken);

                    if (educationalInstitution == null)
                    {
                        return new()
                        {
                            ResponseObject = null,
                            OperationStatus = false,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = $"Educational Institution with the following ID: {request.EduInstitutionID} has not been found!"
                        };
                    }

                    educationalInstitution.EntityAccess.ScheduleForDeletion();
                    await unitOfWork.SaveChangesAsync(cancellationToken);

                    return new()
                    {
                        ResponseObject = new()
                        {
                            EduInstitutionID = educationalInstitution.EduInstitutionID,
                            AccessInformation = educationalInstitution.EntityAccess
                        },
                        OperationStatus = true,
                        StatusCode = HttpStatusCode.Accepted,
                        Message = string.Empty
                    };
                }
            }
            catch (Exception e)
            {
                logger.LogError(
                    "Could not schedule for deletion the Educational Institution with ID: {0}, using {1}'s method: {2} with {3}, error details => {4}",
                    request.EduInstitutionID, unitOfWork.GetType(), unitOfWork.UseEducationalInstitutionRepository().GetType(), nameof(request.EduInstitutionID), e.Message
                    );

                return new()
                {
                    ResponseObject = null,
                    OperationStatus = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while scheduling for deletion the Educational Institution with the following ID: {request.EduInstitutionID}!"
                };
            }
        }
    }
}