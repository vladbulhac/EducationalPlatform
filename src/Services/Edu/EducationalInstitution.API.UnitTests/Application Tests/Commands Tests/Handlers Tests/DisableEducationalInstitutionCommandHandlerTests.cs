using EducationalInstitution.API.Tests.Shared;
using EducationalInstitution.Application;
using EducationalInstitution.Application.Commands;
using EducationalInstitution.Application.Commands.Handlers;
using EducationalInstitution.Application.Commands.Results;
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

            var expectedCommandResult = new AfterDeleteCommandChangesDetails(DateTime.UtcNow, new List<string>(1) { Guid.NewGuid().ToString() });

            SetupMockedDependencies(expectedCommandResult);

            DisableEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                    dependenciesHelper.mockEventBus.Object,
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

            var expectedCommandResult = new AfterDeleteCommandChangesDetails(DateTime.UtcNow, new List<string>(1) { Guid.NewGuid().ToString() });

            SetupMockedDependencies(expectedCommandResult);
            DisableEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                    dependenciesHelper.mockEventBus.Object,
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

            var expectedCommandResult = new AfterDeleteCommandChangesDetails(DateTime.UtcNow, new List<string>(1) { Guid.NewGuid().ToString() });

            SetupMockedDependencies(expectedCommandResult);

            DisableEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                    dependenciesHelper.mockEventBus.Object,
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

            var expectedCommandResult = new AfterDeleteCommandChangesDetails(DateTime.UtcNow, new List<string>(1) { Guid.NewGuid().ToString() });

            SetupMockedDependencies(expectedCommandResult);

            DisableEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                    dependenciesHelper.mockEventBus.Object,
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

            var expectedCommandResult = new AfterDeleteCommandChangesDetails(DateTime.UtcNow, new List<string>(1) { Guid.NewGuid().ToString() });

            SetupMockedDependencies(expectedCommandResult);

            DisableEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                    dependenciesHelper.mockEventBus.Object,
                                                                    dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(expectedCommandResult.ScheduledDateForDeletion.Date, result.Data.DateForPermanentDeletion.Date);
        }

        [Fact]
        public async Task GivenAnID_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            DisableEducationalInstitutionCommand request = new() { EducationalInstitutionID = educationalInstitutionID };

            var expectedCommandResult = new AfterDeleteCommandChangesDetails(DateTime.UtcNow, new List<string>(1) { Guid.NewGuid().ToString() });

            SetupMockedDependencies(expectedCommandResult);

            DisableEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                    dependenciesHelper.mockEventBus.Object,
                                                                    dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.True(result.OperationStatus);
        }

        private void SetupMockedDependencies(AfterDeleteCommandChangesDetails expectedCommandResult)
        {
            dependenciesHelper.mockUnitOfWorkCommand.Setup(muk => muk.UsingEducationalInstitutionCommandRepository())
                                                            .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(cr => cr.ScheduleForDeletionAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                        .ReturnsAsync(expectedCommandResult);
        }

        #endregion An entity exists for the input ID TESTS

        #region No entity is found for the input ID TESTS

        private void SetupMockedDepenencies()
        {
            dependenciesHelper.mockUnitOfWorkCommand.Setup(muk => muk.UsingEducationalInstitutionCommandRepository())
                                                            .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(cr => cr.ScheduleForDeletionAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                        .ReturnsAsync((AfterDeleteCommandChangesDetails)default);
        }

        [Fact]
        public async Task GivenAnID_ThatDoesntExistInDatabase_ShouldReturnStatusCodeNotFound()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            DisableEducationalInstitutionCommand request = new() { EducationalInstitutionID = educationalInstitutionID };

            SetupMockedDepenencies();

            DisableEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                    dependenciesHelper.mockEventBus.Object,
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

            SetupMockedDepenencies();

            DisableEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                    dependenciesHelper.mockEventBus.Object,
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

            SetupMockedDepenencies();

            DisableEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                    dependenciesHelper.mockEventBus.Object,
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

            SetupMockedDepenencies();

            DisableEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                    dependenciesHelper.mockEventBus.Object,
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
                                                                        dependenciesHelper.mockEventBus.Object,
                                                                        dependenciesHelper.mockLogger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null));
        }

        [Fact]
        public void GivenANullArgumentUnitOfWorkForCommandsToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new DisableEducationalInstitutionCommandHandler(null,
                                                                                                    dependenciesHelper.mockEventBus.Object,
                                                                                                    dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentEventBusToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new DisableEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                                                       null,
                                                                                                       dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new DisableEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                                                        dependenciesHelper.mockEventBus.Object,
                                                                                                        null));
        }
    }
}