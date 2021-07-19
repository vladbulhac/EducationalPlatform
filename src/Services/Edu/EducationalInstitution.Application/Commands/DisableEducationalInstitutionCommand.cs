using EducationalInstitution.Application.Commands.Results;
using MediatR;
using System;

namespace EducationalInstitution.Application.Commands
{
    public class DisableEducationalInstitutionCommand : IRequest<Response<DisableEducationalInstitutionCommandResult>>
    {
        public Guid EducationalInstitutionID { get; init; }
    }
}