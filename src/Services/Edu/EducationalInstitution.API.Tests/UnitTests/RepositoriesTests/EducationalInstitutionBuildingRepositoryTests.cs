using EducationalInstitutionAPI.Data.Contexts;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.Repositories.EducationalInstitutionBuildingRepository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.Tests.UnitTests.RepositoriesTests
{
    public class EducationalInstitutionBuildingRepositoryTests : IClassFixture<TestDataFromJSONParser>
    {
        private readonly DbContextOptions<DataContext> dbOptions;
        private readonly TestDataFromJSONParser testDataHelper;
        private readonly IEducationalInstitutionBuildingRepository eduBuildingRepository;
        private readonly DataContext dbContext;

        public EducationalInstitutionBuildingRepositoryTests(TestDataFromJSONParser testDataHelper)
        {
            this.testDataHelper = testDataHelper;

            dbOptions = new DbContextOptionsBuilder<DataContext>()
                            .UseInMemoryDatabase(databaseName: "eduBuilding-inMemory")
                            .Options;

            dbContext = new(dbOptions);
            dbContext.Database.EnsureDeleted();
            dbContext.AddRange(testDataHelper.EduInstitutions);
            dbContext.SaveChanges();

            eduBuildingRepository = new EducationalInstitutionBuildingRepository(dbContext);
        }

        [Fact]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldReturnAGetAllEducationalInstitutionsWithSameBuildingQueryResult()
        {
            //Arrange
            string buildingID = "building123";

            //Act
            var result = await eduBuildingRepository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingID);

            //Assert
            Assert.IsType<GetAllEducationalInstitutionsWithSameBuildingQueryResult>(result);
        }

        [Fact]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldReturnACollectionWithTwoElements()
        {
            //Arrange
            string buildingID = "building123";

            //Act
            var result = await eduBuildingRepository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingID);

            //Assert
            Assert.Equal(2, result.EducationalInstitutions.Count);
        }

        [Fact]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldReturnACollectionWithOneElement()
        {
            //Arrange
            string buildingID = "building12";

            //Act
            var result = await eduBuildingRepository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingID);

            //Assert
            Assert.Equal(1, result.EducationalInstitutions.Count);
        }

        [Fact]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldReturnACollectionWithOneElement_WithExpectedName()
        {
            //Arrange
            string buildingID = "building12";

            //Act
            var result = await eduBuildingRepository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingID);

            //Assert
            Assert.Contains(result.EducationalInstitutions, ei => ei.Name == testDataHelper.EduInstitutions[0].Name);
        }

        [Fact]
        public async Task GivenAnInvalidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldReturnAEmptyCollection()
        {
            //Arrange
            string buildingID = "invalid_building";

            //Act
            var result = await eduBuildingRepository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingID);

            //Assert
            Assert.Equal(0, result.EducationalInstitutions.Count);
        }
    }
}