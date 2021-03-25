using EducationaInstitutionAPI.Data;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EducationaInstitutionAPI.Repositories
{
    /// <summary>
    /// Contains concrete implementations of the methods that execute Queries and Commands over a database
    /// </summary>
    public class EducationalInstitutionRepository : IEducationalInstitutionRepository
    {
        private readonly DataContext context;

        public EducationalInstitutionRepository(DataContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Create(EduInstitution data, CancellationToken cancellationToken = default)
        {
            await context.EducationalInstitutions.AddAsync(data, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> Delete(Guid ID, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await context.EducationalInstitutions.SingleOrDefaultAsync(eduI => eduI.EduInstitutionID == ID, cancellationToken);

            if (educationalInstitution == null) return false;

            context.EducationalInstitutions.Remove(educationalInstitution);
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<ICollection<GetEducationalInstitutionQueryResult>> GetAllLikeName(string Name, int offsetValue = 0, int resultsCount = 1, CancellationToken cancellationToken = default)
        {
            var educationalInstitutions = await context.EducationalInstitutions
                                                        .Where(ei => ei.Name.Contains(Name))
                                                        .Select(ei => new GetEducationalInstitutionQueryResult()
                                                        {
                                                            EduInstitutionID = ei.EduInstitutionID,
                                                            Description = ei.Description,
                                                            LocationID = ei.LocationID,
                                                            BuildingID = ei.BuildingID,
                                                            Name = ei.Name
                                                        })
                                                        .Skip(offsetValue)
                                                        .Take(resultsCount)
                                                        .ToListAsync(cancellationToken);

            if (educationalInstitutions == null) return null;

            return educationalInstitutions;
        }

        public async Task<GetEducationalInstitutionByIDQueryResult> GetByID(Guid ID, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                        .Where(eduI => eduI.EduInstitutionID == ID)
                                                        .Select(ei => new GetEducationalInstitutionByIDQueryResult()
                                                        {
                                                            BuildingID = ei.BuildingID,
                                                            Description = ei.Description,
                                                            Name = ei.Name,
                                                            Personnel = ei.Personnel,
                                                            Professors = ei.Professors,
                                                            Students = ei.Students,
                                                            LocationID = ei.LocationID
                                                        })
                                                        .Include(ei => ei.Personnel)
                                                        .Include(ei => ei.Professors)
                                                        .Include(ei => ei.Students)
                                                        .SingleOrDefaultAsync(cancellationToken);

            if (educationalInstitution == null) return null;

            return educationalInstitution;
        }

        public async Task<GetEducationalInstitutionByLocationQueryResult> GetByLocation(string locationID, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                        .Where(ei => ei.LocationID == locationID)
                                                        .Select(ei => new GetEducationalInstitutionByLocationQueryResult()
                                                        {
                                                            Name = ei.Name,
                                                            BuildingID = ei.BuildingID,
                                                            Description = ei.Description,
                                                            EduInstitutionID = ei.EduInstitutionID
                                                        })
                                                      .SingleOrDefaultAsync(cancellationToken);

            if (educationalInstitution == null) return null;

            return educationalInstitution;
        }

        public async Task<ICollection<GetEducationalInstitutionQueryResult>> GetFromCollectionOfIDs(ICollection<Guid> IDs, CancellationToken cancellationToken = default)
        {
            var educationalInstitutions = await context.EducationalInstitutions.Where(eduI => IDs.Contains(eduI.EduInstitutionID))
                                                        .Select(ei => new GetEducationalInstitutionQueryResult()
                                                        {
                                                            EduInstitutionID = ei.EduInstitutionID,
                                                            LocationID = ei.LocationID,
                                                            BuildingID = ei.BuildingID,
                                                            Name = ei.Name,
                                                            Description = ei.Description
                                                        })
                                                        .ToListAsync(cancellationToken);

            if (educationalInstitutions == null) return null;

            return educationalInstitutions;
        }

        public async Task<bool> Update(EduInstitution data, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                      .SingleOrDefaultAsync(eduI => eduI.EduInstitutionID == data.EduInstitutionID, cancellationToken);
            if (educationalInstitution == null) return false;

            educationalInstitution.Update(data.Name, data.Description, data.BuildingID, data.LocationID);

            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> Update(Guid eduInstitutionID, string locationID, string buildingID, CancellationToken cancellationToken)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                     .SingleOrDefaultAsync(ei => ei.EduInstitutionID == eduInstitutionID, cancellationToken);

            if (educationalInstitution == null) return false;

            educationalInstitution.UpdateEntireLocation(locationID, buildingID);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> Update(Guid eduInstitutionID, string locationID, CancellationToken cancellationToken)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                     .SingleOrDefaultAsync(ei => ei.EduInstitutionID == eduInstitutionID, cancellationToken);

            if (educationalInstitution == null) return false;

            educationalInstitution.UpdateLocation(locationID);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> Update(string buildingID, Guid eduInstitutionID, CancellationToken cancellationToken)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                    .SingleOrDefaultAsync(ei => ei.EduInstitutionID == eduInstitutionID, cancellationToken);

            if (educationalInstitution == null) return false;

            educationalInstitution.UpdateBuilding(buildingID);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}