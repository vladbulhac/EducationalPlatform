namespace EducationalInstitution.API.UnitTests.RepositoriesTests
{
    /* Commented due to the new split in commands and queries repositories
     * public class EducationalInstitutionBuildingRepositoryTests : IClassFixture<TestDataFromJSONParser>
    {
        private readonly DbContextOptions<DataContext> dbOptions;
        private readonly TestDataFromJSONParser testDataHelper;
        private readonly IEducationalInstitutionBuildingRepository eduBuildingRepository;
        private readonly DataContext dbContext;

        /// <remarks>Called before each test</remarks>
        public EducationalInstitutionBuildingRepositoryTests(TestDataFromJSONParser testDataHelper)
        {
            this.testDataHelper = testDataHelper;

            dbOptions = new DbContextOptionsBuilder<DataContext>()
                            .UseInMemoryDatabase(databaseName: "eduBuilding-inMemory")
                            .Options;

            dbContext = new(dbOptions);
            dbContext.Database.EnsureDeleted();
            dbContext.AddRange(testDataHelper.EducationalInstitutions);
            dbContext.SaveChanges();

            eduBuildingRepository = new EducationalInstitutionBuildingRepository(dbContext);
        }

        [Fact(Skip = "Works with Entity Framework Core LINQ query")]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldReturnAGetAllEducationalInstitutionsWithSameBuildingQueryResult()
        {
            //Arrange
            string buildingID = "building123";

            //Act
            var result = await eduBuildingRepository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingID);

            //Assert
            Assert.IsType<GetAllEducationalInstitutionsWithSameBuildingQueryResult>(result);
        }

        [Fact(Skip = "Works with Entity Framework Core LINQ query")]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldReturnACollectionWithTwoElements()
        {
            //Arrange
            string buildingID = "building123";

            //Act
            var result = await eduBuildingRepository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingID);

            //Assert
            Assert.Equal(2, result.EducationalInstitutions.Count);
        }

        [Fact(Skip = "Works with Entity Framework Core LINQ query")]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldReturnACollectionWithOneElement()
        {
            //Arrange
            string buildingID = "building12";

            //Act
            var result = await eduBuildingRepository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingID);

            //Assert
            Assert.Equal(1, result.EducationalInstitutions.Count);
        }

        [Fact(Skip = "Works with Entity Framework Core LINQ query")]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldReturnACollectionWithOneElement_WithExpectedName()
        {
            //Arrange
            string buildingID = "building12";

            //Act
            var result = await eduBuildingRepository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingID);

            //Assert
            Assert.Contains(result.EducationalInstitutions, ei => ei.Name == testDataHelper.EducationalInstitutions[0].Name);
        }

        [Fact(Skip = "Works with Entity Framework Core LINQ query")]
        public async Task GivenAnInvalidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldReturnAEmptyCollection()
        {
            //Arrange
            string buildingID = "invalid_building";

            //Act
            var result = await eduBuildingRepository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingID);

            //Assert
            Assert.Equal(0, result.EducationalInstitutions.Count);
        }
    }*/
}