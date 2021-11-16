using EducationalInstitution.API.Tests.Shared;
using EducationalInstitution.Application;
using EducationalInstitution.Application.Commands;
using EducationalInstitution.Application.Commands.Handlers;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using RabbitMQEventBus.Transactional_Outbox.Services.Outbox_Services;
using System.Net;
using Xunit;

namespace EducationalInstitution.API.UnitTests.Application_Tests.Commands_Tests.Handlers_Tests.UpdateParent;

public class UpdateEducationalInstitutionParentCommandHandlerTests : IClassFixture<MockDependenciesHelper<UpdateEducationalInstitutionParentCommandHandler>>,
                                                                     IClassFixture<TestDataFromJSONParser>
{
    private readonly MockDependenciesHelper<UpdateEducationalInstitutionParentCommandHandler> dependenciesHelper;
    private readonly TestDataFromJSONParser testDataHelper;

    /// <remarks>Called before each test</remarks>
    public UpdateEducationalInstitutionParentCommandHandlerTests(MockDependenciesHelper<UpdateEducationalInstitutionParentCommandHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
    {
        this.dependenciesHelper = dependenciesHelper;
        this.testDataHelper = testDataHelper;
    }

    [Fact]
    public async Task GivenAnUpdateEducationalInstitutionParentCommand_ShouldReturnHttpStatusCodeNoContent()
    {
        //Arrange
        UpdateEducationalInstitutionParentCommand request = new()
        {
            EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
            ParentInstitutionID = testDataHelper.EducationalInstitutions[1].Id
        };

        SetupMockedDependencies();

        var handler = new UpdateEducationalInstitutionParentCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                           dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task GivenAnUpdateEducationalInstitutionParentCommand_ShouldReturnEmptyMessage()
    {
        //Arrange
        UpdateEducationalInstitutionParentCommand request = new()
        {
            EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
            ParentInstitutionID = testDataHelper.EducationalInstitutions[1].Id
        };

        SetupMockedDependencies();

        var handler = new UpdateEducationalInstitutionParentCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                           dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.Empty(result.Message);
    }

    [Fact]
    public async Task GivenAnUpdateEducationalInstitutionParentCommand_ShouldReturnOperationStatusTrue()
    {
        //Arrange
        UpdateEducationalInstitutionParentCommand request = new()
        {
            EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
            ParentInstitutionID = testDataHelper.EducationalInstitutions[1].Id
        };

        SetupMockedDependencies();

        var handler = new UpdateEducationalInstitutionParentCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                           dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.True(result.OperationStatus);
    }

    private void SetupMockedDependencies()
    {
        dependenciesHelper.mockUnitOfWorkCommand.Setup(muowc => muowc.ExecuteTransactionAsync(It.IsAny<Func<IDbContextTransaction, IIntegrationEventOutboxService, UpdateEducationalInstitutionParentCommand, Task<Response>>>(),
                                                                                              It.IsAny<UpdateEducationalInstitutionParentCommand>()))
                                                .ReturnsAsync(new Response
                                                {
                                                    Message = string.Empty,
                                                    OperationStatus = true,
                                                    StatusCode = HttpStatusCode.NoContent
                                                });
    }

    private void SetupMockedDependenciesToFailTheTransaction()
    {
        dependenciesHelper.mockUnitOfWorkCommand.Setup(muowc => muowc.ExecuteTransactionAsync(It.IsAny<Func<IDbContextTransaction, IIntegrationEventOutboxService, UpdateEducationalInstitutionParentCommand, Task<Response>>>(),
                                                                                              It.IsAny<UpdateEducationalInstitutionParentCommand>()))
                                                .ReturnsAsync((Response)default);
    }

    [Fact]
    public async Task GivenAnUpdateEducationalInstitutionParentCommand_TheTransactionFails_ShouldReturnOperationStatusFalse()
    {
        //Arrange
        UpdateEducationalInstitutionParentCommand request = new()
        {
            EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
            ParentInstitutionID = testDataHelper.EducationalInstitutions[1].Id
        };

        SetupMockedDependenciesToFailTheTransaction();

        var handler = new UpdateEducationalInstitutionParentCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                           dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.False(result.OperationStatus);
    }

    [Fact]
    public async Task GivenAnUpdateEducationalInstitutionParentCommand_TheTransactionFails_ShouldReturnHttpStatusCodeInternalServerError()
    {
        //Arrange
        UpdateEducationalInstitutionParentCommand request = new()
        {
            EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
            ParentInstitutionID = testDataHelper.EducationalInstitutions[1].Id
        };

        SetupMockedDependenciesToFailTheTransaction();

        var handler = new UpdateEducationalInstitutionParentCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                           dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }

    [Fact]
    public async Task GivenAnUpdateEducationalInstitutionParentCommand_TheTransactionFails_ShouldReturnExpectedMessage()
    {
        //Arrange
        UpdateEducationalInstitutionParentCommand request = new()
        {
            EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
            ParentInstitutionID = testDataHelper.EducationalInstitutions[1].Id
        };

        SetupMockedDependenciesToFailTheTransaction();

        var handler = new UpdateEducationalInstitutionParentCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                           dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.Equal("Could not successfully handle all the required operations for this request!", result.Message);
    }

    [Fact]
    public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
    {
        //Arrange
        UpdateEducationalInstitutionParentCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockLogger.Object);

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null));
    }

    [Fact]
    public void GivenANullArgumentRepositoryToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
    {
        //Assert
        Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionParentCommandHandler(null, dependenciesHelper.mockLogger.Object));
    }

    [Fact]
    public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
    {
        //Assert
        Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionParentCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, null));
    }
}