using EducationaInstitutionAPI.DTOs.EducationalInstitution.In;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using EducationaInstitutionAPI.Repositories;
using EducationaInstitutionAPI.Utils;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EducationaInstitutionAPI.Business.Queries_Handlers.Queries_on_EducationalInstitution
{
    /// <summary>
    /// Defines a method that handles the operation of getting an Educational Institution by LocationID
    /// </summary>
    public class GetEducationalInstitutionByLocationQueryHandler : IRequestHandler<DTOEducationalInstitutionByLocationQuery, Response<GetEducationalInstitutionByLocationQueryResult>>
    {
        /// <summary>
        /// Outputs to a file information about the state of the machine when an error/exception occurs during an operation
        /// </summary>
        private readonly ILogger<GetEducationalInstitutionByLocationQueryHandler> logger;
        private readonly IEducationalInstitutionRepository eduRepository;

        public GetEducationalInstitutionByLocationQueryHandler(IEducationalInstitutionRepository eduRepository, ILogger<GetEducationalInstitutionByLocationQueryHandler> logger)
        {
            this.eduRepository = eduRepository ?? throw new ArgumentNullException(nameof(eduRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Tries to get an Educational Institution by LocationID
        /// </summary>
        /// <param name="request">Contains the data necessary to get an Educational Institution</param>
        /// <param name="cancellationToken">Cancels the operation _________</param>
        /// <returns>The response object along with information about the events that occurred during the request operation</returns>
        public async Task<Response<GetEducationalInstitutionByLocationQueryResult>> Handle(DTOEducationalInstitutionByLocationQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                var eduInstitution = await eduRepository.GetByLocation(request.LocationID, cancellationToken);

                if (eduInstitution == null)
                    return new()
                    {
                        ResponseObject = null,
                        OperationStatus = false,
                        StatusCode = HttpStatusCode.NotFound,
                        Message = $"No Educational Institution with the following LocationID: {request.LocationID} has been found!"
                    };

                return new()
                {
                    ResponseObject = eduInstitution,
                    OperationStatus = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = string.Empty
                };
            }
            catch (Exception e)
            {
                logger.LogError("Could not find an Educational Institution with LocationID: {0}, using {1}'s method: {2}, error details => {3}", request.LocationID, eduRepository.GetType(), nameof(eduRepository.GetByLocation), e.Message);
                return new()
                {
                    ResponseObject = null,
                    OperationStatus = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = $"An error occurred while searching for the Educational Institution with the following LocationID: {request.LocationID}!"
                };
            }
        }
    }
}