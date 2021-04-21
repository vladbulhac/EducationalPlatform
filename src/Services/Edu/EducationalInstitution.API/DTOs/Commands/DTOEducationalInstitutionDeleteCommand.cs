using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Commands_Results;
using MediatR;
using System;

namespace EducationalInstitutionAPI.DTOs.Commands
{
    public class DTOEducationalInstitutionDeleteCommand : IRequest<Response<DeleteEducationalInstitutionCommandResult>>
    {
        public Guid EducationalInstitutionID { get; init; }
    }
}