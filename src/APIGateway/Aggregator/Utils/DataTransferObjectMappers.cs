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
        public static EducationalInstitutionCreateRequest MapToEducationalInstitutionCreateRequest(this DTOCreateEducationalInstitutionRequest requestData)
        {
            Uuid parentInstitutionID = null;
            if (requestData.ParentInstitutionID.HasValue)
                parentInstitutionID = requestData.ParentInstitutionID.Value.ToProtoUuid();

            EducationalInstitutionCreateRequest request = new()
            {
                Name = requestData.Name,
                Description = requestData.Description,
                LocationId = requestData.LocationID,
                ParentInstitutionId = parentInstitutionID
            };
            request.Buildings.Add(requestData.BuildingsIDs);

            foreach (var adminID in requestData.AdminsIDs)
                request.AdminsIds.Add(adminID.ToProtoUuid());

            return request;
        }

        /// <remarks>If <see cref="Metadata"/> contains elements then the request failed</remarks>
        public static Response<CreateEducationalInstitutionResponse> MapGrpcCallResponse(this GrpcCallResponse<EducationalInstitutionCreateResponse> grpcCallResponse)
        {
            var failedGrpcCallResponse = HandleFailedGrpcCall<Response<CreateEducationalInstitutionResponse>, EducationalInstitutionCreateResponse>(grpcCallResponse);
            if (failedGrpcCallResponse != default) return failedGrpcCallResponse;

            return new()
            {
                Data = new() { EducationalInstitutionID = grpcCallResponse.Body.Data.EducationalInstitutionId.ToGuid() },
                Message = grpcCallResponse.Body.Message,
                StatusCode = grpcCallResponse.Body.StatusCode.ToHttpStatusCode(),
                OperationStatus = grpcCallResponse.Body.OperationStatus
            };
        }

        public static Response<DeleteEducationalInstitutionResponse> MapGrpcCallResponse(this GrpcCallResponse<EducationalInstitutionDeleteResponse> grpcCallResponse)
        {
            var failedGrpcCallResponse = HandleFailedGrpcCall<Response<DeleteEducationalInstitutionResponse>, EducationalInstitutionDeleteResponse>(grpcCallResponse);
            if (failedGrpcCallResponse != default) return failedGrpcCallResponse;

            return new()
            {
                Data = new() { DateForPermanentDeletion = grpcCallResponse.Body.Data.DateForPermanentDeletion.ToDateTime() },
                Message = grpcCallResponse.Body.Message,
                StatusCode = grpcCallResponse.Body.StatusCode.ToHttpStatusCode(),
                OperationStatus = grpcCallResponse.Body.OperationStatus
            };
        }

        public static Response MapGrpcCallResponse(this GrpcCallResponse<EducationalInstitutionUpdateResponse> grpcCallResponse)
        {
            var failedGrpcCallResponse = HandleFailedGrpcCall<Response, EducationalInstitutionUpdateResponse>(grpcCallResponse);
            if (failedGrpcCallResponse != default) return failedGrpcCallResponse;

            return new()
            {
                Message = grpcCallResponse.Body.Message,
                OperationStatus = grpcCallResponse.Body.OperationStatus,
                StatusCode = grpcCallResponse.Body.StatusCode.ToHttpStatusCode()
            };
        }

        public static Response<GetEducationalInstitutionByIDResponse> MapGrpcCallResponse(this GrpcCallResponse<EducationalInstitutionGetResponse> grpcCallResponse)
        {
            var failedGrpcCallResponse = HandleFailedGrpcCall<Response<GetEducationalInstitutionByIDResponse>, EducationalInstitutionGetResponse>(grpcCallResponse);
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
                StatusCode = grpcCallResponse.Body.StatusCode.ToHttpStatusCode(),
                OperationStatus = grpcCallResponse.Body.OperationStatus,
                Message = grpcCallResponse.Body.Message
            };
        }

        public static Response<GetAllEducationalInstitutionsByNameResponse> MapGrpcCallResponse(this GrpcCallResponse<EducationalInstitutionGetByNameResponse> grpcCallResponse)
        {
            var failedGrpcCallResponse = HandleFailedGrpcCall<Response<GetAllEducationalInstitutionsByNameResponse>, EducationalInstitutionGetByNameResponse>(grpcCallResponse);
            if (failedGrpcCallResponse != default) return failedGrpcCallResponse;

            return new()
            {
                Data = new() { EducationalInstitutions = MapInstitutions(grpcCallResponse.Body.Data) },
                OperationStatus = grpcCallResponse.Body.OperationStatus,
                StatusCode = grpcCallResponse.Body.StatusCode.ToHttpStatusCode(),
                Message = grpcCallResponse.Body.Message
            };
        }

        public static Response<GetAllEducationalInstitutionsByLocationResponse> MapGrpcCallResponse(this GrpcCallResponse<EducationalInstitutionsGetByLocationResponse> grpcCallResponse)
        {
            var failedGrpcCallResponse = HandleFailedGrpcCall<Response<GetAllEducationalInstitutionsByLocationResponse>, EducationalInstitutionsGetByLocationResponse>(grpcCallResponse);
            if (failedGrpcCallResponse != default) return failedGrpcCallResponse;

            return new()
            {
                Data = new() { EducationalInstitutions = MapInstitutions(grpcCallResponse.Body.Data) },
                Message = grpcCallResponse.Body.Message,
                OperationStatus = grpcCallResponse.Body.OperationStatus,
                StatusCode = grpcCallResponse.Body.StatusCode.ToHttpStatusCode()
            };
        }

        public static Response<GetAllEducationalInstitutionsByBuildingResponse> MapGrpcCallResponse(this GrpcCallResponse<EducationalInstitutionsGetByBuildingResponse> grpcCallResponse)
        {
            var failedGrpcCallResponse = HandleFailedGrpcCall<Response<GetAllEducationalInstitutionsByBuildingResponse>, EducationalInstitutionsGetByBuildingResponse>(grpcCallResponse);
            if (failedGrpcCallResponse != default) return failedGrpcCallResponse;

            return new()
            {
                Data = new()
                {
                    EducationalInstitutions = grpcCallResponse.Body.Data.Select(ei => new EducationalInstitutionBaseResponse
                    {
                        EducationalInstitutionID = ei.EducationalInstitutionId.ToGuid(),
                        Name = ei.Name,
                        Description = ei.Description
                    }).ToList()
                },
                Message = grpcCallResponse.Body.Message,
                OperationStatus = grpcCallResponse.Body.OperationStatus,
                StatusCode = grpcCallResponse.Body.StatusCode.ToHttpStatusCode()
            };
        }

        public static Response<GetAllAdminsByEducationalInstitutionIDResponse> MapGrpcCallResponse(this GrpcCallResponse<AdminsGetByEducationalInstitutionIdResponse> grpcCallResponse)
        {
            var failedGrpcCallResponse = HandleFailedGrpcCall<Response<GetAllAdminsByEducationalInstitutionIDResponse>, AdminsGetByEducationalInstitutionIdResponse>(grpcCallResponse);
            if (failedGrpcCallResponse != default) return failedGrpcCallResponse;

            return new()
            {
                Data = new() { Admins = grpcCallResponse.Body.Data.Select(a => a.ToGuid()).ToList() },
                Message = grpcCallResponse.Body.Message,
                OperationStatus = grpcCallResponse.Body.OperationStatus,
                StatusCode = grpcCallResponse.Body.StatusCode.ToHttpStatusCode()
            };
        }

        private static ICollection<EducationalInstitutionByLocationResponse> MapInstitutions(RepeatedField<GetByLocationResult> institutions)
        {
            if (institutions is null) return default;

            List<EducationalInstitutionByLocationResponse> mappedInstitutions = new(institutions.Count);
            for (int i = 0; i < institutions.Count; i++)
            {
                mappedInstitutions.Add(new()
                {
                    EducationalInstitutionID = institutions[i].EducationalInstitutionId.ToGuid(),
                    Name = institutions[i].Name,
                    Description = institutions[i].Description,
                    Buildings = institutions[i].Buildings
                });
            }

            return mappedInstitutions;
        }

        private static ICollection<EducationalInstitutionByNameResponse> MapInstitutions(RepeatedField<GetByNameResult> institutions)
        {
            if (institutions is null) return default;

            List<EducationalInstitutionByNameResponse> mappedInstitutions = new(institutions.Count);
            for (int i = 0; i < institutions.Count; i++)
            {
                mappedInstitutions.Add(new()
                {
                    EducationalInstitutionID = institutions[i].EducationalInstitutionId.ToGuid(),
                    Description = institutions[i].Description,
                    Name = institutions[i].Name,
                    LocationID = institutions[i].LocationId
                });
            }
            return mappedInstitutions;
        }

        private static EducationalInstitutionBaseResponse MapInstitution(BaseQueryResult institution)
        {
            if (institution is null) return null;

            return new()
            {
                EducationalInstitutionID = institution.EducationalInstitutionId.ToGuid(),
                Name = institution.Name,
                Description = institution.Description
            };
        }

        private static ICollection<EducationalInstitutionBaseResponse> MapInstitutions(RepeatedField<BaseQueryResult> institutions)
        {
            if (institutions is null) return new List<EducationalInstitutionBaseResponse>(0);

            List<EducationalInstitutionBaseResponse> mappedInstitutions = new(institutions.Count);
            for (int i = 0; i < institutions.Count; i++)
                mappedInstitutions.Add(new()
                {
                    EducationalInstitutionID = institutions[i].EducationalInstitutionId.ToGuid(),
                    Name = institutions[i].Name,
                    Description = institutions[i].Description
                });

            return mappedInstitutions;
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
        private static TResponse HandleFailedGrpcCall<TResponse, TIn>(GrpcCallResponse<TIn> grpcCallResponse) where TIn : class
                                                                                                              where TResponse : Response, new()
        {
            if (grpcCallResponse.Trailers.Count > 0)
                return GetTrailersFromGrpcResponse<TIn, TResponse>(grpcCallResponse);

            if (grpcCallResponse.Body is null)
                return new() { StatusCode = HttpStatusCode.InternalServerError, Message = "An error occurred while trying to reach a service needed for this request!" };

            return default;
        }

        private static TOut GetTrailersFromGrpcResponse<TIn, TOut>(GrpcCallResponse<TIn> grpcCallResponse) where TIn : class
                                                                                                           where TOut : Response, new()
        {
            return new()
            {
                StatusCode = (HttpStatusCode)int.Parse(grpcCallResponse.Trailers.Get("httpstatuscode").Value),
                Message = grpcCallResponse.Trailers.Get("message").Value
            };
        }
    }
}