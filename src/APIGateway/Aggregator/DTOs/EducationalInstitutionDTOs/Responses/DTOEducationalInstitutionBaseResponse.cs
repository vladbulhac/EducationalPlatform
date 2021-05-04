using System;

namespace Aggregator.DTOs.EducationalInstitutionDTOs.Responses
{
    public record DTOEducationalInstitutionBaseResponse
    {
        public Guid EducationalInstitutionID { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }

        public DTOEducationalInstitutionBaseResponse() { }

        public DTOEducationalInstitutionBaseResponse(Guid educationalInstitutionID, string name, string description)
        {
            EducationalInstitutionID = educationalInstitutionID;
            Name = name;
            Description = description;
        }
    }
}