using EducationalInstitution.API.IntegrationTests.Utils;
using EducationalInstitutionAPI.Repositories.EducationalInstitutionBuilding_Repository.Query_Repository;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.IntegrationTests.RepositoriesTests.BuildingRepository_Tests
{
    [Collection("Database collection")]
    public class EducationalInstitutionBuildingQueryRepositoryTests
    {
        private readonly DatabaseFixture dbFixture;
        private readonly IEducationalInstitutionBuildingQueryRepository repository;

        /// <remarks>Called before each test</remarks>
        public EducationalInstitutionBuildingQueryRepositoryTests(DatabaseFixture dbFixture)
        {
            this.dbFixture = dbFixture;
            repository = new EducationalInstitutionBuildingQueryRepository(dbFixture.DbConnection);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldReturnACollectionWithTwoElements()
        {
            //Arrange
            string buildingId = "6050efcd87e2647ab7ac443e";

            //Act
            var result = await repository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingId);

            //Assert
            Assert.Equal(2, result.EducationalInstitutions.Count);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldContainAnEntityWithName_UniversityofTesting()
        {
            //Arrange
            string buildingId = "6050efcd87e2647ab7ac443e";

            //Act
            var result = await repository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingId);

            //Assert
            Assert.Contains(result.EducationalInstitutions, ei => ei.Name.Equals("University of Testing"));
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldContainAnEntityWithName_UniversityofTestingOne()
        {
            //Arrange
            string buildingId = "6050efcd87e2647ab7ac443e";

            //Act
            var result = await repository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingId);

            //Assert
            Assert.Contains(result.EducationalInstitutions, ei => ei.Name.Equals("University of Testing One"));
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldContainAnEntityWithDescription_Testdata()
        {
            //Arrange
            string buildingId = "6050efcd87e2647ab7ac443e";

            //Act
            var result = await repository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingId);

            //Assert
            Assert.Contains(result.EducationalInstitutions, ei => ei.Description.Equals("Test data"));
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldContainAnEntityWithDescription_TestMoreData()
        {
            //Arrange
            string buildingId = "6050efcd87e2647ab7ac443e";

            //Act
            var result = await repository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingId);

            //Assert
            Assert.Contains(result.EducationalInstitutions, ei => ei.Description.Equals("Test More Data"));
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldReturnTheFirstEntityWithAGuidID()
        {
            //Arrange
            string buildingId = "6050efcd87e2647ab7ac443e";

            //Act
            var result = await repository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingId);

            //Assert
            Assert.NotEqual(Guid.Empty, result.EducationalInstitutions.ElementAt(0).EducationalInstitutionID);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldReturnTheSecondEntityWithAGuidID()
        {
            //Arrange
            string buildingId = "6050efcd87e2647ab7ac443e";

            //Act
            var result = await repository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingId);

            //Assert
            Assert.NotEqual(Guid.Empty, result.EducationalInstitutions.ElementAt(1).EducationalInstitutionID);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAnInvalidBuildingID_ThatDoesntExistInDatabase_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldReturnAnEmptyCollection()
        {
            //Arrange
            string buildingId = "6050efcd87e2647ab7ac443F";

            //Act
            var result = await repository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingId);

            //Assert
            Assert.Empty(result.EducationalInstitutions);
        }
    }
}