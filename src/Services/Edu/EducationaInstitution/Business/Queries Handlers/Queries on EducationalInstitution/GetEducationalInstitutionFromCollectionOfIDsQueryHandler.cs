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
    public class GetEducationalInstitutionFromCollectionOfIDsQueryHandler : IRequestHandler<DTOEducationalInstitutionsFromCollectionOfIDsQuery, Response<ICollection<GetEducationalInstitutionQueryResult>>>
    {
        private readonly IEducationalInstitutionRepository EduRepository;
        private readonly ILogger<GetEducationalInstitutionFromCollectionOfIDsQueryHandler> Logger;

        public GetEducationalInstitutionFromCollectionOfIDsQueryHandler(IEducationalInstitutionRepository eduRepository, ILogger<GetEducationalInstitutionFromCollectionOfIDsQueryHandler> logger)
        {
            EduRepository = eduRepository;
            Logger = logger;
        }

        public async Task<Response<ICollection<GetEducationalInstitutionQueryResult>>> Handle(DTOEducationalInstitutionsFromCollectionOfIDsQuery request, CancellationToken cancellationToken)
        {
            ICollection<GetEducationalInstitutionQueryResult> educationInstitutions = default;
            try
            {
                educationInstitutions = await EduRepository.GetFromCollectionOfIDs(request.EducationalInstitutionsIDs, cancellationToken);
                if (educationInstitutions == null || educationInstitutions.Count == 0)
                    return new Response<ICollection<GetEducationalInstitutionQueryResult>>() { ResponseObject = null, OperationStatus = true, StatusCode = HttpStatusCode.NotFound, Message = "Could not find the Education Institutions from the list of given IDs!" };

                return new Response<ICollection<GetEducationalInstitutionQueryResult>>() { ResponseObject = educationInstitutions, OperationStatus = true, StatusCode = HttpStatusCode.OK, Message = null };
            }
            catch (Exception e)
            {
                if (educationInstitutions.Count > 0)
                {
                    Logger.LogError("Could not retrieve all Education Institutions with IDs:{0}, using EducationInstitution Repository's method GetFromCollectionOfIDs(), error details=>{1}", request.EducationalInstitutionsIDs.Except(educationInstitutions.Select(eduI => eduI.EduInstitutionID).ToList()), e.Message);
                    return new Response<ICollection<GetEducationalInstitutionQueryResult>>()
                    {
                        ResponseObject = educationInstitutions,
                        OperationStatus = true,
                        StatusCode = HttpStatusCode.OK,
                        Message = "Not all Education Institutions could have been found!"
                    };
                }
                else
                {
                    Logger.LogError("Could not retrieve any Education Institutions from the following list of IDs:{0}, using EducationInstitution Repository's method GetFromCollectionOfIDs(), error details=>{1}", request.EducationalInstitutionsIDs, e.Message);
                    return new Response<ICollection<GetEducationalInstitutionQueryResult>>()
                    {
                        ResponseObject = null,
                        OperationStatus = true,
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "Could not find any Education Institution from the list of IDs given!"
                    };
                }
            }
        }
    }
}