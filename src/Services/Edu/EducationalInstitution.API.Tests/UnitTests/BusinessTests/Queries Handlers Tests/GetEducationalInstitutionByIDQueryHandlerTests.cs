using EducationalInstitution.API.Tests.UnitTests;
using EducationalInstitutionAPI.Business.Queries_Handlers;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Queries;
using EducationalInstitutionAPI.Unit_of_Work;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.Tests
{
    public class GetEducationalInstitutionByIDQueryHandlerTests : IClassFixture<MockDependenciesHelper<GetEducationalInstitutionByIDQueryHandler>>, IClassFixture<TestDataFromJSONParser>
    {
        private readonly MockDependenciesHelper<GetEducationalInstitutionByIDQueryHandler> dependenciesHelper;
        private readonly TestDataFromJSONParser testDataHelper;
        private readonly GetEducationalInstitutionByIDQueryResult queryResult;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;

        public GetEducationalInstitutionByIDQueryHandlerTests(MockDependenciesHelper<GetEducationalInstitutionByIDQueryHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
            queryResult = new()
            {
                Name = testDataHelper.EduInstitutions[0].Name,
                Description = testDataHelper.EduInstitutions[0].Description,
                LocationID = testDataHelper.EduInstitutions[0].LocationID,
                BuildingsIDs = testDataHelper.EduInstitutions[0].Buildings
            };

            mockUnitOfWork = new();
        }

        #region One object contains the input id TESTS

        [Fact]
        public async Task GivenAnID_ShouldReturnAResponseThatIncludesAStatusCodeOkField()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EduInstitutions[0].EducationalInstitutionID;

            DTOEducationalInstitutionByIDQuery request = new(eduInstitutionID);
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByIDAsync(eduInstitutionID, dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(queryResult);

            GetEducationalInstitutionByIDQueryHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnID_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EduInstitutions[0].EducationalInstitutionID;

            DTOEducationalInstitutionByIDQuery request = new(eduInstitutionID);
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByIDAsync(eduInstitutionID, dependenciesHelper.cancellationToken))
                                     .ReturnsAsync(queryResult);

            GetEducationalInstitutionByIDQueryHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnID_ShouldReturnARecordTypeResponse()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EduInstitutions[0].EducationalInstitutionID;

            DTOEducationalInstitutionByIDQuery request = new(eduInstitutionID);
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByIDAsync(eduInstitutionID, dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(queryResult);

            GetEducationalInstitutionByIDQueryHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<Response<GetEducationalInstitutionByIDQueryResult>>(result);
        }

        [Fact]
        public async Task GivenAnID_ShouldReturnAResponseThatIncludesAResponseObjectOfType_GetEducationalInstitutionByIDQueryResult()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EduInstitutions[0].EducationalInstitutionID;

            DTOEducationalInstitutionByIDQuery request = new(eduInstitutionID);
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByIDAsync(eduInstitutionID, dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(queryResult);

            GetEducationalInstitutionByIDQueryHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<GetEducationalInstitutionByIDQueryResult>(result.ResponseObject);
        }

        [Fact]
        public async Task GivenAnID_ShouldReturnAResponseThatIncludesAResponseObjectWithFieldsEqualToTheModel()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EduInstitutions[0].EducationalInstitutionID;

            DTOEducationalInstitutionByIDQuery request = new(eduInstitutionID);
            GetEducationalInstitutionByIDQueryResult expectedResponse = new()
            {
                Name = testDataHelper.EduInstitutions[0].Name,
                Description = testDataHelper.EduInstitutions[0].Description,
                LocationID = testDataHelper.EduInstitutions[0].LocationID,
                BuildingsIDs = testDataHelper.EduInstitutions[0].Buildings
            };
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByIDAsync(eduInstitutionID, dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(queryResult);

            GetEducationalInstitutionByIDQueryHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(expectedResponse, result.ResponseObject);
        }

        [Fact]
        public async Task GivenAnID_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EduInstitutions[0].EducationalInstitutionID;

            DTOEducationalInstitutionByIDQuery request = new(eduInstitutionID);
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByIDAsync(eduInstitutionID, dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(queryResult);

            GetEducationalInstitutionByIDQueryHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.True(result.OperationStatus);
        }

        #endregion One object contains the input id TESTS

        #region No object contains the input id TESTS

        [Fact]
        public async Task GivenANonExistentID_ShouldReturnAResponseThatIncludesAStatusCodeNotFoundField()
        {
            //Arrange
            Guid eduInstitutionID = new("e1c22f85-6bfe-4f3c-badd-15ec4acbec00");
            DTOEducationalInstitutionByIDQuery request = new(eduInstitutionID);

            GetEducationalInstitutionByIDQueryResult repositoryTaskResult = null;
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByIDAsync(It.IsNotIn(testDataHelper.EduInstitutions[0].EducationalInstitutionID), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(repositoryTaskResult);

            GetEducationalInstitutionByIDQueryHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GivenANonExistentGuidID_ShouldReturnAResponseThatIncludesANullResponseObjectField()
        {
            //Arrange
            Guid eduInstitutionID = new("e1c22f85-6bfe-4f3c-badd-15ec4acbec00");
            DTOEducationalInstitutionByIDQuery request = new(eduInstitutionID);

            GetEducationalInstitutionByIDQueryResult repositoryTaskResult = null;
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByIDAsync(It.IsNotIn(testDataHelper.EduInstitutions[0].EducationalInstitutionID), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(repositoryTaskResult);

            GetEducationalInstitutionByIDQueryHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Null(result.ResponseObject);
        }

        [Fact]
        public async Task GivenANonExistentGuidID_ShouldReturnAResponseThatIncludesAFalseOperationStatusField()
        {
            //Arrange
            Guid eduInstitutionID = new("e1c22f85-6bfe-4f3c-badd-15ec4acbec00");
            DTOEducationalInstitutionByIDQuery request = new(eduInstitutionID);

            GetEducationalInstitutionByIDQueryResult repositoryTaskResult = null;
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByIDAsync(It.IsNotIn(testDataHelper.EduInstitutions[0].EducationalInstitutionID), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(repositoryTaskResult);

            GetEducationalInstitutionByIDQueryHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenANonExistentID_ShouldReturnAResponseThatIncludesAMessageField()
        {
            //Arrange
            Guid eduInstitutionID = new("e1c22f85-6bfe-4f3c-badd-15ec4acbec00");
            DTOEducationalInstitutionByIDQuery request = new(eduInstitutionID);

            GetEducationalInstitutionByIDQueryResult repositoryTaskResult = null;
            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByIDAsync(It.IsNotIn(testDataHelper.EduInstitutions[0].EducationalInstitutionID), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(repositoryTaskResult);

            GetEducationalInstitutionByIDQueryHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal($"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!", result.Message);
        }

        #endregion No object contains the input id TESTS

        [Fact]
        public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            GetEducationalInstitutionByIDQueryHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, dependenciesHelper.cancellationToken));
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
            Assert.Throws<ArgumentNullException>(() => new GetEducationalInstitutionByIDQueryHandler(mockUnitOfWork.Object, null));
        }
    }
}