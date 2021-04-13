﻿using System;

namespace EducationaInstitutionAPI.Data
{
    /// <summary>
    /// Defines an association between a Building and an <see cref="Data.EducationalInstitution"/>
    /// </summary>
    public class EduInstitutionBuilding
    {
        public Guid EducationalInstitutionID { get; init; }
        public EducationalInstitution EducationalInstitution { get; init; }
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