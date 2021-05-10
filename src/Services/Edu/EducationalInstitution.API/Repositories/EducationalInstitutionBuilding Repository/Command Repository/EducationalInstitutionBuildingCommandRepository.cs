using EducationalInstitutionAPI.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Repositories.EducationalInstitutionBuilding_Repository.Command_Repository
{
    public class EducationalInstitutionBuildingCommandRepository : IEducationalInstitutionBuildingCommandRepository
    {
        private readonly DataContext context;

        public EducationalInstitutionBuildingCommandRepository(DataContext context) => this.context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<bool> DeleteAsync(string buildingID, Guid educationalInstitutionID, CancellationToken cancellationToken = default)
        {
            var educationalInstitutionBuilding = await context.Buildings
                                                                .SingleOrDefaultAsync(eib => eib.BuildingID == buildingID && eib.EducationalInstitutionID == educationalInstitutionID, cancellationToken);

            if (educationalInstitutionBuilding is null) return false;

            context.Buildings.Remove(educationalInstitutionBuilding);
            return true;
        }

        public async Task<bool> DeleteAsync(ICollection<string> buildingsIDs, Guid educationalInstitutionID, CancellationToken cancellationToken = default)
        {
            var buildings = await context.Buildings.Where(eib => buildingsIDs.Contains(eib.BuildingID) && eib.EducationalInstitutionID == educationalInstitutionID)
                                                    .ToListAsync(cancellationToken);

            if (buildings is null || buildings.Count == 0) return false;

            context.Buildings.RemoveRange(buildings);
            return true;
        }
    }
}