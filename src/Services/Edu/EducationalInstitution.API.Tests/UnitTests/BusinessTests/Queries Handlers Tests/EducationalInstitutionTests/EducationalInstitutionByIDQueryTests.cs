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
    public class QueryGetEducationalInstitutionByIDTests : IClassFixture<MockDependenciesHelper<GetEducationalInstitutionByIDQueryHandler>>, IClassFixture<TestDataFromJSONParser>
    {
        private readonly MockDependenciesHelper<GetEducationalInstitutionByIDQueryHandler> dependenciesHelper;
        private readonly TestDataFromJSONParser testDataHelper;
        private readonly GetEducationalInstitutionByIDQueryResult queryResult;

        public QueryGetEducationalInstitutionByIDTests(MockDependenciesHelper<GetEducationalInstitutionByIDQueryHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
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

        [Fact]
        public void GivenAGuidID_ShouldReturnAResponseThatIncludesAStatusCodeOkField()
        {
            #region Arrange

            DTOEducationalInstitutionByIDQuery request = new(testDataHelper.EduInstitutions[0].EduInstitutionID);

            #region Mock setup

            dependenciesHelper.mockRepository.Setup(mr => mr.GetByID(request.EduInstitutionID, dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult(queryResult));

            #endregion Mock setup

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            #endregion Arrange

            #region Act

            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            #endregion Act

            #region Assert

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            #endregion Assert
        }

        [Fact]
        public void GivenAGuidID_ShouldReturnAResponseThatIncludesANullMessageField()
        {
            #region Arrange

            DTOEducationalInstitutionByIDQuery request = new(testDataHelper.EduInstitutions[0].EduInstitutionID);

            #region Mock setup

            dependenciesHelper.mockRepository.Setup(mr => mr.GetByID(request.EduInstitutionID, dependenciesHelper.cancellationToken))
                                     .Returns(Task.FromResult(queryResult));

            #endregion Mock setup

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            #endregion Arrange

            #region Act

            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            #endregion Act

            #region Assert

            Assert.Null(result.Message);

            #endregion Assert
        }

        [Fact]
        public void GivenAGuidID_ShouldReturnARecordTypeResponse()
        {
            #region Arrange

            DTOEducationalInstitutionByIDQuery request = new(testDataHelper.EduInstitutions[0].EduInstitutionID);

            #region Mock setup

            dependenciesHelper.mockRepository.Setup(mr => mr.GetByID(request.EduInstitutionID, dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult(queryResult));

            #endregion Mock setup

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            #endregion Arrange

            #region Act

            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            #endregion Act

            #region Assert

            Assert.IsType<Response<GetEducationalInstitutionByIDQueryResult>>(result);

            #endregion Assert
        }

        [Fact]
        public void GivenAGuidID_ShouldReturnAResponseThatIncludesAResponseObjectWithFieldsEqualToTheModel()
        {
            #region Arrange

            DTOEducationalInstitutionByIDQuery request = new(testDataHelper.EduInstitutions[0].EduInstitutionID);
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

            #region Mock setup

            dependenciesHelper.mockRepository.Setup(mr => mr.GetByID(request.EduInstitutionID, dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult(queryResult));

            #endregion Mock setup

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            #endregion Arrange

            #region Act

            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            #endregion Act

            #region Assert

            Assert.Equal(expectedResponse, result.ResponseObject);

            #endregion Assert
        }

        [Fact]
        public void GivenAGuidID_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            #region Arrange

            DTOEducationalInstitutionByIDQuery request = new(testDataHelper.EduInstitutions[0].EduInstitutionID);

            #region Mock setup

            dependenciesHelper.mockRepository.Setup(mr => mr.GetByID(request.EduInstitutionID, dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult(queryResult));

            #endregion Mock setup

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            #endregion Arrange

            #region Act

            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            #endregion Act

            #region Assert

            Assert.False(result.OperationStatus);

            #endregion Assert
        }

        [Fact]
        public void GivenAWrongGuidID_ShouldReturnAResponseThatIncludesAStatusCodeNotFoundField()
        {
            #region Arrange

            DTOEducationalInstitutionByIDQuery request = new(Guid.NewGuid());

            #region Mock setup

            dependenciesHelper.mockRepository.Setup(mr => mr.GetByID(It.IsNotIn<Guid>(new Guid[1] { testDataHelper.EduInstitutions[0].EduInstitutionID }), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult<GetEducationalInstitutionByIDQueryResult>(null));

            #endregion Mock setup

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            #endregion Arrange

            #region Act

            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            #endregion Act

            #region Assert

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);

            #endregion Assert
        }

        [Fact]
        public void GivenAWrongGuidID_ShouldReturnAResponseThatIncludesANullResponseObjectField()
        {
            #region Arrange

            DTOEducationalInstitutionByIDQuery request = new(Guid.NewGuid());

            #region Mock setup

            dependenciesHelper.mockRepository.Setup(mr => mr.GetByID(It.IsNotIn<Guid>(new Guid[1] { testDataHelper.EduInstitutions[0].EduInstitutionID }), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult<GetEducationalInstitutionByIDQueryResult>(null));

            #endregion Mock setup

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            #endregion Arrange

            #region Act

            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            #endregion Act

            #region Assert

            Assert.Null(result.ResponseObject);

            #endregion Assert
        }

        [Fact]
        public void GivenAWrongGuidID_ShouldReturnAResponseThatIncludesAFalseOperationStatusField()
        {
            #region Arrange

            DTOEducationalInstitutionByIDQuery request = new(Guid.NewGuid());

            #region Mock setup

            dependenciesHelper.mockRepository.Setup(mr => mr.GetByID(It.IsNotIn<Guid>(new Guid[1] { testDataHelper.EduInstitutions[0].EduInstitutionID }), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult<GetEducationalInstitutionByIDQueryResult>(null));

            #endregion Mock setup

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            #endregion Arrange

            #region Act

            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            #endregion Act

            #region Assert

            Assert.False(result.OperationStatus);

            #endregion Assert
        }

        [Fact]
        public void GivenAWrongGuidID_ShouldReturnAResponseThatIncludesAMessageField()
        {
            #region Arrange

            DTOEducationalInstitutionByIDQuery request = new(Guid.NewGuid());

            #region Mock setup

            dependenciesHelper.mockRepository.Setup(mr => mr.GetByID(It.IsNotIn<Guid>(new Guid[1] { testDataHelper.EduInstitutions[0].EduInstitutionID }), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult<GetEducationalInstitutionByIDQueryResult>(null));

            #endregion Mock setup

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            #endregion Arrange

            #region Act

            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            #endregion Act

            #region Assert

            Assert.Equal($"Educational Institution with the following ID:{request.EduInstitutionID} has not been found!", result.Message);

            #endregion Assert
        }

        [Fact]
        public void GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            #region Arrange

            DTOEducationalInstitutionByIDQuery request = new(Guid.NewGuid());

            #region Mock setup

            dependenciesHelper.mockRepository.Setup(mr => mr.GetByID(It.IsNotIn<Guid>(new Guid[1] { testDataHelper.EduInstitutions[0].EduInstitutionID }), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult<GetEducationalInstitutionByIDQueryResult>(null));

            #endregion Mock setup

            GetEducationalInstitutionByIDQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            #endregion Arrange

            #region Assert

            Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, dependenciesHelper.cancellationToken));

            #endregion Assert
        }

        [Fact]
        public void GivenANullArgumentRepositoryToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            #region Arrange

            DTOEducationalInstitutionByIDQuery request = new(Guid.NewGuid());

            #region Mock setup

            dependenciesHelper.mockRepository.Setup(mr => mr.GetByID(It.IsNotIn<Guid>(new Guid[1] { testDataHelper.EduInstitutions[0].EduInstitutionID }), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult<GetEducationalInstitutionByIDQueryResult>(null));

            #endregion Mock setup

            #endregion Arrange

            #region Assert

            Assert.Throws<ArgumentNullException>(() => new GetEducationalInstitutionByIDQueryHandler(null, dependenciesHelper.mockLogger.Object));

            #endregion Assert
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            #region Arrange

            DTOEducationalInstitutionByIDQuery request = new(Guid.NewGuid());

            #region Mock setup

            dependenciesHelper.mockRepository.Setup(mr => mr.GetByID(It.IsNotIn<Guid>(new Guid[1] { testDataHelper.EduInstitutions[0].EduInstitutionID }), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult<GetEducationalInstitutionByIDQueryResult>(null));

            #endregion Mock setup

            #endregion Arrange

            #region Assert

            Assert.Throws<ArgumentNullException>(() => new GetEducationalInstitutionByIDQueryHandler(dependenciesHelper.mockRepository.Object, null));

            #endregion Assert
        }
    }
}