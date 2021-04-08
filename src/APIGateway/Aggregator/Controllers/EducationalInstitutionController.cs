using Aggregator.Models.DTOs;
using Aggregator.Services;
using EducationaInstitutionAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Aggregator.Controllers
{
    [ApiController]
    [Route("v1/api/edu")]
    public class EducationalInstitutionController : ControllerBase
    {
        private readonly IEducationalInstitutionService eduInstitutionService;

        public EducationalInstitutionController(IEducationalInstitutionService eduInstitutionService)
        {
            this.eduInstitutionService = eduInstitutionService;
        }

        [HttpPost]
        public async Task<ActionResult<Response<Guid>>> CreateEducationalInstitution([FromBody] DTOCreateEducationalInstitution request)
        {
            var result = await eduInstitutionService.CreateEducationalInstiutionAsync(request);
            return result;
        }
    }
}