using EducationalInstitutionAPI.Business.Queries_Handlers;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Queries;
using Moq;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.UnitTests.BusinessTests.Queries_Handlers_Tests
{
    public class GetEducationalInstitutionByIDQueryHandlerTests : IClassFixture<MockDependenciesHelper<GetEducationalInstitutionByIDQueryHandler>>,
                                                                  IClassFixture<TestDataFromJSONParser>
    {
        private readonly TestDataFromJSONParser testDataHelper;
        private readonly GetEducationalInstitutionByIDQueryResult queryResult;
        private readonly MockDependenciesHelper<GetEducationalInstitutionByIDQueryHandler> dependenciesHelper;

        /// <remarks>Called before each test</remarks>
        public GetEducationalInstitutionByIDQueryHandlerTests(MockDependenciesHelper<GetEducationalInstitutionByIDQueryHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
            queryResult = new()
            {
                Name = testDataHelper.EducationalInstitutions[0].Name,
                Description = testDataHelper.EducationalInstitutions[0].Description,
                LocationID = testDataHelper.EducationalInstitutions[0].LocationID,
                //BuildingsIDs = testDataHelper.EducationalInstitutions[0].Buildings
            };
        }

        #region One object contains the input id TESTS

        [Fact]
        public async Task GivenAnID_ShouldReturnAResponseThatIncludesAStatusCodeOkField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            DTOEducationalInstitutionByIDQuery request = new() { EducationalInstitutionID = educationalInstitutionID };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uok => uok.UsingEducationalInstitutionQueryRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetByIDAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                        .ReturnsAsync(queryResult);

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnID_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            DTOEducationalInstitutionByIDQuery request = new() { EducationalInstitutionID = educationalInstitutionID };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uok => uok.UsingEducationalInstitutionQueryRepository())
                                                     .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetByIDAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                        .ReturnsAsync(queryResult);

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnID_ShouldReturnARecordTypeResponse()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            DTOEducationalInstitutionByIDQuery request = new() { EducationalInstitutionID = educationalInstitutionID };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uok => uok.UsingEducationalInstitutionQueryRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetByIDAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                        .ReturnsAsync(queryResult);

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.IsType<Response<GetEducationalInstitutionByIDQueryResult>>(result);
        }

        [Fact]
        public async Task GivenAnID_ShouldReturnAResponseThatIncludesDataOfType_GetEducationalInstitutionByIDQueryResult()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            DTOEducationalInstitutionByIDQuery request = new() { EducationalInstitutionID = educationalInstitutionID };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uok => uok.UsingEducationalInstitutionQueryRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetByIDAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                        .ReturnsAsync(queryResult);

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.IsType<GetEducationalInstitutionByIDQueryResult>(result.Data);
        }

        [Fact]
        public async Task GivenAnID_ShouldReturnAResponseThatIncludesDataWithFieldsEqualToTheModel()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            DTOEducationalInstitutionByIDQuery request = new() { EducationalInstitutionID = educationalInstitutionID };

            GetEducationalInstitutionByIDQueryResult expectedResponse = new()
            {
                Name = testDataHelper.EducationalInstitutions[0].Name,
                Description = testDataHelper.EducationalInstitutions[0].Description,
                LocationID = testDataHelper.EducationalInstitutions[0].LocationID,
                //BuildingsIDs = testDataHelper.EducationalInstitutions[0].Buildings
            };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uok => uok.UsingEducationalInstitutionQueryRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetByIDAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                    .ReturnsAsync(queryResult);

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(expectedResponse, result.Data);
        }

        [Fact]
        public async Task GivenAnID_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            DTOEducationalInstitutionByIDQuery request = new() { EducationalInstitutionID = educationalInstitutionID };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uok => uok.UsingEducationalInstitutionQueryRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetByIDAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                                                        .ReturnsAsync(queryResult);

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.True(result.OperationStatus);
        }

        #endregion One object contains the input id TESTS

        #region No object contains the input id TESTS

        [Fact]
        public async Task GivenANonExistentID_ShouldReturnAResponseThatIncludesAStatusCodeNotFoundField()
        {
            //Arrange
            Guid educationalInstitutionID = new("e1c22f85-6bfe-4f3c-badd-15ec4acbec00");
            DTOEducationalInstitutionByIDQuery request = new() { EducationalInstitutionID = educationalInstitutionID };

            GetEducationalInstitutionByIDQueryResult repositoryTaskResult = null;
            dependenciesHelper.mockUnitOfWorkQuery.Setup(uok => uok.UsingEducationalInstitutionQueryRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetByIDAsync(It.IsNotIn(testDataHelper.EducationalInstitutions[0].EducationalInstitutionID), It.IsAny<CancellationToken>()))
                                                                        .ReturnsAsync(repositoryTaskResult);

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GivenANonExistentGuidID_ShouldReturnAResponseThatIncludesANullDataField()
        {
            //Arrange
            Guid educationalInstitutionID = new("e1c22f85-6bfe-4f3c-badd-15ec4acbec00");
            DTOEducationalInstitutionByIDQuery request = new() { EducationalInstitutionID = educationalInstitutionID };

            GetEducationalInstitutionByIDQueryResult repositoryTaskResult = null;
            dependenciesHelper.mockUnitOfWorkQuery.Setup(uok => uok.UsingEducationalInstitutionQueryRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetByIDAsync(It.IsNotIn(testDataHelper.EducationalInstitutions[0].EducationalInstitutionID), It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(repositoryTaskResult);

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GivenANonExistentGuidID_ShouldReturnAResponseThatIncludesAFalseOperationStatusField()
        {
            //Arrange
            Guid educationalInstitutionID = new("e1c22f85-6bfe-4f3c-badd-15ec4acbec00");
            DTOEducationalInstitutionByIDQuery request = new() { EducationalInstitutionID = educationalInstitutionID };

            GetEducationalInstitutionByIDQueryResult repositoryTaskResult = null;
            dependenciesHelper.mockUnitOfWorkQuery.Setup(uok => uok.UsingEducationalInstitutionQueryRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetByIDAsync(It.IsNotIn(testDataHelper.EducationalInstitutions[0].EducationalInstitutionID), It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(repositoryTaskResult);

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenANonExistentID_ShouldReturnAResponseThatIncludesAMessageField()
        {
            //Arrange
            Guid educationalInstitutionID = new("e1c22f85-6bfe-4f3c-badd-15ec4acbec00");
            DTOEducationalInstitutionByIDQuery request = new() { EducationalInstitutionID = educationalInstitutionID };

            GetEducationalInstitutionByIDQueryResult repositoryTaskResult = null;
            dependenciesHelper.mockUnitOfWorkQuery.Setup(uok => uok.UsingEducationalInstitutionQueryRepository())
                                .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetByIDAsync(It.IsNotIn(testDataHelper.EducationalInstitutions[0].EducationalInstitutionID), It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(repositoryTaskResult);

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal($"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!", result.Message);
        }

        #endregion No object contains the input id TESTS

        [Fact]
        public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null));
        }

        [Fact]
        public void GivenANullArgumentRepositoryToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new GetEducationalInstitutionByIDQueryHandler(null, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new GetEducationalInstitutionByIDQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, null));
        }
    }
}