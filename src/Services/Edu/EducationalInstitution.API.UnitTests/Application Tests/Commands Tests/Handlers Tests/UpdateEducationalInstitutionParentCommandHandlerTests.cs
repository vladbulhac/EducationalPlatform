using EducationalInstitution.API.Tests.Shared;
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

using Domain = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.API.UnitTests.Application_Tests.Commands_Tests.Handlers_Tests
{
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
        public async Task GivenAValidRequestOfTypeUpdateEducationalInstitutionParentCommand_ShouldReturnAResponseThatIncludesAStatusCodeNoContentField()
        {
            //Arrange
            UpdateEducationalInstitutionParentCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
                ParentInstitutionID = testDataHelper.EducationalInstitutions[1].Id
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.SetupSequence(r => r.GetEducationalInstitutionIncludingAdminsAsync(It.IsIn(request.ParentInstitutionID, request.EducationalInstitutionID), It.IsAny<CancellationToken>()))
                                                                         .ReturnsAsync(testDataHelper.EducationalInstitutions[1])
                                                                         .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

            var handler = new UpdateEducationalInstitutionParentCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                                dependenciesHelper.mockEventBus.Object,
                                                                                dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidRequestOfTypeUpdateEducationalInstitutionParentCommand_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            UpdateEducationalInstitutionParentCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
                ParentInstitutionID = testDataHelper.EducationalInstitutions[1].Id
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.SetupSequence(r => r.GetEducationalInstitutionIncludingAdminsAsync(It.IsIn(request.ParentInstitutionID, request.EducationalInstitutionID), It.IsAny<CancellationToken>()))
                                                                         .ReturnsAsync(testDataHelper.EducationalInstitutions[1])
                                                                         .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

            var handler = new UpdateEducationalInstitutionParentCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                                dependenciesHelper.mockEventBus.Object,
                                                                                dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidRequestOfTypeUpdateEducationalInstitutionParentCommand_ShouldReturnAResponseThatIncludesATrueOperationStatusField()
        {
            //Arrange
            UpdateEducationalInstitutionParentCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
                ParentInstitutionID = testDataHelper.EducationalInstitutions[1].Id
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                     .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.SetupSequence(r => r.GetEducationalInstitutionIncludingAdminsAsync(It.IsIn(request.ParentInstitutionID, request.EducationalInstitutionID), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync(testDataHelper.EducationalInstitutions[1])
                                                                          .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

            var handler = new UpdateEducationalInstitutionParentCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                                dependenciesHelper.mockEventBus.Object,
                                                                                dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidRequestOfTypeUpdateEducationalInstitutionParentCommand_WithParentIDNotInDatabase_ShouldReturnAResponseThatIncludesAStatusCodeNotFoundField()
        {
            //Arrange
            UpdateEducationalInstitutionParentCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
                ParentInstitutionID = Guid.NewGuid()
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(r => r.GetEducationalInstitutionIncludingAdminsAsync(request.ParentInstitutionID, It.IsAny<CancellationToken>()))
                                                            .ReturnsAsync((Domain::EducationalInstitution)default);

            var handler = new UpdateEducationalInstitutionParentCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                                dependenciesHelper.mockEventBus.Object,
                                                                                dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnInvalidRequestOfTypeUpdateEducationalInstitutionParentCommand_WithParentIDNotInDatabase_ShouldReturnAResponseThatIncludesAMessageField()
        {
            //Arrange
            UpdateEducationalInstitutionParentCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
                ParentInstitutionID = Guid.NewGuid()
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(r => r.GetEducationalInstitutionIncludingAdminsAsync(request.ParentInstitutionID, It.IsAny<CancellationToken>()))
                                                           .ReturnsAsync((Domain::EducationalInstitution)default);

            var handler = new UpdateEducationalInstitutionParentCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                                dependenciesHelper.mockEventBus.Object,
                                                                                dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal($"The Parent Educational Institution with the ID: {request.ParentInstitutionID} has not been found!", result.Message);
        }

        [Fact]
        public async Task GivenAnInvalidRequestOfTypeUpdateEducationalInstitutionParentCommand_WithParentIDNotInDatabase_ShouldReturnAResponseThatIncludesAFalseOperationStatusField()
        {
            //Arrange
            UpdateEducationalInstitutionParentCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
                ParentInstitutionID = Guid.NewGuid()
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(r => r.GetEducationalInstitutionIncludingAdminsAsync(request.ParentInstitutionID, It.IsAny<CancellationToken>()))
                                                           .ReturnsAsync((Domain::EducationalInstitution)default);

            var handler = new UpdateEducationalInstitutionParentCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                                dependenciesHelper.mockEventBus.Object,
                                                                                dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidRequestOfTypeUpdateEducationalInstitutionParentCommand_WithEducationalInstitutionNotInDatabase_ShouldReturnAResponseThatIncludesAMessageField()
        {
            //Arrange
            UpdateEducationalInstitutionParentCommand request = new()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                ParentInstitutionID = testDataHelper.EducationalInstitutions[1].Id
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.SetupSequence(r => r.GetEducationalInstitutionIncludingAdminsAsync(It.IsIn(request.ParentInstitutionID, request.EducationalInstitutionID), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync(testDataHelper.EducationalInstitutions[1])
                                                                          .ReturnsAsync((Domain::EducationalInstitution)default);

            var handler = new UpdateEducationalInstitutionParentCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                                dependenciesHelper.mockEventBus.Object,
                                                                                dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal($"The Educational Institution with the ID: {request.EducationalInstitutionID} has not been found!", result.Message);
        }

        [Fact]
        public async Task GivenAnInvalidRequestOfTypeUpdateEducationalInstitutionParentCommand_WithEducationalInstitutionNotInDatabase_ShouldReturnAResponseThatIncludesAFalseOperationStatusField()
        {
            //Arrange
            UpdateEducationalInstitutionParentCommand request = new()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                ParentInstitutionID = testDataHelper.EducationalInstitutions[1].Id
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.SetupSequence(r => r.GetEducationalInstitutionIncludingAdminsAsync(It.IsIn(request.ParentInstitutionID, request.EducationalInstitutionID), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync(testDataHelper.EducationalInstitutions[1])
                                                                          .ReturnsAsync((Domain::EducationalInstitution)default);

            var handler = new UpdateEducationalInstitutionParentCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                                dependenciesHelper.mockEventBus.Object,
                                                                                dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidRequestOfTypeUpdateEducationalInstitutionParentCommand_WithEducationalInstitutionNotInDatabase_ShouldReturnAResponseThatIncludesANotFoundStatusCodeField()
        {
            //Arrange
            UpdateEducationalInstitutionParentCommand request = new()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                ParentInstitutionID = testDataHelper.EducationalInstitutions[1].Id
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.SetupSequence(r => r.GetEducationalInstitutionIncludingAdminsAsync(It.IsIn(request.ParentInstitutionID, request.EducationalInstitutionID), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync(testDataHelper.EducationalInstitutions[1])
                                                                          .ReturnsAsync((Domain::EducationalInstitution)default);

            var handler = new UpdateEducationalInstitutionParentCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                                dependenciesHelper.mockEventBus.Object,
                                                                                dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}