using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationalInstitutionAPI.Data.Contexts;
using EducationalInstitutionAPI.Repositories.EducationalInstitutionBuildingRepository;
using EducationalInstitutionAPI.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Xunit;

namespace EducationalInstitution.API.Tests.IntegrationTests.RepositoriesTests
{
    public class EducationalInstitutionBuildingRepositoryTests
    {
        private readonly string dbConnection;
        private readonly DataContext context;

        public EducationalInstitutionBuildingRepositoryTests()
        {
            dbConnection = ConfigurationHelper.GetCurrentSettings("ConnectionStrings:ConnectionToWriteDB");
            var dbOptions = new DbContextOptionsBuilder<DataContext>()
                                .UseSqlServer(dbConnection, providerOptions => providerOptions.EnableRetryOnFailure());

            context = new(dbOptions.Options);
            CleanupDatabase();
            SeedDatabase();
        }

        [Fact]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldReturnACollectionWithOneElement()
        {
            //Arrange
            string buildingId = "6050efcd87e2647ab7ac443e";
            var repository = new EducationalInstitutionBuildingRepository();

            //Act
            var result = await repository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingId);

            //Assert
            Assert.Equal(1, result.EducationalInstitutions.Count);
        }

        [Fact]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldReturnAnEntityWithName()
        {
            //Arrange
            string buildingId = "6050efcd87e2647ab7ac443e";
            var repository = new EducationalInstitutionBuildingRepository();

            //Act
            var result = await repository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingId);

            //Assert
            Assert.Equal("Integration_Test_Name", result.EducationalInstitutions.ElementAt(0).Name);
        }

        [Fact]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldReturnAnEntityWithDescription()
        {
            //Arrange
            string buildingId = "6050efcd87e2647ab7ac443e";
            var repository = new EducationalInstitutionBuildingRepository();

            //Act
            var result = await repository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingId);

            //Assert
            Assert.Equal("Integration_Test_Description", result.EducationalInstitutions.ElementAt(0).Description);
        }

        [Fact]
        public async Task GivenAValidBuildingID_ToGetAllEducationalInstitutionsWithSameBuildingAsyncMethod_ShouldReturnAnEntityWithAGuidID()
        {
            //Arrange
            string buildingId = "6050efcd87e2647ab7ac443e";
            var repository = new EducationalInstitutionBuildingRepository();

            //Act
            var result = await repository.GetAllEducationalInstitutionsWithSameBuildingAsync(buildingId);

            //Assert
            Assert.NotEqual(Guid.Empty, result.EducationalInstitutions.ElementAt(0).EducationalInstitutionID);
        }

        private void SeedDatabase()
        {
            EducationalInstitutionAPI.Data.EducationalInstitution educationalInstitutionTest = new("Integration_Test_Name", "Integration_Test_Description", "6050efcd87e2647ab7ac443A", new List<string>() { "6050efcd87e2647ab7ac443e" });
            context.EducationalInstitutions.Add(educationalInstitutionTest);
            context.SaveChanges();
        }

        private void CleanupDatabase()
        {
            var testEducationalInstitution = context.EducationalInstitutions.Where(ei => ei.Name == "Integration_Test_Name").ToList();
            testEducationalInstitution.ForEach(ei =>
                    context.Remove(ei)
                );

            var testBuilding = context.EducationalInstitutionsBuildings.Single(b => b.BuildingID == "6050efcd87e2647ab7ac443e");
            context.Remove(testBuilding);
        }
    }
}