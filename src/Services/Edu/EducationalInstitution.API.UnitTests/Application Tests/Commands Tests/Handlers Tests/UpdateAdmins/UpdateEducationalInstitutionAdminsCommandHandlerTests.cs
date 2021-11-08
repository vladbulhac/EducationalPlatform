using EducationalInstitution.API.Tests.Shared;
using EducationalInstitution.Application;
using EducationalInstitution.Application.Commands;
using EducationalInstitution.Application.Commands.Handlers;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using RabbitMQEventBus.Transactional_Outbox.Services.Outbox_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

using Domain = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.API.UnitTests.Application_Tests.Commands_Tests.Handlers_Tests.UpdateAdmins
{
    public class UpdateEducationalInstitutionAdminsCommandHandlerTests : IClassFixture<MockDependenciesHelper<UpdateEducationalInstitutionAdminsCommandHandler>>,
                                                                         IClassFixture<TestDataFromJSONParser>

    {
        private readonly MockDependenciesHelper<UpdateEducationalInstitutionAdminsCommandHandler> dependenciesHelper;
        private readonly TestDataFromJSONParser testDataHelper;

        /// <remarks>Called before each test</remarks>
        public UpdateEducationalInstitutionAdminsCommandHandlerTests(MockDependenciesHelper<UpdateEducationalInstitutionAdminsCommandHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionAdminsCommand_ShouldReturnAResponseType()
        {
            //Arrange
            UpdateEducationalInstitutionAdminsCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
                NewAdmins = new List<AdminDetails>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = new List<string>() { "user.test.all" } } },
                AdminsWithNewPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.delete" } } },
                AdminsWithRevokedPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.update" } } }
            };

            SetupMockedDependencies();

            var handler = new UpdateEducationalInstitutionAdminsCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                              dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.IsType<Response>(result);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionAdminsCommand_ShouldReturnEmptyMessage()
        {
            //Arrange
            UpdateEducationalInstitutionAdminsCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
                NewAdmins = new List<AdminDetails>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = new List<string>() { "user.test.all" } } },
                AdminsWithNewPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.delete" } } },
                AdminsWithRevokedPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.update" } } }
            };

            SetupMockedDependencies();

            var handler = new UpdateEducationalInstitutionAdminsCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                              dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionAdminsCommand_ShouldReturnTrueOperationStatus()
        {
            //Arrange
            UpdateEducationalInstitutionAdminsCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
                NewAdmins = new List<AdminDetails>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = new List<string>() { "user.test.all" } } },
                AdminsWithNewPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.delete" } } },
                AdminsWithRevokedPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.update" } } }
            };

            SetupMockedDependencies();

            var handler = new UpdateEducationalInstitutionAdminsCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                              dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionAdminsCommand_ShouldReturnStatusCodeNoContent()
        {
            //Arrange
            UpdateEducationalInstitutionAdminsCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
                NewAdmins = new List<AdminDetails>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = new List<string>() { "user.test.all" } } },
                AdminsWithNewPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.delete" } } },
                AdminsWithRevokedPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.update" } } }
            };

            SetupMockedDependencies();

            var handler = new UpdateEducationalInstitutionAdminsCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                              dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        private void SetupMockedDependencies()
        {
            dependenciesHelper.mockUnitOfWorkCommand.Setup(muowc => muowc.ExecuteTransactionAsync(It.IsAny<Func<IDbContextTransaction, IIntegrationEventOutboxService, UpdateEducationalInstitutionAdminsCommand, Task<Response>>>(),
                                                                                                It.IsAny<UpdateEducationalInstitutionAdminsCommand>()))
                                                    .ReturnsAsync(new Response
                                                    {
                                                        Message = string.Empty,
                                                        OperationStatus = true,
                                                        StatusCode = HttpStatusCode.NoContent
                                                    });
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionAdminsCommand_TransactionFails_ShouldReturnStatusInternalServerError()
        {
            //Arrange
            UpdateEducationalInstitutionAdminsCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
                NewAdmins = new List<AdminDetails>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = new List<string>() { "user.test.all" } } },
                AdminsWithNewPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.delete" } } },
                AdminsWithRevokedPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.update" } } }
            };

            SetupMockedDependenciesToFailTheTransaction();

            var handler = new UpdateEducationalInstitutionAdminsCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                              dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionAdminsCommand_TransactionFails_ShouldReturnOperationStatusFalse()
        {
            //Arrange
            UpdateEducationalInstitutionAdminsCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
                NewAdmins = new List<AdminDetails>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = new List<string>() { "user.test.all" } } },
                AdminsWithNewPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.delete" } } },
                AdminsWithRevokedPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.update" } } }
            };

            SetupMockedDependenciesToFailTheTransaction();

            var handler = new UpdateEducationalInstitutionAdminsCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                              dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionAdminsCommand_TransactionFails_ShouldReturnExpectedMessage()
        {
            //Arrange
            UpdateEducationalInstitutionAdminsCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
                NewAdmins = new List<AdminDetails>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = new List<string>() { "user.test.all" } } },
                AdminsWithNewPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.delete" } } },
                AdminsWithRevokedPermissions = new List<AdminDetails>() { new() { Identity = testDataHelper.EducationalInstitutions[0].Admins.ElementAt(0).Id, Permissions = new List<string>() { "user.test.update" } } }
            };

            SetupMockedDependenciesToFailTheTransaction();

            var handler = new UpdateEducationalInstitutionAdminsCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                              dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal("Could not successfully handle all the required operations for this request!", result.Message);
        }

        private void SetupMockedDependenciesToFailTheTransaction()
        {
            dependenciesHelper.mockUnitOfWorkCommand.Setup(muowc => muowc.ExecuteTransactionAsync(It.IsAny<Func<IDbContextTransaction, IIntegrationEventOutboxService, UpdateEducationalInstitutionAdminsCommand, Task<Response>>>(),
                                                                                                It.IsAny<UpdateEducationalInstitutionAdminsCommand>()))
                                                    .ReturnsAsync((Response)default);
        }

        [Fact]
        public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            UpdateEducationalInstitutionAdminsCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null));
        }

        [Fact]
        public void GivenANullArgumentRepositoryToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionAdminsCommandHandler(null, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionAdminsCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, null));
        }
    }
}