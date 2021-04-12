using EducationaInstitutionAPI.Data;
using EducationaInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EducationaInstitutionAPI.Repositories.EducationalInstitutionBuildingRepository
{
    /// <summary>
    /// Contains concrete implementations of the methods that execute Queries and Commands over the <see cref="EduInstitutionBuilding"/> entities
    /// </summary>
    public class EducationalInstitutionBuildingRepository : IEducationalInstitutionBuildingRepository
    {
        private readonly DataContext context;

        public EducationalInstitutionBuildingRepository(DataContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<GetAllEducationalInstitutionsWithSameBuildingQueryResult> GetAllEducationalInstitutionsWithSameBuildingAsync(string buildingID, CancellationToken cancellationToken = default)
        {
            return new()
            {
                EducationalInstitutions = await context.EducationalInstitutionsBuildings
                                                                        .Where(eib => eib.BuildingID == buildingID)
                                                                        .Include(ei => ei.EducationalInstitution)
                                                                        .Select(s => new EducationalInstitutionEssentialData()
                                                                        {
                                                                            EducationalInstitutionID = s.EducationalInstitution.EduInstitutionID,
                                                                            Name = s.EducationalInstitution.Name,
                                                                            Description = s.EducationalInstitution.Description
                                                                        })
                                                                        .ToListAsync(cancellationToken)
            };
        }
    }
}