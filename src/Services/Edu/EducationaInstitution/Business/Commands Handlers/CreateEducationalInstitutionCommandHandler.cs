﻿using EducationaInstitutionAPI.Data;
using EducationaInstitutionAPI.Data.Helpers.Queries_and_Commands_Results.Commands_Results;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Commands;
using EducationaInstitutionAPI.Repositories;
using EducationaInstitutionAPI.Unit_of_Work;
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
    /// Defines a method that handles the creation of an <see cref="EducationalInstitution"/> entity
    /// </summary>
    public class CreateEducationalInstitutionCommandHandler : IRequestHandler<DTOEducationalInstitutionCreateCommand, Response<EducationalInstitutionCommandResult>>
    {
        /// <summary>
        /// Outputs to a file information about the state of the machine when an error/exception occurs during an operation
        /// </summary>
        private readonly ILogger<CreateEducationalInstitutionCommandHandler> logger;

        private readonly IUnitOfWork unitOfWork;

        public CreateEducationalInstitutionCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateEducationalInstitutionCommandHandler> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Tries to create and save to the database a new <see cref="EducationalInstitution"/> entity
        /// </summary>
        /// <param name="request">Contains <see cref="EducationalInstitution"/> data that is to be added to the database</param>
        /// <param name="cancellationToken">Cancels the operation ________</param>
        /// <returns>
        /// An <see cref="Response{ResponseType}">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.Created">if operation is successful</see></item>
        /// <item><see cref="HttpStatusCode.InternalServerError">if the entity could not be inserted into the database</see></item>
        /// </list>
        /// </returns>
        public async Task<Response<EducationalInstitutionCommandResult>> Handle(DTOEducationalInstitutionCreateCommand request, CancellationToken cancellationToken = default)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                using (unitOfWork)
                {
                    EducationalInstitution newEducationalInstitution = new(request.Name, request.Description, request.LocationID, request.BuildingsIDs);
                    await unitOfWork.UsingEducationalInstitutionRepository().CreateAsync(newEducationalInstitution, cancellationToken);
                    await unitOfWork.SaveChangesAsync(cancellationToken);

                    return new()
                    {
                        ResponseObject = new() { EduInstitutionID = newEducationalInstitution.EduInstitutionID },
                        OperationStatus = true,
                        StatusCode = HttpStatusCode.Created,
                        Message = string.Empty
                    };
                }
            }
            catch (Exception e)
            {
                logger.LogError("Could not create an Educational Institution with data: {0}, using {1}'s method: {2}, error details => {3}", request.ToString(), unitOfWork.GetType(), nameof(IEducationalInstitutionRepository.CreateAsync), e.Message);
                return new()
                {
                    ResponseObject = null,
                    OperationStatus = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "An error occurred while creating the Educational Institution with the given data!"
                };
            }
        }
    }
}