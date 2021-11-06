using EducationalInstitution.API.Tests.Shared;
using EducationalInstitution.Application.Commands;
using EducationalInstitution.Application.Commands.Handlers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Entity = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.API.UnitTests.Application_Tests.Commands_Tests.Handlers_Tests.Disable
{
    public class CommandRequestHandlerBaseTests : DisableEducationalInstitutionCommandHandler, IClassFixture<MockDependenciesHelper<DisableEducationalInstitutionCommandHandler>>,
                                                                                             IClassFixture<TestDataFromJSONParser>
    {
        private readonly TestDataFromJSONParser testDataHelper;
        private readonly MockDependenciesHelper<DisableEducationalInstitutionCommandHandler> dependenciesHelper;

        public CommandRequestHandlerBaseTests(MockDependenciesHelper<DisableEducationalInstitutionCommandHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        : base(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockLogger.Object)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
        }

        [Fact]
        public async Task GivenADisableEducationalInstitutionCommand_ToTransactionOperationsMethod_TheEducationalInstitutionIsNotFound_ShouldReturnNullData()
        {
            //Arrange
            var request = new DisableEducationalInstitutionCommand { EducationalInstitutionID = Guid.NewGuid() };

            SetupMockedDependenciesToNotFindTheEntity();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GivenADisableEducationalInstitutionCommand_ToTransactionOperationsMethod_TheEducationalInstitutionIsNotFound_ShouldReturnOperationStatusFalse()
        {
            //Arrange
            var request = new DisableEducationalInstitutionCommand { EducationalInstitutionID = Guid.NewGuid() };

            SetupMockedDependenciesToNotFindTheEntity();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenADisableEducationalInstitutionCommand_ToTransactionOperationsMethod_TheEducationalInstitutionIsNotFound_ShouldReturnHttpStatusCodeNotFound()
        {
            //Arrange
            var request = new DisableEducationalInstitutionCommand { EducationalInstitutionID = Guid.NewGuid() };

            SetupMockedDependenciesToNotFindTheEntity();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GivenADisableEducationalInstitutionCommand_ToTransactionOperationsMethod_TheEducationalInstitutionIsNotFound_ShouldReturnExpectedMessage()
        {
            //Arrange
            var request = new DisableEducationalInstitutionCommand { EducationalInstitutionID = Guid.NewGuid() };

            SetupMockedDependenciesToNotFindTheEntity();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal($"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!", result.Message);
        }

        private void SetupMockedDependenciesToNotFindTheEntity()
        {
            dependenciesHelper.mockUnitOfWorkCommand.Setup(muowc => muowc.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(meicr => meicr.GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync((Entity::EducationalInstitution)default);
        }

        [Fact]
        public async Task GivenADisableEducationalInstitutionCommand_ToTransactionOperationsMethod_TheEducationalInstitutionIsNotFound_ShouldReturnExpectedDate()
        {
            //Arrange
            var request = new DisableEducationalInstitutionCommand { EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id };

            var expectedDeleteDate = DateTime.UtcNow.Date.AddDays(30);

            SetupMockedDependencies(testDataHelper.EducationalInstitutions[0]);

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal(expectedDeleteDate.Month, result.Data.DateForPermanentDeletion.Month);
            Assert.Equal(expectedDeleteDate.Day, result.Data.DateForPermanentDeletion.Day);
            Assert.Equal(expectedDeleteDate.Year, result.Data.DateForPermanentDeletion.Year);
        }

        [Fact]
        public async Task GivenADisableEducationalInstitutionCommand_ToTransactionOperationsMethod_TheEducationalInstitutionIsNotFound_ShouldReturnOperationStatusTrue()
        {
            //Arrange
            var request = new DisableEducationalInstitutionCommand { EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id };

            SetupMockedDependencies(testDataHelper.EducationalInstitutions[0]);

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenADisableEducationalInstitutionCommand_ToTransactionOperationsMethod_TheEducationalInstitutionIsNotFound_ShouldReturnHttpStatusCodeAccepted()
        {
            //Arrange
            var request = new DisableEducationalInstitutionCommand { EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id };

            SetupMockedDependencies(testDataHelper.EducationalInstitutions[0]);

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal(HttpStatusCode.Accepted, result.StatusCode);
        }

        [Fact]
        public async Task GivenADisableEducationalInstitutionCommand_ToTransactionOperationsMethod_TheEducationalInstitutionIsNotFound_ShouldReturnEmptyMessage()
        {
            //Arrange
            var request = new DisableEducationalInstitutionCommand { EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id };

            SetupMockedDependencies(testDataHelper.EducationalInstitutions[0]);

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Empty(result.Message);
        }

        private void SetupMockedDependencies(Entity::EducationalInstitution expectedEntity)
        {
            dependenciesHelper.mockUnitOfWorkCommand.Setup(muowc => muowc.UsingEducationalInstitutionCommandRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(meicr => meicr.GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync(expectedEntity);
        }
    }
}