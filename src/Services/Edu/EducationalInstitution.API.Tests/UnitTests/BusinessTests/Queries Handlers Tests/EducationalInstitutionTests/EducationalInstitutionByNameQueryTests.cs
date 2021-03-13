using EducationaInstitutionAPI.Business.Queries.OnEducationalInstitution;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.In;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using EducationaInstitutionAPI.Utils;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.Tests.UnitTests.BusinessTests.QueriesTests.EducationalInstitutionTests
{
    public class EducationalInstitutionByNameQueryTests : IClassFixture<MockDependenciesHelper<GetEducationalInstitutionByNameQueryHandler>>, IClassFixture<TestDataFromJSONParser>
    {
        private readonly MockDependenciesHelper<GetEducationalInstitutionByNameQueryHandler> dependenciesHelper;
        private readonly TestDataFromJSONParser testDataHelper;
        private readonly IList<GetEducationalInstitutionQueryResult> queryResult;

        public EducationalInstitutionByNameQueryTests(MockDependenciesHelper<GetEducationalInstitutionByNameQueryHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
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

        [Fact]
        public void GivenAString_ShouldReturnAResponseThatIncludes_AResponseObjectWithTheEducationalInstitutionThatHasThatName()
        {
            #region Arrange

            DTOEducationalInstitutionsByNameQuery request = new() { Name = "University of Testing", ResultsCount = 1, OffsetValue = 0 };

            #region Mock setup

            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(request.Name, It.IsAny<int>(), It.IsAny<int>(), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult((ICollection<GetEducationalInstitutionQueryResult>)new List<GetEducationalInstitutionQueryResult>() { queryResult[0] }));

            #endregion Mock setup

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            #endregion Arrange

            #region Act

            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            #endregion Act

            #region Assert

            Assert.Equal(queryResult[0], result.ResponseObject.Single());

            #endregion Assert
        }

        [Fact]
        public void GivenAString_ShouldReturnARecordTypeResponse()
        {
            #region Arrange

            DTOEducationalInstitutionsByNameQuery request = new() { Name = "University", ResultsCount = 1, OffsetValue = 0 };

            #region Mock setup

            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(request.Name, It.IsAny<int>(), It.IsAny<int>(), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult((ICollection<GetEducationalInstitutionQueryResult>)new List<GetEducationalInstitutionQueryResult>() { queryResult[0] }));

            #endregion Mock setup

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            #endregion Arrange

            #region Act

            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            #endregion Act

            #region Assert

            Assert.IsType<Response<ICollection<GetEducationalInstitutionQueryResult>>>(result);

            #endregion Assert
        }

        [Fact]
        public void GivenAString_ShouldReturnAResponseThatIncludesANullMessageField()
        {
            #region Arrange

            DTOEducationalInstitutionsByNameQuery request = new() { Name = "University of Testing", ResultsCount = 1, OffsetValue = 0 };

            #region Mock setup

            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(request.Name, It.IsAny<int>(), It.IsAny<int>(), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult((ICollection<GetEducationalInstitutionQueryResult>)new List<GetEducationalInstitutionQueryResult>() { queryResult[0] }));

            #endregion Mock setup

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            #endregion Arrange

            #region Act

            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            #endregion Act

            #region Assert

            Assert.Null(result.Message);

            #endregion Assert
        }

        [Fact]
        public void GivenAString_ShouldReturnAResponseThatIncludesAHttpStatusCodeOkField()
        {
            #region Arrange

            DTOEducationalInstitutionsByNameQuery request = new() { Name = "University of Testing", ResultsCount = 1, OffsetValue = 0 };

            #region Mock setup

            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(request.Name, It.IsAny<int>(), It.IsAny<int>(), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult((ICollection<GetEducationalInstitutionQueryResult>)new List<GetEducationalInstitutionQueryResult>() { queryResult[0] }));

            #endregion Mock setup

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            #endregion Arrange

            #region Act

            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            #endregion Act

            #region Assert

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            #endregion Assert
        }

        [Fact]
        public void GivenAString_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            #region Arrange

            DTOEducationalInstitutionsByNameQuery request = new() { Name = "University of Testing", ResultsCount = 1, OffsetValue = 0 };

            #region Mock setup

            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(request.Name, It.IsAny<int>(), It.IsAny<int>(), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult((ICollection<GetEducationalInstitutionQueryResult>)new List<GetEducationalInstitutionQueryResult>() { queryResult[0] }));

            #endregion Mock setup

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            #endregion Arrange

            #region Act

            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            #endregion Act

            #region Assert

            Assert.True(result.OperationStatus);

            #endregion Assert
        }

        [Fact]
        public void GivenAStringThatDoesNotExistInAnyName_ShouldReturnAResponseThatIncludesAHttpStatusCodeNotFoundField()
        {
            #region Arrange

            DTOEducationalInstitutionsByNameQuery request = new() { Name = "School", ResultsCount = 1, OffsetValue = 0 };

            #region Mock setup

            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(It.IsNotIn<string>(new string[] { "University", "of", "Testing", "One" }), It.IsAny<int>(), It.IsAny<int>(), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult((ICollection<GetEducationalInstitutionQueryResult>)null));

            #endregion Mock setup

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            #endregion Arrange

            #region Act

            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            #endregion Act

            #region Assert

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);

            #endregion Assert
        }

        [Fact]
        public void GivenAStringThatDoesNotExistInAnyName_ShouldReturnAResponseThatIncludesAnOperationStatusFalseField()
        {
            #region Arrange

            DTOEducationalInstitutionsByNameQuery request = new() { Name = "School", ResultsCount = 1, OffsetValue = 0 };

            #region Mock setup

            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(It.IsNotIn<string>(new string[] { "University", "of", "Testing", "One" }), It.IsAny<int>(), It.IsAny<int>(), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult((ICollection<GetEducationalInstitutionQueryResult>)null));

            #endregion Mock setup

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            #endregion Arrange

            #region Act

            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            #endregion Act

            #region Assert

            Assert.False(result.OperationStatus);

            #endregion Assert
        }

        [Fact]
        public void GivenAStringThatDoesNotExistInAnyName_ShouldReturnAResponseThatIncludesANullResponseObjectField()
        {
            #region Arrange

            DTOEducationalInstitutionsByNameQuery request = new() { Name = "School", ResultsCount = 1, OffsetValue = 0 };

            #region Mock setup

            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(It.IsNotIn<string>(new string[] { "University", "of", "Testing", "One" }), It.IsAny<int>(), It.IsAny<int>(), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult((ICollection<GetEducationalInstitutionQueryResult>)null));

            #endregion Mock setup

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            #endregion Arrange

            #region Act

            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            #endregion Act

            #region Assert

            Assert.Null(result.ResponseObject);

            #endregion Assert
        }

        [Fact]
        public void GivenAStringThatDoesNotExistInAnyName_ShouldReturnAResponseThatIncludesAMessageField()
        {
            #region Arrange

            DTOEducationalInstitutionsByNameQuery request = new() { Name = "School", ResultsCount = 1, OffsetValue = 0 };

            #region Mock setup

            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeName(It.IsNotIn<string>(new string[] { "University", "of", "Testing", "One" }), It.IsAny<int>(), It.IsAny<int>(), dependenciesHelper.cancellationToken))
                                    .Returns(Task.FromResult((ICollection<GetEducationalInstitutionQueryResult>)null));

            #endregion Mock setup

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            #endregion Arrange

            #region Act

            var result = handler.Handle(request, dependenciesHelper.cancellationToken).Result;

            #endregion Act

            #region Assert

            Assert.Equal($"Could not find any Educational Institution with a name like:{request.Name}!", result.Message);

            #endregion Assert
        }
    }
}