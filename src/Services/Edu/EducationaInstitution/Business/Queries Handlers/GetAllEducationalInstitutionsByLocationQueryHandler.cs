using EducationaInstitutionAPI.Data;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.In;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using EducationaInstitutionAPI.Repositories;
using EducationaInstitutionAPI.Unit_of_Work;
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
    /// Defines a method that handles the operation of getting an <see cref="EducationalInstitution"/> by LocationID
    /// </summary>
    public class GetAllEducationalInstitutionsByLocationQueryHandler : IRequestHandler<DTOEducationalInstitutionByLocationQuery, Response<GetAllEducationalInstitutionsByLocationQueryResult>>
    {
        /// <summary>
        /// Outputs to a file information about the state of the machine when an error/exception occurs during an operation
        /// </summary>
        private readonly ILogger<GetAllEducationalInstitutionsByLocationQueryHandler> logger;

        private readonly IUnitOfWork unitOfWork;

        public GetAllEducationalInstitutionsByLocationQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAllEducationalInstitutionsByLocationQueryHandler> logger)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Tries to get an <see cref="EducationalInstitution"/> by LocationID
        /// </summary>
        /// <param name="request">Contains the data necessary to get an <see cref="EducationalInstitution"/></param>
        /// <param name="cancellationToken">Cancels the operation _________</param>
        /// <returns>
        /// An <see cref="Response{ResponseType}">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.OK">if operation is successful</see></item>
        /// <item><see cref="HttpStatusCode.NotFound">if no <see cref="EducationalInstitution"/> has been found for the provided id</see></item>
        /// <item><see cref="HttpStatusCode.InternalServerError">if the entity could not be inserted into the database</see></item>
        /// </list>
        /// </returns>
        public async Task<Response<GetAllEducationalInstitutionsByLocationQueryResult>> Handle(DTOEducationalInstitutionByLocationQuery request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                using (unitOfWork)
                {
                    var eduInstitution = await unitOfWork.UsingEducationalInstitutionRepository()
                                                            .GetAllByLocationAsync(request.LocationID, cancellationToken);

                    if (eduInstitution is null)
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
            }
            catch (Exception e)
            {
                logger.LogError("Could not find an Educational Institution with LocationID: {0}, using {1}'s method: {2}, error details => {3}", request.LocationID, unitOfWork.GetType(), nameof(IEducationalInstitutionRepository.GetAllByLocationAsync), e.Message);
                return new()
                {
                    ResponseObject = null,
                    OperationStatus = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while searching for the Educational Institution with the following LocationID: {request.LocationID}!"
                };
            }
        }
    }
}