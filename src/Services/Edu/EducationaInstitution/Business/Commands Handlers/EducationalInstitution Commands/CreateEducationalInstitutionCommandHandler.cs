using EducationaInstitutionAPI.Data;
using EducationaInstitutionAPI.Data.Helpers.Queries_and_Commands_Results.Commands_Results;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Commands;
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
    /// Defines a method that handles the creation and insertion of an Educational Institution into the database
    /// </summary>
    public class CreateEducationalInstitutionCommandHandler : IRequestHandler<DTOEducationalInstitutionCreateCommand, Response<CreateEducationalInstitutionCommandResult>>
    {
        /// <summary>
        /// Outputs to a file information about the state of the machine when an error/exception occurs during an operation
        /// </summary>
        private readonly ILogger<CreateEducationalInstitutionCommandHandler> logger;
        private readonly IEducationalInstitutionRepository eduRepository;

        public CreateEducationalInstitutionCommandHandler(ILogger<CreateEducationalInstitutionCommandHandler> logger, IEducationalInstitutionRepository eduRepository)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.eduRepository = eduRepository ?? throw new ArgumentNullException(nameof(eduRepository));
        }

        /// <summary>
        /// Tries to create and save to the database a new Educational Institution entity
        /// </summary>
        /// <param name="request">Contains Educational Institution data that is to be added to the database</param>
        /// <param name="cancellationToken">Cancels the operation ________</param>
        /// <returns>The response object along with information about the events that occurred during the request operation</returns>
        public async Task<Response<CreateEducationalInstitutionCommandResult>> Handle(DTOEducationalInstitutionCreateCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                EduInstitution newEducationalInstitution = new(request.Name, request.Description, request.LocationID, request.BuildingID);
                await eduRepository.Create(newEducationalInstitution, cancellationToken);

                return new()
                {
                    ResponseObject = new() { EduInstitutionID = newEducationalInstitution.EduInstitutionID },
                    OperationStatus = true,
                    StatusCode = HttpStatusCode.Created,
                    Message = string.Empty
                };
            }
            catch (Exception e)
            {
                logger.LogError("Could not create an Educational Institution with data: {0}, using {1}'s method: {2}, error details => {3}", request.ToString(), eduRepository.GetType(), nameof(eduRepository.Create), e.Message);
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