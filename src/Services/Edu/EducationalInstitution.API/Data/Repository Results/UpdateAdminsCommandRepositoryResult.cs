using System;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.Data.Repository_Results
{
    /// <inheritdoc cref="CommandRepositoryResult"/>
    public class UpdateAdminsCommandRepositoryResult
    {
        public ICollection<Guid> ExistingAdminsToNotify { get; init; }
        public ICollection<Guid> NewAdminsToNotify { get; init; }
        public ICollection<Guid> RemovedAdminsToNotify { get; init; }

        public UpdateAdminsCommandRepositoryResult(ICollection<Guid> existingAdminsToNotify, ICollection<Guid> newAdminsToNotify, ICollection<Guid> removedAdminsToNotify)
        {
            ExistingAdminsToNotify = existingAdminsToNotify;
            NewAdminsToNotify = newAdminsToNotify;
            RemovedAdminsToNotify = removedAdminsToNotify;
        }
    }
}