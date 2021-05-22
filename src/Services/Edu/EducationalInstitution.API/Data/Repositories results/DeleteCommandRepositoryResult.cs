using System;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.Data.Repositories_results
{
    public record DeleteCommandRepositoryResult : CommandRepositoryResult
    {
        public DateTime ScheduledDateForDeletion { get; init; }

        public DeleteCommandRepositoryResult(DateTime deletionDate, ICollection<Guid> adminsToNotify) : base(adminsToNotify)
                            => ScheduledDateForDeletion = deletionDate;
    }
}