using Aggregator.DTOs;
using Aggregator.DTOs.EducationalInstitutionDTOs.Requests;
using Aggregator.DTOs.EducationalInstitutionDTOs.Responses;
using Aggregator.Services.EducationalInstitution;
using Aggregator.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aggregator.Controllers
{
    [ApiController]
    [Route("v1/api/edu")]
    public class EducationalInstitutionController : ControllerBase
    {
        private readonly IEducationalInstitutionCommandService educationalInstitutionService;

        public EducationalInstitutionController(IEducationalInstitutionCommandService educationalInstitutionService)
        {
            this.educationalInstitutionService = educationalInstitutionService;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response<DTOGetEducationalInstitutionByIDResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<DTOGetEducationalInstitutionByIDResponse>), StatusCodes.Status207MultiStatus)]
        [ProducesResponseType(typeof(Response<DTOGetEducationalInstitutionByIDResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<DTOGetEducationalInstitutionByIDResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<DTOGetEducationalInstitutionByIDResponse>>> CreateEducationalInstitutionAsync([FromBody] DTOCreateEducationalInstitutionRequest request)
        {
            var mappedRequest = request.MapToDTOEducationalInstitutionCreateRequest();

            var grpcCallResponse = await educationalInstitutionService.CreateEducationalInstitutionAsync(mappedRequest);

            var mappedResponse = grpcCallResponse.MapGrpcCallResponse();
            return StatusCode((int)mappedResponse.StatusCode, mappedResponse);
        }
    }
}