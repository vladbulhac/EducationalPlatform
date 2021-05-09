using EducationalInstitution.API.UnitTests;
using EducationalInstitutionAPI.Data.Contexts;
using EducationalInstitutionAPI.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EducationalInstitution.API.IntegrationTests
{
    public class DatabaseFixture : IDisposable
    {
        public string DbConnection { get; init; }
        public DataContext Context { get; private set; }
        public readonly TestDataFromJSONParser testDataHelper;

        public DatabaseFixture()
        {
            testDataHelper = new();
            DbConnection = ConfigurationHelper.GetCurrentSettings("ConnectionStrings:IntegrationTestsDB") ?? throw new Exception("Could not find the database connection string used for testing!");

            SetupContext();

            CleanupDatabase();
            SeedDatabase(testDataHelper);
        }

        private void SetupContext()
        {
            var dbOptions = new DbContextOptionsBuilder<DataContext>()
                           .UseSqlServer(DbConnection, providerOptions => providerOptions.EnableRetryOnFailure(1));

            Context = new(dbOptions.Options);
            Context.Database.Migrate();
        }

        private void SeedDatabase(TestDataFromJSONParser testDataHelper)
        {
            foreach (var educationalInstitution in testDataHelper.EducationalInstitutions)
                Context.EducationalInstitutions.Add(educationalInstitution);

            Context.SaveChanges();
        }

        private void CleanupDatabase()
        {
            var testEducationalInstitution = Context.EducationalInstitutions.ToList();
            Context.RemoveRange(testEducationalInstitution);

            var testBuilding = Context.Buildings.ToList();
            Context.RemoveRange(testBuilding);
        }

        public void Dispose()
        {
            CleanupDatabase();
        }
    }
}