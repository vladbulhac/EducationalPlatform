using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Queries;
using EducationalInstitutionAPI.Grpc;
using EducationalInstitutionAPI.Proto;
using EducationalInstitutionAPI.Utils.Mappers;
using Google.Protobuf.WellKnownTypes;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.UnitTests.Grpc_Tests
{
    public class EducationalInstitutionQueryServiceTests : IClassFixture<MockDependenciesHelper<EducationalInstitutionQueryService>>,
                                                           IClassFixture<TestDataFromJSONParser>
    {
        private readonly MockDependenciesHelper<EducationalInstitutionQueryService> dependenciesHelper;
        private readonly TestDataFromJSONParser testDataHelper;

        /// <remarks>Called before each test</remarks>
        public EducationalInstitutionQueryServiceTests(MockDependenciesHelper<EducationalInstitutionQueryService> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
        }

        #region GetEducationalInstitutionByIDMethod TESTS

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnAnEducationalInstitutionGetResponse()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = new()
                {
                    Name = educationalInstitution.Name,
                    Description = educationalInstitution.Description,
                    LocationID = educationalInstitution.LocationID,
                    JoinDate = educationalInstitution.JoinDate,
                    ParentInstitution = null,
                    ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(),
                    BuildingsIDs = new List<string>()
                },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.IsType<EducationalInstitutionGetResponse>(result);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnATrueOperationStatusField()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = new()
                {
                    Name = educationalInstitution.Name,
                    Description = educationalInstitution.Description,
                    LocationID = educationalInstitution.LocationID,
                    JoinDate = educationalInstitution.JoinDate,
                    ParentInstitution = null,
                    ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(),
                    BuildingsIDs = new List<string>()
                },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnAnEmptyMessageField()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = new()
                {
                    Name = educationalInstitution.Name,
                    Description = educationalInstitution.Description,
                    LocationID = educationalInstitution.LocationID,
                    JoinDate = educationalInstitution.JoinDate,
                    ParentInstitution = null,
                    ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(),
                    BuildingsIDs = new List<string>()
                },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnAStatusCodeCreatedField()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = new()
                {
                    Name = educationalInstitution.Name,
                    Description = educationalInstitution.Description,
                    LocationID = educationalInstitution.LocationID,
                    JoinDate = educationalInstitution.JoinDate,
                    ParentInstitution = null,
                    ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(),
                    BuildingsIDs = new List<string>()
                },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithExpectedName()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = new()
                {
                    Name = educationalInstitution.Name,
                    Description = educationalInstitution.Description,
                    LocationID = educationalInstitution.LocationID,
                    JoinDate = educationalInstitution.JoinDate,
                    ParentInstitution = null,
                    ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(),
                    BuildingsIDs = new List<string>()
                },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(expectedMediatorResult.Data.Name, result.Data.Name);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithExpectedDescription()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = new()
                {
                    Name = educationalInstitution.Name,
                    Description = educationalInstitution.Description,
                    LocationID = educationalInstitution.LocationID,
                    JoinDate = educationalInstitution.JoinDate,
                    ParentInstitution = null,
                    ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(),
                    BuildingsIDs = new List<string>()
                },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(expectedMediatorResult.Data.Description, result.Data.Description);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithExpectedLocationID()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = new()
                {
                    Name = educationalInstitution.Name,
                    Description = educationalInstitution.Description,
                    LocationID = educationalInstitution.LocationID,
                    JoinDate = educationalInstitution.JoinDate,
                    ParentInstitution = null,
                    ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(),
                    BuildingsIDs = new List<string>()
                },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(expectedMediatorResult.Data.LocationID, result.Data.LocationId);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithExpectedTimestampJoinDate()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = new()
                {
                    Name = educationalInstitution.Name,
                    Description = educationalInstitution.Description,
                    LocationID = educationalInstitution.LocationID,
                    JoinDate = educationalInstitution.JoinDate,
                    ParentInstitution = null,
                    ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(),
                    BuildingsIDs = new List<string>()
                },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(Timestamp.FromDateTime(expectedMediatorResult.Data.JoinDate), result.Data.JoinDate);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithNullParentInstitution()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = new()
                {
                    Name = educationalInstitution.Name,
                    Description = educationalInstitution.Description,
                    LocationID = educationalInstitution.LocationID,
                    JoinDate = educationalInstitution.JoinDate,
                    ParentInstitution = null,
                    ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(),
                    BuildingsIDs = new List<string>()
                },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Null(result.Data.ParentInstitution);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithEmptyChildInstitutions()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = new()
                {
                    Name = educationalInstitution.Name,
                    Description = educationalInstitution.Description,
                    LocationID = educationalInstitution.LocationID,
                    JoinDate = educationalInstitution.JoinDate,
                    ParentInstitution = null,
                    ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(),
                    BuildingsIDs = new List<string>()
                },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Data.ChildInstitutions);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithEmptyBuildings()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = new()
                {
                    Name = educationalInstitution.Name,
                    Description = educationalInstitution.Description,
                    LocationID = educationalInstitution.LocationID,
                    JoinDate = educationalInstitution.JoinDate,
                    ParentInstitution = null,
                    ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(),
                    BuildingsIDs = new List<string>()
                },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Data.Buildings);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithParentInstitution()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = new()
                {
                    Name = educationalInstitution.Name,
                    Description = educationalInstitution.Description,
                    LocationID = educationalInstitution.LocationID,
                    JoinDate = educationalInstitution.JoinDate,
                    ParentInstitution = new() { EducationalInstitutionID = testDataHelper.EducationalInstitutions[1].EducationalInstitutionID, Name = testDataHelper.EducationalInstitutions[1].Name, Description = testDataHelper.EducationalInstitutions[1].Description },
                    ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(),
                    BuildingsIDs = new List<string>()
                },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.NotNull(result.Data.ParentInstitution);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithExpectedParentInstitutionID()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = new()
                {
                    Name = educationalInstitution.Name,
                    Description = educationalInstitution.Description,
                    LocationID = educationalInstitution.LocationID,
                    JoinDate = educationalInstitution.JoinDate,
                    ParentInstitution = new() { EducationalInstitutionID = testDataHelper.EducationalInstitutions[1].EducationalInstitutionID, Name = testDataHelper.EducationalInstitutions[1].Name, Description = testDataHelper.EducationalInstitutions[1].Description },
                    ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(),
                    BuildingsIDs = new List<string>()
                },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);
            var convertedToGuidID = result.Data.ParentInstitution.EducationalInstitutionId.ToGuid();

            //Assert
            Assert.Equal(testDataHelper.EducationalInstitutions[1].EducationalInstitutionID, convertedToGuidID);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithExpectedParentInstitutionName()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = new()
                {
                    Name = educationalInstitution.Name,
                    Description = educationalInstitution.Description,
                    LocationID = educationalInstitution.LocationID,
                    JoinDate = educationalInstitution.JoinDate,
                    ParentInstitution = new() { EducationalInstitutionID = testDataHelper.EducationalInstitutions[1].EducationalInstitutionID, Name = testDataHelper.EducationalInstitutions[1].Name, Description = testDataHelper.EducationalInstitutions[1].Description },
                    ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(),
                    BuildingsIDs = new List<string>()
                },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(testDataHelper.EducationalInstitutions[1].Name, result.Data.ParentInstitution.Name);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithExpectedParentInstitutionDescription()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = new()
                {
                    Name = educationalInstitution.Name,
                    Description = educationalInstitution.Description,
                    LocationID = educationalInstitution.LocationID,
                    JoinDate = educationalInstitution.JoinDate,
                    ParentInstitution = new() { EducationalInstitutionID = testDataHelper.EducationalInstitutions[1].EducationalInstitutionID, Name = testDataHelper.EducationalInstitutions[1].Name, Description = testDataHelper.EducationalInstitutions[1].Description },
                    ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(),
                    BuildingsIDs = new List<string>()
                },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(testDataHelper.EducationalInstitutions[1].Description, result.Data.ParentInstitution.Description);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithOneChildInstitution()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = new()
                {
                    Name = educationalInstitution.Name,
                    Description = educationalInstitution.Description,
                    LocationID = educationalInstitution.LocationID,
                    JoinDate = educationalInstitution.JoinDate,
                    ParentInstitution = null,
                    ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>() { new() { EducationalInstitutionID = testDataHelper.EducationalInstitutions[1].EducationalInstitutionID, Name = testDataHelper.EducationalInstitutions[1].Name, Description = testDataHelper.EducationalInstitutions[1].Description } },
                    BuildingsIDs = new List<string>()
                },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Single(result.Data.ChildInstitutions);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWitheExpectedChildInstitutionID()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = new()
                {
                    Name = educationalInstitution.Name,
                    Description = educationalInstitution.Description,
                    LocationID = educationalInstitution.LocationID,
                    JoinDate = educationalInstitution.JoinDate,
                    ParentInstitution = null,
                    ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>() { new() { EducationalInstitutionID = testDataHelper.EducationalInstitutions[1].EducationalInstitutionID, Name = testDataHelper.EducationalInstitutions[1].Name, Description = testDataHelper.EducationalInstitutions[1].Description } },
                    BuildingsIDs = new List<string>()
                },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);
            var convertedToGuidID = result.Data.ChildInstitutions[0].EducationalInstitutionId.ToGuid();
            //Assert
            Assert.Equal(testDataHelper.EducationalInstitutions[1].EducationalInstitutionID, convertedToGuidID);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithBuildings()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = new()
                {
                    Name = educationalInstitution.Name,
                    Description = educationalInstitution.Description,
                    LocationID = educationalInstitution.LocationID,
                    JoinDate = educationalInstitution.JoinDate,
                    ParentInstitution = null,
                    ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(),
                    BuildingsIDs = new List<string>() { "building1", "building2" }
                },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(new List<string>() { "building1", "building2" }, result.Data.Buildings);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_WithIdThatDoesntExistInDatabase_ToGetEducationalInstitutionByIDMethod_ShouldReturnDefault()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = null,
                OperationStatus = false,
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Message = $"Educational Institution with the following ID: {id} has not been found!"
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(false);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(new EducationalInstitutionGetResponse(), result);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_WhenAnExceptionIsCaughtInGetEducationalInstitutionByIDQueryHandler_ShouldReturnDefault()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = null,
                OperationStatus = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Message = $"An error occurred while searching for the Educational Institution with the following ID: {id}!"
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(false);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(new EducationalInstitutionGetResponse(), result);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_WhenAnExceptionIsThrownByTheMediator_ShouldReturnDefault()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(false);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(new EducationalInstitutionGetResponse(), result);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionGetByIdRequest_ThatFailsTheRequestValidation_ToGetEducationalInstitutionByIDMethod_ShouldReturnNullData()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = new()
                {
                    Name = educationalInstitution.Name,
                    Description = educationalInstitution.Description,
                    LocationID = educationalInstitution.LocationID,
                    JoinDate = educationalInstitution.JoinDate,
                    ParentInstitution = null,
                    ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(),
                    BuildingsIDs = new List<string>()
                },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(false);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionGetByIdRequest_ThatFailsTheRequestValidation_ToGetEducationalInstitutionByIDMethod_ShouldReturnAnEmptyMessageField()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = new()
                {
                    Name = educationalInstitution.Name,
                    Description = educationalInstitution.Description,
                    LocationID = educationalInstitution.LocationID,
                    JoinDate = educationalInstitution.JoinDate,
                    ParentInstitution = null,
                    ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(),
                    BuildingsIDs = new List<string>()
                },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(false);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionGetByIdRequest_ThatFailsTheRequestValidation_ToGetEducationalInstitutionByIDMethod_ShouldReturnAFalseOperationStatusField()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = new()
                {
                    Name = educationalInstitution.Name,
                    Description = educationalInstitution.Description,
                    LocationID = educationalInstitution.LocationID,
                    JoinDate = educationalInstitution.JoinDate,
                    ParentInstitution = null,
                    ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(),
                    BuildingsIDs = new List<string>()
                },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(false);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionGetByIdRequest_ThatFailsTheRequestValidation_ToGetEducationalInstitutionByIDMethod_ShouldReturnAStatusCodeDefaultField()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.EducationalInstitutionID;

            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = new()
                {
                    Name = educationalInstitution.Name,
                    Description = educationalInstitution.Description,
                    LocationID = educationalInstitution.LocationID,
                    JoinDate = educationalInstitution.JoinDate,
                    ParentInstitution = null,
                    ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(),
                    BuildingsIDs = new List<string>()
                },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(false);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        #endregion GetEducationalInstitutionByIDMethod TESTS

        #region GetAllEducationalInstitutionsByNameMethod TESTS

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByNameRequest_ToGetAllEducationalInstitutionsByNameMethod_ShouldReturnEducationalInstitutionGetByNameResponse()
        {
            //Arrange
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = "testName",
                OffsetValue = 1,
                ResultsCount = 1
            };

            var id = Guid.NewGuid();
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByNameQueryResult>()
            {
                Data = new()
                {
                    EducationalInstitutions = new List<GetEducationalInstitutionQueryResult>() { new() {
                        EducationalInstitutionID = id,
                    Name = "testNameInstitution",
                    Description = "testDescription",
                    LocationID = "testLocation" } }
                },
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.OK
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.IsType<EducationalInstitutionGetByNameResponse>(result);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByNameRequest_ToGetAllEducationalInstitutionsByNameMethod_ShouldReturnACollectionWithOneElement()
        {
            //Arrange
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = "testName",
                OffsetValue = 1,
                ResultsCount = 1
            };

            var id = Guid.NewGuid();
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByNameQueryResult>()
            {
                Data = new()
                {
                    EducationalInstitutions = new List<GetEducationalInstitutionQueryResult>() { new() {
                        EducationalInstitutionID = id,
                    Name = "testNameInstitution",
                    Description = "testDescription",
                    LocationID = "testLocation" } }
                },
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.OK
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Single(result.Data);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByNameRequest_ToGetAllEducationalInstitutionsByNameMethod_ShouldReturnDataWithNameThatContainsRequestName()
        {
            //Arrange
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = "testName",
                OffsetValue = 1,
                ResultsCount = 1
            };

            var id = Guid.NewGuid();
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByNameQueryResult>()
            {
                Data = new()
                {
                    EducationalInstitutions = new List<GetEducationalInstitutionQueryResult>() { new() {
                        EducationalInstitutionID = id,
                    Name = "testNameInstitution",
                    Description = "testDescription",
                    LocationID = "testLocation" } }
                },
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.OK
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Contains(request.Name, result.Data[0].Name);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByNameRequest_ToGetAllEducationalInstitutionsByNameMethod_ShouldReturnDataWithExpectedDescription()
        {
            //Arrange
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = "testName",
                OffsetValue = 1,
                ResultsCount = 1
            };

            var id = Guid.NewGuid();
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByNameQueryResult>()
            {
                Data = new()
                {
                    EducationalInstitutions = new List<GetEducationalInstitutionQueryResult>() { new() {
                        EducationalInstitutionID = id,
                    Name = "testNameInstitution",
                    Description = "testDescription",
                    LocationID = "testLocation" } }
                },
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.OK
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal("testDescription", result.Data[0].Description);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByNameRequest_ToGetAllEducationalInstitutionsByNameMethod_ShouldReturnDataWithExpectedLocationID()
        {
            //Arrange
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = "testName",
                OffsetValue = 1,
                ResultsCount = 1
            };

            var id = Guid.NewGuid();
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByNameQueryResult>()
            {
                Data = new()
                {
                    EducationalInstitutions = new List<GetEducationalInstitutionQueryResult>() { new() {
                        EducationalInstitutionID = id,
                    Name = "testNameInstitution",
                    Description = "testDescription",
                    LocationID = "testLocation" } }
                },
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.OK
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal("testLocation", result.Data[0].LocationId);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByNameRequest_ToGetAllEducationalInstitutionsByNameMethod_ShouldReturnDataWithExpectedEducationalInstitutionID()
        {
            //Arrange
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = "testName",
                OffsetValue = 1,
                ResultsCount = 1
            };

            var id = Guid.NewGuid();
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByNameQueryResult>()
            {
                Data = new()
                {
                    EducationalInstitutions = new List<GetEducationalInstitutionQueryResult>() { new() {
                        EducationalInstitutionID = id,
                    Name = "testNameInstitution",
                    Description = "testDescription",
                    LocationID = "testLocation" } }
                },
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.OK
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(id.ToProtoUuid(), result.Data[0].EducationalInstitutionId);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByNameRequest_ToGetAllEducationalInstitutionsByNameMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = "testName",
                OffsetValue = 1,
                ResultsCount = 1
            };

            var id = Guid.NewGuid();
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByNameQueryResult>()
            {
                Data = new()
                {
                    EducationalInstitutions = new List<GetEducationalInstitutionQueryResult>() { new() {
                        EducationalInstitutionID = id,
                    Name = "testNameInstitution",
                    Description = "testDescription",
                    LocationID = "testLocation" } }
                },
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.OK
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByNameRequest_ToGetAllEducationalInstitutionsByNameMethod_ShouldReturnTrueOperationStatus()
        {
            //Arrange
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = "testName",
                OffsetValue = 1,
                ResultsCount = 1
            };

            var id = Guid.NewGuid();
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByNameQueryResult>()
            {
                Data = new()
                {
                    EducationalInstitutions = new List<GetEducationalInstitutionQueryResult>() { new() {
                        EducationalInstitutionID = id,
                    Name = "testNameInstitution",
                    Description = "testDescription",
                    LocationID = "testLocation" } }
                },
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.OK
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByNameRequest_ToGetAllEducationalInstitutionsByNameMethod_ShouldReturnHttpStatusCodeOK()
        {
            //Arrange
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = "testName",
                OffsetValue = 1,
                ResultsCount = 1
            };

            var id = Guid.NewGuid();
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByNameQueryResult>()
            {
                Data = new()
                {
                    EducationalInstitutions = new List<GetEducationalInstitutionQueryResult>() { new() {
                        EducationalInstitutionID = id,
                    Name = "testNameInstitution",
                    Description = "testDescription",
                    LocationID = "testLocation" } }
                },
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.OK
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Ok, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionGetByNameRequest_ThatFailsValidation_ToGetAllEducationalInstitutionsByNameMethod_ShouldReturnEmptyData()
        {
            //Arrange
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = "",
                OffsetValue = 1,
                ResultsCount = 1
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), out It.Ref<string>.IsAny)).Returns(false);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Data);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionGetByNameRequest_ThatFailsValidation_ToGetAllEducationalInstitutionsByNameMethod_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = "",
                OffsetValue = 1,
                ResultsCount = 1
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), out It.Ref<string>.IsAny)).Returns(false);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionGetByNameRequest_ThatFailsValidation_ToGetAllEducationalInstitutionsByNameMethod_ShouldReturnDefaultStatusCode()
        {
            //Arrange
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = "",
                OffsetValue = 1,
                ResultsCount = 1
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), out It.Ref<string>.IsAny)).Returns(false);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionGetByNameRequest_ThatFailsValidation_ToGetAllEducationalInstitutionsByNameMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = "",
                OffsetValue = 1,
                ResultsCount = 1
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), out It.Ref<string>.IsAny)).Returns(false);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByNameRequest_ToGetAllEducationalInstitutionsByNameMethod_WithNameThatDoesntExistInDatabase_ShouldReturnDefaultStatusCode()
        {
            //Arrange
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = "testName",
                OffsetValue = 1,
                ResultsCount = 1
            };

            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByNameQueryResult>()
            {
                Data = null,
                Message = $"Could not find any Educational Institution with a name like: {request.Name}!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByNameRequest_ToGetAllEducationalInstitutionsByNameMethod_WithNameThatDoesntExistInDatabase_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = "testName",
                OffsetValue = 1,
                ResultsCount = 1
            };

            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByNameQueryResult>()
            {
                Data = null,
                Message = $"Could not find any Educational Institution with a name like: {request.Name}!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByNameRequest_ToGetAllEducationalInstitutionsByNameMethod_WithNameThatDoesntExistInDatabase_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = "testName",
                OffsetValue = 1,
                ResultsCount = 1
            };

            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByNameQueryResult>()
            {
                Data = null,
                Message = $"Could not find any Educational Institution with a name like: {request.Name}!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByNameRequest_ToGetAllEducationalInstitutionsByNameMethod_WithNameThatDoesntExistInDatabase_ShouldReturnEmptyData()
        {
            //Arrange
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = "testName",
                OffsetValue = 1,
                ResultsCount = 1
            };

            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByNameQueryResult>()
            {
                Data = null,
                Message = $"Could not find any Educational Institution with a name like: {request.Name}!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Data);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByNameRequest_ToGetAllEducationalInstitutionsByNameMethod_ExceptionIsCaughtByHandler_ShouldReturnDefaultStatusCode()
        {
            //Arrange
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = "testName",
                OffsetValue = 1,
                ResultsCount = 1
            };

            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByNameQueryResult>()
            {
                Data = null,
                Message = $"An error occurred while searching for any Educational Institution with a name like: {request.Name}!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.InternalServerError
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByNameRequest_ToGetAllEducationalInstitutionsByNameMethod_ExceptionIsCaughtByHandler_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = "testName",
                OffsetValue = 1,
                ResultsCount = 1
            };

            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByNameQueryResult>()
            {
                Data = null,
                Message = $"An error occurred while searching for any Educational Institution with a name like: {request.Name}!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.InternalServerError
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByNameRequest_ToGetAllEducationalInstitutionsByNameMethod_ExceptionIsCaughtByHandler_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = "testName",
                OffsetValue = 1,
                ResultsCount = 1
            };

            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByNameQueryResult>()
            {
                Data = null,
                Message = $"An error occurred while searching for any Educational Institution with a name like: {request.Name}!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.InternalServerError
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByNameRequest_ToGetAllEducationalInstitutionsByNameMethod_ExceptionIsCaughtByHandler_ShouldReturnEmptyData()
        {
            //Arrange
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = "testName",
                OffsetValue = 1,
                ResultsCount = 1
            };

            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByNameQueryResult>()
            {
                Data = null,
                Message = $"An error occurred while searching for any Educational Institution with a name like: {request.Name}!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.InternalServerError
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByNameQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Data);
        }

        #endregion GetAllEducationalInstitutionsByNameMethod TESTS

        #region GetAllEducationalInstitutionsByLocationMethod TESTS

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnEducationalInstitutionsGetByLocationResponse()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByLocationQueryResult>()
            {
                Data = new()
                {
                    EducationalInstitutions = new List<GetEducationalInstitutionByLocationQueryResult>() { new()
                                            { Name="testName",
                                            Description="testDescription",
                                            BuildingsIDs=new List<string>(){"testBuilding"},
                                            EducationalInstitutionID=Guid.NewGuid()
                                            }
                    }
                },
                StatusCode = HttpStatusCode.OK,
                Message = string.Empty,
                OperationStatus = true
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.IsType<EducationalInstitutionsGetByLocationResponse>(result);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnTrueOperationStatus()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByLocationQueryResult>()
            {
                Data = new()
                {
                    EducationalInstitutions = new List<GetEducationalInstitutionByLocationQueryResult>() { new()
                                            { Name="testName",
                                            Description="testDescription",
                                            BuildingsIDs=new List<string>(){"testBuilding"},
                                            EducationalInstitutionID=Guid.NewGuid()
                                            }
                    }
                },
                StatusCode = HttpStatusCode.OK,
                Message = string.Empty,
                OperationStatus = true
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnStatusCodeOK()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByLocationQueryResult>()
            {
                Data = new()
                {
                    EducationalInstitutions = new List<GetEducationalInstitutionByLocationQueryResult>() { new()
                                            { Name="testName",
                                            Description="testDescription",
                                            BuildingsIDs=new List<string>(){"testBuilding"},
                                            EducationalInstitutionID=Guid.NewGuid()
                                            }
                    }
                },
                StatusCode = HttpStatusCode.OK,
                Message = string.Empty,
                OperationStatus = true
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Ok, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByLocationQueryResult>()
            {
                Data = new()
                {
                    EducationalInstitutions = new List<GetEducationalInstitutionByLocationQueryResult>() { new()
                                            { Name="testName",
                                            Description="testDescription",
                                            BuildingsIDs=new List<string>(){"testBuilding"},
                                            EducationalInstitutionID=Guid.NewGuid()
                                            }
                    }
                },
                StatusCode = HttpStatusCode.OK,
                Message = string.Empty,
                OperationStatus = true
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnDataCollectionWithOneElement()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByLocationQueryResult>()
            {
                Data = new()
                {
                    EducationalInstitutions = new List<GetEducationalInstitutionByLocationQueryResult>() { new()
                                            { Name="testName",
                                            Description="testDescription",
                                            BuildingsIDs=new List<string>(){"testBuilding"},
                                            EducationalInstitutionID=Guid.NewGuid()
                                            }
                    }
                },
                StatusCode = HttpStatusCode.OK,
                Message = string.Empty,
                OperationStatus = true
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Single(result.Data);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnDataCollectionWithExpectedName()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByLocationQueryResult>()
            {
                Data = new()
                {
                    EducationalInstitutions = new List<GetEducationalInstitutionByLocationQueryResult>() { new()
                                            { Name="testName",
                                            Description="testDescription",
                                            BuildingsIDs=new List<string>(){"testBuilding"},
                                            EducationalInstitutionID=Guid.NewGuid()
                                            }
                    }
                },
                StatusCode = HttpStatusCode.OK,
                Message = string.Empty,
                OperationStatus = true
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal("testName", result.Data[0].Name);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnDataCollectionWithExpectedEducationalInstitutionID()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };
            var resultEducationalInstitutionID = Guid.NewGuid();
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByLocationQueryResult>()
            {
                Data = new()
                {
                    EducationalInstitutions = new List<GetEducationalInstitutionByLocationQueryResult>() { new()
                                            { Name="testName",
                                            Description="testDescription",
                                            BuildingsIDs=new List<string>(){"testBuilding"},
                                            EducationalInstitutionID=resultEducationalInstitutionID
                                            }
                    }
                },
                StatusCode = HttpStatusCode.OK,
                Message = string.Empty,
                OperationStatus = true
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(resultEducationalInstitutionID, result.Data[0].EducationalInstitutionId.ToGuid());
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnDataCollectionWithExpectedDescription()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByLocationQueryResult>()
            {
                Data = new()
                {
                    EducationalInstitutions = new List<GetEducationalInstitutionByLocationQueryResult>() { new()
                                            { Name="testName",
                                            Description="testDescription",
                                            BuildingsIDs=new List<string>(){"testBuilding"},
                                            EducationalInstitutionID=Guid.NewGuid()
                                            }
                    }
                },
                StatusCode = HttpStatusCode.OK,
                Message = string.Empty,
                OperationStatus = true
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal("testDescription", result.Data[0].Description);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnDataCollectionWithOneElementBuildingsIDsCollection()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByLocationQueryResult>()
            {
                Data = new()
                {
                    EducationalInstitutions = new List<GetEducationalInstitutionByLocationQueryResult>() { new()
                                            { Name="testName",
                                            Description="testDescription",
                                            BuildingsIDs=new List<string>(){"testBuilding"},
                                            EducationalInstitutionID=Guid.NewGuid()
                                            }
                    }
                },
                StatusCode = HttpStatusCode.OK,
                Message = string.Empty,
                OperationStatus = true
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Single(result.Data[0].Buildings);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnDataCollectionWithExpectedBuildingID()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByLocationQueryResult>()
            {
                Data = new()
                {
                    EducationalInstitutions = new List<GetEducationalInstitutionByLocationQueryResult>() { new()
                                            { Name="testName",
                                            Description="testDescription",
                                            BuildingsIDs=new List<string>(){"testBuilding"},
                                            EducationalInstitutionID=Guid.NewGuid()
                                            }
                    }
                },
                StatusCode = HttpStatusCode.OK,
                Message = string.Empty,
                OperationStatus = true
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal("testBuilding", result.Data[0].Buildings[0]);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionsGetByLocationRequest_ThatFailsValidation_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnEmptyData()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = string.Empty };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny)).Returns(false);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Data);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionsGetByLocationRequest_ThatFailsValidation_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = string.Empty };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny)).Returns(false);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionsGetByLocationRequest_ThatFailsValidation_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = string.Empty };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny)).Returns(false);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionsGetByLocationRequest_ThatFailsValidation_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnStatusCodeDefault()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = string.Empty };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny)).Returns(false);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_WithIDThatDoesntExistInDatabase_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnEmptyData()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByLocationQueryResult>()
            {
                Data = new(),
                StatusCode = HttpStatusCode.NotFound,
                Message = $"No Educational Institution with the following LocationID: {request.LocationId} has been found!",
                OperationStatus = false
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Data);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_WithIDThatDoesntExistInDatabase_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByLocationQueryResult>()
            {
                Data = new(),
                StatusCode = HttpStatusCode.NotFound,
                Message = $"No Educational Institution with the following LocationID: {request.LocationId} has been found!",
                OperationStatus = false
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_WithIDThatDoesntExistInDatabase_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByLocationQueryResult>()
            {
                Data = new(),
                StatusCode = HttpStatusCode.NotFound,
                Message = $"No Educational Institution with the following LocationID: {request.LocationId} has been found!",
                OperationStatus = false
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_WithIDThatDoesntExistInDatabase_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnDefaultStatusCode()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByLocationQueryResult>()
            {
                Data = new(),
                StatusCode = HttpStatusCode.NotFound,
                Message = $"No Educational Institution with the following LocationID: {request.LocationId} has been found!",
                OperationStatus = false
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_AnExceptionIsCaughtInHandler_ShouldReturnDefaultStatusCode()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByLocationQueryResult>()
            {
                Data = new(),
                StatusCode = HttpStatusCode.InternalServerError,
                Message = $"An error occurred while searching for the Educational Institution with the following LocationID: {request.LocationId}!",
                OperationStatus = false
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_AnExceptionIsCaughtInHandler_ShouldReturnEmptyData()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByLocationQueryResult>()
            {
                Data = new(),
                StatusCode = HttpStatusCode.InternalServerError,
                Message = $"An error occurred while searching for the Educational Institution with the following LocationID: {request.LocationId}!",
                OperationStatus = false
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Data);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_AnExceptionIsCaughtInHandler_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByLocationQueryResult>()
            {
                Data = new(),
                StatusCode = HttpStatusCode.InternalServerError,
                Message = $"An error occurred while searching for the Educational Institution with the following LocationID: {request.LocationId}!",
                OperationStatus = false
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_AnExceptionIsCaughtInHandler_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByLocationQueryResult>()
            {
                Data = new(),
                StatusCode = HttpStatusCode.InternalServerError,
                Message = $"An error occurred while searching for the Educational Institution with the following LocationID: {request.LocationId}!",
                OperationStatus = false
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionsByLocationQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        #endregion GetAllEducationalInstitutionsByLocationMethod TESTS
    }
}