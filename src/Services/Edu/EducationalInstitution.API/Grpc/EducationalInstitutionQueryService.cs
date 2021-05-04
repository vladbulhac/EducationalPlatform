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

        public override async Task<EducationalInstitutionGetResponse> GetEducationalInstitutionByID(EducationalInstitutionGetByIdRequest request, ServerCallContext context)
        {
            logger.LogInformation("Begin grpc call EducationalInstitutionQueryService.GetEducationalInstitutionByID");

            if (request is null) throw new ArgumentNullException(nameof(request));
            if (context is null) throw new ArgumentNullException(nameof(context));

            var mappedRequest = request.MapToDTOEducationalInstitutionByIDQuery();

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
    }
}