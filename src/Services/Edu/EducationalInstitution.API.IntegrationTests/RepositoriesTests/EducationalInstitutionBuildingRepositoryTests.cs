using EducationalInstitutionAPI.Repositories.EducationalInstitutionBuildingRepository;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.IntegrationTests.RepositoriesTests
{
    [Collection("Database collection")]
    public class EducationalInstitutionBuildingRepositoryTests
    {
        private readonly DatabaseFixture dbFixture;
        private readonly IEducationalInstitutionBuildingRepository repository;

        /// <remarks>Called before each test</remarks>
        public EducationalInstitutionBuildingRepositoryTests(DatabaseFixture dbFixture)
        {
            this.dbFixture = dbFixture;
            repository = new EducationalInstitutionBuildingRepository(dbFixture.DbConnection);
        }

        [Fact]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldReturnACollectionWithTwoElements()
        {
            //Arrange
            string buildingId = "6050efcd87e2647ab7ac443e";

            //Act
            var result = await repository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingId);

            //Assert
            Assert.Equal(2, result.EducationalInstitutions.Count);
        }

        [Fact]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldContainAnEntityWithName_UniversityofTesting()
        {
            //Arrange
            string buildingId = "6050efcd87e2647ab7ac443e";

            //Act
            var result = await repository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingId);

            //Assert
            Assert.Contains(result.EducationalInstitutions, ei => ei.Name.Equals("University of Testing"));
        }

        [Fact]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldContainAnEntityWithName_UniversityofTestingOne()
        {
            //Arrange
            string buildingId = "6050efcd87e2647ab7ac443e";

            //Act
            var result = await repository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingId);

            //Assert
            Assert.Contains(result.EducationalInstitutions, ei => ei.Name.Equals("University of Testing One"));
        }

        [Fact]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldContainAnEntityWithDescription_Testdata()
        {
            //Arrange
            string buildingId = "6050efcd87e2647ab7ac443e";

            //Act
            var result = await repository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingId);

            //Assert
            Assert.Contains(result.EducationalInstitutions, ei => ei.Description.Equals("Test data"));
        }

        [Fact]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldContainAnEntityWithDescription_TestMoreData()
        {
            //Arrange
            string buildingId = "6050efcd87e2647ab7ac443e";

            //Act
            var result = await repository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingId);

            //Assert
            Assert.Contains(result.EducationalInstitutions, ei => ei.Description.Equals("Test More Data"));
        }

        [Fact]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldReturnTheFirstEntityWithAGuidID()
        {
            //Arrange
            string buildingId = "6050efcd87e2647ab7ac443e";

            //Act
            var result = await repository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingId);

            //Assert
            Assert.NotEqual(Guid.Empty, result.EducationalInstitutions.ElementAt(0).EducationalInstitutionID);
        }

        [Fact]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldReturnTheSecondEntityWithAGuidID()
        {
            //Arrange
            string buildingId = "6050efcd87e2647ab7ac443e";

            //Act
            var result = await repository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingId);

            //Assert
            Assert.NotEqual(Guid.Empty, result.EducationalInstitutions.ElementAt(1).EducationalInstitutionID);
        }

        [Fact]
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