using EducationaInstitutionAPI.Business.Validation_Handler;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.In;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using EducationaInstitutionAPI.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EducationaInstitutionAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EducationalInstitutionController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IValidationHandler validationHandler;

        public EducationalInstitutionController(IMediator mediator, IValidationHandler validationHandler)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.validationHandler = validationHandler ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<Response<GetEducationalInstitutionByIDQueryResult>>> GetEducationalInstitutionByID(Guid ID)
        {
            var request = new DTOEducationalInstitutionByIDQuery(ID);

            if (!validationHandler.IsRequestValid(request, out string validationErrors))
                return BadRequest(new Response<GetEducationalInstitutionByIDQueryResult>()
                {
                    ResponseObject = null,
                    OperationStatus = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = validationErrors
                });

            var queryResult = await mediator.Send(request);

            if (!queryResult.OperationStatus)
                return NotFound(queryResult);

            return Ok(queryResult);
        }

        [HttpGet("{name}&{offsetValue}&{resultsCount}")]
        public async Task<ActionResult<Response<GetEducationalInstitutionQueryResult>>> GetEducationalInstitutionsWithName(string name, int offsetValue, int resultsCount)
        {
            var request = new DTOEducationalInstitutionsByNameQuery() { Name = name, ResultsCount = resultsCount, OffsetValue = offsetValue };

            if (!validationHandler.IsRequestValid(request, out string validationErrors))
                return BadRequest(new Response<GetEducationalInstitutionByIDQueryResult>()
                {
                    ResponseObject = null,
                    OperationStatus = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = validationErrors
                });

            var queryResult = await mediator.Send(request);

            if (!queryResult.OperationStatus)
                return NotFound(queryResult);

            return Ok(queryResult);
        }
    }
}