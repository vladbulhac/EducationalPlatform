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

        public async Task<CommandRepositoryResult> UpdateEntireLocationAsync(Guid educationalInstitutionID, string locationID, ICollection<string> addBuildingsIDs, ICollection<string> removeBuildingsIDs, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await GetEducationalInstitution(educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return default;

            educationalInstitution.SetEntireLocation(locationID, addBuildingsIDs, removeBuildingsIDs);
            return new(educationalInstitution.Admins.Select(a => a.AdminID).ToList());
        }

        public async Task<CommandRepositoryResult> UpdateLocationAsync(Guid educationalInstitutionID, string locationID, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await GetEducationalInstitution(educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return default;

            educationalInstitution.SetLocation(locationID);
            return new(educationalInstitution.Admins.Select(a => a.AdminID).ToList());
        }

        public async Task<CommandRepositoryResult> UpdateBuildingsAsync(Guid educationalInstitutionID, ICollection<string> addBuildingsIDs, ICollection<string> removeBuildingsIDs, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await GetEducationalInstitution(educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return default;

            educationalInstitution.CreateAndAddBuildings(addBuildingsIDs);
            educationalInstitution.RemoveBuildings(removeBuildingsIDs);
            return new(educationalInstitution.Admins.Select(a => a.AdminID).ToList());
        }

        public async Task<CommandRepositoryResult> UpdateNameAsync(Guid educationalInstitutionID, string name, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await GetEducationalInstitution(educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return default;

            educationalInstitution.SetName(name);
            return new(educationalInstitution.Admins.Select(a => a.AdminID).ToList());
        }

        public async Task<CommandRepositoryResult> UpdateDescriptionAsync(Guid educationalInstitutionID, string description, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await GetEducationalInstitution(educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return default;

            educationalInstitution.SetDescription(description);
            return new(educationalInstitution.Admins.Select(a => a.AdminID).ToList());
        }

        public async Task<CommandRepositoryResult> UpdateNameAndDescriptionAsync(Guid educationalInstitutionID, string name, string description, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await GetEducationalInstitution(educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return default;

            educationalInstitution.SetNameAndDescription(name, description);
            return new(educationalInstitution.Admins.Select(a => a.AdminID).ToList());
        }

        public async Task<CommandRepositoryResult> UpdateParentInstitutionAsync(Guid educationalInstitutionID, Guid parentInstitutionID, CancellationToken cancellationToken = default)
        {
            var parentInstitution = await GetEducationalInstitution(parentInstitutionID, cancellationToken);
            if (parentInstitution is null) return default;

            var educationalInstitution = await GetEducationalInstitution(educationalInstitutionID, cancellationToken);
            if (educationalInstitution is null) return default;

            educationalInstitution.SetParentInstitution(parentInstitution);
            return new(educationalInstitution.Admins.Select(a => a.AdminID).ToList());
        }

        private async Task<EducationalInstitution> GetEducationalInstitution(Guid educationalInstitutionID, CancellationToken cancellationToken = default)
            => await context.EducationalInstitutions.Include(ei => ei.Admins)
                                                    .SingleOrDefaultAsync(ei => ei.EducationalInstitutionID == educationalInstitutionID, cancellationToken);
    }
}