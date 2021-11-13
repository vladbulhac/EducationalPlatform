using EducationalInstitution.API.Tests.Shared;
using EducationalInstitution.Application;
using EducationalInstitution.Application.Commands;
using EducationalInstitution.Application.Commands.Handlers;
using Moq;
using System.Net;
using Xunit;
using Domain = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.API.UnitTests.Application_Tests.Commands_Tests.Handlers_Tests;

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
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        string locationID = "10Fc4a7f1e00f1BDebAe4509";
        ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
        ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).Id };

        UpdateEducationalInstitutionLocationCommand request = new()
        {
            EducationalInstitutionID = educationalInstitutionID,
            UpdateLocation = true,
            LocationID = locationID,
            UpdateBuildings = true,
            AddBuildingsIDs = addBuildingsIDs,
            RemoveBuildingsIDs = removeBuildingsIDs
        };

        dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

        dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(meicr => meicr.GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                      .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

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
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        string locationID = "10Fc4a7f1e00f1BDebAe4509";
        ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
        ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).Id };

        UpdateEducationalInstitutionLocationCommand request = new()
        {
            EducationalInstitutionID = educationalInstitutionID,
            UpdateLocation = true,
            LocationID = locationID,
            UpdateBuildings = true,
            AddBuildingsIDs = addBuildingsIDs,
            RemoveBuildingsIDs = removeBuildingsIDs
        };

        dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

        dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(meicr => meicr.GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                      .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

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
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        string locationID = "10Fc4a7f1e00f1BDebAe4509";
        ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
        ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).Id };

        UpdateEducationalInstitutionLocationCommand request = new()
        {
            EducationalInstitutionID = educationalInstitutionID,
            UpdateLocation = true,
            LocationID = locationID,
            UpdateBuildings = true,
            AddBuildingsIDs = addBuildingsIDs,
            RemoveBuildingsIDs = removeBuildingsIDs
        };

        dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

        dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(meicr => meicr.GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                      .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

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
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        string locationID = "10Fc4a7f1e00f1BDebAe4509";
        ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
        ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).Id };

        UpdateEducationalInstitutionLocationCommand request = new()
        {
            EducationalInstitutionID = educationalInstitutionID,
            UpdateLocation = true,
            LocationID = locationID,
            UpdateBuildings = true,
            AddBuildingsIDs = addBuildingsIDs,
            RemoveBuildingsIDs = removeBuildingsIDs
        };

        dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

        dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(meicr => meicr.GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                      .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

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
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        string locationID = "10Fc4a7f1e00f1BDebAe4509";

        UpdateEducationalInstitutionLocationCommand request = new()
        {
            EducationalInstitutionID = educationalInstitutionID,
            UpdateLocation = true,
            LocationID = locationID,
            UpdateBuildings = false,
            AddBuildingsIDs = default,
            RemoveBuildingsIDs = default
        };

        dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

        dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(meicr => meicr.GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                      .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

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
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        string locationID = "10Fc4a7f1e00f1BDebAe4509";

        UpdateEducationalInstitutionLocationCommand request = new()
        {
            EducationalInstitutionID = educationalInstitutionID,
            UpdateLocation = true,
            LocationID = locationID,
            UpdateBuildings = false,
            AddBuildingsIDs = default,
            RemoveBuildingsIDs = default
        };

        dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

        dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(meicr => meicr.GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                      .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

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
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        string locationID = "10Fc4a7f1e00f1BDebAe4509";

        UpdateEducationalInstitutionLocationCommand request = new()
        {
            EducationalInstitutionID = educationalInstitutionID,
            UpdateLocation = true,
            LocationID = locationID,
            UpdateBuildings = false,
            AddBuildingsIDs = default,
            RemoveBuildingsIDs = default
        };

        dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

        dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(meicr => meicr.GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                      .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

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
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        string locationID = "10Fc4a7f1e00f1BDebAe4509";

        UpdateEducationalInstitutionLocationCommand request = new()
        {
            EducationalInstitutionID = educationalInstitutionID,
            UpdateLocation = true,
            LocationID = locationID,
            UpdateBuildings = false,
            AddBuildingsIDs = default,
            RemoveBuildingsIDs = default
        };

        dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

        dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(meicr => meicr.GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                      .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

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
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
        ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).Id };

        UpdateEducationalInstitutionLocationCommand request = new()
        {
            EducationalInstitutionID = educationalInstitutionID,
            UpdateLocation = false,
            LocationID = default,
            UpdateBuildings = true,
            AddBuildingsIDs = addBuildingsIDs,
            RemoveBuildingsIDs = removeBuildingsIDs
        };

        dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

        dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(meicr => meicr.GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                      .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

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
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
        ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).Id };

        UpdateEducationalInstitutionLocationCommand request = new()
        {
            EducationalInstitutionID = educationalInstitutionID,
            UpdateLocation = false,
            LocationID = default,
            UpdateBuildings = true,
            AddBuildingsIDs = addBuildingsIDs,
            RemoveBuildingsIDs = removeBuildingsIDs
        };

        dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

        dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(meicr => meicr.GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                      .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

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
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
        ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).Id };

        UpdateEducationalInstitutionLocationCommand request = new()
        {
            EducationalInstitutionID = educationalInstitutionID,
            UpdateLocation = false,
            LocationID = default,
            UpdateBuildings = true,
            AddBuildingsIDs = addBuildingsIDs,
            RemoveBuildingsIDs = removeBuildingsIDs
        };

        dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

        dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(meicr => meicr.GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                      .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

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
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
        ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).Id };

        UpdateEducationalInstitutionLocationCommand request = new()
        {
            EducationalInstitutionID = educationalInstitutionID,
            UpdateLocation = false,
            LocationID = default,
            UpdateBuildings = true,
            AddBuildingsIDs = addBuildingsIDs,
            RemoveBuildingsIDs = removeBuildingsIDs
        };

        dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

        dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(meicr => meicr.GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                      .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

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
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
        ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).Id };

        UpdateEducationalInstitutionLocationCommand request = new()
        {
            EducationalInstitutionID = educationalInstitutionID,
            UpdateLocation = false,
            LocationID = default,
            UpdateBuildings = true,
            AddBuildingsIDs = addBuildingsIDs,
            RemoveBuildingsIDs = removeBuildingsIDs
        };

        dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

        dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(meicr => meicr.GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                      .ReturnsAsync((Domain::EducationalInstitution)default);

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
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
        ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).Id };

        UpdateEducationalInstitutionLocationCommand request = new()
        {
            EducationalInstitutionID = educationalInstitutionID,
            UpdateLocation = false,
            LocationID = default,
            UpdateBuildings = true,
            AddBuildingsIDs = addBuildingsIDs,
            RemoveBuildingsIDs = removeBuildingsIDs
        };

        dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

        dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(meicr => meicr.GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                      .ReturnsAsync((Domain::EducationalInstitution)default);

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
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
        ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).Id };

        UpdateEducationalInstitutionLocationCommand request = new()
        {
            EducationalInstitutionID = educationalInstitutionID,
            UpdateLocation = false,
            LocationID = default,
            UpdateBuildings = true,
            AddBuildingsIDs = addBuildingsIDs,
            RemoveBuildingsIDs = removeBuildingsIDs
        };

        dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

        dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(meicr => meicr.GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                      .ReturnsAsync((Domain::EducationalInstitution)default);

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