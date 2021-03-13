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

namespace EducationaInstitutionAPI.Business.Queries.OnEducationalInstitution
{
    public class GetEducationalInstitutionByIDQueryHandler : IRequestHandler<DTOEducationalInstitutionByIDQuery, Response<GetEducationalInstitutionByIDQueryResult>>
    {
        private readonly IEducationalInstitutionRepository eduRepository;
        private readonly ILogger<GetEducationalInstitutionByIDQueryHandler> logger;

        public GetEducationalInstitutionByIDQueryHandler(IEducationalInstitutionRepository eduRepository, ILogger<GetEducationalInstitutionByIDQueryHandler> logger)
        {
            this.eduRepository = eduRepository ?? throw new ArgumentNullException(nameof(eduRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Response<GetEducationalInstitutionByIDQueryResult>> Handle(DTOEducationalInstitutionByIDQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            try
            {
                var eduInstitution = await eduRepository.GetByID(request.EduInstitutionID, cancellationToken);

                if (eduInstitution == null)
                    return new()
                    {
                        ResponseObject = null,
                        OperationStatus = false,
                        StatusCode = HttpStatusCode.NotFound,
                        Message = $"Educational Institution with the following ID:{request.EduInstitutionID} has not been found!"
                    };

                return new()
                {
                    ResponseObject = eduInstitution,
                    OperationStatus = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = null
                };
            }
            catch (Exception e)
            {
                logger.LogError("Could not retrieve an Educational Institution with ID:{0}, using EducationInstitution Repository's method:{1}, error details=>{2}", request.EduInstitutionID, nameof(eduRepository.GetByID), e.Message);
                return new()
                {
                    ResponseObject = null,
                    OperationStatus = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = $"An error occurred while searching for the Educational Institution with the following ID:{request.EduInstitutionID}!"
                };
            }
        }
    }
}