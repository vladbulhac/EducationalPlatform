using EducationalInstitutionAPI.Business.Commands_Handlers;
using EducationalInstitutionAPI.Data.Repository_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Commands;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.UnitTests.BusinessTests.Commands_Handlers_Tests
{
    public class UpdateEducationalInstitutionEntireLocationCommandHandlerTests : IClassFixture<MockDependenciesHelper<UpdateEducationalInstitutionLocationCommandHandler>>,
                                                                                 IClassFixture<TestDataFromJSONParser>
    {
        private readonly TestDataFromJSONParser testDataHelper;
        private readonly MockDependenciesHelper<UpdateEducationalInstitutionLocationCommandHandler> dependenciesHelper;

        /// <remarks>Called before each test</remarks>
        public UpdateEducationalInstitutionEntireLocationCommandHandlerTests(MockDependenciesHelper<UpdateEducationalInstitutionLocationCommandHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_LocationID_BuildingsIDs_ShouldReturnAResponseThatIncludesAStatusCodeNoContentField()
        {
            //Arrange
            Guid EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).BuildingID };

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = EducationalInstitutionID,
                UpdateLocation = true,
                LocationID = locationID,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(mr => mr.UpdateEntireLocationAsync(EducationalInstitutionID, locationID, addBuildingsIDs, removeBuildingsIDs, It.IsAny<CancellationToken>()))
                                                                            .ReturnsAsync(new CommandRepositoryResult(new List<Guid>() { Guid.NewGuid() }));
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_LocationID_BuildingsIDs_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            Guid EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).BuildingID };

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = EducationalInstitutionID,
                UpdateLocation = true,
                LocationID = locationID,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(mr => mr.UpdateEntireLocationAsync(EducationalInstitutionID, locationID, addBuildingsIDs, removeBuildingsIDs, It.IsAny<CancellationToken>()))
                                                                            .ReturnsAsync(new CommandRepositoryResult(new List<Guid>() { Guid.NewGuid() }));
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_LocationID_BuildingsIDs_ShouldReturnANonGenericResponse()
        {
            //Arrange
            Guid EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).BuildingID };

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = EducationalInstitutionID,
                UpdateLocation = true,
                LocationID = locationID,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(mr => mr.UpdateEntireLocationAsync(EducationalInstitutionID, locationID, addBuildingsIDs, removeBuildingsIDs, It.IsAny<CancellationToken>()))
                                                                            .ReturnsAsync(new CommandRepositoryResult(new List<Guid>() { Guid.NewGuid() }));
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.IsType<Response>(result);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_LocationID_BuildingsIDs_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            Guid EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).BuildingID };

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = EducationalInstitutionID,
                UpdateLocation = true,
                LocationID = locationID,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(mr => mr.UpdateEntireLocationAsync(EducationalInstitutionID, locationID, addBuildingsIDs, removeBuildingsIDs, It.IsAny<CancellationToken>()))
                                                                            .ReturnsAsync(new CommandRepositoryResult(new List<Guid>() { Guid.NewGuid() }));
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_LocationID_ShouldReturnAResponseThatIncludesAStatusCodeNoContentField()
        {
            //Arrange
            Guid EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = EducationalInstitutionID,
                UpdateLocation = true,
                LocationID = locationID,
                UpdateBuildings = false,
                AddBuildingsIDs = default,
                RemoveBuildingsIDs = default
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(mr => mr.UpdateLocationAsync(EducationalInstitutionID, locationID, It.IsAny<CancellationToken>()))
                                                                            .ReturnsAsync(new CommandRepositoryResult(new List<Guid>() { Guid.NewGuid() }));
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_LocationID_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            Guid EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = EducationalInstitutionID,
                UpdateLocation = true,
                LocationID = locationID,
                UpdateBuildings = false,
                AddBuildingsIDs = default,
                RemoveBuildingsIDs = default
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(mr => mr.UpdateLocationAsync(EducationalInstitutionID, locationID, It.IsAny<CancellationToken>()))
                                                                            .ReturnsAsync(new CommandRepositoryResult(new List<Guid>() { Guid.NewGuid() }));
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_LocationID_ShouldReturnANonGenericResponse()
        {
            //Arrange
            Guid EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = EducationalInstitutionID,
                UpdateLocation = true,
                LocationID = locationID,
                UpdateBuildings = false,
                AddBuildingsIDs = default,
                RemoveBuildingsIDs = default
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(mr => mr.UpdateLocationAsync(EducationalInstitutionID, locationID, It.IsAny<CancellationToken>()))
                                                                             .ReturnsAsync(new CommandRepositoryResult(new List<Guid>() { Guid.NewGuid() }));
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.IsType<Response>(result);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_LocationID_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            Guid EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = EducationalInstitutionID,
                UpdateLocation = true,
                LocationID = locationID,
                UpdateBuildings = false,
                AddBuildingsIDs = default,
                RemoveBuildingsIDs = default
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(mr => mr.UpdateLocationAsync(EducationalInstitutionID, locationID, It.IsAny<CancellationToken>()))
                                                                            .ReturnsAsync(new CommandRepositoryResult(new List<Guid>() { Guid.NewGuid() }));
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_BuildingsIDs_ShouldReturnAResponseThatIncludesAStatusCodeNoContentField()
        {
            //Arrange
            Guid EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).BuildingID };

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = EducationalInstitutionID,
                UpdateLocation = false,
                LocationID = default,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(mr => mr.UpdateBuildingsAsync(EducationalInstitutionID, addBuildingsIDs, removeBuildingsIDs, It.IsAny<CancellationToken>()))
                                                                            .ReturnsAsync(new CommandRepositoryResult(new List<Guid>() { Guid.NewGuid() }));
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_BuildingsIDs_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            Guid EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).BuildingID };

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = EducationalInstitutionID,
                UpdateLocation = false,
                LocationID = default,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(mr => mr.UpdateBuildingsAsync(EducationalInstitutionID, addBuildingsIDs, removeBuildingsIDs, It.IsAny<CancellationToken>()))
                                                                           .ReturnsAsync(new CommandRepositoryResult(new List<Guid>() { Guid.NewGuid() }));
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_BuildingsIDs_ShouldReturnANonGenericResponse()
        {
            //Arrange
            Guid EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).BuildingID };

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = EducationalInstitutionID,
                UpdateLocation = false,
                LocationID = default,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(mr => mr.UpdateBuildingsAsync(EducationalInstitutionID, addBuildingsIDs, removeBuildingsIDs, It.IsAny<CancellationToken>()))
                                                                            .ReturnsAsync(new CommandRepositoryResult(new List<Guid>() { Guid.NewGuid() }));
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.IsType<Response>(result);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_BuildingsIDs_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            Guid EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).BuildingID };

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = EducationalInstitutionID,
                UpdateLocation = false,
                LocationID = default,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(mr => mr.UpdateBuildingsAsync(EducationalInstitutionID, addBuildingsIDs, removeBuildingsIDs, It.IsAny<CancellationToken>()))
                                                                           .ReturnsAsync(new CommandRepositoryResult(new List<Guid>() { Guid.NewGuid() }));
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_BuildingsIDs_IDDoesntExistInDatabase_ShouldReturnExpectedMessage()
        {
            //Arrange
            Guid EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).BuildingID };

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = EducationalInstitutionID,
                UpdateLocation = false,
                LocationID = default,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(mr => mr.UpdateBuildingsAsync(EducationalInstitutionID, addBuildingsIDs, removeBuildingsIDs, It.IsAny<CancellationToken>()))
                                                                           .ReturnsAsync((CommandRepositoryResult)default);
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal($"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!", result.Message);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_BuildingsIDs_IDDoesntExistInDatabase_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            Guid EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).BuildingID };

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = EducationalInstitutionID,
                UpdateLocation = false,
                LocationID = default,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(mr => mr.UpdateBuildingsAsync(EducationalInstitutionID, addBuildingsIDs, removeBuildingsIDs, It.IsAny<CancellationToken>()))
                                                                           .ReturnsAsync((CommandRepositoryResult)default);
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_BuildingsIDs_IDDoesntExistInDatabase_ShouldReturnStatusCodeNotFound()
        {
            //Arrange
            Guid EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).BuildingID };

            DTOEducationalInstitutionLocationUpdateCommand request = new()
            {
                EducationalInstitutionID = EducationalInstitutionID,
                UpdateLocation = false,
                LocationID = default,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(mr => mr.UpdateBuildingsAsync(EducationalInstitutionID, addBuildingsIDs, removeBuildingsIDs, It.IsAny<CancellationToken>()))
                                                                           .ReturnsAsync((CommandRepositoryResult)default);
            var handler = new UpdateEducationalInstitutionLocationCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            UpdateEducationalInstitutionLocationCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null));
        }

        [Fact]
        public void GivenANullArgumentRepositoryToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionLocationCommandHandler(null, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentEventBusToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionLocationCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, null, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionLocationCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, null));
        }
    }
}