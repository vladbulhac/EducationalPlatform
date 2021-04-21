using EducationalInstitutionAPI.Business.Commands_Handlers;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Unit_of_Work;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.Tests.UnitTests.BusinessTests.Commands_Handlers_Tests
{
    public class UpdateEducationalInstitutionEntireLocationCommandHandlerTests : IClassFixture<MockDependenciesHelper<UpdateEducationalInstitutionLocationCommandHandler>>, IClassFixture<TestDataFromJSONParser>
    {
        private readonly MockDependenciesHelper<UpdateEducationalInstitutionLocationCommandHandler> dependenciesHelper;
        private readonly TestDataFromJSONParser testDataHelper;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;

        public UpdateEducationalInstitutionEntireLocationCommandHandlerTests(MockDependenciesHelper<UpdateEducationalInstitutionLocationCommandHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
            mockUnitOfWork = new();
        }

        [Fact]
        public async Task GivenAnEduInstitutionID_LocationID_BuildingsIDs_ShouldReturnAResponseThatIncludesAStatusCodeNoContentField()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).BuildingID };

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = eduInstitutionID,
                UpdateLocation = true,
                LocationID = locationID,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.UpdateEntireLocationAsync(eduInstitutionID, locationID, addBuildingsIDs, removeBuildingsIDs, dependenciesHelper.cancellationToken)).ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnEduInstitutionID_LocationID_BuildingsIDs_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).BuildingID };

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = eduInstitutionID,
                UpdateLocation = true,
                LocationID = locationID,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.UpdateEntireLocationAsync(eduInstitutionID, locationID, addBuildingsIDs, removeBuildingsIDs, dependenciesHelper.cancellationToken)).ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnEduInstitutionID_LocationID_BuildingsIDs_ShouldReturnANonGenericResponse()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).BuildingID };

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = eduInstitutionID,
                UpdateLocation = true,
                LocationID = locationID,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.UpdateEntireLocationAsync(eduInstitutionID, locationID, addBuildingsIDs, removeBuildingsIDs, dependenciesHelper.cancellationToken)).ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<Response>(result);
        }

        [Fact]
        public async Task GivenAnEduInstitutionID_LocationID_BuildingsIDs_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).BuildingID };

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = eduInstitutionID,
                UpdateLocation = true,
                LocationID = locationID,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.UpdateEntireLocationAsync(eduInstitutionID, locationID, addBuildingsIDs, removeBuildingsIDs, dependenciesHelper.cancellationToken)).ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnEduInstitutionID_LocationID_ShouldReturnAResponseThatIncludesAStatusCodeNoContentField()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = eduInstitutionID,
                UpdateLocation = true,
                LocationID = locationID,
                UpdateBuildings = false,
                AddBuildingsIDs = default,
                RemoveBuildingsIDs = default
            };
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.UpdateLocationAsync(eduInstitutionID, locationID, dependenciesHelper.cancellationToken)).ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnEduInstitutionID_LocationID_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = eduInstitutionID,
                UpdateLocation = true,
                LocationID = locationID,
                UpdateBuildings = false,
                AddBuildingsIDs = default,
                RemoveBuildingsIDs = default
            };
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.UpdateLocationAsync(eduInstitutionID, locationID, dependenciesHelper.cancellationToken)).ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnEduInstitutionID_LocationID_ShouldReturnANonGenericResponse()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = eduInstitutionID,
                UpdateLocation = true,
                LocationID = locationID,
                UpdateBuildings = false,
                AddBuildingsIDs = default,
                RemoveBuildingsIDs = default
            };
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.UpdateLocationAsync(eduInstitutionID, locationID, dependenciesHelper.cancellationToken)).ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<Response>(result);
        }

        [Fact]
        public async Task GivenAnEduInstitutionID_LocationID_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = eduInstitutionID,
                UpdateLocation = true,
                LocationID = locationID,
                UpdateBuildings = false,
                AddBuildingsIDs = default,
                RemoveBuildingsIDs = default
            };
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.UpdateLocationAsync(eduInstitutionID, locationID, dependenciesHelper.cancellationToken)).ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnEduInstitutionID_BuildingsIDs_ShouldReturnAResponseThatIncludesAStatusCodeNoContentField()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).BuildingID };

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = eduInstitutionID,
                UpdateLocation = false,
                LocationID = default,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.UpdateBuildingsAsync(eduInstitutionID, addBuildingsIDs, removeBuildingsIDs, dependenciesHelper.cancellationToken)).ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnEduInstitutionID_BuildingsIDs_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).BuildingID };

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = eduInstitutionID,
                UpdateLocation = false,
                LocationID = default,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.UpdateBuildingsAsync(eduInstitutionID, addBuildingsIDs, removeBuildingsIDs, dependenciesHelper.cancellationToken)).ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnEduInstitutionID_BuildingsIDs_ShouldReturnANonGenericResponse()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).BuildingID };

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = eduInstitutionID,
                UpdateLocation = false,
                LocationID = default,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.UpdateBuildingsAsync(eduInstitutionID, addBuildingsIDs, removeBuildingsIDs, dependenciesHelper.cancellationToken)).ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<Response>(result);
        }

        [Fact]
        public async Task GivenAnEduInstitutionID_BuildingsIDs_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).BuildingID };

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = eduInstitutionID,
                UpdateLocation = false,
                LocationID = default,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.UpdateBuildingsAsync(eduInstitutionID, addBuildingsIDs, removeBuildingsIDs, dependenciesHelper.cancellationToken)).ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            UpdateEducationalInstitutionLocationCommandHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, dependenciesHelper.cancellationToken));
        }

        [Fact]
        public void GivenANullArgumentRepositoryToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionLocationCommandHandler(null, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionLocationCommandHandler(mockUnitOfWork.Object, null));
        }
    }
}