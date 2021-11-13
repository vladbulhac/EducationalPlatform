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

namespace EducationalInstitution.API.UnitTests.Application_Tests.Commands_Tests.Handlers_Tests.Disable;

public class DisableEducationalInstitutionCommandHandlerTests : IClassFixture<MockDependenciesHelper<DisableEducationalInstitutionCommandHandler>>,
                                                                IClassFixture<TestDataFromJSONParser>
{
    private readonly MockDependenciesHelper<DisableEducationalInstitutionCommandHandler> dependenciesHelper;
    private readonly TestDataFromJSONParser testDataHelper;

    /// <remarks>Called before each test</remarks>
    public DisableEducationalInstitutionCommandHandlerTests(MockDependenciesHelper<DisableEducationalInstitutionCommandHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
    {
        this.dependenciesHelper = dependenciesHelper;
        this.testDataHelper = testDataHelper;
    }

    #region An entity exists for the input ID TESTS

    [Fact]
    public async Task GivenAnID_ShouldReturnAResponseThatIncludesAStatusCodeAcceptedField()
    {
        //Arrange
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        DisableEducationalInstitutionCommand request = new() { EducationalInstitutionID = educationalInstitutionID };

        SetupMockedDependencies();

        DisableEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                  dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.Equal(HttpStatusCode.Accepted, result.StatusCode);
    }

    [Fact]
    public async Task GivenAnID_ShouldReturnAResponseThatIncludesAnEmptyMessage()
    {
        //Arrange
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        DisableEducationalInstitutionCommand request = new() { EducationalInstitutionID = educationalInstitutionID };

        SetupMockedDependencies();
        DisableEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                  dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.Empty(result.Message);
    }

    [Fact]
    public async Task GivenAnID_ShouldReturnARecordTypeResponse()
    {
        //Arrange
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        DisableEducationalInstitutionCommand request = new() { EducationalInstitutionID = educationalInstitutionID };

        SetupMockedDependencies();

        DisableEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                  dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.IsType<Response<DisableEducationalInstitutionCommandResult>>(result);
    }

    [Fact]
    public async Task GivenAnID_ShouldReturnAResponseThatIncludesDataOfType_DisableEducationalInstitutionCommandResult()
    {
        //Arrange
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        DisableEducationalInstitutionCommand request = new() { EducationalInstitutionID = educationalInstitutionID };

        SetupMockedDependencies();

        DisableEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                  dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.IsType<DisableEducationalInstitutionCommandResult>(result.Data);
    }

    [Fact]
    public async Task GivenAnID_ShouldReturnAResponseThatIncludesDataWithExpectedDateForPermanentDeletion()
    {
        //Arrange
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        DisableEducationalInstitutionCommand request = new() { EducationalInstitutionID = educationalInstitutionID };

        var expectedDeletionDate = new DateTime(2021, 3, 1);

        SetupMockedDependencies();

        DisableEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                  dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.Equal(expectedDeletionDate, result.Data.DateForPermanentDeletion);
    }

    [Fact]
    public async Task GivenAnID_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
    {
        //Arrange
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        DisableEducationalInstitutionCommand request = new() { EducationalInstitutionID = educationalInstitutionID };

        SetupMockedDependencies();

        DisableEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                  dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.True(result.OperationStatus);
    }

    private void SetupMockedDependencies()
    {
        dependenciesHelper.mockUnitOfWorkCommand.Setup(mukc => mukc.ExecuteTransactionAsync(It.IsAny<Func<IDbContextTransaction, IIntegrationEventOutboxService, DisableEducationalInstitutionCommand, Task<Response<DisableEducationalInstitutionCommandResult>>>>(),
                                                                                            It.IsAny<DisableEducationalInstitutionCommand>()))
                                                .ReturnsAsync(new Response<DisableEducationalInstitutionCommandResult>()
                                                {
                                                    Data = new() { DateForPermanentDeletion = new DateTime(2021, 3, 1) },
                                                    OperationStatus = true,
                                                    StatusCode = HttpStatusCode.Accepted,
                                                    Message = string.Empty
                                                });
    }

    #endregion An entity exists for the input ID TESTS

    #region No entity is found for the input ID TESTS

    private void SetupMockedDependenciesToNotFindTheEntity(Guid entityId)
    {
        dependenciesHelper.mockUnitOfWorkCommand.Setup(mukc => mukc.ExecuteTransactionAsync(It.IsAny<Func<IDbContextTransaction, IIntegrationEventOutboxService, DisableEducationalInstitutionCommand, Task<Response<DisableEducationalInstitutionCommandResult>>>>(),
                                                                                            It.IsAny<DisableEducationalInstitutionCommand>()))
                                                .ReturnsAsync(new Response<DisableEducationalInstitutionCommandResult>()
                                                {
                                                    Data = null,
                                                    OperationStatus = false,
                                                    StatusCode = HttpStatusCode.NotFound,
                                                    Message = $"Educational Institution with the following ID: {entityId} has not been found!"
                                                });
    }

    [Fact]
    public async Task GivenAnID_ThatDoesntExistInDatabase_ShouldReturnStatusCodeNotFound()
    {
        //Arrange
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        DisableEducationalInstitutionCommand request = new() { EducationalInstitutionID = educationalInstitutionID };

        SetupMockedDependenciesToNotFindTheEntity(educationalInstitutionID);

        DisableEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task GivenAnID_ThatDoesntExistInDatabase_ShouldReturnExpectedMessage()
    {
        //Arrange
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        DisableEducationalInstitutionCommand request = new() { EducationalInstitutionID = educationalInstitutionID };

        SetupMockedDependenciesToNotFindTheEntity(educationalInstitutionID);

        DisableEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.Equal($"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!", result.Message);
    }

    [Fact]
    public async Task GivenAnID_ThatDoesntExistInDatabase_ShouldReturnFalseOperationStatus()
    {
        //Arrange
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        DisableEducationalInstitutionCommand request = new() { EducationalInstitutionID = educationalInstitutionID };

        SetupMockedDependenciesToNotFindTheEntity(educationalInstitutionID);

        DisableEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.False(result.OperationStatus);
    }

    [Fact]
    public async Task GivenAnID_ThatDoesntExistInDatabase_ShouldReturnNullData()
    {
        //Arrange
        Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
        DisableEducationalInstitutionCommand request = new() { EducationalInstitutionID = educationalInstitutionID };

        SetupMockedDependenciesToNotFindTheEntity(educationalInstitutionID);

        DisableEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(request);

        //Assert
        Assert.Null(result.Data);
    }

    #endregion No entity is found for the input ID TESTS

    [Fact]
    public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
    {
        //Arrange
        DisableEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                    dependenciesHelper.mockLogger.Object);

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null));
    }

    [Fact]
    public void GivenANullArgumentUnitOfWorkForCommandsToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
    {
        //Assert
        Assert.Throws<ArgumentNullException>(() => new DisableEducationalInstitutionCommandHandler(null,
                                                                                                dependenciesHelper.mockLogger.Object));
    }

    [Fact]
    public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
    {
        //Assert
        Assert.Throws<ArgumentNullException>(() => new DisableEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                                                    null));
    }
}