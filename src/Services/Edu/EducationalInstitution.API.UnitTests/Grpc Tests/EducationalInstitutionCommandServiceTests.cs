using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Commands_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Grpc;
using EducationalInstitutionAPI.Proto;
using EducationalInstitutionAPI.Utils;
using Moq;
using System;
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
                StatusCode = System.Net.HttpStatusCode.Created,
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
    }
}