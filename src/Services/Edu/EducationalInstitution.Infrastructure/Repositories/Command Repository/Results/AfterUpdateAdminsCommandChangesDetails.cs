using System;
using System.Collections.Generic;

namespace EducationalInstitution.Infrastructure.Repositories.Command_Repository.Results
{
    /// <inheritdoc cref="AfterCommandChangesDetails"/>
    public record AfterUpdateAdminsCommandChangesDetails : AfterCommandChangesDetails
    {
        public ICollection<string> NewAdminsToNotify { get; init; }
        public ICollection<string> RemovedAdminsToNotify { get; init; }

        public AfterUpdateAdminsCommandChangesDetails(ICollection<string> existingAdminsToNotify, ICollection<string> newAdminsToNotify, ICollection<string> removedAdminsToNotify) : base(existingAdminsToNotify)
        {
            NewAdminsToNotify = newAdminsToNotify;
            RemovedAdminsToNotify = removedAdminsToNotify;
        }
    }
}