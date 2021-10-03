using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.Infrastructure.Repositories.Command_Repository
{
    /// <summary>
    /// Contains create, delete, and update methods used to change the data from a data source
    /// </summary>
    public interface IEducationalInstitutionCommandRepository : IRepository<Domain::EducationalInstitution>
    {
        public Task CreateAsync(Domain::EducationalInstitution data, CancellationToken cancellationToken = default);

        public Task<Domain::EducationalInstitution> GetEducationalInstitutionIncludingAdminsAsync(Guid educationalInstitutionID, CancellationToken cancellationToken = default);

        public Task<Domain::EducationalInstitution> GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(Guid educationalInstitutionID, CancellationToken cancellationToken = default);
    }
}