﻿using EducationaInstitutionAPI.Data;
using EducationaInstitutionAPI.Data.Helpers.Queries_and_Commands_Results.Commands_Results;
using EducationaInstitutionAPI.DTOs.Commands;
using EducationaInstitutionAPI.Repositories;
using EducationaInstitutionAPI.Utils;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EducationaInstitutionAPI.Business.Commands_Handlers.EducationalInstitution_Commands
{
    /// <summary>
    /// Defines a method that handles the creation of an <see cref="EduInstitution"/> entity that has a parent institution
    /// </summary>
    public class CreateEducationalInstitutionWithParentCommandHandler : IRequestHandler<DTOEducationalInstitutionWithParentCreateCommand, Response<EducationalInstitutionCommandResult>>
    {
        /// <summary>
        /// Outputs to a file information about the state of the machine when an error/exception occurs during an operation
        /// </summary>
        private readonly ILogger<CreateEducationalInstitutionWithParentCommandHandler> logger;

        private readonly IEducationalInstitutionRepository eduRepository;

        public CreateEducationalInstitutionWithParentCommandHandler(IEducationalInstitutionRepository eduRepository, ILogger<CreateEducationalInstitutionWithParentCommandHandler> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.eduRepository = eduRepository ?? throw new ArgumentNullException(nameof(eduRepository));
        }

        /// <summary>
        /// Tries to create and save to the database a new <see cref="EduInstitution"/> entity
        /// </summary>
        /// <param name="request">Contains <see cref="EduInstitution"/> data that is to be added to the database</param>
        /// <param name="cancellationToken">Cancels the operation ________</param>
        /// <returns>
        /// An <see cref="Response{ResponseType}">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.Created">if operation is successful</see></item>
        /// <item><see cref="HttpStatusCode.MultiStatus">if the <see cref="EduInstitution">Parent Institution</see> has not been found</see></item>
        /// <item><see cref="HttpStatusCode.InternalServerError">if the entity could not be inserted into the database</see></item>
        /// </list>
        /// </returns>
        public async Task<Response<EducationalInstitutionCommandResult>> Handle(DTOEducationalInstitutionWithParentCreateCommand request, CancellationToken cancellationToken = default)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                var parentInstitution = await eduRepository.GetEntityByIDAsync(request.ParentInstitutionID, cancellationToken);

                EduInstitution newEducationalInstitution = new(request.Name, request.Description, request.LocationID, request.BuildingsIDs, parentInstitution);
                await eduRepository.CreateAsync(newEducationalInstitution, cancellationToken);

                var httpStatusCode = HttpStatusCode.Created;
                string responseMessage = string.Empty;
                if (parentInstitution is null)
                {
                    httpStatusCode = HttpStatusCode.MultiStatus;
                    responseMessage = $"The Educational Institution has been successfully created but the Parent Institution with the following ID: {request.ParentInstitutionID} has not been found!";
                }

                return new()
                {
                    ResponseObject = new() { EduInstitutionID = newEducationalInstitution.EduInstitutionID },
                    OperationStatus = true,
                    StatusCode = httpStatusCode,
                    Message = responseMessage
                };
            }
            catch (Exception e)
            {
                logger.LogError("Could not create an Educational Institution with data: {0}, using {1}'s method: {2}, error details => {3}", request.ToString(), eduRepository.GetType(), nameof(eduRepository.CreateAsync), e.Message);
                return new()
                {
                    ResponseObject = null,
                    OperationStatus = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while creating the Educational Institution with data: {request}!"
                };
            }
        }
    }
}