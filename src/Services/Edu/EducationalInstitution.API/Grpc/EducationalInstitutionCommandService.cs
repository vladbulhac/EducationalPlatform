using EducationalInstitutionAPI.Business.Commands_Handlers;
using EducationalInstitutionAPI.Business.Validation_Handler;
using EducationalInstitutionAPI.Proto;
using EducationalInstitutionAPI.Utils;
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
        private readonly ILogger<EducationalInstitutionCommandService> logger;
        private readonly IValidationHandler validationHandler;

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
            logger.LogInformation("Begin grpc call EducationalInstitutionCommandService.CreateEducationalInstitution");

            if (request is null) throw new ArgumentNullException(nameof(request));
            if (context is null) throw new ArgumentNullException(nameof(context));

            var mappedRequest = request.MapToDTOEducationalInstitutionCreateCommand();

            if (!validationHandler.IsRequestValid(mappedRequest, out string validationErrors))
            {
                context.Status = new(StatusCode.InvalidArgument, validationErrors);
                context.ResponseTrailers.AddMultiple(new (string key, string value)[2] {
                    ("Message", validationErrors),
                    ("HttpStatusCode", ((int)HttpStatusCode.BadRequest).ToString())
                });

                return new();
            }

            try
            {
                var result = await mediator.Send(mappedRequest);

                if (result.OperationStatus)
                {
                    context.Status = new(StatusCode.OK, "Educational Institution was successfully created!");

                    return new()
                    {
                        Data = new() { EducationalInstitutionId = result.Data.EducationalInstitutionID.ToProtoUuid() },
                        OperationStatus = result.OperationStatus,
                        StatusCode = result.StatusCode.MapToEquivalentProtoHttpStatusCodeOrOK(),
                        Message = result.Message
                    };
                }
                else
                {
                    context.Status = new(StatusCode.Aborted, result.Message);
                    context.ResponseTrailers.AddMultiple(new (string key, string value)[2] {
                    ("Message", result.Message),
                    ("HttpStatusCode",((int)result.StatusCode).ToString())
                    });
                }
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
                context.ResponseTrailers.AddMultiple(new (string key, string value)[2] {
                    ("Message", "An error occurred while processing the request!"),
                    ("HttpStatusCode", ((int)HttpStatusCode.InternalServerError).ToString())
                });
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
                context.Status = new(StatusCode.InvalidArgument, validationErrors);
                context.ResponseTrailers.AddMultiple(new (string key, string value)[2] {
                    ("Message", validationErrors),
                    ("HttpStatusCode", ((int)HttpStatusCode.BadRequest).ToString())
                });

                return new();
            }

            try
            {
                var result = await mediator.Send(mappedRequest);

                if (result.OperationStatus)
                {
                    context.Status = new(StatusCode.OK, "Educational Institution was successfully scheduled for deletion!");

                    return new()
                    {
                        Data = new() { DateForPermanentDeletion = Timestamp.FromDateTime(result.Data.DateForPermanentDeletion.ToUniversalTime()) },
                        OperationStatus = result.OperationStatus,
                        StatusCode = result.StatusCode.MapToEquivalentProtoHttpStatusCodeOrOK(),
                        Message = result.Message
                    };
                }
                else
                {
                    context.Status = new(StatusCode.Aborted, result.Message);
                    context.ResponseTrailers.AddMultiple(new (string key, string value)[2] {
                    ("Message", result.Message),
                    ("HttpStatusCode",((int)result.StatusCode).ToString())
                    });
                }
            }
            catch (Exception e)
            {
                logger.LogError(
                    "Could not schedule for deletion the Educational Institution with the request data: {0}, using {1}, error details => {2}",
                    JsonConvert.SerializeObject(request),
                    mediator.GetType(),
                    e.Message
                );

                context.Status = new(StatusCode.Aborted, "An error occurred while processing the request!");
                context.ResponseTrailers.AddMultiple(new (string key, string value)[2] {
                    ("Message", "An error occurred while processing the request!"),
                    ("HttpStatusCode", ((int)HttpStatusCode.InternalServerError).ToString())
                });
            }

            return new();
        }
    }
}