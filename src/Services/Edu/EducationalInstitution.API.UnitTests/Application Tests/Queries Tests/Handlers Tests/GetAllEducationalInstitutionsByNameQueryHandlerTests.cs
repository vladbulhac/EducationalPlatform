using EducationalInstitution.API.Tests.Shared;
using EducationalInstitution.Application;
using EducationalInstitution.Application.Queries;
using EducationalInstitution.Application.Queries.Handlers;
using EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.UnitTests.Application_Tests.Queries_Tests.Handlers_Tests
{
    public class GetAllEducationalInstitutionsByNameQueryHandlerTests : IClassFixture<MockDependenciesHelper<GetAllEducationalInstitutionsByNameQueryHandler>>,
                                                                    IClassFixture<TestDataFromJSONParser>
    {
        private readonly TestDataFromJSONParser testDataHelper;
        private readonly IList<GetEducationalInstitutionQueryResult> queryResult;
        private readonly MockDependenciesHelper<GetAllEducationalInstitutionsByNameQueryHandler> dependenciesHelper;

        /// <remarks>Called before each test</remarks>
        public GetAllEducationalInstitutionsByNameQueryHandlerTests(MockDependenciesHelper<GetAllEducationalInstitutionsByNameQueryHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
            queryResult = new List<GetEducationalInstitutionQueryResult>(2) {
                new() {
                EducationalInstitutionID=testDataHelper.EducationalInstitutions[0].Id,
                Name = testDataHelper.EducationalInstitutions[0].Name,
                Description = testDataHelper.EducationalInstitutions[0].Description,
                LocationID = testDataHelper.EducationalInstitutions[0].LocationID
                },
                new(){
                EducationalInstitutionID=testDataHelper.EducationalInstitutions[1].Id,
                Name = testDataHelper.EducationalInstitutions[1].Name,
                Description = testDataHelper.EducationalInstitutions[1].Description,
                LocationID = testDataHelper.EducationalInstitutions[1].LocationID
                }
            };
        }

        #region One object contains the input string TESTS

        [Fact]
        public async Task GivenName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludes_DataWithTheEducationalInstitutionThatHasThatName()
        {
            //Arrange
            string name = "University of Testing";
            int offsetValue = 0;
            int resultsCount = 1;

            GetAllEducationalInstitutionsByNameQuery request = new()
            {
                Name = name,
                ResultsCount = resultsCount,
                OffsetValue = offsetValue
            };

            GetAllEducationalInstitutionsByNameQueryResult expectedQueryResult = new()
            { EducationalInstitutions = new List<GetEducationalInstitutionQueryResult>() { queryResult[0] } };

            SetupMockedDependencies(expectedQueryResult);

            GetAllEducationalInstitutionsByNameQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(queryResult[0], result.Data.EducationalInstitutions.Single());
        }

        [Fact]
        public async Task GivenName_OffsetValue_ResultsCount_ShouldReturnARecordTypeResponse()
        {
            //Arrange
            string name = "University";
            int offsetValue = 0;
            int resultsCount = 1;

            GetAllEducationalInstitutionsByNameQuery request = new()
            {
                Name = name,
                ResultsCount = resultsCount,
                OffsetValue = offsetValue
            };

            GetAllEducationalInstitutionsByNameQueryResult expectedQueryResult = new()
            { EducationalInstitutions = new List<GetEducationalInstitutionQueryResult>() { queryResult[0] } };

            SetupMockedDependencies(expectedQueryResult);

            GetAllEducationalInstitutionsByNameQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.IsType<Response<GetAllEducationalInstitutionsByNameQueryResult>>(result);
        }

        [Fact]
        public async Task GivenName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            string name = "University of Testing";
            int offsetValue = 0;
            int resultsCount = 1;

            GetAllEducationalInstitutionsByNameQuery request = new()
            {
                Name = name,
                ResultsCount = resultsCount,
                OffsetValue = offsetValue
            };

            GetAllEducationalInstitutionsByNameQueryResult expectedQueryResult = new()
            { EducationalInstitutions = new List<GetEducationalInstitutionQueryResult>() { queryResult[0] } };

            SetupMockedDependencies(expectedQueryResult);

            GetAllEducationalInstitutionsByNameQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesAHttpStatusCodeOkField()
        {
            //Arrange
            string name = "University of Testing";
            int offsetValue = 0;
            int resultsCount = 1;

            GetAllEducationalInstitutionsByNameQuery request = new()
            {
                Name = name,
                ResultsCount = resultsCount,
                OffsetValue = offsetValue
            };

            GetAllEducationalInstitutionsByNameQueryResult expectedQueryResult = new()
            { EducationalInstitutions = new List<GetEducationalInstitutionQueryResult>() { queryResult[0] } };

            SetupMockedDependencies(expectedQueryResult);

            GetAllEducationalInstitutionsByNameQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task GivenName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            string name = "University of Testing";
            int offsetValue = 0;
            int resultsCount = 1;

            GetAllEducationalInstitutionsByNameQuery request = new()
            {
                Name = name,
                ResultsCount = resultsCount,
                OffsetValue = offsetValue
            };

            GetAllEducationalInstitutionsByNameQueryResult expectedQueryResult = new()
            { EducationalInstitutions = new List<GetEducationalInstitutionQueryResult>() { queryResult[0] } };

            SetupMockedDependencies(expectedQueryResult);

            GetAllEducationalInstitutionsByNameQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.True(result.OperationStatus);
        }

        private void SetupMockedDependencies(GetAllEducationalInstitutionsByNameQueryResult expectedQueryResult)
        {
            dependenciesHelper.mockUnitOfWorkQuery.Setup(uok => uok.UsingEducationalInstitutionQueryRepository())
                                                  .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);

            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetAllByNameAsync(It.IsAny<string>(),
                                                                                                            It.IsInRange(0, 150, Moq.Range.Inclusive),
                                                                                                            It.IsInRange(0, 100, Moq.Range.Inclusive),
                                                                                                            It.IsAny<CancellationToken>()))
                                                                         .ReturnsAsync(expectedQueryResult);
        }

        #endregion One object contains the input string TESTS

        #region No object exists that contains the input string TESTS

        private void SetupMockedDependencies_ToNotFindAnyEntity(GetAllEducationalInstitutionsByNameQueryResult expectedQueryResult)
        {
            dependenciesHelper.mockUnitOfWorkQuery.Setup(uok => uok.UsingEducationalInstitutionQueryRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);

            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetAllByNameAsync(It.IsNotIn("University", "of", "Testing", "One"),
                                                                                                            It.IsInRange(0, 150, Moq.Range.Inclusive),
                                                                                                            It.IsInRange(0, 100, Moq.Range.Inclusive),
                                                                                                            It.IsAny<CancellationToken>()))
                                                                        .ReturnsAsync(expectedQueryResult);
        }

        [Fact]
        public async Task GivenNonExistentName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesAHttpStatusCodeNotFoundField()
        {
            //Arrange
            string name = "School";
            int offsetValue = 0;
            int resultsCount = 1;

            GetAllEducationalInstitutionsByNameQuery request = new()
            {
                Name = name,
                ResultsCount = resultsCount,
                OffsetValue = offsetValue
            };

            GetAllEducationalInstitutionsByNameQueryResult expectedQueryResult = new() { EducationalInstitutions = new List<GetEducationalInstitutionQueryResult>() };
            SetupMockedDependencies_ToNotFindAnyEntity(expectedQueryResult);

            GetAllEducationalInstitutionsByNameQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GivenNonExistentName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesAnOperationStatusFalseField()
        {
            //Arrange
            string name = "School";
            int offsetValue = 0;
            int resultsCount = 1;

            GetAllEducationalInstitutionsByNameQuery request = new()
            {
                Name = name,
                ResultsCount = resultsCount,
                OffsetValue = offsetValue
            };

            GetAllEducationalInstitutionsByNameQueryResult expectedQueryResult = new() { EducationalInstitutions = new List<GetEducationalInstitutionQueryResult>() };
            SetupMockedDependencies_ToNotFindAnyEntity(expectedQueryResult);

            GetAllEducationalInstitutionsByNameQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenNonExistentName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesANullDataField()
        {
            //Arrange
            string name = "School";
            int offsetValue = 0;
            int resultsCount = 1;

            GetAllEducationalInstitutionsByNameQuery request = new()
            {
                Name = name,
                ResultsCount = resultsCount,
                OffsetValue = offsetValue
            };

            GetAllEducationalInstitutionsByNameQueryResult expectedQueryResult = new() { EducationalInstitutions = new List<GetEducationalInstitutionQueryResult>() };
            SetupMockedDependencies_ToNotFindAnyEntity(expectedQueryResult);

            GetAllEducationalInstitutionsByNameQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GivenNonExistentName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesAMessageField()
        {
            //Arrange
            string name = "School";
            int offsetValue = 0;
            int resultsCount = 1;

            GetAllEducationalInstitutionsByNameQuery request = new()
            {
                Name = name,
                ResultsCount = resultsCount,
                OffsetValue = offsetValue
            };

            GetAllEducationalInstitutionsByNameQueryResult expectedQueryResult = new() { EducationalInstitutions = new List<GetEducationalInstitutionQueryResult>() };
            SetupMockedDependencies_ToNotFindAnyEntity(expectedQueryResult);

            GetAllEducationalInstitutionsByNameQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal($"Could not find any Educational Institution with a name like: {request.Name}!", result.Message);
        }

        #endregion No object exists that contains the input string TESTS

        #region Null arguments TESTS

        [Fact]
        public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            GetAllEducationalInstitutionsByNameQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null));
        }

        [Fact]
        public void GivenANullArgumentRepositoryToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new GetAllEducationalInstitutionsByNameQueryHandler(null, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new GetAllEducationalInstitutionsByNameQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, null));
        }

        #endregion Null arguments TESTS
    }
}