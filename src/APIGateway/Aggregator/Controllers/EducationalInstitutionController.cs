using Aggregator.Authorization;
using Aggregator.Authorization.Policies;
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
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class EducationalInstitutionController : ControllerBase
    {
        private readonly IEducationalInstitutionCommandService commandService;
        private readonly IEducationalInstitutionQueryService queryService;
        private readonly IRequestAuthorizationService authorizationService;

        public EducationalInstitutionController(IEducationalInstitutionCommandService commandService, IEducationalInstitutionQueryService queryService, IRequestAuthorizationService authorizationService)
        {
            this.commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));
            this.queryService = queryService ?? throw new ArgumentNullException(nameof(queryService));
            this.authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ForbidResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<CreateEducationalInstitutionResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<CreateEducationalInstitutionResponse>), StatusCodes.Status207MultiStatus)]
        [ProducesResponseType(typeof(Response<CreateEducationalInstitutionResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<CreateEducationalInstitutionResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<CreateEducationalInstitutionResponse>>> CreateAsync([FromBody] DTOCreateEducationalInstitutionRequest data)
        {
            if (!authorizationService.IsRequestValid(User, new CreateEducationalInstitutionPolicy(), out ActionResult authorizationResponse))
                return authorizationResponse;

            var mappedRequest = data.MapToEducationalInstitutionCreateRequest();

            var grpcCallResponse = await commandService.CreateEducationalInstitutionAsync(mappedRequest);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            return StatusCode((int)mappedResponse.StatusCode, mappedResponse);
        }

        [HttpGet("{id}"), AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response<GetEducationalInstitutionByIDResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<GetEducationalInstitutionByIDResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<GetEducationalInstitutionByIDResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<GetEducationalInstitutionByIDResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<GetEducationalInstitutionByIDResponse>>> GetByIDAsync([FromRoute] Guid id)
        {
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var grpcCallResponse = await queryService.GetEducationalInstitutionByIDAsync(request);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            return StatusCode((int)mappedResponse.StatusCode, mappedResponse);
        }

        [HttpGet("location/{locationID}"), AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response<GetAllEducationalInstitutionsByLocationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<GetAllEducationalInstitutionsByLocationResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<GetAllEducationalInstitutionsByLocationResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<GetAllEducationalInstitutionsByLocationResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<GetAllEducationalInstitutionsByLocationResponse>>> GetAllByLocationAsync([FromRoute] string locationID)
        {
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = locationID };

            var grpcCallResponse = await queryService.GetAllEducationalInstitutionsByLocationAsync(request);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            return StatusCode((int)mappedResponse.StatusCode, mappedResponse);
        }

        [HttpGet("location/buildings/{buildingID}"), AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response<GetAllEducationalInstitutionsByBuildingResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<GetAllEducationalInstitutionsByBuildingResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<GetAllEducationalInstitutionsByBuildingResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<GetAllEducationalInstitutionsByBuildingResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<GetAllEducationalInstitutionsByBuildingResponse>>> GetAllByBuildingAsync([FromRoute] string buildingID)
        {
            EducationalInstitutionsGetByBuildingRequest request = new() { BuildingId = buildingID };

            var grpcCallResponse = await queryService.GetAllEducationalInstitutionsByBuildingAsync(request);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            return StatusCode((int)mappedResponse.StatusCode, mappedResponse);
        }

        [HttpGet("{name}&{offset}&{results}"), AllowAnonymous]
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

        [HttpGet("{id}/admins"), AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response<GetAllAdminsByEducationalInstitutionIDResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<GetAllAdminsByEducationalInstitutionIDResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<GetAllAdminsByEducationalInstitutionIDResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<GetAllAdminsByEducationalInstitutionIDResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<GetAllAdminsByEducationalInstitutionIDResponse>>> GetAllAdminsByEducationalInstitutionIDAsync([FromRoute] Guid id)
        {
            AdminsGetByEducationalInstitutionIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var grpcCallResponse = await queryService.GetAllAdminsByEducationalInstitutionIDAsync(request);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            return StatusCode((int)mappedResponse.StatusCode, mappedResponse);
        }

        [HttpPatch("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response>> UpdateAsync([FromRoute] Guid id, [FromBody] DTOUpdateEducationalInstitutionRequest data)
        {
            EducationalInstitutionUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
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

        [HttpPatch("{id}/location")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response>> UpdateLocationAsync([FromRoute] Guid id, [FromBody] DTOUpdateEducationalInstitutionLocationRequest data)
        {
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
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

        [HttpPatch("{id}/parent")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response>> UpdateParentAsync([FromRoute] Guid id, [FromBody] DTOUpdateEducationalInstitutionParentRequest data)
        {
            EducationalInstitutionParentUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                ParentInstitutionId = data.ParentInstitutionID.ToProtoUuid()
            };

            var grpcCallResponse = await commandService.UpdateEducationalInstitutionParentAsync(request);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            if ((int)mappedResponse.StatusCode is not 204)
                return StatusCode((int)mappedResponse.StatusCode, mappedResponse);

            return NoContent();
        }

        [HttpPatch("{id}/admins")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response>> UpdateAdminsAsync([FromRoute] Guid id, [FromBody] DTOUpdateEducationalInstitutionAdminRequest data)
        {
            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                AddAdminsIds = { data.AddAdminsIDs },
                RemoveAdminsIds = { data.RemoveAdminsIDs }
            };

            var grpcCallResponse = await commandService.UpdateEducationalInstitutionAdminAsync(request);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            if ((int)mappedResponse.StatusCode is not 204)
                return StatusCode((int)mappedResponse.StatusCode, mappedResponse);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response<DeleteEducationalInstitutionResponse>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(Response<DeleteEducationalInstitutionResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<DeleteEducationalInstitutionResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<DeleteEducationalInstitutionResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<DeleteEducationalInstitutionResponse>>> DeleteAsync([FromRoute] Guid id)
        {
            if (!authorizationService.IsRequestValid(User, new DeleteEducationalInstitutionPolicy(), out ActionResult authorizationResponse))
                return authorizationResponse;

            EducationalInstitutionDeleteRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var grpcCallResponse = await commandService.DeleteEducationalInstitutionAsync(request);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            return StatusCode((int)mappedResponse.StatusCode, mappedResponse);
        }
    }
}