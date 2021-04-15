using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Queries;
using EducationalInstitutionAPI.Repositories.EducationalInstitutionRepository;
using EducationalInstitutionAPI.Unit_of_Work;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Business.Queries_Handlers
{
    /// <summary>
    /// Defines a method that handles the operation of getting <see cref="EducationalInstitution"/>s based on a collection of ids
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
        /// Tries to get <see cref="EducationalInstitution"/>s based on the given collection of ids
        /// </summary>
        /// <param name="request">Contains the data necessary to get the <see cref="EducationalInstitution"/>s</param>
        /// <param name="cancellationToken">Cancels the operation ________</param>
        /// <returns>
        /// An <see cref="Response{ResponseType}">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.OK">if operation is successful</see></item>
        /// <item><see cref="HttpStatusCode.MultiStatus">if not all <see cref="EducationalInstitution"/> have been found from the provided ids</see></item>
        /// <item><see cref="HttpStatusCode.NotFound">if no <see cref="EducationalInstitution"/> has been found from the provided ids</see></item>
        /// <item><see cref="HttpStatusCode.InternalServerError">if the entity could not be inserted into the database</see></item>
        /// </list>
        /// </returns>
        public async Task<Response<ICollection<GetEducationalInstitutionQueryResult>>> Handle(DTOEducationalInstitutionsFromCollectionOfIDsQuery request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                using (unitOfWork)
                {
                    var educationalInstitutions = await unitOfWork.UsingEducationalInstitutionRepository()
                                                                .GetFromCollectionOfIDsAsync(request.EducationalInstitutionsIDs, cancellationToken);

                    if (educationalInstitutions is null || educationalInstitutions.Count is 0)
                        return new()
                        {
                            ResponseObject = educationalInstitutions,
                            OperationStatus = true,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "Could not find the Educational Institutions from the list of given IDs!"
                        };
                    else
                    if (educationalInstitutions.Count < request.EducationalInstitutionsIDs.Count)
                        return new()
                        {
                            ResponseObject = educationalInstitutions,
                            OperationStatus = true,
                            StatusCode = HttpStatusCode.MultiStatus,
                            Message = $"Could not retrieve Educational Institutions with the following IDs: {JsonConvert.SerializeObject(request.EducationalInstitutionsIDs.Except(educationalInstitutions.Select(eduI => eduI.EducationalInstitutionID).ToList()))}"
                        };
                    else
                        return new()
                        {
                            ResponseObject = educationalInstitutions,
                            OperationStatus = true,
                            StatusCode = HttpStatusCode.OK,
                            Message = string.Empty
                        };
                }
            }
            catch (Exception e)
            {
                logger.LogError(
                    "Could not retrieve any Educational Institutions from the following list of IDs: {0}, using {1} with {2}'s method {3}, error details => {4}",
                    JsonConvert.SerializeObject(request.EducationalInstitutionsIDs),
                    unitOfWork.GetType(),
                    unitOfWork.UsingEducationalInstitutionRepository().GetType(),
                    nameof(IEducationalInstitutionRepository.GetFromCollectionOfIDsAsync),
                    e.Message
                    );

                return new()
                {
                    ResponseObject = null,
                    OperationStatus = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Could not find any Educational Institution from the list of IDs given!"
                };
            }
        }
    }
}