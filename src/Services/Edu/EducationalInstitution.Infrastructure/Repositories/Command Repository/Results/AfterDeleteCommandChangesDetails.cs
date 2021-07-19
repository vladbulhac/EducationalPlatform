using System;
using System.Collections.Generic;

namespace EducationalInstitution.Infrastructure.Repositories.Command_Repository.Results
{
    /// <inheritdoc cref="AfterCommandChangesDetails"/>
    public record AfterDeleteCommandChangesDetails : AfterCommandChangesDetails
    {
        public DateTime ScheduledDateForDeletion { get; init; }

        public AfterDeleteCommandChangesDetails(DateTime deletionDate, ICollection<Guid> adminsToNotify) : base(adminsToNotify)
                            => ScheduledDateForDeletion = deletionDate;
    }
}