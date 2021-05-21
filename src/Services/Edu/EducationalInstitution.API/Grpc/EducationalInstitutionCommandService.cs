using EducationalInstitutionAPI.Business.Commands_Handlers;
using EducationalInstitutionAPI.Business.Validation_Handler;
using EducationalInstitutionAPI.Proto;
using EducationalInstitutionAPI.Utils.Mappers;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Grpc
{
    /// <summary>
    /// Implements the methods that handle the Remote Call Procedure requests
    /// </summary>
    public class EducationalInstitutionCommandService : Command.CommandBase
    {
        private readonly IMediator mediator;
        private readonly IValidationHandler validationHandler;
        private readonly ILogger<EducationalInstitutionCommandService> logger;

        public EducationalInstitutionCommandService(IMediator mediator, ILogger<EducationalInstitutionCommandService> logger, IValidationHandler validationHandler)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.validationHandler = validationHandler ?? throw new ArgumentNullException(nameof(validationHandler));
        }

        /// <summary>
        /// Overrides the auto generated Remote Call Procedure method from proto file, validates the request fields and sends it to the <see cref="Mediator"/> to handle it
        /// </summary>
        /// <returns>
        /// In addition to the returned <see cref="HttpStatusCode">HttpStatusCodes</see> by <see cref="CreateEducationalInstitutionCommandHandler">handler</see>:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.BadRequest">BadRequest</see> if <paramref name="request"/>'s fields fail the validation process</item>
        /// </list>
        /// <see cref="ServerCallContext"/>'s Status is also set, before returning from method, to:
        /// <list type="bullet">
        /// <item><see cref="StatusCode.OK">OK</see> if successful</item>
        /// <item><see cref="StatusCode.InvalidArgument">InvalidArgument</see> if the validation process fails</item>
        /// <item><see cref="StatusCode.Aborted">Aborted</see> if the request fails or an exception is caught</item>
        /// </list>
        /// If the request fails (e.g an Exception is thrown somewhere) then <see cref="ServerCallContext"/>'s ResponseTrailers are set with a message and <see cref="HttpStatusCode"/>
        /// </returns>
        public override async Task<EducationalInstitutionCreateResponse> CreateEducationalInstitution(EducationalInstitutionCreateRequest request, ServerCallContext context)
        {
            logger.LogInformation("Begin gRPC call EducationalInstitutionCommandService.CreateEducationalInstitution");

            if (request is null) throw new ArgumentNullException(nameof(request));
            if (context is null) throw new ArgumentNullException(nameof(context));

            var mappedRequest = request.MapToDTOEducationalInstitutionCreateCommand();

            if (!validationHandler.IsRequestValid(mappedRequest, out string validationErrors))
            {
                SetStatusAndTrailersOfContext(ref context, StatusCode.InvalidArgument, validationErrors, HttpStatusCode.BadRequest);
                return new();
            }

            try
            {
                var result = await mediator.Send(mappedRequest);

                if (result.OperationStatus)
                {
                    context.Status = new(result.StatusCode.ToRPCCallContextStatusCode(), "Educational Institution was successfully created!");

                    return new()
                    {
                        Data = new() { EducationalInstitutionId = result.Data.EducationalInstitutionID.ToProtoUuid() },
                        OperationStatus = result.OperationStatus,
                        StatusCode = result.StatusCode.MapToEquivalentProtoHttpStatusCodeOrOK(),
                        Message = result.Message
                    };
                }
                else
                    SetStatusAndTrailersOfContext(ref context, result.StatusCode.ToRPCCallContextStatusCode(), result.Message, result.StatusCode);
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

        /// <summary>
        /// Overrides the auto generated Remote Call Procedure method from proto file, validates the request fields and sends it to the <see cref="Mediator"/> to handle it
        /// </summary>
        /// <returns>
        /// In addition to the returned <see cref="HttpStatusCode">HttpStatusCodes</see> by <see cref="DeleteEducationalInstitutionCommandHandler">handler</see>:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.BadRequest">BadRequest</see> if <paramref name="request"/>'s fields fail the validation process</item>
        /// </list>
        /// <see cref="ServerCallContext"/>'s Status is also set, before returning from method, to:
        /// <list type="bullet">
        /// <item><see cref="StatusCode.OK">OK</see> if successful</item>
        /// <item><see cref="StatusCode.InvalidArgument">InvalidArgument</see> if the validation process fails</item>
        /// <item><see cref="StatusCode.Aborted">Aborted</see> if the request fails or an exception is caught</item>
        /// </list>
        /// If the request fails (e.g an Exception is thrown somewhere) then <see cref="ServerCallContext"/>'s ResponseTrailers are set with a message and <see cref="HttpStatusCode"/>
        /// </returns>
        public override async Task<EducationalInstitutionDeleteResponse> DeleteEducationalInstitution(EducationalInstitutionDeleteRequest request, ServerCallContext context)
        {
            logger.LogInformation("Begin grpc call EducationalInstitutionCommandService.DeleteEducationalInstitution");

            if (request is null) throw new ArgumentNullException(nameof(request));
            if (context is null) throw new ArgumentNullException(nameof(context));

            var mappedRequest = request.MapToDTOEducationalInstitutionDeleteCommand();

            if (!validationHandler.IsRequestValid(mappedRequest, out string validationErrors))
            {
                SetStatusAndTrailersOfContextWhenValidationFails(ref context, validationErrors);
                return new();
            }

            try
            {
                var result = await mediator.Send(mappedRequest);

                if (result.OperationStatus)
                {
                    context.Status = new(result.StatusCode.ToRPCCallContextStatusCode(), "Educational Institution was successfully scheduled for deletion!");

                    return new()
                    {
                        Data = new() { DateForPermanentDeletion = Timestamp.FromDateTime(result.Data.DateForPermanentDeletion.ToUniversalTime()) },
                        OperationStatus = result.OperationStatus,
                        StatusCode = result.StatusCode.MapToEquivalentProtoHttpStatusCodeOrOK(),
                        Message = result.Message
                    };
                }
                else
                    SetStatusAndTrailersOfContext(ref context, result.StatusCode.ToRPCCallContextStatusCode(), result.Message, result.StatusCode);
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

        /// <summary>
        /// Overrides the auto generated Remote Call Procedure method from proto file, validates the request fields and sends it to the <see cref="Mediator"/> to handle it
        /// </summary>
        /// <returns>
        /// In addition to the returned <see cref="HttpStatusCode">HttpStatusCodes</see> by <see cref="UpdateEducationalInstitutionParentCommandHandler">handler</see>:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.BadRequest">BadRequest</see> if <paramref name="request"/>'s fields fail the validation process</item>
        /// </list>
        /// <see cref="ServerCallContext"/>'s Status is also set, before returning from method, to:
        /// <list type="bullet">
        /// <item><see cref="StatusCode.OK">OK</see> if successful</item>
        /// <item><see cref="StatusCode.InvalidArgument">InvalidArgument</see> if the validation process fails</item>
        /// <item><see cref="StatusCode.Aborted">Aborted</see> if the request fails or an exception is caught</item>
        /// </list>
        /// If the request fails (e.g an Exception is thrown somewhere) then <see cref="ServerCallContext"/>'s ResponseTrailers are set with a message and <see cref="HttpStatusCode"/>
        /// </returns>
        public override async Task<EducationalInstitutionUpdateResponse> UpdateEducationalInstitutionParent(EducationalInstitutionParentUpdateRequest request, ServerCallContext context)
        {
            logger.LogInformation("Begin grpc call EducationalInstitutionCommandService.UpdateEducationalInstitutionParent");

            if (request is null) throw new ArgumentNullException(nameof(request));
            if (context is null) throw new ArgumentNullException(nameof(context));

            var mappedRequest = request.MapToDTOEducationalInstitutionParentUpdateCommand();

            if (!validationHandler.IsRequestValid(mappedRequest, out string validationErrors))
            {
                SetStatusAndTrailersOfContextWhenValidationFails(ref context, validationErrors);
                return new();
            }

            try
            {
                var result = await mediator.Send(mappedRequest);

                if (result.OperationStatus)
                {
                    context.Status = new(result.StatusCode.ToRPCCallContextStatusCode(), "Educational Institution's parent has been successfully updated!");
                    return new()
                    {
                        Message = result.Message,
                        OperationStatus = result.OperationStatus,
                        StatusCode = result.StatusCode.MapToEquivalentProtoHttpStatusCodeOrOK()
                    };
                }
                else
                    SetStatusAndTrailersOfContext(ref context, result.StatusCode.ToRPCCallContextStatusCode(), result.Message, result.StatusCode);
            }
            catch (Exception e)
            {
                HandleException(logger,
                                ref context,
                                "Could not update the parent of the Educational Institution with the request data: {0}, using {1}, error details=> {2}",
                                mediator.GetType(),
                                JsonConvert.SerializeObject(request),
                                e.Message);
            }

            return new();
        }

        /// <summary>
        /// Overrides the auto generated Remote Call Procedure method from proto file, validates the request fields and sends it to the <see cref="Mediator"/> to handle it
        /// </summary>
        /// <returns>
        /// In addition to the returned <see cref="HttpStatusCode">HttpStatusCodes</see> by <see cref="UpdateEducationalInstitutionLocationCommandHandler">handler</see>:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.BadRequest">BadRequest</see> if <paramref name="request"/>'s fields fail the validation process</item>
        /// </list>
        /// <see cref="ServerCallContext"/>'s Status is also set, before returning from method, to:
        /// <list type="bullet">
        /// <item><see cref="StatusCode.OK">OK</see> if successful</item>
        /// <item><see cref="StatusCode.InvalidArgument">InvalidArgument</see> if the validation process fails</item>
        /// <item><see cref="StatusCode.Aborted">Aborted</see> if the request fails or an exception is caught</item>
        /// </list>
        /// If the request fails (e.g an Exception is thrown somewhere) then <see cref="ServerCallContext"/>'s ResponseTrailers are set with a message and <see cref="HttpStatusCode"/>
        /// </returns>
        public override async Task<EducationalInstitutionUpdateResponse> UpdateEducationalInstitutionLocation(EducationalInstitutionLocationUpdateRequest request, ServerCallContext context)
        {
            logger.LogInformation("Begin grpc call EducationalInstitutionCommandService.UpdateEducationalInstitutionLocation");

            if (request is null) throw new ArgumentNullException(nameof(request));
            if (context is null) throw new ArgumentNullException(nameof(context));

            var mappedRequest = request.MapToDTOEducationalInstitutionLocationUpdateCommand();

            if (!validationHandler.IsRequestValid(mappedRequest, out string validationErrors))
            {
                SetStatusAndTrailersOfContextWhenValidationFails(ref context, validationErrors);
                return new();
            }

            try
            {
                var result = await mediator.Send(mappedRequest);

                if (result.OperationStatus)
                {
                    context.Status = new(result.StatusCode.ToRPCCallContextStatusCode(), "Educational Institution's location has been successfully updated!");
                    return new()
                    {
                        OperationStatus = result.OperationStatus,
                        Message = result.Message,
                        StatusCode = result.StatusCode.MapToEquivalentProtoHttpStatusCodeOrOK()
                    };
                }
                else
                    SetStatusAndTrailersOfContext(ref context, result.StatusCode.ToRPCCallContextStatusCode(), result.Message, result.StatusCode);
            }
            catch (Exception e)
            {
                HandleException(logger,
                               ref context,
                               "Could not update the location of the Educational Institution with the request data: {0}, using {1}, error details=> {2}",
                               mediator.GetType(),
                               JsonConvert.SerializeObject(request),
                               e.Message);
            }

            return new();
        }
    }
}