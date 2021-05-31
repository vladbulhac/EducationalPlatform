﻿using EducationalInstitutionAPI.Repositories.EducationalInstitution_Repository.Query_Repository;
using EducationalInstitutionAPI.Utils;
using System;

namespace EducationalInstitutionAPI.Unit_of_Work.Query_Unit_of_Work
{
    public class UnitOfWorkForQueries : IUnitOfWorkForQueries
    {
        private readonly string connectionString;

        public IEducationalInstitutionQueryRepository EducationalInstitutionQueryRepository { get; private set; }

        public UnitOfWorkForQueries(string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString))
                this.connectionString = ConfigurationHelper.GetCurrentSettings("ConnectionStrings:ConnectionToWriteDB") ?? throw new ArgumentNullException(nameof(connectionString));
            else
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