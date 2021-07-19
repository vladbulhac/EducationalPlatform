using System;
using System.Collections.Generic;

namespace EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results
{
    public record GetAllAdminsOfEducationalInstitutionQueryResult
    {
        public ICollection<Guid> AdminsIDs { get; init; }
    }
}