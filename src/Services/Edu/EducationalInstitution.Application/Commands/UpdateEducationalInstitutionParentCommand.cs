using MediatR;
using System;

namespace EducationalInstitution.Application.Commands
{
    public class UpdateEducationalInstitutionParentCommand : IRequest<Response>
    {
        public Guid EducationalInstitutionID { get; init; }
        public Guid ParentInstitutionID { get; init; }
    }
}