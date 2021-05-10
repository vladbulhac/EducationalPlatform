using EducationalInstitutionAPI.Business.Queries_Handlers;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Queries;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.UnitTests.BusinessTests.Queries_Handlers_Tests
{
    public class GetEducationalInstitutionByLocationQueryHandlerTests : IClassFixture<MockDependenciesHelper<GetAllEducationalInstitutionsByLocationQueryHandler>>, IClassFixture<TestDataFromJSONParser>
    {
        private readonly TestDataFromJSONParser testDataHelper;
        private readonly GetAllEducationalInstitutionsByLocationQueryResult queryResult;
        private readonly MockDependenciesHelper<GetAllEducationalInstitutionsByLocationQueryHandler> dependenciesHelper;

        /// <remarks>Called before each test</remarks>
        public GetEducationalInstitutionByLocationQueryHandlerTests(MockDependenciesHelper<GetAllEducationalInstitutionsByLocationQueryHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
            queryResult = new()
            {
                EducationalInstitutions = new List<GetEducationalInstitutionByLocationQueryResult>() { new(){
                        Name = testDataHelper.EducationalInstitutions[0].Name,
                        Description = testDataHelper.EducationalInstitutions[0].Description,
                        BuildingsIDs = testDataHelper.EducationalInstitutions[0].Buildings.Select(b=>b.BuildingID).ToList(),
                        EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID
                        }
                    }
            };
        }

        #region One object contains the input locationID TESTS

        [Fact]
        public async Task GivenALocationID_ShouldReturnAResponseThatIncludesDataWithTheEducationalInstitutionThatHasThatLocationID()
        {
            //Arrange
            string locationID = "location1";
            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uok => uok.UsingEducationalInstitutionQueryRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetAllByLocationAsync(locationID, It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(queryResult);

            GetAllEducationalInstitutionsByLocationQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(queryResult, result.Data);
        }

        [Fact]
        public async Task GivenALocationID_ShouldReturnARecordTypeResponse()
        {
            //Arrange
            string locationID = "location1";
            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uok => uok.UsingEducationalInstitutionQueryRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetAllByLocationAsync(locationID, It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(queryResult);

            GetAllEducationalInstitutionsByLocationQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Arrange
            Assert.IsType<Response<GetAllEducationalInstitutionsByLocationQueryResult>>(result);
        }

        [Fact]
        public async Task GivenALocationID_ShouldReturnAResponseThatIncludesDataOfTypeGetEducationalInstitutionByLocationQueryResult()
        {
            //Arrange
            string locationID = "location1";
            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uok => uok.UsingEducationalInstitutionQueryRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetAllByLocationAsync(locationID, It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(queryResult);

            GetAllEducationalInstitutionsByLocationQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Arrange
            Assert.IsType<GetAllEducationalInstitutionsByLocationQueryResult>(result.Data);
        }

        [Fact]
        public async Task GivenALocationID_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            string locationID = "location1";
            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uok => uok.UsingEducationalInstitutionQueryRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetAllByLocationAsync(locationID, It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(queryResult);

            GetAllEducationalInstitutionsByLocationQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Arrange
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenALocationID_ShouldReturnAResponseThatIncludesAHttpStatusCodeOkField()
        {
            //Arrange
            string locationID = "location1";
            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uok => uok.UsingEducationalInstitutionQueryRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetAllByLocationAsync(locationID, It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(queryResult);

            GetAllEducationalInstitutionsByLocationQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Arrange
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task GivenALocationID_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            string locationID = "location1";
            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uok => uok.UsingEducationalInstitutionQueryRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetAllByLocationAsync(locationID, It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(queryResult);

            GetAllEducationalInstitutionsByLocationQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Arrange
            Assert.True(result.OperationStatus);
        }

        #endregion One object contains the input locationID TESTS

        #region No object exists that contains the input string TESTS

        [Fact]
        public async Task GivenANonExistentLocationID_ShouldReturnAResponseThatIncludesAHttpStatusCodeNotFoundField()
        {
            //Arrange
            string locationID = "e32Loq4";
            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };

            GetAllEducationalInstitutionsByLocationQueryResult repositoryTaskResult = null;
            dependenciesHelper.mockUnitOfWorkQuery.Setup(uok => uok.UsingEducationalInstitutionQueryRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetAllByLocationAsync(It.IsNotIn("location1", "location12"), It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(repositoryTaskResult);

            GetAllEducationalInstitutionsByLocationQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Arrange
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GivenANonExistentLocationID_ShouldReturnAResponseThatIncludesAnOperationStatusFalseField()
        {
            //Arrange
            string locationID = "e32Loq4";
            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };

            GetAllEducationalInstitutionsByLocationQueryResult repositoryTaskResult = null;
            dependenciesHelper.mockUnitOfWorkQuery.Setup(uok => uok.UsingEducationalInstitutionQueryRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetAllByLocationAsync(It.IsNotIn("location1", "location12"), It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(repositoryTaskResult);

            GetAllEducationalInstitutionsByLocationQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Arrange
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenANonExistentLocationID_ShouldReturnAResponseThatIncludesANullDataField()
        {
            //Arrange
            string locationID = "e32Loq4";
            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };

            GetAllEducationalInstitutionsByLocationQueryResult repositoryTaskResult = null;
            dependenciesHelper.mockUnitOfWorkQuery.Setup(uok => uok.UsingEducationalInstitutionQueryRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetAllByLocationAsync(It.IsNotIn("location1", "location12"), It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(repositoryTaskResult);

            GetAllEducationalInstitutionsByLocationQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Arrange
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GivenANonExistentLocationID_ShouldReturnAResponseThatIncludesAMessageField()
        {
            //Arrange
            string locationID = "e32Loq4";
            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };

            GetAllEducationalInstitutionsByLocationQueryResult repositoryTaskResult = null;
            dependenciesHelper.mockUnitOfWorkQuery.Setup(uok => uok.UsingEducationalInstitutionQueryRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetAllByLocationAsync(It.IsNotIn("location1", "location12"), It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(repositoryTaskResult);

            GetAllEducationalInstitutionsByLocationQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Arrange
            Assert.Equal($"No Educational Institution with the following LocationID: {request.LocationID} has been found!", result.Message);
        }

        #endregion No object exists that contains the input string TESTS

        #region Null arguments TESTS

        [Fact]
        public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            GetAllEducationalInstitutionsByLocationQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null));
        }

        [Fact]
        public void GivenANullArgumentRepositoryToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new GetAllEducationalInstitutionsByLocationQueryHandler(null, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new GetAllEducationalInstitutionsByLocationQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, null));
        }

        #endregion Null arguments TESTS
    }
}