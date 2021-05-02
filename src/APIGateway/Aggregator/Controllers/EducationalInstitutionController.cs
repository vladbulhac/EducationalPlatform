using Aggregator.DTOs;
using Aggregator.DTOs.EducationalInstitutionDTOs.Requests;
using Aggregator.DTOs.EducationalInstitutionDTOs.Responses;
using Aggregator.Services.EducationalInstitution;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aggregator.Controllers
{
    [ApiController]
    [Route("v1/api/edu")]
    public class EducationalInstitutionController : ControllerBase
    {
        private readonly IEducationalInstitutionCommandService eduInstitutionService;

        public EducationalInstitutionController(IEducationalInstitutionCommandService eduInstitutionService)
        {
            this.eduInstitutionService = eduInstitutionService;
        }

        [HttpPost]
        public async Task<ActionResult<Response<DTOGetEducationalInstitutionByIDResponse>>> CreateEducationalInstitution([FromBody] DTOCreateEducationalInstitutionRequest request)
        {
            var result = await eduInstitutionService.CreateEducationalInstitutionAsync(request);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}