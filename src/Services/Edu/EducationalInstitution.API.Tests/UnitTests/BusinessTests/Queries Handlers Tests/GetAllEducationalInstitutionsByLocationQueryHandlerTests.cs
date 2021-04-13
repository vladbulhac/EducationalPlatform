using EducationalInstitutionAPI.Business.Queries_Handlers;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Queries;
using EducationalInstitutionAPI.Unit_of_Work;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.Tests.UnitTests.BusinessTests.Queries_Handlers_Tests
{
    public class GetEducationalInstitutionByLocationQueryHandlerTests : IClassFixture<MockDependenciesHelper<GetAllEducationalInstitutionsByLocationQueryHandler>>, IClassFixture<TestDataFromJSONParser>
    {
        private readonly MockDependenciesHelper<GetAllEducationalInstitutionsByLocationQueryHandler> dependenciesHelper;
        private readonly TestDataFromJSONParser testDataHelper;
        private readonly GetAllEducationalInstitutionsByLocationQueryResult queryResult;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;

        public GetEducationalInstitutionByLocationQueryHandlerTests(MockDependenciesHelper<GetAllEducationalInstitutionsByLocationQueryHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
            queryResult = new()
            {
                EducationalInstitutions = new List<GetEducationalInstitutionByLocationQueryResult>() { new(){
                        Name = testDataHelper.EduInstitutions[0].Name,
                        Description = testDataHelper.EduInstitutions[0].Description,
                        BuildingsIDs = testDataHelper.EduInstitutions[0].Buildings,
                        EduInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID
                        }
                    }
            };

            mockUnitOfWork = new();
        }

        #region One object contains the input locationID TESTS

        [Fact]
        public async Task GivenALocationID_ShouldReturnAResponseThatIncludesAResponseObjectWithTheEducationalInstitutionThatHasThatLocationID()
        {
            //Arrange
            string locationID = "location1";

            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllByLocationAsync(locationID, dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(queryResult);

            GetAllEducationalInstitutionsByLocationQueryHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(queryResult, result.ResponseObject);
        }

        [Fact]
        public async Task GivenALocationID_ShouldReturnARecordTypeResponse()
        {
            //Arrange
            string locationID = "location1";

            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllByLocationAsync(locationID, dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(queryResult);

            GetAllEducationalInstitutionsByLocationQueryHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Arrange
            Assert.IsType<Response<GetAllEducationalInstitutionsByLocationQueryResult>>(result);
        }

        [Fact]
        public async Task GivenALocationID_ShouldReturnAResponseThatIncludesAResponseObjectOfTypeGetEducationalInstitutionByLocationQueryResult()
        {
            //Arrange
            string locationID = "location1";

            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllByLocationAsync(locationID, dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(queryResult);

            GetAllEducationalInstitutionsByLocationQueryHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Arrange
            Assert.IsType<GetAllEducationalInstitutionsByLocationQueryResult>(result.ResponseObject);
        }

        [Fact]
        public async Task GivenALocationID_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            string locationID = "location1";

            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllByLocationAsync(locationID, dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(queryResult);

            GetAllEducationalInstitutionsByLocationQueryHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Arrange
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenALocationID_ShouldReturnAResponseThatIncludesAHttpStatusCodeOkField()
        {
            //Arrange
            string locationID = "location1";

            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllByLocationAsync(locationID, dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(queryResult);

            GetAllEducationalInstitutionsByLocationQueryHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Arrange
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task GivenALocationID_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            string locationID = "location1";

            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllByLocationAsync(locationID, dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(queryResult);

            GetAllEducationalInstitutionsByLocationQueryHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

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
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllByLocationAsync(It.IsNotIn("location1", "location12"), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(repositoryTaskResult);

            GetAllEducationalInstitutionsByLocationQueryHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

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
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllByLocationAsync(It.IsNotIn("location1", "location12"), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(repositoryTaskResult);

            GetAllEducationalInstitutionsByLocationQueryHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Arrange
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenANonExistentLocationID_ShouldReturnAResponseThatIncludesANullResponseObjectField()
        {
            //Arrange
            string locationID = "e32Loq4";
            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };

            GetAllEducationalInstitutionsByLocationQueryResult repositoryTaskResult = null;
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllByLocationAsync(It.IsNotIn("location1", "location12"), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(repositoryTaskResult);

            GetAllEducationalInstitutionsByLocationQueryHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Arrange
            Assert.Null(result.ResponseObject);
        }

        [Fact]
        public async Task GivenANonExistentLocationID_ShouldReturnAResponseThatIncludesAMessageField()
        {
            //Arrange
            string locationID = "e32Loq4";
            DTOEducationalInstitutionByLocationQuery request = new() { LocationID = locationID };

            GetAllEducationalInstitutionsByLocationQueryResult repositoryTaskResult = null;
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllByLocationAsync(It.IsNotIn("location1", "location12"), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(repositoryTaskResult);

            GetAllEducationalInstitutionsByLocationQueryHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Arrange
            Assert.Equal($"No Educational Institution with the following LocationID: {request.LocationID} has been found!", result.Message);
        }

        #endregion No object exists that contains the input string TESTS

        #region Null arguments TESTS

        [Fact]
        public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            GetAllEducationalInstitutionsByLocationQueryHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, dependenciesHelper.cancellationToken));
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
            Assert.Throws<ArgumentNullException>(() => new GetAllEducationalInstitutionsByLocationQueryHandler(mockUnitOfWork.Object, null));
        }

        #endregion Null arguments TESTS
    }
}