using EducationalInstitution.API.Tests.Shared;
using EducationalInstitution.Application;
using EducationalInstitution.Application.Commands;
using EducationalInstitution.Application.Commands.Handlers;
using EducationalInstitution.Application.Commands.Results;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using RabbitMQEventBus.Transactional_Outbox.Services.Outbox_Services;
using System.Net;
using Xunit;

namespace EducationalInstitution.API.UnitTests.Application_Tests.Commands_Tests.Handlers_Tests.Create;

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
        var adminId = Guid.NewGuid().ToString();
        Guid parentInstitutionID = testDataHelper.EducationalInstitutions[0].Id;

        CreateEducationalInstitutionCommand request = new()
        {
            Name = name,
            Description = description,
            LocationID = locationID,
            BuildingsIDs = buildingsIDs,
            ParentInstitutionID = parentInstitutionID,
            AdminId = adminId
        };

        SetupMockedDependencies();

        var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
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
        var adminId = Guid.NewGuid().ToString();
        Guid parentInstitutionID = testDataHelper.EducationalInstitutions[0].Id;

        CreateEducationalInstitutionCommand request = new()
        {
            Name = name,
            Description = description,
            LocationID = locationID,
            BuildingsIDs = buildingsIDs,
            ParentInstitutionID = parentInstitutionID,
            AdminId = adminId
        };

        SetupMockedDependencies();

        var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
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
        var adminId = Guid.NewGuid().ToString();
        Guid parentInstitutionID = testDataHelper.EducationalInstitutions[0].Id;

        CreateEducationalInstitutionCommand request = new()
        {
            Name = name,
            Description = description,
            LocationID = locationID,
            BuildingsIDs = buildingsIDs,
            ParentInstitutionID = parentInstitutionID,
            AdminId = adminId
        };

        SetupMockedDependencies();

        var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
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
        var adminId = Guid.NewGuid().ToString();
        Guid parentInstitutionID = testDataHelper.EducationalInstitutions[0].Id;

        CreateEducationalInstitutionCommand request = new()
        {
            Name = name,
            Description = description,
            LocationID = locationID,
            BuildingsIDs = buildingsIDs,
            ParentInstitutionID = parentInstitutionID,
            AdminId = adminId
        };

        SetupMockedDependencies();

        var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
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
        var adminId = Guid.NewGuid().ToString();
        Guid parentInstitutionID = testDataHelper.EducationalInstitutions[0].Id;

        CreateEducationalInstitutionCommand request = new()
        {
            Name = name,
            Description = description,
            LocationID = locationID,
            BuildingsIDs = buildingsIDs,
            ParentInstitutionID = parentInstitutionID,
            AdminId = adminId
        };

        SetupMockedDependencies();

        var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
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
        var adminId = Guid.NewGuid().ToString();
        Guid parentInstitutionID = testDataHelper.EducationalInstitutions[0].Id;

        CreateEducationalInstitutionCommand request = new()
        {
            Name = name,
            Description = description,
            LocationID = locationID,
            BuildingsIDs = buildingsIDs,
            ParentInstitutionID = parentInstitutionID,
            AdminId = adminId
        };

        SetupMockedDependencies();

        var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
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
        var adminId = Guid.NewGuid().ToString();
        Guid parentInstitutionID = testDataHelper.EducationalInstitutions[1].Id;

        CreateEducationalInstitutionCommand request = new()
        {
            Name = name,
            Description = description,
            LocationID = locationID,
            BuildingsIDs = buildingsIDs,
            ParentInstitutionID = parentInstitutionID,
            AdminId = adminId
        };

        SetupMockedDependenciesToNotFindTheParent(parentInstitutionID);

        var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
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
        var adminId = Guid.NewGuid().ToString();
        Guid parentInstitutionID = testDataHelper.EducationalInstitutions[1].Id;

        CreateEducationalInstitutionCommand request = new()
        {
            Name = name,
            Description = description,
            LocationID = locationID,
            BuildingsIDs = buildingsIDs,
            ParentInstitutionID = parentInstitutionID,
            AdminId = adminId
        };

        SetupMockedDependenciesToNotFindTheParent(parentInstitutionID);

        var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
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
        var adminId = Guid.NewGuid().ToString();
        Guid parentInstitutionID = testDataHelper.EducationalInstitutions[1].Id;

        CreateEducationalInstitutionCommand request = new()
        {
            Name = name,
            Description = description,
            LocationID = locationID,
            BuildingsIDs = buildingsIDs,
            ParentInstitutionID = parentInstitutionID,
            AdminId = adminId
        };

        SetupMockedDependenciesToNotFindTheParent(parentInstitutionID);

        var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
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
        var adminId = Guid.NewGuid().ToString();
        Guid parentInstitutionID = testDataHelper.EducationalInstitutions[1].Id;

        CreateEducationalInstitutionCommand request = new()
        {
            Name = name,
            Description = description,
            LocationID = locationID,
            BuildingsIDs = buildingsIDs,
            ParentInstitutionID = parentInstitutionID,
            AdminId = adminId
        };

        SetupMockedDependenciesToNotFindTheParent(parentInstitutionID);

        var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                     dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.Equal($"The Educational Institution has been successfully created but the Parent Institution with the following ID: {parentInstitutionID} has not been found!", result.Message);
    }

    private void SetupMockedDependencies()
    {
        dependenciesHelper.mockUnitOfWorkCommand.Setup(muokc => muokc.ExecuteTransactionAsync(It.IsAny<Func<IDbContextTransaction, IIntegrationEventOutboxService, CreateEducationalInstitutionCommand, Task<Response<CreateEducationalInstitutionCommandResult>>>>(),
                                                                                           It.IsAny<CreateEducationalInstitutionCommand>()))
                                               .ReturnsAsync(new Response<CreateEducationalInstitutionCommandResult>()
                                               {
                                                   Data = new() { EducationalInstitutionID = Guid.NewGuid() },
                                                   OperationStatus = true,
                                                   StatusCode = HttpStatusCode.Created,
                                                   Message = string.Empty
                                               });
    }

    private void SetupMockedDependenciesToNotFindTheParent(Guid parentId)
    {
        dependenciesHelper.mockUnitOfWorkCommand.Setup(muokc => muokc.ExecuteTransactionAsync(It.IsAny<Func<IDbContextTransaction, IIntegrationEventOutboxService, CreateEducationalInstitutionCommand, Task<Response<CreateEducationalInstitutionCommandResult>>>>(),
                                                                                           It.IsAny<CreateEducationalInstitutionCommand>()))
                                               .ReturnsAsync(new Response<CreateEducationalInstitutionCommandResult>()
                                               {
                                                   Data = new() { EducationalInstitutionID = Guid.NewGuid() },
                                                   OperationStatus = true,
                                                   StatusCode = HttpStatusCode.MultiStatus,
                                                   Message = $"The Educational Institution has been successfully created but the Parent Institution with the following ID: {parentId} has not been found!"
                                               });
    }

    [Fact]
    public async Task GivenAName_Description_LocationID_BuildingID_AdminID_WithDefaultParentInstitutionID__ShouldReturnAResponseThatIncludesAStatusCodeCreatedField()
    {
        //Arrange
        string name = "West High School";
        string description = "School";
        string locationID = "location1235";
        var buildingsIDs = new List<string>() { "building1235" };
        var adminId = Guid.NewGuid().ToString();
        Guid parentInstitutionID = Guid.Empty;

        CreateEducationalInstitutionCommand request = new()
        {
            Name = name,
            Description = description,
            LocationID = locationID,
            BuildingsIDs = buildingsIDs,
            ParentInstitutionID = parentInstitutionID,
            AdminId = adminId
        };

        SetupMockedDependencies();

        var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
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
        var adminId = Guid.NewGuid().ToString();
        Guid parentInstitutionID = Guid.Empty;

        CreateEducationalInstitutionCommand request = new()
        {
            Name = name,
            Description = description,
            LocationID = locationID,
            BuildingsIDs = buildingsIDs,
            ParentInstitutionID = parentInstitutionID,
            AdminId = adminId
        };

        SetupMockedDependencies();

        var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
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
        var adminId = Guid.NewGuid().ToString();
        Guid parentInstitutionID = Guid.Empty;

        CreateEducationalInstitutionCommand request = new()
        {
            Name = name,
            Description = description,
            LocationID = locationID,
            BuildingsIDs = buildingsIDs,
            ParentInstitutionID = parentInstitutionID,
            AdminId = adminId
        };

        SetupMockedDependencies();
        var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
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
        var adminId = Guid.NewGuid().ToString();
        Guid parentInstitutionID = Guid.Empty;

        CreateEducationalInstitutionCommand request = new()
        {
            Name = name,
            Description = description,
            LocationID = locationID,
            BuildingsIDs = buildingsIDs,
            ParentInstitutionID = parentInstitutionID,
            AdminId = adminId
        };

        SetupMockedDependencies();

        var handler = new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
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
                                                                dependenciesHelper.mockLogger.Object);

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null));
    }

    [Fact]
    public void GivenANullArgumentUnitOfWorkForCommandsToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
    {
        //Assert
        Assert.Throws<ArgumentNullException>(() => new CreateEducationalInstitutionCommandHandler(null,
                                                                                                  dependenciesHelper.mockLogger.Object));
    }

    [Fact]
    public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
    {
        //Assert
        Assert.Throws<ArgumentNullException>(() => new CreateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                                                  null));
    }
}