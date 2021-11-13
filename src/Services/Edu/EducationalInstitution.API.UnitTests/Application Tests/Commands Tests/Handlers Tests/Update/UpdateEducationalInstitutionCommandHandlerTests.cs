using EducationalInstitution.API.Tests.Shared;
using EducationalInstitution.Application;
using EducationalInstitution.Application.Commands;
using EducationalInstitution.Application.Commands.Handlers;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using RabbitMQEventBus.Transactional_Outbox.Services.Outbox_Services;
using System.Net;
using Xunit;

namespace EducationalInstitution.API.UnitTests.Application_Tests.Commands_Tests.Handlers_Tests.Update;

public class UpdateEducationalInstitutionCommandHandlerTests : IClassFixture<MockDependenciesHelper<UpdateEducationalInstitutionCommandHandler>>,
                                                               IClassFixture<TestDataFromJSONParser>
{
    private readonly TestDataFromJSONParser testDataHelper;
    private readonly MockDependenciesHelper<UpdateEducationalInstitutionCommandHandler> dependenciesHelper;

    /// <remarks>Called before each test</remarks>
    public UpdateEducationalInstitutionCommandHandlerTests(MockDependenciesHelper<UpdateEducationalInstitutionCommandHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
    {
        this.dependenciesHelper = dependenciesHelper;
        this.testDataHelper = testDataHelper;
    }

    [Fact]
    public async Task GivenAnUpdateEducationalInstitutionCommand_ShouldReturnOperationStatusTrue()
    {
        //Arrange
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        string name = "New_Name";
        string description = "New_Description";

        UpdateEducationalInstitutionCommand request = new()
        {
            EducationalInstitutionID = educationalInstitutionID,
            UpdateName = true,
            Name = name,
            UpdateDescription = true,
            Description = description
        };

        SetupMockedDependencies();

        var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.True(result.OperationStatus);
    }

    [Fact]
    public async Task GivenAnUpdateEducationalInstitutionCommand_ShouldReturnEmptyMessage()
    {
        //Arrange
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        string name = "New_Name";
        string description = "New_Description";

        UpdateEducationalInstitutionCommand request = new()
        {
            EducationalInstitutionID = educationalInstitutionID,
            UpdateName = true,
            Name = name,
            UpdateDescription = true,
            Description = description
        };

        SetupMockedDependencies();

        var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.Empty(result.Message);
    }

    [Fact]
    public async Task GivenAnUpdateEducationalInstitutionCommand_ShouldReturnHttpStatusCodeNoContent()
    {
        //Arrange
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        string name = "New_Name";
        string description = "New_Description";

        UpdateEducationalInstitutionCommand request = new()
        {
            EducationalInstitutionID = educationalInstitutionID,
            UpdateName = true,
            Name = name,
            UpdateDescription = true,
            Description = description
        };

        SetupMockedDependencies();

        var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    private void SetupMockedDependencies()
    {
        dependenciesHelper.mockUnitOfWorkCommand.Setup(muowc => muowc.ExecuteTransactionAsync(It.IsAny<Func<IDbContextTransaction, IIntegrationEventOutboxService, UpdateEducationalInstitutionCommand, Task<Response>>>(),
                                                                                              It.IsAny<UpdateEducationalInstitutionCommand>()))
                                               .ReturnsAsync(new Response
                                               {
                                                   Message = string.Empty,
                                                   OperationStatus = true,
                                                   StatusCode = HttpStatusCode.NoContent
                                               });
    }

    [Fact]
    public async Task GivenAnUpdateEducationalInstitutionCommand_ShouldReturnHttpStatusCodeInternalServerError()
    {
        //Arrange
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        string name = "New_Name";
        string description = "New_Description";

        UpdateEducationalInstitutionCommand request = new()
        {
            EducationalInstitutionID = educationalInstitutionID,
            UpdateName = true,
            Name = name,
            UpdateDescription = true,
            Description = description
        };

        SetupMockedDependenciesToFailTheTransaction();

        var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }

    [Fact]
    public async Task GivenAnUpdateEducationalInstitutionCommand_ShouldReturnOperationStatusFalse()
    {
        //Arrange
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        string name = "New_Name";
        string description = "New_Description";

        UpdateEducationalInstitutionCommand request = new()
        {
            EducationalInstitutionID = educationalInstitutionID,
            UpdateName = true,
            Name = name,
            UpdateDescription = true,
            Description = description
        };

        SetupMockedDependenciesToFailTheTransaction();

        var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.False(result.OperationStatus);
    }

    [Fact]
    public async Task GivenAnUpdateEducationalInstitutionCommand_ShouldReturnExpectedMessage()
    {
        //Arrange
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        string name = "New_Name";
        string description = "New_Description";

        UpdateEducationalInstitutionCommand request = new()
        {
            EducationalInstitutionID = educationalInstitutionID,
            UpdateName = true,
            Name = name,
            UpdateDescription = true,
            Description = description
        };

        SetupMockedDependenciesToFailTheTransaction();

        var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.Equal("Could not successfully handle all the required operations for this request!", result.Message);
    }

    private void SetupMockedDependenciesToFailTheTransaction()
    {
        dependenciesHelper.mockUnitOfWorkCommand.Setup(muowc => muowc.ExecuteTransactionAsync(It.IsAny<Func<IDbContextTransaction, IIntegrationEventOutboxService, UpdateEducationalInstitutionCommand, Task<Response>>>(),
                                                                                              It.IsAny<UpdateEducationalInstitutionCommand>()))
                                               .ReturnsAsync((Response)default);
    }

    [Fact]
    public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
    {
        //Arrange
        UpdateEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockLogger.Object);

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null));
    }

    [Fact]
    public void GivenANullArgumentRepositoryToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
    {
        //Assert
        Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionCommandHandler(null, dependenciesHelper.mockLogger.Object));
    }

    [Fact]
    public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
    {
        //Assert
        Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, null));
    }
}