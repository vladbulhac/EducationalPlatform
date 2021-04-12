using EducationaInstitutionAPI.Data;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Queries;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using EducationaInstitutionAPI.Repositories;
using EducationaInstitutionAPI.Unit_of_Work;
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
    /// Defines a method that handles the operation of getting <see cref="EduInstitution"/>s based on a collection of ids
    /// </summary>
    public class GetAllEducationalInstitutionsFromCollectionOfIDsQueryHandler : IRequestHandler<DTOEducationalInstitutionsFromCollectionOfIDsQuery, Response<ICollection<GetEducationalInstitutionQueryResult>>>
    {
        /// <summary>
        /// Outputs to a file information about the state of the machine when an error/exception occurs during an operation
        /// </summary>
        private readonly ILogger<GetAllEducationalInstitutionsFromCollectionOfIDsQueryHandler> logger;

        private readonly IUnitOfWork unitOfWork;

        public GetAllEducationalInstitutionsFromCollectionOfIDsQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAllEducationalInstitutionsFromCollectionOfIDsQueryHandler> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Tries to get <see cref="EduInstitution"/>s based on the given collection of ids
        /// </summary>
        /// <param name="request">Contains the data necessary to get the <see cref="EduInstitution"/>s</param>
        /// <param name="cancellationToken">Cancels the operation ________</param>
        /// <returns>
        /// An <see cref="Response{ResponseType}">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.OK">if operation is successful</see></item>
        /// <item><see cref="HttpStatusCode.MultiStatus">if not all <see cref="EduInstitution"/> have been found from the provided ids</see></item>
        /// <item><see cref="HttpStatusCode.NotFound">if no <see cref="EduInstitution"/> has been found from the provided ids</see></item>
        /// <item><see cref="HttpStatusCode.InternalServerError">if the entity could not be inserted into the database</see></item>
        /// </list>
        /// </returns>
        public async Task<Response<ICollection<GetEducationalInstitutionQueryResult>>> Handle(DTOEducationalInstitutionsFromCollectionOfIDsQuery request, CancellationToken cancellationToken)
        {
            ICollection<GetEducationalInstitutionQueryResult> educationInstitutions = default;
            try
            {
                using (unitOfWork)
                {
                    educationInstitutions = await unitOfWork.UsingEducationalInstitutionRepository()
                                                                .GetFromCollectionOfIDsAsync(request.EducationalInstitutionsIDs, cancellationToken);

                    if (educationInstitutions is null || educationInstitutions.Count is 0)
                        return new Response<ICollection<GetEducationalInstitutionQueryResult>>()
                        {
                            ResponseObject = null,
                            OperationStatus = true,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "Could not find the Educational Institutions from the list of given IDs!"
                        };

                    var httpStatusCode = HttpStatusCode.OK;
                    string message = string.Empty;
                    if (educationInstitutions.Count < request.EducationalInstitutionsIDs.Count)
                    {
                        httpStatusCode = HttpStatusCode.MultiStatus;
                        message = $"Could not retrieve all Educational Institutions with IDs: {request.EducationalInstitutionsIDs.Except(educationInstitutions.Select(eduI => eduI.EduInstitutionID).ToList())}";
                    }

                    return new Response<ICollection<GetEducationalInstitutionQueryResult>>()
                    {
                        ResponseObject = educationInstitutions,
                        OperationStatus = true,
                        StatusCode = httpStatusCode,
                        Message = message
                    };
                }
            }
            catch (Exception e)
            {
                if (educationInstitutions.Count > 0)
                {
                    var eduInstitutionsIDsNotFound = request.EducationalInstitutionsIDs.Except(educationInstitutions.Select(eduI => eduI.EduInstitutionID).ToList());
                    logger.LogError("Could not retrieve all Educational Institutions with IDs: {0}, using {1}'s method {2}, error details => {3}", request.EducationalInstitutionsIDs.Except(educationInstitutions.Select(eduI => eduI.EduInstitutionID).ToList()), unitOfWork.GetType(), nameof(IEducationalInstitutionRepository.GetFromCollectionOfIDsAsync), e.Message);
                    return new Response<ICollection<GetEducationalInstitutionQueryResult>>()
                    {
                        ResponseObject = educationInstitutions,
                        OperationStatus = true,
                        StatusCode = HttpStatusCode.MultiStatus,
                        Message = $"The following Educational Institutions' IDs have not been found: {eduInstitutionsIDsNotFound}"
                    };
                }
                else
                {
                    logger.LogError("Could not retrieve any Educational Institutions from the following list of IDs:{0}, using {1}'s method {2}, error details => {3}", request.EducationalInstitutionsIDs, unitOfWork.GetType(), nameof(unitOfWork.UsingEducationalInstitutionRepository), e.Message);
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