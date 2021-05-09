using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Queries;
using EducationalInstitutionAPI.Repositories.EducationalInstitution_Repository;
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
    public class GetAllEducationalInstitutionsFromCollectionOfIDsQueryHandler : IRequestHandler<DTOEducationalInstitutionsFromCollectionOfIDsQuery, Response<ICollection<GetEducationalInstitutionQueryResult>>>
    {
        /// <summary>
        /// Outputs to a file information about the state of the machine when an error/exception occurs during an operation
        /// </summary>
        private readonly ILogger<GetAllEducationalInstitutionsFromCollectionOfIDsQueryHandler> logger;

        private readonly IUnitOfWork unitOfWork;

        /// <exception cref="ArgumentNullException"/>
        public GetAllEducationalInstitutionsFromCollectionOfIDsQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAllEducationalInstitutionsFromCollectionOfIDsQueryHandler> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Tries to get <see cref="EducationalInstitution"/>s based on the given collection of ids
        /// </summary>
        /// <param name="cancellationToken">Cancels the operation ________</param>
        /// <returns>
        /// An <see cref="Response{TData}">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.OK">Ok</see> if operation is successful</item>
        /// <item><see cref="HttpStatusCode.MultiStatus">MultiStatus</see> if not all <see cref="EducationalInstitution"/> have been found from the provided ids</item>
        /// <item><see cref="HttpStatusCode.NotFound">NotFound</see> if no <see cref="EducationalInstitution"/> has been found from the provided ids</item>
        /// <item><see cref="HttpStatusCode.InternalServerError">InternalServerError</see> if the entity could not be inserted into the database</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
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
                            Data = educationalInstitutions,
                            OperationStatus = true,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "Could not find the Educational Institutions from the list of given IDs!"
                        };

                    if (educationalInstitutions.Count < request.EducationalInstitutionsIDs.Count)
                        return new()
                        {
                            Data = educationalInstitutions,
                            OperationStatus = true,
                            StatusCode = HttpStatusCode.MultiStatus,
                            Message = $"Could not retrieve Educational Institutions with the following IDs: {JsonConvert.SerializeObject(request.EducationalInstitutionsIDs.Except(educationalInstitutions.Select(eduI => eduI.EducationalInstitutionID).ToList()))}"
                        };

                    return new()
                    {
                        Data = educationalInstitutions,
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
                    Data = null,
                    OperationStatus = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Could not find any Educational Institution from the list of IDs given!"
                };
            }
        }
    }
}