namespace Aggregator.Models.DTOs.EducationalInstitutionDTOs.Requests
{
    public record DTOUpdateEducationalInstitutionRequest
    {
        public bool UpdateName { get; init; }
        public string Name { get; init; }

        public bool UpdateDescription { get; init; }
        public string Description { get; init; }
    }
}