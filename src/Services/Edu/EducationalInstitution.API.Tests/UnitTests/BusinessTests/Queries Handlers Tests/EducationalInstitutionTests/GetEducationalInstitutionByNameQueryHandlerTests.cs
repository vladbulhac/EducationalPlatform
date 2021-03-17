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
        public void GivenName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludes_AResponseObjectWithTheEducationalInstitutionThatHasThatName()
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
                                    .Returns(Task.FromResult((ICollection<GetEducationalInstitutionQueryResult>)new List<GetEducationalInstitutionQueryResult>() { queryResult[0] }));

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Assert
            Assert.Equal(queryResult[0], result.ResponseObject.Single());
        }

        #endregion Input: string Name | Expect: Educational Institution with Name that contains the input string

        #region Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result to be of type record

        [Fact]
        public void GivenName_OffsetValue_ResultsCount_ShouldReturnARecordTypeResponse()
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
                                    .Returns(Task.FromResult((ICollection<GetEducationalInstitutionQueryResult>)new List<GetEducationalInstitutionQueryResult>() { queryResult[0] }));

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Assert
            Assert.IsType<Response<ICollection<GetEducationalInstitutionQueryResult>>>(result);
        }

        #endregion Input: string Name | Expect: record type object in return

        #region Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's Message field to be empty

        [Fact]
        public void GivenName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
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
                                     .Returns(Task.FromResult((ICollection<GetEducationalInstitutionQueryResult>)new List<GetEducationalInstitutionQueryResult>() { queryResult[0] }));

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Assert
            Assert.Empty(result.Message);
        }

        #endregion Input: string Name | Expect: null Message field

        #region Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's StatusCode field to be equal HTTPStatusCode.OK
        [Fact]
        public void GivenName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesAHttpStatusCodeOkField()
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
                                    .Returns(Task.FromResult((ICollection<GetEducationalInstitutionQueryResult>)new List<GetEducationalInstitutionQueryResult>() { queryResult[0] }));

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
        #endregion

        #region Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's OperationStatus field to be true
        [Fact]
        public void GivenName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
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
                                    .Returns(Task.FromResult((ICollection<GetEducationalInstitutionQueryResult>)new List<GetEducationalInstitutionQueryResult>() { queryResult[0] }));

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Assert
            Assert.True(result.OperationStatus);
        }
        #endregion

        #endregion

        #region No object exists that contains the input string TESTS

        #region Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's StatusCode field to be equal HTTPStatusCode.NotFound
        [Fact]
        public void GivenNonExistentName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesAHttpStatusCodeNotFoundField()
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
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(It.IsNotIn<string>(new string[] { "University", "of", "Testing", "One" }), It.IsInRange(0, 150, Moq.Range.Inclusive), It.IsInRange(0, 100, Moq.Range.Inclusive), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult((ICollection<GetEducationalInstitutionQueryResult>)null));

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
        #endregion

        #region Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's OperationStatus field to be false
        [Fact]
        public void GivenNonExistentName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesAnOperationStatusFalseField()
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
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(It.IsNotIn<string>(new string[] { "University", "of", "Testing", "One" }), It.IsInRange(0, 150, Moq.Range.Inclusive), It.IsInRange(0, 100, Moq.Range.Inclusive), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult((ICollection<GetEducationalInstitutionQueryResult>)null));

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Assert
            Assert.False(result.OperationStatus);
        }
        #endregion

        #region Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's ResponseObject field to be null
        [Fact]
        public void GivenNonExistentName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesANullResponseObjectField()
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
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(It.IsNotIn<string>(new string[] { "University", "of", "Testing", "One" }), It.IsInRange(0, 150, Moq.Range.Inclusive), It.IsInRange(0, 100, Moq.Range.Inclusive), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult((ICollection<GetEducationalInstitutionQueryResult>)null));

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Assert
            Assert.Null(result.ResponseObject);
        }
        #endregion

        #region Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's Message field to contain a message
        [Fact]
        public void GivenNonExistentName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesAMessageField()
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
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(It.IsNotIn<string>(new string[] { "University", "of", "Testing", "One" }), It.IsInRange(0, 150, Moq.Range.Inclusive), It.IsInRange(0, 100, Moq.Range.Inclusive), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult((ICollection<GetEducationalInstitutionQueryResult>)null));

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Assert
            Assert.Equal($"Could not find any Educational Institution with a name like: {request.Name}!", result.Message);
        }
        #endregion

        #endregion

        #region Null arguments TESTS
        [Fact]
        public void GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            DTOEducationalInstitutionsByNameQuery request = new()
            {
                Name = "School",
                ResultsCount = 1,
                OffsetValue = 0
            };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(It.IsNotIn("University", "of", "Testing", "One" ), It.IsAny<int>(), It.IsAny<int>(), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult((ICollection<GetEducationalInstitutionQueryResult>)null));

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, dependenciesHelper.cancellationToken));
        }

        [Fact]
        public void GivenANullArgumentRepositoryToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Arrange
            DTOEducationalInstitutionsByNameQuery request = new()
            {
                Name = "School",
                ResultsCount = 1,
                OffsetValue = 0
            };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(It.IsNotIn("University", "of", "Testing", "One"), It.IsAny<int>(), It.IsAny<int>(), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult((ICollection<GetEducationalInstitutionQueryResult>)null));

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            Assert.Throws<ArgumentNullException>(() => new GetEducationalInstitutionByNameQueryHandler(null, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Arrange
            DTOEducationalInstitutionsByNameQuery request = new()
            {
                Name = "School",
                ResultsCount = 1,
                OffsetValue = 0
            };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(It.IsNotIn("University", "of", "Testing", "One"), It.IsAny<int>(), It.IsAny<int>(), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult((ICollection<GetEducationalInstitutionQueryResult>)null));

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            Assert.Throws<ArgumentNullException>(() => new GetEducationalInstitutionByNameQueryHandler(dependenciesHelper.mockRepository.Object, null));
        }
        #endregion
    }
}