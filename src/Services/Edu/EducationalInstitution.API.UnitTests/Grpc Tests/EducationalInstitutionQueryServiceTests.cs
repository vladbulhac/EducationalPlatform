using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Queries;
using EducationalInstitutionAPI.Grpc;
using EducationalInstitutionAPI.Proto;
using EducationalInstitutionAPI.Utils;
using Google.Protobuf.WellKnownTypes;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.UnitTests.Grpc_Tests
{
    public class EducationalInstitutionQueryServiceTests : IClassFixture<MockDependenciesHelper<EducationalInstitutionQueryService>>, IClassFixture<TestDataFromJSONParser>
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
    }
}