﻿using EducationaInstitutionAPI.Business.Queries.OnEducationalInstitution;
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
                BuildingsIDs = testDataHelper.EduInstitutions[0].Buildings
                },
                new(){
                EduInstitutionID=testDataHelper.EduInstitutions[1].EduInstitutionID,
                Name = testDataHelper.EduInstitutions[1].Name,
                Description = testDataHelper.EduInstitutions[1].Description,
                LocationID = testDataHelper.EduInstitutions[1].LocationID,
                BuildingsIDs = testDataHelper.EduInstitutions[1].Buildings
                }
            };
        }

        #region One object contains the input string TESTS

        [Fact]
        public async Task GivenName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludes_AResponseObjectWithTheEducationalInstitutionThatHasThatName()
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
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeNameAsync(request.Name, It.IsInRange(0, 150, Moq.Range.Inclusive), It.IsInRange(0, 100, Moq.Range.Inclusive), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(new List<GetEducationalInstitutionQueryResult>() { queryResult[0] });

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(queryResult[0], result.ResponseObject.Single());
        }

        [Fact]
        public async Task GivenName_OffsetValue_ResultsCount_ShouldReturnARecordTypeResponse()
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
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeNameAsync(request.Name, It.IsInRange(0, 150, Moq.Range.Inclusive), It.IsInRange(0, 100, Moq.Range.Inclusive), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(new List<GetEducationalInstitutionQueryResult>() { queryResult[0] });

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<Response<ICollection<GetEducationalInstitutionQueryResult>>>(result);
        }

        [Fact]
        public async Task GivenName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
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
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeNameAsync(request.Name, It.IsInRange(0, 150, Moq.Range.Inclusive), It.IsInRange(0, 100, Moq.Range.Inclusive), dependenciesHelper.cancellationToken))
                                     .ReturnsAsync(new List<GetEducationalInstitutionQueryResult>() { queryResult[0] });

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesAHttpStatusCodeOkField()
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
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeNameAsync(request.Name, It.IsInRange(0, 150, Moq.Range.Inclusive), It.IsInRange(0, 100, Moq.Range.Inclusive), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(new List<GetEducationalInstitutionQueryResult>() { queryResult[0] });

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task GivenName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
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
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeNameAsync(request.Name, It.IsInRange(0, 150, Moq.Range.Inclusive), It.IsInRange(0, 100, Moq.Range.Inclusive), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(new List<GetEducationalInstitutionQueryResult>() { queryResult[0] });

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.True(result.OperationStatus);
        }

        #endregion One object contains the input string TESTS

        #region No object exists that contains the input string TESTS

        [Fact]
        public async Task GivenNonExistentName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesAHttpStatusCodeNotFoundField()
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

            ICollection<GetEducationalInstitutionQueryResult> repositoryTaskResult = null;
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeNameAsync(It.IsNotIn(new string[] { "University", "of", "Testing", "One" }), It.IsInRange(0, 150, Moq.Range.Inclusive), It.IsInRange(0, 100, Moq.Range.Inclusive), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(repositoryTaskResult);

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GivenNonExistentName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesAnOperationStatusFalseField()
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

            ICollection<GetEducationalInstitutionQueryResult> repositoryTaskResult = null;
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeNameAsync(It.IsNotIn<string>(new string[] { "University", "of", "Testing", "One" }), It.IsInRange(0, 150, Moq.Range.Inclusive), It.IsInRange(0, 100, Moq.Range.Inclusive), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(repositoryTaskResult);

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenNonExistentName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesANullResponseObjectField()
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

            ICollection<GetEducationalInstitutionQueryResult> repositoryTaskResult = null;
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeNameAsync(It.IsNotIn(new string[] { "University", "of", "Testing", "One" }), It.IsInRange(0, 150, Moq.Range.Inclusive), It.IsInRange(0, 100, Moq.Range.Inclusive), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(repositoryTaskResult);

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Null(result.ResponseObject);
        }

        [Fact]
        public async Task GivenNonExistentName_OffsetValue_ResultsCount_ShouldReturnAResponseThatIncludesAMessageField()
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

            ICollection<GetEducationalInstitutionQueryResult> repositoryTaskResult = null;
            dependenciesHelper.mockRepository.Setup(mr => mr.GetAllLikeNameAsync(It.IsNotIn<string>(new string[] { "University", "of", "Testing", "One" }), It.IsInRange(0, 150, Moq.Range.Inclusive), It.IsInRange(0, 100, Moq.Range.Inclusive), dependenciesHelper.cancellationToken))
                                    .ReturnsAsync(repositoryTaskResult);

            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal($"Could not find any Educational Institution with a name like: {request.Name}!", result.Message);
        }

        #endregion No object exists that contains the input string TESTS

        #region Null arguments TESTS

        [Fact]
        public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            GetEducationalInstitutionByNameQueryHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, dependenciesHelper.cancellationToken));
        }

        [Fact]
        public void GivenANullArgumentRepositoryToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new GetEducationalInstitutionByNameQueryHandler(null, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new GetEducationalInstitutionByNameQueryHandler(dependenciesHelper.mockRepository.Object, null));
        }

        #endregion Null arguments TESTS
    }
}