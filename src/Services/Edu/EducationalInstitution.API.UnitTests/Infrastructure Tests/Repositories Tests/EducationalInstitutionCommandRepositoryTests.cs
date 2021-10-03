using EducationalInstitution.API.Tests.Shared;
using EducationalInstitution.Infrastructure;
using EducationalInstitution.Infrastructure.Repositories.Command_Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Domain = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.API.UnitTests.Infrastructure_Tests.Repositories_Tests
{
    public class EducationalInstitutionCommandRepositoryTests : IClassFixture<TestDataFromJSONParser>
    {
        private readonly TestDataFromJSONParser testDataHelper;

        private readonly DataContext dbContext;
        private readonly DbContextOptions<DataContext> dbOptions;
        private readonly IEducationalInstitutionCommandRepository repository;

        /// <remarks>Called before each test</remarks>
        public EducationalInstitutionCommandRepositoryTests(TestDataFromJSONParser testDataHelper)
        {
            this.testDataHelper = testDataHelper;

            dbOptions = new DbContextOptionsBuilder<DataContext>()
                            .UseInMemoryDatabase(databaseName: "edu-inMemory")
                            .Options;

            dbContext = new(dbOptions);
            dbContext.Database.EnsureDeleted();
            dbContext.AddRange(testDataHelper.EducationalInstitutions);
            dbContext.SaveChanges();

            repository = new EducationalInstitutionCommandRepository(dbContext);
        }

        [Fact]
        public async Task GivenAnEducationalInstitution_ToCreateAsyncMethod_ShouldAddItToTheCollectionOfEntities()
        {
            //Arrange
            Domain::EducationalInstitution newEducationalInstitution = new(
                "Test_Name",
                "Test_Description",
                "Test_LocationID",
                new List<string>() {
                    "Test_Building_ID1",
                    "Test_Building_ID2"
                },
                Guid.NewGuid().ToString(),
                testDataHelper.EducationalInstitutions[0]);

            //Act
            await repository.CreateAsync(newEducationalInstitution);
            dbContext.SaveChanges();

            //Assert
            Assert.Contains(newEducationalInstitution, dbContext.EducationalInstitutions);
        }

        [Fact]
        public async Task GivenAValidID_ToGetEducationalInstitutionIncludingAdminsAsyncMethod_ShouldReturnExpectedEntity()
        {
            //Arrange
            var id = testDataHelper.EducationalInstitutions[0].Id;

            //Act
            var result = await repository.GetEducationalInstitutionIncludingAdminsAsync(id);

            //Assert
            Assert.Equal(result, testDataHelper.EducationalInstitutions[0]);
        }

        [Fact]
        public async Task GivenAnInvalidID_ToGetEducationalInstitutionIncludingAdminsAsyncMethod_ShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            //Act
            var result = await repository.GetEducationalInstitutionIncludingAdminsAsync(id);

            //Assert
            Assert.Null(result);
        }
    }
}