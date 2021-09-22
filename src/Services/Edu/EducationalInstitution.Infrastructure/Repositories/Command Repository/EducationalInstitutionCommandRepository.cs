using EducationalInstitution.Infrastructure.Repositories.Command_Repository.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.Infrastructure.Repositories.Command_Repository
{
    /// <inheritdoc cref="IEducationalInstitutionCommandRepository"/>
    public class EducationalInstitutionCommandRepository : IEducationalInstitutionCommandRepository
    {
        private readonly DataContext context;

        /// <exception cref="ArgumentNullException"/>
        public EducationalInstitutionCommandRepository(DataContext context) => this.context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task CreateAsync(Domain::EducationalInstitution data, CancellationToken cancellationToken = default) => await context.EducationalInstitutions.AddAsync(data, cancellationToken);

        public async Task<AfterDeleteCommandChangesDetails> ScheduleForDeletionAsync(Guid educationalInstitutionID, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return default;

            educationalInstitution.ScheduleForDeletion();
            return new(educationalInstitution.Access.DateForPermanentDeletion.Value.ToUniversalTime(), educationalInstitution.Admins.Select(a => a.AdminId).ToList());
        }

        public async Task<AfterCommandChangesDetails> UpdateEntireLocationAsync(Guid educationalInstitutionID, string locationID, ICollection<string> addBuildingsIDs, ICollection<string> removeBuildingsIDs, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return default;

            educationalInstitution.SetEntireLocation(locationID, addBuildingsIDs, removeBuildingsIDs);
            return new(educationalInstitution.Admins.Select(a => a.AdminId).ToList());
        }

        public async Task<AfterCommandChangesDetails> UpdateLocationAsync(Guid educationalInstitutionID, string locationID, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await GetEducationalInstitutionIncludingAdminsAsync(educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return default;

            educationalInstitution.SetLocation(locationID);
            return new(educationalInstitution.Admins.Select(a => a.AdminId).ToList());
        }

        public async Task<AfterCommandChangesDetails> UpdateBuildingsAsync(Guid educationalInstitutionID, ICollection<string> addBuildingsIDs, ICollection<string> removeBuildingsIDs, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return default;

            educationalInstitution.CreateAndAddBuildings(addBuildingsIDs);
            educationalInstitution.RemoveBuildings(removeBuildingsIDs);
            return new(educationalInstitution.Admins.Select(a => a.AdminId).ToList());
        }

        public async Task<AfterCommandChangesDetails> UpdateNameAsync(Guid educationalInstitutionID, string name, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await GetEducationalInstitutionIncludingAdminsAsync(educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return default;

            educationalInstitution.SetName(name);
            return new(educationalInstitution.Admins.Select(a => a.AdminId).ToList());
        }

        public async Task<AfterCommandChangesDetails> UpdateDescriptionAsync(Guid educationalInstitutionID, string description, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await GetEducationalInstitutionIncludingAdminsAsync(educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return default;

            educationalInstitution.SetDescription(description);
            return new(educationalInstitution.Admins.Select(a => a.AdminId).ToList());
        }

        public async Task<AfterCommandChangesDetails> UpdateNameAndDescriptionAsync(Guid educationalInstitutionID, string name, string description, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await GetEducationalInstitutionIncludingAdminsAsync(educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return default;

            educationalInstitution.SetNameAndDescription(name, description);
            return new(educationalInstitution.Admins.Select(a => a.AdminId).ToList());
        }

        public async Task<AfterCommandChangesDetails> UpdateParentInstitutionAsync(Guid educationalInstitutionID, Guid parentInstitutionID, CancellationToken cancellationToken = default)
        {
            Domain::EducationalInstitution parentInstitution = null;
            if (parentInstitutionID != default)
            {
                parentInstitution = await GetEducationalInstitutionIncludingAdminsAsync(parentInstitutionID, cancellationToken);
                if (parentInstitution is null) return default;
            }

            var educationalInstitution = await context.EducationalInstitutions
                                                            .Include(ei => ei.Admins)
                                                            .Include(ei => ei.ParentInstitution)
                                                            .Where(ei => !ei.Access.IsDisabled && ei.Id == educationalInstitutionID)
                                                            .SingleOrDefaultAsync(cancellationToken);
            if (educationalInstitution is null) return default;

            educationalInstitution.SetParentInstitution(parentInstitution);
            return new(educationalInstitution.Admins.Select(a => a.AdminId).ToList());
        }

        public async Task<AfterUpdateAdminsCommandChangesDetails> UpdateAdminsAsync(Guid educationalInstitutionID, ICollection<string> addAdminsIDs, ICollection<string> removeAdminsIDs, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await GetEducationalInstitutionIncludingAdminsAsync(educationalInstitutionID, cancellationToken);
            if (educationalInstitution is null) return default;

            educationalInstitution.CreateAndAddAdmins(addAdminsIDs);
            educationalInstitution.RemoveAdmins(removeAdminsIDs);

            return new(educationalInstitution.Admins.Select(a => a.AdminId).Except(addAdminsIDs).ToList(), addAdminsIDs, removeAdminsIDs);
        }

        public async Task<Domain::EducationalInstitution> GetEducationalInstitutionIncludingAdminsAsync(Guid educationalInstitutionID, CancellationToken cancellationToken = default)
            => await context.EducationalInstitutions.Include(ei => ei.Admins.Where(a => !a.Access.IsDisabled))
                                                    .Where(ei => !ei.Access.IsDisabled && ei.Id == educationalInstitutionID)
                                                    .SingleOrDefaultAsync(cancellationToken);

        private async Task<Domain::EducationalInstitution> GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(Guid educationalInstitutionID, CancellationToken cancellationToken = default)
            => await context.EducationalInstitutions.Include(ei => ei.Admins.Where(a => !a.Access.IsDisabled))
                                                    .Include(ei => ei.Buildings.Where(b => !b.Access.IsDisabled))
                                                    .Where(ei => !ei.Access.IsDisabled && ei.Id == educationalInstitutionID)
                                                    .SingleOrDefaultAsync(cancellationToken);
    }
}