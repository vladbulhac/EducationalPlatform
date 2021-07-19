using EducationalInstitution.API.Tests.Shared;
using EducationalInstitution.Application;
using EducationalInstitution.Application.Commands;
using EducationalInstitution.Application.Commands.Handlers;
using EducationalInstitution.Infrastructure.Repositories.Command_Repository.Results;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.UnitTests.Application_Tests.Commands_Tests.Handlers_Tests
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
                AddAdminsIDs = new List<Guid>() { Guid.NewGuid() },
                RemoveAdminsIDs = new List<Guid>() { Guid.NewGuid() }
            };

            var expectedUniOfWorkResponse = new AfterUpdateAdminsCommandChangesDetails(
                                                                               testDataHelper.EducationalInstitutions[0].Admins.Select(a => a.AdminID).ToList(),
                                                                               request.AddAdminsIDs,
                                                                               request.RemoveAdminsIDs);

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uokc => uokc.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(eicr => eicr.UpdateAdminsAsync(It.IsAny<Guid>(), It.IsAny<ICollection<Guid>>(), It.IsAny<ICollection<Guid>>(), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync(expectedUniOfWorkResponse);

            var handler = new UpdateEducationalInstitutionAdminsCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                              dependenciesHelper.mockEventBus.Object,
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
                AddAdminsIDs = new List<Guid>() { Guid.NewGuid() },
                RemoveAdminsIDs = new List<Guid>() { Guid.NewGuid() }
            };

            var expectedUniOfWorkResponse = new AfterUpdateAdminsCommandChangesDetails(
                                                                               testDataHelper.EducationalInstitutions[0].Admins.Select(a => a.AdminID).ToList(),
                                                                               request.AddAdminsIDs,
                                                                               request.RemoveAdminsIDs);

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uokc => uokc.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(eicr => eicr.UpdateAdminsAsync(It.IsAny<Guid>(), It.IsAny<ICollection<Guid>>(), It.IsAny<ICollection<Guid>>(), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync(expectedUniOfWorkResponse);

            var handler = new UpdateEducationalInstitutionAdminsCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                              dependenciesHelper.mockEventBus.Object,
                                                                              dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionAdminsCommand_EntityIsNotFound_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            UpdateEducationalInstitutionAdminsCommand request = new()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<Guid>() { Guid.NewGuid() },
                RemoveAdminsIDs = new List<Guid>() { Guid.NewGuid() }
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uokc => uokc.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(eicr => eicr.UpdateAdminsAsync(It.IsAny<Guid>(), It.IsAny<ICollection<Guid>>(), It.IsAny<ICollection<Guid>>(), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync((AfterUpdateAdminsCommandChangesDetails)default);

            var handler = new UpdateEducationalInstitutionAdminsCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                              dependenciesHelper.mockEventBus.Object,
                                                                              dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionAdminsCommand_EntityIsNotFound_ShouldReturnStatusCodeNotFound()
        {
            //Arrange
            UpdateEducationalInstitutionAdminsCommand request = new()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<Guid>() { Guid.NewGuid() },
                RemoveAdminsIDs = new List<Guid>() { Guid.NewGuid() }
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uokc => uokc.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(eicr => eicr.UpdateAdminsAsync(It.IsAny<Guid>(), It.IsAny<ICollection<Guid>>(), It.IsAny<ICollection<Guid>>(), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync((AfterUpdateAdminsCommandChangesDetails)default);

            var handler = new UpdateEducationalInstitutionAdminsCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                              dependenciesHelper.mockEventBus.Object,
                                                                              dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionAdminsCommand_EntityIsNotFound_ShouldReturnMessage()
        {
            //Arrange
            UpdateEducationalInstitutionAdminsCommand request = new()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<Guid>() { Guid.NewGuid() },
                RemoveAdminsIDs = new List<Guid>() { Guid.NewGuid() }
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uokc => uokc.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(eicr => eicr.UpdateAdminsAsync(It.IsAny<Guid>(), It.IsAny<ICollection<Guid>>(), It.IsAny<ICollection<Guid>>(), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync((AfterUpdateAdminsCommandChangesDetails)default);

            var handler = new UpdateEducationalInstitutionAdminsCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                              dependenciesHelper.mockEventBus.Object,
                                                                              dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal($"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!", result.Message);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionAdminsCommand_ShouldReturnTrueOperationStatus()
        {
            //Arrange
            UpdateEducationalInstitutionAdminsCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
                AddAdminsIDs = new List<Guid>() { Guid.NewGuid() },
                RemoveAdminsIDs = new List<Guid>() { Guid.NewGuid() }
            };

            var expectedUniOfWorkResponse = new AfterUpdateAdminsCommandChangesDetails(
                                                                               testDataHelper.EducationalInstitutions[0].Admins.Select(a => a.AdminID).ToList(),
                                                                               request.AddAdminsIDs,
                                                                               request.RemoveAdminsIDs);

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uokc => uokc.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(eicr => eicr.UpdateAdminsAsync(It.IsAny<Guid>(), It.IsAny<ICollection<Guid>>(), It.IsAny<ICollection<Guid>>(), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync(expectedUniOfWorkResponse);

            var handler = new UpdateEducationalInstitutionAdminsCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                              dependenciesHelper.mockEventBus.Object,
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
                AddAdminsIDs = new List<Guid>() { Guid.NewGuid() },
                RemoveAdminsIDs = new List<Guid>() { Guid.NewGuid() }
            };

            var expectedUniOfWorkResponse = new AfterUpdateAdminsCommandChangesDetails(
                                                                               testDataHelper.EducationalInstitutions[0].Admins.Select(a => a.AdminID).ToList(),
                                                                               request.AddAdminsIDs,
                                                                               request.RemoveAdminsIDs);

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uokc => uokc.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(eicr => eicr.UpdateAdminsAsync(It.IsAny<Guid>(), It.IsAny<ICollection<Guid>>(), It.IsAny<ICollection<Guid>>(), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync(expectedUniOfWorkResponse);

            var handler = new UpdateEducationalInstitutionAdminsCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                              dependenciesHelper.mockEventBus.Object,
                                                                              dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            UpdateEducationalInstitutionAdminsCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null));
        }

        [Fact]
        public void GivenANullArgumentRepositoryToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionAdminsCommandHandler(null, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentEventBusToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionAdminsCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, null, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionAdminsCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, null));
        }
    }
}