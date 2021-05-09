using EducationalInstitution.API.IntegrationTests.Utils;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.Repositories.EducationalInstitution_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.IntegrationTests.RepositoriesTests
{
    [Collection("Database collection")]
    public class EducationalInstitutionRepositoryTests
    {
        private readonly DatabaseFixture dbFixture;
        private IEducationalInstitutionRepository repository;

        /// <remarks>Called before each test</remarks>
        public EducationalInstitutionRepositoryTests(DatabaseFixture dbFixture)
        {
            this.dbFixture = dbFixture;
            repository = new EducationalInstitutionRepository(dbFixture.DbConnection);
        }

        #region GetByIDAsync() TESTS

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAGetEducationalInstitutionByIDQueryResult()
        {
            //Arrange
            Guid educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            //Act
            var result = await repository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.IsType<GetEducationalInstitutionByIDQueryResult>(result);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAnEntityWithExpectedName()
        {
            //Arrange
            Guid educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            //Act
            var result = await repository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(dbFixture.testDataHelper.EducationalInstitutions[0].Name, result.Name);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAnEntityWithExpectedDescription()
        {
            //Arrange
            Guid educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            //Act
            var result = await repository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(dbFixture.testDataHelper.EducationalInstitutions[0].Description, result.Description);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAnEntityWithExpectedLocationID()
        {
            //Arrange
            Guid educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            //Act
            var result = await repository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(dbFixture.testDataHelper.EducationalInstitutions[0].LocationID, result.LocationID);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAnEntityWithExpectedBuildingsIDs()
        {
            //Arrange
            Guid educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            //Act
            var result = await repository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(new HashSet<string>() { "6050efcd87e2647ab7ac443e", "building12", "building123" }, result.BuildingsIDs);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAnEntityWithExpectedParentInstitutionEducationalInstitutionID()
        {
            //Arrange
            Guid educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            //Act
            var result = await repository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(dbFixture.testDataHelper.EducationalInstitutions[0].ParentInstitution.EducationalInstitutionID, result.ParentInstitution.EducationalInstitutionID);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAnEntityWithExpectedParentInstitutionName()
        {
            //Arrange
            Guid educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            //Act
            var result = await repository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(dbFixture.testDataHelper.EducationalInstitutions[0].ParentInstitution.Name, result.ParentInstitution.Name);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAnEntityWithExpectedParentInstitutionDescription()
        {
            //Arrange
            Guid educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            //Act
            var result = await repository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(dbFixture.testDataHelper.EducationalInstitutions[0].ParentInstitution.Description, result.ParentInstitution.Description);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAnEntityWithExpectedJoinDateDate()
        {
            //Arrange
            Guid educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            //Act
            var result = await repository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(dbFixture.testDataHelper.EducationalInstitutions[0].JoinDate.Date, result.JoinDate.Date);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAnEntityWithExpectedJoinDateDayOfTheWeek()
        {
            //Arrange
            Guid educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            //Act
            var result = await repository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(dbFixture.testDataHelper.EducationalInstitutions[0].JoinDate.DayOfWeek, result.JoinDate.DayOfWeek);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAnEntityWithExpectedJoinDateMonth()
        {
            //Arrange
            Guid educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            //Act
            var result = await repository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(dbFixture.testDataHelper.EducationalInstitutions[0].JoinDate.Month, result.JoinDate.Month);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAnEntityWithNoChildInstitutions()
        {
            //Arrange
            Guid educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            //Act
            var result = await repository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Empty(result.ChildInstitutions);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAnEntityWithOneChildInstitution()
        {
            //Arrange
            Guid educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[1].EducationalInstitutionID;

            //Act
            var result = await repository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(1, result.ChildInstitutions.Count);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAnEntityWithExpectedChildInstitutionEducationalInstitutionID()
        {
            //Arrange
            Guid educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[1].EducationalInstitutionID;

            //Act
            var result = await repository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(dbFixture.testDataHelper.EducationalInstitutions[1].ChildInstitutions.ElementAt(0).EducationalInstitutionID, result.ChildInstitutions.ElementAt(0).EducationalInstitutionID);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAnEntityWithExpectedChildInstitutionName()
        {
            //Arrange
            Guid educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[1].EducationalInstitutionID;

            //Act
            var result = await repository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(dbFixture.testDataHelper.EducationalInstitutions[1].ChildInstitutions.ElementAt(0).Name, result.ChildInstitutions.ElementAt(0).Name);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidID_ToGetByIDAsyncMethod_ShouldReturnAnEntityWithExpectedChildInstitutionDescription()
        {
            //Arrange
            Guid educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[1].EducationalInstitutionID;

            //Act
            var result = await repository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(dbFixture.testDataHelper.EducationalInstitutions[1].ChildInstitutions.ElementAt(0).Description, result.ChildInstitutions.ElementAt(0).Description);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAnInvalidID_ThatDoesntExistInDatabase_ToGetByIDAsyncMethod_ShouldReturnNull()
        {
            //Arrange
            Guid educationalInstitutionID = Guid.NewGuid();

            //Act
            var result = await repository.GetByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Null(result);
        }

        #endregion GetByIDAsync() TESTS

        #region GetEntityByIDAsync() TESTS

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidEducationalInstitutionID_ToGetEntityByIDAsyncMethod_ShouldReturnAnEntityWithExpectedEducationalInstitutionID()
        {
            //Arrange
            Guid educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            //Act
            var result = await repository.GetEntityByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(educationalInstitutionID, result.EducationalInstitutionID);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidEducationalInstitutionID_ToGetEntityByIDAsyncMethod_ShouldReturnAnEntityWithExpectedName()
        {
            //Arrange
            Guid educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            //Act
            var result = await repository.GetEntityByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(dbFixture.testDataHelper.EducationalInstitutions[0].Name, result.Name);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidEducationalInstitutionID_ToGetEntityByIDAsyncMethod_ShouldReturnAnEntityWithExpectedDescription()
        {
            //Arrange
            Guid educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            //Act
            var result = await repository.GetEntityByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(dbFixture.testDataHelper.EducationalInstitutions[0].Description, result.Description);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidEducationalInstitutionID_ToGetEntityByIDAsyncMethod_ShouldReturnAnEntityWithExpectedJoinDate()
        {
            //Arrange
            Guid educationalInstitutionID = dbFixture.testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            //Act
            var result = await repository.GetEntityByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Equal(dbFixture.testDataHelper.EducationalInstitutions[0].JoinDate, result.JoinDate);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAnInvalidEducationalInstitutionID_ToGetEntityByIDAsyncMethod_ShouldReturnNull()
        {
            //Arrange
            Guid educationalInstitutionID = Guid.NewGuid();

            //Act
            var result = await repository.GetEntityByIDAsync(educationalInstitutionID);

            //Assert
            Assert.Null(result);
        }

        #endregion GetEntityByIDAsync() TESTS

        #region GetAllBYLocationAsync() TESTS

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidLocationID_ToGetAllByLocationAsyncMethod_ShouldReturnAGetAllEducationalInstitutionsByLocationQueryResult()
        {
            //Arrange
            string locationID = "1350efcd87A2647ab7ac443F";

            //Act
            var result = await repository.GetAllByLocationAsync(locationID);

            //Assert
            Assert.IsType<GetAllEducationalInstitutionsByLocationQueryResult>(result);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidLocationID_ToGetAllByLocationAsyncMethod_ShouldReturnACollectionWithTwoElements()
        {
            //Arrange
            string locationID = "1350efcd87A2647ab7ac443F";

            //Act
            var result = await repository.GetAllByLocationAsync(locationID);

            //Assert
            Assert.Equal(2, result.EducationalInstitutions.Count);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidLocationID_ToGetAllByLocationAsyncMethod_ShouldReturnFirstResultWithExpectedEducationalInstitutionID()
        {
            //Arrange
            string locationID = "1350efcd87A2647ab7ac443F";

            //Act
            var result = await repository.GetAllByLocationAsync(locationID);

            //Assert
            Assert.Equal(dbFixture.testDataHelper.EducationalInstitutions[3].EducationalInstitutionID, result.EducationalInstitutions.ElementAt(0).EducationalInstitutionID);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidLocationID_ToGetAllByLocationAsyncMethod_ShouldReturnFirstResultWithExpectedName()
        {
            //Arrange
            string locationID = "1350efcd87A2647ab7ac443F";

            //Act
            var result = await repository.GetAllByLocationAsync(locationID);

            //Assert
            Assert.Equal(dbFixture.testDataHelper.EducationalInstitutions[3].Name, result.EducationalInstitutions.ElementAt(0).Name);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidLocationID_ToGetAllByLocationAsyncMethod_ShouldReturnFirstResultWithExpectedDescription()
        {
            //Arrange
            string locationID = "1350efcd87A2647ab7ac443F";

            //Act
            var result = await repository.GetAllByLocationAsync(locationID);

            //Assert
            Assert.Equal(dbFixture.testDataHelper.EducationalInstitutions[3].Description, result.EducationalInstitutions.ElementAt(0).Description);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidLocationID_ToGetAllByLocationAsyncMethod_ShouldReturnFirstResultWithExpectedBuildings()
        {
            //Arrange
            string locationID = "1350efcd87A2647ab7ac443F";

            //Act
            var result = await repository.GetAllByLocationAsync(locationID);

            //Assert
            Assert.Equal(new HashSet<string>() { "4150efcd87A2647ab7ac443E", "5550efcd87e2647ab7ac4435" }, result.EducationalInstitutions.ElementAt(0).BuildingsIDs);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidLocationID_ToGetAllByLocationAsyncMethod_ShouldReturnSecondResultWithExpectedEducationalInstitutionID()
        {
            //Arrange
            string locationID = "1350efcd87A2647ab7ac443F";

            //Act
            var result = await repository.GetAllByLocationAsync(locationID);

            //Assert
            Assert.Equal(dbFixture.testDataHelper.EducationalInstitutions[2].EducationalInstitutionID, result.EducationalInstitutions.ElementAt(1).EducationalInstitutionID);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidLocationID_ToGetAllByLocationAsyncMethod_ShouldReturnSecondResultWithExpectedName()
        {
            //Arrange
            string locationID = "1350efcd87A2647ab7ac443F";

            //Act
            var result = await repository.GetAllByLocationAsync(locationID);

            //Assert
            Assert.Equal(dbFixture.testDataHelper.EducationalInstitutions[2].Name, result.EducationalInstitutions.ElementAt(1).Name);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidLocationID_ToGetAllByLocationAsyncMethod_ShouldReturnSecondResultWithExpectedDescription()
        {
            //Arrange
            string locationID = "1350efcd87A2647ab7ac443F";

            //Act
            var result = await repository.GetAllByLocationAsync(locationID);

            //Assert
            Assert.Equal(dbFixture.testDataHelper.EducationalInstitutions[2].Description, result.EducationalInstitutions.ElementAt(1).Description);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAValidLocationID_ToGetAllByLocationAsyncMethod_ShouldReturnSecondResultWithExpectedBuildings()
        {
            //Arrange
            string locationID = "1350efcd87A2647ab7ac443F";

            //Act
            var result = await repository.GetAllByLocationAsync(locationID);

            //Assert
            Assert.Equal(new HashSet<string>() { "6050efcd87A2647ab7ac443E", "6050efcd87e2647ab7ac4435" }, result.EducationalInstitutions.ElementAt(1).BuildingsIDs);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAnInvalidLocationID_ThatDoesntExistInDatabase_ToGetAllByLocationAsyncMethod_ShouldReturnNull()
        {
            //Arrange
            string locationID = "1AA0efcd87A2647ab7ac4430";

            //Act
            var result = await repository.GetAllByLocationAsync(locationID);

            //Assert
            Assert.Null(result);
        }

        #endregion GetAllBYLocationAsync() TESTS

        #region GetAllLikeNameAsync() TESTS

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenValidName_OffsetValue_ResultsCount_ToGetAllLikeNameAsyncMethod_ShouldReturnACollectionWithTwoElements()
        {
            //Arrange
            string name = "University";
            int offsetValue = 0;
            int resultsCount = 10;

            //Act
            var result = await repository.GetAllLikeNameAsync(name, offsetValue, resultsCount);

            //Assert
            Assert.Equal(2, result.Count);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenValidName_OffsetValue_ResultsCount_ToGetAllLikeNameAsyncMethod_ShouldContainAnEntityWithName_UniversityofTesting()
        {
            //Arrange
            string name = "University";
            int offsetValue = 0;
            int resultsCount = 10;

            //Act
            var result = await repository.GetAllLikeNameAsync(name, offsetValue, resultsCount);

            //Assert
            Assert.Contains(result, ei => ei.Name.Equals("University of Testing"));
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenValidName_OffsetValue_ResultsCount_ToGetAllLikeNameAsyncMethod_ShouldContainAnEntityWitDescription_Testdata()
        {
            //Arrange
            string name = "University";
            int offsetValue = 0;
            int resultsCount = 10;

            //Act
            var result = await repository.GetAllLikeNameAsync(name, offsetValue, resultsCount);

            //Assert
            Assert.Contains(result, ei => ei.Description.Equals("Test data"));
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenValidName_OffsetValue_ResultsCount_ToGetAllLikeNameAsyncMethod_ShouldContainAnEntityWithName_UniversityofTestingOne()
        {
            //Arrange
            string name = "University";
            int offsetValue = 0;
            int resultsCount = 10;

            //Act
            var result = await repository.GetAllLikeNameAsync(name, offsetValue, resultsCount);

            //Assert
            Assert.Contains(result, ei => ei.Name.Equals("University of Testing One"));
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenValidName_OffsetValue_ResultsCount_ToGetAllLikeNameAsyncMethod_ShouldContainAnEntityWitDescription_TestMoreData()
        {
            //Arrange
            string name = "University";
            int offsetValue = 0;
            int resultsCount = 10;

            //Act
            var result = await repository.GetAllLikeNameAsync(name, offsetValue, resultsCount);

            //Assert
            Assert.Contains(result, ei => ei.Description.Equals("Test More Data"));
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenValidName_OffsetValue_WithResultsCount1_ToGetAllLikeNameAsyncMethod_ShouldReturnACollectionWithOneElement()
        {
            //Arrange
            string name = "University";
            int offsetValue = 0;
            int resultsCount = 1;

            //Act
            var result = await repository.GetAllLikeNameAsync(name, offsetValue, resultsCount);

            //Assert
            Assert.Equal(1, result.Count);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenValidName_OffsetValue_WithResultsCount1_ToGetAllLikeNameAsyncMethod_ShouldReturnAnEntityWithName_UniversityofTesting()
        {
            //Arrange
            string name = "University";
            int offsetValue = 0;
            int resultsCount = 1;

            //Act
            var result = await repository.GetAllLikeNameAsync(name, offsetValue, resultsCount);

            //Assert
            Assert.Equal("University of Testing", result.ElementAt(0).Name);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenValidName_ResultsCount_WithOffsetValue1_ToGetAllLikeNameAsyncMethod_ShouldReturnACollectionWithOneElement()
        {
            //Arrange
            string name = "University";
            int offsetValue = 1;
            int resultsCount = 10;

            //Act
            var result = await repository.GetAllLikeNameAsync(name, offsetValue, resultsCount);

            //Assert
            Assert.Equal(1, result.Count);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenValidName_ResultsCount_WithOffsetValue1_ToGetAllLikeNameAsyncMethod_ShouldReturnAnEntityWithName_UniversityofTestingOne()
        {
            //Arrange
            string name = "University";
            int offsetValue = 1;
            int resultsCount = 10;

            //Act
            var result = await repository.GetAllLikeNameAsync(name, offsetValue, resultsCount);

            //Assert
            Assert.Equal("University of Testing One", result.ElementAt(0).Name);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenValidName_ResultsCount_WithOffsetValueGreaterThanTotalQueryResults_ToGetAllLikeNameAsyncMethod_ShouldReturnAnEmptyCollection()
        {
            //Arrange
            string name = "University";
            int offsetValue = 3;
            int resultsCount = 10;

            //Act
            var result = await repository.GetAllLikeNameAsync(name, offsetValue, resultsCount);

            //Assert
            Assert.Empty(result);
        }

        [IgnoreWhenDatabaseIsNotLoaded]
        public async Task GivenAnInvalidName_ThatDoesntExistInDatabase_OffsetValue_ResultsCount_ToGetAllLikeNameAsyncMethod_ShouldReturnAnEmptyCollection()
        {
            //Arrange
            string name = "InvalidName";
            int offsetValue = 0;
            int resultsCount = 10;

            //Act
            var result = await repository.GetAllLikeNameAsync(name, offsetValue, resultsCount);

            //Assert
            Assert.Empty(result);
        }

        #endregion GetAllLikeNameAsync() TESTS
    }
}