using EducationalInstitutionAPI.Business.Validation_Handler;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.DTOs.Queries;
using EducationalInstitutionAPI.Proto;
using EducationalInstitutionAPI.Utils;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Grpc
{
    public class EducationalInstitutionQueryService : Query.QueryBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<EducationalInstitutionQueryService> logger;
        private readonly IValidationHandler validationHandler;

        public EducationalInstitutionQueryService(IMediator mediator, ILogger<EducationalInstitutionQueryService> logger, IValidationHandler validationHandler)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.validationHandler = validationHandler ?? throw new ArgumentNullException(nameof(validationHandler));
        }

        public override async Task<EducationalInstitutionGetResponse> GetEducationalInstitutionByID(DTOEducationalInstitutionGetByIdRequest request, ServerCallContext context)
        {
            logger.LogInformation("Begin grpc call EducationalInstitutionQueryService.GetEducationalInstitutionByID");

            if (request is null) throw new ArgumentNullException(nameof(request));
            if (context is null) throw new ArgumentNullException(nameof(context));

            var mappedRequest = MapRequestToDTOEducationalInstitutionByIDQuery(request);

            if (!validationHandler.IsRequestValid(mappedRequest, out string validationErrors))
            {
                context.Status = new(StatusCode.InvalidArgument, validationErrors);

                return new();
            }

            try
            {
                var result = await mediator.Send(mappedRequest);

                if (result.OperationStatus)
                {
                    context.Status = new(StatusCode.OK, "Educational Institution with given ID was found!");
                    result.StatusCode.MapToEquivalentProtoHttpStatusCodeOrOK(out ProtoHttpStatusCode protoStatusCode);

                    return new()
                    {
                        Data = MapResultToEducationalInstitutionGetResponse(result.Data),
                        OperationStatus = result.OperationStatus,
                        StatusCode = protoStatusCode,
                        Message = result.Message
                    };
                }
                else
                if (result.StatusCode == HttpStatusCode.NotFound)
                    context.Status = new(StatusCode.NotFound, result.Message);
                else
                    context.Status = new(StatusCode.Aborted, result.Message);

                context.ResponseTrailers.AddMultiple(new (string key, string value)[2] {
                    ("Message", result.Message),
                    ("HttpStatusCode",((int)result.StatusCode).ToString())
                    });
            }
            catch (Exception e)
            {
                logger.LogError(
                   "Could not create an Educational Institution with the request data: {0}, using {1}, error details => {2}",
                   JsonConvert.SerializeObject(request),
                   mediator.GetType(),
                   e.Message
               );

                context.Status = new(StatusCode.Aborted, "An error occurred while processing the request!");
            }

            return new();
        }

        private static GetByIDQueryResult MapResultToEducationalInstitutionGetResponse(GetEducationalInstitutionByIDQueryResult data)
        {
            BaseQueryResult parentInstitution = null;
            if (data.ParentInstitution is not null)
                parentInstitution = new()
                {
                    EducationalInstitutionId = data.ParentInstitution.EducationalInstitutionID.ToProtocolBufferLanguageEquivalent(),
                    Name = data.ParentInstitution.Name,
                    Description = data.ParentInstitution.Description
                };

            GetByIDQueryResult result = new()
            {
                Name = data.Name,
                Description = data.Description,
                JoinDate = Timestamp.FromDateTime(data.JoinDate),
                LocationId = data.LocationID,
                ParentInstitution = parentInstitution
            };

            foreach (var buildingID in data.BuildingsIDs)
                result.Buildings.Add(buildingID);

            if (data.ChildInstitutions.Count > 0)
                AddChildInstitutionToResult(ref result, data.ChildInstitutions);

            return result;
        }

        private static void AddChildInstitutionToResult(ref GetByIDQueryResult result, ICollection<EducationalInstitutionBaseQueryResult> childInstitutions)
        {
            foreach (var childInstitution in childInstitutions)
            {
                BaseQueryResult institution = new()
                {
                    EducationalInstitutionId = childInstitution.EducationalInstitutionID.ToProtocolBufferLanguageEquivalent(),
                    Name = childInstitution.Name,
                    Description = childInstitution.Description
                };

                result.ChildInstitutions.Add(institution);
            }
        }

        private static DTOEducationalInstitutionByIDQuery MapRequestToDTOEducationalInstitutionByIDQuery(DTOEducationalInstitutionGetByIdRequest request)
        {
            return new()
            {
                EducationalInstitutionID = ProtobufGuidConverter.DecodeGuid(request.EducationalInstitutionId.High64, request.EducationalInstitutionId.Low64)
            };
        }
    }
}