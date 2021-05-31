using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Queries;
using EducationalInstitutionAPI.Repositories.Query_Repository;
using EducationalInstitutionAPI.Unit_of_Work.Query_Unit_of_Work;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Business.Queries_Handlers
{
    public class GetAllEducationalInstitutionsByLocationQueryHandler : HandlerBase<GetAllEducationalInstitutionsByLocationQueryHandler>,
                                                                       IRequestHandler<DTOEducationalInstitutionsByLocationQuery, Response<GetAllEducationalInstitutionsByLocationQueryResult>>
    {
        private readonly IUnitOfWorkForQueries unitOfWork;

        /// <exception cref="ArgumentNullException"/>
        public GetAllEducationalInstitutionsByLocationQueryHandler(IUnitOfWorkForQueries unitOfWork, ILogger<GetAllEducationalInstitutionsByLocationQueryHandler> logger) : base(logger)
            => this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        /// <summary>
        /// Tries to get an <see cref="EducationalInstitution"/> by LocationID
        /// </summary>
        /// <param name="request">Contains the data necessary to get an <see cref="EducationalInstitution"/></param>
        /// <param name="cancellationToken">Cancels the operation _________</param>
        /// <returns>
        /// An <see cref="Response{ResponseType}">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.OK">Ok</see> if operation is successful</item>
        /// <item><see cref="HttpStatusCode.NotFound">NotFound</see> if no <see cref="EducationalInstitution"/> has been found for the provided id</item>
        /// <item><see cref="HttpStatusCode.InternalServerError">InternalServerError</see> if the entity could not be inserted into the database</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public async Task<Response<GetAllEducationalInstitutionsByLocationQueryResult>> Handle(DTOEducationalInstitutionsByLocationQuery request, CancellationToken cancellationToken = default)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                var educationalInstitutions = await unitOfWork.UsingEducationalInstitutionQueryRepository()
                                                        .GetAllByLocationAsync(request.LocationID, cancellationToken);

                if (educationalInstitutions is null || educationalInstitutions.EducationalInstitutions.Count == 0)
                    return new()
                    {
                        Data = null,
                        OperationStatus = false,
                        StatusCode = HttpStatusCode.NotFound,
                        Message = $"No Educational Institution with the following LocationID: {request.LocationID} has been found!"
                    };

                return new()
                {
                    Data = educationalInstitutions,
                    OperationStatus = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = string.Empty
                };
            }
            catch (Exception e)
            {
                return HandleException<Response<GetAllEducationalInstitutionsByLocationQueryResult>>(
                            error_message: "Could not find an Educational Institution with LocationID: {0}, using {1} with {2}'s method: {3}, error details => {4}",
                            response_message: "An error occurred while searching for the Educational Institution with the following LocationID: {0}!",
                            request.LocationID,
                            unitOfWork.GetType(),
                            unitOfWork.UsingEducationalInstitutionQueryRepository().GetType(),
                            nameof(IEducationalInstitutionQueryRepository.GetAllByLocationAsync),
                            e.Message);
            }
        }
    }
}