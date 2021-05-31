using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Commands_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Grpc;
using EducationalInstitutionAPI.Proto;
using EducationalInstitutionAPI.Utils.Mappers;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.UnitTests.Grpc_Tests
{
    public class EducationalInstitutionCommandServiceTests : IClassFixture<MockDependenciesHelper<EducationalInstitutionCommandService>>
    {
        private readonly MockDependenciesHelper<EducationalInstitutionCommandService> dependenciesHelper;

        /// <remarks>Called before each test</remarks>
        public EducationalInstitutionCommandServiceTests(MockDependenciesHelper<EducationalInstitutionCommandService> dependenciesHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
        }

        #region CreateEducationalInstitutionMethod TESTS

        [Fact]
        public async Task GivenAValidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_ShouldReturnAnEducationalInstitutionCreateResponse()
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

            var expectedMediatorResult = new Response<EducationalInstitutionCommandResult>()
            {
                Data = new() { EducationalInstitutionID = id },
                OperationStatus = true,
                StatusCode = HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionCreateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionCommandService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.IsType<EducationalInstitutionCreateResponse>(result);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_ShouldReturnA_ProtoStatusCodeCreatedField()
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

            var expectedMediatorResult = new Response<EducationalInstitutionCommandResult>()
            {
                Data = new() { EducationalInstitutionID = id },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionCreateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionCommandService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_ShouldReturnAResponseThatIncludesA_MessageEmptyField()
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

            var expectedMediatorResult = new Response<EducationalInstitutionCommandResult>()
            {
                Data = new() { EducationalInstitutionID = id },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionCreateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionCommandService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_ShouldReturnAResponseThatIncludesAn_OperationStatusTrueField()
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

            var expectedMediatorResult = new Response<EducationalInstitutionCommandResult>()
            {
                Data = new() { EducationalInstitutionID = id },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionCreateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionCommandService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_FailsTheValidation_ShouldReturnAResponseThatIncludesAn_OperationStatusFalseField()
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

            string validationErrors = "LocationID failed the validation process!";
            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out validationErrors)).Returns(false);

            EducationalInstitutionCommandService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

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

            string validationErrors = "LocationID failed the validation process!";
            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out validationErrors)).Returns(false);

            EducationalInstitutionCommandService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

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

            string validationErrors = "LocationID failed the validation process!";
            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out validationErrors)).Returns(false);

            EducationalInstitutionCommandService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_DatabaseInsertOperationFails_ShouldReturnANullDataField()
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

            var expectedMediatorResult = new Response<EducationalInstitutionCommandResult>()
            {
                Data = null,
                OperationStatus = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Message = "An error occurred while creating the Educational Institution with the given data!"
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionCreateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionCommandService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_DatabaseInsertOperationFails_ShouldReturnAFalseOperationStatusField()
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

            var expectedMediatorResult = new Response<EducationalInstitutionCommandResult>()
            {
                Data = null,
                OperationStatus = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Message = "An error occurred while creating the Educational Institution with the given data!"
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionCreateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionCommandService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
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

            var expectedMediatorResult = new Response<EducationalInstitutionCommandResult>()
            {
                Data = new() { EducationalInstitutionID = Guid.NewGuid() },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.MultiStatus,
                Message = "Educational Institution was created but parent institution was not found!"
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionCreateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionCommandService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal("Educational Institution was created but parent institution was not found!", result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_ParentIsNotFound_ShouldReturnAnOperationStatusTrueField()
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

            var expectedMediatorResult = new Response<EducationalInstitutionCommandResult>()
            {
                Data = new() { EducationalInstitutionID = Guid.NewGuid() },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.MultiStatus,
                Message = "Educational Institution was created but parent institution was not found!"
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionCreateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionCommandService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

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

            var expectedMediatorResult = new Response<EducationalInstitutionCommandResult>()
            {
                Data = new() { EducationalInstitutionID = Guid.NewGuid() },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.MultiStatus,
                Message = "Educational Institution was created but parent institution was not found!"
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionCreateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionCommandService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

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

            var expectedMediatorResult = new Response<EducationalInstitutionCommandResult>()
            {
                Data = new() { EducationalInstitutionID = Guid.NewGuid() },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.MultiStatus,
                Message = "Educational Institution was created but parent institution was not found!"
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionCreateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionCommandService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.NotNull(result.Data);
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

            var expectedMediatorResult = new Response<EducationalInstitutionCommandResult>()
            {
                Data = new() { EducationalInstitutionID = id },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionCreateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionCommandService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_WithDefaultParentInstitutionID_ShouldReturnAnEmptyMessageField()
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

            var expectedMediatorResult = new Response<EducationalInstitutionCommandResult>()
            {
                Data = new() { EducationalInstitutionID = Guid.NewGuid() },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionCreateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionCommandService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

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

            var expectedMediatorResult = new Response<EducationalInstitutionCommandResult>()
            {
                Data = new() { EducationalInstitutionID = Guid.NewGuid() },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionCreateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionCommandService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_WithDefaultParentInstitutionID_ShouldAGuidIDField()
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

            var expectedMediatorResult = new Response<EducationalInstitutionCommandResult>()
            {
                Data = new() { EducationalInstitutionID = Guid.NewGuid() },
                OperationStatus = true,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = string.Empty
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionCreateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionCommandService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(expectedMediatorResult.Data.EducationalInstitutionID.ToProtoUuid(), result.Data.EducationalInstitutionId);
        }

        #endregion CreateEducationalInstitutionMethod TESTS

        #region DeleteEducationalInstitutionMethod TESTS

        [Fact]
        public async Task GivenAValidEducationalInstitutionDeleteRequest_ToDeleteEducationalInstitutionMethod_ShouldReturnEducationalInstitutionDeleteResponseType()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            EducationalInstitutionDeleteRequest request = new() { EducationalInstitutionId = educationalInstitutionID.ToProtoUuid() };

            Response<DeleteEducationalInstitutionCommandResult> expectedMediatorResult = new()
            {
                Data = new() { DateForPermanentDeletion = DateTime.UtcNow },
                OperationStatus = true,
                Message = string.Empty,
                StatusCode = HttpStatusCode.Accepted
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionDeleteCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionDeleteCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);
            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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

            Response<DeleteEducationalInstitutionCommandResult> expectedMediatorResult = new()
            {
                Data = new() { DateForPermanentDeletion = DateTime.UtcNow },
                OperationStatus = true,
                Message = string.Empty,
                StatusCode = HttpStatusCode.Accepted
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionDeleteCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionDeleteCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);
            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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

            Response<DeleteEducationalInstitutionCommandResult> expectedMediatorResult = new()
            {
                Data = new() { DateForPermanentDeletion = DateTime.UtcNow },
                OperationStatus = true,
                Message = string.Empty,
                StatusCode = HttpStatusCode.Accepted
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionDeleteCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionDeleteCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);
            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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

            Response<DeleteEducationalInstitutionCommandResult> expectedMediatorResult = new()
            {
                Data = new() { DateForPermanentDeletion = DateTime.UtcNow },
                OperationStatus = true,
                Message = string.Empty,
                StatusCode = HttpStatusCode.Accepted
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionDeleteCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionDeleteCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);
            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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

            Response<DeleteEducationalInstitutionCommandResult> expectedMediatorResult = new()
            {
                Data = new() { DateForPermanentDeletion = DateTime.UtcNow },
                OperationStatus = true,
                Message = string.Empty,
                StatusCode = HttpStatusCode.Accepted
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionDeleteCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionDeleteCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);
            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.DeleteEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Accepted, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionDeleteRequest_WithIDThatDoesntExistInDatabase_ToDeleteEducationalInstitutionMethod_ShouldReturnHttpStatusCodeNotDefault()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            EducationalInstitutionDeleteRequest request = new() { EducationalInstitutionId = educationalInstitutionID.ToProtoUuid() };

            Response<DeleteEducationalInstitutionCommandResult> expectedMediatorResult = new()
            {
                Data = null,
                OperationStatus = false,
                Message = $"Educational Institution with the following ID: {educationalInstitutionID} has not been found!",
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionDeleteCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionDeleteCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);
            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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

            Response<DeleteEducationalInstitutionCommandResult> expectedMediatorResult = new()
            {
                Data = null,
                OperationStatus = false,
                Message = $"Educational Institution with the following ID: {educationalInstitutionID} has not been found!",
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionDeleteCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionDeleteCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);
            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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

            Response<DeleteEducationalInstitutionCommandResult> expectedMediatorResult = new()
            {
                Data = null,
                OperationStatus = false,
                Message = $"Educational Institution with the following ID: {educationalInstitutionID} has not been found!",
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionDeleteCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionDeleteCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);
            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.DeleteEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GivenAnInvalidEducationalInstitutionDeleteRequest_ThatFailsTheValidation_ToDeleteEducationalInstitutionMethod_ShouldReturnNullData()
        {
            //Arrange
            Guid educationalInstitutionID = default;
            EducationalInstitutionDeleteRequest request = new() { EducationalInstitutionId = educationalInstitutionID.ToProtoUuid() };

            Response<DeleteEducationalInstitutionCommandResult> expectedMediatorResult = new()
            {
                Data = null,
                OperationStatus = false,
                Message = $"Educational Institution with the following ID: {educationalInstitutionID} has not been found!",
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionDeleteCommand>(), out It.Ref<string>.IsAny)).Returns(false);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionDeleteCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);
            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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

            Response<DeleteEducationalInstitutionCommandResult> expectedMediatorResult = new()
            {
                Data = null,
                OperationStatus = false,
                Message = $"Educational Institution with the following ID: {educationalInstitutionID} has not been found!",
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionDeleteCommand>(), out It.Ref<string>.IsAny)).Returns(false);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionDeleteCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);
            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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

            Response<DeleteEducationalInstitutionCommandResult> expectedMediatorResult = new()
            {
                Data = null,
                OperationStatus = false,
                Message = $"Educational Institution with the following ID: {educationalInstitutionID} has not been found!",
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionDeleteCommand>(), out It.Ref<string>.IsAny)).Returns(false);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionDeleteCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);
            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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

            Response<DeleteEducationalInstitutionCommandResult> expectedMediatorResult = new()
            {
                Data = null,
                OperationStatus = false,
                Message = $"Educational Institution with the following ID: {educationalInstitutionID} has not been found!",
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionDeleteCommand>(), out It.Ref<string>.IsAny)).Returns(false);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionDeleteCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);
            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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
            var expectedMediatorResult = new Response()
            {
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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
            var expectedMediatorResult = new Response()
            {
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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
            var expectedMediatorResult = new Response()
            {
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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
            var expectedMediatorResult = new Response()
            {
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.UpdateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionUpdateRequest_IDDoesntExistInDatabase_ToUpdateEducationalInstitutionMethod_ShouldReturnStatusCodeDefault()
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
            var expectedMediatorResult = new Response()
            {
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound,
                Message = $"Educational Institution with the following ID: {request.EducationalInstitutionId} has not been found!"
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.UpdateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionUpdateRequest_IDDoesntExistInDatabase_ToUpdateEducationalInstitutionMethod_ShouldReturnEmptyMessage()
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
            var expectedMediatorResult = new Response()
            {
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound,
                Message = $"Educational Institution with the following ID: {request.EducationalInstitutionId} has not been found!"
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.UpdateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionUpdateRequest_IDDoesntExistInDatabase_ToUpdateEducationalInstitutionMethod_ShouldReturnFalseOperationStatus()
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
            var expectedMediatorResult = new Response()
            {
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound,
                Message = $"Educational Institution with the following ID: {request.EducationalInstitutionId} has not been found!"
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.UpdateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionUpdateRequest_ToUpdateEducationalInstitutionMethod_AnExceptionIsCaughtInHandler_ShouldReturnStatusCodeDefault()
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
            var expectedMediatorResult = new Response()
            {
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occurred while updating the Educational Institution with the following ID: {0}!"
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.UpdateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionUpdateRequest_ToUpdateEducationalInstitutionMethod_AnExceptionIsCaughtInHandler_ShouldReturnEmptyMessage()
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
            var expectedMediatorResult = new Response()
            {
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occurred while updating the Educational Institution with the following ID: {0}!"
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.UpdateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionUpdateRequest_ToUpdateEducationalInstitutionMethod_AnExceptionIsCaughtInHandler_ShouldReturnFalseOperationStatus()
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
            var expectedMediatorResult = new Response()
            {
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound,
                Message = "An error occurred while updating the Educational Institution with the following ID: {0}!"
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(false);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(false);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(false);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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
            var expectedMediatorResult = new Response()
            {
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object,
                                                                    dependenciesHelper.mockLogger.Object,
                                                                    dependenciesHelper.mockValidationHandler.Object);

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
            var expectedMediatorResult = new Response()
            {
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object,
                                                                    dependenciesHelper.mockLogger.Object,
                                                                    dependenciesHelper.mockValidationHandler.Object);

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
            var expectedMediatorResult = new Response()
            {
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object,
                                                                    dependenciesHelper.mockLogger.Object,
                                                                    dependenciesHelper.mockValidationHandler.Object);

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
            var expectedMediatorResult = new Response()
            {
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object,
                                                                    dependenciesHelper.mockLogger.Object,
                                                                    dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.UpdateEducationalInstitutionAdmin(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionAdminUpdateRequest_IDDoesntExistInDatabase_ToUpdateEducationalInstitutionAdminMethod_ShouldReturnStatusCodeDefault()
        {
            //Arrange
            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                AddAdminsIds = { Guid.NewGuid().ToProtoUuid() },
                RemoveAdminsIds = { Guid.NewGuid().ToProtoUuid() }
            };
            var expectedMediatorResult = new Response()
            {
                Message = $"Educational Institution with the following ID: {request.EducationalInstitutionId} has not been found!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object,
                                                                    dependenciesHelper.mockLogger.Object,
                                                                    dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.UpdateEducationalInstitutionAdmin(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionAdminUpdateRequest_IDDoesntExistInDatabase_ToUpdateEducationalInstitutionAdminMethod_ShouldReturnEmptyMessage()
        {
            //Arrange
            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                AddAdminsIds = { Guid.NewGuid().ToProtoUuid() },
                RemoveAdminsIds = { Guid.NewGuid().ToProtoUuid() }
            };
            var expectedMediatorResult = new Response()
            {
                Message = $"Educational Institution with the following ID: {request.EducationalInstitutionId} has not been found!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object,
                                                                    dependenciesHelper.mockLogger.Object,
                                                                    dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.UpdateEducationalInstitutionAdmin(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionAdminUpdateRequest_IDDoesntExistInDatabase_ToUpdateEducationalInstitutionAdminMethod_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = Guid.NewGuid().ToProtoUuid(),
                AddAdminsIds = { Guid.NewGuid().ToProtoUuid() },
                RemoveAdminsIds = { Guid.NewGuid().ToProtoUuid() }
            };
            var expectedMediatorResult = new Response()
            {
                Message = $"Educational Institution with the following ID: {request.EducationalInstitutionId} has not been found!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object,
                                                                    dependenciesHelper.mockLogger.Object,
                                                                    dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.UpdateEducationalInstitutionAdmin(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
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
            var expectedMediatorResult = new Response()
            {
                Message = "An error occurred while updating the Educational Institution's admins with the given data!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.InternalServerError
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);

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
            var expectedMediatorResult = new Response()
            {
                Message = "An error occurred while updating the Educational Institution's admins with the given data!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.InternalServerError
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object,
                                                                    dependenciesHelper.mockLogger.Object,
                                                                    dependenciesHelper.mockValidationHandler.Object);

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
            var expectedMediatorResult = new Response()
            {
                Message = "An error occurred while updating the Educational Institution's admins with the given data!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.InternalServerError
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object,
                                                                    dependenciesHelper.mockLogger.Object,
                                                                    dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.UpdateEducationalInstitutionAdmin(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
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

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), It.IsAny<CancellationToken>()))
                                            .ThrowsAsync(new InvalidOperationException("Test"));

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object,
                                                                    dependenciesHelper.mockLogger.Object,
                                                                    dependenciesHelper.mockValidationHandler.Object);

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

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), It.IsAny<CancellationToken>()))
                                            .ThrowsAsync(new InvalidOperationException("Test"));

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object,
                                                                    dependenciesHelper.mockLogger.Object,
                                                                    dependenciesHelper.mockValidationHandler.Object);

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

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(true);

            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), It.IsAny<CancellationToken>()))
                                            .ThrowsAsync(new InvalidOperationException("Test"));

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object,
                                                                    dependenciesHelper.mockLogger.Object,
                                                                    dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.UpdateEducationalInstitutionAdmin(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
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

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(false);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object,
                                                                    dependenciesHelper.mockLogger.Object,
                                                                    dependenciesHelper.mockValidationHandler.Object);

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

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(false);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object,
                                                                    dependenciesHelper.mockLogger.Object,
                                                                    dependenciesHelper.mockValidationHandler.Object);

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

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionAdminUpdateCommand>(), out It.Ref<string>.IsAny))
                                                    .Returns(false);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object,
                                                                    dependenciesHelper.mockLogger.Object,
                                                                    dependenciesHelper.mockValidationHandler.Object);

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

            var expectedMediatorResult = new Response()
            {
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionParentUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionParentUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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

            var expectedMediatorResult = new Response()
            {
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionParentUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionParentUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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

            var expectedMediatorResult = new Response()
            {
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionParentUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionParentUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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

            var expectedMediatorResult = new Response()
            {
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionParentUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionParentUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.UpdateEducationalInstitutionParent(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.NoContent, result.StatusCode);
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

            var expectedMediatorResult = new Response()
            {
                Message = $"The Parent Educational Institution with the following ID: {request.ParentInstitutionId} has not been found!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionParentUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionParentUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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

            var expectedMediatorResult = new Response()
            {
                Message = $"The Parent Educational Institution with the following ID: {request.ParentInstitutionId} has not been found!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionParentUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionParentUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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

            var expectedMediatorResult = new Response()
            {
                Message = $"The Parent Educational Institution with the following ID: {request.ParentInstitutionId} has not been found!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionParentUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionParentUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.UpdateEducationalInstitutionParent(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
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

            var expectedMediatorResult = new Response()
            {
                Message = $"An error occurred while updating the Educational Institution with the following ID: {educationalInstitutionID}!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.InternalServerError
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionParentUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionParentUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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

            var expectedMediatorResult = new Response()
            {
                Message = $"An error occurred while updating the Educational Institution with the following ID: {educationalInstitutionID}!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.InternalServerError
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionParentUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionParentUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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

            var expectedMediatorResult = new Response()
            {
                Message = $"An error occurred while updating the Educational Institution with the following ID: {educationalInstitutionID}!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.InternalServerError
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionParentUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionParentUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionParentUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(false);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionParentUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(false);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionParentUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(false);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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
            var expectedMediatorResult = new Response()
            {
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionLocationUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionLocationUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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
            var expectedMediatorResult = new Response()
            {
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionLocationUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionLocationUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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
            var expectedMediatorResult = new Response()
            {
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionLocationUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionLocationUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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
            var expectedMediatorResult = new Response()
            {
                Message = string.Empty,
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionLocationUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionLocationUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.UpdateEducationalInstitutionLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionLocationUpdateRequest_ToUpdateEducationalInstitutionLocationMethod_AnExceptionIsCaughtInHandler_ShouldReturnDefaultStatusCode()
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
            var expectedMediatorResult = new Response()
            {
                Message = $"An error occurred while updating the Educational Institution with the following ID: {request.EducationalInstitutionId.ToGuid()}!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.InternalServerError
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionLocationUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionLocationUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.UpdateEducationalInstitutionLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionLocationUpdateRequest_ToUpdateEducationalInstitutionLocationMethod_AnExceptionIsCaughtInHandler_ShouldReturnEmptyMessage()
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
            var expectedMediatorResult = new Response()
            {
                Message = $"An error occurred while updating the Educational Institution with the following ID: {request.EducationalInstitutionId.ToGuid()}!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.InternalServerError
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionLocationUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionLocationUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.UpdateEducationalInstitutionLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionLocationUpdateRequest_ToUpdateEducationalInstitutionLocationMethod_AnExceptionIsCaughtInHandler_ShouldReturnFalseOperationStatus()
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
            var expectedMediatorResult = new Response()
            {
                Message = $"An error occurred while updating the Educational Institution with the following ID: {request.EducationalInstitutionId.ToGuid()}!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.InternalServerError
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionLocationUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionLocationUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.UpdateEducationalInstitutionLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionLocationUpdateRequest_IDDoesntExistInDatabase_ToUpdateEducationalInstitutionLocationMethod_ShouldReturnStatusCodeDefault()
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
            var expectedMediatorResult = new Response()
            {
                Message = $"Educational Institution with the following ID: {request.EducationalInstitutionId.ToGuid()} has not been found!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionLocationUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionLocationUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.UpdateEducationalInstitutionLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionLocationUpdateRequest_IDDoesntExistInDatabase_ToUpdateEducationalInstitutionLocationMethod_ShouldReturnFalseOperationStatus()
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
            var expectedMediatorResult = new Response()
            {
                Message = $"Educational Institution with the following ID: {request.EducationalInstitutionId.ToGuid()} has not been found!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionLocationUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionLocationUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.UpdateEducationalInstitutionLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidEducationalInstitutionLocationUpdateRequest_IDDoesntExistInDatabase_ToUpdateEducationalInstitutionLocationMethod_ShouldReturnEmptyMessage()
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
            var expectedMediatorResult = new Response()
            {
                Message = $"Educational Institution with the following ID: {request.EducationalInstitutionId.ToGuid()} has not been found!",
                OperationStatus = false,
                StatusCode = HttpStatusCode.NotFound
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionLocationUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionLocationUpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.UpdateEducationalInstitutionLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
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

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionLocationUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(false);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionLocationUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(false);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

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

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionLocationUpdateCommand>(), out It.Ref<string>.IsAny)).Returns(false);

            var service = new EducationalInstitutionCommandService(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await service.UpdateEducationalInstitutionLocation(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Default, result.StatusCode);
        }

        #endregion UpdateEducationalInstitutionLocationMethod TESTS
    }
}