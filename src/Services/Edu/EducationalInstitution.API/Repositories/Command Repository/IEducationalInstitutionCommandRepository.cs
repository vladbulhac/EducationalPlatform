using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Repository_Results;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Repositories.Command_Repository
{
    /// <summary>
    /// Contains create, delete and update methods that are used to execute commands on a database
    /// </summary>
    public interface IEducationalInstitutionCommandRepository
    {
        public Task CreateAsync(EducationalInstitution data, CancellationToken cancellationToken = default);

        public Task<bool> DeleteAsync(Guid ID, CancellationToken cancellationToken = default);

        public Task<DeleteCommandRepositoryResult> ScheduleForDeletionAsync(Guid educationalInstitutionID, CancellationToken cancellationToken = default);

        public Task<CommandRepositoryResult> UpdateEntireLocationAsync(Guid educationalInstitutionID, string locationID, ICollection<string> addBuildingsIDs, ICollection<string> removeBuildingsIDs, CancellationToken cancellationToken = default);

        public Task<CommandRepositoryResult> UpdateLocationAsync(Guid educationalInstitutionID, string locationID, CancellationToken cancellationToken = default);

        public Task<CommandRepositoryResult> UpdateBuildingsAsync(Guid educationalInstitutionID, ICollection<string> addBuildingsIDs, ICollection<string> removeBuildingsIDs, CancellationToken cancellationToken = default);

        public Task<UpdateAdminsCommandRepositoryResult> UpdateAdminsAsync(Guid educationalInstitutionID, ICollection<Guid> addAdminsIDs, ICollection<Guid> removeAdminsIDs, CancellationToken cancellationToken = default);

        public Task<CommandRepositoryResult> UpdateNameAsync(Guid educationalInstitutionID, string name, CancellationToken cancellationToken = default);

        public Task<CommandRepositoryResult> UpdateDescriptionAsync(Guid educationalInstitutionID, string description, CancellationToken cancellationToken = default);

        public Task<CommandRepositoryResult> UpdateNameAndDescriptionAsync(Guid educationalInstitutionID, string name, string description, CancellationToken cancellationToken = default);

        public Task<CommandRepositoryResult> UpdateParentInstitutionAsync(Guid educationalInstitutionID, Guid parentInstitutionID, CancellationToken cancellationToken = default);
    }
}