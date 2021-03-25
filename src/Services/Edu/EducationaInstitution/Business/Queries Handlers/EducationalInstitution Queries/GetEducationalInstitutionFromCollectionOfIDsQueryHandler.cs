using EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Queries;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using EducationaInstitutionAPI.Repositories;
using EducationaInstitutionAPI.Utils;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EducationaInstitutionAPI.Business.Queries.OnEducationalInstitution
{
    /// <summary>
    /// Defines a method that handles the operation of getting Educational Institutions based on a collection of ids
    /// </summary>
    public class GetEducationalInstitutionFromCollectionOfIDsQueryHandler : IRequestHandler<DTOEducationalInstitutionsFromCollectionOfIDsQuery, Response<ICollection<GetEducationalInstitutionQueryResult>>>
    {
        /// <summary>
        /// Outputs to a file information about the state of the machine when an error/exception occurs during an operation
        /// </summary>
        private readonly ILogger<GetEducationalInstitutionFromCollectionOfIDsQueryHandler> logger;

        private readonly IEducationalInstitutionRepository eduRepository;

        public GetEducationalInstitutionFromCollectionOfIDsQueryHandler(IEducationalInstitutionRepository eduRepository, ILogger<GetEducationalInstitutionFromCollectionOfIDsQueryHandler> logger)
        {
            this.eduRepository = eduRepository;
            this.logger = logger;
        }

        /// <summary>
        /// Tries to get Educational Institutions based on the given collection of ids
        /// </summary>
        /// <param name="request">Contains the data necessary to get the Educational Institutions</param>
        /// <param name="cancellationToken">Cancels the operation ________</param>
        /// <returns>A collection of response objects along with information about the events that occurred during the request operation</returns>
        public async Task<Response<ICollection<GetEducationalInstitutionQueryResult>>> Handle(DTOEducationalInstitutionsFromCollectionOfIDsQuery request, CancellationToken cancellationToken)
        {
            ICollection<GetEducationalInstitutionQueryResult> educationInstitutions = default;
            try
            {
                educationInstitutions = await eduRepository.GetFromCollectionOfIDs(request.EducationalInstitutionsIDs, cancellationToken);
                if (educationInstitutions == null || educationInstitutions.Count == 0)
                    return new Response<ICollection<GetEducationalInstitutionQueryResult>>()
                    {
                        ResponseObject = null,
                        OperationStatus = true,
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "Could not find the Education Institutions from the list of given IDs!"
                    };

                return new Response<ICollection<GetEducationalInstitutionQueryResult>>()
                {
                    ResponseObject = educationInstitutions,
                    OperationStatus = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = string.Empty
                };
            }
            catch (Exception e)
            {
                if (educationInstitutions.Count > 0)
                {
                    var eduInstitutionsIDsNotFound = request.EducationalInstitutionsIDs.Except(educationInstitutions.Select(eduI => eduI.EduInstitutionID).ToList());
                    logger.LogError("Could not retrieve all Educational Institutions with IDs: {0}, using {1}'s method {2}, error details => {3}", request.EducationalInstitutionsIDs.Except(educationInstitutions.Select(eduI => eduI.EduInstitutionID).ToList()), eduRepository.GetType(), nameof(eduRepository.GetFromCollectionOfIDs), e.Message);
                    return new Response<ICollection<GetEducationalInstitutionQueryResult>>()
                    {
                        ResponseObject = educationInstitutions,
                        OperationStatus = true,
                        StatusCode = HttpStatusCode.OK,
                        Message = $"The following Educational Institutions' IDs have not been found: {eduInstitutionsIDsNotFound}"
                    };
                }
                else
                {
                    logger.LogError("Could not retrieve any Educational Institutions from the following list of IDs:{0}, using {1}'s method {2}, error details => {3}", request.EducationalInstitutionsIDs, eduRepository.GetType(), nameof(eduRepository.GetFromCollectionOfIDs), e.Message);
                    return new Response<ICollection<GetEducationalInstitutionQueryResult>>()
                    {
                        ResponseObject = null,
                        OperationStatus = true,
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "Could not find any Educational Institution from the list of IDs given!"
                    };
                }
            }
        }
    }
}