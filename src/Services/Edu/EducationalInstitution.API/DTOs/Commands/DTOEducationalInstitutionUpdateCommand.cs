using MediatR;
using System;

namespace EducationalInstitutionAPI.DTOs.Commands
{
    public class DTOEducationalInstitutionUpdateCommand : IRequest<Response>
    {
        public Guid EduInstitutionID { get; init; }

        public bool UpdateName { get; init; }
        public string Name { get; init; }

        public bool UpdateDescription { get; init; }
        public string Description { get; init; }
    }
}