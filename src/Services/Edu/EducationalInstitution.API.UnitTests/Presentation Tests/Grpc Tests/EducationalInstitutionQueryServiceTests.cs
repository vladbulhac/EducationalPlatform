using EducationalInstitution.API.Tests.Shared;
using EducationalInstitution.Application;
using EducationalInstitution.Application.Queries;
using EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results;
using EducationalInstitutionAPI.Presentation.Grpc;
using EducationalInstitutionAPI.Proto;
using EducationalInstitutionAPI.Utils.Mappers;
using Google.Protobuf.WellKnownTypes;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Domain = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.API.UnitTests.Presentation_Tests.Grpc_Tests
{
    public class EducationalInstitutionQueryServiceTests : SetupGrpcServicesHelper<EducationalInstitutionQueryService>,
                                                           IClassFixture<MockDependenciesHelper<EducationalInstitutionQueryService>>,
                                                           IClassFixture<TestDataFromJSONParser>
    {
        private readonly EducationalInstitutionQueryService service;

        private readonly TestDataFromJSONParser testDataHelper;

        /// <remarks>Called before each test</remarks>
        public EducationalInstitutionQueryServiceTests(MockDependenciesHelper<EducationalInstitutionQueryService> dependenciesHelper, TestDataFromJSONParser testDataHelper) : base(dependenciesHelper)
        {
            this.testDataHelper = testDataHelper;
            service = new(dependenciesHelper.mockMediator.Object,
                          dependenciesHelper.mockLogger.Object,
                          dependenciesHelper.mockValidationHandler.Object);
        }

        #region GetEducationalInstitutionByIDMethod TESTS

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnAnEducationalInstitutionGetResponse()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            GetEducationalInstitutionByIDMethod_Setup(educationalInstitution);

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.IsType<EducationalInstitutionGetResponse>(result);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnATrueOperationStatusField()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            GetEducationalInstitutionByIDMethod_Setup(educationalInstitution);

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnAnEmptyMessageField()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            GetEducationalInstitutionByIDMethod_Setup(educationalInstitution);

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnAStatusCodeCreatedField()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            GetEducationalInstitutionByIDMethod_Setup(educationalInstitution);

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithExpectedName()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            GetEducationalInstitutionByIDMethod_Setup(educationalInstitution);

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(educationalInstitution.Name, result.Data.Name);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithExpectedDescription()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            GetEducationalInstitutionByIDMethod_Setup(educationalInstitution);

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(educationalInstitution.Description, result.Data.Description);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithExpectedLocationID()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            GetEducationalInstitutionByIDMethod_Setup(educationalInstitution);

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(educationalInstitution.LocationID, result.Data.LocationId);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithExpectedTimestampJoinDate()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            GetEducationalInstitutionByIDMethod_Setup(educationalInstitution);

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(Timestamp.FromDateTime(educationalInstitution.JoinDate), result.Data.JoinDate);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithNullParentInstitution()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            GetEducationalInstitutionByIDMethod_Setup(educationalInstitution);

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Null(result.Data.ParentInstitution);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithEmptyChildInstitutions()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            GetEducationalInstitutionByIDMethod_Setup(educationalInstitution);

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Data.ChildInstitutions);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithEmptyBuildings()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            GetEducationalInstitutionByIDMethod_Setup(educationalInstitution);

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Data.Buildings);
        }

        private void GetEducationalInstitutionByIDMethod_Setup(Domain::EducationalInstitution educationalInstitution)
        {
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
                StatusCode = HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<GetEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<GetEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithParentInstitution()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            GetEducationalInstitutionByIDMethod_SetupWithParentInstitution(educationalInstitution);

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.NotNull(result.Data.ParentInstitution);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithExpectedParentInstitutionID()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            GetEducationalInstitutionByIDMethod_SetupWithParentInstitution(educationalInstitution);

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);
            var convertedToGuidID = result.Data.ParentInstitution.EducationalInstitutionId.ToGuid();

            //Assert
            Assert.Equal(testDataHelper.EducationalInstitutions[1].Id, convertedToGuidID);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithExpectedParentInstitutionName()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            GetEducationalInstitutionByIDMethod_SetupWithParentInstitution(educationalInstitution);

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(testDataHelper.EducationalInstitutions[1].Name, result.Data.ParentInstitution.Name);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithExpectedParentInstitutionDescription()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            GetEducationalInstitutionByIDMethod_SetupWithParentInstitution(educationalInstitution);

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(testDataHelper.EducationalInstitutions[1].Description, result.Data.ParentInstitution.Description);
        }

        private void GetEducationalInstitutionByIDMethod_SetupWithParentInstitution(Domain::EducationalInstitution educationalInstitution)
        {
            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = new()
                {
                    Name = educationalInstitution.Name,
                    Description = educationalInstitution.Description,
                    LocationID = educationalInstitution.LocationID,
                    JoinDate = educationalInstitution.JoinDate,
                    ParentInstitution = new() { EducationalInstitutionID = testDataHelper.EducationalInstitutions[1].Id, Name = testDataHelper.EducationalInstitutions[1].Name, Description = testDataHelper.EducationalInstitutions[1].Description },
                    ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(),
                    BuildingsIDs = new List<string>()
                },
                OperationStatus = true,
                StatusCode = HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<GetEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<GetEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithOneChildInstitution()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            GetEducationalInstitutionByIDMethod_SetupWithChildInstitutions(educationalInstitution);

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Single(result.Data.ChildInstitutions);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWitheExpectedChildInstitutionID()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            GetEducationalInstitutionByIDMethod_SetupWithChildInstitutions(educationalInstitution);

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);
            var convertedToGuidID = result.Data.ChildInstitutions[0].EducationalInstitutionId.ToGuid();

            //Assert
            Assert.Equal(testDataHelper.EducationalInstitutions[1].Id, convertedToGuidID);
        }

        private void GetEducationalInstitutionByIDMethod_SetupWithChildInstitutions(Domain::EducationalInstitution educationalInstitution)
        {
            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = new()
                {
                    Name = educationalInstitution.Name,
                    Description = educationalInstitution.Description,
                    LocationID = educationalInstitution.LocationID,
                    JoinDate = educationalInstitution.JoinDate,
                    ParentInstitution = null,
                    ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>() { new() { EducationalInstitutionID = testDataHelper.EducationalInstitutions[1].Id, Name = testDataHelper.EducationalInstitutions[1].Name, Description = testDataHelper.EducationalInstitutions[1].Description } },
                    BuildingsIDs = new List<string>()
                },
                OperationStatus = true,
                StatusCode = HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<GetEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<GetEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_ShouldReturnDataWithBuildings()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;
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
                StatusCode = HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<GetEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<GetEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService service = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(new List<string>() { "building1", "building2" }, result.Data.Buildings);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_WithIdThatDoesntExistInDatabase_ToGetEducationalInstitutionByIDMethod_ShouldReturnDefault()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = null,
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound,
                Message = $"Educational Institution with the following ID: {id} has not been found!"
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<GetEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(false);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<GetEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService service = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(new EducationalInstitutionGetResponse(), result);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_WhenAnExceptionIsCaughtInGetEducationalInstitutionByIDQueryHandler_ShouldReturnDefault()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            var expectedMediatorResult = new Response<GetEducationalInstitutionByIDQueryResult>()
            {
                Data = null,
                OperationStatus = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = $"An error occurred while searching for the Educational Institution with the following ID: {id}!"
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<GetEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(false);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<GetEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionQueryService service = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(new EducationalInstitutionGetResponse(), result);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionGetByIdRequest_ToGetEducationalInstitutionByIDMethod_WhenAnExceptionIsThrownByTheMediator_ShouldReturnDefault()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<GetEducationalInstitutionByIDQuery>(), out It.Ref<string>.IsAny)).Returns(false);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<GetEducationalInstitutionByIDQuery>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

            EducationalInstitutionQueryService service = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(new EducationalInstitutionGetResponse(), result);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionGetByIdRequest_ThatFailsTheRequestValidation_ToGetEducationalInstitutionByIDMethod_ShouldReturnNullData()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            SetupMockedDependenciesToFailValidation<GetEducationalInstitutionByIDQuery>();

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionGetByIdRequest_ThatFailsTheRequestValidation_ToGetEducationalInstitutionByIDMethod_ShouldReturnAnEmptyMessageField()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            SetupMockedDependenciesToFailValidation<GetEducationalInstitutionByIDQuery>();

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionGetByIdRequest_ThatFailsTheRequestValidation_ToGetEducationalInstitutionByIDMethod_ShouldReturnAFalseOperationStatusField()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;
            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            SetupMockedDependenciesToFailValidation<GetEducationalInstitutionByIDQuery>();

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionGetByIdRequest_ThatFailsTheRequestValidation_ToGetEducationalInstitutionByIDMethod_ShouldReturnAStatusCodeDefaultField()
        {
            //Arrange
            var educationalInstitution = testDataHelper.EducationalInstitutions[0];
            var id = educationalInstitution.Id;

            EducationalInstitutionGetByIdRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            SetupMockedDependenciesToFailValidation<GetEducationalInstitutionByIDQuery>();

            //Act
            var result = await service.GetEducationalInstitutionByID(request, dependenciesHelper.mockServerCallContext.Object);

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

            GetAllEducationalInstitutionsByNameMethod_Setup(id);

            //Act
            var result = await service.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

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

            GetAllEducationalInstitutionsByNameMethod_Setup(id);

            //Act
            var result = await service.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

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

            GetAllEducationalInstitutionsByNameMethod_Setup(id);

            //Act
            var result = await service.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

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

            GetAllEducationalInstitutionsByNameMethod_Setup(id);

            //Act
            var result = await service.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

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

            GetAllEducationalInstitutionsByNameMethod_Setup(id);

            //Act
            var result = await service.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

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

            GetAllEducationalInstitutionsByNameMethod_Setup(id);

            //Act
            var result = await service.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

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

            GetAllEducationalInstitutionsByNameMethod_Setup(id);

            //Act
            var result = await service.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

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

            GetAllEducationalInstitutionsByNameMethod_Setup(id);

            //Act
            var result = await service.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

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

            GetAllEducationalInstitutionsByNameMethod_Setup(id);

            //Act
            var result = await service.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Ok, result.StatusCode);
        }

        private void GetAllEducationalInstitutionsByNameMethod_Setup(Guid id)
        {
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

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<GetAllEducationalInstitutionsByNameQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<GetAllEducationalInstitutionsByNameQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);
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

            SetupMockedDependenciesToFailValidation<GetAllEducationalInstitutionsByNameQuery>();

            //Act
            var result = await service.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

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

            SetupMockedDependenciesToFailValidation<GetAllEducationalInstitutionsByNameQuery>();

            //Act
            var result = await service.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

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

            SetupMockedDependenciesToFailValidation<GetAllEducationalInstitutionsByNameQuery>();

            //Act
            var result = await service.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

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

            SetupMockedDependenciesToFailValidation<GetAllEducationalInstitutionsByNameQuery>();

            //Act
            var result = await service.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

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

            GetAllEducationalInstitutionsByNameMethod_SetupToNotFindEntity(request.Name);

            //Act
            var result = await service.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

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

            GetAllEducationalInstitutionsByNameMethod_SetupToNotFindEntity(request.Name);

            //Act
            var result = await service.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

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

            GetAllEducationalInstitutionsByNameMethod_SetupToNotFindEntity(request.Name);

            //Act
            var result = await service.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

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

            GetAllEducationalInstitutionsByNameMethod_SetupToNotFindEntity(request.Name);

            //Act
            var result = await service.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Data);
        }

        private void GetAllEducationalInstitutionsByNameMethod_SetupToNotFindEntity(string name)
        {
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByNameQueryResult>()
            {
                Data = null,
                Message = $"Could not find any Educational Institution with a name like: {name}!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<GetAllEducationalInstitutionsByNameQuery>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<GetAllEducationalInstitutionsByNameQuery>(), It.IsAny<CancellationToken>()))
                                           .ReturnsAsync(expectedMediatorResult);
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

            GetAllEducationalInstitutionsByNameMethod_Setup(request.Name);

            //Act
            var result = await service.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

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

            GetAllEducationalInstitutionsByNameMethod_Setup(request.Name);

            //Act
            var result = await service.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

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

            GetAllEducationalInstitutionsByNameMethod_Setup(request.Name);

            //Act
            var result = await service.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

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

            GetAllEducationalInstitutionsByNameMethod_Setup(request.Name);

            //Act
            var result = await service.GetAllEducationalInstitutionsByName(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Data);
        }

        private void GetAllEducationalInstitutionsByNameMethod_Setup(string name)
        {
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByNameQueryResult>()
            {
                Data = null,
                Message = $"An error occurred while searching for any Educational Institution with a name like: {name}!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.InternalServerError
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<GetAllEducationalInstitutionsByNameQuery>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<GetAllEducationalInstitutionsByNameQuery>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);
        }

        #endregion GetAllEducationalInstitutionsByNameMethod TESTS

        #region GetAllEducationalInstitutionsByLocationMethod TESTS

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnEducationalInstitutionsGetByLocationResponse()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };

            GetAllEducationalInstitutionsByLocationMethod_Setup(Guid.NewGuid());

            //Act
            var result = await service.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.IsType<EducationalInstitutionsGetByLocationResponse>(result);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnTrueOperationStatus()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };

            GetAllEducationalInstitutionsByLocationMethod_Setup(Guid.NewGuid());

            //Act
            var result = await service.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnStatusCodeOK()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };

            GetAllEducationalInstitutionsByLocationMethod_Setup(Guid.NewGuid());

            //Act
            var result = await service.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Ok, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };

            GetAllEducationalInstitutionsByLocationMethod_Setup(Guid.NewGuid());

            //Act
            var result = await service.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnDataCollectionWithOneElement()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };

            GetAllEducationalInstitutionsByLocationMethod_Setup(Guid.NewGuid());

            //Act
            var result = await service.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Single(result.Data);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnDataCollectionWithExpectedName()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };

            GetAllEducationalInstitutionsByLocationMethod_Setup(Guid.NewGuid());

            //Act
            var result = await service.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal("testName", result.Data[0].Name);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnDataCollectionWithExpectedEducationalInstitutionID()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };
            var id = Guid.NewGuid();

            GetAllEducationalInstitutionsByLocationMethod_Setup(id);

            //Act
            var result = await service.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(id, result.Data[0].EducationalInstitutionId.ToGuid());
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnDataCollectionWithExpectedDescription()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };

            GetAllEducationalInstitutionsByLocationMethod_Setup(Guid.NewGuid());

            //Act
            var result = await service.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal("testDescription", result.Data[0].Description);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnDataCollectionWithOneElementBuildingsIDsCollection()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };

            GetAllEducationalInstitutionsByLocationMethod_Setup(Guid.NewGuid());

            //Act
            var result = await service.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Single(result.Data[0].Buildings);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnDataCollectionWithExpectedBuildingID()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };

            GetAllEducationalInstitutionsByLocationMethod_Setup(Guid.NewGuid());

            //Act
            var result = await service.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal("testBuilding", result.Data[0].Buildings[0]);
        }

        private void GetAllEducationalInstitutionsByLocationMethod_Setup(Guid educationalInstitutionID)
        {
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByLocationQueryResult>()
            {
                Data = new()
                {
                    EducationalInstitutions = new List<GetEducationalInstitutionByLocationQueryResult>() { new()
                                            { Name="testName",
                                            Description="testDescription",
                                            BuildingsIDs=new List<string>(){"testBuilding"},
                                            EducationalInstitutionID=educationalInstitutionID
                                            }
                    }
                },
                StatusCode = HttpStatusCode.OK,
                Message = string.Empty,
                OperationStatus = true
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<GetAllEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<GetAllEducationalInstitutionsByLocationQuery>(), It.IsAny<CancellationToken>()))
                                           .ReturnsAsync(expectedMediatorResult);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionsGetByLocationRequest_ThatFailsValidation_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnEmptyData()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = string.Empty };

            SetupMockedDependenciesToFailValidation<GetAllEducationalInstitutionsByLocationQuery>();

            //Act
            var result = await service.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Data);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionsGetByLocationRequest_ThatFailsValidation_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = string.Empty };

            SetupMockedDependenciesToFailValidation<GetAllEducationalInstitutionsByLocationQuery>();

            //Act
            var result = await service.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionsGetByLocationRequest_ThatFailsValidation_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = string.Empty };

            SetupMockedDependenciesToFailValidation<GetAllEducationalInstitutionsByLocationQuery>();

            //Act
            var result = await service.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionsGetByLocationRequest_ThatFailsValidation_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnStatusCodeDefault()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = string.Empty };

            SetupMockedDependenciesToFailValidation<GetAllEducationalInstitutionsByLocationQuery>();

            //Act
            var result = await service.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_WithIDThatDoesntExistInDatabase_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnEmptyData()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };

            GetAllEducationalInstitutionsByLocationMethod_SetupToNotFindEntity(request.LocationId);

            //Act
            var result = await service.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Data);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_WithIDThatDoesntExistInDatabase_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };

            GetAllEducationalInstitutionsByLocationMethod_SetupToNotFindEntity(request.LocationId);

            //Act
            var result = await service.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_WithIDThatDoesntExistInDatabase_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };

            GetAllEducationalInstitutionsByLocationMethod_SetupToNotFindEntity(request.LocationId);

            //Act
            var result = await service.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_WithIDThatDoesntExistInDatabase_ToGetAllEducationalInstitutionsByLocationMethod_ShouldReturnDefaultStatusCode()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };

            GetAllEducationalInstitutionsByLocationMethod_SetupToNotFindEntity(request.LocationId);

            //Act
            var result = await service.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        private void GetAllEducationalInstitutionsByLocationMethod_SetupToNotFindEntity(string locationID)
        {
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByLocationQueryResult>()
            {
                Data = new(),
                StatusCode = HttpStatusCode.NotFound,
                Message = $"No Educational Institution with the following LocationID: {locationID} has been found!",
                OperationStatus = false
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<GetAllEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<GetAllEducationalInstitutionsByLocationQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_AnExceptionIsCaughtInHandler_ShouldReturnDefaultStatusCode()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };

            GetAllEducationalInstitutionsByLocationMethod_Setup(request.LocationId);

            //Act
            var result = await service.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_AnExceptionIsCaughtInHandler_ShouldReturnEmptyData()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };

            GetAllEducationalInstitutionsByLocationMethod_Setup(request.LocationId);

            //Act
            var result = await service.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Data);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_AnExceptionIsCaughtInHandler_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };

            GetAllEducationalInstitutionsByLocationMethod_Setup(request.LocationId);

            //Act
            var result = await service.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByLocationRequest_ToGetAllEducationalInstitutionsByLocationMethod_AnExceptionIsCaughtInHandler_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            EducationalInstitutionsGetByLocationRequest request = new() { LocationId = "location123" };

            GetAllEducationalInstitutionsByLocationMethod_Setup(request.LocationId);

            //Act
            var result = await service.GetAllEducationalInstitutionsByLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        private void GetAllEducationalInstitutionsByLocationMethod_Setup(string locationID)
        {
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsByLocationQueryResult>()
            {
                Data = new(),
                StatusCode = HttpStatusCode.InternalServerError,
                Message = $"An error occurred while searching for the Educational Institution with the following LocationID: {locationID}!",
                OperationStatus = false
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<GetAllEducationalInstitutionsByLocationQuery>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<GetAllEducationalInstitutionsByLocationQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);
        }

        #endregion GetAllEducationalInstitutionsByLocationMethod TESTS

        #region GetAllEducationalInstitutionsByBuildingMethod TESTS

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByBuildingRequest_ToGetAllEducationalInstitutionsByBuildingMethod_ShouldReturnEducationalInstitutionsGetByBuildingResponseType()
        {
            //Arrange
            EducationalInstitutionsGetByBuildingRequest request = new() { BuildingId = "building123" };

            GetAllEducationalInstitutionsByBuildingMethod_Setup(out _);

            //Act
            var result = await service.GetAllEducationalInstitutionsByBuilding(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.IsType<EducationalInstitutionsGetByBuildingResponse>(result);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByBuildingRequest_ToGetAllEducationalInstitutionsByBuildingMethod_ShouldReturnTrueOperationStatus()
        {
            //Arrange
            EducationalInstitutionsGetByBuildingRequest request = new() { BuildingId = "building123" };

            GetAllEducationalInstitutionsByBuildingMethod_Setup(out _);

            //Act
            var result = await service.GetAllEducationalInstitutionsByBuilding(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByBuildingRequest_ToGetAllEducationalInstitutionsByBuildingMethod_ShouldReturnStatusCodeOK()
        {
            //Arrange
            EducationalInstitutionsGetByBuildingRequest request = new() { BuildingId = "building123" };

            GetAllEducationalInstitutionsByBuildingMethod_Setup(out _);

            //Act
            var result = await service.GetAllEducationalInstitutionsByBuilding(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Ok, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByBuildingRequest_ToGetAllEducationalInstitutionsByBuildingMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionsGetByBuildingRequest request = new() { BuildingId = "building123" };

            GetAllEducationalInstitutionsByBuildingMethod_Setup(out _);

            //Act
            var result = await service.GetAllEducationalInstitutionsByBuilding(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByBuildingRequest_ToGetAllEducationalInstitutionsByBuildingMethod_ShouldReturnCollectionWithOneItem()
        {
            //Arrange
            EducationalInstitutionsGetByBuildingRequest request = new() { BuildingId = "building123" };

            GetAllEducationalInstitutionsByBuildingMethod_Setup(out _);

            //Act
            var result = await service.GetAllEducationalInstitutionsByBuilding(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Single(result.Data);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByBuildingRequest_ToGetAllEducationalInstitutionsByBuildingMethod_ShouldReturnExpectedEducationalInstitutionID()
        {
            //Arrange
            EducationalInstitutionsGetByBuildingRequest request = new() { BuildingId = "building123" };

            GetAllEducationalInstitutionsByBuildingMethod_Setup(out Response<GetAllEducationalInstitutionsWithSameBuildingQueryResult> expectedMediatorResult);

            //Act
            var result = await service.GetAllEducationalInstitutionsByBuilding(request, dependenciesHelper.mockServerCallContext.Object);
            var expectedID = expectedMediatorResult.Data
                                                   .EducationalInstitutions
                                                   .ElementAt(0)
                                                   .EducationalInstitutionID
                                                   .ToProtoUuid();

            //Assert
            Assert.Equal(expectedID, result.Data[0].EducationalInstitutionId);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByBuildingRequest_ToGetAllEducationalInstitutionsByBuildingMethod_ShouldReturnExpectedName()
        {
            //Arrange
            EducationalInstitutionsGetByBuildingRequest request = new() { BuildingId = "building123" };

            GetAllEducationalInstitutionsByBuildingMethod_Setup(out Response<GetAllEducationalInstitutionsWithSameBuildingQueryResult> expectedMediatorResult);

            //Act
            var result = await service.GetAllEducationalInstitutionsByBuilding(request, dependenciesHelper.mockServerCallContext.Object);
            var expectedName = expectedMediatorResult.Data
                                                   .EducationalInstitutions
                                                   .ElementAt(0)
                                                   .Name;

            //Assert
            Assert.Equal(expectedName, result.Data[0].Name);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByBuildingRequest_ToGetAllEducationalInstitutionsByBuildingMethod_ShouldReturnExpectedDescription()
        {
            //Arrange
            EducationalInstitutionsGetByBuildingRequest request = new() { BuildingId = "building123" };

            GetAllEducationalInstitutionsByBuildingMethod_Setup(out Response<GetAllEducationalInstitutionsWithSameBuildingQueryResult> expectedMediatorResult);

            //Act
            var result = await service.GetAllEducationalInstitutionsByBuilding(request, dependenciesHelper.mockServerCallContext.Object);
            var expectedDescription = expectedMediatorResult.Data
                                                   .EducationalInstitutions
                                                   .ElementAt(0)
                                                   .Description;

            //Assert
            Assert.Equal(expectedDescription, result.Data[0].Description);
        }

        private void GetAllEducationalInstitutionsByBuildingMethod_Setup(out Response<GetAllEducationalInstitutionsWithSameBuildingQueryResult> expectedMediatorResult)
        {
            expectedMediatorResult = new()
            {
                Data = new()
                {
                    EducationalInstitutions = new List<EducationalInstitutionBaseQueryResult>
                        {
                            new()
                            {
                                EducationalInstitutionID=Guid.NewGuid(),
                                Name="testName",
                                Description="testDescription"
                            }
                        }
                },
                StatusCode = HttpStatusCode.OK,
                OperationStatus = true,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<GetAllEducationalInstitutionsByBuildingQuery>(),
                                                                                              out It.Ref<string>.IsAny))
                                                    .Returns(true);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<GetAllEducationalInstitutionsByBuildingQuery>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionsGetByBuildingRequest_ThatFailsValidation_ToGetAllEducationalInstitutionsByBuildingMethod_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            EducationalInstitutionsGetByBuildingRequest request = new();

            SetupMockedDependenciesToFailValidation<GetAllEducationalInstitutionsByBuildingQuery>();

            //Act
            var result = await service.GetAllEducationalInstitutionsByBuilding(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionsGetByBuildingRequest_ThatFailsValidation_ToGetAllEducationalInstitutionsByBuildingMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionsGetByBuildingRequest request = new();

            SetupMockedDependenciesToFailValidation<GetAllEducationalInstitutionsByBuildingQuery>();

            //Act
            var result = await service.GetAllEducationalInstitutionsByBuilding(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionsGetByBuildingRequest_ThatFailsValidation_ToGetAllEducationalInstitutionsByBuildingMethod_ShouldReturnStatusCodeDefault()
        {
            //Arrange
            EducationalInstitutionsGetByBuildingRequest request = new();

            SetupMockedDependenciesToFailValidation<GetAllEducationalInstitutionsByBuildingQuery>();

            //Act
            var result = await service.GetAllEducationalInstitutionsByBuilding(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionsGetByBuildingRequest_ThatFailsValidation_ToGetAllEducationalInstitutionsByBuildingMethod_ShouldReturnEmptyData()
        {
            //Arrange
            EducationalInstitutionsGetByBuildingRequest request = new();

            SetupMockedDependenciesToFailValidation<GetAllEducationalInstitutionsByBuildingQuery>();

            //Act
            var result = await service.GetAllEducationalInstitutionsByBuilding(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Data);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByBuildingRequest_ToGetAllEducationalInstitutionsByBuildingMethod_IDIsDNotFoundInDatabase_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            EducationalInstitutionsGetByBuildingRequest request = new() { BuildingId = "building123" };

            GetAllEducationalInstitutionsByBuildingMethod_SetupToNotFindAnyEntity(request.BuildingId);

            //Act
            var result = await service.GetAllEducationalInstitutionsByBuilding(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByBuildingRequest_ToGetAllEducationalInstitutionsByBuildingMethod_IDIsDNotFoundInDatabase_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionsGetByBuildingRequest request = new() { BuildingId = "building123" };

            GetAllEducationalInstitutionsByBuildingMethod_SetupToNotFindAnyEntity(request.BuildingId);

            //Act
            var result = await service.GetAllEducationalInstitutionsByBuilding(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByBuildingRequest_ToGetAllEducationalInstitutionsByBuildingMethod_IDIsDNotFoundInDatabase_ShouldReturnDefaultStatusCode()
        {
            //Arrange
            EducationalInstitutionsGetByBuildingRequest request = new() { BuildingId = "building123" };

            GetAllEducationalInstitutionsByBuildingMethod_SetupToNotFindAnyEntity(request.BuildingId);

            //Act
            var result = await service.GetAllEducationalInstitutionsByBuilding(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByBuildingRequest_ToGetAllEducationalInstitutionsByBuildingMethod_IDIsDNotFoundInDatabase_ShouldReturnEmptyData()
        {
            //Arrange
            EducationalInstitutionsGetByBuildingRequest request = new() { BuildingId = "building123" };

            GetAllEducationalInstitutionsByBuildingMethod_SetupToNotFindAnyEntity(request.BuildingId);

            //Act
            var result = await service.GetAllEducationalInstitutionsByBuilding(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Data);
        }

        private void GetAllEducationalInstitutionsByBuildingMethod_SetupToNotFindAnyEntity(string buildingID)
        {
            var expectedMediatorResult = new Response<GetAllEducationalInstitutionsWithSameBuildingQueryResult>
            {
                Data = new()
                {
                    EducationalInstitutions = new List<EducationalInstitutionBaseQueryResult>(0)
                },
                StatusCode = HttpStatusCode.NotFound,
                OperationStatus = false,
                Message = $"No Educational Institution that has a building: {buildingID} has been found!"
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<GetAllEducationalInstitutionsByBuildingQuery>(),
                                                                                              out It.Ref<string>.IsAny))
                                                    .Returns(true);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<GetAllEducationalInstitutionsByBuildingQuery>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByBuildingRequest_ToGetAllEducationalInstitutionsByBuildingMethod_AnExceptionIsCaught_ShouldReturnEmptyData()
        {
            //Arrange
            EducationalInstitutionsGetByBuildingRequest request = new() { BuildingId = "building123" };

            GetAllEducationalInstitutionsByBuildingMethod_SetupMediatorToThrowException();

            //Act
            var result = await service.GetAllEducationalInstitutionsByBuilding(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Data);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByBuildingRequest_ToGetAllEducationalInstitutionsByBuildingMethod_AnExceptionIsCaught_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            EducationalInstitutionsGetByBuildingRequest request = new() { BuildingId = "building123" };

            GetAllEducationalInstitutionsByBuildingMethod_SetupMediatorToThrowException();

            //Act
            var result = await service.GetAllEducationalInstitutionsByBuilding(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByBuildingRequest_ToGetAllEducationalInstitutionsByBuildingMethod_AnExceptionIsCaught_ShouldReturnDefaultStatusCode()
        {
            //Arrange
            EducationalInstitutionsGetByBuildingRequest request = new() { BuildingId = "building123" };

            GetAllEducationalInstitutionsByBuildingMethod_SetupMediatorToThrowException();

            //Act
            var result = await service.GetAllEducationalInstitutionsByBuilding(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionsGetByBuildingRequest_ToGetAllEducationalInstitutionsByBuildingMethod_AnExceptionIsCaught_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionsGetByBuildingRequest request = new() { BuildingId = "building123" };

            GetAllEducationalInstitutionsByBuildingMethod_SetupMediatorToThrowException();

            //Act
            var result = await service.GetAllEducationalInstitutionsByBuilding(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        private void GetAllEducationalInstitutionsByBuildingMethod_SetupMediatorToThrowException()
        {
            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<GetAllEducationalInstitutionsByBuildingQuery>(),
                                                                                              out It.Ref<string>.IsAny))
                                                    .Returns(true);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<GetAllEducationalInstitutionsByBuildingQuery>(), It.IsAny<CancellationToken>()))
                                            .ThrowsAsync(new ArgumentNullException("test"));
        }

        #endregion GetAllEducationalInstitutionsByBuildingMethod TESTS

        #region GetAllAdminsByEducationalInstitutionIDMethod TESTS

        [Fact]
        public async Task GivenAValidAdminsGetByEducationalInstitutionIdRequest_ToGetAllAdminsByEducationalInstitutionIDMethod_ShouldReturnAdminsGetByEducationalInstitutionIdResponseType()
        {
            //Arrange
            AdminsGetByEducationalInstitutionIdRequest request = new()
            { EducationalInstitutionId = Guid.NewGuid().ToProtoUuid() };

            GetAllAdminsByEducationalInstitutionIDMethod_Setup(out Response<GetAllAdminsOfEducationalInstitutionQueryResult> expectedMediatorResult);

            //Act
            var result = await service.GetAllAdminsByEducationalInstitutionID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.IsType<AdminsGetByEducationalInstitutionIdResponse>(result);
        }

        [Fact]
        public async Task GivenAValidAdminsGetByEducationalInstitutionIdRequest_ToGetAllAdminsByEducationalInstitutionIDMethod_ShouldReturnTrueOperationStatus()
        {
            //Arrange
            AdminsGetByEducationalInstitutionIdRequest request = new()
            { EducationalInstitutionId = Guid.NewGuid().ToProtoUuid() };

            GetAllAdminsByEducationalInstitutionIDMethod_Setup(out Response<GetAllAdminsOfEducationalInstitutionQueryResult> expectedMediatorResult);

            //Act
            var result = await service.GetAllAdminsByEducationalInstitutionID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidAdminsGetByEducationalInstitutionIdRequest_ToGetAllAdminsByEducationalInstitutionIDMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            AdminsGetByEducationalInstitutionIdRequest request = new()
            { EducationalInstitutionId = Guid.NewGuid().ToProtoUuid() };

            GetAllAdminsByEducationalInstitutionIDMethod_Setup(out Response<GetAllAdminsOfEducationalInstitutionQueryResult> expectedMediatorResult);

            //Act
            var result = await service.GetAllAdminsByEducationalInstitutionID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidAdminsGetByEducationalInstitutionIdRequest_ToGetAllAdminsByEducationalInstitutionIDMethod_ShouldReturnStatusCodeOK()
        {
            //Arrange
            AdminsGetByEducationalInstitutionIdRequest request = new()
            { EducationalInstitutionId = Guid.NewGuid().ToProtoUuid() };

            GetAllAdminsByEducationalInstitutionIDMethod_Setup(out Response<GetAllAdminsOfEducationalInstitutionQueryResult> expectedMediatorResult);

            //Act
            var result = await service.GetAllAdminsByEducationalInstitutionID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(HttpStatusCode.OK.ToProtoHttpStatusCode(), result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidAdminsGetByEducationalInstitutionIdRequest_ToGetAllAdminsByEducationalInstitutionIDMethod_ShouldReturnExpectedAdminsIDsCollection()
        {
            //Arrange
            AdminsGetByEducationalInstitutionIdRequest request = new()
            { EducationalInstitutionId = Guid.NewGuid().ToProtoUuid() };

            GetAllAdminsByEducationalInstitutionIDMethod_Setup(out Response<GetAllAdminsOfEducationalInstitutionQueryResult> expectedMediatorResult);

            //Act
            var result = await service.GetAllAdminsByEducationalInstitutionID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(expectedMediatorResult.Data.AdminsIDs.Select(a => a.ToProtoUuid()).ToList(), result.Data);
        }

        private void GetAllAdminsByEducationalInstitutionIDMethod_Setup(out Response<GetAllAdminsOfEducationalInstitutionQueryResult> expectedMediatorResult)
        {
            expectedMediatorResult = new()
            {
                Data = new() { AdminsIDs = new List<Guid>() { Guid.NewGuid() } },
                OperationStatus = true,
                Message = string.Empty,
                StatusCode = HttpStatusCode.OK
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<GetAllAdminsByEducationalInstitutionIDQuery>(), out It.Ref<string>.IsAny))
                                                     .Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<GetAllAdminsByEducationalInstitutionIDQuery>(), It.IsAny<CancellationToken>()))
                                           .ReturnsAsync(expectedMediatorResult);
        }

        [Fact]
        public async Task GivenAnInvalidAdminsGetByEducationalInstitutionIdRequest_ThatFailsValidation_ToGetAllAdminsByEducationalInstitutionIDMethod_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            AdminsGetByEducationalInstitutionIdRequest request = new()
            { EducationalInstitutionId = default };

            SetupMockedDependenciesToFailValidation<GetAllAdminsByEducationalInstitutionIDQuery>();

            //Act
            var result = await service.GetAllAdminsByEducationalInstitutionID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidAdminsGetByEducationalInstitutionIdRequest_ThatFailsValidation_ToGetAllAdminsByEducationalInstitutionIDMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            AdminsGetByEducationalInstitutionIdRequest request = new()
            { EducationalInstitutionId = default };

            SetupMockedDependenciesToFailValidation<GetAllAdminsByEducationalInstitutionIDQuery>();

            //Act
            var result = await service.GetAllAdminsByEducationalInstitutionID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnInvalidAdminsGetByEducationalInstitutionIdRequest_ThatFailsValidation_ToGetAllAdminsByEducationalInstitutionIDMethod_ShouldReturnEmptyData()
        {
            //Arrange
            AdminsGetByEducationalInstitutionIdRequest request = new()
            { EducationalInstitutionId = default };

            SetupMockedDependenciesToFailValidation<GetAllAdminsByEducationalInstitutionIDQuery>();

            //Act
            var result = await service.GetAllAdminsByEducationalInstitutionID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Data);
        }

        [Fact]
        public async Task GivenAnInvalidAdminsGetByEducationalInstitutionIdRequest_ThatFailsValidation_ToGetAllAdminsByEducationalInstitutionIDMethod_ShouldReturnStatusCodeDefault()
        {
            //Arrange
            AdminsGetByEducationalInstitutionIdRequest request = new()
            { EducationalInstitutionId = default };

            SetupMockedDependenciesToFailValidation<GetAllAdminsByEducationalInstitutionIDQuery>();

            //Act
            var result = await service.GetAllAdminsByEducationalInstitutionID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidAdminsGetByEducationalInstitutionIdReques_IDDoesntExistInDatabase_ToToGetAllAdminsByEducationalInstitutionIDMethod_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            var id = Guid.NewGuid();
            AdminsGetByEducationalInstitutionIdRequest request = new()
            { EducationalInstitutionId = id.ToProtoUuid() };

            GetAllAdminsByEducationalInstitutionIDMethod_SetupToNotFindEntity(out Response<GetAllAdminsOfEducationalInstitutionQueryResult> expectedMediatorResult, id);

            //Act
            var result = await service.GetAllAdminsByEducationalInstitutionID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidAdminsGetByEducationalInstitutionIdReques_IDDoesntExistInDatabase_ToToGetAllAdminsByEducationalInstitutionIDMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            var id = Guid.NewGuid();
            AdminsGetByEducationalInstitutionIdRequest request = new()
            { EducationalInstitutionId = id.ToProtoUuid() };

            GetAllAdminsByEducationalInstitutionIDMethod_SetupToNotFindEntity(out Response<GetAllAdminsOfEducationalInstitutionQueryResult> expectedMediatorResult, id);

            //Act
            var result = await service.GetAllAdminsByEducationalInstitutionID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidAdminsGetByEducationalInstitutionIdReques_IDDoesntExistInDatabase_ToToGetAllAdminsByEducationalInstitutionIDMethod_ShouldReturnEmptyData()
        {
            //Arrange
            var id = Guid.NewGuid();
            AdminsGetByEducationalInstitutionIdRequest request = new()
            { EducationalInstitutionId = id.ToProtoUuid() };

            GetAllAdminsByEducationalInstitutionIDMethod_SetupToNotFindEntity(out Response<GetAllAdminsOfEducationalInstitutionQueryResult> expectedMediatorResult, id);

            //Act
            var result = await service.GetAllAdminsByEducationalInstitutionID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Data);
        }

        [Fact]
        public async Task GivenAValidAdminsGetByEducationalInstitutionIdReques_IDDoesntExistInDatabase_ToToGetAllAdminsByEducationalInstitutionIDMethod_ShouldReturnStatusCodeDefault()
        {
            //Arrange
            var id = Guid.NewGuid();
            AdminsGetByEducationalInstitutionIdRequest request = new()
            { EducationalInstitutionId = id.ToProtoUuid() };

            GetAllAdminsByEducationalInstitutionIDMethod_SetupToNotFindEntity(out Response<GetAllAdminsOfEducationalInstitutionQueryResult> expectedMediatorResult, id);

            //Act
            var result = await service.GetAllAdminsByEducationalInstitutionID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        private void GetAllAdminsByEducationalInstitutionIDMethod_SetupToNotFindEntity(out Response<GetAllAdminsOfEducationalInstitutionQueryResult> expectedMediatorResult, Guid id)
        {
            expectedMediatorResult = new()
            {
                Data = null,
                OperationStatus = false,
                Message = $"Educational Institution with the following ID: {id} has not been found!",
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<GetAllAdminsByEducationalInstitutionIDQuery>(), out It.Ref<string>.IsAny))
                                                     .Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<GetAllAdminsByEducationalInstitutionIDQuery>(), It.IsAny<CancellationToken>()))
                                           .ReturnsAsync(expectedMediatorResult);
        }

        [Fact]
        public async Task GivenAValidAdminsGetByEducationalInstitutionIdReques_ToToGetAllAdminsByEducationalInstitutionIDMethod_AnExceptionIsCaught_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            var id = Guid.NewGuid();
            AdminsGetByEducationalInstitutionIdRequest request = new()
            { EducationalInstitutionId = id.ToProtoUuid() };

            GetAllAdminsByEducationalInstitutionIDMethod_SetupToThrowAnException();

            //Act
            var result = await service.GetAllAdminsByEducationalInstitutionID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidAdminsGetByEducationalInstitutionIdReques_ToToGetAllAdminsByEducationalInstitutionIDMethod_AnExceptionIsCaught_ShouldReturnEmptyMessage()
        {
            //Arrange
            var id = Guid.NewGuid();
            AdminsGetByEducationalInstitutionIdRequest request = new()
            { EducationalInstitutionId = id.ToProtoUuid() };

            GetAllAdminsByEducationalInstitutionIDMethod_SetupToThrowAnException();

            //Act
            var result = await service.GetAllAdminsByEducationalInstitutionID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidAdminsGetByEducationalInstitutionIdReques_ToToGetAllAdminsByEducationalInstitutionIDMethod_AnExceptionIsCaught_ShouldReturnEmptyData()
        {
            //Arrange
            var id = Guid.NewGuid();
            AdminsGetByEducationalInstitutionIdRequest request = new()
            { EducationalInstitutionId = id.ToProtoUuid() };

            GetAllAdminsByEducationalInstitutionIDMethod_SetupToThrowAnException();

            //Act
            var result = await service.GetAllAdminsByEducationalInstitutionID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Data);
        }

        [Fact]
        public async Task GivenAValidAdminsGetByEducationalInstitutionIdReques_ToToGetAllAdminsByEducationalInstitutionIDMethod_AnExceptionIsCaught_ShouldReturnStatusCodeDefault()
        {
            //Arrange
            var id = Guid.NewGuid();
            AdminsGetByEducationalInstitutionIdRequest request = new()
            { EducationalInstitutionId = id.ToProtoUuid() };

            GetAllAdminsByEducationalInstitutionIDMethod_SetupToThrowAnException();

            //Act
            var result = await service.GetAllAdminsByEducationalInstitutionID(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        private void GetAllAdminsByEducationalInstitutionIDMethod_SetupToThrowAnException()
        {
            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<GetAllAdminsByEducationalInstitutionIDQuery>(), out It.Ref<string>.IsAny))
                                                     .Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<GetAllAdminsByEducationalInstitutionIDQuery>(), It.IsAny<CancellationToken>()))
                                           .ThrowsAsync(new ArgumentNullException());
        }

        #endregion GetAllAdminsByEducationalInstitutionIDMethod TESTS
    }
}