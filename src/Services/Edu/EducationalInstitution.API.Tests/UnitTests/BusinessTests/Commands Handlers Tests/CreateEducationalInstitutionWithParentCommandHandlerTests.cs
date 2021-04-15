using EducationalInstitutionAPI.Business.Commands_Handlers;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Commands_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Unit_of_Work;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.Tests.UnitTests.BusinessTests.Commands_Handlers_Tests
{
    public class CreateEducationalInstitutionWithParentCommandHandlerTests : IClassFixture<MockDependenciesHelper<CreateEducationalInstitutionWithParentCommandHandler>>, IClassFixture<TestDataFromJSONParser>
    {
        private readonly MockDependenciesHelper<CreateEducationalInstitutionWithParentCommandHandler> dependenciesHelper;
        private readonly TestDataFromJSONParser testDataHelper;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;

        public CreateEducationalInstitutionWithParentCommandHandlerTests(MockDependenciesHelper<CreateEducationalInstitutionWithParentCommandHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
            mockUnitOfWork = new();
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ParentInstitutionID__ShouldReturnAResponseThatIncludesAStatusCodeCreatedField()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            Guid parentInstitutionID = testDataHelper.EduInstitutions[0].EducationalInstitutionID;

            DTOEducationalInstitutionWithParentCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(parentInstitutionID, dependenciesHelper.cancellationToken)).ReturnsAsync(testDataHelper.EduInstitutions[0]);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EducationalInstitutionAPI.Data.EducationalInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionWithParentCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ParentInstitutionID_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            Guid parentInstitutionID = testDataHelper.EduInstitutions[0].EducationalInstitutionID;

            DTOEducationalInstitutionWithParentCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(parentInstitutionID, dependenciesHelper.cancellationToken)).ReturnsAsync(testDataHelper.EduInstitutions[0]);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EducationalInstitutionAPI.Data.EducationalInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionWithParentCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ParentInstitutionID_ShouldReturnARecordTypeResponse()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            Guid parentInstitutionID = testDataHelper.EduInstitutions[0].EducationalInstitutionID;

            DTOEducationalInstitutionWithParentCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(parentInstitutionID, dependenciesHelper.cancellationToken)).ReturnsAsync(testDataHelper.EduInstitutions[0]);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EducationalInstitutionAPI.Data.EducationalInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionWithParentCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<Response<EducationalInstitutionCommandResult>>(result);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ParentInstitutionID_ShouldReturnAResponseThatIncludesAResponseObjectWithTheEducationalInstitutionID()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            Guid parentInstitutionID = testDataHelper.EduInstitutions[0].EducationalInstitutionID;

            DTOEducationalInstitutionWithParentCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(parentInstitutionID, dependenciesHelper.cancellationToken)).ReturnsAsync(testDataHelper.EduInstitutions[0]);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EducationalInstitutionAPI.Data.EducationalInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionWithParentCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<Guid>(result.ResponseObject.EduInstitutionID);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ParentInstitutionID_ShouldReturnAResponseThatIncludesAResponseObjectOfTypeCreateEducationalInstitutionCommandResult()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            Guid parentInstitutionID = testDataHelper.EduInstitutions[0].EducationalInstitutionID;

            DTOEducationalInstitutionWithParentCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(parentInstitutionID, dependenciesHelper.cancellationToken)).ReturnsAsync(testDataHelper.EduInstitutions[0]);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EducationalInstitutionAPI.Data.EducationalInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionWithParentCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<EducationalInstitutionCommandResult>(result.ResponseObject);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ParentInstitutionID_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            Guid parentInstitutionID = testDataHelper.EduInstitutions[0].EducationalInstitutionID;

            DTOEducationalInstitutionWithParentCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(parentInstitutionID, dependenciesHelper.cancellationToken)).ReturnsAsync(testDataHelper.EduInstitutions[0]);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EducationalInstitutionAPI.Data.EducationalInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionWithParentCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_InvalidParentInstitutionID_ShouldReturnAResponseThatIncludesAStatusCodeMultiStatusField()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            Guid parentInstitutionID = testDataHelper.EduInstitutions[1].EducationalInstitutionID;

            DTOEducationalInstitutionWithParentCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID
            };

            EducationalInstitutionAPI.Data.EducationalInstitution repositoryTaskResult = null;
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(It.IsNotIn(testDataHelper.EduInstitutions[0].EducationalInstitutionID), dependenciesHelper.cancellationToken)).ReturnsAsync(repositoryTaskResult);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EducationalInstitutionAPI.Data.EducationalInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionWithParentCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(HttpStatusCode.MultiStatus, result.StatusCode);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_InvalidParentInstitutionID_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            Guid parentInstitutionID = testDataHelper.EduInstitutions[1].EducationalInstitutionID;

            DTOEducationalInstitutionWithParentCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID
            };

            EducationalInstitutionAPI.Data.EducationalInstitution repositoryTaskResult = null;
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(It.IsNotIn(testDataHelper.EduInstitutions[0].EducationalInstitutionID), dependenciesHelper.cancellationToken)).ReturnsAsync(repositoryTaskResult);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EducationalInstitutionAPI.Data.EducationalInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionWithParentCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_InvalidParentInstitutionID_ShouldReturnAResponseThatIncludesAResponseObjectWithTheEducationalInstitutionID()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            Guid parentInstitutionID = testDataHelper.EduInstitutions[1].EducationalInstitutionID;

            DTOEducationalInstitutionWithParentCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID
            };

            EducationalInstitutionAPI.Data.EducationalInstitution repositoryTaskResult = null;
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(It.IsNotIn(testDataHelper.EduInstitutions[0].EducationalInstitutionID), dependenciesHelper.cancellationToken)).ReturnsAsync(repositoryTaskResult);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EducationalInstitutionAPI.Data.EducationalInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionWithParentCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<Guid>(result.ResponseObject.EduInstitutionID);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_InvalidParentInstitutionID_ShouldReturnAResponseThatIncludesAMessageField()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            Guid parentInstitutionID = testDataHelper.EduInstitutions[1].EducationalInstitutionID;

            DTOEducationalInstitutionWithParentCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID
            };

            EducationalInstitutionAPI.Data.EducationalInstitution repositoryTaskResult = null;
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(It.IsNotIn(testDataHelper.EduInstitutions[0].EducationalInstitutionID), dependenciesHelper.cancellationToken)).ReturnsAsync(repositoryTaskResult);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EducationalInstitutionAPI.Data.EducationalInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionWithParentCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal($"The Educational Institution has been successfully created but the Parent Institution with the following ID: {parentInstitutionID} has not been found!", result.Message);
        }

        [Fact]
        public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            CreateEducationalInstitutionWithParentCommandHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, dependenciesHelper.cancellationToken));
        }

        [Fact]
        public void GivenANullArgumentRepositoryToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new CreateEducationalInstitutionWithParentCommandHandler(null, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new CreateEducationalInstitutionWithParentCommandHandler(mockUnitOfWork.Object, null));
        }
    }
}