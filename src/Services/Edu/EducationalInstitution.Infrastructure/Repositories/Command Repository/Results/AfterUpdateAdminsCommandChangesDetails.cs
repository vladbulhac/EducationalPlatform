using System;
using System.Collections.Generic;

namespace EducationalInstitution.Infrastructure.Repositories.Command_Repository.Results
{
    /// <inheritdoc cref="AfterCommandChangesDetails"/>
    public record AfterUpdateAdminsCommandChangesDetails : AfterCommandChangesDetails
    {
        public ICollection<Guid> NewAdminsToNotify { get; init; }
        public ICollection<Guid> RemovedAdminsToNotify { get; init; }

        public AfterUpdateAdminsCommandChangesDetails(ICollection<Guid> existingAdminsToNotify, ICollection<Guid> newAdminsToNotify, ICollection<Guid> removedAdminsToNotify) : base(existingAdminsToNotify)
        {
            NewAdminsToNotify = newAdminsToNotify;
            RemovedAdminsToNotify = removedAdminsToNotify;
        }
    }
}