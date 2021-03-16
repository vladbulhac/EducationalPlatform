using EducationaInstitutionAPI.Business.Queries_Handlers.Queries_on_EducationalInstitution;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.In;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using EducationaInstitutionAPI.Utils;
using EducationalInstitution.API.Tests.UnitTests.BusinessTests.QueriesTests.EducationalInstitutionTests;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.Tests.UnitTests.BusinessTests.Queries_Handlers_Tests.EducationalInstitutionTests
{
    public class GetEducationalInstitutionByLocationQueryHandlerTests : IClassFixture<MockDependenciesHelper<GetEducationalInstitutionByLocationQueryHandler>>, IClassFixture<TestDataFromJSONParser>
    {
        private readonly MockDependenciesHelper<GetEducationalInstitutionByLocationQueryHandler> dependenciesHelper;
        private readonly TestDataFromJSONParser testDataHelper;
        private readonly GetEducationalInstitutionByLocationQueryResult queryResult;

        public GetEducationalInstitutionByLocationQueryHandlerTests(MockDependenciesHelper<GetEducationalInstitutionByLocationQueryHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
            queryResult = new()
            {
                Name = testDataHelper.EduInstitutions[0].Name,
                Description = testDataHelper.EduInstitutions[0].Description,
                BuildingID = testDataHelper.EduInstitutions[0].BuildingID,
                EduInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID
            };
        }

        #region One object contains the input locationID TESTS
        #region Input: LocationID = string | Expect: Result's ResponseObject field to be equal to an expected object
        [Fact]
        public void GivenALocationID_ShouldReturnAResponseThatIncludes_AResponseObjectWithTheEducationalInstitutionThatHasThatLocationID()
        {
            //Arrange
            string locationID = "location1";

            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByLocation(locationID, dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult(queryResult));

            GetEducationalInstitutionByLocationQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Assert
            Assert.Equal(queryResult, result.ResponseObject);
        }
        #endregion

        #region Input: LocationID = string | Expect: Result to be of type record
        [Fact]
        public void GivenALocationID_ShouldReturnARecordTypeResponse()
        {
            //Arrange
            string locationID = "location1";

            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByLocation(locationID, dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult(queryResult));

            GetEducationalInstitutionByLocationQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Arrange
            Assert.IsType<Response<GetEducationalInstitutionByLocationQueryResult>>(result);
        }
        #endregion

        #region Input: LocationID = string | Expect: Result's Message field to be empty
        [Fact]
        public void GivenALocationID_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            string locationID = "location1";

            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByLocation(locationID, dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult(queryResult));

            GetEducationalInstitutionByLocationQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Arrange
            Assert.Empty(result.Message);
        }
        #endregion

        #region Input: LocationID = string | Expect: Result's StatusCode field to be equal HTTPStatusCode.OK
        [Fact]
        public void GivenALocationID_ShouldReturnAResponseThatIncludesAHttpStatusCodeOkField()
        {
            //Arrange
            string locationID = "location1";

            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByLocation(locationID, dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult(queryResult));

            GetEducationalInstitutionByLocationQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Arrange
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
        #endregion

        #region Input: LocationID = string | Expect: Result's OperationStatus field to be true
        [Fact]
        public void GivenALocationID_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            string locationID = "location1";

            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByLocation(locationID, dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult(queryResult));

            GetEducationalInstitutionByLocationQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Arrange
            Assert.True(result.OperationStatus);
        }
        #endregion
        #endregion

        #region No object exists that contains the input string TESTS
        #region Input: LocationID = string | Expect: Result's StatusCode field to be equal HTTPStatusCode.NotFound
        [Fact]
        public void GivenANonExistentLocationID_ShouldReturnAResponseThatIncludesAHttpStatusCodeNotFoundField()
        {
            //Arrange
            string locationID = "e32Loq4";

            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByLocation(It.IsNotIn("location1","location12"), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult<GetEducationalInstitutionByLocationQueryResult>(null));

            GetEducationalInstitutionByLocationQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Arrange
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
        #endregion

        #region Input: LocationID = string | Expect: Result's OperationStatus field to be false
        [Fact]
        public void GivenANonExistentLocationID_ShouldReturnAResponseThatIncludesAnOperationStatusFalseField()
        {
            //Arrange
            string locationID = "e32Loq4";

            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByLocation(It.IsNotIn("location1", "location12"), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult<GetEducationalInstitutionByLocationQueryResult>(null));

            GetEducationalInstitutionByLocationQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Arrange
            Assert.False(result.OperationStatus);
        }
        #endregion

        #region Input: LocationID = string | Expect: Result's ResponseObject field to be null
        [Fact]
        public void GivenANonExistentLocationID_ShouldReturnAResponseThatIncludesANullResponseObjectField()
        {
            //Arrange
            string locationID = "e32Loq4";

            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByLocation(It.IsNotIn("location1", "location12"), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult<GetEducationalInstitutionByLocationQueryResult>(null));

            GetEducationalInstitutionByLocationQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Arrange
            Assert.Null(result.ResponseObject);
        }
        #endregion

        #region Input: LocationID = string | Expect: Result's Message field to contain a message
        [Fact]
        public void GivenANonExistentLocationID_ShouldReturnAResponseThatIncludesAMessageField()
        {
            //Arrange
            string locationID = "e32Loq4";

            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByLocation(It.IsNotIn("location1", "location12"), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult<GetEducationalInstitutionByLocationQueryResult>(null));

            GetEducationalInstitutionByLocationQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Arrange
            Assert.Equal($"No Educational Institution with the following LocationID: {request.LocationID} has been found!", result.Message);
        }
        #endregion
        #endregion

        #region Null arguments TESTS
        [Fact]
        public void GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = "location1" };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByLocation(It.IsNotIn("location1", "location12" ), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult((GetEducationalInstitutionByLocationQueryResult)null));

            GetEducationalInstitutionByLocationQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, dependenciesHelper.cancellationToken));
        }

        [Fact]
        public void GivenANullArgumentRepositoryToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Arrange
            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = "location1" };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByLocation(It.IsNotIn("location1", "location12" ), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult((GetEducationalInstitutionByLocationQueryResult)null));

            GetEducationalInstitutionByLocationQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            Assert.Throws<ArgumentNullException>(() => new GetEducationalInstitutionByLocationQueryHandler(null, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Arrange
            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = "location1" };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByLocation(It.IsNotIn("location1", "location12" ), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult((GetEducationalInstitutionByLocationQueryResult)null));

            GetEducationalInstitutionByLocationQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            Assert.Throws<ArgumentNullException>(() => new GetEducationalInstitutionByLocationQueryHandler(dependenciesHelper.mockRepository.Object, null));
        }
        #endregion
    }
}