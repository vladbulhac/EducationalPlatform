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
    public class EducationalInstitutionServiceTests : IClassFixture<MockDependenciesHelper<EducationalInstitutionService>>
    {
        private readonly MockDependenciesHelper<EducationalInstitutionService> dependenciesHelper;

        /// <remarks>Called before each test</remarks>
        public EducationalInstitutionServiceTests(MockDependenciesHelper<EducationalInstitutionService> dependenciesHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
        }

        #region EducationalInstitutionService CreateEducationalInstitutionMethod TESTS

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_ShouldReturnAnEducationalInstitutionCreateResponse()
        {
            //Arrange
            var id = Guid.NewGuid();
            id.Encode(out UInt64 high64, out UInt64 low64);

            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = new Uuid()
                {
                    High64 = high64,
                    Low64 = low64
                }
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

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.IsType<EducationalInstitutionCreateResponse>(result);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_ShouldReturnA_ProtoStatusCodeCreatedField()
        {
            //Arrange
            var id = Guid.NewGuid();
            id.Encode(out UInt64 high64, out UInt64 low64);

            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = new Uuid()
                {
                    High64 = high64,
                    Low64 = low64
                }
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

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_ShouldReturnAResponseThatIncludesA_MessageEmptyField()
        {
            //Arrange
            var id = Guid.NewGuid();
            id.Encode(out UInt64 high64, out UInt64 low64);

            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = new Uuid()
                {
                    High64 = high64,
                    Low64 = low64
                }
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

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_ShouldReturnAResponseThatIncludesAn_OperationStatusTrueField()
        {
            //Arrange
            var id = Guid.NewGuid();
            id.Encode(out UInt64 high64, out UInt64 low64);

            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = new Uuid()
                {
                    High64 = high64,
                    Low64 = low64
                }
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

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidDTOEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_FailsTheValidation_ShouldReturnAResponseThatIncludesAn_OperationStatusFalseField()
        {
            //Arrange
            var id = Guid.NewGuid();
            id.Encode(out UInt64 high64, out UInt64 low64);

            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location_invalid_data",
                ParentInstitutionId = new Uuid()
                {
                    High64 = high64,
                    Low64 = low64
                }
            };
            request.Buildings.Add("building_invalid_data");

            string validationErrors = "LocationID failed the validation process!";
            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out validationErrors)).Returns(false);

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidDTOEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_FailsTheValidation_ShouldReturnAResponseThatIncludesA_NotEmptyMessageField()
        {
            //Arrange
            var id = Guid.NewGuid();
            id.Encode(out UInt64 high64, out UInt64 low64);

            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location_invalid_data",
                ParentInstitutionId = new Uuid()
                {
                    High64 = high64,
                    Low64 = low64
                }
            };
            request.Buildings.Add("building_invalid_data");

            string validationErrors = "LocationID failed the validation process!";
            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out validationErrors)).Returns(false);

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.NotEmpty(result.Message);
        }

        [Fact]
        public async Task GivenAnInvalidDTOEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_FailsTheValidation_ShouldReturnAResponseThatIncludesA_DefaultDataField()
        {
            //Arrange
            var id = Guid.NewGuid();
            id.Encode(out UInt64 high64, out UInt64 low64);

            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location_invalid_data",
                ParentInstitutionId = new Uuid()
                {
                    High64 = high64,
                    Low64 = low64
                }
            };
            request.Buildings.Add("building_invalid_data");

            string validationErrors = "LocationID failed the validation process!";
            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out validationErrors)).Returns(false);

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GivenAnInvalidDTOEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_FailsTheValidation_ShouldReturnAResponseThatIncludesA_ProtoStatusCodeBadRequestField()
        {
            //Arrange
            var id = Guid.NewGuid();
            id.Encode(out UInt64 high64, out UInt64 low64);

            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location_invalid_data",
                ParentInstitutionId = new Uuid()
                {
                    High64 = high64,
                    Low64 = low64
                }
            };
            request.Buildings.Add("building_invalid_data");

            string validationErrors = "LocationID failed the validation process!";
            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out validationErrors)).Returns(false);

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_DatabaseInsertOperationFails_ShouldReturnANullDataField()
        {
            //Arrange
            var id = Guid.NewGuid();
            id.Encode(out UInt64 high64, out UInt64 low64);

            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = new Uuid()
                {
                    High64 = high64,
                    Low64 = low64
                }
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

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_DatabaseInsertOperationFails_ShouldReturnAFalseOperationStatusField()
        {
            //Arrange
            var id = Guid.NewGuid();
            id.Encode(out UInt64 high64, out UInt64 low64);

            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = new Uuid()
                {
                    High64 = high64,
                    Low64 = low64
                }
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

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_DatabaseInsertOperationFails_ShouldReturnAProtoStatusCodeInternalServerErrorField()
        {
            //Arrange
            var id = Guid.NewGuid();
            id.Encode(out UInt64 high64, out UInt64 low64);

            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = new Uuid()
                {
                    High64 = high64,
                    Low64 = low64
                }
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

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_DatabaseInsertOperationFails_ShouldReturnAMessageField()
        {
            //Arrange
            var id = Guid.NewGuid();
            id.Encode(out UInt64 high64, out UInt64 low64);

            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = new Uuid()
                {
                    High64 = high64,
                    Low64 = low64
                }
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

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal("An error occurred while processing the request!", result.Message);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_ParentIsNotFound_ShouldReturnAMessageField()
        {
            //Arrange
            var id = Guid.NewGuid();
            id.Encode(out UInt64 high64, out UInt64 low64);

            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = new Uuid()
                {
                    High64 = high64,
                    Low64 = low64
                }
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

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal("Educational Institution was created but parent institution was not found!", result.Message);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_ParentIsNotFound_ShouldReturnAnOperationStatusTrueField()
        {
            //Arrange
            var id = Guid.NewGuid();
            id.Encode(out UInt64 high64, out UInt64 low64);

            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = new Uuid()
                {
                    High64 = high64,
                    Low64 = low64
                }
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

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_ParentIsNotFound_ShouldReturnAProtoStatusCodeMultiStatusField()
        {
            //Arrange
            var id = Guid.NewGuid();
            id.Encode(out UInt64 high64, out UInt64 low64);

            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = new Uuid()
                {
                    High64 = high64,
                    Low64 = low64
                }
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

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(HttpStatusCode.MultiStatus, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_ParentIsNotFound_ShouldReturnDataThatContainsAnId()
        {
            //Arrange
            var id = Guid.NewGuid();
            id.Encode(out UInt64 high64, out UInt64 low64);

            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = new Uuid()
                {
                    High64 = high64,
                    Low64 = low64
                }
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

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_WithDefaultParentInstitutionID_ShouldReturnAProtoStatusCodeCreatedField()
        {
            //Arrange
            var id = Guid.Empty;
            id.Encode(out UInt64 high64, out UInt64 low64);

            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = new Uuid()
                {
                    High64 = high64,
                    Low64 = low64
                }
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

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_WithDefaultParentInstitutionID_ShouldReturnAnEmptyMessageField()
        {
            //Arrange
            var id = Guid.Empty;
            id.Encode(out UInt64 high64, out UInt64 low64);

            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = new Uuid()
                {
                    High64 = high64,
                    Low64 = low64
                }
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

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_WithDefaultParentInstitutionID_ShouldReturnATrueOperationStatusField()
        {
            //Arrange
            var id = Guid.Empty;
            id.Encode(out UInt64 high64, out UInt64 low64);

            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = new Uuid()
                {
                    High64 = high64,
                    Low64 = low64
                }
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

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionCreateRequest_ToCreateEducationalInstitutionMethod_WithDefaultParentInstitutionID_ShouldAGuidIDField()
        {
            //Arrange
            var id = Guid.Empty;
            id.Encode(out UInt64 high64, out UInt64 low64);

            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235",
                ParentInstitutionId = new Uuid()
                {
                    High64 = high64,
                    Low64 = low64
                }
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

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(expectedMediatorResult.Data.EducationalInstitutionID.ToProtocolBufferLanguageEquivalent(), result.Data.EducationalInstitutionId);
        }

        #endregion EducationalInstitutionService CreateEducationalInstitutionMethod TESTS
    }
}