using EducationalInstitutionAPI.Repositories.EducationalInstitution_Repository.Query_Repository;

namespace EducationalInstitutionAPI.Unit_of_Work.Query_Unit_of_Work
{
    public interface IUnitOfWorkForQueries
    {
        /// <returns>Returns an instance of a Repository class that implements <see cref="IEducationalInstitutionQueryRepository"/></returns>
        public IEducationalInstitutionQueryRepository UsingEducationalInstitutionQueryRepository();
    }
}