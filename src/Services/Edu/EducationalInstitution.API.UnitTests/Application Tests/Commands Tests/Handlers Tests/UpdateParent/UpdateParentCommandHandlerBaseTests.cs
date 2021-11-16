using EducationalInstitution.API.Tests.Shared;
using EducationalInstitution.Application.Commands;
using EducationalInstitution.Application.Commands.Handlers;
using Moq;
using System.Net;
using Xunit;
using Domain = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.API.UnitTests.Application_Tests.Commands_Tests.Handlers_Tests.UpdateParent
{
    public class UpdateParentCommandHandlerBaseTests : UpdateEducationalInstitutionParentCommandHandler, IClassFixture<MockDependenciesHelper<UpdateEducationalInstitutionParentCommandHandler>>,
                                                                                                         IClassFixture<TestDataFromJSONParser>
    {
        private readonly MockDependenciesHelper<UpdateEducationalInstitutionParentCommandHandler> dependenciesHelper;
        private readonly TestDataFromJSONParser testDataHelper;

        /// <remarks>Called before each test</remarks>
        public UpdateParentCommandHandlerBaseTests(MockDependenciesHelper<UpdateEducationalInstitutionParentCommandHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        : base(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockLogger.Object)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
        }

        [Fact]
        public async Task GivenAnUpdateEducationalInstitutionParentCommand_ToTransactionOperationsMethod_ShouldReturnAResponseThatIncludesAStatusCodeNoContentField()
        {
            //Arrange
            UpdateEducationalInstitutionParentCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
                ParentInstitutionID = testDataHelper.EducationalInstitutions[1].Id
            };

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnUpdateEducationalInstitutionParentCommand_ToTransactionOperationsMethod_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            UpdateEducationalInstitutionParentCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
                ParentInstitutionID = testDataHelper.EducationalInstitutions[1].Id
            };

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnUpdateEducationalInstitutionParentCommand_ToTransactionOperationsMethod_ShouldReturnAResponseThatIncludesATrueOperationStatusField()
        {
            //Arrange
            UpdateEducationalInstitutionParentCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
                ParentInstitutionID = testDataHelper.EducationalInstitutions[1].Id
            };

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.True(result.OperationStatus);
        }

        private void SetupMockedDependencies()
        {
            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.SetupSequence(r => r.GetEducationalInstitutionIncludingAdminsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync(testDataHelper.EducationalInstitutions[1])
                                                                          .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);
        }

        [Fact]
        public async Task GivenAnUpdateEducationalInstitutionParentCommand_ToTransactionOperationsMethod_TheParentIDIsNotInDatabase_ShouldReturnAResponseThatIncludesAStatusCodeNotFoundField()
        {
            //Arrange
            UpdateEducationalInstitutionParentCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
                ParentInstitutionID = Guid.NewGuid()
            };

            SetupMockedDependenciesToNotFindTheParent(request.ParentInstitutionID);

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnUpdateEducationalInstitutionParentCommand_ToTransactionOperationsMethod_TheParentIDIsNotInDatabase_ShouldReturnAResponseThatIncludesAMessageField()
        {
            //Arrange
            UpdateEducationalInstitutionParentCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
                ParentInstitutionID = Guid.NewGuid()
            };

            SetupMockedDependenciesToNotFindTheParent(request.ParentInstitutionID);

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal($"The Parent Educational Institution with the ID: {request.ParentInstitutionID} has not been found!", result.Message);
        }

        [Fact]
        public async Task GivenAnUpdateEducationalInstitutionParentCommand_ToTransactionOperationsMethod_TheParentIDIsNotInDatabase_ShouldReturnAResponseThatIncludesAFalseOperationStatusField()
        {
            //Arrange
            UpdateEducationalInstitutionParentCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id,
                ParentInstitutionID = Guid.NewGuid()
            };

            SetupMockedDependenciesToNotFindTheParent(request.ParentInstitutionID);

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.False(result.OperationStatus);
        }

        private void SetupMockedDependenciesToNotFindTheParent(Guid parentId)
        {
            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(r => r.GetEducationalInstitutionIncludingAdminsAsync(parentId, It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync((Domain::EducationalInstitution)default);
        }

        [Fact]
        public async Task GivenAnUpdateEducationalInstitutionParentCommand_ToTransactionOperationsMethod_TheEducationalInstitutionIsNotInDatabase_ShouldReturnAResponseThatIncludesAMessageField()
        {
            //Arrange
            UpdateEducationalInstitutionParentCommand request = new()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                ParentInstitutionID = testDataHelper.EducationalInstitutions[1].Id
            };

            SetupMockedDependenciesToNotFindTheEducationalInstitution(request.EducationalInstitutionID, request.ParentInstitutionID);

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal($"The Educational Institution with the ID: {request.EducationalInstitutionID} has not been found!", result.Message);
        }

        [Fact]
        public async Task GivenAnUpdateEducationalInstitutionParentCommand_ToTransactionOperationsMethod_TheEducationalInstitutionIsNotInDatabase_ShouldReturnAResponseThatIncludesAFalseOperationStatusField()
        {
            //Arrange
            UpdateEducationalInstitutionParentCommand request = new()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                ParentInstitutionID = testDataHelper.EducationalInstitutions[1].Id
            };

            SetupMockedDependenciesToNotFindTheEducationalInstitution(request.EducationalInstitutionID, request.ParentInstitutionID);

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnUpdateEducationalInstitutionParentCommand_ToTransactionOperationsMethod_TheEducationalInstitutionIsNotInDatabase_ShouldReturnAResponseThatIncludesANotFoundStatusCodeField()
        {
            //Arrange
            UpdateEducationalInstitutionParentCommand request = new()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                ParentInstitutionID = testDataHelper.EducationalInstitutions[1].Id
            };

            SetupMockedDependenciesToNotFindTheEducationalInstitution(request.EducationalInstitutionID, request.ParentInstitutionID);

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        private void SetupMockedDependenciesToNotFindTheEducationalInstitution(Guid educationalInstitutionId, Guid parentId)
        {
            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.SetupSequence(r => r.GetEducationalInstitutionIncludingAdminsAsync(It.IsIn(parentId, educationalInstitutionId), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync(testDataHelper.EducationalInstitutions[1])
                                                                          .ReturnsAsync((Domain::EducationalInstitution)default);
        }
    }
}