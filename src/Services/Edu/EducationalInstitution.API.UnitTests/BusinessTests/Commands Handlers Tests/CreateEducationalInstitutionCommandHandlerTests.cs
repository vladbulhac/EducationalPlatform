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

namespace EducationalInstitution.API.UnitTests.BusinessTests.Commands_Handlers_Tests
{
    public class CreateEducationalInstitutionCommandHandlerTests : IClassFixture<MockDependenciesHelper<CreateEducationalInstitutionCommandHandler>>, IClassFixture<TestDataFromJSONParser>
    {
        private readonly MockDependenciesHelper<CreateEducationalInstitutionCommandHandler> dependenciesHelper;
        private readonly TestDataFromJSONParser testDataHelper;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;

        /// <remarks>Called before each test</remarks>
        public CreateEducationalInstitutionCommandHandlerTests(MockDependenciesHelper<CreateEducationalInstitutionCommandHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
            mockUnitOfWork = new();
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ParentInstitutionID_AdminID_ShouldReturnAResponseThatIncludesAStatusCodeCreatedField()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };
            Guid parentInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(parentInstitutionID, dependenciesHelper.cancellationToken)).ReturnsAsync(testDataHelper.EducationalInstitutions[0]);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EducationalInstitutionAPI.Data.EducationalInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ParentInstitutionID_AdminID_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };
            Guid parentInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(parentInstitutionID, dependenciesHelper.cancellationToken)).ReturnsAsync(testDataHelper.EducationalInstitutions[0]);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EducationalInstitutionAPI.Data.EducationalInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ParentInstitutionID_AdminID_ShouldReturnARecordTypeResponse()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };
            Guid parentInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(parentInstitutionID, dependenciesHelper.cancellationToken)).ReturnsAsync(testDataHelper.EducationalInstitutions[0]);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EducationalInstitutionAPI.Data.EducationalInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<Response<EducationalInstitutionCommandResult>>(result);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ParentInstitutionID_AdminID_ShouldReturnAResponseThatIncludesDataWithTheEducationalInstitutionID()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };
            Guid parentInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(parentInstitutionID, dependenciesHelper.cancellationToken)).ReturnsAsync(testDataHelper.EducationalInstitutions[0]);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EducationalInstitutionAPI.Data.EducationalInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<Guid>(result.Data.EducationalInstitutionID);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ParentInstitutionID_AdminID_ShouldReturnAResponseThatIncludesDataOfTypeCreateEducationalInstitutionCommandResult()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };
            Guid parentInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(parentInstitutionID, dependenciesHelper.cancellationToken)).ReturnsAsync(testDataHelper.EducationalInstitutions[0]);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EducationalInstitutionAPI.Data.EducationalInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<EducationalInstitutionCommandResult>(result.Data);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ParentInstitutionID_AdminID_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };
            Guid parentInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(parentInstitutionID, dependenciesHelper.cancellationToken)).ReturnsAsync(testDataHelper.EducationalInstitutions[0]);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EducationalInstitutionAPI.Data.EducationalInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_AdminID_InvalidParentInstitutionID_ShouldReturnAResponseThatIncludesAStatusCodeMultiStatusField()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };
            Guid parentInstitutionID = testDataHelper.EducationalInstitutions[1].EducationalInstitutionID;

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            EducationalInstitutionAPI.Data.EducationalInstitution repositoryTaskResult = null;
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(It.IsNotIn(testDataHelper.EducationalInstitutions[0].EducationalInstitutionID), dependenciesHelper.cancellationToken)).ReturnsAsync(repositoryTaskResult);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EducationalInstitutionAPI.Data.EducationalInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(HttpStatusCode.MultiStatus, result.StatusCode);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_AdminID_InvalidParentInstitutionID_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };
            Guid parentInstitutionID = testDataHelper.EducationalInstitutions[1].EducationalInstitutionID;

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            EducationalInstitutionAPI.Data.EducationalInstitution repositoryTaskResult = null;
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(It.IsNotIn(testDataHelper.EducationalInstitutions[0].EducationalInstitutionID), dependenciesHelper.cancellationToken)).ReturnsAsync(repositoryTaskResult);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EducationalInstitutionAPI.Data.EducationalInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_AdminID_InvalidParentInstitutionID_ShouldReturnAResponseThatIncludesDataWithTheEducationalInstitutionID()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };
            Guid parentInstitutionID = testDataHelper.EducationalInstitutions[1].EducationalInstitutionID;

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            EducationalInstitutionAPI.Data.EducationalInstitution repositoryTaskResult = null;
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(It.IsNotIn(testDataHelper.EducationalInstitutions[0].EducationalInstitutionID), dependenciesHelper.cancellationToken)).ReturnsAsync(repositoryTaskResult);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EducationalInstitutionAPI.Data.EducationalInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<Guid>(result.Data.EducationalInstitutionID);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_AdminID_InvalidParentInstitutionID_ShouldReturnAResponseThatIncludesAMessageField()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };
            Guid parentInstitutionID = testDataHelper.EducationalInstitutions[1].EducationalInstitutionID;

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            EducationalInstitutionAPI.Data.EducationalInstitution repositoryTaskResult = null;
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(It.IsNotIn(testDataHelper.EducationalInstitutions[0].EducationalInstitutionID), dependenciesHelper.cancellationToken)).ReturnsAsync(repositoryTaskResult);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EducationalInstitutionAPI.Data.EducationalInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal($"The Educational Institution has been successfully created but the Parent Institution with the following ID: {parentInstitutionID} has not been found!", result.Message);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_AdminID_WithDefaultParentInstitutionID__ShouldReturnAResponseThatIncludesAStatusCodeCreatedField()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };
            Guid parentInstitutionID = Guid.Empty;

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EducationalInstitutionAPI.Data.EducationalInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID__AdminID_WithDefaultParentInstitutionID_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };
            Guid parentInstitutionID = Guid.Empty;

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EducationalInstitutionAPI.Data.EducationalInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_AdminID_WithDefaultParentInstitutionID_ShouldReturnAResponseThatIncludesATrueOperationStatusField()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };
            Guid parentInstitutionID = Guid.Empty;

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EducationalInstitutionAPI.Data.EducationalInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_AdminID_WithDefaultParentInstitutionID_ShouldReturnAResponseThatIncludesAGuidIDField()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };
            Guid parentInstitutionID = Guid.Empty;

            DTOEducationalInstitutionCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EducationalInstitutionAPI.Data.EducationalInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<Guid>(result.Data.EducationalInstitutionID);
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