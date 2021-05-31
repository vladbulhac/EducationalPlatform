using System;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.Data.Repository_Results
{
    public class UpdateAdminsCommandRepositoryResult
    {
        public ICollection<Guid> existingAdminsToNotify { get; init; }
        public ICollection<Guid> newAdminsToNotify { get; init; }
        public ICollection<Guid> removedAdminsToNotify { get; init; }

        public UpdateAdminsCommandRepositoryResult(ICollection<Guid> existingAdminsToNotify, ICollection<Guid> newAdminsToNotify, ICollection<Guid> removedAdminsToNotify)
        {
            this.existingAdminsToNotify = existingAdminsToNotify;
            this.newAdminsToNotify = newAdminsToNotify;
            this.removedAdminsToNotify = removedAdminsToNotify;
        }
    }
}