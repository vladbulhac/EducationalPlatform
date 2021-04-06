﻿using EducationaInstitutionAPI.Business.Validation_Handler;
using EducationaInstitutionAPI.DTOs.Commands;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Commands;
using EducationaInstitutionAPI.Proto;
using EducationaInstitutionAPI.Utils;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EducationaInstitutionAPI.Grpc
{
    /// <summary>
    /// Implements the methods that handle the Remote Call Procedure requests
    /// </summary>
    public class EducationalInstitutionService : EducationalInstitution.EducationalInstitutionBase
    {
        private readonly IMediator mediator;
        private readonly ILogger logger;
        private readonly IValidationHandler validationHandler;

        public EducationalInstitutionService(IMediator mediator, ILogger<EducationalInstitutionService> logger, IValidationHandler validationHandler)
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
        /// <item><see cref="HttpStatusCode.Created">if operation is successful</see></item>
        /// <item><see cref="HttpStatusCode.BadRequest">if <paramref name="request"/>'s fields fail the validation process</see></item>
        /// <item><see cref="HttpStatusCode.InternalServerError">if the entity could not be inserted into the database</see></item>
        /// </list>
        /// </returns>
        public override async Task<EducationalInstitutionCreateResponse> CreateEducationalInstitution(DTOEducationalInstitutionCreateRequest request, ServerCallContext context)
        {
            logger.LogInformation("Begin grpc call EducationalInstitutionService.CreateEducationalInstitution");
            var requestDTO = new DTOEducationalInstitutionCreateCommand()
            {
                Name = request.Name,
                Description = request.Description,
                LocationID = request.LocationId,
                BuildingsIDs = request.Buildings
            };

            var responseObject = new ResponseObject();
            bool operationStatus = true;
            string message = string.Empty;
            var statusCode = HttpStatusCode.Created;

            if (validationHandler.IsRequestValid(requestDTO, out string validationErrors) == false)
            {
                context.Status = new(StatusCode.FailedPrecondition, validationErrors);

                responseObject = null;
                operationStatus = false;
                statusCode = HttpStatusCode.BadRequest;
                message = validationErrors;
            }
            else
            {
                var result = await mediator.Send(requestDTO);

                if (result.OperationStatus == false)
                {
                    context.Status = new(StatusCode.Aborted, result.Message);

                    responseObject = null;
                    operationStatus = result.OperationStatus;
                    statusCode = HttpStatusCode.InternalServerError;
                    message = result.Message;
                }
                else
                {
                    context.Status = new(StatusCode.OK, "Educational Institution was successfully created");

                    responseObject.EduInstitutionId = ConvertGuidToProtocolBufferLanguage(result.ResponseObject.EduInstitutionID);
                }
            }

            return new()
            {
                ResponseObject = responseObject,
                OperationStatus = operationStatus,
                StatusCode = statusCode,
                Message = message
            };
        }

        /// <summary>
        /// Overrides the auto generated Remote Call Procedure method from proto file, validates the request fields and sends it to the <see cref="Mediator"/> to handle it
        /// </summary>
        /// <param name="request">A <see cref="DTOEducationalInstitutionWithParentCreateRequest"/> message as defined in the proto file</param>
        /// <returns>
        /// A <see cref="EducationalInstitutionCreateResponse">message</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.Created">if operation is successful</see></item>
        /// <item><see cref="HttpStatusCode.BadRequest">if <paramref name="request"/>'s fields fail the validation process</see></item>
        /// <item><see cref="HttpStatusCode.InternalServerError">if the entity could not be inserted into the database</see></item>
        /// </list>
        /// </returns>
        public override async Task<EducationalInstitutionCreateResponse> CreateEducationalInstitutionWithParent(DTOEducationalInstitutionWithParentCreateRequest request, ServerCallContext context)
        {
            logger.LogInformation("Begin grpc call EducationalInstitutionService.CreateEducationalInstitution");
            var requestDTO = new DTOEducationalInstitutionWithParentCreateCommand()
            {
                Name = request.Name,
                Description = request.Description,
                LocationID = request.LocationId,
                BuildingsIDs = request.Buildings
            };

            var responseObject = new ResponseObject();
            bool operationStatus = true;
            string message = string.Empty;
            var statusCode = HttpStatusCode.Created;

            if (validationHandler.IsRequestValid(requestDTO, out string validationErrors) == false)
            {
                context.Status = new(StatusCode.FailedPrecondition, validationErrors);

                responseObject = null;
                operationStatus = false;
                statusCode = HttpStatusCode.BadRequest;
                message = validationErrors;
            }
            else
            {
                var result = await mediator.Send(requestDTO);

                if (result.OperationStatus == false)
                {
                    context.Status = new(StatusCode.Aborted, result.Message);

                    responseObject = null;
                    statusCode = HttpStatusCode.InternalServerError;
                }
                else
                {
                    context.Status = new(StatusCode.OK, "Educational Institution was successfully created");

                    if (!string.IsNullOrEmpty(result.Message))
                        statusCode = HttpStatusCode.MultiStatus;

                    responseObject.EduInstitutionId = ConvertGuidToProtocolBufferLanguage(result.ResponseObject.EduInstitutionID);
                }

                operationStatus = result.OperationStatus;
                message = result.Message;
            }

            return new()
            {
                ResponseObject = responseObject,
                OperationStatus = operationStatus,
                StatusCode = statusCode,
                Message = message
            };
        }

        private static Uuid ConvertGuidToProtocolBufferLanguage(Guid identifier)
        {
            ProtobufGuidConverter.EncodeGuid(identifier, out ulong High64, out ulong Low64);

            Uuid protobufID = new();
            protobufID.High64 = High64;
            protobufID.Low64 = Low64;

            return protobufID;
        }
    }
}