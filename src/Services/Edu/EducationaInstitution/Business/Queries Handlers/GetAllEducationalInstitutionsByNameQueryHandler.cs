using EducationaInstitutionAPI.Data;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.In;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using EducationaInstitutionAPI.Repositories;
using EducationaInstitutionAPI.Unit_of_Work;
using EducationaInstitutionAPI.Utils;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EducationaInstitutionAPI.Business.Queries.OnEducationalInstitution
{
    /// <summary>
    /// Defines a method that handles the operation of getting <see cref="EducationalInstitution"/>s whose Name's contain a certain string
    /// </summary>
    public class GetAllEducationalInstitutionsByNameQueryHandler : IRequestHandler<DTOEducationalInstitutionsByNameQuery, Response<ICollection<GetEducationalInstitutionQueryResult>>>
    {
        /// <summary>
        /// Outputs to a file information about the state of the machine when an error/exception occurs during an operation
        /// </summary>
        private readonly ILogger<GetAllEducationalInstitutionsByNameQueryHandler> logger;

        private readonly IUnitOfWork unitOfWork;

        public GetAllEducationalInstitutionsByNameQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAllEducationalInstitutionsByNameQueryHandler> logger)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Tries to get one or more <see cref="EducationalInstitution"/>s whose Name's contain a given string
        /// </summary>
        /// <param name="request">Contains the data necessary to get the <see cref="EducationalInstitution"/>s</param>
        /// <param name="cancellationToken">Cancels the operation ______</param>
        /// <returns>
        /// An <see cref="Response{ResponseType}">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.OK">if operation is successful</see></item>
        /// <item><see cref="HttpStatusCode.NotFound">if no <see cref="EducationalInstitution"/> has been found for the provided id</see></item>
        /// <item><see cref="HttpStatusCode.InternalServerError">if the entity could not be inserted into the database</see></item>
        /// </list>
        /// </returns>
        public async Task<Response<ICollection<GetEducationalInstitutionQueryResult>>> Handle(DTOEducationalInstitutionsByNameQuery request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                using (unitOfWork)
                {
                    var eduInstitutions = await unitOfWork.UsingEducationalInstitutionRepository()
                                                            .GetAllLikeNameAsync(request.Name, request.OffsetValue, request.ResultsCount, cancellationToken);

                    if (eduInstitutions is null)
                        return new()
                        {
                            ResponseObject = null,
                            OperationStatus = false,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = $"Could not find any Educational Institution with a name like: {request.Name}!"
                        };

                    return new()
                    {
                        ResponseObject = eduInstitutions,
                        OperationStatus = true,
                        StatusCode = HttpStatusCode.OK,
                        Message = string.Empty
                    };
                }
            }
            catch (Exception e)
            {
                logger.LogError("Could not find any Educational Institution with a name like: {0}, using {1}'s method: {2}, error details => {3}", request.Name, unitOfWork.GetType(), nameof(IEducationalInstitutionRepository.GetAllLikeNameAsync), e.Message);
                return new()
                {
                    ResponseObject = null,
                    OperationStatus = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = $"An error occured while searching for any Educational Institution with a name like: {request.Name}!"
                };
            }
        }
    }
}