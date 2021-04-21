using EducationalInstitutionAPI.Business.Validation_Handler;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Controllers
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
            var request = new DTOEducationalInstitutionByIDQuery() { EducationalInstitutionID = ID };

            if (!validationHandler.IsRequestValid(request, out string validationErrors))
                return BadRequest(new Response<GetEducationalInstitutionByIDQueryResult>()
                {
                    Data = null,
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
                    Data = null,
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