using System.Collections.Generic;

namespace Aggregator.Models.DTOs
{
    public record DTOCreateEducationalInstitution
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string LocationID { get; init; }
        public ICollection<string> BuildingsIDs { get; init; }
    }
}