using EducationaInstitutionAPI.Data.Helpers.Queries_and_Commands_Results.Commands_Results;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Commands;
using EducationaInstitutionAPI.Grpc;
using EducationaInstitutionAPI.Proto;
using EducationaInstitutionAPI.Utils;
using EducationalInstitution.API.Tests.UnitTests.BusinessTests.QueriesTests.EducationalInstitutionTests;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.Tests.UnitTests.BusinessTests.Grpc_Tests
{
    public class EducationalInstitutionServiceTests : IClassFixture<MockDependenciesHelper<EducationalInstitutionService>>
    {
        private readonly MockDependenciesHelper<EducationalInstitutionService> dependenciesHelper;

        public EducationalInstitutionServiceTests(MockDependenciesHelper<EducationalInstitutionService> dependenciesHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
        }

        #region EducationalInstitutionService CreateEducationalInstitutionMethod TESTS

        [Fact]
        public async Task GivenADTOEducationalInstitutionCreateRequest_CreateEducationalInstitutionMethod_ShouldReturnAnEducationalInstitutionCreateResponse()
        {
            //Arrange
            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235"
            };
            request.Buildings.Add("building1235");

            var expectedMediatorResult = new Response<EducationalInstitutionCommandResult>()
            {
                ResponseObject = new() { EduInstitutionID = Guid.NewGuid() },
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
        public async Task GivenADTOEducationalInstitutionCreateRequest_CreateEducationalInstitutionMethod_ShouldReturnAResponseThatIncludesA_ProtoStatusCodeCreatedField()
        {
            //Arrange
            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235"
            };
            request.Buildings.Add("building1235");

            var expectedMediatorResult = new Response<EducationalInstitutionCommandResult>()
            {
                ResponseObject = new() { EduInstitutionID = Guid.NewGuid() },
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
        public async Task GivenADTOEducationalInstitutionCreateRequest_CreateEducationalInstitutionMethod_ShouldReturnAResponseThatIncludesA_MessageEmptyField()
        {
            //Arrange
            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235"
            };
            request.Buildings.Add("building1235");

            var expectedMediatorResult = new Response<EducationalInstitutionCommandResult>()
            {
                ResponseObject = new() { EduInstitutionID = Guid.NewGuid() },
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
        public async Task GivenADTOEducationalInstitutionCreateRequest_CreateEducationalInstitutionMethod_ShouldReturnAResponseThatIncludesAn_OperationStatusTrueField()
        {
            //Arrange
            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235"
            };
            request.Buildings.Add("building1235");

            var expectedMediatorResult = new Response<EducationalInstitutionCommandResult>()
            {
                ResponseObject = new() { EduInstitutionID = Guid.NewGuid() },
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
        public async Task GivenAnInvalidDTOEducationalInstitutionCreateRequest_CreateEducationalInstitutionMethod_ShouldReturnAResponseThatIncludesAn_OperationStatusFalseField()
        {
            //Arrange
            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "invalid_location_data"
            };
            request.Buildings.Add("invalid_building_data");

            string validationErrors = "LocationID validation failed!";
            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out validationErrors)).Returns(false);

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidDTOEducationalInstitutionCreateRequest_CreateEducationalInstitutionMethod_ShouldReturnAResponseThatIncludesA_ProtoStatusCodeBadRequestField()
        {
            //Arrange
            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "invalid_location_data"
            };
            request.Buildings.Add("invalid_building_data");

            string validationErrors = "LocationID validation failed!";
            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out validationErrors)).Returns(false);

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnInvalidDTOEducationalInstitutionCreateRequest_CreateEducationalInstitutionMethod_ShouldReturnAResponseThatIncludesA_NotEmptyMessageField()
        {
            //Arrange
            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "invalid_location_data"
            };
            request.Buildings.Add("invalid_building_data");

            string validationErrors = "LocationID validation failed!";
            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out validationErrors)).Returns(false);

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.NotEmpty(result.Message);
        }

        [Fact]
        public async Task GivenAnInvalidDTOEducationalInstitutionCreateRequest_CreateEducationalInstitutionMethod_ShouldReturnAResponseThatIncludesA_DefaultResponseObjectField()
        {
            //Arrange
            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "invalid_location_data"
            };
            request.Buildings.Add("invalid_building_data");

            string validationErrors = "LocationID validation failed!";
            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out validationErrors)).Returns(false);

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Null(result.ResponseObject);
        }

        [Fact]
        public async Task GivenADTOEducationalInstitutionCreateRequest_CreateEducationalInstitutionMethod_ShouldReturnAResponseThatIncludesA_DefaultResponseObjectField()
        {
            //Arrange
            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235"
            };
            request.Buildings.Add("building1235");

            var expectedMediatorResult = new Response<EducationalInstitutionCommandResult>()
            {
                ResponseObject = null,
                OperationStatus = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Message = $"An error occurred while creating the Educational Institution with data: !"
            };

            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTOEducationalInstitutionCreateCommand>(), out It.Ref<string>.IsAny)).Returns(true);
            dependenciesHelper.mockMediator.Setup(m => m.Send(It.IsAny<DTOEducationalInstitutionCreateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedMediatorResult);

            EducationalInstitutionService handler = new(dependenciesHelper.mockMediator.Object, dependenciesHelper.mockLogger.Object, dependenciesHelper.mockValidationHandler.Object);

            //Act
            var result = await handler.CreateEducationalInstitution(request, dependenciesHelper.mockServerCallContext.Object);

            //Assert
            Assert.Null(result.ResponseObject);
        }

        [Fact]
        public async Task GivenADTOEducationalInstitutionCreateRequest_CreateEducationalInstitutionMethod_ShouldReturnAResponseThatIncludesAn_OperationStatusFalseField()
        {
            //Arrange
            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235"
            };
            request.Buildings.Add("building1235");

            var expectedMediatorResult = new Response<EducationalInstitutionCommandResult>()
            {
                ResponseObject = null,
                OperationStatus = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Message = $"An error occurred while creating the Educational Institution with data: !"
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
        public async Task GivenADTOEducationalInstitutionCreateRequest_CreateEducationalInstitutionMethod_ShouldReturnAResponseThatIncludesA_ProtoStatusCodeInternalServerErrorField()
        {
            //Arrange
            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235"
            };
            request.Buildings.Add("building1235");

            var expectedMediatorResult = new Response<EducationalInstitutionCommandResult>()
            {
                ResponseObject = null,
                OperationStatus = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Message = $"An error occurred while creating the Educational Institution with data: !"
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
        public async Task GivenADTOEducationalInstitutionCreateRequest_CreateEducationalInstitutionMethod_ShouldReturnAResponseThatIncludesA_MessageField()
        {
            //Arrange
            DTOEducationalInstitutionCreateRequest request = new()
            {
                Name = "Educational_Institution_TestName",
                Description = "Educational_Institution_TestDescription",
                LocationId = "location1235"
            };
            request.Buildings.Add("building1235");

            var expectedMediatorResult = new Response<EducationalInstitutionCommandResult>()
            {
                ResponseObject = null,
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
            Assert.Equal("An error occurred while creating the Educational Institution with the given data!", result.Message);
        }

        #endregion EducationalInstitutionService CreateEducationalInstitutionMethod TESTS
    }
}