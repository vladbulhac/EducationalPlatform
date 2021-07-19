using EducationalInstitution.Domain.Building_Blocks;
using System;
using Model = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.Domain.Models
{
    public class EducationalInstitutionBuilding : Entity
    {
        public Model::EducationalInstitution EducationalInstitution { get; init; }
        public string BuildingID { get; init; }

        public EducationalInstitutionBuilding()
        {
        }

        public EducationalInstitutionBuilding(string buildingID, Guid educationalInstitutionID) : base(educationalInstitutionID)
                => BuildingID = string.IsNullOrEmpty(buildingID) ? throw new ArgumentNullException(nameof(buildingID)) : buildingID;
    }
}