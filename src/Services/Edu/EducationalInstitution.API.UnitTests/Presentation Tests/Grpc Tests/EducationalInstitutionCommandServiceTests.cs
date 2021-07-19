using EducationalInstitution.Application;
using EducationalInstitution.Application.Commands;
using EducationalInstitution.Application.Commands.Results;
using EducationalInstitutionAPI.Presentation.Grpc;
using EducationalInstitutionAPI.Proto;
using EducationalInstitutionAPI.Utils.Mappers;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.UnitTests.Presentation_Tests.Grpc_Tests
{
    public class EducationalInstitutionCommandServiceTests : SetupGrpcServicesHelper<EducationalInstitutionCommandService>,
                                                             IClassFixture<MockDependenciesHelper<EducationalInstitutionCommandService>>
    {
        private readonly EducationalInstitutionCommandService service;

        /// <remarks>Called before each test</remarks>
        public EducationalInstitutionCommandServiceTests(MockDependenciesHelper<EducationalInstitutionCommandService> dependenciesHelper) : base(dependenciesHelper)
                => service = new(dependenciesHelper.mockMediator.Object,
                                dependenciesHelper.mockLogger.Object,
                                dependenciesHelper.mockValidationHandler.Object);

        #region CreateEducationalInstitutionMethod TESTS

        [Fact]
        public async Task GivenAValidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_ShouldReturnAnEducationalInstitutionCreateResponse()
        {
            //Arrange
            EducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = Guid.NewGuid().ToProtoUuid()
            };
            request.Buildings.Add("building1235");

            CreateEducationalInstitutionMethod_Setup();

            //Act
            var result = await service.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.IsType<EducationalInstitutionCreateResponse>(result);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_ShouldReturnA_ProtoStatusCodeCreatedField()
        {
            //Arrange
            EducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = Guid.NewGuid().ToProtoUuid()
            };
            request.Buildings.Add("building1235");

            CreateEducationalInstitutionMethod_Setup();

            //Act
            var result = await service.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_ShouldReturnAResponseThatIncludesA_MessageEmptyField()
        {
            //Arrange
            EducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = Guid.NewGuid().ToProtoUuid()
            };
            request.Buildings.Add("building1235");

            CreateEducationalInstitutionMethod_Setup();

            //Act
            var result = await service.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_ShouldReturnAResponseThatIncludesAn_OperationStatusTrueField()
        {
            //Arrange
            EducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = Guid.NewGuid().ToProtoUuid()
            };
            request.Buildings.Add("building1235");

            CreateEducationalInstitutionMethod_Setup();

            //Act
            var result = await service.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_FailsTheValidation_ShouldReturnAResponseThatIncludesAn_OperationStatusFalseField()
        {
            //Arrange
            EducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location_invalid_data",
                ParentInstitutionId = Guid.NewGuid().ToProtoUuid()
            };
            request.Buildings.Add("building_invalid_data");

            SetupMockedDependenciesToFailValidation<CreateEducationalInstitutionCommand>();

            //Act
            var result = await service.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_FailsTheValidation_ShouldReturnDefault()
        {
            //Arrange
            var id = Guid.NewGuid();

            EducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location_invalid_data",
                ParentInstitutionId = id.ToProtoUuid()
            };
            request.Buildings.Add("building_invalid_data");

            SetupMockedDependenciesToFailValidation<CreateEducationalInstitutionCommand>();

            //Act
            var result = await service.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(new EducationalInstitutionCreateResponse(), result);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_FailsTheValidation_ShouldReturnAResponseThatIncludesA_DefaultDataField()
        {
            //Arrange
            var id = Guid.NewGuid();

            EducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location_invalid_data",
                ParentInstitutionId = id.ToProtoUuid()
            };
            request.Buildings.Add("building_invalid_data");

            SetupMockedDependenciesToFailValidation<CreateEducationalInstitutionCommand>();

            //Act
            var result = await service.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_DatabaseInsertOperationFails_ShouldReturnANullDataField()
        {
            //Arrange
            EducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = Guid.NewGuid().ToProtoUuid()
            };
            request.Buildings.Add("building1235");

            CreateEducationalInstitutionMethod_SetupToFailDatabaseInsertion();

            //Act
            var result = await service.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_DatabaseInsertOperationFails_ShouldReturnAFalseOperationStatusField()
        {
            //Arrange
            EducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = Guid.NewGuid().ToProtoUuid()
            };
            request.Buildings.Add("building1235");

            CreateEducationalInstitutionMethod_SetupToFailDatabaseInsertion();

            //Act
            var result = await service.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        private void CreateEducationalInstitutionMethod_SetupToFailDatabaseInsertion()
        {
            var expectedMediatorResult = new Response<CreateEducationalInstitutionCommandResult>()
            {
                Data = null,
                OperationStatus = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "An error occurred while creating the Educational Institution with the given data!"
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<CreateEducationalInstitutionCommand>(),
                                                                                              out It.Ref<string>.IsAny))
                                                    .Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<CreateEducationalInstitutionCommand>(),
                                                               It.IsAny<CancellationToken>()))
                                           .ReturnsAsync(expectedMediatorResult);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_ParentIsNotFound_ShouldReturnAMessageField()
        {
            //Arrange
            var id = Guid.NewGuid();

            EducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = id.ToProtoUuid()
            };
            request.Buildings.Add("building1235");

            CreateEducationalInstitutionMethod_SetupToNotFindParent();

            //Act
            var result = await service.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal("Educational Institution was created but parent institution was not found!", result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_ParentIsNotFound_ShouldReturnAnOperationStatusTrueField()
        {
            //Arrange
            EducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = Guid.NewGuid().ToProtoUuid()
            };
            request.Buildings.Add("building1235");

            CreateEducationalInstitutionMethod_SetupToNotFindParent();

            //Act
            var result = await service.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_ParentIsNotFound_ShouldReturnAProtoStatusCodeMultiStatusField()
        {
            //Arrange
            var id = Guid.NewGuid();

            EducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = id.ToProtoUuid()
            };
            request.Buildings.Add("building1235");

            CreateEducationalInstitutionMethod_SetupToNotFindParent();

            //Act
            var result = await service.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.MultiStatus, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_ParentIsNotFound_ShouldReturnDataThatContainsAnId()
        {
            //Arrange
            var id = Guid.NewGuid();

            EducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = id.ToProtoUuid()
            };
            request.Buildings.Add("building1235");

            CreateEducationalInstitutionMethod_SetupToNotFindParent();

            //Act
            var result = await service.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.NotNull(result.Data);
        }

        private void CreateEducationalInstitutionMethod_SetupToNotFindParent()
        {
            var expectedMediatorResult = new Response<CreateEducationalInstitutionCommandResult>()
            {
                Data = new() { EducationalInstitutionID = Guid.NewGuid() },
                OperationStatus = true,
                StatusCode = HttpStatusCode.MultiStatus,
                Message = "Educational Institution was created but parent institution was not found!"
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<CreateEducationalInstitutionCommand>(),
                                                                                              out It.Ref<string>.IsAny))
                                                    .Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<CreateEducationalInstitutionCommand>(),
                                                              It.IsAny<CancellationToken>()))
                                           .ReturnsAsync(expectedMediatorResult);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_WithDefaultParentInstitutionID_ShouldReturnAProtoStatusCodeCreatedField()
        {
            //Arrange
            var id = Guid.Empty;

            EducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = id.ToProtoUuid()
            };
            request.Buildings.Add("building1235");

            CreateEducationalInstitutionMethod_Setup();

            //Act
            var result = await service.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_WithDefaultParentInstitutionID_ShouldReturnAnEmptyMessageField()
        {
            //Arrange
            EducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = Guid.NewGuid().ToProtoUuid()
            };
            request.Buildings.Add("building1235");

            CreateEducationalInstitutionMethod_Setup();

            //Act
            var result = await service.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_WithDefaultParentInstitutionID_ShouldReturnATrueOperationStatusField()
        {
            //Arrange
            var id = Guid.Empty;

            EducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = id.ToProtoUuid()
            };
            request.Buildings.Add("building1235");

            CreateEducationalInstitutionMethod_Setup();

            //Act
            var result = await service.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_WithDefaultParentInstitutionID_ShouldAReturnAGuidIDField()
        {
            //Arrange
            var id = Guid.Empty;

            EducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = id.ToProtoUuid()
            };
            request.Buildings.Add("building1235");

            CreateEducationalInstitutionMethod_Setup();

            //Act
            var result = await service.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.NotEqual(default, result.Data.EducationalInstitutionId);
        }

        private void CreateEducationalInstitutionMethod_Setup()
        {
            var expectedMediatorResult = new Response<CreateEducationalInstitutionCommandResult>()
            {
                Data = new() { EducationalInstitutionID = Guid.NewGuid() },
                OperationStatus = true,
                StatusCode = HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<CreateEducationalInstitutionCommand>(),
                                                                                              out It.Ref<string>.IsAny))
                                                    .Returns(true);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<CreateEducationalInstitutionCommand>(),
                                                              It.IsAny<CancellationToken>()))
                                           .ReturnsAsync(expectedMediatorResult);
        }

        #endregion CreateEducationalInstitutionMethod TESTS

        #region DeleteEducationalInstitutionMethod TESTS

        [Fact]
        public async Task GivenAValidEducationalInstitutionDeleteRequest_ToDeleteEducationalInstitutionMethod_ShouldReturnEducationalInstitutionDeleteResponseType()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            EducationalInstitutionDeleteRequest request = new() { EducationalInstitutionId = educationalInstitutionID.ToProtoUuid() };

            DeleteEducationalInstitutionMethod_Setup(out Response<DisableEducationalInstitutionCommandResult> expectedMediatorResult);

            //Act
            var result = await service.DeleteEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.IsType<EducationalInstitutionDeleteResponse>(result);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionDeleteRequest_ToDeleteEducationalInstitutionMethod_ShouldReturnExpectedDeletionDate()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            EducationalInstitutionDeleteRequest request = new() { EducationalInstitutionId = educationalInstitutionID.ToProtoUuid() };

            DeleteEducationalInstitutionMethod_Setup(out Response<DisableEducationalInstitutionCommandResult> expectedMediatorResult);

            //Act
            var result = await service.DeleteEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(expectedMediatorResult.Data.DateForPermanentDeletion, result.Data.DateForPermanentDeletion.ToDateTime());
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionDeleteRequest_ToDeleteEducationalInstitutionMethod_ShouldReturnTrueOperationStatus()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            EducationalInstitutionDeleteRequest request = new() { EducationalInstitutionId = educationalInstitutionID.ToProtoUuid() };

            DeleteEducationalInstitutionMethod_Setup(out Response<DisableEducationalInstitutionCommandResult> expectedMediatorResult);

            //Act
            var result = await service.DeleteEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionDeleteRequest_ToDeleteEducationalInstitutionMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            EducationalInstitutionDeleteRequest request = new() { EducationalInstitutionId = educationalInstitutionID.ToProtoUuid() };

            DeleteEducationalInstitutionMethod_Setup(out Response<DisableEducationalInstitutionCommandResult> expectedMediatorResult);

            //Act
            var result = await service.DeleteEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionDeleteRequest_ToDeleteEducationalInstitutionMethod_ShouldReturnHttpStatusCodeAccepted()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            EducationalInstitutionDeleteRequest request = new() { EducationalInstitutionId = educationalInstitutionID.ToProtoUuid() };

            DeleteEducationalInstitutionMethod_Setup(out Response<DisableEducationalInstitutionCommandResult> expectedMediatorResult);

            //Act
            var result = await service.DeleteEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Accepted, result.StatusCode);
        }

        private void DeleteEducationalInstitutionMethod_Setup(out Response<DisableEducationalInstitutionCommandResult> expectedMediatorResult)
        {
            expectedMediatorResult = new()
            {
                Data = new() { DateForPermanentDeletion = DateTime.UtcNow },
                OperationStatus = true,
                Message = string.Empty,
                StatusCode = HttpStatusCode.Accepted
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<DisableEducationalInstitutionCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DisableEducationalInstitutionCommand>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionDeleteRequest_WithIDThatDoesntExistInDatabase_ToDeleteEducationalInstitutionMethod_ShouldReturnHttpStatusCodeNotDefault()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            EducationalInstitutionDeleteRequest request = new() { EducationalInstitutionId = educationalInstitutionID.ToProtoUuid() };

            DeleteEducationalInstitutionMethod_SetupToNotFindEntity(educationalInstitutionID);

            //Act
            var result = await service.DeleteEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionDeleteRequest_WithIDThatDoesntExistInDatabase_ToDeleteEducationalInstitutionMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            EducationalInstitutionDeleteRequest request = new() { EducationalInstitutionId = educationalInstitutionID.ToProtoUuid() };

            DeleteEducationalInstitutionMethod_SetupToNotFindEntity(educationalInstitutionID);

            //Act
            var result = await service.DeleteEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionDeleteRequest_WithIDThatDoesntExistInDatabase_ToDeleteEducationalInstitutionMethod_ShouldReturnNullData()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            EducationalInstitutionDeleteRequest request = new() { EducationalInstitutionId = educationalInstitutionID.ToProtoUuid() };

            DeleteEducationalInstitutionMethod_SetupToNotFindEntity(educationalInstitutionID);

            //Act
            var result = await service.DeleteEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Null(result.Data);
        }

        private void DeleteEducationalInstitutionMethod_SetupToNotFindEntity(Guid id)
        {
            Response<DisableEducationalInstitutionCommandResult> expectedMediatorResult = new()
            {
                Data = null,
                OperationStatus = false,
                Message = $"Educational Institution with the following ID: {id} has not been found!",
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<DisableEducationalInstitutionCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DisableEducationalInstitutionCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionDeleteRequest_ThatFailsTheValidation_ToDeleteEducationalInstitutionMethod_ShouldReturnNullData()
        {
            //Arrange
            Guid educationalInstitutionID = default;
            EducationalInstitutionDeleteRequest request = new() { EducationalInstitutionId = educationalInstitutionID.ToProtoUuid() };

            SetupMockedDependenciesToFailValidation<DisableEducationalInstitutionCommand>();

            //Act
            var result = await service.DeleteEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionDeleteRequest_ThatFailsTheValidation_ToDeleteEducationalInstitutionMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            Guid educationalInstitutionID = default;
            EducationalInstitutionDeleteRequest request = new() { EducationalInstitutionId = educationalInstitutionID.ToProtoUuid() };

            SetupMockedDependenciesToFailValidation<DisableEducationalInstitutionCommand>();

            //Act
            var result = await service.DeleteEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionDeleteRequest_ThatFailsTheValidation_ToDeleteEducationalInstitutionMethod_ShouldReturnHttpStatusCodeDefault()
        {
            //Arrange
            Guid educationalInstitutionID = default;
            EducationalInstitutionDeleteRequest request = new() { EducationalInstitutionId = educationalInstitutionID.ToProtoUuid() };

            SetupMockedDependenciesToFailValidation<DisableEducationalInstitutionCommand>();

            //Act
            var result = await service.DeleteEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionDeleteRequest_ThatFailsTheValidation_ToDeleteEducationalInstitutionMethod_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            Guid educationalInstitutionID = default;
            EducationalInstitutionDeleteRequest request = new() { EducationalInstitutionId = educationalInstitutionID.ToProtoUuid() };

            SetupMockedDependenciesToFailValidation<DisableEducationalInstitutionCommand>();

            //Act
            var result = await service.DeleteEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        #endregion DeleteEducationalInstitutionMethod TESTS

        #region UpdateEducationalInstitutionMethod TESTS

        [Fact]
        public async Task GivenAValidEducationalInstitutionUpdateRequest_ToUpdateEducationalInstitutionMethod_ShouldReturnEducationalInstitutionUpdateResponseType()
        {
            //Arrange
            EducationalInstitutionUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                UpdateName = true,
                Name = "newName",
                UpdateDescription = true,
                Description = "newDescription"
            };

            UpdateEducationalInstitutionMethod_Setup();

            //Act
            var result = await service.UpdateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.IsType<EducationalInstitutionUpdateResponse>(result);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionUpdateRequest_ToUpdateEducationalInstitutionMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                UpdateName = true,
                Name = "newName",
                UpdateDescription = true,
                Description = "newDescription"
            };

            UpdateEducationalInstitutionMethod_Setup();

            //Act
            var result = await service.UpdateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionUpdateRequest_ToUpdateEducationalInstitutionMethod_ShouldReturnTrueOperationStatus()
        {
            //Arrange
            EducationalInstitutionUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                UpdateName = true,
                Name = "newName",
                UpdateDescription = true,
                Description = "newDescription"
            };

            UpdateEducationalInstitutionMethod_Setup();

            //Act
            var result = await service.UpdateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionUpdateRequest_ToUpdateEducationalInstitutionMethod_ShouldReturnStatusCodeNoContent()
        {
            //Arrange
            EducationalInstitutionUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                UpdateName = true,
                Name = "newName",
                UpdateDescription = true,
                Description = "newDescription"
            };

            UpdateEducationalInstitutionMethod_Setup();

            //Act
            var result = await service.UpdateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.NoContent, result.StatusCode);
        }

        private void UpdateEducationalInstitutionMethod_Setup()
        {
            var expectedMediatorResult = new Response()
            {
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<UpdateEducationalInstitutionCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<UpdateEducationalInstitutionCommand>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionUpdateRequest_IDDoesntExistInDatabase_ToUpdateEducationalInstitutionMethod_ShouldReturnStatusCodeDefault()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            EducationalInstitutionUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                UpdateName = true,
                Name = "newName",
                UpdateDescription = true,
                Description = "newDescription"
            };

            UpdateEducationalInstitutionMethod_SetupToNotFindEntity(educationalInstitutionID);

            //Act
            var result = await service.UpdateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionUpdateRequest_IDDoesntExistInDatabase_ToUpdateEducationalInstitutionMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            EducationalInstitutionUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                UpdateName = true,
                Name = "newName",
                UpdateDescription = true,
                Description = "newDescription"
            };

            UpdateEducationalInstitutionMethod_SetupToNotFindEntity(educationalInstitutionID);

            //Act
            var result = await service.UpdateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionUpdateRequest_IDDoesntExistInDatabase_ToUpdateEducationalInstitutionMethod_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            EducationalInstitutionUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                UpdateName = true,
                Name = "newName",
                UpdateDescription = true,
                Description = "newDescription"
            };

            UpdateEducationalInstitutionMethod_SetupToNotFindEntity(educationalInstitutionID);

            //Act
            var result = await service.UpdateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        private void UpdateEducationalInstitutionMethod_SetupToNotFindEntity(Guid id)
        {
            var expectedMediatorResult = new Response()
            {
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound,
                Message = $"Educational Institution with the following ID: {id} has not been found!"
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<UpdateEducationalInstitutionCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<UpdateEducationalInstitutionCommand>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);
        }

        private void UpdateEducationalInstitutionMethod_Setup(Guid id)
        {
            var expectedMediatorResult = new Response()
            {
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound,
                Message = $"An error occurred while updating the Educational Institution with the following ID: {id}!"
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<UpdateEducationalInstitutionCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<UpdateEducationalInstitutionCommand>(), It.IsAny<CancellationToken>()))
                                           .ReturnsAsync(expectedMediatorResult);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionUpdateRequest_ToUpdateEducationalInstitutionMethod_AnExceptionIsCaughtInHandler_ShouldReturnStatusCodeDefault()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            EducationalInstitutionUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                UpdateName = true,
                Name = "newName",
                UpdateDescription = true,
                Description = "newDescription"
            };

            UpdateEducationalInstitutionMethod_Setup(educationalInstitutionID);

            //Act
            var result = await service.UpdateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionUpdateRequest_ToUpdateEducationalInstitutionMethod_AnExceptionIsCaughtInHandler_ShouldReturnEmptyMessage()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            EducationalInstitutionUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                UpdateName = true,
                Name = "newName",
                UpdateDescription = true,
                Description = "newDescription"
            };

            UpdateEducationalInstitutionMethod_Setup(educationalInstitutionID);

            //Act
            var result = await service.UpdateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionUpdateRequest_ToUpdateEducationalInstitutionMethod_AnExceptionIsCaughtInHandler_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            EducationalInstitutionUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                UpdateName = true,
                Name = "newName",
                UpdateDescription = true,
                Description = "newDescription"
            };

            UpdateEducationalInstitutionMethod_Setup(educationalInstitutionID);

            //Act
            var result = await service.UpdateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionUpdateRequest_ThatFailsValidation_ToUpdateEducationalInstitutionMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                UpdateName = true,
                Name = string.Empty,
                UpdateDescription = true,
                Description = string.Empty
            };

            SetupMockedDependenciesToFailValidation<UpdateEducationalInstitutionCommand>();

            //Act
            var result = await service.UpdateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionUpdateRequest_ThatFailsValidation_ToUpdateEducationalInstitutionMethod_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            EducationalInstitutionUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                UpdateName = true,
                Name = string.Empty,
                UpdateDescription = true,
                Description = string.Empty
            };

            SetupMockedDependenciesToFailValidation<UpdateEducationalInstitutionCommand>();

            //Act
            var result = await service.UpdateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionUpdateRequest_ThatFailsValidation_ToUpdateEducationalInstitutionMethod_ShouldReturnStatusCodeDefault()
        {
            //Arrange
            EducationalInstitutionUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                UpdateName = true,
                Name = string.Empty,
                UpdateDescription = true,
                Description = string.Empty
            };

            SetupMockedDependenciesToFailValidation<UpdateEducationalInstitutionCommand>();

            //Act
            var result = await service.UpdateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        #endregion UpdateEducationalInstitutionMethod TESTS

        #region UpdateEducationalInstitutionAdminMethod TESTS

        [Fact]
        public async Task GivenAValidEducationalInstitutionAdminUpdateRequest_ToUpdateEducationalInstitutionAdminMethod_ShouldReturnEducationalInstitutionUpdateResponseType()
        {
            //Arrange
            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                AddAdminsIds = { Guid.NewGuid().ToProtoUuid() },
                RemoveAdminsIds = { Guid.NewGuid().ToProtoUuid() }
            };

            UpdateEducationalInstitutionAdminMethod_Setup();

            //Act
            var result = await service.UpdateEducationalInstitutionAdmin(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.IsType<EducationalInstitutionUpdateResponse>(result);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionAdminUpdateRequest_ToUpdateEducationalInstitutionAdminMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                AddAdminsIds = { Guid.NewGuid().ToProtoUuid() },
                RemoveAdminsIds = { Guid.NewGuid().ToProtoUuid() }
            };

            UpdateEducationalInstitutionAdminMethod_Setup();

            //Act
            var result = await service.UpdateEducationalInstitutionAdmin(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionAdminUpdateRequest_ToUpdateEducationalInstitutionAdminMethod_ShouldReturnTrueOperationStatus()
        {
            //Arrange
            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                AddAdminsIds = { Guid.NewGuid().ToProtoUuid() },
                RemoveAdminsIds = { Guid.NewGuid().ToProtoUuid() }
            };

            UpdateEducationalInstitutionAdminMethod_Setup();

            //Act
            var result = await service.UpdateEducationalInstitutionAdmin(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionAdminUpdateRequest_ToUpdateEducationalInstitutionAdminMethod_ShouldReturnStatusCodeNoContent()
        {
            //Arrange
            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                AddAdminsIds = { Guid.NewGuid().ToProtoUuid() },
                RemoveAdminsIds = { Guid.NewGuid().ToProtoUuid() }
            };

            UpdateEducationalInstitutionAdminMethod_Setup();

            //Act
            var result = await service.UpdateEducationalInstitutionAdmin(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.NoContent, result.StatusCode);
        }

        private void UpdateEducationalInstitutionAdminMethod_Setup()
        {
            var expectedMediatorResult = new Response()
            {
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<UpdateEducationalInstitutionAdminsCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<UpdateEducationalInstitutionAdminsCommand>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionAdminUpdateRequest_IDDoesntExistInDatabase_ToUpdateEducationalInstitutionAdminMethod_ShouldReturnStatusCodeDefault()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                AddAdminsIds = { Guid.NewGuid().ToProtoUuid() },
                RemoveAdminsIds = { Guid.NewGuid().ToProtoUuid() }
            };

            UpdateEducationalInstitutionAdminMethod_SetupToNotFindEntity(educationalInstitutionID);

            //Act
            var result = await service.UpdateEducationalInstitutionAdmin(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionAdminUpdateRequest_IDDoesntExistInDatabase_ToUpdateEducationalInstitutionAdminMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                AddAdminsIds = { Guid.NewGuid().ToProtoUuid() },
                RemoveAdminsIds = { Guid.NewGuid().ToProtoUuid() }
            };

            UpdateEducationalInstitutionAdminMethod_SetupToNotFindEntity(educationalInstitutionID);

            //Act
            var result = await service.UpdateEducationalInstitutionAdmin(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionAdminUpdateRequest_IDDoesntExistInDatabase_ToUpdateEducationalInstitutionAdminMethod_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                AddAdminsIds = { Guid.NewGuid().ToProtoUuid() },
                RemoveAdminsIds = { Guid.NewGuid().ToProtoUuid() }
            };

            UpdateEducationalInstitutionAdminMethod_SetupToNotFindEntity(educationalInstitutionID);

            //Act
            var result = await service.UpdateEducationalInstitutionAdmin(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        private void UpdateEducationalInstitutionAdminMethod_SetupToNotFindEntity(Guid id)
        {
            var expectedMediatorResult = new Response()
            {
                Message = $"Educational Institution with the following ID: {id} has not been found!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<UpdateEducationalInstitutionAdminsCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<UpdateEducationalInstitutionAdminsCommand>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionAdminUpdateRequest_ToUpdateEducationalInstitutionAdminMethod_AnExceptionIsCaughtInHandler_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                AddAdminsIds = { Guid.NewGuid().ToProtoUuid() },
                RemoveAdminsIds = { Guid.NewGuid().ToProtoUuid() }
            };

            UpdateEducationalInstitutionAdminMethod_SetupWhenAnExceptionIsCaughtInHandler();

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object,
                                                                    dependenciesHelper.mockLogger.Object,
                                                                    dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.UpdateEducationalInstitutionAdmin(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionAdminUpdateRequest_ToUpdateEducationalInstitutionAdminMethod_AnExceptionIsCaughtInHandler_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                AddAdminsIds = { Guid.NewGuid().ToProtoUuid() },
                RemoveAdminsIds = { Guid.NewGuid().ToProtoUuid() }
            };

            UpdateEducationalInstitutionAdminMethod_SetupWhenAnExceptionIsCaughtInHandler();

            //Act
            var result = await service.UpdateEducationalInstitutionAdmin(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionAdminUpdateRequest_ToUpdateEducationalInstitutionAdminMethod_AnExceptionIsCaughtInHandler_ShouldReturnStatusCodeDefault()
        {
            //Arrange
            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                AddAdminsIds = { Guid.NewGuid().ToProtoUuid() },
                RemoveAdminsIds = { Guid.NewGuid().ToProtoUuid() }
            };

            UpdateEducationalInstitutionAdminMethod_SetupWhenAnExceptionIsCaughtInHandler();

            //Act
            var result = await service.UpdateEducationalInstitutionAdmin(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        private void UpdateEducationalInstitutionAdminMethod_SetupWhenAnExceptionIsCaughtInHandler()
        {
            var expectedMediatorResult = new Response()
            {
                Message = "An error occurred while updating the Educational Institution's admins with the given data!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.InternalServerError
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<UpdateEducationalInstitutionAdminsCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<UpdateEducationalInstitutionAdminsCommand>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionAdminUpdateRequest_ToUpdateEducationalInstitutionAdminMethod_AnExceptionIsThrownByMediator_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                AddAdminsIds = { Guid.NewGuid().ToProtoUuid() },
                RemoveAdminsIds = { Guid.NewGuid().ToProtoUuid() }
            };

            UpdateEducationalInstitutionAdminMethod_SetupMediatorToThrowException();

            //Act
            var result = await service.UpdateEducationalInstitutionAdmin(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionAdminUpdateRequest_ToUpdateEducationalInstitutionAdminMethod_AnExceptionIsThrownByMediator_ShouldReturnStatusCodeDefault()
        {
            //Arrange
            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                AddAdminsIds = { Guid.NewGuid().ToProtoUuid() },
                RemoveAdminsIds = { Guid.NewGuid().ToProtoUuid() }
            };

            UpdateEducationalInstitutionAdminMethod_SetupMediatorToThrowException();

            //Act
            var result = await service.UpdateEducationalInstitutionAdmin(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionAdminUpdateRequest_ToUpdateEducationalInstitutionAdminMethod_AnExceptionIsThrownByMediator_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                AddAdminsIds = { Guid.NewGuid().ToProtoUuid() },
                RemoveAdminsIds = { Guid.NewGuid().ToProtoUuid() }
            };

            UpdateEducationalInstitutionAdminMethod_SetupMediatorToThrowException();

            //Act
            var result = await service.UpdateEducationalInstitutionAdmin(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        private void UpdateEducationalInstitutionAdminMethod_SetupMediatorToThrowException()
        {
            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<UpdateEducationalInstitutionAdminsCommand>(), out It.Ref<string>.IsAny))
                                                   .Returns(true);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<UpdateEducationalInstitutionAdminsCommand>(), It.IsAny<CancellationToken>()))
                                            .ThrowsAsync(new InvalidOperationException("Test"));
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionAdminUpdateRequest_ToUpdateEducationalInstitutionAdminMethod_ValidationFails_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.Empty.ToProtoUuid(),
                AddAdminsIds = { Guid.NewGuid().ToProtoUuid() },
                RemoveAdminsIds = { Guid.NewGuid().ToProtoUuid() }
            };

            SetupMockedDependenciesToFailValidation<UpdateEducationalInstitutionAdminsCommand>();

            //Act
            var result = await service.UpdateEducationalInstitutionAdmin(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionAdminUpdateRequest_ToUpdateEducationalInstitutionAdminMethod_ValidationFails_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.Empty.ToProtoUuid(),
                AddAdminsIds = { Guid.NewGuid().ToProtoUuid() },
                RemoveAdminsIds = { Guid.NewGuid().ToProtoUuid() }
            };

            SetupMockedDependenciesToFailValidation<UpdateEducationalInstitutionAdminsCommand>();

            //Act
            var result = await service.UpdateEducationalInstitutionAdmin(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionAdminUpdateRequest_ToUpdateEducationalInstitutionAdminMethod_ValidationFails_ShouldReturnStatusCodeDefault()
        {
            //Arrange
            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.Empty.ToProtoUuid(),
                AddAdminsIds = { Guid.NewGuid().ToProtoUuid() },
                RemoveAdminsIds = { Guid.NewGuid().ToProtoUuid() }
            };

            SetupMockedDependenciesToFailValidation<UpdateEducationalInstitutionAdminsCommand>();

            //Act
            var result = await service.UpdateEducationalInstitutionAdmin(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        #endregion UpdateEducationalInstitutionAdminMethod TESTS

        #region UpdateEducationalInstitutionParentMethod TESTS

        [Fact]
        public async Task GivenAValidEducationalInstitutionParentUpdateRequest_ToUpdateEducationalInstitutionParentMethod_ShouldReturnEducationalInstitutionUpdateResponseType()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            var parentInstitutionID = Guid.NewGuid();
            EducationalInstitutionParentUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };

            UpdateEducationalInstitutionParentMethod_Setup();

            //Act
            var result = await service.UpdateEducationalInstitutionParent(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.IsType<EducationalInstitutionUpdateResponse>(result);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionParentUpdateRequest_ToUpdateEducationalInstitutionParentMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            var parentInstitutionID = Guid.NewGuid();
            EducationalInstitutionParentUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };

            UpdateEducationalInstitutionParentMethod_Setup();

            //Act
            var result = await service.UpdateEducationalInstitutionParent(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionParentUpdateRequest_ToUpdateEducationalInstitutionParentMethod_ShouldReturnTrueOperationStatus()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            var parentInstitutionID = Guid.NewGuid();
            EducationalInstitutionParentUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };

            UpdateEducationalInstitutionParentMethod_Setup();

            //Act
            var result = await service.UpdateEducationalInstitutionParent(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionParentUpdateRequest_ToUpdateEducationalInstitutionParentMethod_ShouldReturnStatusCodeNoContent()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            var parentInstitutionID = Guid.NewGuid();
            EducationalInstitutionParentUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };

            UpdateEducationalInstitutionParentMethod_Setup();

            //Act
            var result = await service.UpdateEducationalInstitutionParent(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.NoContent, result.StatusCode);
        }

        private void UpdateEducationalInstitutionParentMethod_Setup()
        {
            var expectedMediatorResult = new Response()
            {
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<UpdateEducationalInstitutionParentCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<UpdateEducationalInstitutionParentCommand>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionParentUpdateRequest_ToUpdateEducationalInstitutionParentMethod_ParentInstitutionIsNotFound_ShouldReturnEmptyMessage()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            var parentInstitutionID = Guid.NewGuid();
            EducationalInstitutionParentUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };

            UpdateEducationalInstitutionParentMethod_SetupToNotFindParentEntity(parentInstitutionID);

            //Act
            var result = await service.UpdateEducationalInstitutionParent(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionParentUpdateRequest_ToUpdateEducationalInstitutionParentMethod_ParentInstitutionIsNotFound_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            var parentInstitutionID = Guid.NewGuid();
            EducationalInstitutionParentUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };

            UpdateEducationalInstitutionParentMethod_SetupToNotFindParentEntity(parentInstitutionID);

            //Act
            var result = await service.UpdateEducationalInstitutionParent(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionParentUpdateRequest_ToUpdateEducationalInstitutionParentMethod_ParentInstitutionIsNotFound_ShouldReturnStatusCodeDefault()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            var parentInstitutionID = Guid.NewGuid();
            EducationalInstitutionParentUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };

            UpdateEducationalInstitutionParentMethod_SetupToNotFindParentEntity(parentInstitutionID);

            //Act
            var result = await service.UpdateEducationalInstitutionParent(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        private void UpdateEducationalInstitutionParentMethod_SetupToNotFindParentEntity(Guid parentID)
        {
            var expectedMediatorResult = new Response()
            {
                Message = $"The Parent Educational Institution with the following ID: {parentID} has not been found!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<UpdateEducationalInstitutionParentCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<UpdateEducationalInstitutionParentCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);
        }

        private void UpdateEducationalInstitutionParentMethod_Setup(Guid id)
        {
            var expectedMediatorResult = new Response()
            {
                Message = $"An error occurred while updating the Educational Institution with the following ID: {id}!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.InternalServerError
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<UpdateEducationalInstitutionParentCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<UpdateEducationalInstitutionParentCommand>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionParentUpdateRequest_ToUpdateEducationalInstitutionParentMethod_AnExceptionIsCaughtInHandler_ShouldReturnEmptyMessage()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            var parentInstitutionID = Guid.NewGuid();
            EducationalInstitutionParentUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };

            UpdateEducationalInstitutionParentMethod_Setup(educationalInstitutionID);

            //Act
            var result = await service.UpdateEducationalInstitutionParent(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionParentUpdateRequest_ToUpdateEducationalInstitutionParentMethod_AnExceptionIsCaughtInHandler_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            var parentInstitutionID = Guid.NewGuid();
            EducationalInstitutionParentUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };

            UpdateEducationalInstitutionParentMethod_Setup(educationalInstitutionID);

            //Act
            var result = await service.UpdateEducationalInstitutionParent(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionParentUpdateRequest_ToUpdateEducationalInstitutionParentMethod_AnExceptionIsCaughtInHandler_ShouldReturnStatusCodeDefault()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            var parentInstitutionID = Guid.NewGuid();
            EducationalInstitutionParentUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };

            UpdateEducationalInstitutionParentMethod_Setup(educationalInstitutionID);

            //Act
            var result = await service.UpdateEducationalInstitutionParent(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionParentUpdateRequest_ThatFailsValidation_ToUpdateEducationalInstitutionParentMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            var educationalInstitutionID = Guid.Empty;
            var parentInstitutionID = Guid.NewGuid();
            EducationalInstitutionParentUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };

            SetupMockedDependenciesToFailValidation<UpdateEducationalInstitutionParentCommand>();

            //Act
            var result = await service.UpdateEducationalInstitutionParent(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionParentUpdateRequest_ThatFailsValidation_ToUpdateEducationalInstitutionParentMethod_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            var educationalInstitutionID = Guid.Empty;
            var parentInstitutionID = Guid.NewGuid();
            EducationalInstitutionParentUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };

            SetupMockedDependenciesToFailValidation<UpdateEducationalInstitutionParentCommand>();

            //Act
            var result = await service.UpdateEducationalInstitutionParent(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionParentUpdateRequest_ThatFailsValidation_ToUpdateEducationalInstitutionParentMethod_ShouldReturnStatusCodeDefault()
        {
            //Arrange
            var educationalInstitutionID = Guid.Empty;
            var parentInstitutionID = Guid.NewGuid();
            EducationalInstitutionParentUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };

            SetupMockedDependenciesToFailValidation<UpdateEducationalInstitutionParentCommand>();

            //Act
            var result = await service.UpdateEducationalInstitutionParent(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        #endregion UpdateEducationalInstitutionParentMethod TESTS

        #region UpdateEducationalInstitutionLocationMethod TESTS

        [Fact]
        public async Task GivenAValidEducationalInstitutionLocationUpdateRequest_ToUpdateEducationalInstitutionLocationMethod_ShouldReturnEducationalInstitutionUpdateResponseType()
        {
            //Arrange
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                UpdateLocation = true,
                LocationId = "newLocation123",
                UpdateBuildings = true,
                AddBuildingsIds = { new List<string>() { "newBuilding123" } },
                RemoveBuildingsIds = { new List<string>() { "oldBuilding012" } }
            };

            UpdateEducationalInstitutionLocationMethod_Setup();

            //Act
            var result = await service.UpdateEducationalInstitutionLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.IsType<EducationalInstitutionUpdateResponse>(result);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionLocationUpdateRequest_ToUpdateEducationalInstitutionLocationMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                UpdateLocation = true,
                LocationId = "newLocation123",
                UpdateBuildings = true,
                AddBuildingsIds = { new List<string>() { "newBuilding123" } },
                RemoveBuildingsIds = { new List<string>() { "oldBuilding012" } }
            };

            UpdateEducationalInstitutionLocationMethod_Setup();

            //Act
            var result = await service.UpdateEducationalInstitutionLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionLocationUpdateRequest_ToUpdateEducationalInstitutionLocationMethod_ShouldReturnTrueOperationStatus()
        {
            //Arrange
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                UpdateLocation = true,
                LocationId = "newLocation123",
                UpdateBuildings = true,
                AddBuildingsIds = { new List<string>() { "newBuilding123" } },
                RemoveBuildingsIds = { new List<string>() { "oldBuilding012" } }
            };

            UpdateEducationalInstitutionLocationMethod_Setup();

            //Act
            var result = await service.UpdateEducationalInstitutionLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionLocationUpdateRequest_ToUpdateEducationalInstitutionLocationMethod_ShouldReturnStatusCodeNoContent()
        {
            //Arrange
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                UpdateLocation = true,
                LocationId = "newLocation123",
                UpdateBuildings = true,
                AddBuildingsIds = { new List<string>() { "newBuilding123" } },
                RemoveBuildingsIds = { new List<string>() { "oldBuilding012" } }
            };

            UpdateEducationalInstitutionLocationMethod_Setup();

            //Act
            var result = await service.UpdateEducationalInstitutionLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.NoContent, result.StatusCode);
        }

        private void UpdateEducationalInstitutionLocationMethod_Setup()
        {
            var expectedMediatorResult = new Response()
            {
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<UpdateEducationalInstitutionLocationCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<UpdateEducationalInstitutionLocationCommand>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionLocationUpdateRequest_ToUpdateEducationalInstitutionLocationMethod_AnExceptionIsCaughtInHandler_ShouldReturnDefaultStatusCode()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateLocation = true,
                LocationId = "newLocation123",
                UpdateBuildings = true,
                AddBuildingsIds = { new List<string>() { "newBuilding123" } },
                RemoveBuildingsIds = { new List<string>() { "oldBuilding012" } }
            };

            UpdateEducationalInstitutionLocationMethod_SetupWhenAnExceptionIsCaughtInHandler(id);

            //Act
            var result = await service.UpdateEducationalInstitutionLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionLocationUpdateRequest_ToUpdateEducationalInstitutionLocationMethod_AnExceptionIsCaughtInHandler_ShouldReturnEmptyMessage()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateLocation = true,
                LocationId = "newLocation123",
                UpdateBuildings = true,
                AddBuildingsIds = { new List<string>() { "newBuilding123" } },
                RemoveBuildingsIds = { new List<string>() { "oldBuilding012" } }
            };

            UpdateEducationalInstitutionLocationMethod_SetupWhenAnExceptionIsCaughtInHandler(id);

            //Act
            var result = await service.UpdateEducationalInstitutionLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionLocationUpdateRequest_ToUpdateEducationalInstitutionLocationMethod_AnExceptionIsCaughtInHandler_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateLocation = true,
                LocationId = "newLocation123",
                UpdateBuildings = true,
                AddBuildingsIds = { new List<string>() { "newBuilding123" } },
                RemoveBuildingsIds = { new List<string>() { "oldBuilding012" } }
            };

            UpdateEducationalInstitutionLocationMethod_SetupWhenAnExceptionIsCaughtInHandler(id);

            //Act
            var result = await service.UpdateEducationalInstitutionLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        private void UpdateEducationalInstitutionLocationMethod_SetupWhenAnExceptionIsCaughtInHandler(Guid id)
        {
            var expectedMediatorResult = new Response()
            {
                Message = $"An error occurred while updating the Educational Institution with the following ID: {id}!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.InternalServerError
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<UpdateEducationalInstitutionLocationCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<UpdateEducationalInstitutionLocationCommand>(), It.IsAny<CancellationToken>()))
                                           .ReturnsAsync(expectedMediatorResult);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionLocationUpdateRequest_IDDoesntExistInDatabase_ToUpdateEducationalInstitutionLocationMethod_ShouldReturnStatusCodeDefault()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateLocation = true,
                LocationId = "newLocation123",
                UpdateBuildings = true,
                AddBuildingsIds = { new List<string>() { "newBuilding123" } },
                RemoveBuildingsIds = { new List<string>() { "oldBuilding012" } }
            };

            UpdateEducationalInstitutionLocationMethod_SetupToNotFindEntity(id);

            //Act
            var result = await service.UpdateEducationalInstitutionLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionLocationUpdateRequest_IDDoesntExistInDatabase_ToUpdateEducationalInstitutionLocationMethod_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateLocation = true,
                LocationId = "newLocation123",
                UpdateBuildings = true,
                AddBuildingsIds = { new List<string>() { "newBuilding123" } },
                RemoveBuildingsIds = { new List<string>() { "oldBuilding012" } }
            };

            UpdateEducationalInstitutionLocationMethod_SetupToNotFindEntity(id);

            //Act
            var result = await service.UpdateEducationalInstitutionLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionLocationUpdateRequest_IDDoesntExistInDatabase_ToUpdateEducationalInstitutionLocationMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateLocation = true,
                LocationId = "newLocation123",
                UpdateBuildings = true,
                AddBuildingsIds = { new List<string>() { "newBuilding123" } },
                RemoveBuildingsIds = { new List<string>() { "oldBuilding012" } }
            };

            UpdateEducationalInstitutionLocationMethod_SetupToNotFindEntity(id);

            //Act
            var result = await service.UpdateEducationalInstitutionLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        private void UpdateEducationalInstitutionLocationMethod_SetupToNotFindEntity(Guid id)
        {
            var expectedMediatorResult = new Response()
            {
                Message = $"Educational Institution with the following ID: {id} has not been found!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<UpdateEducationalInstitutionLocationCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<UpdateEducationalInstitutionLocationCommand>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionLocationUpdateRequest_ThatFailsValidation_ToUpdateEducationalInstitutionLocationMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                UpdateLocation = true,
                LocationId = "newLocation123",
                UpdateBuildings = true,
                AddBuildingsIds = { new List<string>() { "newBuilding123" } },
                RemoveBuildingsIds = { new List<string>() { "oldBuilding012" } }
            };

            SetupMockedDependenciesToFailValidation<UpdateEducationalInstitutionLocationCommand>();

            //Act
            var result = await service.UpdateEducationalInstitutionLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionLocationUpdateRequest_ThatFailsValidation_ToUpdateEducationalInstitutionLocationMethod_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                UpdateLocation = true,
                LocationId = "newLocation123",
                UpdateBuildings = true,
                AddBuildingsIds = { new List<string>() { "newBuilding123" } },
                RemoveBuildingsIds = { new List<string>() { "oldBuilding012" } }
            };

            SetupMockedDependenciesToFailValidation<UpdateEducationalInstitutionLocationCommand>();

            //Act
            var result = await service.UpdateEducationalInstitutionLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionLocationUpdateRequest_ThatFailsValidation_ToUpdateEducationalInstitutionLocationMethod_ShouldReturnDefaultStatusCode()
        {
            //Arrange
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                UpdateLocation = true,
                LocationId = "newLocation123",
                UpdateBuildings = true,
                AddBuildingsIds = { new List<string>() { "newBuilding123" } },
                RemoveBuildingsIds = { new List<string>() { "oldBuilding012" } }
            };

            SetupMockedDependenciesToFailValidation<UpdateEducationalInstitutionLocationCommand>();

            //Act
            var result = await service.UpdateEducationalInstitutionLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        #endregion UpdateEducationalInstitutionLocationMethod TESTS
    }
}