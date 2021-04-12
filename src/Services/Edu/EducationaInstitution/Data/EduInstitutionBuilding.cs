using System;

namespace EducationaInstitutionAPI.Data
{
    /// <summary>
    /// Defines an association between a Building and an <see cref="EduInstitution"/>
    /// </summary>
    public class EduInstitutionBuilding
    {
        public Guid EducationalInstitutionID { get; init; }
        public EduInstitution EducationalInstitution { get; init; }
        public string BuildingID { get; init; }
        public Access EntityAccess { get; private set; }

        public EduInstitutionBuilding()
        {
        }

        public EduInstitutionBuilding(string buildingID, Guid eduInstitutionID)
        {
            BuildingID = buildingID;
            EducationalInstitutionID = eduInstitutionID;
        }
    }
}