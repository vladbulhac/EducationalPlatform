using EducationalInstitutionAPI.Repositories.EducationalInstitution_Repository.Command_Repository;
using EducationalInstitutionAPI.Repositories.EducationalInstitutionAdmin_Repository.Command_Repository;
using EducationalInstitutionAPI.Repositories.EducationalInstitutionBuilding_Repository.Command_Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Unit_of_Work.Command_Unit_of_Work
{
    public interface IUnitOfWorkForCommands : IDisposable
    {
        /// <returns>Returns an instance of a Repository class that implements <see cref="IEducationalInstitutionCommandRepository"/></returns>
        public IEducationalInstitutionCommandRepository UsingEducationalInstitutionCommandRepository();

        /// <returns>Returns an instance of a Repository class that implements <see cref="IEducationalInstitutionBuildingCommandRepository"/></returns>
        public IEducationalInstitutionBuildingCommandRepository UsingEducationalInstitutionBuildingRepository();

        /// <returns>Returns an instance of a Repository class that implements <see cref="IEducationalInstitutionAdminCommandRepository"/></returns>
        public IEducationalInstitutionAdminCommandRepository UsingEducationalInstitutionAdminCommandRepository();

        public Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}