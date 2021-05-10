using EducationalInstitutionAPI.Repositories.EducationalInstitution_Repository.Query_Repository;
using EducationalInstitutionAPI.Repositories.EducationalInstitutionAdmin_Repository.Query_Repostiory;
using EducationalInstitutionAPI.Repositories.EducationalInstitutionBuilding_Repository.Query_Repository;

namespace EducationalInstitutionAPI.Unit_of_Work.Query_Unit_of_Work
{
    public interface IUnitOfWorkForQueries
    {
        /// <returns>Returns an instance of a Repository class that implements <see cref="IEducationalInstitutionQueryRepository"/></returns>
        public IEducationalInstitutionQueryRepository UsingEducationalInstitutionQueryRepository();

        /// <returns>Returns an instance of a Repository class that implements <see cref="IEducationalInstitutionBuildingQueryRepository"/></returns>
        public IEducationalInstitutionBuildingQueryRepository UsingEducationalInstitutionBuildingRepository();

        /// <returns>Returns an instance of a Repository class that implements <see cref="IEducationalInstitutionAdminQueryRepository"/></returns>
        public IEducationalInstitutionAdminQueryRepository UsingEducationalInstitutionAdminQueryRepository();
    }
}