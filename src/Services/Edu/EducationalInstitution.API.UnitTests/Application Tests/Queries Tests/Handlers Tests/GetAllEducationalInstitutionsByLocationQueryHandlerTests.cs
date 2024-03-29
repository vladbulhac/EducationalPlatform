﻿using EducationalInstitution.API.Tests.Shared;
using EducationalInstitution.Application;
using EducationalInstitution.Application.Queries;
using EducationalInstitution.Application.Queries.Handlers;
using EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results;
using Moq;
using System.Net;
using Xunit;

namespace EducationalInstitution.API.UnitTests.Application_Tests.Queries_Tests.Handlers_Tests;

public class GetAllEducationalInstitutionsByLocationQueryHandlerTests : IClassFixture<MockDependenciesHelper<GetAllEducationalInstitutionsByLocationQueryHandler>>,
                                                                        IClassFixture<TestDataFromJSONParser>
{
    private readonly TestDataFromJSONParser testDataHelper;
    private readonly GetAllEducationalInstitutionsByLocationQueryResult queryResult;
    private readonly MockDependenciesHelper<GetAllEducationalInstitutionsByLocationQueryHandler> dependenciesHelper;

    /// <remarks>Called before each test</remarks>
    public GetAllEducationalInstitutionsByLocationQueryHandlerTests(MockDependenciesHelper<GetAllEducationalInstitutionsByLocationQueryHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
    {
        this.dependenciesHelper = dependenciesHelper;
        this.testDataHelper = testDataHelper;
        queryResult = new()
        {
            EducationalInstitutions = new List<GetEducationalInstitutionByLocationQueryResult>() { new(){
                        Name = testDataHelper.EducationalInstitutions[0].Name,
                        Description = testDataHelper.EducationalInstitutions[0].Description,
                        BuildingsIDs = testDataHelper.EducationalInstitutions[0].Buildings.Select(b=>b.Id).ToList(),
                        EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].Id
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
        GetAllEducationalInstitutionsByLocationQuery request = new() { LocationID = locationID };

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
        GetAllEducationalInstitutionsByLocationQuery request = new() { LocationID = locationID };

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
    public async Task GivenALocationID_ShouldReturnAResponseThatIncludesDataOfTypeGetAllEducationalInstitutionsByLocationQueryResult()
    {
        //Arrange
        string locationID = "location1";
        GetAllEducationalInstitutionsByLocationQuery request = new() { LocationID = locationID };

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
        GetAllEducationalInstitutionsByLocationQuery request = new() { LocationID = locationID };

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
        GetAllEducationalInstitutionsByLocationQuery request = new() { LocationID = locationID };

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
        GetAllEducationalInstitutionsByLocationQuery request = new() { LocationID = locationID };

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
        GetAllEducationalInstitutionsByLocationQuery request = new() { LocationID = locationID };

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
        GetAllEducationalInstitutionsByLocationQuery request = new() { LocationID = locationID };

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
        GetAllEducationalInstitutionsByLocationQuery request = new() { LocationID = locationID };

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
        GetAllEducationalInstitutionsByLocationQuery request = new() { LocationID = locationID };

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