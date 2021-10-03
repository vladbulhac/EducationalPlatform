using Aggregator.EducationalInstitutionAPI.Proto;
using Aggregator.Models.DTOs;
using Aggregator.Models.DTOs.EducationalInstitutionDTOs.Requests;
using Aggregator.Models.DTOs.EducationalInstitutionDTOs.Responses;
using Aggregator.Models.ObjectMappers;
using Aggregator.Services.EducationalInstitution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aggregator.Controllers
{
    [ApiController]
    [Route("api/v1/edu")]
    public class EducationalInstitutionController : ControllerBase
    {
        private readonly IEducationalInstitutionCommandService commandService;
        private readonly IEducationalInstitutionQueryService queryService;

        public EducationalInstitutionController(IEducationalInstitutionCommandService commandService, IEducationalInstitutionQueryService queryService)
        {
            this.commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));
            this.queryService = queryService ?? throw new ArgumentNullException(nameof(queryService));
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ForbidResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<CreateEducationalInstitutionResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<CreateEducationalInstitutionResponse>), StatusCodes.Status207MultiStatus)]
        [ProducesResponseType(typeof(Response<CreateEducationalInstitutionResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<CreateEducationalInstitutionResponse>), StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme, Policy = "CreateEducationalInstitutionPolicy")]
        public async Task<ActionResult<Response<CreateEducationalInstitutionResponse>>> CreateAsync([FromBody] DTOCreateEducationalInstitutionRequest data)
        {
            var mappedRequest = data.MapToEducationalInstitutionCreateRequest();

            var grpcCallResponse = await commandService.CreateEducationalInstitutionAsync(mappedRequest);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            return StatusCode((int)mappedResponse.StatusCode, mappedResponse);
        }

        [HttpGet("{resourceId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response<GetEducationalInstitutionByIDResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<GetEducationalInstitutionByIDResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<GetEducationalInstitutionByIDResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<GetEducationalInstitutionByIDResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<GetEducationalInstitutionByIDResponse>>> GetByIDAsync([FromRoute] Guid resourceId)
        {
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = resourceId.ToProtoUuid() };

            var grpcCallResponse = await queryService.GetEducationalInstitutionByIDAsync(request);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            return StatusCode((int)mappedResponse.StatusCode, mappedResponse);
        }

        [HttpGet("location/{resourceId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response<GetAllEducationalInstitutionsByLocationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<GetAllEducationalInstitutionsByLocationResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<GetAllEducationalInstitutionsByLocationResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<GetAllEducationalInstitutionsByLocationResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<GetAllEducationalInstitutionsByLocationResponse>>> GetAllByLocationAsync([FromRoute] string resourceId)
        {
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = resourceId };

            var grpcCallResponse = await queryService.GetAllEducationalInstitutionsByLocationAsync(request);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            return StatusCode((int)mappedResponse.StatusCode, mappedResponse);
        }

        [HttpGet("location/buildings/{resourceId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response<GetAllEducationalInstitutionsByBuildingResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<GetAllEducationalInstitutionsByBuildingResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<GetAllEducationalInstitutionsByBuildingResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<GetAllEducationalInstitutionsByBuildingResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<GetAllEducationalInstitutionsByBuildingResponse>>> GetAllByBuildingAsync([FromRoute] string resourceId)
        {
            EducationalInstitutionsGetByBuildingRequest request = new() { BuildingId = resourceId };

            var grpcCallResponse = await queryService.GetAllEducationalInstitutionsByBuildingAsync(request);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            return StatusCode((int)mappedResponse.StatusCode, mappedResponse);
        }

        [HttpGet("{name}&{offset}&{results}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response<GetAllEducationalInstitutionsByNameResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<GetAllEducationalInstitutionsByNameResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<GetAllEducationalInstitutionsByNameResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<GetAllEducationalInstitutionsByNameResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<GetAllEducationalInstitutionsByNameResponse>>> GetAllByNameAsync([FromRoute] string name, [FromRoute] int offset, [FromRoute] int results)
        {
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = name,
                OffsetValue = offset,
                ResultsCount = results
            };

            var grpcCallResponse = await queryService.GetAllEducationalInstitutionsByNameAsync(request);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            return StatusCode((int)mappedResponse.StatusCode, mappedResponse);
        }

        [HttpGet("{resourceId}/admins")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response<GetAllAdminsByEducationalInstitutionIDResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<GetAllAdminsByEducationalInstitutionIDResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<GetAllAdminsByEducationalInstitutionIDResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<GetAllAdminsByEducationalInstitutionIDResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<GetAllAdminsByEducationalInstitutionIDResponse>>> GetAllAdminsByEducationalInstitutionIDAsync([FromRoute] Guid resourceId)
        {
            AdminsGetByEducationalInstitutionIdRequest request = new() { EducationalInstitutionId = resourceId.ToProtoUuid() };

            var grpcCallResponse = await queryService.GetAllAdminsByEducationalInstitutionIDAsync(request);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            return StatusCode((int)mappedResponse.StatusCode, mappedResponse);
        }

        [HttpPatch("{resourceId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response>> UpdateAsync([FromRoute] Guid resourceId, [FromBody] DTOUpdateEducationalInstitutionRequest data)
        {
            EducationalInstitutionUpdateRequest request = new()
            {
                EducationalInstitutionId = resourceId.ToProtoUuid(),
                UpdateName = data.UpdateName,
                Name = data.Name,
                UpdateDescription = data.UpdateDescription,
                Description = data.Description
            };

            var grpcCallResponse = await commandService.UpdateEducationalInstitutionAsync(request);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            if ((int)mappedResponse.StatusCode is not 204)
                return StatusCode((int)mappedResponse.StatusCode, mappedResponse);

            return NoContent();
        }

        [HttpPatch("{resourceId}/location")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response>> UpdateLocationAsync([FromRoute] Guid resourceId, [FromBody] DTOUpdateEducationalInstitutionLocationRequest data)
        {
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = resourceId.ToProtoUuid(),
                UpdateLocation = data.UpdateLocation,
                LocationId = data.LocationID,
                UpdateBuildings = data.UpdateBuildings,
                AddBuildingsIds = { data.AddBuildingsIDs },
                RemoveBuildingsIds = { data.RemoveBuildingsIDs }
            };

            var grpcCallResponse = await commandService.UpdateEducationalInstitutionLocationAsync(request);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            if ((int)mappedResponse.StatusCode is not 204)
                return StatusCode((int)mappedResponse.StatusCode, mappedResponse);

            return NoContent();
        }

        [HttpPatch("{resourceId}/parent")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response>> UpdateParentAsync([FromRoute] Guid resourceId, [FromBody] DTOUpdateEducationalInstitutionParentRequest data)
        {
            EducationalInstitutionParentUpdateRequest request = new()
            {
                EducationalInstitutionId = resourceId.ToProtoUuid(),
                ParentInstitutionId = data.ParentInstitutionID.ToProtoUuid()
            };

            var grpcCallResponse = await commandService.UpdateEducationalInstitutionParentAsync(request);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            if ((int)mappedResponse.StatusCode is not 204)
                return StatusCode((int)mappedResponse.StatusCode, mappedResponse);

            return NoContent();
        }

        [HttpPatch("{resourceId}/admins")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response>> UpdateAdminsAsync([FromRoute] Guid resourceId, [FromBody] DTOUpdateEducationalInstitutionAdminRequest data)
        {
            var request = data.MapToEducationalInstitutionAdminUpdateRequest(resourceId);

            var grpcCallResponse = await commandService.UpdateEducationalInstitutionAdminAsync(request);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            if ((int)mappedResponse.StatusCode is not 204)
                return StatusCode((int)mappedResponse.StatusCode, mappedResponse);

            return NoContent();
        }

        [HttpDelete("{resourceId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response<DeleteEducationalInstitutionResponse>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(Response<DeleteEducationalInstitutionResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<DeleteEducationalInstitutionResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<DeleteEducationalInstitutionResponse>), StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme, Policy = "DeleteEducationalInstitutionPolicy")]
        public async Task<ActionResult<Response<DeleteEducationalInstitutionResponse>>> DeleteAsync([FromRoute] Guid resourceId)
        {
            EducationalInstitutionDeleteRequest request = new() { EducationalInstitutionId = resourceId.ToProtoUuid() };

            var grpcCallResponse = await commandService.DeleteEducationalInstitutionAsync(request);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            return StatusCode((int)mappedResponse.StatusCode, mappedResponse);
        }
    }
}