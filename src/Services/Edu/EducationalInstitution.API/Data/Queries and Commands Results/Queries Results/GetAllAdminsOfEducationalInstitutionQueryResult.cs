using System;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results
{
    public record GetAllAdminsOfEducationalInstitutionQueryResult
    {
        public ICollection<Guid> AdminsIDs { get; init; }
    }
}