using EducationalInstitution.API.Tests.Shared;
using EducationalInstitution.Application;
using EducationalInstitution.Application.Commands;
using EducationalInstitution.Application.Commands.Handlers;
using Moq;
using System.Net;
using Xunit;
using Aggregate = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.API.UnitTests.Application_Tests.Commands_Tests.Handlers_Tests.UpdateLocation
{
    public class UpdateLocationCommandRequestHandlerBaseTests : UpdateEducationalInstitutionLocationCommandHandler,
                                                                IClassFixture<MockDependenciesHelper<UpdateEducationalInstitutionLocationCommandHandler>>,
                                                                IClassFixture<TestDataFromJSONParser>
    {
        private readonly TestDataFromJSONParser testDataHelper;
        private readonly MockDependenciesHelper<UpdateEducationalInstitutionLocationCommandHandler> dependenciesHelper;

        /// <remarks>Called before each test</remarks>
        public UpdateLocationCommandRequestHandlerBaseTests(MockDependenciesHelper<UpdateEducationalInstitutionLocationCommandHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        : base(dependenciesHelper.mockUnitOfWorkCommand.Object, dependenciesHelper.mockLogger.Object)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_LocationID_BuildingsIDs_ToTransactionOperationsMethod_ShouldReturnAResponseThatIncludesAStatusCodeNoContentField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).Id };

            UpdateEducationalInstitutionLocationCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateLocation = true,
                LocationID = locationID,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_LocationID_BuildingsIDs_ToTransactionOperationsMethod_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).Id };

            UpdateEducationalInstitutionLocationCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateLocation = true,
                LocationID = locationID,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_LocationID_BuildingsIDs_ToTransactionOperationsMethod_ShouldReturnANonGenericResponse()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).Id };

            UpdateEducationalInstitutionLocationCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateLocation = true,
                LocationID = locationID,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.IsType<Response>(result);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_LocationID_BuildingsIDs_ToTransactionOperationsMethod_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).Id };

            UpdateEducationalInstitutionLocationCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateLocation = true,
                LocationID = locationID,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_LocationID_ToTransactionOperationsMethod_ShouldReturnAResponseThatIncludesAStatusCodeNoContentField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";

            UpdateEducationalInstitutionLocationCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateLocation = true,
                LocationID = locationID,
                UpdateBuildings = false,
                AddBuildingsIDs = default,
                RemoveBuildingsIDs = default
            };

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_LocationID_ToTransactionOperationsMethod_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";

            UpdateEducationalInstitutionLocationCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateLocation = true,
                LocationID = locationID,
                UpdateBuildings = false,
                AddBuildingsIDs = default,
                RemoveBuildingsIDs = default
            };

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_LocationID_ToTransactionOperationsMethod_ShouldReturnANonGenericResponse()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";

            UpdateEducationalInstitutionLocationCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateLocation = true,
                LocationID = locationID,
                UpdateBuildings = false,
                AddBuildingsIDs = default,
                RemoveBuildingsIDs = default
            };

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.IsType<Response>(result);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_LocationID_ToTransactionOperationsMethod_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";

            UpdateEducationalInstitutionLocationCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateLocation = true,
                LocationID = locationID,
                UpdateBuildings = false,
                AddBuildingsIDs = default,
                RemoveBuildingsIDs = default
            };

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_BuildingsIDs_ToTransactionOperationsMethod_ShouldReturnAResponseThatIncludesAStatusCodeNoContentField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).Id };

            UpdateEducationalInstitutionLocationCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateLocation = false,
                LocationID = default,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_BuildingsIDs_ToTransactionOperationsMethod_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).Id };

            UpdateEducationalInstitutionLocationCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateLocation = false,
                LocationID = default,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_BuildingsIDs_ToTransactionOperationsMethod_ShouldReturnANonGenericResponse()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).Id };

            UpdateEducationalInstitutionLocationCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateLocation = false,
                LocationID = default,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };

            SetupMockedDependencies();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.IsType<Response>(result);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_BuildingsIDs_ToTransactionOperationsMethod_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).Id };

            UpdateEducationalInstitutionLocationCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateLocation = false,
                LocationID = default,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
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

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(meicr => meicr.GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_BuildingsIDs_ToTransactionOperationsMethod_IDDoesntExistInDatabase_ShouldReturnExpectedMessage()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).Id };

            UpdateEducationalInstitutionLocationCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateLocation = false,
                LocationID = default,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };

            SetupMockedDependenciesToNotFindTheEntity();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.Equal($"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!", result.Message);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_BuildingsIDs_ToTransactionOperationsMethod_IDDoesntExistInDatabase_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).Id };

            UpdateEducationalInstitutionLocationCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateLocation = false,
                LocationID = default,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
            };

            SetupMockedDependenciesToNotFindTheEntity();

            //Act
            var result = await TransactionOperations(dependenciesHelper.mockTransaction.Object, dependenciesHelper.mockOutboxService.Object, request);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnEducationalInstitutionID_BuildingsIDs_ToTransactionOperationsMethod_IDDoesntExistInDatabase_ShouldReturnStatusCodeNotFound()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id;
            ICollection<string> addBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };
            ICollection<string> removeBuildingsIDs = new List<string>() { testDataHelper.EducationalInstitutions[0].Buildings.ElementAt(0).Id };

            UpdateEducationalInstitutionLocationCommand request = new()
            {
                EducationalInstitutionID = educationalInstitutionID,
                UpdateLocation = false,
                LocationID = default,
                UpdateBuildings = true,
                AddBuildingsIDs = addBuildingsIDs,
                RemoveBuildingsIDs = removeBuildingsIDs
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

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(meicr => meicr.GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                          .ReturnsAsync((Aggregate::EducationalInstitution)default);
        }
    }
}