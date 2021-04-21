using MediatR;
using System;

namespace EducationalInstitutionAPI.DTOs.Commands
{
    public class DTOEducationalInstitutionParentUpdateCommand : IRequest<Response>
    {
        public Guid EducationalInstitutionID { get; init; }
        public Guid ParentInstitutionID { get; init; }
    }
}