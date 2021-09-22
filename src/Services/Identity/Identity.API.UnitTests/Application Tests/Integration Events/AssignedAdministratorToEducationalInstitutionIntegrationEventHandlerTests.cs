using Identity.API.Application.Integration_Events.Events;
using Identity.API.Application.Integration_Events.Handlers;
using Identity.API.Configuration.User_Permissions;
using Identity.API.Infrastructure.Repositories;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Identity.API.UnitTests.Application_Tests.Integration_Events
{
    public class AssignedAdministratorToEducationalInstitutionIntegrationEventHandlerTests
    {
        private readonly Mock<UserManager<User>> userManagerMock;
        private readonly Mock<IIdentityRepository> identityRepositoryMock;
        private readonly AssignedAdministratorToEducationalInstitutionIntegrationEventHandler eventHandler;

        public AssignedAdministratorToEducationalInstitutionIntegrationEventHandlerTests()
        {
            userManagerMock = new(new Mock<IUserStore<User>>().Object,
                                      new Mock<IOptions<IdentityOptions>>().Object,
                                      new Mock<IPasswordHasher<User>>().Object,
                                      new IUserValidator<User>[0],
                                      new IPasswordValidator<User>[0],
                                      new Mock<ILookupNormalizer>().Object,
                                      new Mock<IdentityErrorDescriber>().Object,
                                      new Mock<IServiceProvider>().Object,
                                      new Mock<ILogger<UserManager<User>>>().Object);

            identityRepositoryMock = new();

            eventHandler = new(userManagerMock.Object, identityRepositoryMock.Object);
        }

        [Fact]
        public async Task GivenAnAssignedAdministratorToEducationalInstitutionIntegrationEvent_WithAllPermission_ShouldAddANewEducationalInstitutionAdministratorToTheDatabase()
        {
            //Arrange
            AssignedAdministratorToEducationalInstitutionIntegrationEvent @event = new()
            {
                EducationalInstitutionId = Guid.NewGuid(),
                Message = "test message",
                Permissions = new List<string>(1) { DefinedUserPermissions.EducationalInstitutionPermissions.All },
                TriggeredBy = new RabbitMQEventBus.IntegrationEvents.EventTrigger() { Action = "test", ServiceName = "Identity.API" },
                Uri = string.Empty,
                UserId = Guid.NewGuid()
            };

            User user = new()
            {
                Id = @event.UserId.ToString(),
                Email = "test@test.com",
                FirstName = "T F",
                LastName = "T L"
            };

            List<EducationalInstitutionAdministrator> fakeDatabase = new();

            userManagerMock.Setup(umm => umm.FindByIdAsync(@event.UserId.ToString())).ReturnsAsync(user);
            identityRepositoryMock.Setup(irm => irm.AddEducationalInstitutionAdministratorAsync(It.IsAny<EducationalInstitutionAdministrator>()))
                                  .Callback<EducationalInstitutionAdministrator>(a => fakeDatabase.Add(a));

            //Act
            await eventHandler.HandleEvent(@event);

            //Assert
            Assert.Single(fakeDatabase);
        }

        [Fact]
        public async Task GivenAnAssignedAdministratorToEducationalInstitutionIntegrationEvent_WithAllPermission_ShouldCreateAnEducationalInstitutionAdministratorWith_CanRemoveEducationalInstitutionFieldTrue()
        {
            //Arrange
            AssignedAdministratorToEducationalInstitutionIntegrationEvent @event = new()
            {
                EducationalInstitutionId = Guid.NewGuid(),
                Message = "test message",
                Permissions = new List<string>(1) { DefinedUserPermissions.EducationalInstitutionPermissions.All },
                TriggeredBy = new RabbitMQEventBus.IntegrationEvents.EventTrigger() { Action = "test", ServiceName = "Identity.API" },
                Uri = string.Empty,
                UserId = Guid.NewGuid()
            };

            User user = new()
            {
                Id = @event.UserId.ToString(),
                Email = "test@test.com",
                FirstName = "T F",
                LastName = "T L"
            };

            List<EducationalInstitutionAdministrator> fakeDatabase = new();

            userManagerMock.Setup(umm => umm.FindByIdAsync(@event.UserId.ToString())).ReturnsAsync(user);
            identityRepositoryMock.Setup(irm => irm.AddEducationalInstitutionAdministratorAsync(It.IsAny<EducationalInstitutionAdministrator>()))
                                  .Callback<EducationalInstitutionAdministrator>(a => fakeDatabase.Add(a));

            //Act
            await eventHandler.HandleEvent(@event);

            //Assert
            Assert.True(fakeDatabase.FirstOrDefault().CanRemoveEducationalInstitution);
        }

        [Fact]
        public async Task GivenAnAssignedAdministratorToEducationalInstitutionIntegrationEvent_WithAllPermission_ShouldCreateAnEducationalInstitutionAdministratorWith_CanUpdateEducationalInstitutionDetailsFieldTrue()
        {
            //Arrange
            AssignedAdministratorToEducationalInstitutionIntegrationEvent @event = new()
            {
                EducationalInstitutionId = Guid.NewGuid(),
                Message = "test message",
                Permissions = new List<string>(1) { DefinedUserPermissions.EducationalInstitutionPermissions.All },
                TriggeredBy = new RabbitMQEventBus.IntegrationEvents.EventTrigger() { Action = "test", ServiceName = "Identity.API" },
                Uri = string.Empty,
                UserId = Guid.NewGuid()
            };

            User user = new()
            {
                Id = @event.UserId.ToString(),
                Email = "test@test.com",
                FirstName = "T F",
                LastName = "T L"
            };

            List<EducationalInstitutionAdministrator> fakeDatabase = new();

            userManagerMock.Setup(umm => umm.FindByIdAsync(@event.UserId.ToString())).ReturnsAsync(user);
            identityRepositoryMock.Setup(irm => irm.AddEducationalInstitutionAdministratorAsync(It.IsAny<EducationalInstitutionAdministrator>()))
                                  .Callback<EducationalInstitutionAdministrator>(a => fakeDatabase.Add(a));

            //Act
            await eventHandler.HandleEvent(@event);

            //Assert
            Assert.True(fakeDatabase.FirstOrDefault().CanUpdateEducationalInstitutionDetails);
        }

        [Fact]
        public async Task GivenAnAssignedAdministratorToEducationalInstitutionIntegrationEvent_WithAllPermission_ShouldCreateAnEducationalInstitutionAdministratorWith_CanChangeAdministratorsFieldTrue()
        {
            //Arrange
            AssignedAdministratorToEducationalInstitutionIntegrationEvent @event = new()
            {
                EducationalInstitutionId = Guid.NewGuid(),
                Message = "test message",
                Permissions = new List<string>(1) { DefinedUserPermissions.EducationalInstitutionPermissions.All },
                TriggeredBy = new RabbitMQEventBus.IntegrationEvents.EventTrigger() { Action = "test", ServiceName = "Identity.API" },
                Uri = string.Empty,
                UserId = Guid.NewGuid()
            };

            User user = new()
            {
                Id = @event.UserId.ToString(),
                Email = "test@test.com",
                FirstName = "T F",
                LastName = "T L"
            };

            List<EducationalInstitutionAdministrator> fakeDatabase = new();

            userManagerMock.Setup(umm => umm.FindByIdAsync(@event.UserId.ToString())).ReturnsAsync(user);
            identityRepositoryMock.Setup(irm => irm.AddEducationalInstitutionAdministratorAsync(It.IsAny<EducationalInstitutionAdministrator>()))
                                  .Callback<EducationalInstitutionAdministrator>(a => fakeDatabase.Add(a));

            //Act
            await eventHandler.HandleEvent(@event);

            //Assert
            Assert.True(fakeDatabase.FirstOrDefault().CanChangeAdministrators);
        }

        [Fact]
        public async Task GivenAnAssignedAdministratorToEducationalInstitutionIntegrationEvent_WithDeletePermission_ShouldCreateAnEducationalInstitutionAdministratorWith_CanRemoveEducationalInstitutionFieldTrue()
        {
            //Arrange
            AssignedAdministratorToEducationalInstitutionIntegrationEvent @event = new()
            {
                EducationalInstitutionId = Guid.NewGuid(),
                Message = "test message",
                Permissions = new List<string>(1) { DefinedUserPermissions.EducationalInstitutionPermissions.Delete },
                TriggeredBy = new RabbitMQEventBus.IntegrationEvents.EventTrigger() { Action = "test", ServiceName = "Identity.API" },
                Uri = string.Empty,
                UserId = Guid.NewGuid()
            };

            User user = new()
            {
                Id = @event.UserId.ToString(),
                Email = "test@test.com",
                FirstName = "T F",
                LastName = "T L"
            };

            List<EducationalInstitutionAdministrator> fakeDatabase = new();

            userManagerMock.Setup(umm => umm.FindByIdAsync(@event.UserId.ToString())).ReturnsAsync(user);
            identityRepositoryMock.Setup(irm => irm.AddEducationalInstitutionAdministratorAsync(It.IsAny<EducationalInstitutionAdministrator>()))
                                  .Callback<EducationalInstitutionAdministrator>(a => fakeDatabase.Add(a));

            //Act
            await eventHandler.HandleEvent(@event);

            //Assert
            Assert.True(fakeDatabase.FirstOrDefault().CanRemoveEducationalInstitution);
        }

        [Fact]
        public async Task GivenAnAssignedAdministratorToEducationalInstitutionIntegrationEvent_WithDeletePermission_ShouldCreateAnEducationalInstitutionAdministratorWith_CanUpdateEducationalInstitutionDetailsFieldFalse()
        {
            //Arrange
            AssignedAdministratorToEducationalInstitutionIntegrationEvent @event = new()
            {
                EducationalInstitutionId = Guid.NewGuid(),
                Message = "test message",
                Permissions = new List<string>(1) { DefinedUserPermissions.EducationalInstitutionPermissions.Delete },
                TriggeredBy = new RabbitMQEventBus.IntegrationEvents.EventTrigger() { Action = "test", ServiceName = "Identity.API" },
                Uri = string.Empty,
                UserId = Guid.NewGuid()
            };

            User user = new()
            {
                Id = @event.UserId.ToString(),
                Email = "test@test.com",
                FirstName = "T F",
                LastName = "T L"
            };

            List<EducationalInstitutionAdministrator> fakeDatabase = new();

            userManagerMock.Setup(umm => umm.FindByIdAsync(@event.UserId.ToString())).ReturnsAsync(user);
            identityRepositoryMock.Setup(irm => irm.AddEducationalInstitutionAdministratorAsync(It.IsAny<EducationalInstitutionAdministrator>()))
                                  .Callback<EducationalInstitutionAdministrator>(a => fakeDatabase.Add(a));

            //Act
            await eventHandler.HandleEvent(@event);

            //Assert
            Assert.False(fakeDatabase.FirstOrDefault().CanUpdateEducationalInstitutionDetails);
        }

        [Fact]
        public async Task GivenAnAssignedAdministratorToEducationalInstitutionIntegrationEvent_WithDeletePermission_ShouldCreateAnEducationalInstitutionAdministratorWith_CanChangeAdministratorsFieldFalse()
        {
            //Arrange
            AssignedAdministratorToEducationalInstitutionIntegrationEvent @event = new()
            {
                EducationalInstitutionId = Guid.NewGuid(),
                Message = "test message",
                Permissions = new List<string>(1) { DefinedUserPermissions.EducationalInstitutionPermissions.Delete },
                TriggeredBy = new RabbitMQEventBus.IntegrationEvents.EventTrigger() { Action = "test", ServiceName = "Identity.API" },
                Uri = string.Empty,
                UserId = Guid.NewGuid()
            };

            User user = new()
            {
                Id = @event.UserId.ToString(),
                Email = "test@test.com",
                FirstName = "T F",
                LastName = "T L"
            };

            List<EducationalInstitutionAdministrator> fakeDatabase = new();

            userManagerMock.Setup(umm => umm.FindByIdAsync(@event.UserId.ToString())).ReturnsAsync(user);
            identityRepositoryMock.Setup(irm => irm.AddEducationalInstitutionAdministratorAsync(It.IsAny<EducationalInstitutionAdministrator>()))
                                  .Callback<EducationalInstitutionAdministrator>(a => fakeDatabase.Add(a));

            //Act
            await eventHandler.HandleEvent(@event);

            //Assert
            Assert.False(fakeDatabase.FirstOrDefault().CanChangeAdministrators);
        }

        [Fact]
        public async Task GivenAnAssignedAdministratorToEducationalInstitutionIntegrationEvent_WithChangeAdministratorsPermission_ShouldCreateAnEducationalInstitutionAdministratorWith_CanChangeAdministratorsFieldTrue()
        {
            //Arrange
            AssignedAdministratorToEducationalInstitutionIntegrationEvent @event = new()
            {
                EducationalInstitutionId = Guid.NewGuid(),
                Message = "test message",
                Permissions = new List<string>(1) { DefinedUserPermissions.EducationalInstitutionPermissions.ChangeAdministrators },
                TriggeredBy = new RabbitMQEventBus.IntegrationEvents.EventTrigger() { Action = "test", ServiceName = "Identity.API" },
                Uri = string.Empty,
                UserId = Guid.NewGuid()
            };

            User user = new()
            {
                Id = @event.UserId.ToString(),
                Email = "test@test.com",
                FirstName = "T F",
                LastName = "T L"
            };

            List<EducationalInstitutionAdministrator> fakeDatabase = new();

            userManagerMock.Setup(umm => umm.FindByIdAsync(@event.UserId.ToString())).ReturnsAsync(user);
            identityRepositoryMock.Setup(irm => irm.AddEducationalInstitutionAdministratorAsync(It.IsAny<EducationalInstitutionAdministrator>()))
                                  .Callback<EducationalInstitutionAdministrator>(a => fakeDatabase.Add(a));

            //Act
            await eventHandler.HandleEvent(@event);

            //Assert
            Assert.True(fakeDatabase.FirstOrDefault().CanChangeAdministrators);
        }

        [Fact]
        public async Task GivenAnAssignedAdministratorToEducationalInstitutionIntegrationEvent_WithChangeAdministratorsPermission_ShouldCreateAnEducationalInstitutionAdministratorWith_CanRemoveEducationalInstitutionFieldFalse()
        {
            //Arrange
            AssignedAdministratorToEducationalInstitutionIntegrationEvent @event = new()
            {
                EducationalInstitutionId = Guid.NewGuid(),
                Message = "test message",
                Permissions = new List<string>(1) { DefinedUserPermissions.EducationalInstitutionPermissions.ChangeAdministrators },
                TriggeredBy = new RabbitMQEventBus.IntegrationEvents.EventTrigger() { Action = "test", ServiceName = "Identity.API" },
                Uri = string.Empty,
                UserId = Guid.NewGuid()
            };

            User user = new()
            {
                Id = @event.UserId.ToString(),
                Email = "test@test.com",
                FirstName = "T F",
                LastName = "T L"
            };

            List<EducationalInstitutionAdministrator> fakeDatabase = new();

            userManagerMock.Setup(umm => umm.FindByIdAsync(@event.UserId.ToString())).ReturnsAsync(user);
            identityRepositoryMock.Setup(irm => irm.AddEducationalInstitutionAdministratorAsync(It.IsAny<EducationalInstitutionAdministrator>()))
                                  .Callback<EducationalInstitutionAdministrator>(a => fakeDatabase.Add(a));

            //Act
            await eventHandler.HandleEvent(@event);

            //Assert
            Assert.False(fakeDatabase.FirstOrDefault().CanRemoveEducationalInstitution);
        }

        [Fact]
        public async Task GivenAnAssignedAdministratorToEducationalInstitutionIntegrationEvent_WithChangeAdministratorsPermission_ShouldCreateAnEducationalInstitutionAdministratorWith_CanUpdateEducationalInstitutionDetailsFieldFalse()
        {
            //Arrange
            AssignedAdministratorToEducationalInstitutionIntegrationEvent @event = new()
            {
                EducationalInstitutionId = Guid.NewGuid(),
                Message = "test message",
                Permissions = new List<string>(1) { DefinedUserPermissions.EducationalInstitutionPermissions.ChangeAdministrators },
                TriggeredBy = new RabbitMQEventBus.IntegrationEvents.EventTrigger() { Action = "test", ServiceName = "Identity.API" },
                Uri = string.Empty,
                UserId = Guid.NewGuid()
            };

            User user = new()
            {
                Id = @event.UserId.ToString(),
                Email = "test@test.com",
                FirstName = "T F",
                LastName = "T L"
            };

            List<EducationalInstitutionAdministrator> fakeDatabase = new();

            userManagerMock.Setup(umm => umm.FindByIdAsync(@event.UserId.ToString())).ReturnsAsync(user);
            identityRepositoryMock.Setup(irm => irm.AddEducationalInstitutionAdministratorAsync(It.IsAny<EducationalInstitutionAdministrator>()))
                                  .Callback<EducationalInstitutionAdministrator>(a => fakeDatabase.Add(a));

            //Act
            await eventHandler.HandleEvent(@event);

            //Assert
            Assert.False(fakeDatabase.FirstOrDefault().CanUpdateEducationalInstitutionDetails);
        }

        [Fact]
        public async Task GivenAnAssignedAdministratorToEducationalInstitutionIntegrationEvent_WithUpdateDetailsPermission_ShouldCreateAnEducationalInstitutionAdministratorWith_CanUpdateEducationalInstitutionDetailsFieldTrue()
        {
            //Arrange
            AssignedAdministratorToEducationalInstitutionIntegrationEvent @event = new()
            {
                EducationalInstitutionId = Guid.NewGuid(),
                Message = "test message",
                Permissions = new List<string>(1) { DefinedUserPermissions.EducationalInstitutionPermissions.UpdateDetails },
                TriggeredBy = new RabbitMQEventBus.IntegrationEvents.EventTrigger() { Action = "test", ServiceName = "Identity.API" },
                Uri = string.Empty,
                UserId = Guid.NewGuid()
            };

            User user = new()
            {
                Id = @event.UserId.ToString(),
                Email = "test@test.com",
                FirstName = "T F",
                LastName = "T L"
            };

            List<EducationalInstitutionAdministrator> fakeDatabase = new();

            userManagerMock.Setup(umm => umm.FindByIdAsync(@event.UserId.ToString())).ReturnsAsync(user);
            identityRepositoryMock.Setup(irm => irm.AddEducationalInstitutionAdministratorAsync(It.IsAny<EducationalInstitutionAdministrator>()))
                                  .Callback<EducationalInstitutionAdministrator>(a => fakeDatabase.Add(a));

            //Act
            await eventHandler.HandleEvent(@event);

            //Assert
            Assert.True(fakeDatabase.FirstOrDefault().CanUpdateEducationalInstitutionDetails);
        }

        [Fact]
        public async Task GivenAnAssignedAdministratorToEducationalInstitutionIntegrationEvent_WithUpdateDetailsPermission_ShouldCreateAnEducationalInstitutionAdministratorWith_CanChangeAdministratorsFieldFalse()
        {
            //Arrange
            AssignedAdministratorToEducationalInstitutionIntegrationEvent @event = new()
            {
                EducationalInstitutionId = Guid.NewGuid(),
                Message = "test message",
                Permissions = new List<string>(1) { DefinedUserPermissions.EducationalInstitutionPermissions.UpdateDetails },
                TriggeredBy = new RabbitMQEventBus.IntegrationEvents.EventTrigger() { Action = "test", ServiceName = "Identity.API" },
                Uri = string.Empty,
                UserId = Guid.NewGuid()
            };

            User user = new()
            {
                Id = @event.UserId.ToString(),
                Email = "test@test.com",
                FirstName = "T F",
                LastName = "T L"
            };

            List<EducationalInstitutionAdministrator> fakeDatabase = new();

            userManagerMock.Setup(umm => umm.FindByIdAsync(@event.UserId.ToString())).ReturnsAsync(user);
            identityRepositoryMock.Setup(irm => irm.AddEducationalInstitutionAdministratorAsync(It.IsAny<EducationalInstitutionAdministrator>()))
                                  .Callback<EducationalInstitutionAdministrator>(a => fakeDatabase.Add(a));

            //Act
            await eventHandler.HandleEvent(@event);

            //Assert
            Assert.False(fakeDatabase.FirstOrDefault().CanChangeAdministrators);
        }

        [Fact]
        public async Task GivenAnAssignedAdministratorToEducationalInstitutionIntegrationEvent_WithUpdateDetailsPermission_ShouldCreateAnEducationalInstitutionAdministratorWith_CanRemoveEducationalInstitutionFieldFalse()
        {
            //Arrange
            AssignedAdministratorToEducationalInstitutionIntegrationEvent @event = new()
            {
                EducationalInstitutionId = Guid.NewGuid(),
                Message = "test message",
                Permissions = new List<string>(1) { DefinedUserPermissions.EducationalInstitutionPermissions.UpdateDetails },
                TriggeredBy = new RabbitMQEventBus.IntegrationEvents.EventTrigger() { Action = "test", ServiceName = "Identity.API" },
                Uri = string.Empty,
                UserId = Guid.NewGuid()
            };

            User user = new()
            {
                Id = @event.UserId.ToString(),
                Email = "test@test.com",
                FirstName = "T F",
                LastName = "T L"
            };

            List<EducationalInstitutionAdministrator> fakeDatabase = new();

            userManagerMock.Setup(umm => umm.FindByIdAsync(@event.UserId.ToString())).ReturnsAsync(user);
            identityRepositoryMock.Setup(irm => irm.AddEducationalInstitutionAdministratorAsync(It.IsAny<EducationalInstitutionAdministrator>()))
                                  .Callback<EducationalInstitutionAdministrator>(a => fakeDatabase.Add(a));

            //Act
            await eventHandler.HandleEvent(@event);

            //Assert
            Assert.False(fakeDatabase.FirstOrDefault().CanRemoveEducationalInstitution);
        }
    }
}