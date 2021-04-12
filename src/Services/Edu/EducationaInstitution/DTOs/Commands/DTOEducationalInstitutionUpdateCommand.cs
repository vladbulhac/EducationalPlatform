using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationaInstitutionAPI.DTOs.Commands
{
    public class DTOEducationalInstitutionUpdateCommand
    {
        public Guid EduInstitutionID { get; init; }

        public bool UpdateName { get; init; }
        public string Name { get; init; }

        public bool UpdateDescription { get; init; }
        public string Description { get; init; }
    }
}