using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Contexts;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Repositories.EducationalInstitutionRepository
{
    /// <summary>
    /// Contains concrete implementations of the methods that execute Queries and Commands over the <see cref="EducationalInstitution"/> entities
    /// </summary>
    public class EducationalInstitutionRepository : IEducationalInstitutionRepository
    {
        private readonly DataContext context;

        public EducationalInstitutionRepository(DataContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateAsync(EducationalInstitution data, CancellationToken cancellationToken = default) => await context.EducationalInstitutions.AddAsync(data, cancellationToken);

        public async Task<bool> DeleteAsync(Guid ID, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await context.EducationalInstitutions.SingleOrDefaultAsync(eduI => eduI.EducationalInstitutionID == ID, cancellationToken);

            if (educationalInstitution is null) return false;

            context.EducationalInstitutions.Remove(educationalInstitution);
            return true;
        }

        public async Task<ICollection<GetEducationalInstitutionQueryResult>> GetAllLikeNameAsync(string Name, int offsetValue = 0, int resultsCount = 1, CancellationToken cancellationToken = default)
        {
            return await context.EducationalInstitutions
                                 .Where(ei => ei.Name.Contains(Name))
                                 .Include(ei => ei.Buildings)
                                 .Select(ei => new GetEducationalInstitutionQueryResult()
                                 {
                                     EducationalInstitutionID = ei.EducationalInstitutionID,
                                     Description = ei.Description,
                                     LocationID = ei.LocationID,
                                     BuildingsIDs = ei.Buildings,
                                     Name = ei.Name
                                 })
                                 .Skip(offsetValue)
                                 .Take(resultsCount)
                                 .ToListAsync(cancellationToken);
        }

        public async Task<EducationalInstitution> GetEntityByIDAsync(Guid ID, CancellationToken cancellationToken = default)
        {
            return await context.EducationalInstitutions
                                 .SingleOrDefaultAsync(ei => ei.EducationalInstitutionID == ID, cancellationToken);
        }

        public async Task<GetEducationalInstitutionByIDQueryResult> GetByIDAsync(Guid ID, CancellationToken cancellationToken = default)
        {
            return await context.EducationalInstitutions
                                 .Where(eduI => eduI.EducationalInstitutionID == ID)
                                 .Include(ei => ei.Buildings)
                                 .Include(ei => ei.ChildInstitutions)
                                 .Include(ei => ei.ParentInstitution)
                                 .Select(ei => new GetEducationalInstitutionByIDQueryResult()
                                 {
                                     BuildingsIDs = ei.Buildings,
                                     Description = ei.Description,
                                     Name = ei.Name,
                                     LocationID = ei.LocationID,
                                     ChildInstitutions = ei.ChildInstitutions,
                                     ParentInstitution = ei.ParentInstitution,
                                     JoinDate = ei.JoinDate
                                 })
                                 .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<GetAllEducationalInstitutionsByLocationQueryResult> GetAllByLocationAsync(string locationID, CancellationToken cancellationToken = default)
        {
            return new()
            {
                EducationalInstitutions = await context.EducationalInstitutions
                                                        .Where(ei => ei.LocationID == locationID)
                                                        .Include(ei => ei.Buildings)
                                                        .Select(ei => new GetEducationalInstitutionByLocationQueryResult()
                                                        {
                                                            Name = ei.Name,
                                                            BuildingsIDs = ei.Buildings,
                                                            Description = ei.Description,
                                                            EducationalInstitutionID = ei.EducationalInstitutionID
                                                        })
                                                      .ToListAsync(cancellationToken)
            };
        }

        public async Task<ICollection<GetEducationalInstitutionQueryResult>> GetFromCollectionOfIDsAsync(ICollection<Guid> IDs, CancellationToken cancellationToken = default)
        {
            return await context.EducationalInstitutions
                                 .Where(eduI => IDs.Contains(eduI.EducationalInstitutionID))
                                 .Include(ei => ei.Buildings)
                                 .Select(ei => new GetEducationalInstitutionQueryResult()
                                 {
                                     EducationalInstitutionID = ei.EducationalInstitutionID,
                                     LocationID = ei.LocationID,
                                     BuildingsIDs = ei.Buildings,
                                     Name = ei.Name,
                                     Description = ei.Description
                                 })
                                 .ToListAsync(cancellationToken);
        }

        public async Task<bool> UpdateAsync(EducationalInstitution data, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                      .SingleOrDefaultAsync(eduI => eduI.EducationalInstitutionID == data.EducationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.Update(data.Name, data.Description, data.LocationID);
            return true;
        }

        public async Task<bool> UpdateEntireLocationAsync(Guid educationalInstitutionID, string locationID, ICollection<string> addBuildingsIDs, ICollection<string> removeBuildingsIDs, CancellationToken cancellationToken)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                     .SingleOrDefaultAsync(ei => ei.EducationalInstitutionID == educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.UpdateEntireLocation(locationID, addBuildingsIDs, removeBuildingsIDs);
            return true;
        }

        public async Task<bool> UpdateLocationAsync(Guid educationalInstitutionID, string locationID, CancellationToken cancellationToken)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                     .SingleOrDefaultAsync(ei => ei.EducationalInstitutionID == educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.UpdateLocation(locationID);
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

            educationalInstitution.UpdateName(name);
            return true;
        }

        public async Task<bool> UpdateDescriptionAsync(Guid educationalInstitutionID, string description, CancellationToken cancellationToken)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                    .SingleOrDefaultAsync(ei => ei.EducationalInstitutionID == educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.UpdateDescription(description);
            return true;
        }

        public async Task<bool> UpdateNameAndDescriptionAsync(Guid educationalInstitutionID, string name, string description, CancellationToken cancellationToken)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                        .SingleOrDefaultAsync(ei => ei.EducationalInstitutionID == educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.Update(name, description);
            return true;
        }

        public async Task<bool> UpdateParentInstitutionAsync(Guid educationalInstitutionID, EducationalInstitution parentInstitution, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                        .SingleOrDefaultAsync(ei => ei.EducationalInstitutionID == educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.UpdateParentInstitution(parentInstitution);
            return true;
        }
    }
}