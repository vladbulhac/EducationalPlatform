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

        public async Task CreateAsync(Domain::EducationalInstitution entity, CancellationToken cancellationToken = default) => await context.EducationalInstitutions.AddAsync(entity, cancellationToken);

        public async Task<Domain::EducationalInstitution> GetEducationalInstitutionIncludingAdminsAsync(Guid educationalInstitutionID, CancellationToken cancellationToken = default)
            => await context.EducationalInstitutions.Include(ei => ei.Admins.Where(a => !a.Access.IsDisabled))
                                                    .Where(ei => !ei.Access.IsDisabled && ei.Id == educationalInstitutionID)
                                                    .SingleOrDefaultAsync(cancellationToken);

        public async Task<Domain::EducationalInstitution> GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(Guid educationalInstitutionID, CancellationToken cancellationToken = default)
            => await context.EducationalInstitutions.Include(ei => ei.Admins.Where(a => !a.Access.IsDisabled))
                                                    .Include(ei => ei.Buildings.Where(b => !b.Access.IsDisabled))
                                                    .Where(ei => !ei.Access.IsDisabled && ei.Id == educationalInstitutionID)
                                                    .SingleOrDefaultAsync(cancellationToken);
    }
}