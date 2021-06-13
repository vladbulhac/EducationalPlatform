using DataValidation;
using EducationalInstitutionAPI.Proto;
using EducationalInstitutionAPI.Utils.Mappers;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Grpc
{
    /// <inheritdoc cref="EducationalInstitutionCommandService"/>
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

        /// <inheritdoc cref="EducationalInstitutionCommandService.CreateEducationalInstitution"/>
        public override async Task<EducationalInstitutionGetResponse> GetEducationalInstitutionByID(EducationalInstitutionGetByIdRequest request, ServerCallContext context)
        {
            logger.LogInformation("EducationalInstitutionQueryService.GetEducationalInstitutionByID received gRPC request");

            if (request is null) throw new ArgumentNullException(nameof(request));
            if (context is null) throw new ArgumentNullException(nameof(context));

            var dto = request.MapToDTOEducationalInstitutionByIDQuery();
            if (!validationHandler.IsDataTransferObjectValid(dto, out string validationErrors))
            {
                SetStatusAndTrailersOfContextWhenValidationFails(ref context, validationErrors);
                return new();
            }

            try
            {
                var result = await mediator.Send(dto);

                if (result.OperationStatus)
                {
                    context.Status = new(StatusCode.OK, "Educational Institution with given ID was found!");

                    return new()
                    {
                        Data = result.Data.MapToEducationalInstitutionGetResponse(),
                        OperationStatus = result.OperationStatus,
                        StatusCode = result.StatusCode.ToProtoHttpStatusCode(),
                        Message = result.Message
                    };
                }
                else
                    SetStatusAndTrailersOfContext(ref context, result.StatusCode, result.Message);
            }
            catch (Exception e)
            {
                HandleException(logger,
                                ref context,
                               "Could not get the Educational Institution with the request data: {0}, using {1}, error details => {2}",
                                JsonConvert.SerializeObject(request),
                                mediator.GetType(),
                                e.Message);
            }

            return new();
        }

        /// <inheritdoc cref="EducationalInstitutionCommandService.CreateEducationalInstitution"/>
        public override async Task<EducationalInstitutionGetByNameResponse> GetAllEducationalInstitutionsByName(EducationalInstitutionGetByNameRequest request, ServerCallContext context)
        {
            logger.LogInformation("EducationalInstitutionQueryService.GetAllEducationalInstitutionsByName received gRPC request");

            if (request is null) throw new ArgumentNullException(nameof(request));
            if (context is null) throw new ArgumentNullException(nameof(context));

            var dto = request.MapToDTOEducationalInstitutionsByNameQuery();
            if (!validationHandler.IsDataTransferObjectValid(dto, out string validationErrors))
            {
                SetStatusAndTrailersOfContextWhenValidationFails(ref context, validationErrors);
                return new();
            }

            try
            {
                var result = await mediator.Send(dto);

                if (result.OperationStatus)
                {
                    context.Status = new(StatusCode.OK, "Successfully retrieved Educational Institutions!");

                    return new()
                    {
                        Data = { result.Data.MapToGetByNameResult() },
                        OperationStatus = result.OperationStatus,
                        StatusCode = result.StatusCode.ToProtoHttpStatusCode(),
                        Message = result.Message
                    };
                }
                else
                    SetStatusAndTrailersOfContext(ref context, result.StatusCode, result.Message);
            }
            catch (Exception e)
            {
                HandleException(logger,
                                ref context,
                                "Could not get any Educational Institution with the request data: {0}, using {1}, error details => {2}",
                                JsonConvert.SerializeObject(request),
                                mediator.GetType(),
                                e.Message);
            }

            return new();
        }

        /// <inheritdoc cref="EducationalInstitutionCommandService.CreateEducationalInstitution"/>
        public override async Task<EducationalInstitutionsGetByBuildingResponse> GetAllEducationalInstitutionsByBuilding(EducationalInstitutionsGetByBuildingRequest request, ServerCallContext context)
        {
            logger.LogInformation("EducationalInstitutionQueryService.GetAllEducationalInstitutionsByBuilding received gRPC request");

            if (request is null) throw new ArgumentNullException(nameof(request));
            if (context is null) throw new ArgumentNullException(nameof(context));

            var dto = request.MapToDTOEducationalInstitutionsByBuildingQuery();
            if (!validationHandler.IsDataTransferObjectValid(dto, out string validationErrors))
            {
                SetStatusAndTrailersOfContextWhenValidationFails(ref context, validationErrors);
                return new();
            }

            try
            {
                var result = await mediator.Send(dto);

                if (result.OperationStatus)
                {
                    context.Status = new(StatusCode.OK, "Successfully retrieved Educational Institutions!");

                    return new()
                    {
                        Data = { result.Data.MapToBaseQueryResult() },
                        OperationStatus = result.OperationStatus,
                        StatusCode = result.StatusCode.ToProtoHttpStatusCode(),
                        Message = result.Message
                    };
                }
                else
                    SetStatusAndTrailersOfContext(ref context, result.StatusCode, result.Message);
            }
            catch (Exception e)
            {
                HandleException(logger,
                                 ref context,
                                 "Could not get any Educational Institution with the request data: {0}, using {1}, error details => {2}",
                                 JsonConvert.SerializeObject(request),
                                 mediator.GetType(),
                                 e.Message);
            }

            return new();
        }

        /// <inheritdoc cref="EducationalInstitutionCommandService.CreateEducationalInstitution"/>
        public override async Task<EducationalInstitutionsGetByLocationResponse> GetAllEducationalInstitutionsByLocation(EducationalInstitutionsGetByLocationRequest request, ServerCallContext context)
        {
            logger.LogInformation("EducationalInstitutionQueryService.GetAllEducationalInstitutionsByLocation received gRPC request");

            if (request is null) throw new ArgumentNullException(nameof(request));
            if (context is null) throw new ArgumentNullException(nameof(context));

            var dto = request.MapToDTOEducationalInstitutionsByLocationQuery();
            if (!validationHandler.IsDataTransferObjectValid(dto, out string validationErrors))
            {
                SetStatusAndTrailersOfContextWhenValidationFails(ref context, validationErrors);
                return new();
            }

            try
            {
                var result = await mediator.Send(dto);

                if (result.OperationStatus)
                {
                    context.Status = new(StatusCode.OK, "Successfully retrieved Educational Institutions!");

                    return new()
                    {
                        Data = { result.Data.MapToGetByLocationResult() },
                        OperationStatus = result.OperationStatus,
                        StatusCode = result.StatusCode.ToProtoHttpStatusCode(),
                        Message = result.Message
                    };
                }
                SetStatusAndTrailersOfContext(ref context, result.StatusCode, result.Message);
            }
            catch (Exception e)
            {
                HandleException(logger,
                                 ref context,
                                 "Could not get any Educational Institution with the request data: {0}, using {1}, error details => {2}",
                                 JsonConvert.SerializeObject(request),
                                 mediator.GetType(),
                                 e.Message);
            }

            return new();
        }

        /// <inheritdoc cref="EducationalInstitutionCommandService.CreateEducationalInstitution"/>
        public override async Task<AdminsGetByEducationalInstitutionIdResponse> GetAllAdminsByEducationalInstitutionID(AdminsGetByEducationalInstitutionIdRequest request, ServerCallContext context)
        {
            logger.LogInformation("EducationalInstitutionQueryService.GetAllAdminsByEducationalInstitutionID received gRPC request");

            if (request is null) throw new ArgumentNullException(nameof(request));
            if (context is null) throw new ArgumentNullException(nameof(context));

            var dto = request.MapToDTOAdminsByEducationalInstitutionIDQuery();
            if (!validationHandler.IsDataTransferObjectValid(dto, out string validationErrors))
            {
                SetStatusAndTrailersOfContextWhenValidationFails(ref context, validationErrors);
                return new();
            }

            try
            {
                var result = await mediator.Send(dto);

                if (result.OperationStatus)
                {
                    context.Status = new(result.StatusCode.ToRPCCallContextStatusCode(), "Successfully retrieved the admins of this Educational Institution!");
                    return new()
                    {
                        Data = { result.Data.AdminsIDs.Select(id => id.ToProtoUuid()).ToList() },
                        Message = result.Message,
                        OperationStatus = result.OperationStatus,
                        StatusCode = result.StatusCode.ToProtoHttpStatusCode()
                    };
                }
                else
                    SetStatusAndTrailersOfContext(ref context, result.StatusCode, result.Message);
            }
            catch (Exception e)
            {
                HandleException(logger,
                                  ref context,
                                  "Could not get any admins with the request data: {0}, using {1}, error details => {2}",
                                  JsonConvert.SerializeObject(request),
                                  mediator.GetType(),
                                  e.Message);
            }

            return new();
        }
    }
}