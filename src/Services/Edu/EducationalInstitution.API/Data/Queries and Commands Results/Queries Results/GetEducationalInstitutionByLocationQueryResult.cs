﻿using System;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results
{
    public record GetEducationalInstitutionByLocationQueryResult
    {
        public Guid EducationalInstitutionID { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public ICollection<EducationalInstitutionBuilding> BuildingsIDs { get; init; }
    }
}