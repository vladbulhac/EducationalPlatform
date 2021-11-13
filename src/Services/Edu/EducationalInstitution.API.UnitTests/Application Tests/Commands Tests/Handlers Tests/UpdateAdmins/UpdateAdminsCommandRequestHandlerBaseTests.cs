using EducationalInstitution.API.Tests.Shared;
using EducationalInstitution.Application;
using EducationalInstitution.Application.Commands;
using EducationalInstitution.Application.Commands.Handlers;
using Moq;
using System.Net;
using Xunit;
using Domain = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.API.UnitTests.Application_Tests.Commands_Tests.Handlers_Tests.UpdateAdmins;

public class UpdateAdminsCommandRequestHandlerBaseTests : UpdateEducationalInstitutionAdminsCommandHandler, IClassFixture<MockDependenciesHelper<UpdateEducationalInstitutionAdminsCommandHandler>>,
                                                                                                            IClassFixture<TestDataFromJSONParser>
{
    private readonly TestDataFromJSONParser testDataHelper;
    private readonly MockDependenciesHelper<UpdateEducationalInstitutionAdminsCommandHandler> dependenciesHelper;

    public UpdateAdminsCommandRequestHandlerBaseTests(MockDependenciesHelper<UpdateEducationalInstitutionAdminsCommandHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
    : base(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockLogger.Object)
    {
        this.dependenciesHelper = dependenciesHelper;
        this.testDataHelper = testDataHelper;
    }

    [Fact]
    public async Task GivenAValidUpdateEducationalInstitutionAdminsCommand_ToTransactionOperationsMethod_ShouldReturnAResponseType()
    {
        //Arrange
        UpdateEducationalInstitutionAdminsCommand request = new()
        {
            EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
            NewAdmins = new List<AdminDetails>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = new List<string>() { "user.test.all" } } },
            AdminsWithNewPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.delete" } } },
            AdminsWithRevokedPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.update" } } }
        };

        dependenciesHelper.mockUnitOfWorkCommand.Setup(uokc => uokc.UsingEducationalInstitutionCommandRepository())
                                                .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

        dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(eicr => eicr.GetEducationalInstitutionIncludingAdminsAsync(request.EducationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                      .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

        //Act
        var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

        //Assert
        Assert.IsType<Response>(result);
    }

    [Fact]
    public async Task GivenAValidUpdateEducationalInstitutionAdminsCommand_ToTransactionOperationsMethod_ShouldReturnEmptyMessage()
    {
        //Arrange
        UpdateEducationalInstitutionAdminsCommand request = new()
        {
            EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
            NewAdmins = new List<AdminDetails>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = new List<string>() { "user.test.all" } } },
            AdminsWithNewPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.delete" } } },
            AdminsWithRevokedPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.update" } } }
        };

        dependenciesHelper.mockUnitOfWorkCommand.Setup(uokc => uokc.UsingEducationalInstitutionCommandRepository())
                                                .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

        dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(eicr => eicr.GetEducationalInstitutionIncludingAdminsAsync(request.EducationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                     .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

        //Act
        var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

        //Assert
        Assert.Empty(result.Message);
    }

    [Fact]
    public async Task GivenAValidUpdateEducationalInstitutionAdminsCommand_ToTransactionOperationsMethod_EntityIsNotFound_ShouldReturnFalseOperationStatus()
    {
        //Arrange
        UpdateEducationalInstitutionAdminsCommand request = new()
        {
            EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
            NewAdmins = new List<AdminDetails>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = new List<string>() { "user.test.all" } } },
            AdminsWithNewPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.delete" } } },
            AdminsWithRevokedPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.update" } } }
        };

        dependenciesHelper.mockUnitOfWorkCommand.Setup(uokc => uokc.UsingEducationalInstitutionCommandRepository())
                                                .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

        dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(eicr => eicr.GetEducationalInstitutionIncludingAdminsAsync(request.EducationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                     .ReturnsAsync((Domain::EducationalInstitution)default);

        //Act
        var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

        //Assert
        Assert.False(result.OperationStatus);
    }

    [Fact]
    public async Task GivenAValidUpdateEducationalInstitutionAdminsCommand_ToTransactionOperationsMethod_EntityIsNotFound_ShouldReturnStatusCodeNotFound()
    {
        //Arrange
        UpdateEducationalInstitutionAdminsCommand request = new()
        {
            EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
            NewAdmins = new List<AdminDetails>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = new List<string>() { "user.test.all" } } },
            AdminsWithNewPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.delete" } } },
            AdminsWithRevokedPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.update" } } }
        };

        dependenciesHelper.mockUnitOfWorkCommand.Setup(uokc => uokc.UsingEducationalInstitutionCommandRepository())
                                                .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

        dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(eicr => eicr.GetEducationalInstitutionIncludingAdminsAsync(request.EducationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                     .ReturnsAsync((Domain::EducationalInstitution)default);

        //Act
        var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task GivenAValidUpdateEducationalInstitutionAdminsCommand_ToTransactionOperationsMethod_EntityIsNotFound_ShouldReturnMessage()
    {
        //Arrange
        UpdateEducationalInstitutionAdminsCommand request = new()
        {
            EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
            NewAdmins = new List<AdminDetails>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = new List<string>() { "user.test.all" } } },
            AdminsWithNewPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.delete" } } },
            AdminsWithRevokedPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.update" } } }
        };

        dependenciesHelper.mockUnitOfWorkCommand.Setup(uokc => uokc.UsingEducationalInstitutionCommandRepository())
                                                .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

        dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(eicr => eicr.GetEducationalInstitutionIncludingAdminsAsync(request.EducationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                     .ReturnsAsync((Domain::EducationalInstitution)default);

        //Act
        var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

        //Assert
        Assert.Equal($"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!", result.Message);
    }

    [Fact]
    public async Task GivenAValidUpdateEducationalInstitutionAdminsCommand_ToTransactionOperationsMethod_ShouldReturnTrueOperationStatus()
    {
        //Arrange
        UpdateEducationalInstitutionAdminsCommand request = new()
        {
            EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
            NewAdmins = new List<AdminDetails>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = new List<string>() { "user.test.all" } } },
            AdminsWithNewPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.delete" } } },
            AdminsWithRevokedPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.update" } } }
        };

        dependenciesHelper.mockUnitOfWorkCommand.Setup(uokc => uokc.UsingEducationalInstitutionCommandRepository())
                                                .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

        dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(eicr => eicr.GetEducationalInstitutionIncludingAdminsAsync(request.EducationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                     .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

        //Act
        var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

        //Assert
        Assert.True(result.OperationStatus);
    }

    [Fact]
    public async Task GivenAValidUpdateEducationalInstitutionAdminsCommand_ToTransactionOperationsMethod_ShouldReturnStatusCodeNoContent()
    {
        //Arrange
        UpdateEducationalInstitutionAdminsCommand request = new()
        {
            EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
            NewAdmins = new List<AdminDetails>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = new List<string>() { "user.test.all" } } },
            AdminsWithNewPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.delete" } } },
            AdminsWithRevokedPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.update" } } }
        };

        dependenciesHelper.mockUnitOfWorkCommand.Setup(uokc => uokc.UsingEducationalInstitutionCommandRepository())
                                                .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

        dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(eicr => eicr.GetEducationalInstitutionIncludingAdminsAsync(request.EducationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                     .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

        //Act
        var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

        //Assert
        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }
}