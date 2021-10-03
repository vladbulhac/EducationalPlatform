using DataValidation.Abstractions;
using EducationalInstitutionAPI.Proto;
using EducationalInstitutionAPI.Utils.Mappers;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenIddict.Validation.AspNetCore;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Presentation.Grpc
{
    /// <summary>
    /// Implements the methods that handle the gRPC requests
    /// </summary>
    public class EducationalInstitutionCommandService : Command.CommandBase
    {
        private readonly IMediator mediator;
        private readonly IValidationHandler validationHandler;
        private readonly IHttpContextAccessor httpContext;
        private readonly ILogger<EducationalInstitutionCommandService> logger;

        public EducationalInstitutionCommandService(IHttpContextAccessor httpContext, IMediator mediator, ILogger<EducationalInstitutionCommandService> logger, IValidationHandler validationHandler)
        {
            this.httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.validationHandler = validationHandler ?? throw new ArgumentNullException(nameof(validationHandler));
        }

        /// <summary>
        /// Overrides the auto generated Remote Call Procedure method from proto file, validates the request fields and sends it to the <see cref="Mediator"/> to handle it
        /// </summary>
        /// <returns>
        /// In addition to the returned <see cref="HttpStatusCode">HttpStatusCodes</see> by the handler:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.BadRequest">BadRequest</see> if <paramref name="request"/>'s fields fail the validation process</item>
        /// </list>
        /// <see cref="ServerCallContext"/>'s Status is also set before returning from method to:
        /// <list type="bullet">
        /// <item><see cref="StatusCode.OK">OK</see> if successful</item>
        /// <item><see cref="StatusCode.InvalidArgument">InvalidArgument</see> if the validation process fails</item>
        /// <item><see cref="StatusCode.Aborted">Aborted</see> if the request fails or an exception is caught</item>
        /// </list>
        /// <i>If the request fails (e.g an Exception is caught by this method) then <see cref="ServerCallContext"/>'s ResponseTrailers are set with a Message and <see cref="HttpStatusCode"/></i>
        /// </returns>
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]//, Policy = "CreateEducationalInstitutionPolicy")]
        public override async Task<EducationalInstitutionCreateResponse> CreateEducationalInstitution(EducationalInstitutionCreateRequest request, ServerCallContext context)
        {
            logger.LogInformation("EducationalInstitutionCommandService.CreateEducationalInstitution received gRPC request");

            if (request is null) throw new ArgumentNullException(nameof(request));
            if (context is null) throw new ArgumentNullException(nameof(context));

            var resourceOwnerIdentity = httpContext.HttpContext
                                                   .User
                                                   .FindFirst("sub")
                                                   .Value;
            request.AdminId = resourceOwnerIdentity;

            var dto = request.MapToCreateEducationalInstitutionCommand();

            if (!validationHandler.IsDataTransferObjectValid(dto, out string validationErrors))
            {
                SetStatusAndTrailersOfContext(ref context, StatusCode.InvalidArgument, validationErrors, HttpStatusCode.BadRequest);
                return new();
            }

            try
            {
                var result = await mediator.Send(dto);

                if (result.OperationStatus)
                {
                    context.Status = new(result.StatusCode.ToGrpcContextStatusCode(), "Educational Institution has been successfully created!");

                    return new()
                    {
                        Data = new() { EducationalInstitutionId = result.Data.EducationalInstitutionID.ToProtoUuid() },
                        OperationStatus = result.OperationStatus,
                        StatusCode = result.StatusCode.ToProtoHttpStatusCode(),
                        Message = result.Message
                    };
                }
                else
                    SetStatusAndTrailersOfContext(ref context, result.StatusCode.ToGrpcContextStatusCode(), result.Message, result.StatusCode);
            }
            catch (Exception e)
            {
                HandleException(logger,
                                ref context,
                                "Could not create an Educational Institution with the request data: {0}, using {1}, error details => {2}",
                                JsonConvert.SerializeObject(request),
                                mediator.GetType(),
                                e.Message);
            }

            return new();
        }

        /// <inheritdoc cref="CreateEducationalInstitution"/>
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme, Policy = "DeletePolicy")]
        public override async Task<EducationalInstitutionDeleteResponse> DeleteEducationalInstitution(EducationalInstitutionDeleteRequest request, ServerCallContext context)
        {
            logger.LogInformation("EducationalInstitutionCommandService.DeleteEducationalInstitution received gRPC request");

            if (request is null) throw new ArgumentNullException(nameof(request));
            if (context is null) throw new ArgumentNullException(nameof(context));

            var dto = request.MapToDisableEducationalInstitutionCommand();
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
                    context.Status = new(result.StatusCode.ToGrpcContextStatusCode(), "Educational Institution was successfully scheduled for deletion!");

                    return new()
                    {
                        Data = new() { DateForPermanentDeletion = Timestamp.FromDateTime(result.Data.DateForPermanentDeletion.ToUniversalTime()) },
                        OperationStatus = result.OperationStatus,
                        StatusCode = result.StatusCode.ToProtoHttpStatusCode(),
                        Message = result.Message
                    };
                }
                else
                    SetStatusAndTrailersOfContext(ref context, result.StatusCode.ToGrpcContextStatusCode(), result.Message, result.StatusCode);
            }
            catch (Exception e)
            {
                HandleException(logger,
                                ref context,
                                "Could not schedule for deletion the Educational Institution with the request data: {0}, using {1}, error details => {2}",
                                JsonConvert.SerializeObject(request),
                                mediator.GetType(),
                                e.Message);
            }

            return new();
        }

        /// <inheritdoc cref="CreateEducationalInstitution"/>
        public override async Task<EducationalInstitutionUpdateResponse> UpdateEducationalInstitution(EducationalInstitutionUpdateRequest request, ServerCallContext context)
        {
            logger.LogInformation("EducationalInstitutionCommandService.UpdateEducationalInstitution received gRPC request");

            if (request is null) throw new ArgumentNullException(nameof(request));
            if (context is null) throw new ArgumentNullException(nameof(context));

            var dto = request.MapToUpdateEducationalInstitutionCommand();
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
                    context.Status = new(result.StatusCode.ToGrpcContextStatusCode(), "Educational Institution has been successfully updated!");
                    return new()
                    {
                        Message = result.Message,
                        OperationStatus = result.OperationStatus,
                        StatusCode = result.StatusCode.ToProtoHttpStatusCode()
                    };
                }
                else
                    SetStatusAndTrailersOfContext(ref context, result.StatusCode.ToGrpcContextStatusCode(), result.Message, result.StatusCode);
            }
            catch (Exception e)
            {
                HandleException(logger,
                                ref context,
                                "Could not the Educational Institution with the request data: {0}, using {1}, error details=> {2}",
                                JsonConvert.SerializeObject(request),
                                mediator.GetType(),
                                e.Message);
            }

            return new();
        }

        /// <inheritdoc cref="CreateEducationalInstitution"/>
        public override async Task<EducationalInstitutionUpdateResponse> UpdateEducationalInstitutionAdmin(EducationalInstitutionAdminUpdateRequest request, ServerCallContext context)
        {
            logger.LogInformation("EducationalInstitutionCommandService.UpdateEducationalInstitutionAdmin received gRPC request");

            if (request is null) throw new ArgumentNullException(nameof(request));
            if (context is null) throw new ArgumentNullException(nameof(context));

            var dto = request.MapToUpdateEducationalInstitutionAdminsCommand();
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
                    context.Status = new(result.StatusCode.ToGrpcContextStatusCode(), "Educational Institution's admins were successfully updated!");
                    return new()
                    {
                        Message = result.Message,
                        OperationStatus = result.OperationStatus,
                        StatusCode = result.StatusCode.ToProtoHttpStatusCode()
                    };
                }
                else
                    SetStatusAndTrailersOfContext(ref context, result.StatusCode.ToGrpcContextStatusCode(), result.Message, result.StatusCode);
            }
            catch (Exception e)
            {
                HandleException(logger,
                                ref context,
                                "Could not update the admins of the Educational Institution with the request data: {0}, using {1}, error details=> {2}",
                                JsonConvert.SerializeObject(request),
                                mediator.GetType(),
                                e.Message);
            }
            return new();
        }

        /// <inheritdoc cref="CreateEducationalInstitution"/>
        public override async Task<EducationalInstitutionUpdateResponse> UpdateEducationalInstitutionParent(EducationalInstitutionParentUpdateRequest request, ServerCallContext context)
        {
            logger.LogInformation("EducationalInstitutionCommandService.UpdateEducationalInstitutionParent received gRPC request");

            if (request is null) throw new ArgumentNullException(nameof(request));
            if (context is null) throw new ArgumentNullException(nameof(context));

            var dto = request.MapToUpdateEducationalInstitutionParentCommand();
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
                    context.Status = new(result.StatusCode.ToGrpcContextStatusCode(), "Educational Institution's parent has been successfully updated!");
                    return new()
                    {
                        Message = result.Message,
                        OperationStatus = result.OperationStatus,
                        StatusCode = result.StatusCode.ToProtoHttpStatusCode()
                    };
                }
                else
                    SetStatusAndTrailersOfContext(ref context, result.StatusCode.ToGrpcContextStatusCode(), result.Message, result.StatusCode);
            }
            catch (Exception e)
            {
                HandleException(logger,
                                ref context,
                                "Could not update the parent of the Educational Institution with the request data: {0}, using {1}, error details=> {2}",
                                JsonConvert.SerializeObject(request),
                                mediator.GetType(),
                                e.Message);
            }

            return new();
        }

        /// <inheritdoc cref="CreateEducationalInstitution"/>
        public override async Task<EducationalInstitutionUpdateResponse> UpdateEducationalInstitutionLocation(EducationalInstitutionLocationUpdateRequest request, ServerCallContext context)
        {
            logger.LogInformation("EducationalInstitutionCommandService.UpdateEducationalInstitutionLocation received gRPC request");

            if (request is null) throw new ArgumentNullException(nameof(request));
            if (context is null) throw new ArgumentNullException(nameof(context));

            var dto = request.MapToUpdateEducationalInstitutionLocationCommand();
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
                    context.Status = new(result.StatusCode.ToGrpcContextStatusCode(), "Educational Institution's location has been successfully updated!");
                    return new()
                    {
                        OperationStatus = result.OperationStatus,
                        Message = result.Message,
                        StatusCode = result.StatusCode.ToProtoHttpStatusCode()
                    };
                }
                else
                    SetStatusAndTrailersOfContext(ref context, result.StatusCode.ToGrpcContextStatusCode(), result.Message, result.StatusCode);
            }
            catch (Exception e)
            {
                HandleException(logger,
                               ref context,
                               "Could not update the location of the Educational Institution with the request data: {0}, using {1}, error details=> {2}",
                               JsonConvert.SerializeObject(request),
                               mediator.GetType(),
                               e.Message);
            }

            return new();
        }
    }
}