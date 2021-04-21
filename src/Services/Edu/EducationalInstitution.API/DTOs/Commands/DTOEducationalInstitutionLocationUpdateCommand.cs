using MediatR;
using System;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.DTOs.Commands
{
    public class DTOEducationalInstitutionLocationUpdateCommand : IRequest<Response>
    {
        public Guid EducationalInstitutionID { get; init; }

        public bool UpdateLocation { get; init; }
        public string LocationID { get; init; }

        public bool UpdateBuildings { get; init; }
        public ICollection<string> AddBuildingsIDs { get; init; }
        public ICollection<string> RemoveBuildingsIDs { get; init; }
    }
}