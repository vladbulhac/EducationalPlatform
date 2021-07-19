using EducationalInstitution.Infrastructure.Repositories.Command_Repository.Results;
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

        public Task<AfterDeleteCommandChangesDetails> ScheduleForDeletionAsync(Guid educationalInstitutionID, CancellationToken cancellationToken = default);

        public Task<AfterCommandChangesDetails> UpdateEntireLocationAsync(Guid educationalInstitutionID, string locationID, ICollection<string> addBuildingsIDs, ICollection<string> removeBuildingsIDs, CancellationToken cancellationToken = default);

        public Task<AfterCommandChangesDetails> UpdateLocationAsync(Guid educationalInstitutionID, string locationID, CancellationToken cancellationToken = default);

        public Task<AfterCommandChangesDetails> UpdateBuildingsAsync(Guid educationalInstitutionID, ICollection<string> addBuildingsIDs, ICollection<string> removeBuildingsIDs, CancellationToken cancellationToken = default);

        public Task<AfterUpdateAdminsCommandChangesDetails> UpdateAdminsAsync(Guid educationalInstitutionID, ICollection<Guid> addAdminsIDs, ICollection<Guid> removeAdminsIDs, CancellationToken cancellationToken = default);

        public Task<AfterCommandChangesDetails> UpdateNameAsync(Guid educationalInstitutionID, string name, CancellationToken cancellationToken = default);

        public Task<AfterCommandChangesDetails> UpdateDescriptionAsync(Guid educationalInstitutionID, string description, CancellationToken cancellationToken = default);

        public Task<AfterCommandChangesDetails> UpdateNameAndDescriptionAsync(Guid educationalInstitutionID, string name, string description, CancellationToken cancellationToken = default);

        public Task<AfterCommandChangesDetails> UpdateParentInstitutionAsync(Guid educationalInstitutionID, Guid parentInstitutionID, CancellationToken cancellationToken = default);

        public Task<Domain::EducationalInstitution> GetEducationalInstitutionIncludingAdminsAsync(Guid educationalInstitutionID, CancellationToken cancellationToken = default);
    }
}