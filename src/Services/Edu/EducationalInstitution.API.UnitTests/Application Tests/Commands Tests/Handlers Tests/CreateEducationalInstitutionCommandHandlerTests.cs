﻿using EducationalInstitution.API.Tests.Shared;
using EducationalInstitution.Application;
using EducationalInstitution.Application.Commands;
using EducationalInstitution.Application.Commands.Handlers;
using EducationalInstitution.Application.Commands.Results;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Domain = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.API.UnitTests.Application_Tests.Commands_Tests.Handlers_Tests
{
    public class CreateEducationalInstitutionCommandHandlerTests : IClassFixture<MockDependenciesHelper<CreateEducationalInstitutionCommandHandler>>,
                                                                   IClassFixture<TestDataFromJSONParser>
    {
        private readonly TestDataFromJSONParser testDataHelper;
        private readonly MockDependenciesHelper<CreateEducationalInstitutionCommandHandler> dependenciesHelper;

        /// <remarks>Called before each test</remarks>
        public CreateEducationalInstitutionCommandHandlerTests(MockDependenciesHelper<CreateEducationalInstitutionCommandHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
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
            Guid parentInstitutionID = testDataHelper.EducationalInstitutions[0].Id;

            CreateEducationalInstitutionCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            SetupMockedDependencies(testDataHelper.EducationalInstitutions[0]);

            var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                         dependenciesHelper.mockEventBus.Object,
                                                                         dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

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
            Guid parentInstitutionID = testDataHelper.EducationalInstitutions[0].Id;

            CreateEducationalInstitutionCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            SetupMockedDependencies(testDataHelper.EducationalInstitutions[0]);

            var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                         dependenciesHelper.mockEventBus.Object,
                                                                         dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

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
            Guid parentInstitutionID = testDataHelper.EducationalInstitutions[0].Id;

            CreateEducationalInstitutionCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            SetupMockedDependencies(testDataHelper.EducationalInstitutions[0]);

            var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                         dependenciesHelper.mockEventBus.Object,
                                                                         dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.IsType<Response<CreateEducationalInstitutionCommandResult>>(result);
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
            Guid parentInstitutionID = testDataHelper.EducationalInstitutions[0].Id;

            CreateEducationalInstitutionCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            SetupMockedDependencies(testDataHelper.EducationalInstitutions[0]);

            var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                         dependenciesHelper.mockEventBus.Object,
                                                                         dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

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
            Guid parentInstitutionID = testDataHelper.EducationalInstitutions[0].Id;

            CreateEducationalInstitutionCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            SetupMockedDependencies(testDataHelper.EducationalInstitutions[0]);

            var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                         dependenciesHelper.mockEventBus.Object,
                                                                         dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.IsType<CreateEducationalInstitutionCommandResult>(result.Data);
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
            Guid parentInstitutionID = testDataHelper.EducationalInstitutions[0].Id;

            CreateEducationalInstitutionCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            SetupMockedDependencies(testDataHelper.EducationalInstitutions[0]);

            var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                         dependenciesHelper.mockEventBus.Object,
                                                                         dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

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
            Guid parentInstitutionID = testDataHelper.EducationalInstitutions[1].Id;

            CreateEducationalInstitutionCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            SetupMockedDependencies(null);

            var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                         dependenciesHelper.mockEventBus.Object,
                                                                         dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

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
            Guid parentInstitutionID = testDataHelper.EducationalInstitutions[1].Id;

            CreateEducationalInstitutionCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            SetupMockedDependencies(null);

            var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                         dependenciesHelper.mockEventBus.Object,
                                                                         dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

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
            Guid parentInstitutionID = testDataHelper.EducationalInstitutions[1].Id;

            CreateEducationalInstitutionCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            SetupMockedDependencies(null);

            var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                         dependenciesHelper.mockEventBus.Object,
                                                                         dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

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
            Guid parentInstitutionID = testDataHelper.EducationalInstitutions[1].Id;

            CreateEducationalInstitutionCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            SetupMockedDependencies(null);

            var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                         dependenciesHelper.mockEventBus.Object,
                                                                         dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal($"The Educational Institution has been successfully created but the Parent Institution with the following ID: {parentInstitutionID} has not been found!", result.Message);
        }

        private void SetupMockedDependencies(Domain::EducationalInstitution expectedEntityReturned)
        {
            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(mr => mr.GetEducationalInstitutionIncludingAdminsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync(expectedEntityReturned);
            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(mr => mr.CreateAsync(It.IsAny<Domain::EducationalInstitution>(), It.IsAny<CancellationToken>()));
        }

        private void SetupMockedDependencies()
        {
            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(mr => mr.CreateAsync(It.IsAny<Domain::EducationalInstitution>(), It.IsAny<CancellationToken>()));
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

            CreateEducationalInstitutionCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            SetupMockedDependencies();

            var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                         dependenciesHelper.mockEventBus.Object,
                                                                         dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

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

            CreateEducationalInstitutionCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            SetupMockedDependencies();

            var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                         dependenciesHelper.mockEventBus.Object,
                                                                         dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

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

            CreateEducationalInstitutionCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            SetupMockedDependencies();
            var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                         dependenciesHelper.mockEventBus.Object,
                                                                         dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

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

            CreateEducationalInstitutionCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = adminsIDs
            };

            SetupMockedDependencies();

            var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                         dependenciesHelper.mockEventBus.Object,
                                                                         dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.IsType<Guid>(result.Data.EducationalInstitutionID);
        }

        [Fact]
        public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            CreateEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                    dependenciesHelper.mockEventBus.Object,
                                                                    dependenciesHelper.mockLogger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null));
        }

        [Fact]
        public void GivenANullArgumentUnitOfWorkForCommandsToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new CreateEducationalInstitutionCommandHandler(null,
                                                                                                        dependenciesHelper.mockEventBus.Object,
                                                                                                        dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                                                        dependenciesHelper.mockEventBus.Object,
                                                                                                        null));
        }

        [Fact]
        public void GivenANullArgumentEventBusToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                                                        null,
                                                                                                        dependenciesHelper.mockLogger.Object));
        }
    }
}