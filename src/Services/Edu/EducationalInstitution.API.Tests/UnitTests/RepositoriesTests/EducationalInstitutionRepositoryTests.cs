using EducationalInstitutionAPI.Data.Contexts;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.Repositories.EducationalInstitutionRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.Tests.UnitTests.RepositoriesTests
{
    public class EducationalInstitutionRepositoryTests : IClassFixture<TestDataFromJSONParser>
    {
        private readonly DbContextOptions<DataContext> dbOptions;
        private readonly TestDataFromJSONParser testDataHelper;
        private readonly IEducationalInstitutionRepository eduRepository;
        private readonly DataContext dbContext;

        /// <remarks>Called before each test</remarks>
        public EducationalInstitutionRepositoryTests(TestDataFromJSONParser testDataHelper)
        {
            this.testDataHelper = testDataHelper;

            dbOptions = new DbContextOptionsBuilder<DataContext>()
                            .UseInMemoryDatabase(databaseName: "edu-inMemory")
                            .Options;

            dbContext = new(dbOptions);
            dbContext.Database.EnsureDeleted();
            dbContext.AddRange(testDataHelper.EduInstitutions);
            dbContext.SaveChanges();

            eduRepository = new EducationalInstitutionRepository(dbContext);
        }

        #region GetEntityByIDAsync() TESTS

        [Fact]
        public async Task GivenAValidID_ToGetEntityByIDAsyncMethod_ShouldReturnANotNullEduInstitutionEntity()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            //Act
            var result = await eduRepository.GetEntityByIDAsync(educationalInstitutionID);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GivenAValidID_ToGetEntityByIDAsyncMethod_ShouldReturnAnEduInstitutionEntity_WithMatchingName()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            //Act
            var result = await eduRepository.GetEntityByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(testDataHelper.EduInstitutions[0].Name, result.Name);
        }

        [Fact]
        public async Task GivenAValidID_ToGetEntityByIDAsyncMethod_ShouldReturnAnEduInstitutionEntity_WithMatchingDescription()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            //Act
            var result = await eduRepository.GetEntityByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(testDataHelper.EduInstitutions[0].Description, result.Description);
        }

        [Fact]
        public async Task GivenAValidID_ToGetEntityByIDAsyncMethod_ShouldReturnAnEduInstitutionEntity_WithMatchingIDs()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            //Act
            var result = await eduRepository.GetEntityByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(testDataHelper.EduInstitutions[0].EduInstitutionID, result.EduInstitutionID);
        }

        [Fact]
        public async Task GivenAValidID_ToGetEntityByIDAsyncMethod_ShouldReturnAnEduInstitutionEntity_WithMatchingLocationIDs()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            //Act
            var result = await eduRepository.GetEntityByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(testDataHelper.EduInstitutions[0].LocationID, result.LocationID);
        }

        [Fact]
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

        [Fact]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldNotReturnNull()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            //Act
            var result = await eduRepository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAGetEducationalInstitutionByIDQueryResultObject()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            //Act
            var result = await eduRepository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.IsType<GetEducationalInstitutionByIDQueryResult>(result);
        }

        [Fact]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAGetEducationalInstitutionByIDQueryResultObject_WithMatchingName()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            //Act
            var result = await eduRepository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(testDataHelper.EduInstitutions[0].Name, result.Name);
        }

        [Fact]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAGetEducationalInstitutionByIDQueryResultObject_WithMatchingDescription()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            //Act
            var result = await eduRepository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(testDataHelper.EduInstitutions[0].Description, result.Description);
        }

        [Fact]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAGetEducationalInstitutionByIDQueryResultObject_WithMatchingLocation()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            //Act
            var result = await eduRepository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(testDataHelper.EduInstitutions[0].LocationID, result.LocationID);
        }

        [Fact]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAGetEducationalInstitutionByIDQueryResultObject_WithMatchingBuildings()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            //Act
            var result = await eduRepository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(testDataHelper.EduInstitutions[0].Buildings, result.BuildingsIDs);
        }

        [Fact]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAGetEducationalInstitutionByIDQueryResultObject_WithMatchingParentInstitutions()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            //Act
            var result = await eduRepository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(testDataHelper.EduInstitutions[0].ParentInstitution, result.ParentInstitution);
        }

        [Fact]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAGetEducationalInstitutionByIDQueryResultObject_WithMatchingChildInstitutions()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            //Act
            var result = await eduRepository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(testDataHelper.EduInstitutions[0].ChildInstitutions, result.ChildInstitutions);
        }

        [Fact]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAGetEducationalInstitutionByIDQueryResultObject_WithJoinDateToday()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            //Act
            var result = await eduRepository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(DateTime.UtcNow.Date, result.JoinDate.Date);
        }

        [Fact]
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

        [Fact]
        public async Task GivenAValidLocationID_ToGetAllByLocationAsyncMethod_ShouldReturnAGetAllEducationalInstitutionsByLocationQueryResultObject()
        {
            //Arrange
            string locationID = testDataHelper.EduInstitutions[0].LocationID;

            //Act
            var result = await eduRepository.GetAllByLocationAsync(locationID);

            //Assert
            Assert.IsType<GetAllEducationalInstitutionsByLocationQueryResult>(result);
        }

        [Fact]
        public async Task GivenAValidLocationID_ToGetAllByLocationAsyncMethod_ShouldReturnAGetAllEducationalInstitutionsByLocationQueryResultObject_WithACollectionThatHasOneEntry()
        {
            //Arrange
            string locationID = testDataHelper.EduInstitutions[0].LocationID;

            //Act
            var result = await eduRepository.GetAllByLocationAsync(locationID);

            //Assert
            Assert.Equal(1, result.EducationalInstitutions.Count);
        }

        [Fact]
        public async Task GivenAValidLocationID_ToGetAllByLocationAsyncMethod_ShouldReturnAGetAllEducationalInstitutionsByLocationQueryResultObject_WithMatchingName()
        {
            //Arrange
            string locationID = testDataHelper.EduInstitutions[0].LocationID;

            //Act
            var result = await eduRepository.GetAllByLocationAsync(locationID);

            //Assert
            Assert.Equal(testDataHelper.EduInstitutions[0].Name, result.EducationalInstitutions.ElementAt(0).Name);
        }

        [Fact]
        public async Task GivenAValidLocationID_ToGetAllByLocationAsyncMethod_ShouldReturnAGetAllEducationalInstitutionsByLocationQueryResultObject_WithMatchingDescription()
        {
            //Arrange
            string locationID = testDataHelper.EduInstitutions[0].LocationID;

            //Act
            var result = await eduRepository.GetAllByLocationAsync(locationID);

            //Assert
            Assert.Equal(testDataHelper.EduInstitutions[0].Description, result.EducationalInstitutions.ElementAt(0).Description);
        }

        [Fact]
        public async Task GivenAValidLocationID_ToGetAllByLocationAsyncMethod_ShouldReturnAGetAllEducationalInstitutionsByLocationQueryResultObject_WithMatchingBuildings()
        {
            //Arrange
            string locationID = testDataHelper.EduInstitutions[0].LocationID;

            //Act
            var result = await eduRepository.GetAllByLocationAsync(locationID);

            //Assert
            Assert.Equal(testDataHelper.EduInstitutions[0].Buildings, result.EducationalInstitutions.ElementAt(0).BuildingsIDs);
        }

        [Fact]
        public async Task GivenAnInvalidLocationID_ToGetAllByLocationAsyncMethod_ShouldReturnAGetAllEducationalInstitutionsByLocationQueryResultObject_WithEmptyCollection()
        {
            //Arrange
            string locationID = "bad_location_test";

            //Act
            var result = await eduRepository.GetAllByLocationAsync(locationID);

            //Assert
            Assert.Empty(result.EducationalInstitutions);
        }

        #endregion GetAllByLocationAsync() TESTS

        #region CreateAsync() TESTS

        [Fact]
        public async Task GivenAnEduInstitution_ToCreateAsyncMethod_ShouldAddItToTheCollectionOfEntities()
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
                testDataHelper.EduInstitutions[0]);

            //Act
            await eduRepository.CreateAsync(newEducationalInstitution);
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
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;
            string locationID = "Update_Test_LocationID";
            var buildings = new List<string>() { "Update_Test_Building_ID1",
                    "Update_Test_Building_ID2" };

            //Act
            var result = await eduRepository.UpdateAsync(educationalInstitutionID, locationID, buildings);
            dbContext.SaveChanges();

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GivenAValidID_LocationID_ToUpdateAsyncMethod_ShouldReturnTheNewLocationID()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;
            string locationID = "Update_Test_LocationID";

            //Act
            var result = await eduRepository.UpdateAsync(educationalInstitutionID, locationID);
            dbContext.SaveChanges();

            //Assert
            Assert.Equal(locationID, dbContext.EducationalInstitutions.SingleOrDefault(ei => ei.EduInstitutionID == educationalInstitutionID).LocationID);
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
                testDataHelper.EduInstitutions[0]);

            await eduRepository.CreateAsync(newEducationalInstitution);
            dbContext.SaveChanges();

            //Act
            var result = await eduRepository.DeleteAsync(newEducationalInstitution.EduInstitutionID);
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
                testDataHelper.EduInstitutions[0]);

            await eduRepository.CreateAsync(newEducationalInstitution);
            dbContext.SaveChanges();

            //Act
            var result = await eduRepository.DeleteAsync(newEducationalInstitution.EduInstitutionID);
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
            var result = await eduRepository.DeleteAsync(educationalInstitutionID);
            dbContext.SaveChanges();

            //Assert
            Assert.False(result);
        }

        #endregion DeleteAsync() TESTS
    }
}