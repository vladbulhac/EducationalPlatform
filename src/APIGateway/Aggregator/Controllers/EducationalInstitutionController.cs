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
            this.commandService = commandService;
            this.queryService = queryService;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response<DTOCreateEducationalInstitutionResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<DTOCreateEducationalInstitutionResponse>), StatusCodes.Status207MultiStatus)]
        [ProducesResponseType(typeof(Response<DTOCreateEducationalInstitutionResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<DTOCreateEducationalInstitutionResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<DTOCreateEducationalInstitutionResponse>>> CreateEducationalInstitutionAsync([FromBody] DTOCreateEducationalInstitutionRequest request)
        {
            var mappedRequest = request.MapToDTOEducationalInstitutionCreateRequest();

            var grpcCallResponse = await commandService.CreateEducationalInstitutionAsync(mappedRequest);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            return StatusCode((int)mappedResponse.StatusCode, mappedResponse);
        }

        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response<DTOGetEducationalInstitutionByIDResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<DTOGetEducationalInstitutionByIDResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<DTOGetEducationalInstitutionByIDResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<DTOGetEducationalInstitutionByIDResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<DTOGetEducationalInstitutionByIDResponse>>> GetEducationalInstitutionByIDAsync([FromRoute] Guid id)
        {
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var grpcCallResponse = await queryService.GetEducationalInstitutionByIDAsync(request);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            return StatusCode((int)mappedResponse.StatusCode, mappedResponse);
        }
    }
}