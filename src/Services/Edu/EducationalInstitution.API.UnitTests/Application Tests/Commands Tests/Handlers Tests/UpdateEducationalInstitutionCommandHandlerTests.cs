using EducationalInstitution.API.Tests.Shared;
using EducationalInstitution.Application;
using EducationalInstitution.Application.Commands;
using EducationalInstitution.Application.Commands.Handlers;
using EducationalInstitution.Infrastructure.Repositories.Command_Repository.Results;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.UnitTests.Application_Tests.Commands_Tests.Handlers_Tests
{
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

        #region An entity exists for the input ID TESTS

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithNameDescription_ShouldReturnAResponseThatIncludesAStatusCodeNoContentField()
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

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository
                                .Setup(r => r.UpdateNameAndDescriptionAsync(educationalInstitutionID, name, description, It.IsAny<CancellationToken>()))
                                .ReturnsAsync(new AfterCommandChangesDetails(new List<Guid>(1) { Guid.NewGuid() }));

            var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithNameDescription_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
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

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                     .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository
                                .Setup(r => r.UpdateNameAndDescriptionAsync(educationalInstitutionID, name, description, It.IsAny<CancellationToken>()))
                                .ReturnsAsync(new AfterCommandChangesDetails(new List<Guid>(1) { Guid.NewGuid() }));

            var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithNameDescription_ShouldReturnAResponseThatIncludesATrueOperationStatusField()
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

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository
                                .Setup(r => r.UpdateNameAndDescriptionAsync(educationalInstitutionID, name, description, It.IsAny<CancellationToken>()))
                                .ReturnsAsync(new AfterCommandChangesDetails(new List<Guid>(1) { Guid.NewGuid() }));

            var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithNameDescription_ShouldReturnANonGenericTypeResponse()
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

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository
                                .Setup(r => r.UpdateNameAndDescriptionAsync(educationalInstitutionID, name, description, It.IsAny<CancellationToken>()))
                                .ReturnsAsync(new AfterCommandChangesDetails(new List<Guid>(1) { Guid.NewGuid() }));

            var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.IsType<Response>(result);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithName_ShouldReturnAResponseThatIncludesAStatusCodeNoContentField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            string name = "New_Name";

            UpdateEducationalInstitutionCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateName = true,
                Name = name,
                UpdateDescription = false,
                Description = string.Empty
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository
                                .Setup(r => r.UpdateNameAsync(educationalInstitutionID, name, It.IsAny<CancellationToken>()))
                                .ReturnsAsync(new AfterCommandChangesDetails(new List<Guid>(1) { Guid.NewGuid() }));

            var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithName_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            string name = "New_Name";

            UpdateEducationalInstitutionCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateName = true,
                Name = name,
                UpdateDescription = false,
                Description = string.Empty
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository
                                .Setup(r => r.UpdateNameAsync(educationalInstitutionID, name, It.IsAny<CancellationToken>()))
                                .ReturnsAsync(new AfterCommandChangesDetails(new List<Guid>(1) { Guid.NewGuid() }));

            var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithName_ShouldReturnAResponseThatIncludesATrueOperationStatusField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            string name = "New_Name";

            UpdateEducationalInstitutionCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateName = true,
                Name = name,
                UpdateDescription = false,
                Description = string.Empty
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository
                                .Setup(r => r.UpdateNameAsync(educationalInstitutionID, name, It.IsAny<CancellationToken>()))
                                .ReturnsAsync(new AfterCommandChangesDetails(new List<Guid>(1) { Guid.NewGuid() }));

            var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithName_ShouldReturnANonGenericTypeResponse()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            string name = "New_Name";

            UpdateEducationalInstitutionCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateName = true,
                Name = name,
                UpdateDescription = false,
                Description = string.Empty
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository
                                .Setup(r => r.UpdateNameAsync(educationalInstitutionID, name, It.IsAny<CancellationToken>()))
                                .ReturnsAsync(new AfterCommandChangesDetails(new List<Guid>(1) { Guid.NewGuid() }));

            var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.IsType<Response>(result);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithDescription_ShouldReturnAResponseThatIncludesAStatusCodeNoContentField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            string description = "New_Description";

            UpdateEducationalInstitutionCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateName = false,
                Name = string.Empty,
                UpdateDescription = true,
                Description = description
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository
                                .Setup(r => r.UpdateDescriptionAsync(educationalInstitutionID, description, It.IsAny<CancellationToken>()))
                                .ReturnsAsync(new AfterCommandChangesDetails(new List<Guid>(1) { Guid.NewGuid() }));

            var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithDescription_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            string description = "New_Description";

            UpdateEducationalInstitutionCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateName = false,
                Name = string.Empty,
                UpdateDescription = true,
                Description = description
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                     .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository
                                .Setup(r => r.UpdateDescriptionAsync(educationalInstitutionID, description, It.IsAny<CancellationToken>()))
                                .ReturnsAsync(new AfterCommandChangesDetails(new List<Guid>(1) { Guid.NewGuid() }));

            var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithDescription_ShouldReturnAResponseThatIncludesATrueOperationStatusField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            string description = "New_Description";

            UpdateEducationalInstitutionCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateName = false,
                Name = string.Empty,
                UpdateDescription = true,
                Description = description
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository
                                .Setup(r => r.UpdateDescriptionAsync(educationalInstitutionID, description, It.IsAny<CancellationToken>()))
                                .ReturnsAsync(new AfterCommandChangesDetails(new List<Guid>(1) { Guid.NewGuid() }));

            var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithDescription_ShouldReturnANonGenericTypeResponse()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            string description = "New_Description";

            UpdateEducationalInstitutionCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateName = false,
                Name = string.Empty,
                UpdateDescription = true,
                Description = description
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository
                                .Setup(r => r.UpdateDescriptionAsync(educationalInstitutionID, description, It.IsAny<CancellationToken>()))
                                .ReturnsAsync(new AfterCommandChangesDetails(new List<Guid>(1) { Guid.NewGuid() }));

            var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.IsType<Response>(result);
        }

        #endregion An entity exists for the input ID TESTS

        #region No entity is found for the input ID TESTS

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithNameDescription_IDDoesntExistInDatabase_ShouldReturnFalseOperationStatus()
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

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository
                                .Setup(r => r.UpdateNameAndDescriptionAsync(educationalInstitutionID, name, description, It.IsAny<CancellationToken>()))
                                .Returns(Task.FromResult<AfterCommandChangesDetails>(default));

            var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithNameDescription_IDDoesntExistInDatabase_ShouldReturnStatusCodeNotFound()
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

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository
                                .Setup(r => r.UpdateNameAndDescriptionAsync(educationalInstitutionID, name, description, It.IsAny<CancellationToken>()))
                                .Returns(Task.FromResult<AfterCommandChangesDetails>(default));

            var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithNameDescription_IDDoesntExistInDatabase_ShouldReturnExpectedMessage()
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

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository
                                .Setup(r => r.UpdateNameAndDescriptionAsync(educationalInstitutionID, name, description, It.IsAny<CancellationToken>()))
                                .Returns(Task.FromResult<AfterCommandChangesDetails>(default));

            var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal($"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!", result.Message);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithName_IDDoesntExistInDatabase_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            string name = "New_Name";

            UpdateEducationalInstitutionCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateName = true,
                Name = name,
                UpdateDescription = false,
                Description = string.Empty
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository
                                .Setup(r => r.UpdateNameAsync(educationalInstitutionID, name, It.IsAny<CancellationToken>()))
                                .Returns(Task.FromResult<AfterCommandChangesDetails>(default));

            var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithName_IDDoesntExistInDatabase_ShouldReturnStatusCodeNotFound()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            string name = "New_Name";

            UpdateEducationalInstitutionCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateName = true,
                Name = name,
                UpdateDescription = false,
                Description = string.Empty
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository
                                .Setup(r => r.UpdateNameAsync(educationalInstitutionID, name, It.IsAny<CancellationToken>()))
                                .Returns(Task.FromResult<AfterCommandChangesDetails>(default));

            var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithName_IDDoesntExistInDatabase_ShouldReturnExpectedMessage()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            string name = "New_Name";

            UpdateEducationalInstitutionCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateName = true,
                Name = name,
                UpdateDescription = false,
                Description = string.Empty
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository
                                .Setup(r => r.UpdateNameAsync(educationalInstitutionID, name, It.IsAny<CancellationToken>()))
                                .Returns(Task.FromResult<AfterCommandChangesDetails>(default));

            var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal($"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!", result.Message);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithDescription_IDDoesntExistInDatabase_ShouldReturnExpectedMessage()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            string description = "New_Description";

            UpdateEducationalInstitutionCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateName = false,
                Name = string.Empty,
                UpdateDescription = true,
                Description = description
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository
                                .Setup(r => r.UpdateDescriptionAsync(educationalInstitutionID, description, It.IsAny<CancellationToken>()))
                                .Returns(Task.FromResult<AfterCommandChangesDetails>(default));

            var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal($"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!", result.Message);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithDescription_IDDoesntExistInDatabase_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            string description = "New_Description";

            UpdateEducationalInstitutionCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateName = false,
                Name = string.Empty,
                UpdateDescription = true,
                Description = description
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository
                                .Setup(r => r.UpdateDescriptionAsync(educationalInstitutionID, description, It.IsAny<CancellationToken>()))
                                .Returns(Task.FromResult<AfterCommandChangesDetails>(default));

            var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithDescription_IDDoesntExistInDatabase_ShouldReturnStatusCodeNotFound()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            string description = "New_Description";

            UpdateEducationalInstitutionCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateName = false,
                Name = string.Empty,
                UpdateDescription = true,
                Description = description
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository
                                .Setup(r => r.UpdateDescriptionAsync(educationalInstitutionID, description, It.IsAny<CancellationToken>()))
                                .Returns(Task.FromResult<AfterCommandChangesDetails>(default));

            var handler = new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        #endregion No entity is found for the input ID TESTS

        [Fact]
        public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            UpdateEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null));
        }

        [Fact]
        public void GivenANullArgumentRepositoryToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionCommandHandler(null, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentEventBusToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, null, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, null));
        }
    }
}