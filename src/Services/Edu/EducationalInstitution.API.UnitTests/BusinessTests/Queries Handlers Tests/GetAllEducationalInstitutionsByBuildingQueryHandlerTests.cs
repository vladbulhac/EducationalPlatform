using EducationalInstitutionAPI.Business.Queries_Handlers;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Queries;
using Moq;
using Moq.Language;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.UnitTests.BusinessTests.Queries_Handlers_Tests
{
    public class GetAllEducationalInstitutionsByBuildingQueryHandlerTests : IClassFixture<MockDependenciesHelper<GetAllEducationalInstitutionsByBuildingQueryHandler>>
    {
        private readonly MockDependenciesHelper<GetAllEducationalInstitutionsByBuildingQueryHandler> dependenciesHelper;

        /// <remarks>Called before each test</remarks>
        public GetAllEducationalInstitutionsByBuildingQueryHandlerTests(MockDependenciesHelper<GetAllEducationalInstitutionsByBuildingQueryHandler> dependenciesHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
        }

        [Fact]
        public async Task GivenADTOEducationalInstitutionsByBuildingQuery_ShouldReturnResponseOfGetAllEducationalInstitutionsWithSameBuildingQueryResultType()
        {
            //Arrange
            DTOEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

            GetAllEducationalInstitutionsWithSameBuildingQueryResult expectedQueryResult = new()
            {
                EducationalInstitutions = new List<EducationalInstitutionBaseQueryResult>() {
                                                new(){ EducationalInstitutionID=Guid.NewGuid(),
                                                       Name="testName",
                                                       Description="testDescription"
                                                }}
            };

            SetupMockedDependencies(expectedQueryResult);

            var handler = new GetAllEducationalInstitutionsByBuildingQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(dto);

            //Assert
            Assert.IsType<Response<GetAllEducationalInstitutionsWithSameBuildingQueryResult>>(result);
        }

        [Fact]
        public async Task GivenADTOEducationalInstitutionsByBuildingQuery_ShouldReturnTrueOperationStatus()
        {
            //Arrange
            DTOEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

            GetAllEducationalInstitutionsWithSameBuildingQueryResult expectedQueryResult = new()
            {
                EducationalInstitutions = new List<EducationalInstitutionBaseQueryResult>() {
                                                new(){ EducationalInstitutionID=Guid.NewGuid(),
                                                       Name="testName",
                                                       Description="testDescription"
                                                }}
            };

            SetupMockedDependencies(expectedQueryResult);

            var handler = new GetAllEducationalInstitutionsByBuildingQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(dto);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenADTOEducationalInstitutionsByBuildingQuery_ShouldReturnEmptyMessage()
        {
            //Arrange
            DTOEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

            GetAllEducationalInstitutionsWithSameBuildingQueryResult expectedQueryResult = new()
            {
                EducationalInstitutions = new List<EducationalInstitutionBaseQueryResult>() {
                                                new(){ EducationalInstitutionID=Guid.NewGuid(),
                                                       Name="testName",
                                                       Description="testDescription"
                                                }}
            };

            SetupMockedDependencies(expectedQueryResult);

            var handler = new GetAllEducationalInstitutionsByBuildingQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(dto);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenADTOEducationalInstitutionsByBuildingQuery_ShouldReturnStatusCodeOk()
        {
            //Arrange
            DTOEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

            GetAllEducationalInstitutionsWithSameBuildingQueryResult expectedQueryResult = new()
            {
                EducationalInstitutions = new List<EducationalInstitutionBaseQueryResult>() {
                                                new(){ EducationalInstitutionID=Guid.NewGuid(),
                                                       Name="testName",
                                                       Description="testDescription"
                                                }}
            };

            SetupMockedDependencies(expectedQueryResult);

            var handler = new GetAllEducationalInstitutionsByBuildingQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(dto);

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task GivenADTOEducationalInstitutionsByBuildingQuery_ShouldReturnCollectionWithOneElement()
        {
            //Arrange
            DTOEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

            GetAllEducationalInstitutionsWithSameBuildingQueryResult expectedQueryResult = new()
            {
                EducationalInstitutions = new List<EducationalInstitutionBaseQueryResult>() {
                                                new(){ EducationalInstitutionID=Guid.NewGuid(),
                                                       Name="testName",
                                                       Description="testDescription"
                                                }}
            };

            SetupMockedDependencies(expectedQueryResult);

            var handler = new GetAllEducationalInstitutionsByBuildingQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(dto);

            //Assert
            Assert.Single(result.Data.EducationalInstitutions);
        }

        [Fact]
        public async Task GivenADTOEducationalInstitutionsByBuildingQuery_ShouldReturnExpectedCollectionItems()
        {
            //Arrange
            DTOEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

            GetAllEducationalInstitutionsWithSameBuildingQueryResult expectedQueryResult = new()
            {
                EducationalInstitutions = new List<EducationalInstitutionBaseQueryResult>() {
                                                new(){ EducationalInstitutionID=Guid.NewGuid(),
                                                       Name="testName",
                                                       Description="testDescription"
                                                }}
            };

            SetupMockedDependencies(expectedQueryResult);

            var handler = new GetAllEducationalInstitutionsByBuildingQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(dto);

            //Assert
            Assert.Equal(expectedQueryResult.EducationalInstitutions, result.Data.EducationalInstitutions);
        }

        [Fact]
        public async Task GivenADTOEducationalInstitutionsByBuildingQuery_NoEducationalInstitutionsEntityIsFound_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            DTOEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

            GetAllEducationalInstitutionsWithSameBuildingQueryResult expectedQueryResult = new()
            {
                EducationalInstitutions = new List<EducationalInstitutionBaseQueryResult>(0)
            };

            SetupMockedDependencies(expectedQueryResult);

            var handler = new GetAllEducationalInstitutionsByBuildingQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(dto);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenADTOEducationalInstitutionsByBuildingQuery_NoEducationalInstitutionsEntityIsFound_ShouldReturnStatusCodeNotFound()
        {
            //Arrange
            DTOEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

            GetAllEducationalInstitutionsWithSameBuildingQueryResult expectedQueryResult = new()
            {
                EducationalInstitutions = new List<EducationalInstitutionBaseQueryResult>(0)
            };

            SetupMockedDependencies(expectedQueryResult);

            var handler = new GetAllEducationalInstitutionsByBuildingQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(dto);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GivenADTOEducationalInstitutionsByBuildingQuery_NoEducationalInstitutionsEntityIsFound_ShouldReturnNullData()
        {
            //Arrange
            DTOEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

            GetAllEducationalInstitutionsWithSameBuildingQueryResult expectedQueryResult = new()
            {
                EducationalInstitutions = new List<EducationalInstitutionBaseQueryResult>(0)
            };

            SetupMockedDependencies(expectedQueryResult);

            var handler = new GetAllEducationalInstitutionsByBuildingQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(dto);

            //Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GivenADTOEducationalInstitutionsByBuildingQuery_NoEducationalInstitutionsEntityIsFound_ShouldReturnMessage()
        {
            //Arrange
            DTOEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

            GetAllEducationalInstitutionsWithSameBuildingQueryResult expectedQueryResult = new()
            {
                EducationalInstitutions = new List<EducationalInstitutionBaseQueryResult>(0)
            };

            SetupMockedDependencies(expectedQueryResult);

            var handler = new GetAllEducationalInstitutionsByBuildingQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(dto);

            //Assert
            Assert.Equal($"No Educational Institution that has a building: {dto.BuildingID} has been found!", result.Message);
        }

        [Fact]
        public async Task GivenADTOEducationalInstitutionsByBuildingQuery_AnExceptionIsCaught_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            DTOEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

            SetupRepositoryToThrowException();

            var handler = new GetAllEducationalInstitutionsByBuildingQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(dto);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenADTOEducationalInstitutionsByBuildingQuery_AnExceptionIsCaught_ShouldReturnStatusCodeInternalServerError()
        {
            //Arrange
            DTOEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

            SetupRepositoryToThrowException();

            var handler = new GetAllEducationalInstitutionsByBuildingQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(dto);

            //Assert
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task GivenADTOEducationalInstitutionsByBuildingQuery_AnExceptionIsCaught_ShouldReturnNullData()
        {
            //Arrange
            DTOEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

            SetupRepositoryToThrowException();

            var handler = new GetAllEducationalInstitutionsByBuildingQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(dto);

            //Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GivenADTOEducationalInstitutionsByBuildingQuery_AnExceptionIsCaught_ShouldReturnMessage()
        {
            //Arrange
            DTOEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

            SetupRepositoryToThrowException();

            var handler = new GetAllEducationalInstitutionsByBuildingQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(dto);

            //Assert
            Assert.Equal($"An error occurred while searching for any Educational Institution with the following BuildingID: {dto.BuildingID}!", result.Message);
        }

        private void SetupRepositoryToThrowException()
        {
            dependenciesHelper.mockUnitOfWorkQuery.Setup(uowq => uowq.UsingEducationalInstitutionQueryRepository())
                                                  .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(eiqr => eiqr.GetAllEducationalInstitutionsWithSameBuildingAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                                                                         .ThrowsAsync(new Exception());
        }

        private void SetupMockedDependencies(GetAllEducationalInstitutionsWithSameBuildingQueryResult expectedQueryResult)
        {
            dependenciesHelper.mockUnitOfWorkQuery.Setup(uowq => uowq.UsingEducationalInstitutionQueryRepository())
                                                  .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(eiqr => eiqr.GetAllEducationalInstitutionsWithSameBuildingAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                                                                         .ReturnsAsync(expectedQueryResult);
        }
    }
}