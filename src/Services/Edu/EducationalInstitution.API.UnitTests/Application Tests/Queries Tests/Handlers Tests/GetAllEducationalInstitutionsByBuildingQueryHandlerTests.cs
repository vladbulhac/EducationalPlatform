using EducationalInstitution.Application;
using EducationalInstitution.Application.Queries;
using EducationalInstitution.Application.Queries.Handlers;
using EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results;
using Moq;
using System.Net;
using Xunit;

namespace EducationalInstitution.API.UnitTests.Application_Tests.Queries_Tests.Handlers_Tests;

public class GetAllEducationalInstitutionsByBuildingQueryHandlerTests : IClassFixture<MockDependenciesHelper<GetAllEducationalInstitutionsByBuildingQueryHandler>>
{
    private readonly MockDependenciesHelper<GetAllEducationalInstitutionsByBuildingQueryHandler> dependenciesHelper;

    /// <remarks>Called before each test</remarks>
    public GetAllEducationalInstitutionsByBuildingQueryHandlerTests(MockDependenciesHelper<GetAllEducationalInstitutionsByBuildingQueryHandler> dependenciesHelper)
    {
        this.dependenciesHelper = dependenciesHelper;
    }

    [Fact]
    public async Task GivenAGetAllEducationalInstitutionsByBuildingQuery_ShouldReturnResponseOfGetAllEducationalInstitutionsWithSameBuildingQueryResultType()
    {
        //Arrange
        GetAllEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

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
    public async Task GivenAGetAllEducationalInstitutionsByBuildingQuery_ShouldReturnTrueOperationStatus()
    {
        //Arrange
        GetAllEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

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
    public async Task GivenAGetAllEducationalInstitutionsByBuildingQuery_ShouldReturnEmptyMessage()
    {
        //Arrange
        GetAllEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

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
    public async Task GivenAGetAllEducationalInstitutionsByBuildingQuery_ShouldReturnStatusCodeOk()
    {
        //Arrange
        GetAllEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

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
    public async Task GivenAGetAllEducationalInstitutionsByBuildingQuery_ShouldReturnCollectionWithOneElement()
    {
        //Arrange
        GetAllEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

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
    public async Task GivenAGetAllEducationalInstitutionsByBuildingQuery_ShouldReturnExpectedCollectionItems()
    {
        //Arrange
        GetAllEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

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
    public async Task GivenAGetAllEducationalInstitutionsByBuildingQuery_NoEducationalInstitutionsEntityIsFound_ShouldReturnFalseOperationStatus()
    {
        //Arrange
        GetAllEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

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
    public async Task GivenAGetAllEducationalInstitutionsByBuildingQuery_NoEducationalInstitutionsEntityIsFound_ShouldReturnStatusCodeNotFound()
    {
        //Arrange
        GetAllEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

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
    public async Task GivenAGetAllEducationalInstitutionsByBuildingQuery_NoEducationalInstitutionsEntityIsFound_ShouldReturnNullData()
    {
        //Arrange
        GetAllEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

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
    public async Task GivenAGetAllEducationalInstitutionsByBuildingQuery_NoEducationalInstitutionsEntityIsFound_ShouldReturnMessage()
    {
        //Arrange
        GetAllEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

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
    public async Task GivenAGetAllEducationalInstitutionsByBuildingQuery_AnExceptionIsCaught_ShouldReturnFalseOperationStatus()
    {
        //Arrange
        GetAllEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

        SetupRepositoryToThrowException();

        var handler = new GetAllEducationalInstitutionsByBuildingQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(dto);

        //Assert
        Assert.False(result.OperationStatus);
    }

    [Fact]
    public async Task GivenAGetAllEducationalInstitutionsByBuildingQuery_AnExceptionIsCaught_ShouldReturnStatusCodeInternalServerError()
    {
        //Arrange
        GetAllEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

        SetupRepositoryToThrowException();

        var handler = new GetAllEducationalInstitutionsByBuildingQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(dto);

        //Assert
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }

    [Fact]
    public async Task GivenAGetAllEducationalInstitutionsByBuildingQuery_AnExceptionIsCaught_ShouldReturnNullData()
    {
        //Arrange
        GetAllEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

        SetupRepositoryToThrowException();

        var handler = new GetAllEducationalInstitutionsByBuildingQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

        //Act
        var result = await handler.Handle(dto);

        //Assert
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GivenAGetAllEducationalInstitutionsByBuildingQuery_AnExceptionIsCaught_ShouldReturnMessage()
    {
        //Arrange
        GetAllEducationalInstitutionsByBuildingQuery dto = new() { BuildingID = "building123" };

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