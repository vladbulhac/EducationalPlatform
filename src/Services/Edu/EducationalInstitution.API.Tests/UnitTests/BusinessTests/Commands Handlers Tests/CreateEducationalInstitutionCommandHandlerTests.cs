using EducationaInstitutionAPI.Business.Commands_Handlers.EducationalInstitution_Commands;
using EducationaInstitutionAPI.Data;
using EducationaInstitutionAPI.Data.Helpers.Queries_and_Commands_Results.Commands_Results;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Commands;
using EducationaInstitutionAPI.Unit_of_Work;
using EducationaInstitutionAPI.Utils;
using EducationalInstitution.API.Tests.UnitTests.BusinessTests.QueriesTests.EducationalInstitutionTests;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.Tests.UnitTests.BusinessTests.Commands_Handlers_Tests.EducationalInstitution_Tests
{
    public class CreateEducationalInstitutionCommandHandlerTests : IClassFixture<MockDependenciesHelper<CreateEducationalInstitutionCommandHandler>>
    {
        private readonly MockDependenciesHelper<CreateEducationalInstitutionCommandHandler> dependenciesHelper;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;

        public CreateEducationalInstitutionCommandHandlerTests(MockDependenciesHelper<CreateEducationalInstitutionCommandHandler> dependenciesHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
            mockUnitOfWork = new();
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ShouldReturnAResponseThatIncludesAStatusCodeCreatedField()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs
            };

            EducationaInstitutionAPI.Data.EducationalInstitution newEduInstitution = new(request.Name, request.Description, request.LocationID, request.BuildingsIDs);
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(newEduInstitution, dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs
            };

            EducationaInstitutionAPI.Data.EducationalInstitution newEduInstitution = new(request.Name, request.Description, request.LocationID, request.BuildingsIDs);
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(newEduInstitution, dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ShouldReturnARecordTypeResponse()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs
            };

            EducationaInstitutionAPI.Data.EducationalInstitution newEduInstitution = new(request.Name, request.Description, request.LocationID, request.BuildingsIDs);
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(newEduInstitution, dependenciesHelper.cancellationToken));

            CreateEducationalInstitutionCommandHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<Response<EducationalInstitutionCommandResult>>(result);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ShouldReturnAResponseThatIncludesAResponseObjectWithTheEducationalInstitutionID()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs
            };

            EducationaInstitutionAPI.Data.EducationalInstitution newEduInstitution = new(request.Name, request.Description, request.LocationID, request.BuildingsIDs);
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(newEduInstitution, dependenciesHelper.cancellationToken));

            CreateEducationalInstitutionCommandHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<Guid>(result.ResponseObject.EduInstitutionID);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ShouldReturnAResponseThatIncludesAResponseObjectOfTypeCreateEducationalInstitutionCommandResult()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs
            };

            EducationaInstitutionAPI.Data.EducationalInstitution newEduInstitution = new(request.Name, request.Description, request.LocationID, request.BuildingsIDs);
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(newEduInstitution, dependenciesHelper.cancellationToken));

            CreateEducationalInstitutionCommandHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<EducationalInstitutionCommandResult>(result.ResponseObject);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs
            };

            EducationaInstitutionAPI.Data.EducationalInstitution newEduInstitution = new(request.Name, request.Description, request.LocationID, request.BuildingsIDs);
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(newEduInstitution, dependenciesHelper.cancellationToken));

            CreateEducationalInstitutionCommandHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            CreateEducationalInstitutionCommandHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

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
            Assert.Throws<ArgumentNullException>(() => new CreateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, null));
        }
    }
}