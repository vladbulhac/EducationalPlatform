using Aggregator.Common.Proto;
using Aggregator.DTOs;
using Aggregator.DTOs.EducationalInstitutionDTOs.Requests;
using Aggregator.DTOs.EducationalInstitutionDTOs.Responses;
using Aggregator.EducationalInstitutionAPI.Proto;
using EducationaInstitutionAPI.Utils;
using Google.Protobuf.Collections;
using Grpc.Core;
using System.Collections.Generic;
using System.Linq;
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
                parentInstitutionID = request_data.ParentInstitutionID.Value.ToProtoUuid();

            EducationalInstitutionCreateRequest request = new()
            {
                Name = request_data.Name,
                Description = request_data.Description,
                LocationId = request_data.LocationID,
                ParentInstitutionId = parentInstitutionID
            };
            request.Buildings.Add(request_data.BuildingsIDs);

            foreach (var adminID in request_data.AdminsIDs)
                request.AdminsIds.Add(adminID.ToProtoUuid());

            return request;
        }

        /// <remarks>If <see cref="Metadata"/> contains elements then the request failed</remarks>
        public static Response<DTOCreateEducationalInstitutionResponse> MapGrpcCallResponse(this GrpcCallResponse<EducationalInstitutionCreateResponse> grpcCallResponse)
        {
            var failedGrpcCallResponse = HandleFailedGrpcCall<Response<DTOCreateEducationalInstitutionResponse>, EducationalInstitutionCreateResponse>(grpcCallResponse);
            if (failedGrpcCallResponse != default) return failedGrpcCallResponse;

            return new()
            {
                Data = new() { EducationalInstitutionID = grpcCallResponse.Body.Data.EducationalInstitutionId.ToGuid() },
                Message = grpcCallResponse.Body.Message,
                StatusCode = grpcCallResponse.Body.StatusCode.MapToEquivalentProtoHttpStatusCodeOrOK(),
                OperationStatus = grpcCallResponse.Body.OperationStatus
            };
        }

        public static Response<DTOGetEducationalInstitutionByIDResponse> MapGrpcCallResponse(this GrpcCallResponse<EducationalInstitutionGetResponse> grpcCallResponse)
        {
            var failedGrpcCallResponse = HandleFailedGrpcCall<Response<DTOGetEducationalInstitutionByIDResponse>, EducationalInstitutionGetResponse>(grpcCallResponse);
            if (failedGrpcCallResponse != default) return failedGrpcCallResponse;

            return new()
            {
                Data = new()
                {
                    Name = grpcCallResponse.Body.Data.Name,
                    Description = grpcCallResponse.Body.Data.Description,
                    JoinDate = grpcCallResponse.Body.Data.JoinDate.ToDateTime(),
                    LocationID = grpcCallResponse.Body.Data.LocationId,
                    BuildingsIDs = grpcCallResponse.Body.Data.Buildings,
                    ChildInstitutions = MapInstitutions(grpcCallResponse.Body.Data.ChildInstitutions),
                    ParentInstitution = MapInstitution(grpcCallResponse.Body.Data.ParentInstitution)
                },
                StatusCode = grpcCallResponse.Body.StatusCode.MapToEquivalentProtoHttpStatusCodeOrOK(),
                OperationStatus = grpcCallResponse.Body.OperationStatus,
                Message = grpcCallResponse.Body.Message
            };
        }

        private static DTOEducationalInstitutionBaseResponse MapInstitution(BaseQueryResult institution)
        {
            if (institution is null) return null;

            return new()
            {
                EducationalInstitutionID = institution.EducationalInstitutionId.ToGuid(),
                Name = institution.Name,
                Description = institution.Description
            };
        }

        private static ICollection<DTOEducationalInstitutionBaseResponse> MapInstitutions(RepeatedField<BaseQueryResult> institutions)
        {
            if (institutions is null) return new List<DTOEducationalInstitutionBaseResponse>(0);

            List<DTOEducationalInstitutionBaseResponse> mappedInstitutions = new(institutions.Count);
            for (int i = 0; i < institutions.Count; i++)
                mappedInstitutions.Add(new()
                {
                    EducationalInstitutionID = institutions[i].EducationalInstitutionId.ToGuid(),
                    Name = institutions[i].Name,
                    Description = institutions[i].Description
                });

            return mappedInstitutions;
        }

        private static TOut GetTrailersFromGrpcResponse<TIn, TOut>(GrpcCallResponse<TIn> grpcCallResponse) where TIn : class where TOut : Response, new()
        {
            return new()
            {
                StatusCode = (HttpStatusCode)int.Parse(grpcCallResponse.Trailers.Get("httpstatuscode").Value),
                Message = grpcCallResponse.Trailers.Get("message").Value
            };
        }

        /// <summary>
        /// Handles a grpc call whose <paramref name="grpcCallResponse"/> failed by returning <see cref="Metadata">Trailers</see> or a null Body
        /// </summary>
        /// <returns>
        /// <list type="table">
        /// <item> <typeparamref name="TResponse"/> if <paramref name="grpcCallResponse"/> has <see cref="Metadata">Trailers</see> </item>
        /// <item> <typeparamref name="TResponse"/> if <paramref name="grpcCallResponse"/>.Body is null </item>
        /// <item> Default <typeparamref name="TResponse"/> if the grpc call did not fail </item>
        /// </list>
        /// </returns>
        private static TResponse HandleFailedGrpcCall<TResponse, TIn>(GrpcCallResponse<TIn> grpcCallResponse) where TIn : class where TResponse : Response, new()
        {
            if (grpcCallResponse.Trailers.Count > 0)
                return GetTrailersFromGrpcResponse<TIn, TResponse>(grpcCallResponse);

            if (grpcCallResponse.Body is null)
                return new() { StatusCode = HttpStatusCode.InternalServerError, Message = "An error occurred while trying to reach a service needed for this request!" };

            return default;
        }
    }
}