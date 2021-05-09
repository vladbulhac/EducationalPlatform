using EducationalInstitution.API.IntegrationTests.Utils;
using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Repositories.EducationalInstitutionAdmin_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.IntegrationTests.RepositoriesTests
{
    [Collection("Database collection")]
    public class EducationalInstitutionAdminRepositoryTests
    {
        private readonly DatabaseFixture dbFixture;
        private readonly IEducationalInstitutionAdminRepository repository;

        /// <remarks>Called before each test</remarks>
        public EducationalInstitutionAdminRepositoryTests(DatabaseFixture dbFixture)
        {
            this.dbFixture = dbFixture;
            repository = new EducationalInstitutionAdminRepository(dbFixture.Context, dbFixture.DbConnection);
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

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidAdminIDAndEducationalInstitutionID_ToDeleteAsyncMethod_ShouldReturnTrue()
        {
            //Arrange
            var adminID = Guid.NewGuid();
            var educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[3].EducationalInstitutionID;
            var entity = new EducationalInstitutionAdmin(adminID, educationalInstitutionID);

            dbFixture.Context.Admins.Add(entity);
            dbFixture.Context.SaveChanges();

            //Act
            var result = await repository.DeleteAsync(adminID, educationalInstitutionID);
            dbFixture.Context.SaveChanges();

            //Assert
            Assert.True(result);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidAdminIDAndEducationalInstitutionID_ToDeleteAsyncMethod_ShouldRemoveIt()
        {
            //Arrange
            var adminID = Guid.NewGuid();
            var educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[3].EducationalInstitutionID;
            var entity = new EducationalInstitutionAdmin(adminID, educationalInstitutionID);

            dbFixture.Context.Admins.Add(entity);
            dbFixture.Context.SaveChanges();

            //Act
            var result = await repository.DeleteAsync(adminID, educationalInstitutionID);
            dbFixture.Context.SaveChanges();

            //Assert
            Assert.False(dbFixture.Context.Admins.Any(a => a.AdminID == adminID && a.EducationalInstitutionID == educationalInstitutionID));
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidAdminIDAndEducationalInstitutionID_ToDeleteAsyncMethod_WithAdminIDThatDoesntExistInDatabase_ShouldReturnFalse()
        {
            //Arrange
            var adminID = Guid.NewGuid();
            var educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[3].EducationalInstitutionID;

            //Act
            var result = await repository.DeleteAsync(adminID, educationalInstitutionID);
            dbFixture.Context.SaveChanges();

            //Assert
            Assert.False(result);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidAdminIDAndEducationalInstitutionID_ToDeleteAsyncMethod_WithEducationalInstitutionIDThatDoesntExistInDatabase_ShouldReturnFalse()
        {
            //Arrange
            var adminID = new Guid("9a0b8def-c8f0-43a4-b6da-030affb9683d");
            var educationalInstitutionID = Guid.NewGuid();

            //Act
            var result = await repository.DeleteAsync(adminID, educationalInstitutionID);
            dbFixture.Context.SaveChanges();

            //Assert
            Assert.False(result);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidCollectionOfAdminIDsAndEducationalInstitutionID_ToDeleteAsyncMethod_ShouldReturnTrue()
        {
            //Arrange
            var adminsIDs = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid() };
            var educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[3].EducationalInstitutionID;

            foreach (var adminID in adminsIDs)
            {
                var entity = new EducationalInstitutionAdmin(adminID, educationalInstitutionID);

                dbFixture.Context.Admins.Add(entity);
                dbFixture.Context.SaveChanges();
            }

            //Act
            var result = await repository.DeleteAsync(adminsIDs, educationalInstitutionID);
            dbFixture.Context.SaveChanges();

            //Assert
            Assert.True(result);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidCollectionOfAdminIDsAndEducationalInstitutionID_ToDeleteAsyncMethod_ShouldRemoveIt()
        {
            //Arrange
            var adminsIDs = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid() };
            var educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[3].EducationalInstitutionID;

            foreach (var adminID in adminsIDs)
            {
                var entity = new EducationalInstitutionAdmin(adminID, educationalInstitutionID);

                dbFixture.Context.Admins.Add(entity);
                dbFixture.Context.SaveChanges();
            }

            //Act
            var result = await repository.DeleteAsync(adminsIDs, educationalInstitutionID);
            dbFixture.Context.SaveChanges();

            //Assert
            Assert.False(dbFixture.Context.Admins.Any(a => adminsIDs.Contains(a.AdminID) && a.EducationalInstitutionID == educationalInstitutionID));
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidCollectionOfAdminIDsAndEducationalInstitutionID_ToDeleteAsyncMethod_WithEducationalInstitutionIDThatDoesntExistInDatabase_ShouldReturnFalse()
        {
            //Arrange
            var adminsIDs = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid() };
            var educationalInstitutionID = Guid.NewGuid();

            //Act
            var result = await repository.DeleteAsync(adminsIDs, educationalInstitutionID);
            dbFixture.Context.SaveChanges();

            //Assert
            Assert.False(result);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidCollectionOfAdminIDsAndEducationalInstitutionID_ToDeleteAsyncMethod_WithAdminsIDsThatDontExistInDatabase_ShouldReturnFalse()
        {
            //Arrange
            var adminsIDs = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid() };
            var educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[3].EducationalInstitutionID;

            //Act
            var result = await repository.DeleteAsync(adminsIDs, educationalInstitutionID);
            dbFixture.Context.SaveChanges();

            //Assert
            Assert.False(result);
        }
    }
}