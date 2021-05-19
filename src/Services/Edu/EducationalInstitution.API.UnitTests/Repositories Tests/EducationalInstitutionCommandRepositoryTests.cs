using EducationalInstitutionAPI.Data.Contexts;
using EducationalInstitutionAPI.Repositories.EducationalInstitution_Repository.Command_Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.UnitTests.Repositories_Tests
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

        #region CreateAsync() TESTS

        [Fact]
        public async Task GivenAnEducationalInstitution_ToCreateAsyncMethod_ShouldAddItToTheCollectionOfEntities()
        {
            //Arrange
            EducationalInstitutionAPI.Data.EducationalInstitution newEducationalInstitution = new(
                "Test_Name",
                "Test_Description",
                "Test_LocationID",
                new List<string>() {
                    "Test_Building_ID1",
                    "Test_Building_ID2"
                },
                new List<Guid>() {
                    Guid.NewGuid(),
                    Guid.NewGuid()
                },
                testDataHelper.EducationalInstitutions[0]);

            //Act
            await repository.CreateAsync(newEducationalInstitution);
            dbContext.SaveChanges();

            //Assert
            Assert.Contains(newEducationalInstitution, dbContext.EducationalInstitutions);
        }

        #endregion CreateAsync() TESTS

        #region UpdateAsync() TESTS

        [Fact]
        public async Task GivenAValidID_LocationID_BuildingsIDs_ToUpdateAsyncMethod_ShouldReturnTrue()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            string locationID = "Update_Test_LocationID";
            var addBuildingsIDs = new List<string>() { "Update_Test_Building_ID1",
                    "Update_Test_Building_ID2" };
            var removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAtOrDefault(0).BuildingID };

            //Act
            var result = await repository.UpdateEntireLocationAsync(educationalInstitutionID, locationID, addBuildingsIDs, removeBuildingsIDs);
            dbContext.SaveChanges();

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GivenAValidID_LocationID_ToUpdateAsyncMethod_ShouldReturnTheNewLocationID()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            string locationID = "Update_Test_LocationID";

            //Act
            var result = await repository.UpdateLocationAsync(educationalInstitutionID, locationID);
            dbContext.SaveChanges();

            //Assert
            Assert.Equal(locationID, dbContext.EducationalInstitutions.SingleOrDefault(ei => ei.EducationalInstitutionID == educationalInstitutionID).LocationID);
        }

        #endregion UpdateAsync() TESTS

        #region DeleteAsync() TESTS

        [Fact]
        public async Task GivenAValidID_ToDeleteAsyncMethod_ShouldReturnTrue()
        {
            //Arrange
            EducationalInstitutionAPI.Data.EducationalInstitution newEducationalInstitution = new(
                "Test_Name",
                "Test_Description",
                "Test_LocationID",
                new List<string>() {
                    "Test_Building_ID1",
                    "Test_Building_ID2"
                },
                new List<Guid>() { Guid.NewGuid() },
                testDataHelper.EducationalInstitutions[0]);

            await repository.CreateAsync(newEducationalInstitution);
            dbContext.SaveChanges();

            //Act
            var result = await repository.DeleteAsync(newEducationalInstitution.EducationalInstitutionID);
            dbContext.SaveChanges();

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GivenAValidID_ToDeleteAsyncMethod_ShouldRemoveAnEntity()
        {
            //Arrange
            EducationalInstitutionAPI.Data.EducationalInstitution newEducationalInstitution = new(
                "Test_Name",
                "Test_Description",
                "Test_LocationID",
                new List<string>() {
                    "Test_Building_ID1",
                    "Test_Building_ID2"
                },
                new List<Guid>() { Guid.NewGuid() },
                testDataHelper.EducationalInstitutions[0]);

            await repository.CreateAsync(newEducationalInstitution);
            dbContext.SaveChanges();

            //Act
            var result = await repository.DeleteAsync(newEducationalInstitution.EducationalInstitutionID);
            dbContext.SaveChanges();

            //Assert
            Assert.DoesNotContain(newEducationalInstitution, dbContext.EducationalInstitutions);
        }

        [Fact]
        public async Task GivenAnInvalidID_ToDeleteAsyncMethod_ShouldRemoveFalse()
        {
            //Arrange
            Guid educationalInstitutionID = new("5b426f94-d83f-4af0-a578-a09116eff0b7");

            //Act
            var result = await repository.DeleteAsync(educationalInstitutionID);
            dbContext.SaveChanges();

            //Assert
            Assert.False(result);
        }

        #endregion DeleteAsync() TESTS

        /*       Commented due to the new split of repositories in commands and queries
         *       #region GetEntityByIDAsync() TESTS

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAValidID_ToGetEntityByIDAsyncMethod_ShouldReturnANotNullEducationalInstitutionEntity()
                {
                    //Arrange
                    Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

                    //Act
                    var result = await eduRepository.GetEntityByIDAsync(educationalInstitutionID);

                    //Assert
                    Assert.NotNull(result);
                }

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAValidID_ToGetEntityByIDAsyncMethod_ShouldReturnAnEducationalInstitutionEntity_WithMatchingName()
                {
                    //Arrange
                    Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

                    //Act
                    var result = await eduRepository.GetEntityByIDAsync(educationalInstitutionID);

                    //Assert
                    Assert.Equal(testDataHelper.EducationalInstitutions[0].Name, result.Name);
                }

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAValidID_ToGetEntityByIDAsyncMethod_ShouldReturnAnEducationalInstitutionEntity_WithMatchingDescription()
                {
                    //Arrange
                    Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

                    //Act
                    var result = await eduRepository.GetEntityByIDAsync(educationalInstitutionID);

                    //Assert
                    Assert.Equal(testDataHelper.EducationalInstitutions[0].Description, result.Description);
                }

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAValidID_ToGetEntityByIDAsyncMethod_ShouldReturnAnEducationalInstitutionEntity_WithMatchingIDs()
                {
                    //Arrange
                    Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

                    //Act
                    var result = await eduRepository.GetEntityByIDAsync(educationalInstitutionID);

                    //Assert
                    Assert.Equal(testDataHelper.EducationalInstitutions[0].EducationalInstitutionID, result.EducationalInstitutionID);
                }

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAValidID_ToGetEntityByIDAsyncMethod_ShouldReturnAnEducationalInstitutionEntity_WithMatchingLocationIDs()
                {
                    //Arrange
                    Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

                    //Act
                    var result = await eduRepository.GetEntityByIDAsync(educationalInstitutionID);

                    //Assert
                    Assert.Equal(testDataHelper.EducationalInstitutions[0].LocationID, result.LocationID);
                }

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAnInvalidID_ToGetEntityByIDAsyncMethod_ShouldReturnNull()
                {
                    //Arrange
                    Guid educationalInstitutionID = new("5b426f94-d83f-4af0-a578-a09116eff0b7");

                    //Act
                    var result = await eduRepository.GetEntityByIDAsync(educationalInstitutionID);

                    //Assert
                    Assert.Null(result);
                }

                #endregion GetEntityByIDAsync() TESTS

                #region GetByIDAsync() TESTS

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldNotReturnNull()
                {
                    //Arrange
                    Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

                    //Act
                    var result = await eduRepository.GetByIDAsync(educationalInstitutionID);

                    //Assert
                    Assert.NotNull(result);
                }

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAGetEducationalInstitutionByIDQueryResultObject()
                {
                    //Arrange
                    Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

                    //Act
                    var result = await eduRepository.GetByIDAsync(educationalInstitutionID);

                    //Assert
                    Assert.IsType<GetEducationalInstitutionByIDQueryResult>(result);
                }

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAGetEducationalInstitutionByIDQueryResultObject_WithMatchingName()
                {
                    //Arrange
                    Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

                    //Act
                    var result = await eduRepository.GetByIDAsync(educationalInstitutionID);

                    //Assert
                    Assert.Equal(testDataHelper.EducationalInstitutions[0].Name, result.Name);
                }

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAGetEducationalInstitutionByIDQueryResultObject_WithMatchingDescription()
                {
                    //Arrange
                    Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

                    //Act
                    var result = await eduRepository.GetByIDAsync(educationalInstitutionID);

                    //Assert
                    Assert.Equal(testDataHelper.EducationalInstitutions[0].Description, result.Description);
                }

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAGetEducationalInstitutionByIDQueryResultObject_WithMatchingLocation()
                {
                    //Arrange
                    Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

                    //Act
                    var result = await eduRepository.GetByIDAsync(educationalInstitutionID);

                    //Assert
                    Assert.Equal(testDataHelper.EducationalInstitutions[0].LocationID, result.LocationID);
                }

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAGetEducationalInstitutionByIDQueryResultObject_WithMatchingBuildings()
                {
                    //Arrange
                    Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

                    //Act
                    var result = await eduRepository.GetByIDAsync(educationalInstitutionID);

                    //Assert
                    Assert.Equal(new HashSet<string>() { "6050efcd87e2647ab7ac443e", "building12", "building123" }, result.BuildingsIDs);
                }

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAGetEducationalInstitutionByIDQueryResultObject_WithMatchingParentInstitutionID()
                {
                    //Arrange
                    Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

                    //Act
                    var result = await eduRepository.GetByIDAsync(educationalInstitutionID);

                    //Assert
                    Assert.Equal(testDataHelper.EducationalInstitutions[0].ParentInstitution.EducationalInstitutionID, result.ParentInstitution.EducationalInstitutionID);
                }

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAGetEducationalInstitutionByIDQueryResultObject_WithMatchingParentInstitutionName()
                {
                    //Arrange
                    Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

                    //Act
                    var result = await eduRepository.GetByIDAsync(educationalInstitutionID);

                    //Assert
                    Assert.Equal(testDataHelper.EducationalInstitutions[0].ParentInstitution.Name, result.ParentInstitution.Name);
                }

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAGetEducationalInstitutionByIDQueryResultObject_WithMatchingParentInstitutionDescription()
                {
                    //Arrange
                    Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

                    //Act
                    var result = await eduRepository.GetByIDAsync(educationalInstitutionID);

                    //Assert
                    Assert.Equal(testDataHelper.EducationalInstitutions[0].ParentInstitution.Description, result.ParentInstitution.Description);
                }

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAGetEducationalInstitutionByIDQueryResultObject_WithMatchingChildInstitutionID()
                {
                    //Arrange
                    Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[1].EducationalInstitutionID;

                    //Act
                    var result = await eduRepository.GetByIDAsync(educationalInstitutionID);

                    //Assert
                    Assert.Equal(testDataHelper.EducationalInstitutions[0].ChildInstitutions.ElementAt(0).EducationalInstitutionID, result.ChildInstitutions.ElementAt(0).EducationalInstitutionID);
                }

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAGetEducationalInstitutionByIDQueryResultObject_WithMatchingChildInstitutionName()
                {
                    //Arrange
                    Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[1].EducationalInstitutionID;

                    //Act
                    var result = await eduRepository.GetByIDAsync(educationalInstitutionID);

                    //Assert
                    Assert.Equal(testDataHelper.EducationalInstitutions[0].ChildInstitutions.ElementAt(0).Name, result.ChildInstitutions.ElementAt(0).Name);
                }

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAGetEducationalInstitutionByIDQueryResultObject_WithMatchingChildInstitutionDescription()
                {
                    //Arrange
                    Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[1].EducationalInstitutionID;

                    //Act
                    var result = await eduRepository.GetByIDAsync(educationalInstitutionID);

                    //Assert
                    Assert.Equal(testDataHelper.EducationalInstitutions[0].ChildInstitutions.ElementAt(0).Description, result.ChildInstitutions.ElementAt(0).Description);
                }

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAGetEducationalInstitutionByIDQueryResultObject_WithJoinDateToday()
                {
                    //Arrange
                    Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

                    //Act
                    var result = await eduRepository.GetByIDAsync(educationalInstitutionID);

                    //Assert
                    Assert.Equal(DateTime.UtcNow.Date, result.JoinDate.Date);
                }

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAnInvalidID_ToGetByIDAsyncMethod_ShouldReturnNull()
                {
                    //Arrange
                    Guid educationalInstitutionID = new("5b426f94-d83f-4af0-a578-a09116eff0b7");

                    //Act
                    var result = await eduRepository.GetByIDAsync(educationalInstitutionID);

                    //Assert
                    Assert.Null(result);
                }

                #endregion GetByIDAsync() TESTS

                #region GetAllByLocationAsync() TESTS

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAValidLocationID_ToGetAllByLocationAsyncMethod_ShouldReturnAGetAllEducationalInstitutionsByLocationQueryResultObject()
                {
                    //Arrange
                    string locationID = testDataHelper.EducationalInstitutions[0].LocationID;

                    //Act
                    var result = await eduRepository.GetAllByLocationAsync(locationID);

                    //Assert
                    Assert.IsType<GetAllEducationalInstitutionsByLocationQueryResult>(result);
                }

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAValidLocationID_ToGetAllByLocationAsyncMethod_ShouldReturnAGetAllEducationalInstitutionsByLocationQueryResultObject_WithACollectionThatHasOneEntry()
                {
                    //Arrange
                    string locationID = testDataHelper.EducationalInstitutions[0].LocationID;

                    //Act
                    var result = await eduRepository.GetAllByLocationAsync(locationID);

                    //Assert
                    Assert.Equal(1, result.EducationalInstitutions.Count);
                }

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAValidLocationID_ToGetAllByLocationAsyncMethod_ShouldReturnAGetAllEducationalInstitutionsByLocationQueryResultObject_WithMatchingName()
                {
                    //Arrange
                    string locationID = testDataHelper.EducationalInstitutions[0].LocationID;

                    //Act
                    var result = await eduRepository.GetAllByLocationAsync(locationID);

                    //Assert
                    Assert.Equal(testDataHelper.EducationalInstitutions[0].Name, result.EducationalInstitutions.ElementAt(0).Name);
                }

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAValidLocationID_ToGetAllByLocationAsyncMethod_ShouldReturnAGetAllEducationalInstitutionsByLocationQueryResultObject_WithMatchingDescription()
                {
                    //Arrange
                    string locationID = testDataHelper.EducationalInstitutions[0].LocationID;

                    //Act
                    var result = await eduRepository.GetAllByLocationAsync(locationID);

                    //Assert
                    Assert.Equal(testDataHelper.EducationalInstitutions[0].Description, result.EducationalInstitutions.ElementAt(0).Description);
                }

                [Fact]
                public async Task GivenAValidLocationID_ToGetAllByLocationAsyncMethod_ShouldReturnAGetAllEducationalInstitutionsByLocationQueryResultObject_WithMatchingBuildings()
                {
                    //Arrange
                    string locationID = testDataHelper.EducationalInstitutions[0].LocationID;

                    //Act
                    var result = await eduRepository.GetAllByLocationAsync(locationID);

                    //Assert
                    Assert.Equal(testDataHelper.EducationalInstitutions[0].Buildings, result.EducationalInstitutions.ElementAt(0).BuildingsIDs);
                }

                [Fact(Skip = "Works with Entity Framework Core LINQ query")]
                public async Task GivenAnInvalidLocationID_ToGetAllByLocationAsyncMethod_ShouldReturnAGetAllEducationalInstitutionsByLocationQueryResultObject_WithEmptyCollection()
                {
                    //Arrange
                    string locationID = "bad_location_test";

                    //Act
                    var result = await eduRepository.GetAllByLocationAsync(locationID);

                    //Assert
                    Assert.Empty(result.EducationalInstitutions);
                }

                #endregion GetAllByLocationAsync() TESTS*/
    }
}