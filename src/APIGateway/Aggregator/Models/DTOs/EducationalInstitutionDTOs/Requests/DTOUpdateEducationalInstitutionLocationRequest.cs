using System.Collections.Generic;

namespace Aggregator.Models.DTOs.EducationalInstitutionDTOs.Requests
{
    public record DTOUpdateEducationalInstitutionLocationRequest
    {
        public bool UpdateLocation { get; init; }
        public string LocationID { get; init; }

        public bool UpdateBuildings { get; init; }
        public ICollection<string> AddBuildingsIDs { get; init; }
        public ICollection<string> RemoveBuildingsIDs { get; init; }
    }
}