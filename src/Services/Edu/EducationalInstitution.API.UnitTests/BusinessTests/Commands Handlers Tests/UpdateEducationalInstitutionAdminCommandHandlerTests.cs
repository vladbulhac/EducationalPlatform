using EducationalInstitutionAPI.Business.Commands_Handlers;
using EducationalInstitutionAPI.Data.Repository_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Commands;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.UnitTests.BusinessTests.Commands_Handlers_Tests
{
    public class UpdateEducationalInstitutionAdminCommandHandlerTests : IClassFixture<MockDependenciesHelper<UpdateEducationalInstitutionAdminCommandHandler>>,
                                                                        IClassFixture<TestDataFromJSONParser>

    {
        private readonly MockDependenciesHelper<UpdateEducationalInstitutionAdminCommandHandler> dependenciesHelper;
        private readonly TestDataFromJSONParser testDataHelper;

        /// <remarks>Called before each test</remarks>
        public UpdateEducationalInstitutionAdminCommandHandlerTests(MockDependenciesHelper<UpdateEducationalInstitutionAdminCommandHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionAdminUpdateCommand_ShouldReturnAResponseType()
        {
            //Arrange
            DTOEducationalInstitutionAdminUpdateCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID,
                AddAdminsIDs = new List<Guid>() { Guid.NewGuid() },
                RemoveAdminsIDs = new List<Guid>() { Guid.NewGuid() }
            };

            var expectedUniOfWorkResponse = new UpdateAdminsCommandRepositoryResult(
                                                                               testDataHelper.EducationalInstitutions[0].Admins.Select(a => a.AdminID).ToList(),
                                                                               request.AddAdminsIDs,
                                                                               request.RemoveAdminsIDs);

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uokc => uokc.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(eicr => eicr.UpdateAdminsAsync(It.IsAny<Guid>(), It.IsAny<ICollection<Guid>>(), It.IsAny<ICollection<Guid>>(), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync(expectedUniOfWorkResponse);

            var handler = new UpdateEducationalInstitutionAdminCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                              dependenciesHelper.mockEventBus.Object,
                                                                              dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.IsType<Response>(result);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionAdminUpdateCommand_ShouldReturnEmptyMessage()
        {
            //Arrange
            DTOEducationalInstitutionAdminUpdateCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID,
                AddAdminsIDs = new List<Guid>() { Guid.NewGuid() },
                RemoveAdminsIDs = new List<Guid>() { Guid.NewGuid() }
            };

            var expectedUniOfWorkResponse = new UpdateAdminsCommandRepositoryResult(
                                                                               testDataHelper.EducationalInstitutions[0].Admins.Select(a => a.AdminID).ToList(),
                                                                               request.AddAdminsIDs,
                                                                               request.RemoveAdminsIDs);

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uokc => uokc.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(eicr => eicr.UpdateAdminsAsync(It.IsAny<Guid>(), It.IsAny<ICollection<Guid>>(), It.IsAny<ICollection<Guid>>(), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync(expectedUniOfWorkResponse);

            var handler = new UpdateEducationalInstitutionAdminCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                              dependenciesHelper.mockEventBus.Object,
                                                                              dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionAdminUpdateCommand_EntityIsNotFound_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            DTOEducationalInstitutionAdminUpdateCommand request = new()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<Guid>() { Guid.NewGuid() },
                RemoveAdminsIDs = new List<Guid>() { Guid.NewGuid() }
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uokc => uokc.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(eicr => eicr.UpdateAdminsAsync(It.IsAny<Guid>(), It.IsAny<ICollection<Guid>>(), It.IsAny<ICollection<Guid>>(), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync((UpdateAdminsCommandRepositoryResult)default);

            var handler = new UpdateEducationalInstitutionAdminCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                              dependenciesHelper.mockEventBus.Object,
                                                                              dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionAdminUpdateCommand_EntityIsNotFound_ShouldReturnStatusCodeNotFound()
        {
            //Arrange
            DTOEducationalInstitutionAdminUpdateCommand request = new()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<Guid>() { Guid.NewGuid() },
                RemoveAdminsIDs = new List<Guid>() { Guid.NewGuid() }
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uokc => uokc.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(eicr => eicr.UpdateAdminsAsync(It.IsAny<Guid>(), It.IsAny<ICollection<Guid>>(), It.IsAny<ICollection<Guid>>(), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync((UpdateAdminsCommandRepositoryResult)default);

            var handler = new UpdateEducationalInstitutionAdminCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                              dependenciesHelper.mockEventBus.Object,
                                                                              dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionAdminUpdateCommand_EntityIsNotFound_ShouldReturnMessage()
        {
            //Arrange
            DTOEducationalInstitutionAdminUpdateCommand request = new()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<Guid>() { Guid.NewGuid() },
                RemoveAdminsIDs = new List<Guid>() { Guid.NewGuid() }
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uokc => uokc.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(eicr => eicr.UpdateAdminsAsync(It.IsAny<Guid>(), It.IsAny<ICollection<Guid>>(), It.IsAny<ICollection<Guid>>(), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync((UpdateAdminsCommandRepositoryResult)default);

            var handler = new UpdateEducationalInstitutionAdminCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                              dependenciesHelper.mockEventBus.Object,
                                                                              dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal($"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!", result.Message);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionAdminUpdateCommand_ShouldReturnTrueOperationStatus()
        {
            //Arrange
            DTOEducationalInstitutionAdminUpdateCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID,
                AddAdminsIDs = new List<Guid>() { Guid.NewGuid() },
                RemoveAdminsIDs = new List<Guid>() { Guid.NewGuid() }
            };

            var expectedUniOfWorkResponse = new UpdateAdminsCommandRepositoryResult(
                                                                               testDataHelper.EducationalInstitutions[0].Admins.Select(a => a.AdminID).ToList(),
                                                                               request.AddAdminsIDs,
                                                                               request.RemoveAdminsIDs);

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uokc => uokc.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(eicr => eicr.UpdateAdminsAsync(It.IsAny<Guid>(), It.IsAny<ICollection<Guid>>(), It.IsAny<ICollection<Guid>>(), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync(expectedUniOfWorkResponse);

            var handler = new UpdateEducationalInstitutionAdminCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                              dependenciesHelper.mockEventBus.Object,
                                                                              dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionAdminUpdateCommand_ShouldReturnStatusCodeNoContent()
        {
            //Arrange
            DTOEducationalInstitutionAdminUpdateCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID,
                AddAdminsIDs = new List<Guid>() { Guid.NewGuid() },
                RemoveAdminsIDs = new List<Guid>() { Guid.NewGuid() }
            };

            var expectedUniOfWorkResponse = new UpdateAdminsCommandRepositoryResult(
                                                                               testDataHelper.EducationalInstitutions[0].Admins.Select(a => a.AdminID).ToList(),
                                                                               request.AddAdminsIDs,
                                                                               request.RemoveAdminsIDs);

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uokc => uokc.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(eicr => eicr.UpdateAdminsAsync(It.IsAny<Guid>(), It.IsAny<ICollection<Guid>>(), It.IsAny<ICollection<Guid>>(), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync(expectedUniOfWorkResponse);

            var handler = new UpdateEducationalInstitutionAdminCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
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
            UpdateEducationalInstitutionAdminCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null));
        }

        [Fact]
        public void GivenANullArgumentRepositoryToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionAdminCommandHandler(null, dependenciesHelper.mockEventBus.Object, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentEventBusToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionAdminCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, null, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionAdminCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockEventBus.Object, null));
        }
    }
}