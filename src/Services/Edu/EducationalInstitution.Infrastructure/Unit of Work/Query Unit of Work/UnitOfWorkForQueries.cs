using EducationalInstitution.Infrastructure.Repositories.Query_Repository;
using System;

namespace EducationalInstitution.Infrastructure.Unit_of_Work.Query_Unit_of_Work
{
    public class UnitOfWorkForQueries : IUnitOfWorkForQueries
    {
        private readonly string connectionString;

        public IEducationalInstitutionQueryRepository EducationalInstitutionQueryRepository { get; private set; }

        public UnitOfWorkForQueries(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            this.connectionString = connectionString;
        }

        public IEducationalInstitutionQueryRepository UsingEducationalInstitutionQueryRepository()
        {
            if (EducationalInstitutionQueryRepository is null)
                EducationalInstitutionQueryRepository = new EducationalInstitutionQueryRepository(connectionString);

            return EducationalInstitutionQueryRepository;
        }
    }
}