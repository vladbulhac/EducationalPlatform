using EducationaInstitutionAPI.Business.Commands_Handlers.EducationalInstitution_Commands;
using EducationaInstitutionAPI.Data;
using EducationaInstitutionAPI.Data.Helpers.Queries_and_Commands_Results.Commands_Results;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Commands;
using EducationaInstitutionAPI.Utils;
using EducationalInstitution.API.Tests.UnitTests.BusinessTests.QueriesTests.EducationalInstitutionTests;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.Tests.UnitTests.BusinessTests.Commands_Handlers_Tests.EducationalInstitution_Tests
{
    public class CreateEducationalInstitutionCommandHandlerTests : IClassFixture<MockDependenciesHelper<CreateEducationalInstitutionCommandHandler>>
    {
        private readonly MockDependenciesHelper<CreateEducationalInstitutionCommandHandler> dependenciesHelper;

        public CreateEducationalInstitutionCommandHandlerTests(MockDependenciesHelper<CreateEducationalInstitutionCommandHandler> dependenciesHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
        }

        #region Input: Name = string, Description = string, LocationID = string, BuildingID = string | Expect: Result's StatusCode field to be equal HTTPStatusCode.Created

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ShouldReturnAResponseThatIncludesAStatusCodeCreatedField()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            string buildingID = "building1235";

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingID = buildingID
            };

            EduInstitution newEduInstitution = new(request.Name, request.Description, request.LocationID, request.BuildingID);
            dependenciesHelper.mockRepository.Setup(mr => mr.Create(newEduInstitution, dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionCommandHandler( dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        #endregion Input: Name = string, Description = string, LocationID = string, BuildingID = string | Expect: Result's StatusCode field to be equal HTTPStatusCode.Created

        #region Input: Name = string, Description = string, LocationID = string, BuildingID = string | Expect: Result's Message field to be empty

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            string buildingID = "building1235";

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingID = buildingID
            };

            EduInstitution newEduInstitution = new(request.Name, request.Description, request.LocationID, request.BuildingID);
            dependenciesHelper.mockRepository.Setup(mr => mr.Create(newEduInstitution, dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionCommandHandler( dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Empty(result.Message);
        }

        #endregion Input: Name = string, Description = string, LocationID = string, BuildingID = string | Expect: Result's Message field to be empty

        #region Input: Name = string, Description = string, LocationID = string, BuildingID = string | Expect: Result to be of type record Response<CreateEducationalInstitutionCommandResult>

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ShouldReturnARecordTypeResponse()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            string buildingID = "building1235";

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingID = buildingID
            };

            EduInstitution newEduInstitution = new(request.Name, request.Description, request.LocationID, request.BuildingID);
            dependenciesHelper.mockRepository.Setup(mr => mr.Create(newEduInstitution, dependenciesHelper.cancellationToken));

            CreateEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<Response<EducationalInstitutionCommandResult>>(result);
        }

        #endregion Input: Name = string, Description = string, LocationID = string, BuildingID = string | Expect: Result to be of type record Response<CreateEducationalInstitutionCommandResult>

        #region Input: Name = string, Description = string, LocationID = string, BuildingID = string | Expect: Result's ResponseObject to have a Guid ID

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ShouldReturnAResponseThatIncludesAResponseObjectWithTheEducationalInstitutionID()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            string buildingID = "building1235";

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingID = buildingID
            };

            EduInstitution newEduInstitution = new(request.Name, request.Description, request.LocationID, request.BuildingID);
            dependenciesHelper.mockRepository.Setup(mr => mr.Create(newEduInstitution, dependenciesHelper.cancellationToken));

            CreateEducationalInstitutionCommandHandler handler = new( dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<Guid>(result.ResponseObject.EduInstitutionID);
        }

        #endregion Input: Name = string, Description = string, LocationID = string, BuildingID = string | Expect: Result's ResponseObject to have a Guid ID

        #region Input: Name = string, Description = string, LocationID = string, BuildingID = string | Expect: Result's ResponseObject field to be of type CreateEducationalInstitutionCommandResult

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ShouldReturnAResponseThatIncludesAResponseObjectOfTypeCreateEducationalInstitutionCommandResult()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            string buildingID = "building1235";

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingID = buildingID
            };

            EduInstitution newEduInstitution = new(request.Name, request.Description, request.LocationID, request.BuildingID);
            dependenciesHelper.mockRepository.Setup(mr => mr.Create(newEduInstitution, dependenciesHelper.cancellationToken));

            CreateEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<EducationalInstitutionCommandResult>(result.ResponseObject);
        }

        #endregion Input: Name = string, Description = string, LocationID = string, BuildingID = string | Expect: Result's ResponseObject field to be of type CreateEducationalInstitutionCommandResult

        #region Input: Name = string, Description = string, LocationID = string, BuildingID = string | Expect: Result's OperationStatus field to be true

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            string buildingID = "building1235";

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingID = buildingID
            };

            EduInstitution newEduInstitution = new(request.Name, request.Description, request.LocationID, request.BuildingID);
            dependenciesHelper.mockRepository.Setup(mr => mr.Create(newEduInstitution, dependenciesHelper.cancellationToken));

            CreateEducationalInstitutionCommandHandler handler = new( dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.True(result.OperationStatus);
        }

        #endregion Input: Name = string, Description = string, LocationID = string, BuildingID = string | Expect: Result's OperationStatus field to be true

        #region When an object with the same field's value exists TESTS
        #endregion

        #region NULL arguments TESTS
        [Fact]
        public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            CreateEducationalInstitutionCommandHandler handler = new( dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, dependenciesHelper.cancellationToken));
        }

        [Fact]
        public void GivenANullArgumentRepositoryToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new CreateEducationalInstitutionCommandHandler(null, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockRepository.Object, null));
        }
        #endregion
    }
}