using EducationalInstitution.Domain.Building_Blocks;
using System;

namespace EducationalInstitution.Domain.Models
{
    public record Access : ValueObject
    {
        /// <value>True if the entity is scheduled for deletion, False otherwise</value>
        public bool IsDisabled { get; init; }

        /// <summary>
        /// Describes the date at which the entity is completely removed from the data source
        /// </summary>
        /// <value>A <see cref="DateTime"/> if IsDisabled is True or NULL value if IsDisabled is False</value>
        public DateTime? DateForPermanentDeletion { get; init; }
    }
}