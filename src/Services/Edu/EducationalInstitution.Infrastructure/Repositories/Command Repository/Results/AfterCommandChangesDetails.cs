using System;
using System.Collections.Generic;

namespace EducationalInstitution.Infrastructure.Repositories.Command_Repository.Results
{
    /// <summary>
    /// Contains the data that is used to notify other services of the changes
    /// </summary>
    public record AfterCommandChangesDetails
    {
        public ICollection<Guid> AdminsToNotify { get; init; }

        public AfterCommandChangesDetails(ICollection<Guid> adminsToNotify) => AdminsToNotify = adminsToNotify;
    }
}