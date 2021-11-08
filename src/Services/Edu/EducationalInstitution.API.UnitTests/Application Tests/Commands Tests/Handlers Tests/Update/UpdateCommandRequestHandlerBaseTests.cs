using EducationalInstitution.API.Tests.Shared;
using EducationalInstitution.Application;
using EducationalInstitution.Application.Commands;
using EducationalInstitution.Application.Commands.Handlers;
using Moq;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Domain = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.API.UnitTests.Application_Tests.Commands_Tests.Handlers_Tests.Update
{
    public class UpdateCommandRequestHandlerBaseTests : UpdateEducationalInstitutionCommandHandler,
                                                        IClassFixture<MockDependenciesHelper<UpdateEducationalInstitutionCommandHandler>>,
                                                        IClassFixture<TestDataFromJSONParser>
    {
        private readonly TestDataFromJSONParser testDataHelper;
        private readonly MockDependenciesHelper<UpdateEducationalInstitutionCommandHandler> dependenciesHelper;

        /// <remarks>Called before each test</remarks>
        public UpdateCommandRequestHandlerBaseTests(MockDependenciesHelper<UpdateEducationalInstitutionCommandHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        : base(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockLogger.Object)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
        }

        #region An entity exists for the input ID TESTS

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithNameDescription_ToTransactionOperationsMethod_ShouldReturnAResponseThatIncludesAStatusCodeNoContentField()
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

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithNameDescription_ToTransactionOperationsMethod_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
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

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithNameDescription_ToTransactionOperationsMethod_ShouldReturnAResponseThatIncludesATrueOperationStatusField()
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

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithNameDescription_ToTransactionOperationsMethod_ShouldReturnANonGenericTypeResponse()
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

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.IsType<Response>(result);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithName_ToTransactionOperationsMethod_ShouldReturnAResponseThatIncludesAStatusCodeNoContentField()
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

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithName_ToTransactionOperationsMethod_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
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

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithName_ToTransactionOperationsMethod_ShouldReturnAResponseThatIncludesATrueOperationStatusField()
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

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithName_ToTransactionOperationsMethod_ShouldReturnANonGenericTypeResponse()
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

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.IsType<Response>(result);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithDescription_ToTransactionOperationsMethod_ShouldReturnAResponseThatIncludesAStatusCodeNoContentField()
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

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithDescription_ToTransactionOperationsMethod_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
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

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithDescription_ToTransactionOperationsMethod_ShouldReturnAResponseThatIncludesATrueOperationStatusField()
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

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithDescription_ToTransactionOperationsMethod_ShouldReturnANonGenericTypeResponse()
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

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.IsType<Response>(result);
        }

        private void SetupMockedDependencies()
        {
            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(meicr => meicr.GetEducationalInstitutionIncludingAdminsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);
        }

        #endregion An entity exists for the input ID TESTS

        #region No entity is found for the input ID TESTS

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithNameDescription_ToTransactionOperationsMethod_IDDoesntExistInDatabase_ShouldReturnFalseOperationStatus()
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

            SetupMockedDependenciesToNotFindTheEntity();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithNameDescription_ToTransactionOperationsMethod_IDDoesntExistInDatabase_ShouldReturnStatusCodeNotFound()
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

            SetupMockedDependenciesToNotFindTheEntity();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithNameDescription_ToTransactionOperationsMethod_IDDoesntExistInDatabase_ShouldReturnExpectedMessage()
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

            SetupMockedDependenciesToNotFindTheEntity();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal($"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!", result.Message);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithName_ToTransactionOperationsMethod_IDDoesntExistInDatabase_ShouldReturnFalseOperationStatus()
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

            SetupMockedDependenciesToNotFindTheEntity();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithName_ToTransactionOperationsMethod_IDDoesntExistInDatabase_ShouldReturnStatusCodeNotFound()
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

            SetupMockedDependenciesToNotFindTheEntity();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithName_ToTransactionOperationsMethod_IDDoesntExistInDatabase_ShouldReturnExpectedMessage()
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

            SetupMockedDependenciesToNotFindTheEntity();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal($"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!", result.Message);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithDescription_ToTransactionOperationsMethod_IDDoesntExistInDatabase_ShouldReturnExpectedMessage()
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

            SetupMockedDependenciesToNotFindTheEntity();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal($"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!", result.Message);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithDescription_ToTransactionOperationsMethod_IDDoesntExistInDatabase_ShouldReturnFalseOperationStatus()
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

            SetupMockedDependenciesToNotFindTheEntity();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidUpdateEducationalInstitutionCommand_WithDescription_ToTransactionOperationsMethod_IDDoesntExistInDatabase_ShouldReturnStatusCodeNotFound()
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

            SetupMockedDependenciesToNotFindTheEntity();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        private void SetupMockedDependenciesToNotFindTheEntity()
        {
            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                     .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(meicr => meicr.GetEducationalInstitutionIncludingAdminsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync((Domain::EducationalInstitution)default);
        }

        #endregion No entity is found for the input ID TESTS
    }
}