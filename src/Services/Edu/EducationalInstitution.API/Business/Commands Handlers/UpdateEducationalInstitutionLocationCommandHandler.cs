﻿using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Commands_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Repositories.EducationalInstitutionRepository;
using EducationalInstitutionAPI.Unit_of_Work;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Business.Commands_Handlers
{
    /// <summary>
    /// Defines a method that handles the update of an <see cref="EducationalInstitution"/>'s location and/or buildings
    /// </summary>
    public class UpdateEducationalInstitutionLocationCommandHandler : IRequestHandler<DTOEducationalInstitutionLocationUpdateCommand, Response>
    {
        /// <summary>
        /// Outputs to a file information about the state of the machine when an error/exception occurs during an operation
        /// </summary>
        private readonly ILogger<UpdateEducationalInstitutionLocationCommandHandler> logger;

        private readonly IUnitOfWork unitOfWork;

        public UpdateEducationalInstitutionLocationCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateEducationalInstitutionLocationCommandHandler> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Tries to update the locationID and/or the BuildingsIDs of an <see cref="EducationalInstitution"/> entity
        /// </summary>
        /// <param name="request">Contains <see cref="EducationalInstitution.LocationID"/> and/or <see cref="EducationalInstitution.Buildings"/></param>
        /// <param name="cancellationToken">Cancels the operation ____________</param>
        /// <returns>
        /// An <see cref="Response{ResponseType}">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.NoContent">if operation is successful</see></item>
        /// <item><see cref="HttpStatusCode.NotFound">if no <see cref="EducationalInstitution"/> has been found for the provided id</see></item>
        /// <item><see cref="HttpStatusCode.InternalServerError">if the entity could not be updated</see></item>
        /// </list>
        /// </returns>
        public async Task<Response> Handle(DTOEducationalInstitutionLocationUpdateCommand request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                using (unitOfWork)
                {
                    if (!await IsEducationalInstitutionUpdatedAndSavedToDatabase(request, cancellationToken))
                        return new()
                        {
                            OperationStatus = false,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = $"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!"
                        };
                    else
                        return new()
                        {
                            OperationStatus = true,
                            StatusCode = HttpStatusCode.NoContent,
                            Message = string.Empty
                        };
                }
            }
            catch (Exception e)
            {
                logger.LogError(
                   "Could not update the Educational Institution with the given data: {0}, using {1} with {2}'s method: {3}, error details => {4}",
                   JsonConvert.SerializeObject(request),
                   unitOfWork.GetType(),
                   unitOfWork.UsingEducationalInstitutionRepository().GetType(),
                   GetNameOfRepositoryMethodThatWasCalledInHandler(request),
                   e.Message
                   );

                return new()
                {
                    OperationStatus = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while updating the Educational Institution with the following ID: {request.EducationalInstitutionID}!"
                };
            }
        }

        private async Task<bool> IsEducationalInstitutionUpdatedAndSavedToDatabase(DTOEducationalInstitutionLocationUpdateCommand request, CancellationToken cancellationToken)
        {
            bool isEntityUpdated;
            if (request.UpdateBuildings && request.UpdateLocation)
                isEntityUpdated = await unitOfWork.UsingEducationalInstitutionRepository()
                                                    .UpdateEntireLocationAsync(request.EducationalInstitutionID, request.LocationID, request.BuildingsIDs, cancellationToken);
            else
            if (request.UpdateLocation)
                isEntityUpdated = await unitOfWork.UsingEducationalInstitutionRepository()
                                                    .UpdateLocationAsync(request.EducationalInstitutionID, request.LocationID, cancellationToken);
            else
                isEntityUpdated = await unitOfWork.UsingEducationalInstitutionRepository()
                                                    .UpdateBuildingsAsync(request.EducationalInstitutionID, request.BuildingsIDs, cancellationToken);

            if (isEntityUpdated)
                await unitOfWork.SaveChangesAsync(cancellationToken);

            return isEntityUpdated;
        }

        private static string GetNameOfRepositoryMethodThatWasCalledInHandler(DTOEducationalInstitutionLocationUpdateCommand request)
        {
            if (request.UpdateBuildings && request.UpdateLocation)
                return nameof(IEducationalInstitutionRepository.UpdateEntireLocationAsync);
            else
            if (request.UpdateLocation)
                return nameof(IEducationalInstitutionRepository.UpdateLocationAsync);
            else
                return nameof(IEducationalInstitutionRepository.UpdateBuildingsAsync);
        }
    }
}