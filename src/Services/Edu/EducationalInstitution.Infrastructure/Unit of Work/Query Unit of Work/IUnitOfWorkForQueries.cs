using EducationalInstitution.Infrastructure.Repositories.Query_Repository;

namespace EducationalInstitution.Infrastructure.Unit_of_Work.Query_Unit_of_Work;

public interface IUnitOfWorkForQueries
{
    /// <returns>Returns an instance of a Repository class that implements <see cref="IEducationalInstitutionQueryRepository"/></returns>
    public IEducationalInstitutionQueryRepository UsingEducationalInstitutionQueryRepository();
}