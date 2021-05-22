using System;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.Data.Repositories_results
{
    public record CommandRepositoryResult
    {
        public ICollection<Guid> AdminsToNotify { get; init; }

        public CommandRepositoryResult(ICollection<Guid> adminsToNotify) => AdminsToNotify = adminsToNotify;
    }
}