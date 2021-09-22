using System;
using System.Collections.Generic;

namespace EducationalInstitution.Infrastructure.Repositories.Command_Repository.Results
{
    /// <summary>
    /// Contains the data that is used to notify other services of the changes
    /// </summary>
    public record AfterCommandChangesDetails
    {
        public ICollection<string> AdminsToNotify { get; init; }

        public AfterCommandChangesDetails(ICollection<string> adminsToNotify) => AdminsToNotify = adminsToNotify;
    }
}