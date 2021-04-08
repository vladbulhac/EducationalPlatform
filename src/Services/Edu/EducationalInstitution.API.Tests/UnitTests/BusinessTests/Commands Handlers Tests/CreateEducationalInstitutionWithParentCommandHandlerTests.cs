﻿using EducationaInstitutionAPI.Business.Commands_Handlers.EducationalInstitution_Commands;
using EducationaInstitutionAPI.Data;
using EducationaInstitutionAPI.Data.Helpers.Queries_and_Commands_Results.Commands_Results;
using EducationaInstitutionAPI.DTOs.Commands;
using EducationaInstitutionAPI.Utils;
using EducationalInstitution.API.Tests.UnitTests.BusinessTests.QueriesTests.EducationalInstitutionTests;
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

        public CreateEducationalInstitutionWithParentCommandHandlerTests(MockDependenciesHelper<CreateEducationalInstitutionWithParentCommandHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
        }

        [Fact]
        public async Task GivenAName_Description_LocationID_BuildingID_ParentInstitutionID__ShouldReturnAResponseThatIncludesAStatusCodeCreatedField()
        {
            //Arrange
            string name = "West High School";
            string description = "School";
            string locationID = "location1235";
            var buildingsIDs = new List<string>() { "building1235" };
            Guid parentInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            DTOEducationalInstitutionWithParentCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID
            };

            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(parentInstitutionID, dependenciesHelper.cancellationToken)).ReturnsAsync(testDataHelper.EduInstitutions[0]);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EduInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionWithParentCommandHandler(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

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
            Guid parentInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            DTOEducationalInstitutionWithParentCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID
            };

            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(parentInstitutionID, dependenciesHelper.cancellationToken)).ReturnsAsync(testDataHelper.EduInstitutions[0]);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EduInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionWithParentCommandHandler(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

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
            Guid parentInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            DTOEducationalInstitutionWithParentCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID
            };

            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(parentInstitutionID, dependenciesHelper.cancellationToken)).ReturnsAsync(testDataHelper.EduInstitutions[0]);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EduInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionWithParentCommandHandler(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

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
            Guid parentInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            DTOEducationalInstitutionWithParentCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID
            };

            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(parentInstitutionID, dependenciesHelper.cancellationToken)).ReturnsAsync(testDataHelper.EduInstitutions[0]);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EduInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionWithParentCommandHandler(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

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
            Guid parentInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            DTOEducationalInstitutionWithParentCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID
            };

            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(parentInstitutionID, dependenciesHelper.cancellationToken)).ReturnsAsync(testDataHelper.EduInstitutions[0]);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EduInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionWithParentCommandHandler(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

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
            Guid parentInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            DTOEducationalInstitutionWithParentCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID
            };

            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(parentInstitutionID, dependenciesHelper.cancellationToken)).ReturnsAsync(testDataHelper.EduInstitutions[0]);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EduInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionWithParentCommandHandler(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

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
            Guid parentInstitutionID = testDataHelper.EduInstitutions[1].EduInstitutionID;

            DTOEducationalInstitutionWithParentCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID
            };

            EduInstitution repositoryTaskResult = null;
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(It.IsNotIn(testDataHelper.EduInstitutions[0].EduInstitutionID), dependenciesHelper.cancellationToken)).ReturnsAsync(repositoryTaskResult);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EduInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionWithParentCommandHandler(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

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
            Guid parentInstitutionID = testDataHelper.EduInstitutions[1].EduInstitutionID;

            DTOEducationalInstitutionWithParentCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID
            };

            EduInstitution repositoryTaskResult = null;
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(It.IsNotIn(testDataHelper.EduInstitutions[0].EduInstitutionID), dependenciesHelper.cancellationToken)).ReturnsAsync(repositoryTaskResult);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EduInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionWithParentCommandHandler(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

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
            Guid parentInstitutionID = testDataHelper.EduInstitutions[1].EduInstitutionID;

            DTOEducationalInstitutionWithParentCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID
            };

            EduInstitution repositoryTaskResult = null;
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(It.IsNotIn(testDataHelper.EduInstitutions[0].EduInstitutionID), dependenciesHelper.cancellationToken)).ReturnsAsync(repositoryTaskResult);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EduInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionWithParentCommandHandler(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

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
            Guid parentInstitutionID = testDataHelper.EduInstitutions[1].EduInstitutionID;

            DTOEducationalInstitutionWithParentCreateCommand request = new()
            {
                Name = name,
                Description = description,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs,
                ParentInstitutionID = parentInstitutionID
            };

            EduInstitution repositoryTaskResult = null;
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(It.IsNotIn(testDataHelper.EduInstitutions[0].EduInstitutionID), dependenciesHelper.cancellationToken)).ReturnsAsync(repositoryTaskResult);
            dependenciesHelper.mockRepository.Setup(mr => mr.CreateAsync(It.IsAny<EduInstitution>(), dependenciesHelper.cancellationToken));

            var handler = new CreateEducationalInstitutionWithParentCommandHandler(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal($"The Educational Institution has been successfully created but the Parent Institution with the following ID: {parentInstitutionID} has not been found!", result.Message);
        }

        [Fact]
        public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            CreateEducationalInstitutionWithParentCommandHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

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
            Assert.Throws<ArgumentNullException>(() => new CreateEducationalInstitutionWithParentCommandHandler(dependenciesHelper.mockRepository.Object, null));
        }
    }
}