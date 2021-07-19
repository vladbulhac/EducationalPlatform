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

        [Fact]
        public async Task GivenAValidID_LocationID_BuildingsIDs_ToUpdateEntireLocationAsyncMethod_ShouldReturnCollectionWithExpectedAdminsIDs()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            var adminsIDs = testDataHelper.EducationalInstitutions[0].Admins.Select(a => a.AdminID).ToList();
            string locationID = "Update_Test_LocationID";
            var addBuildingsIDs = new List<string>() { "Update_Test_Building_ID1", "Update_Test_Building_ID2" };
            var removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAtOrDefault(0).BuildingID };

            //Act
            var result = await repository.UpdateEntireLocationAsync(educationalInstitutionID, locationID, addBuildingsIDs, removeBuildingsIDs);
            dbContext.SaveChanges();

            //Assert
            Assert.Equal(adminsIDs, result.AdminsToNotify);
        }

        [Fact]
        public async Task GivenAValidID_LocationID_BuildingsIDs_ToUpdateEntireLocationAsyncMethod_ShouldReturnCollectionOfAdminsIDsWithOneElement()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            var adminsIDs = testDataHelper.EducationalInstitutions[0].Admins.Select(a => a.AdminID).ToList();
            string locationID = "Update_Test_LocationID";
            var addBuildingsIDs = new List<string>() { "Update_Test_Building_ID21", "Update_Test_Building_ID22" };
            var removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAtOrDefault(0).BuildingID };

            //Act
            var result = await repository.UpdateEntireLocationAsync(educationalInstitutionID, locationID, addBuildingsIDs, removeBuildingsIDs);
            dbContext.SaveChanges();

            //Assert
            Assert.Single(result.AdminsToNotify);
        }

        [Fact]
        public async Task GivenAValidID_LocationID_BuildingsIDs_IDDoesntExistInDatabase_ToUpdateEntireLocationAsyncMethod_ShouldReturnDefault()
        {
            //Arrange
            Guid educationalInstitutionID = Guid.NewGuid();
            string locationID = "Update_Test_LocationID";
            var addBuildingsIDs = new List<string>() { "Update_Test_Building_ID1", "Update_Test_Building_ID2" };
            var removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAtOrDefault(0).BuildingID };

            //Act
            var result = await repository.UpdateEntireLocationAsync(educationalInstitutionID, locationID, addBuildingsIDs, removeBuildingsIDs);
            dbContext.SaveChanges();

            //Assert
            Assert.Equal(default, result);
        }

        [Fact]
        public async Task GivenAValidID_LocationID_ToUpdateLocationAsyncMethod_ShouldReturnTheNewLocationID()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            var adminsIDs = testDataHelper.EducationalInstitutions[0].Admins.Select(a => a.AdminID).ToList();
            string locationID = "Update_Test_LocationID";

            //Act
            var result = await repository.UpdateLocationAsync(educationalInstitutionID, locationID);
            dbContext.SaveChanges();

            //Assert
            Assert.Equal(locationID, dbContext.EducationalInstitutions.SingleOrDefault(ei => ei.Id == educationalInstitutionID).LocationID);
        }

        [Fact]
        public async Task GivenAValidID_LocationID_ToUpdateLocationAsyncMethod_ShouldReturnACollectionOfAdminsIDs()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            var adminsIDs = testDataHelper.EducationalInstitutions[0].Admins.Select(a => a.AdminID).ToList();
            string locationID = "Update_Test_LocationID";

            //Act
            var result = await repository.UpdateLocationAsync(educationalInstitutionID, locationID);
            dbContext.SaveChanges();

            //Assert
            Assert.Equal(adminsIDs, result.AdminsToNotify);
        }

        [Fact]
        public async Task GivenAValidID_LocationID_IDDoesntExistInDatabase_ToUpdateLocationAsyncMethod_ShouldReturnDefault()
        {
            //Arrange
            Guid educationalInstitutionID = Guid.NewGuid();
            string locationID = "Update_Test_LocationID";

            //Act
            var result = await repository.UpdateLocationAsync(educationalInstitutionID, locationID);
            dbContext.SaveChanges();

            //Assert
            Assert.Equal(default, result);
        }

        [Fact]
        public async Task GivenAValidID_BuildingsIDs_IDDoesntExistInDatabase_ToUpdateBuildingsAsyncMethod_ShouldReturnDefault()
        {
            //Arrange
            Guid educationalInstitutionID = Guid.NewGuid();
            var addBuildingsIDs = new List<string>() { "Update_Test_Building_ID101", "Update_Test_Building_ID201" };
            var removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAtOrDefault(0).BuildingID };

            //Act
            var result = await repository.UpdateBuildingsAsync(educationalInstitutionID, addBuildingsIDs, removeBuildingsIDs);
            dbContext.SaveChanges();

            //Assert
            Assert.Equal(default, result);
        }

        [Fact]
        public async Task GivenAValidID_BuildingsIDs_ToUpdateBuildingsAsyncMethod_ShouldReturnCollectionOfAdminsIDs()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            var adminsIDs = testDataHelper.EducationalInstitutions[0].Admins.Select(a => a.AdminID).ToList();
            var addBuildingsIDs = new List<string>() { "Update_Test_Building_ID101", "Update_Test_Building_ID201" };
            var removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAtOrDefault(0).BuildingID };

            //Act
            var result = await repository.UpdateBuildingsAsync(educationalInstitutionID, addBuildingsIDs, removeBuildingsIDs);
            dbContext.SaveChanges();

            //Assert
            Assert.Equal(adminsIDs, result.AdminsToNotify);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionID_ParentID_ToUpdateParentAsyncMethod_ShouldReturnCollectionOfAdminsIDs()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            Guid parentInstitutionID = testDataHelper.EducationalInstitutions[3].Id;

            var adminsIDs = testDataHelper.EducationalInstitutions[0].Admins.Select(a => a.AdminID).ToList();

            //Act
            var result = await repository.UpdateParentInstitutionAsync(educationalInstitutionID, parentInstitutionID);
            dbContext.SaveChanges();

            //Assert
            Assert.Equal(adminsIDs, result.AdminsToNotify);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionID_ParentID_ToUpdateParentAsyncMethod_ShouldHaveTheNewParentInstitution()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            Guid parentInstitutionID = testDataHelper.EducationalInstitutions[3].Id;

            var adminsIDs = testDataHelper.EducationalInstitutions[0].Admins.Select(a => a.AdminID).ToList();

            //Act
            var result = await repository.UpdateParentInstitutionAsync(educationalInstitutionID, parentInstitutionID);
            dbContext.SaveChanges();

            //Assert
            Assert.Equal(testDataHelper.EducationalInstitutions[3], dbContext.EducationalInstitutions.Single(ei => ei.Id == educationalInstitutionID).ParentInstitution);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionID_DefaultParentID_ToUpdateParentAsyncMethod_ShouldCollectionOfAdminsIDs()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            Guid parentInstitutionID = default;

            var adminsIDs = testDataHelper.EducationalInstitutions[0].Admins.Select(a => a.AdminID).ToList();

            //Act
            var result = await repository.UpdateParentInstitutionAsync(educationalInstitutionID, parentInstitutionID);
            dbContext.SaveChanges();

            //Assert
            Assert.Equal(adminsIDs, result.AdminsToNotify);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionID_DefaultParentID_ToUpdateParentAsyncMethod_ShouldHaveNoParentInstitution()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            Guid parentInstitutionID = default;

            var adminsIDs = testDataHelper.EducationalInstitutions[0].Admins.Select(a => a.AdminID).ToList();

            //Act
            var result = await repository.UpdateParentInstitutionAsync(educationalInstitutionID, parentInstitutionID);
            dbContext.SaveChanges();

            //Assert
            Assert.Null(dbContext.EducationalInstitutions.Single(ei => ei.Id == educationalInstitutionID).ParentInstitution);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionID_ParentID_IDDoesntExistInDatabase_ToUpdateParentAsyncMethod_ShouldReturnDefault()
        {
            //Arrange
            Guid educationalInstitutionID = Guid.NewGuid();
            Guid parentInstitutionID = testDataHelper.EducationalInstitutions[3].Id;

            var adminsIDs = testDataHelper.EducationalInstitutions[0].Admins.Select(a => a.AdminID).ToList();

            //Act
            var result = await repository.UpdateParentInstitutionAsync(educationalInstitutionID, parentInstitutionID);
            dbContext.SaveChanges();

            //Assert
            Assert.Equal(default, result);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionID_ParentID_ParentIDDoesntExistInDatabase_ToUpdateParentAsyncMethod_ShouldReturnDefault()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            Guid parentInstitutionID = Guid.NewGuid();

            var adminsIDs = testDataHelper.EducationalInstitutions[0].Admins.Select(a => a.AdminID).ToList();

            //Act
            var result = await repository.UpdateParentInstitutionAsync(educationalInstitutionID, parentInstitutionID);
            dbContext.SaveChanges();

            //Assert
            Assert.Equal(default, result);
        }

        [Fact]
        public async Task GivenAValidID_ToScheduleForDeletionAsyncMethod_ShouldReturnCollectionOfAdminsIDs()
        {
            //Arrange
            var educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            var adminsIDs = testDataHelper.EducationalInstitutions[0].Admins.Select(a => a.AdminID).ToList();

            //Act
            var result = await repository.ScheduleForDeletionAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(adminsIDs, result.AdminsToNotify);

            //Clean up
            UndoDeletion(educationalInstitutionID);
        }

        [Fact]
        public async Task GivenAValidID_ToScheduleForDeletionAsyncMethod_ShouldReturnExpectedScheduledDateForDeletion_Day()
        {
            //Arrange
            var educationalInstitutionID = testDataHelper.EducationalInstitutions[1].Id;
            var adminsIDs = testDataHelper.EducationalInstitutions[1].Admins.Select(a => a.AdminID).ToList();

            //Act
            var result = await repository.ScheduleForDeletionAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(DateTime.UtcNow.AddDays(30).Day, result.ScheduledDateForDeletion.Day);

            //Clean up
            UndoDeletion(educationalInstitutionID);
        }

        [Fact]
        public async Task GivenAValidID_ToScheduleForDeletionAsyncMethod_ShouldReturnExpectedScheduledDateForDeletion_Month()
        {
            //Arrange
            var educationalInstitutionID = testDataHelper.EducationalInstitutions[2].Id;
            var adminsIDs = testDataHelper.EducationalInstitutions[2].Admins.Select(a => a.AdminID).ToList();

            //Act
            var result = await repository.ScheduleForDeletionAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(DateTime.UtcNow.AddDays(30).Month, result.ScheduledDateForDeletion.Month);

            //Clean up
            UndoDeletion(educationalInstitutionID);
        }

        [Fact]
        public async Task GivenAValidID_ToScheduleForDeletionAsyncMethod_ShouldReturnExpectedScheduledDateForDeletion_Year()
        {
            //Arrange
            var educationalInstitutionID = testDataHelper.EducationalInstitutions[3].Id;
            var adminsIDs = testDataHelper.EducationalInstitutions[3].Admins.Select(a => a.AdminID).ToList();

            //Act
            var result = await repository.ScheduleForDeletionAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(DateTime.UtcNow.AddDays(30).Year, result.ScheduledDateForDeletion.Year);

            //Clean up
            UndoDeletion(educationalInstitutionID);
        }

        [Fact]
        public async Task GivenAValidID_ToScheduleForDeletionAsyncMethod_ShouldHaveTrueEntityAccessIsDisabled()
        {
            //Arrange
            var educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            var adminsIDs = testDataHelper.EducationalInstitutions[0].Admins.Select(a => a.AdminID).ToList();

            //Act
            var result = await repository.ScheduleForDeletionAsync(educationalInstitutionID);

            //Assert
            Assert.True(dbContext.EducationalInstitutions.Single(ei => ei.Id == educationalInstitutionID).Access.IsDisabled);

            //Clean up
            UndoDeletion(educationalInstitutionID);
        }

        private void UndoDeletion(Guid id)
        {
            var educationalInstitution = dbContext.EducationalInstitutions.Single(e => e.Id == id);
            educationalInstitution.ClearDeletionDate();
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