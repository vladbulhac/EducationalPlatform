using EducationalInstitutionAPI.Data.Helpers;
using System;

namespace EducationalInstitutionAPI.Data
{
    /// <summary>
    /// Defines an association between a Building and an <see cref="EducationalInstitution"/>
    /// </summary>
    public class EducationalInstitutionBuilding
    {
        public Guid EducationalInstitutionID { get; init; }
        public EducationalInstitution EducationalInstitution { get; init; }
        public string BuildingID { get; init; }
        public Access EntityAccess { get; private set; }

        public EducationalInstitutionBuilding()
        {
        }

        public EducationalInstitutionBuilding(string buildingID, Guid educationalInstitutionID)
        {
            BuildingID = buildingID;
            EducationalInstitutionID = educationalInstitutionID;
        }
    }
}