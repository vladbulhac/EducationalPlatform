﻿using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Queries;
using EducationalInstitutionAPI.Repositories.EducationalInstitution_Repository.Query_Repository;
using EducationalInstitutionAPI.Unit_of_Work.Query_Unit_of_Work;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Business.Queries_Handlers
{
    public class GetEducationalInstitutionByIDQueryHandler : HandlerBase<GetEducationalInstitutionByIDQueryHandler>,
                                                             IRequestHandler<DTOEducationalInstitutionByIDQuery, Response<GetEducationalInstitutionByIDQueryResult>>
    {
        private readonly IUnitOfWorkForQueries unitOfWork;

        /// <exception cref="ArgumentNullException"/>
        public GetEducationalInstitutionByIDQueryHandler(IUnitOfWorkForQueries unitOfWork, ILogger<GetEducationalInstitutionByIDQueryHandler> logger) : base(logger)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Tries to get an <see cref="EducationalInstitution"/> by EducationalInstitutionID
        /// </summary>
        /// <param name="cancellationToken">Cancels the operation ______</param>
        /// <returns>
        /// An <see cref="Response{TData}">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.OK">Ok</see> if operation is successful</item>
        /// <item><see cref="HttpStatusCode.NotFound">NotFound</see> if no <see cref="EducationalInstitution"/> has been found for the provided id</item>
        /// <item><see cref="HttpStatusCode.InternalServerError">InternalServerError</see> if the entity could not be inserted into the database</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public async Task<Response<GetEducationalInstitutionByIDQueryResult>> Handle(DTOEducationalInstitutionByIDQuery request, CancellationToken cancellationToken = default)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                var educationalInstitution = await unitOfWork.UsingEducationalInstitutionQueryRepository()
                                                        .GetByIDAsync(request.EducationalInstitutionID, cancellationToken);

                if (educationalInstitution is null)
                    return new()
                    {
                        Data = null,
                        OperationStatus = false,
                        StatusCode = HttpStatusCode.NotFound,
                        Message = $"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!"
                    };

                return new()
                {
                    Data = educationalInstitution,
                    OperationStatus = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = string.Empty
                };
            }
            catch (Exception e)
            {
                return HandleException<Response<GetEducationalInstitutionByIDQueryResult>>(
                            error_message: "Could not find an Educational Institution with ID: {0}, using {1}'s method: {2}, error details => {3}",
                            response_message: "An error occurred while searching for the Educational Institution with the following ID: {0}!",
                            request.EducationalInstitutionID,
                            unitOfWork.GetType(),
                            nameof(IEducationalInstitutionQueryRepository.GetByIDAsync),
                            e.Message);
            }
        }
    }
}