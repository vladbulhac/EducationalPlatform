using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Contexts;
using EducationalInstitutionAPI.Data.Repositories_results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var educationalInstitution = await GetEducationalInstitution(educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            context.EducationalInstitutions.Remove(educationalInstitution);
            return true;
        }

        public async Task<DeleteCommandRepositoryResult> ScheduleForDeletionAsync(Guid educationalInstitutionID, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await GetEducationalInstitution(educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return default;

            educationalInstitution.EntityAccess.ScheduleForDeletion();
            return new(educationalInstitution.EntityAccess.DateForPermanentDeletion.Value.ToUniversalTime(), educationalInstitution.Admins.Select(a => a.AdminID).ToList());
        }

        public async Task<bool> UpdateEntireLocationAsync(Guid educationalInstitutionID, string locationID, ICollection<string> addBuildingsIDs, ICollection<string> removeBuildingsIDs, CancellationToken cancellationToken)
        {
            var educationalInstitution = await GetEducationalInstitution(educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.SetEntireLocation(locationID, addBuildingsIDs, removeBuildingsIDs);
            return true;
        }

        public async Task<bool> UpdateLocationAsync(Guid educationalInstitutionID, string locationID, CancellationToken cancellationToken)
        {
            var educationalInstitution = await GetEducationalInstitution(educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.SetLocation(locationID);
            return true;
        }

        public async Task<bool> UpdateBuildingsAsync(Guid educationalInstitutionID, ICollection<string> addBuildingsIDs, ICollection<string> removeBuildingsIDs, CancellationToken cancellationToken)
        {
            var educationalInstitution = await GetEducationalInstitution(educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.CreateAndAddBuildings(addBuildingsIDs);
            educationalInstitution.RemoveBuildings(removeBuildingsIDs);
            return true;
        }

        public async Task<CommandRepositoryResult> UpdateNameAsync(Guid educationalInstitutionID, string name, CancellationToken cancellationToken)
        {
            var educationalInstitution = await GetEducationalInstitution(educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return default;

            educationalInstitution.SetName(name);
            return new(educationalInstitution.Admins.Select(a => a.AdminID).ToList());
        }

        public async Task<CommandRepositoryResult> UpdateDescriptionAsync(Guid educationalInstitutionID, string description, CancellationToken cancellationToken)
        {
            var educationalInstitution = await GetEducationalInstitution(educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return default;

            educationalInstitution.SetDescription(description);
            return new(educationalInstitution.Admins.Select(a => a.AdminID).ToList());
        }

        public async Task<CommandRepositoryResult> UpdateNameAndDescriptionAsync(Guid educationalInstitutionID, string name, string description, CancellationToken cancellationToken)
        {
            var educationalInstitution = await GetEducationalInstitution(educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return default;

            educationalInstitution.SetNameAndDescription(name, description);
            return new(educationalInstitution.Admins.Select(a => a.AdminID).ToList());
        }

        public async Task<bool> UpdateParentInstitutionAsync(Guid educationalInstitutionID, EducationalInstitution parentInstitution, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await GetEducationalInstitution(educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.SetParentInstitution(parentInstitution);
            return true;
        }

        private async Task<EducationalInstitution> GetEducationalInstitution(Guid educationalInstitutionID, CancellationToken cancellationToken = default)
            => await context.EducationalInstitutions.Include(ei => ei.Admins)
                                                    .SingleOrDefaultAsync(ei => ei.EducationalInstitutionID == educationalInstitutionID, cancellationToken);
    }
}