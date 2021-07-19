using System;

namespace EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results
{
    public record EducationalInstitutionBaseQueryResult
    {
        public Guid EducationalInstitutionID { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }

        public EducationalInstitutionBaseQueryResult() { }

        public EducationalInstitutionBaseQueryResult(Guid educationalInstitutionID, string name, string description)
        {
            EducationalInstitutionID = educationalInstitutionID;
            Name = name;
            Description = description;
        }
    }
}