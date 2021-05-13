using EducationalInstitutionAPI.Business.Queries_Handlers;
using EducationalInstitutionAPI.Business.Validation_Handler;
using EducationalInstitutionAPI.Proto;
using EducationalInstitutionAPI.Utils;
using EducationalInstitutionAPI.Utils.Mappers;
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

        /// <summary>
        /// Overrides the auto generated Remote Call Procedure method from proto file, validates the request fields and sends it to the <see cref="Mediator"/> to handle it
        /// </summary>
        /// <returns>
        /// In addition to the returned <see cref="HttpStatusCode">HttpStatusCodes</see> by <see cref="GetEducationalInstitutionByIDQueryHandler">handler</see>:
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
        public override async Task<EducationalInstitutionGetResponse> GetEducationalInstitutionByID(EducationalInstitutionGetByIdRequest request, ServerCallContext context)
        {
            logger.LogInformation("Begin grpc call EducationalInstitutionQueryService.GetEducationalInstitutionByID");

            if (request is null) throw new ArgumentNullException(nameof(request));
            if (context is null) throw new ArgumentNullException(nameof(context));

            var mappedRequest = request.MapToDTOEducationalInstitutionByIDQuery();

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
                    context.Status = new(StatusCode.OK, "Educational Institution with given ID was found!");

                    return new()
                    {
                        Data = result.Data.MapToEducationalInstitutionGetResponse(),
                        OperationStatus = result.OperationStatus,
                        StatusCode = result.StatusCode.MapToEquivalentProtoHttpStatusCodeOrOK(),
                        Message = result.Message
                    };
                }
                else
                if (result.StatusCode == HttpStatusCode.NotFound)
                    SetStatusAndTrailersOfContext(ref context, StatusCode.NotFound, result.Message, result.StatusCode);
                else
                    SetStatusAndTrailersOfContext(ref context, StatusCode.Aborted, result.Message, result.StatusCode);
            }
            catch (Exception e)
            {
                HandleException(
                            logger,
                            ref context,
                           "Could not get the Educational Institution with the request data: {0}, using {1}, error details => {2}",
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
        /// In addition to the returned <see cref="HttpStatusCode">HttpStatusCodes</see> by <see cref="GetEducationalInstitutionByIDQueryHandler">handler</see>:
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
        public override async Task<EducationalInstitutionGetByNameResponse> GetAllEducationalInstitutionsByName(EducationalInstitutionGetByNameRequest request, ServerCallContext context)
        {
            logger.LogInformation("Begin grpc call EducationalInstitutionQueryService.GetAllEducationalInstitutionsByName");

            if (request is null) throw new ArgumentNullException(nameof(request));
            if (context is null) throw new ArgumentNullException(nameof(context));

            var mappedRequest = request.MapToDTOEducationalInstitutionsByNameQuery();

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
                    context.Status = new(StatusCode.OK, "Successfully retrieved Educational Institutions!");

                    return new()
                    {
                        Data = { result.Data.MapToGetByNameResult() },
                        OperationStatus = result.OperationStatus,
                        StatusCode = result.StatusCode.MapToEquivalentProtoHttpStatusCodeOrOK(),
                        Message = result.Message
                    };
                }
                else
                if (result.StatusCode == HttpStatusCode.NotFound)
                    SetStatusAndTrailersOfContext(ref context, StatusCode.NotFound, result.Message, result.StatusCode);
                else
                    SetStatusAndTrailersOfContext(ref context, StatusCode.Aborted, result.Message, result.StatusCode);
            }
            catch (Exception e)
            {
                HandleException(
                    logger,
                    ref context,
                    "Could not get any Educational Institution with the request data: {0}, using {1}, error details => {2}",
                    JsonConvert.SerializeObject(request),
                    mediator.GetType(),
                    e.Message);
            }

            return new();
        }
    }
}