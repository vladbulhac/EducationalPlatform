using Aggregator.EducationaInstitutionAPI.Proto;
using Aggregator.Models.DTOs;
using Aggregator.Utils;
using EducationaInstitutionAPI.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Aggregator.Services
{
    public class EducationalInstitutionService : IEducationalInstitutionService
    {
        private readonly ILogger<EducationalInstitutionService> logger;
        private readonly EducationalInstitution.EducationalInstitutionClient eduInstitutionClient;

        public EducationalInstitutionService(ILogger<EducationalInstitutionService> logger, EducationalInstitution.EducationalInstitutionClient eduInstitutionClient)
        {
            this.logger = logger;
            this.eduInstitutionClient = eduInstitutionClient;
        }

        public async Task<Response<Guid>> CreateEducationalInstiutionAsync(DTOCreateEducationalInstitution educationalInstitutionData)
        {
            //logger.LogDebug("grpc client created, request = {@educationalInstitutionData}", educationalInstitutionData);

            var createRequest = MapRequestToDTOEducationalInstiutionCreateRequest(educationalInstitutionData);
            var response = await eduInstitutionClient.CreateEducationalInstitutionAsync(createRequest);

            //logger.LogDebug(" grpc response: {@response}", response);

            return MapResponseToGenericResponse(response);
        }

        private static Response<Guid> MapResponseToGenericResponse(EducationalInstitutionCreateResponse responseData)
        {
            return new()
            {
                ResponseObject = ProtobufGuidConverter.DecodeGuid(responseData.ResponseObject.EduInstitutionId.High64, responseData.ResponseObject.EduInstitutionId.Low64),
                Message = responseData.Message,
                StatusCode = responseData.StatusCode.DecodeStatusCode(),
                OperationStatus = responseData.OperationStatus
            };
        }

        private static DTOEducationalInstitutionCreateRequest MapRequestToDTOEducationalInstiutionCreateRequest(DTOCreateEducationalInstitution requestData)
        {
            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = requestData.Name,
                Description = requestData.Description,
                LocationId = requestData.LocationID
            };
            request.Buildings.Add(requestData.BuildingsIDs);

            return request;
        }
    }
}