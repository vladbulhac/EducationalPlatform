using EducationalInstitution.API.IntegrationTests.Utils;
using EducationalInstitutionAPI.Repositories.EducationalInstitutionAdmin_Repository.Query_Repostiory;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.IntegrationTests.RepositoriesTests.AdminRepository_Tests
{
    [Collection("Database collection")]
    public class EducationalInstitutionAdminQueryRepositoryTests
    {
        private readonly DatabaseFixture dbFixture;
        private readonly IEducationalInstitutionAdminQueryRepository repository;

        /// <remarks>Called before each test</remarks>
        public EducationalInstitutionAdminQueryRepositoryTests(DatabaseFixture dbFixture)
        {
            this.dbFixture = dbFixture;
            repository = new EducationalInstitutionAdminQueryRepository(dbFixture.DbConnection);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidEducationalInstitutionID_ToGetAllAdminsForEducationalInstitutionAsyncMethod_WithIDThatDoesntExistInDatabse_ShouldReturnAnEmptyCollection()
        {
            //Arrange
            var educationalInstitutionID = new Guid("9a0b8def-c8f0-43a4-b6da-030affb9683d");

            //Act
            var result = await repository.GetAllAdminsForEducationalInstitutionAsync(educationalInstitutionID);

            //Assert
            Assert.Empty(result.AdminsIDs);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidEducationalInstitutionID_ToGetAllAdminsForEducationalInstitutionAsyncMethod_ShouldReturnACollectionWithTwoElements()
        {
            //Arrange
            var educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[3].EducationalInstitutionID;

            //Act
            var result = await repository.GetAllAdminsForEducationalInstitutionAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(2, result.AdminsIDs.Count);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidEducationalInstitutionID_ToGetAllAdminsForEducationalInstitutionAsyncMethod_ShouldReturnACollectionWithExpectedID()
        {
            //Arrange
            var educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[3].EducationalInstitutionID;

            //Act
            var result = await repository.GetAllAdminsForEducationalInstitutionAsync(educationalInstitutionID);

            //Assert
            Assert.Contains(new Guid("9a0b8def-c8f0-43a4-b6da-030affb9683d"), result.AdminsIDs);
        }
    }
}