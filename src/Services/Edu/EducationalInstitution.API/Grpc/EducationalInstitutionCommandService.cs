using EducationalInstitutionAPI.Business.Validation_Handler;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Proto;
using EducationalInstitutionAPI.Utils;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
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
        /// <param name="request">A <see cref="DTOEducationalInstitutionCreateRequest"/> message as defined in the proto file</param>
        /// <returns>
        /// A <see cref="EducationalInstitutionCreateResponse">message</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.Created">Created</see> if operation is successful</item>
        /// <item><see cref="HttpStatusCode.BadRequest">BadRequest</see> if <paramref name="request"/>'s fields fail the validation process</item>
        /// <item><see cref="HttpStatusCode.MultiStatus">MultiStatus</see> </item>
        /// <item><see cref="HttpStatusCode.InternalServerError">InternalServerError</see> if the entity could not be inserted into the database</item>
        /// </list>
        /// </returns>
        public override async Task<EducationalInstitutionCreateResponse> CreateEducationalInstitution(DTOEducationalInstitutionCreateRequest request, ServerCallContext context)
        {
            logger.LogInformation("Begin grpc call EducationalInstitutionCommandService.CreateEducationalInstitution");

            if (request is null) throw new ArgumentNullException(nameof(request));
            if (context is null) throw new ArgumentNullException(nameof(context));

            var mappedRequest = mapRequestToDTOEducationalInstitutionCreateCommand(request);

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
                    context.Status = new(StatusCode.OK, "Educational Institution was successfully created!");
                    result.StatusCode.CanMapToProto(out HttpStatusCode protoStatusCode);

                    return new()
                    {
                        Data = new() { EducationalInstitutionId = result.Data.EducationalInstitutionID.ToProtocolBufferLanguageEquivalent() },
                        OperationStatus = result.OperationStatus,
                        StatusCode = protoStatusCode,
                        Message = result.Message
                    };
                }
                else
                    context.Status = new(StatusCode.Aborted, result.Message);
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

        private static DTOEducationalInstitutionCreateCommand mapRequestToDTOEducationalInstitutionCreateCommand(DTOEducationalInstitutionCreateRequest clientData)
        {
            return new()
            {
                Name = clientData.Name,
                Description = clientData.Description,
                LocationID = clientData.LocationId,
                BuildingsIDs = clientData.Buildings,
                ParentInstitutionID = ProtobufGuidConverter.DecodeGuid(clientData.ParentInstitutionId.High64, clientData.ParentInstitutionId.Low64)
            };
        }
    }
}