using System;

namespace Aggregator.Models.DTOs.EducationalInstitutionDTOs.Responses
{
    public record EducationalInstitutionBaseResponse
    {
        public Guid EducationalInstitutionID { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }

        public EducationalInstitutionBaseResponse() { }

        public EducationalInstitutionBaseResponse(Guid educationalInstitutionID, string name, string description)
        {
            EducationalInstitutionID = educationalInstitutionID;
            Name = name;
            Description = description;
        }
    }
}