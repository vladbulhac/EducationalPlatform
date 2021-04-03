﻿using EducationaInstitutionAPI.Data;
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
    /// Contains concrete implementations of the methods that execute Queries and Commands over the <see cref="EduInstitution"/> entities
    /// </summary>
    public class EducationalInstitutionRepository : IEducationalInstitutionRepository
    {
        private readonly DataContext context;

        public EducationalInstitutionRepository(DataContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateAsync(EduInstitution data, CancellationToken cancellationToken = default)
        {
            await context.EducationalInstitutions.AddAsync(data, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> DeleteAsync(Guid ID, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await context.EducationalInstitutions.SingleOrDefaultAsync(eduI => eduI.EduInstitutionID == ID, cancellationToken);

            if (educationalInstitution == null) return false;

            context.EducationalInstitutions.Remove(educationalInstitution);
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<ICollection<GetEducationalInstitutionQueryResult>> GetAllLikeNameAsync(string Name, int offsetValue = 0, int resultsCount = 1, CancellationToken cancellationToken = default)
        {
            var educationalInstitutions = await context.EducationalInstitutions
                                                        .Where(ei => ei.Name.Contains(Name))
                                                        .Include(ei => ei.Buildings)
                                                        .Select(ei => new GetEducationalInstitutionQueryResult()
                                                        {
                                                            EduInstitutionID = ei.EduInstitutionID,
                                                            Description = ei.Description,
                                                            LocationID = ei.LocationID,
                                                            BuildingsIDs = ei.Buildings,
                                                            Name = ei.Name
                                                        })
                                                        .Skip(offsetValue)
                                                        .Take(resultsCount)
                                                        .ToListAsync(cancellationToken);

            if (educationalInstitutions == null) return null;

            return educationalInstitutions;
        }

        public async Task<EduInstitution> GetEntityByIDAsync(Guid ID, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                                .SingleOrDefaultAsync(ei => ei.EduInstitutionID == ID, cancellationToken);

            return educationalInstitution;
        }

        public async Task<GetEducationalInstitutionByIDQueryResult> GetByIDAsync(Guid ID, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                        .Where(eduI => eduI.EduInstitutionID == ID)
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

            if (educationalInstitution == null) return null;

            return educationalInstitution;
        }

        public async Task<GetEducationalInstitutionByLocationQueryResult> GetByLocationAsync(string locationID, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                        .Where(ei => ei.LocationID == locationID)
                                                        .Include(ei => ei.Buildings)
                                                        .Select(ei => new GetEducationalInstitutionByLocationQueryResult()
                                                        {
                                                            Name = ei.Name,
                                                            BuildingsIDs = ei.Buildings,
                                                            Description = ei.Description,
                                                            EduInstitutionID = ei.EduInstitutionID
                                                        })
                                                      .SingleOrDefaultAsync(cancellationToken);

            if (educationalInstitution == null) return null;

            return educationalInstitution;
        }

        public async Task<ICollection<GetEducationalInstitutionQueryResult>> GetFromCollectionOfIDsAsync(ICollection<Guid> IDs, CancellationToken cancellationToken = default)
        {
            var educationalInstitutions = await context.EducationalInstitutions.Where(eduI => IDs.Contains(eduI.EduInstitutionID))
                                                        .Include(ei => ei.Buildings)
                                                        .Select(ei => new GetEducationalInstitutionQueryResult()
                                                        {
                                                            EduInstitutionID = ei.EduInstitutionID,
                                                            LocationID = ei.LocationID,
                                                            BuildingsIDs = ei.Buildings,
                                                            Name = ei.Name,
                                                            Description = ei.Description
                                                        })
                                                        .ToListAsync(cancellationToken);

            if (educationalInstitutions == null) return null;

            return educationalInstitutions;
        }

        public async Task<bool> UpdateAsync(EduInstitution data, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                      .SingleOrDefaultAsync(eduI => eduI.EduInstitutionID == data.EduInstitutionID, cancellationToken);
            if (educationalInstitution == null) return false;

            educationalInstitution.Update(data.Name, data.Description, data.LocationID);

            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> UpdateAsync(Guid eduInstitutionID, string locationID, ICollection<string> buildingsIDs, CancellationToken cancellationToken)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                     .SingleOrDefaultAsync(ei => ei.EduInstitutionID == eduInstitutionID, cancellationToken);

            if (educationalInstitution == null) return false;

            educationalInstitution.UpdateEntireLocation(locationID, buildingsIDs);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> UpdateAsync(Guid eduInstitutionID, string locationID, CancellationToken cancellationToken)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                     .SingleOrDefaultAsync(ei => ei.EduInstitutionID == eduInstitutionID, cancellationToken);

            if (educationalInstitution == null) return false;

            educationalInstitution.UpdateLocation(locationID);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> UpdateAsync(ICollection<string> buildingsIDs, Guid eduInstitutionID, CancellationToken cancellationToken)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                    .SingleOrDefaultAsync(ei => ei.EduInstitutionID == eduInstitutionID, cancellationToken);

            if (educationalInstitution == null) return false;

            educationalInstitution.CreateAndAddABuilding(buildingsIDs);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}