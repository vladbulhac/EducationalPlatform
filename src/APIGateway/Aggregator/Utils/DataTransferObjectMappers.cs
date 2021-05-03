using Aggregator.Common.Proto;
using Aggregator.DTOs;
using Aggregator.DTOs.EducationalInstitutionDTOs.Requests;
using Aggregator.DTOs.EducationalInstitutionDTOs.Responses;
using Aggregator.EducationalInstitutionAPI.Proto;
using EducationaInstitutionAPI.Utils;
using System;
using System.Net;

namespace Aggregator.Utils
{
    /// <summary>
    /// Contains extension methods that are used to map requests/responses
    /// </summary>
    public static class DataTransferObjectMappers
    {
        public static EducationalInstitutionCreateRequest MapToDTOEducationalInstitutionCreateRequest(this DTOCreateEducationalInstitutionRequest request_data)
        {
            Uuid parentInstitutionID = null;
            if (request_data.ParentInstitutionID.HasValue)
            {
                request_data.ParentInstitutionID.Value.EncodeGuid(out UInt64 high64, out UInt64 low64);
                parentInstitutionID = new() { High64 = high64, Low64 = low64 };
            }

            EducationalInstitutionCreateRequest request = new()
            {
                Name = request_data.Name,
                Description = request_data.Description,
                LocationId = request_data.LocationID,
                ParentInstitutionId = parentInstitutionID
            };
            request.Buildings.Add(request_data.BuildingsIDs);

            return request;
        }

        /// <remarks>If <see cref="Metadata"/> contains elements then the request failed</remarks>
        public static Response<DTOGetEducationalInstitutionByIDResponse> MapGrpcCallResponse(this GrpcCallResponse<EducationalInstitutionCreateResponse> grpcCallResponse)
        {
            if (grpcCallResponse.Trailers.Count > 0)
                return new()
                {
                    StatusCode = (HttpStatusCode)int.Parse(grpcCallResponse.Trailers.Get("httpstatuscode").Value),
                    Message = grpcCallResponse.Trailers.Get("message").Value
                };

            return new()
            {
                Data = new() { EducationalInstitutionID = ProtobufGuidConverter.DecodeGuid(grpcCallResponse.Body.Data.EducationalInstitutionId.High64, grpcCallResponse.Body.Data.EducationalInstitutionId.Low64) },
                Message = grpcCallResponse.Body.Message,
                StatusCode = grpcCallResponse.Body.StatusCode.MapToEquivalentProtoHttpStatusCodeOrOK(),
                OperationStatus = grpcCallResponse.Body.OperationStatus
            };
        }
    }
}