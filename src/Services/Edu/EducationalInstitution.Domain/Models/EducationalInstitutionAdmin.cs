using EducationalInstitution.Domain.Building_Blocks;
using System;
using Model = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.Domain.Models
{
    public class EducationalInstitutionAdmin : Entity
    {
        public string AdminId { get; init; }
        public Model::EducationalInstitution EducationalInstitution { get; init; }

        public EducationalInstitutionAdmin()
        {
        }

        public EducationalInstitutionAdmin(string adminID, Guid educationalInstitutionID) : base(educationalInstitutionID)
                => AdminId = adminID ?? throw new ArgumentException($"{nameof(adminID)} doesn't have a valid value!");
    }
}