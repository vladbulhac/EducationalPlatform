using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Repositories.EducationalInstitution_Repository.Command_Repository
{
    /// <summary>
    /// Contains concrete implementations of the methods that execute commands over the <see cref="EducationalInstitution"/> entities
    /// </summary>
    public class EducationalInstitutionCommandRepository : IEducationalInstitutionCommandRepository
    {
        private readonly DataContext context;

        public EducationalInstitutionCommandRepository(DataContext context) => this.context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task CreateAsync(EducationalInstitution data, CancellationToken cancellationToken = default) => await context.EducationalInstitutions.AddAsync(data, cancellationToken);

        public async Task<bool> DeleteAsync(Guid educationalInstitutionID, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await context.EducationalInstitutions.SingleOrDefaultAsync(eduI => eduI.EducationalInstitutionID == educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            context.EducationalInstitutions.Remove(educationalInstitution);
            return true;
        }

        public async Task<bool> UpdateEntireLocationAsync(Guid educationalInstitutionID, string locationID, ICollection<string> addBuildingsIDs, ICollection<string> removeBuildingsIDs, CancellationToken cancellationToken)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                     .SingleOrDefaultAsync(ei => ei.EducationalInstitutionID == educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.SetEntireLocation(locationID, addBuildingsIDs, removeBuildingsIDs);
            return true;
        }

        public async Task<bool> UpdateLocationAsync(Guid educationalInstitutionID, string locationID, CancellationToken cancellationToken)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                     .SingleOrDefaultAsync(ei => ei.EducationalInstitutionID == educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.SetLocation(locationID);
            return true;
        }

        public async Task<bool> UpdateBuildingsAsync(Guid educationalInstitutionID, ICollection<string> addBuildingsIDs, ICollection<string> removeBuildingsIDs, CancellationToken cancellationToken)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                    .SingleOrDefaultAsync(ei => ei.EducationalInstitutionID == educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.CreateAndAddBuildings(addBuildingsIDs);
            educationalInstitution.RemoveBuildings(removeBuildingsIDs);
            return true;
        }

        public async Task<bool> UpdateNameAsync(Guid educationalInstitutionID, string name, CancellationToken cancellationToken)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                    .SingleOrDefaultAsync(ei => ei.EducationalInstitutionID == educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.SetName(name);
            return true;
        }

        public async Task<bool> UpdateDescriptionAsync(Guid educationalInstitutionID, string description, CancellationToken cancellationToken)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                    .SingleOrDefaultAsync(ei => ei.EducationalInstitutionID == educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.SetDescription(description);
            return true;
        }

        public async Task<bool> UpdateNameAndDescriptionAsync(Guid educationalInstitutionID, string name, string description, CancellationToken cancellationToken)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                        .SingleOrDefaultAsync(ei => ei.EducationalInstitutionID == educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.SetNameAndDescription(name, description);
            return true;
        }

        public async Task<bool> UpdateParentInstitutionAsync(Guid educationalInstitutionID, EducationalInstitution parentInstitution, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                        .SingleOrDefaultAsync(ei => ei.EducationalInstitutionID == educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.SetParentInstitution(parentInstitution);
            return true;
        }
    }
}