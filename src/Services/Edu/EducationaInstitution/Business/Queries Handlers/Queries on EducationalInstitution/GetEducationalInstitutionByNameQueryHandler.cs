using EducationaInstitutionAPI.DTOs.EducationalInstitution.In;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using EducationaInstitutionAPI.Repositories;
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
    public class GetEducationalInstitutionByNameQueryHandler : IRequestHandler<DTOEducationalInstitutionsByNameQuery, Response<ICollection<GetEducationalInstitutionQueryResult>>>
    {
        private readonly IEducationalInstitutionRepository eduRepository;
        private readonly ILogger<GetEducationalInstitutionByNameQueryHandler> logger;

        public GetEducationalInstitutionByNameQueryHandler(IEducationalInstitutionRepository eduRepository, ILogger<GetEducationalInstitutionByNameQueryHandler> logger)
        {
            this.eduRepository = eduRepository ?? throw new ArgumentNullException(nameof(eduRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Response<ICollection<GetEducationalInstitutionQueryResult>>> Handle(DTOEducationalInstitutionsByNameQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            try
            {
                var eduInstitutions = await eduRepository.GetAllLikeName(request.Name, request.OffsetValue, request.ResultsCount, cancellationToken);
                if (eduInstitutions == null)
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
            catch (Exception e)
            {
                logger.LogError("Could not find any Educational Institution with a name like: {0}, using {1}'s method: {2}, error details => {3}", request.Name, eduRepository.GetType(), nameof(eduRepository.GetAllLikeName), e.Message);
                return new()
                {
                    ResponseObject = null,
                    OperationStatus = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = $"An error occured while searching for any Educational Institution with a name like: {request.Name}!"
                };
            }
        }
    }
}