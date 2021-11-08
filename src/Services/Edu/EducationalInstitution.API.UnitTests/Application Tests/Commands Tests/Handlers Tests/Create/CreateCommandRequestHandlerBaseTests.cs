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
using Domain = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.API.UnitTests.Application_Tests.Commands_Tests.Handlers_Tests.Create
{
    public class CreateCommandRequestHandlerBaseTests : CreateEducationalInstitutionCommandHandler, IClassFixture<MockDependenciesHelper<CreateEducationalInstitutionCommandHandler>>,
                                                                                                    IClassFixture<TestDataFromJSONParser>
    {
        private readonly TestDataFromJSONParser testDataHelper;
        private readonly MockDependenciesHelper<CreateEducationalInstitutionCommandHandler> dependenciesHelper;

        public CreateCommandRequestHandlerBaseTests(MockDependenciesHelper<CreateEducationalInstitutionCommandHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
            : base(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockLogger.Object)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
        }

        [Fact]
        public async Task GivenACreateEducationalInstitutionCommand_ToTransactionOperationsMethod_ShouldReturnHttpStatusCodeCreated()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var request = new CreateEducationalInstitutionCommand
            {
                Name = educationalInstitution.Name,
                Description = educationalInstitution.Description,
                LocationID = educationalInstitution.LocationID,
                BuildingsIDs = new string[1] { "test_building" },
                AdminId = "test_admin_id",
                ParentInstitutionID = Guid.NewGuid()
            };

            SetupMockedDependencies(educationalInstitution.ParentInstitution);

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task GivenACreateEducationalInstitutionCommand_ToTransactionOperationsMethod_ShouldReturnOperationStatusTrue()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var request = new CreateEducationalInstitutionCommand
            {
                Name = educationalInstitution.Name,
                Description = educationalInstitution.Description,
                LocationID = educationalInstitution.LocationID,
                BuildingsIDs = new string[1] { "test_building" },
                AdminId = "test_admin_id",
                ParentInstitutionID = Guid.NewGuid()
            };

            SetupMockedDependencies(educationalInstitution.ParentInstitution);

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenACreateEducationalInstitutionCommand_ToTransactionOperationsMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var request = new CreateEducationalInstitutionCommand
            {
                Name = educationalInstitution.Name,
                Description = educationalInstitution.Description,
                LocationID = educationalInstitution.LocationID,
                BuildingsIDs = new string[1] { "test_building" },
                AdminId = "test_admin_id",
                ParentInstitutionID = Guid.NewGuid()
            };

            SetupMockedDependencies(educationalInstitution.ParentInstitution);

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenACreateEducationalInstitutionCommand_ToTransactionOperationsMethod_ShouldReturnAGuidId()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var request = new CreateEducationalInstitutionCommand
            {
                Name = educationalInstitution.Name,
                Description = educationalInstitution.Description,
                LocationID = educationalInstitution.LocationID,
                BuildingsIDs = new string[1] { "test_building" },
                AdminId = "test_admin_id",
                ParentInstitutionID = Guid.NewGuid()
            };

            SetupMockedDependencies(educationalInstitution.ParentInstitution);

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.NotEqual(Guid.Empty, result.Data.EducationalInstitutionID);
        }

        private void SetupMockedDependencies(Domain::EducationalInstitution parentInstitution)
        {
            dependenciesHelper.mockUnitOfWorkCommand.Setup(muokc => muokc.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(meicr => meicr.GetEducationalInstitutionIncludingAdminsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync(parentInstitution);
        }

        [Fact]
        public async Task GivenACreateEducationalInstitutionCommandWithNotFoundParentInstitution_ToTransactionOperationsMethod_ShouldReturnHttpStatusCodeMultiStatus()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var request = new CreateEducationalInstitutionCommand
            {
                Name = educationalInstitution.Name,
                Description = educationalInstitution.Description,
                LocationID = educationalInstitution.LocationID,
                BuildingsIDs = new string[1] { "test_building" },
                AdminId = "test_admin_id",
                ParentInstitutionID = Guid.NewGuid()
            };

            SetupMockedDependenciesWithNotFoundParentInstitution();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal(HttpStatusCode.MultiStatus, result.StatusCode);
        }

        [Fact]
        public async Task GivenACreateEducationalInstitutionCommandWithNotFoundParentInstitution_ToTransactionOperationsMethod_ShouldReturnOperationStatusTrue()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var request = new CreateEducationalInstitutionCommand
            {
                Name = educationalInstitution.Name,
                Description = educationalInstitution.Description,
                LocationID = educationalInstitution.LocationID,
                BuildingsIDs = new string[1] { "test_building" },
                AdminId = "test_admin_id",
                ParentInstitutionID = Guid.NewGuid()
            };

            SetupMockedDependenciesWithNotFoundParentInstitution();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenACreateEducationalInstitutionCommandWithNotFoundParentInstitution_ToTransactionOperationsMethod_ShouldReturnMessage()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var request = new CreateEducationalInstitutionCommand
            {
                Name = educationalInstitution.Name,
                Description = educationalInstitution.Description,
                LocationID = educationalInstitution.LocationID,
                BuildingsIDs = new string[1] { "test_building" },
                AdminId = "test_admin_id",
                ParentInstitutionID = Guid.NewGuid()
            };

            SetupMockedDependenciesWithNotFoundParentInstitution();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal($"The Educational Institution has been successfully created but the Parent Institution with the following ID: {request.ParentInstitutionID} has not been found!", result.Message);
        }

        private void SetupMockedDependenciesWithNotFoundParentInstitution()
        {
            dependenciesHelper.mockUnitOfWorkCommand.Setup(muokc => muokc.UsingEducationalInstitutionCommandRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(meicr => meicr.GetEducationalInstitutionIncludingAdminsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync((Domain::EducationalInstitution)default);
        }
    }
}