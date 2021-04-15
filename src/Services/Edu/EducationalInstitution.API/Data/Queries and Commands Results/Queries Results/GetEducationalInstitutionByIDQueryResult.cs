using System;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results
{
    /// <summary>
    /// Defines the data that is returned as the result of a Get by ID operation
    /// </summary>
    public record GetEducationalInstitutionByIDQueryResult
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string LocationID { get; init; }
        public DateTime JoinDate { get; init; }
        public EducationalInstitution ParentInstitution { get; init; }
        public ICollection<EducationalInstitutionBuilding> BuildingsIDs { get; init; }
        public ICollection<EducationalInstitution> ChildInstitutions { get; init; }
    }
}