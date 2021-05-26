using Aggregator.DTOs;
using Aggregator.DTOs.EducationalInstitutionDTOs.Requests;
using Aggregator.DTOs.EducationalInstitutionDTOs.Responses;
using Aggregator.EducationalInstitutionAPI.Proto;
using Aggregator.Services.EducationalInstitution;
using Aggregator.Utils;
using EducationaInstitutionAPI.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Aggregator.Controllers
{
    [ApiController]
    [Route("v1/api/edu")]
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
        [ProducesResponseType(typeof(Response<CreateEducationalInstitutionResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<CreateEducationalInstitutionResponse>), StatusCodes.Status207MultiStatus)]
        [ProducesResponseType(typeof(Response<CreateEducationalInstitutionResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<CreateEducationalInstitutionResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<CreateEducationalInstitutionResponse>>> CreateEducationalInstitutionAsync([FromBody] DTOCreateEducationalInstitutionRequest request)
        {
            var mappedRequest = request.MapToEducationalInstitutionCreateRequest();

            var grpcCallResponse = await commandService.CreateEducationalInstitutionAsync(mappedRequest);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            return StatusCode((int)mappedResponse.StatusCode, mappedResponse);
        }

        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response<GetEducationalInstitutionByIDResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<GetEducationalInstitutionByIDResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<GetEducationalInstitutionByIDResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<GetEducationalInstitutionByIDResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<GetEducationalInstitutionByIDResponse>>> GetEducationalInstitutionByIDAsync([FromRoute] Guid id)
        {
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var grpcCallResponse = await queryService.GetEducationalInstitutionByIDAsync(request);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            return StatusCode((int)mappedResponse.StatusCode, mappedResponse);
        }

        [HttpGet]
        [Route("location/{locationID}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response<GetEducationalInstitutionByIDResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<GetEducationalInstitutionByIDResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<GetEducationalInstitutionByIDResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<GetEducationalInstitutionByIDResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<GetAllEducationalInstitutionsByLocationResponse>>> GetAllEducationalInstitutionsByLocationAsync([FromRoute] string locationID)
        {
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = locationID };

            var grpcCallResponse = await queryService.GetAllEducationalInstitutionsByLocationAsync(request);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            return StatusCode((int)mappedResponse.StatusCode, mappedResponse);
        }

        [HttpGet("{name}&{offset}&{results}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response<GetAllEducationalInstitutionsByNameResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<GetAllEducationalInstitutionsByNameResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<GetAllEducationalInstitutionsByNameResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<GetAllEducationalInstitutionsByNameResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<GetAllEducationalInstitutionsByNameResponse>>> GetAllEducationalInstitutionsByNameAsync([FromRoute] string name, [FromRoute] int offset, [FromRoute] int results)
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

        [HttpPatch]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response>> UpdateEducationalInstitutionAsync([FromRoute] Guid id, [FromBody] DTOUpdateEducationalInstitutionRequest request)
        {
            EducationalInstitutionUpdateRequest mappedRequest = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateName = request.UpdateName,
                Name = request.Name,
                UpdateDescription = request.UpdateDescription,
                Description = request.Description
            };

            var grpcCallResponse = await commandService.UpdateEducationalInstitutionAsync(mappedRequest);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            if ((int)mappedResponse.StatusCode is not 204)
                return StatusCode((int)mappedResponse.StatusCode, mappedResponse);

            return NoContent();
        }

        [HttpPatch]
        [Route("{id}/location")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response>> UpdateEducationalInstitutionLocationAsync([FromRoute] Guid id, [FromBody] DTOUpdateEducationalInstitutionLocationRequest request)
        {
            EducationalInstitutionLocationUpdateRequest mappedRequest = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateLocation = request.UpdateLocation,
                LocationId = request.LocationID,
                UpdateBuildings = request.UpdateBuildings,
                AddBuildingsIds = { request.AddBuildingsIDs },
                RemoveBuildingsIds = { request.RemoveBuildingsIDs }
            };

            var grpcCallResponse = await commandService.UpdateEducationalInstitutionLocationAsync(mappedRequest);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            if ((int)mappedResponse.StatusCode is not 204)
                return StatusCode((int)mappedResponse.StatusCode, mappedResponse);

            return NoContent();
        }

        [HttpPatch]
        [Route("{id}/parent")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response>> UpdateEducationalInstitutionParentAsync([FromRoute] Guid id, [FromBody] DTOUpdateEducationalInstitutionParentRequest request)
        {
            EducationalInstitutionParentUpdateRequest mappedRequest = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                ParentInstitutionId = request.ParentInstitutionID.ToProtoUuid()
            };

            var grpcCallResponse = await commandService.UpdateEducationalInstitutionParentAsync(mappedRequest);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            if ((int)mappedResponse.StatusCode is not 204)
                return StatusCode((int)mappedResponse.StatusCode, mappedResponse);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response<DeleteEducationalInstitutionResponse>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(Response<DeleteEducationalInstitutionResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<DeleteEducationalInstitutionResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<DeleteEducationalInstitutionResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<DeleteEducationalInstitutionResponse>>> DeleteEducationalInstitutionAsync([FromRoute] Guid id)
        {
            EducationalInstitutionDeleteRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var grpcCallResponse = await commandService.DeleteEducationalInstitutionAsync(request);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            return StatusCode((int)mappedResponse.StatusCode, mappedResponse);
        }
    }
}