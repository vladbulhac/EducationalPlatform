using System;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.Data.Repository_Results
{
    /// <inheritdoc cref="CommandRepositoryResult"/>
    public record DeleteCommandRepositoryResult : CommandRepositoryResult
    {
        public DateTime ScheduledDateForDeletion { get; init; }

        public DeleteCommandRepositoryResult(DateTime deletionDate, ICollection<Guid> adminsToNotify) : base(adminsToNotify)
                            => ScheduledDateForDeletion = deletionDate;
    }
}