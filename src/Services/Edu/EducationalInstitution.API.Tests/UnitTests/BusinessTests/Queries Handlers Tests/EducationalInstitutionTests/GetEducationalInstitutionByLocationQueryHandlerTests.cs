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
        public async Task GivenALocationID_ShouldReturnAResponseThatIncludesAResponseObjectWithTheEducationalInstitutionThatHasThatLocationID()
        {
            //Arrange
            string locationID = "location1";

            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByLocation(locationID, dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(queryResult);

            GetEducationalInstitutionByLocationQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(queryResult, result.ResponseObject);
        }

        #endregion Input: LocationID = string | Expect: Result's ResponseObject field to be equal to an expected object

        #region Input: LocationID = string | Expect: Result to be of type record Response<GetEducationalInstitutionByLocationQueryResult>

        [Fact]
        public async Task GivenALocationID_ShouldReturnARecordTypeResponse()
        {
            //Arrange
            string locationID = "location1";

            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByLocation(locationID, dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(queryResult);

            GetEducationalInstitutionByLocationQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Arrange
            Assert.IsType<Response<GetEducationalInstitutionByLocationQueryResult>>(result);
        }

        #endregion Input: LocationID = string | Expect: Result to be of type record Response<GetEducationalInstitutionByLocationQueryResult>

        #region Input: LocationID = string | Expect: Result's ResponseObject field to be of type GetEducationalInstitutionByLocationQueryResult

        [Fact]
        public async Task GivenALocationID_ShouldReturnAResponseThatIncludesAResponseObjectOfTypeGetEducationalInstitutionByLocationQueryResult()
        {
            //Arrange
            string locationID = "location1";

            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByLocation(locationID, dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(queryResult);

            GetEducationalInstitutionByLocationQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Arrange
            Assert.IsType<GetEducationalInstitutionByLocationQueryResult>(result.ResponseObject);
        }

        #endregion Input: LocationID = string | Expect: Result's ResponseObject field to be of type GetEducationalInstitutionByLocationQueryResult

        #region Input: LocationID = string | Expect: Result's Message field to be empty

        [Fact]
        public async Task GivenALocationID_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            string locationID = "location1";

            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByLocation(locationID, dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(queryResult);

            GetEducationalInstitutionByLocationQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Arrange
            Assert.Empty(result.Message);
        }

        #endregion Input: LocationID = string | Expect: Result's Message field to be empty

        #region Input: LocationID = string | Expect: Result's StatusCode field to be equal HTTPStatusCode.OK

        [Fact]
        public async Task GivenALocationID_ShouldReturnAResponseThatIncludesAHttpStatusCodeOkField()
        {
            //Arrange
            string locationID = "location1";

            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByLocation(locationID, dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(queryResult);

            GetEducationalInstitutionByLocationQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Arrange
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        #endregion Input: LocationID = string | Expect: Result's StatusCode field to be equal HTTPStatusCode.OK

        #region Input: LocationID = string | Expect: Result's OperationStatus field to be true

        [Fact]
        public async Task GivenALocationID_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            string locationID = "location1";

            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByLocation(locationID, dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(queryResult);

            GetEducationalInstitutionByLocationQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Arrange
            Assert.True(result.OperationStatus);
        }

        #endregion Input: LocationID = string | Expect: Result's OperationStatus field to be true

        #endregion One object contains the input locationID TESTS

        #region No object exists that contains the input string TESTS

        #region Input: LocationID = string | Expect: Result's StatusCode field to be equal HTTPStatusCode.NotFound

        [Fact]
        public async Task GivenANonExistentLocationID_ShouldReturnAResponseThatIncludesAHttpStatusCodeNotFoundField()
        {
            //Arrange
            string locationID = "e32Loq4";
            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };

            GetEducationalInstitutionByLocationQueryResult repositoryTaskResult = null;
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByLocation(It.IsNotIn("location1", "location12"), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(repositoryTaskResult);

            GetEducationalInstitutionByLocationQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Arrange
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        #endregion Input: LocationID = string | Expect: Result's StatusCode field to be equal HTTPStatusCode.NotFound

        #region Input: LocationID = string | Expect: Result's OperationStatus field to be false

        [Fact]
        public async Task GivenANonExistentLocationID_ShouldReturnAResponseThatIncludesAnOperationStatusFalseField()
        {
            //Arrange
            string locationID = "e32Loq4";
            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };

            GetEducationalInstitutionByLocationQueryResult repositoryTaskResult = null;
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByLocation(It.IsNotIn("location1", "location12"), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(repositoryTaskResult);

            GetEducationalInstitutionByLocationQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Arrange
            Assert.False(result.OperationStatus);
        }

        #endregion Input: LocationID = string | Expect: Result's OperationStatus field to be false

        #region Input: LocationID = string | Expect: Result's ResponseObject field to be null

        [Fact]
        public async Task GivenANonExistentLocationID_ShouldReturnAResponseThatIncludesANullResponseObjectField()
        {
            //Arrange
            string locationID = "e32Loq4";
            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };

            GetEducationalInstitutionByLocationQueryResult repositoryTaskResult = null;
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByLocation(It.IsNotIn("location1", "location12"), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(repositoryTaskResult);

            GetEducationalInstitutionByLocationQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Arrange
            Assert.Null(result.ResponseObject);
        }

        #endregion Input: LocationID = string | Expect: Result's ResponseObject field to be null

        #region Input: LocationID = string | Expect: Result's Message field to contain a message

        [Fact]
        public async Task GivenANonExistentLocationID_ShouldReturnAResponseThatIncludesAMessageField()
        {
            //Arrange
            string locationID = "e32Loq4";
            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };

            GetEducationalInstitutionByLocationQueryResult repositoryTaskResult = null;
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByLocation(It.IsNotIn("location1", "location12"), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(repositoryTaskResult);

            GetEducationalInstitutionByLocationQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Arrange
            Assert.Equal($"No Educational Institution with the following LocationID: {request.LocationID} has been found!", result.Message);
        }

        #endregion Input: LocationID = string | Expect: Result's Message field to contain a message

        #endregion No object exists that contains the input string TESTS

        #region Null arguments TESTS

        [Fact]
        public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            GetEducationalInstitutionByLocationQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, dependenciesHelper.cancellationToken));
        }

        [Fact]
        public void GivenANullArgumentRepositoryToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new GetEducationalInstitutionByLocationQueryHandler(null, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new GetEducationalInstitutionByLocationQueryHandler(dependenciesHelper.mockRepository.Object, null));
        }

        #endregion Null arguments TESTS
    }
}