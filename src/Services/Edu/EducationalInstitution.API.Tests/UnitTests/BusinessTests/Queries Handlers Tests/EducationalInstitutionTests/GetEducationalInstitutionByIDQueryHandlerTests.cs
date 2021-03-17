using EducationaInstitutionAPI.Business.Queries.OnEducationalInstitution;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.In;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using EducationaInstitutionAPI.Utils;
using EducationalInstitution.API.Tests.UnitTests;
using EducationalInstitution.API.Tests.UnitTests.BusinessTests.QueriesTests.EducationalInstitutionTests;
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

        public GetEducationalInstitutionByIDQueryHandlerTests(MockDependenciesHelper<GetEducationalInstitutionByIDQueryHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
            queryResult = new()
            {
                Name = testDataHelper.EduInstitutions[0].Name,
                Description = testDataHelper.EduInstitutions[0].Description,
                Students = testDataHelper.EduInstitutions[0].Students,
                Personnel = testDataHelper.EduInstitutions[0].Personnel,
                Professors = testDataHelper.EduInstitutions[0].Professors,
                LocationID = testDataHelper.EduInstitutions[0].LocationID,
                BuildingID = testDataHelper.EduInstitutions[0].BuildingID
            };
        }

        #region One object contains the input id TESTS

        #region Input: ID = guid | Expect: Result's StatusCode field to be equal HTTPStatusCode.OK
        [Fact]
        public void GivenAnID_ShouldReturnAResponseThatIncludesAStatusCodeOkField()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            DTOEducationalInstitutionByIDQuery request = new(eduInstitutionID);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByID(eduInstitutionID, dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult(queryResult));

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
        #endregion

        #region Input: ID = guid | Expect: Result's Message field to be empty
        [Fact]
        public void GivenAnID_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            DTOEducationalInstitutionByIDQuery request = new(eduInstitutionID);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByID(eduInstitutionID, dependenciesHelper.cancellationToken))
                                     .Returns(Task.FromResult(queryResult));

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Assert
            Assert.Empty(result.Message);
        }
        #endregion

        #region Input: ID = guid | Expect: Result to be of type record
        [Fact]
        public void GivenAnID_ShouldReturnARecordTypeResponse()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            DTOEducationalInstitutionByIDQuery request = new(eduInstitutionID);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByID(eduInstitutionID, dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult(queryResult));

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Assert
            Assert.IsType<Response<GetEducationalInstitutionByIDQueryResult>>(result);
        }
        #endregion

        #region Input: ID = guid | Expect: Result's ResponseObject field to be equal to an expected object
        [Fact]
        public void GivenAnID_ShouldReturnAResponseThatIncludesAResponseObjectWithFieldsEqualToTheModel()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            DTOEducationalInstitutionByIDQuery request = new(eduInstitutionID);
            GetEducationalInstitutionByIDQueryResult expectedResponse = new()
            {
                Name = testDataHelper.EduInstitutions[0].Name,
                Description = testDataHelper.EduInstitutions[0].Description,
                LocationID = testDataHelper.EduInstitutions[0].LocationID,
                BuildingID = testDataHelper.EduInstitutions[0].BuildingID,
                Personnel = testDataHelper.EduInstitutions[0].Personnel,
                Students = testDataHelper.EduInstitutions[0].Students,
                Professors = testDataHelper.EduInstitutions[0].Professors
            };
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByID(eduInstitutionID, dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult(queryResult));

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Assert
            Assert.Equal(expectedResponse, result.ResponseObject);
        }
        #endregion

        #region Input: ID = guid | Expect: Result's OperationStatus field to be true
        [Fact]
        public void GivenAnID_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;

            DTOEducationalInstitutionByIDQuery request = new(eduInstitutionID);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByID(eduInstitutionID, dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult(queryResult));

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Assert
            Assert.True(result.OperationStatus);
        }

        #endregion

        #endregion

        #region No object contains the input id TESTS

        #region Input: ID = guid | Expect: Result's StatusCode field to be equal HTTPStatusCode.NotFound
        [Fact]
        public void GivenANonExistentID_ShouldReturnAResponseThatIncludesAStatusCodeNotFoundField()
        {
            //Arrange
            Guid eduInstitutionID = new Guid("e1c22f85-6bfe-4f3c-badd-15ec4acbec00");

            DTOEducationalInstitutionByIDQuery request = new(eduInstitutionID);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByID(It.IsNotIn(testDataHelper.EduInstitutions[0].EduInstitutionID), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult<GetEducationalInstitutionByIDQueryResult>(null));

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
        #endregion

        #region Input: ID = guid | Expect: Result's ResponseObject field to be null
        [Fact]
        public void GivenANonExistentGuidID_ShouldReturnAResponseThatIncludesANullResponseObjectField()
        {
            //Arrange
            Guid eduInstitutionID = new Guid("e1c22f85-6bfe-4f3c-badd-15ec4acbec00");

            DTOEducationalInstitutionByIDQuery request = new(eduInstitutionID);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByID(It.IsNotIn(testDataHelper.EduInstitutions[0].EduInstitutionID), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult<GetEducationalInstitutionByIDQueryResult>(null));

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Assert
            Assert.Null(result.ResponseObject);
        }
        #endregion

        #region Input: ID = guid | Expect: Result's OperationStatus field to be false
        [Fact]
        public void GivenANonExistentGuidID_ShouldReturnAResponseThatIncludesAFalseOperationStatusField()
        {
            //Arrange
            Guid eduInstitutionID = new Guid("e1c22f85-6bfe-4f3c-badd-15ec4acbec00");

            DTOEducationalInstitutionByIDQuery request = new(eduInstitutionID);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByID(It.IsNotIn(testDataHelper.EduInstitutions[0].EduInstitutionID), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult<GetEducationalInstitutionByIDQueryResult>(null));

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Assert
            Assert.False(result.OperationStatus);
        }
        #endregion

        #region Input: ID = guid | Expect: Result's Message field to contain a message
        [Fact]
        public void GivenANonExistentID_ShouldReturnAResponseThatIncludesAMessageField()
        {
            //Arrange
            Guid eduInstitutionID = new Guid("e1c22f85-6bfe-4f3c-badd-15ec4acbec00");

            DTOEducationalInstitutionByIDQuery request = new(eduInstitutionID);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByID(It.IsNotIn(testDataHelper.EduInstitutions[0].EduInstitutionID ), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult<GetEducationalInstitutionByIDQueryResult>(null));

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            //Assert
            Assert.Equal($"Educational Institution with the following ID: {request.EduInstitutionID} has not been found!", result.Message);
        }
        #endregion

        #endregion

        #region Null arguments TESTS
        [Fact]
        public void GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            DTOEducationalInstitutionByIDQuery request = new(Guid.NewGuid());
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByID(It.IsNotIn(testDataHelper.EduInstitutions[0].EduInstitutionID), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult<GetEducationalInstitutionByIDQueryResult>(null));

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, dependenciesHelper.cancellationToken));
        }

        [Fact]
        public void GivenANullArgumentRepositoryToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Arrange
            DTOEducationalInstitutionByIDQuery request = new(Guid.NewGuid());
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByID(It.IsNotIn(testDataHelper.EduInstitutions[0].EduInstitutionID), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult<GetEducationalInstitutionByIDQueryResult>(null));

            //Assert
            Assert.Throws<ArgumentNullException>(() => new GetEducationalInstitutionByIDQueryHandler(null, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Arrange
            DTOEducationalInstitutionByIDQuery request = new(Guid.NewGuid());
            dependenciesHelper.mockRepository.Setup(mr => mr.GetByID(It.IsNotIn(testDataHelper.EduInstitutions[0].EduInstitutionID), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult<GetEducationalInstitutionByIDQueryResult>(null));

            //Assert
            Assert.Throws<ArgumentNullException>(() => new GetEducationalInstitutionByIDQueryHandler(dependenciesHelper.mockRepository.Object, null));
        }
        #endregion
    }
}