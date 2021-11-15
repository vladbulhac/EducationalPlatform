using EducationalInstitution.API.Tests.Shared;
using EducationalInstitution.Application;
using EducationalInstitution.Application.Commands;
using EducationalInstitution.Application.Commands.Handlers;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using RabbitMQEventBus.Transactional_Outbox.Services.Outbox_Services;
using System.Net;
using Xunit;

namespace EducationalInstitution.API.UnitTests.Application_Tests.Commands_Tests.Handlers_Tests.UpdateLocation;

public class UpdateEducationalInstitutionLocationCommandHandlerTests : IClassFixture<MockDependenciesHelper<UpdateEducationalInstitutionLocationCommandHandler>>,
                                                                       IClassFixture<TestDataFromJSONParser>
{
    private readonly TestDataFromJSONParser testDataHelper;
    private readonly MockDependenciesHelper<UpdateEducationalInstitutionLocationCommandHandler> dependenciesHelper;

    /// <remarks>Called before each test</remarks>
    public UpdateEducationalInstitutionLocationCommandHandlerTests(MockDependenciesHelper<UpdateEducationalInstitutionLocationCommandHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
    {
        this.dependenciesHelper = dependenciesHelper;
        this.testDataHelper = testDataHelper;
    }

    [Fact]
    public async Task GivenAnUpdateEducationalInstitutionLocationCommand_ShouldReturnHttpStatusCodeNoContent()
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

        SetupMockedDependencies();
        var handler = new UpdateEducationalInstitutionLocationCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task GivenAnUpdateEducationalInstitutionLocationCommand_ShouldReturnAnEmptyMessage()
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

        SetupMockedDependencies();
        var handler = new UpdateEducationalInstitutionLocationCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task GivenAnUpdateEducationalInstitutionLocationCommand_ShouldReturnOperationStatusTrue()
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

        SetupMockedDependencies();
        var handler = new UpdateEducationalInstitutionLocationCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.True(result.OperationStatus);
    }

    private void SetupMockedDependencies()
    {
        dependenciesHelper.mockUnitOfWorkCommand.Setup(muowc => muowc.ExecuteTransactionAsync(It.IsAny<Func<IDbContextTransaction, IIntegrationEventOutboxService, UpdateEducationalInstitutionLocationCommand, Task<Response>>>(),
                                                                                             It.IsAny<UpdateEducationalInstitutionLocationCommand>()))
                                                 .ReturnsAsync(new Response
                                                 {
                                                     Message = string.Empty,
                                                     OperationStatus = true,
                                                     StatusCode = HttpStatusCode.NoContent
                                                 });
    }

    [Fact]
    public async Task GivenAnUpdateEducationalInstitutionLocationCommand_TransactionFails_ShouldReturnOperationStatusFalse()
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

        SetupMockedDependenciesToFailTheTransaction();
        var handler = new UpdateEducationalInstitutionLocationCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.False(result.OperationStatus);
    }

    [Fact]
    public async Task GivenAnUpdateEducationalInstitutionLocationCommand_TransactionFails_ShouldReturnHttpStatusCodeInternalServerError()
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

        SetupMockedDependenciesToFailTheTransaction();
        var handler = new UpdateEducationalInstitutionLocationCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }

    [Fact]
    public async Task GivenAnUpdateEducationalInstitutionLocationCommand_TransactionFails_ShouldReturnExpectedMessage()
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

        SetupMockedDependenciesToFailTheTransaction();
        var handler = new UpdateEducationalInstitutionLocationCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.Equal("Could not successfully handle all the required operations for this request!", result.Message);
    }

    private void SetupMockedDependenciesToFailTheTransaction()
    {
        dependenciesHelper.mockUnitOfWorkCommand.Setup(muowc => muowc.ExecuteTransactionAsync(It.IsAny<Func<IDbContextTransaction, IIntegrationEventOutboxService, UpdateEducationalInstitutionLocationCommand, Task<Response>>>(),
                                                                                             It.IsAny<UpdateEducationalInstitutionLocationCommand>()))
                                                .ReturnsAsync((Response)default);
    }

    [Fact]
    public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
    {
        //Arrange
        UpdateEducationalInstitutionLocationCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockLogger.Object);

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null));
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
        Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionLocationCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, null));
    }
}