using Aggregator.Common.Proto;
using Aggregator.DTOs;
using Aggregator.DTOs.EducationalInstitutionDTOs.Requests;
using Aggregator.DTOs.EducationalInstitutionDTOs.Responses;
using Aggregator.EducationalInstitutionAPI.Proto;
using Aggregator.Utils;
using EducationaInstitutionAPI.Utils;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Aggregator.Services.EducationalInstitution
{
    public class EducationalInstitutionCommandService : IEducationalInstitutionCommandService
    {
        private readonly ILogger<EducationalInstitutionCommandService> logger;
        private readonly Command.CommandClient client;

        public EducationalInstitutionCommandService(ILogger<EducationalInstitutionCommandService> logger, Command.CommandClient commandClient)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            client = commandClient ?? throw new ArgumentNullException(nameof(commandClient));
        }

        public async Task<Response<DTOGetEducationalInstitutionByIDResponse>> CreateEducationalInstitutionAsync(DTOCreateEducationalInstitutionRequest educationalInstitutionData)
        {
            //logger.LogDebug("grpc client created, request = {@educationalInstitutionData}", educationalInstitutionData);

            var createRequest = MapRequestToDTOEducationalInstiutionCreateRequest(educationalInstitutionData);

            var requestCall = client.CreateEducationalInstitutionAsync(createRequest);

            var response = await requestCall.ResponseAsync;
            var metadata = requestCall.GetTrailers();

            //logger.LogDebug(" grpc response: {@response}", response);

            return MapGrpcCallResponse(response, metadata);
        }

        /// <remarks>If <see cref="Metadata"/> contains elements then the request failed</remarks>
        private static Response<DTOGetEducationalInstitutionByIDResponse> MapGrpcCallResponse(EducationalInstitutionCreateResponse response_data, Metadata metadata)
        {
            if (metadata.Count > 0)
                return new()
                {
                    StatusCode = (HttpStatusCode)int.Parse(metadata.Get("httpstatuscode").Value),
                    Message = metadata.Get("message").Value
                };

            return new()
            {
                Data = new() { EducationalInstitutionID = ProtobufGuidConverter.DecodeGuid(response_data.Data.EducationalInstitutionId.High64, response_data.Data.EducationalInstitutionId.Low64) },
                Message = response_data.Message,
                StatusCode = response_data.StatusCode.DecodeStatusCode(),
                OperationStatus = response_data.OperationStatus
            };
        }

        private static DTOEducationalInstitutionCreateRequest MapRequestToDTOEducationalInstiutionCreateRequest(DTOCreateEducationalInstitutionRequest requestData)
        {
            Uuid parentInstitutionID = null;
            if (requestData.ParentInstitutionID.HasValue)
            {
                requestData.ParentInstitutionID.Value.EncodeGuid(out UInt64 high64, out UInt64 low64);
                parentInstitutionID = new() { High64 = high64, Low64 = low64 };
            }

            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = requestData.Name,
                Description = requestData.Description,
                LocationId = requestData.LocationID,
                ParentInstitutionId = parentInstitutionID
            };
            request.Buildings.Add(requestData.BuildingsIDs);

            return request;
        }
    }
}