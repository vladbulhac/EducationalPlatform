using EducationaInstitutionAPI.Business.Queries.OnEducationalInstitution;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.In;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using EducationaInstitutionAPI.Utils;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.Tests.UnitTests.BusinessTests.QueriesTests.EducationalInstitutionTests
{
    public class GetEducationalInstitutionByNameQueryHandlerTests : IClassFixture<MockDependenciesHelper<GetEducationalInstitutionByNameQueryHandler>>, IClassFixture<TestDataFromJSONParser>
    {
        private readonly MockDependenciesHelper<GetEducationalInstitutionByNameQueryHandler> dependenciesHelper;
        private readonly TestDataFromJSONParser testDataHelper;
        private readonly IList<GetEducationalInstitutionQueryResult> queryResult;

        public GetEducationalInstitutionByNameQueryHandlerTests(MockDependenciesHelper<GetEducationalInstitutionByNameQueryHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
            queryResult = new List<GetEducationalInstitutionQueryResult>(2) {
                new() {
                EduInstitutionID=testDataHelper.EduInstitutions[0].EduInstitutionID,
                Name = testDataHelper.EduInstitutions[0].Name,
                Description = testDataHelper.EduInstitutions[0].Description,
                LocationID = testDataHelper.EduInstitutions[0].LocationID,
                BuildingID = testDataHelper.EduInstitutions[0].BuildingID
                },
                new(){
                EduInstitutionID=testDataHelper.EduInstitutions[1].EduInstitutionID,
                Name = testDataHelper.EduInstitutions[1].Name,
                Description = testDataHelper.EduInstitutions[1].Description,
                LocationID = testDataHelper.EduInstitutions[1].LocationID,
                BuildingID = testDataHelper.EduInstitutions[1].BuildingID
                }
            };
        }

        #region One object contains the input string TESTS

        #region Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's ResponseObject field to be an Educational Institution with Name that contains the input string

        [Fact]
        public async Task GivenName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludes_AResponseObjectWithTheEducationalInstitutionThatHasThatName()
        {
            //Arrange
            string name = "University of Testing";
            int offsetValue = 0;
            int resultsCount = 1;

            DTOEducationalInstitutionsByNameQuery request = new()
            {
                Name = name,
                ResultsCount = resultsCount,
                OffsetValue = offsetValue
            };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(request.Name, It.IsInRange(0, 150, Moq.Range.Inclusive), It.IsInRange(0, 100, Moq.Range.Inclusive), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(new List<GetEducationalInstitutionQueryResult>() { queryResult[0] });

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(queryResult[0], result.ResponseObject.Single());
        }

        #endregion Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's ResponseObject field to be an Educational Institution with Name that contains the input string

        #region Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result to be of type record Response<ICollection<GetEducationalInstitutionQueryResult>>

        [Fact]
        public async Task GivenName_OffsetValue_ResultsCount_ShouldReturnARecordTypeResponse()
        {
            //Arrange
            string name = "University";
            int offsetValue = 0;
            int resultsCount = 1;

            DTOEducationalInstitutionsByNameQuery request = new()
            {
                Name = name,
                ResultsCount = resultsCount,
                OffsetValue = offsetValue
            };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(request.Name, It.IsInRange(0, 150, Moq.Range.Inclusive), It.IsInRange(0, 100, Moq.Range.Inclusive), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(new List<GetEducationalInstitutionQueryResult>() { queryResult[0] });

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<Response<ICollection<GetEducationalInstitutionQueryResult>>>(result);
        }

        #endregion Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result to be of type record Response<ICollection<GetEducationalInstitutionQueryResult>>

        #region Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's Message field to be empty

        [Fact]
        public async Task GivenName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            string name = "University of Testing";
            int offsetValue = 0;
            int resultsCount = 1;

            DTOEducationalInstitutionsByNameQuery request = new()
            {
                Name = name,
                ResultsCount = resultsCount,
                OffsetValue = offsetValue
            };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(request.Name, It.IsInRange(0, 150, Moq.Range.Inclusive), It.IsInRange(0, 100, Moq.Range.Inclusive), dependenciesHelper.cancellationToken))
                                     .ReturnsAsync(new List<GetEducationalInstitutionQueryResult>() { queryResult[0] });

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Empty(result.Message);
        }

        #endregion Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's Message field to be empty

        #region Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's StatusCode field to be equal HTTPStatusCode.OK

        [Fact]
        public async Task GivenName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesAHttpStatusCodeOkField()
        {
            //Arrange
            string name = "University of Testing";
            int offsetValue = 0;
            int resultsCount = 1;

            DTOEducationalInstitutionsByNameQuery request = new()
            {
                Name = name,
                ResultsCount = resultsCount,
                OffsetValue = offsetValue
            };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(request.Name, It.IsInRange(0, 150, Moq.Range.Inclusive), It.IsInRange(0, 100, Moq.Range.Inclusive), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(new List<GetEducationalInstitutionQueryResult>() { queryResult[0] });

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        #endregion Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's StatusCode field to be equal HTTPStatusCode.OK

        #region Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's OperationStatus field to be true

        [Fact]
        public async Task GivenName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            string name = "University of Testing";
            int offsetValue = 0;
            int resultsCount = 1;

            DTOEducationalInstitutionsByNameQuery request = new()
            {
                Name = name,
                ResultsCount = resultsCount,
                OffsetValue = offsetValue
            };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(request.Name, It.IsInRange(0, 150, Moq.Range.Inclusive), It.IsInRange(0, 100, Moq.Range.Inclusive), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(new List<GetEducationalInstitutionQueryResult>() { queryResult[0] });

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.True(result.OperationStatus);
        }

        #endregion Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's OperationStatus field to be true

        #endregion One object contains the input string TESTS

        #region No object exists that contains the input string TESTS

        #region Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's StatusCode field to be equal HTTPStatusCode.NotFound

        [Fact]
        public async Task GivenNonExistentName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesAHttpStatusCodeNotFoundField()
        {
            //Arrange
            string name = "School";
            int offsetValue = 0;
            int resultsCount = 1;

            DTOEducationalInstitutionsByNameQuery request = new()
            {
                Name = name,
                ResultsCount = resultsCount,
                OffsetValue = offsetValue
            };

            ICollection<GetEducationalInstitutionQueryResult> repositoryTaskResult = null;
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(It.IsNotIn(new string[] { "University", "of", "Testing", "One" }), It.IsInRange(0, 150, Moq.Range.Inclusive), It.IsInRange(0, 100, Moq.Range.Inclusive), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(repositoryTaskResult);

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        #endregion Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's StatusCode field to be equal HTTPStatusCode.NotFound

        #region Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's OperationStatus field to be false

        [Fact]
        public async Task GivenNonExistentName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesAnOperationStatusFalseField()
        {
            //Arrange
            string name = "School";
            int offsetValue = 0;
            int resultsCount = 1;

            DTOEducationalInstitutionsByNameQuery request = new()
            {
                Name = name,
                ResultsCount = resultsCount,
                OffsetValue = offsetValue
            };

            ICollection<GetEducationalInstitutionQueryResult> repositoryTaskResult = null;
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(It.IsNotIn<string>(new string[] { "University", "of", "Testing", "One" }), It.IsInRange(0, 150, Moq.Range.Inclusive), It.IsInRange(0, 100, Moq.Range.Inclusive), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(repositoryTaskResult);

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.False(result.OperationStatus);
        }

        #endregion Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's OperationStatus field to be false

        #region Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's ResponseObject field to be null

        [Fact]
        public async Task GivenNonExistentName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesANullResponseObjectField()
        {
            //Arrange
            string name = "School";
            int offsetValue = 0;
            int resultsCount = 1;

            DTOEducationalInstitutionsByNameQuery request = new()
            {
                Name = name,
                ResultsCount = resultsCount,
                OffsetValue = offsetValue
            };

            ICollection<GetEducationalInstitutionQueryResult> repositoryTaskResult = null;
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(It.IsNotIn(new string[] { "University", "of", "Testing", "One" }), It.IsInRange(0, 150, Moq.Range.Inclusive), It.IsInRange(0, 100, Moq.Range.Inclusive), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(repositoryTaskResult);

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Null(result.ResponseObject);
        }

        #endregion Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's ResponseObject field to be null

        #region Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's Message field to contain a message

        [Fact]
        public async Task GivenNonExistentName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesAMessageField()
        {
            //Arrange
            string name = "School";
            int offsetValue = 0;
            int resultsCount = 1;

            DTOEducationalInstitutionsByNameQuery request = new()
            {
                Name = name,
                ResultsCount = resultsCount,
                OffsetValue = offsetValue
            };

            ICollection<GetEducationalInstitutionQueryResult> repositoryTaskResult = null;
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(It.IsNotIn<string>(new string[] { "University", "of", "Testing", "One" }), It.IsInRange(0, 150, Moq.Range.Inclusive), It.IsInRange(0, 100, Moq.Range.Inclusive), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(repositoryTaskResult);

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal($"Could not find any Educational Institution with a name like: {request.Name}!", result.Message);
        }

        #endregion Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's Message field to contain a message

        #endregion No object exists that contains the input string TESTS

        #region Null arguments TESTS

        [Fact]
        public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, dependenciesHelper.cancellationToken));
        }

        [Fact]
        public void GivenANullArgumentRepositoryToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new GetEducationalInstitutionByNameQueryHandler(null, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new GetEducationalInstitutionByNameQueryHandler(dependenciesHelper.mockRepository.Object, null));
        }

        #endregion Null arguments TESTS
    }
}