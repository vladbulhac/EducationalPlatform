using EducationalInstitution.Domain.Building_Blocks;
using System;
using Model = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.Domain.Models
{
    public class EducationalInstitutionAdmin : Entity
    {
        public Guid AdminID { get; init; }
        public Model::EducationalInstitution EducationalInstitution { get; init; }

        public EducationalInstitutionAdmin()
        {
        }

        public EducationalInstitutionAdmin(Guid adminID, Guid educationalInstitutionID) : base(educationalInstitutionID)
                => AdminID = adminID == default ? throw new ArgumentException($"{nameof(adminID)} doesn't have a valid value!") : adminID;
    }
}