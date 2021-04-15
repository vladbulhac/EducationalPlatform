using EducationalInstitutionAPI.Business.Commands_Handlers;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Unit_of_Work;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.Tests.UnitTests.BusinessTests.Commands_Handlers_Tests
{
    public class UpdateEducationalInstitutionCommandHandlerTests : IClassFixture<MockDependenciesHelper<UpdateEducationalInstitutionCommandHandler>>, IClassFixture<TestDataFromJSONParser>
    {
        private readonly MockDependenciesHelper<UpdateEducationalInstitutionCommandHandler> dependenciesHelper;
        private readonly TestDataFromJSONParser testDataHelper;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;

        public UpdateEducationalInstitutionCommandHandlerTests(MockDependenciesHelper<UpdateEducationalInstitutionCommandHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
            mockUnitOfWork = new();
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionUpdateCommand_WithNameDescription_ShouldReturnAResponseThatIncludesAStatusCodeNoContentField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;
            string name = "New_Name";
            string description = "New_Description";

            DTOEducationalInstitutionUpdateCommand request = new()
            {
                EduInstitutionID = educationalInstitutionID,
                UpdateName = true,
                Name = name,
                UpdateDescription = true,
                Description = description
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository
                                .Setup(r => r.UpdateNameDescriptionAsync(educationalInstitutionID, name, description, dependenciesHelper.cancellationToken))
                                .ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionUpdateCommand_WithNameDescription_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;
            string name = "New_Name";
            string description = "New_Description";

            DTOEducationalInstitutionUpdateCommand request = new()
            {
                EduInstitutionID = educationalInstitutionID,
                UpdateName = true,
                Name = name,
                UpdateDescription = true,
                Description = description
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository
                                .Setup(r => r.UpdateNameDescriptionAsync(educationalInstitutionID, name, description, dependenciesHelper.cancellationToken))
                                .ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionUpdateCommand_WithNameDescription_ShouldReturnAResponseThatIncludesATrueOperationStatusField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;
            string name = "New_Name";
            string description = "New_Description";

            DTOEducationalInstitutionUpdateCommand request = new()
            {
                EduInstitutionID = educationalInstitutionID,
                UpdateName = true,
                Name = name,
                UpdateDescription = true,
                Description = description
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository
                                .Setup(r => r.UpdateNameDescriptionAsync(educationalInstitutionID, name, description, dependenciesHelper.cancellationToken))
                                .ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionUpdateCommand_WithNameDescription_ShouldReturnANonGenericTypeResponse()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;
            string name = "New_Name";
            string description = "New_Description";

            DTOEducationalInstitutionUpdateCommand request = new()
            {
                EduInstitutionID = educationalInstitutionID,
                UpdateName = true,
                Name = name,
                UpdateDescription = true,
                Description = description
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository
                                .Setup(r => r.UpdateNameDescriptionAsync(educationalInstitutionID, name, description, dependenciesHelper.cancellationToken))
                                .ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<Response>(result);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionUpdateCommand_WithName_ShouldReturnAResponseThatIncludesAStatusCodeNoContentField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;
            string name = "New_Name";

            DTOEducationalInstitutionUpdateCommand request = new()
            {
                EduInstitutionID = educationalInstitutionID,
                UpdateName = true,
                Name = name,
                UpdateDescription = false,
                Description = string.Empty
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository
                                .Setup(r => r.UpdateNameAsync(educationalInstitutionID, name, dependenciesHelper.cancellationToken))
                                .ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionUpdateCommand_WithName_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;
            string name = "New_Name";

            DTOEducationalInstitutionUpdateCommand request = new()
            {
                EduInstitutionID = educationalInstitutionID,
                UpdateName = true,
                Name = name,
                UpdateDescription = false,
                Description = string.Empty
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository
                                .Setup(r => r.UpdateNameAsync(educationalInstitutionID, name, dependenciesHelper.cancellationToken))
                                .ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionUpdateCommand_WithName_ShouldReturnAResponseThatIncludesATrueOperationStatusField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;
            string name = "New_Name";

            DTOEducationalInstitutionUpdateCommand request = new()
            {
                EduInstitutionID = educationalInstitutionID,
                UpdateName = true,
                Name = name,
                UpdateDescription = false,
                Description = string.Empty
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository
                                .Setup(r => r.UpdateNameAsync(educationalInstitutionID, name, dependenciesHelper.cancellationToken))
                                .ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionUpdateCommand_WithName_ShouldReturnANonGenericTypeResponse()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;
            string name = "New_Name";

            DTOEducationalInstitutionUpdateCommand request = new()
            {
                EduInstitutionID = educationalInstitutionID,
                UpdateName = true,
                Name = name,
                UpdateDescription = false,
                Description = string.Empty
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository
                                .Setup(r => r.UpdateNameAsync(educationalInstitutionID, name, dependenciesHelper.cancellationToken))
                                .ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<Response>(result);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionUpdateCommand_WithDescription_ShouldReturnAResponseThatIncludesAStatusCodeNoContentField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;
            string description = "New_Description";

            DTOEducationalInstitutionUpdateCommand request = new()
            {
                EduInstitutionID = educationalInstitutionID,
                UpdateName = false,
                Name = string.Empty,
                UpdateDescription = true,
                Description = description
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository
                                .Setup(r => r.UpdateDescriptionAsync(educationalInstitutionID, description, dependenciesHelper.cancellationToken))
                                .ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionUpdateCommand_WithDescription_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;
            string description = "New_Description";

            DTOEducationalInstitutionUpdateCommand request = new()
            {
                EduInstitutionID = educationalInstitutionID,
                UpdateName = false,
                Name = string.Empty,
                UpdateDescription = true,
                Description = description
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository
                                .Setup(r => r.UpdateDescriptionAsync(educationalInstitutionID, description, dependenciesHelper.cancellationToken))
                                .ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionUpdateCommand_WithDescription_ShouldReturnAResponseThatIncludesATrueOperationStatusField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;
            string description = "New_Description";

            DTOEducationalInstitutionUpdateCommand request = new()
            {
                EduInstitutionID = educationalInstitutionID,
                UpdateName = false,
                Name = string.Empty,
                UpdateDescription = true,
                Description = description
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository
                                .Setup(r => r.UpdateDescriptionAsync(educationalInstitutionID, description, dependenciesHelper.cancellationToken))
                                .ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidDTOEducationalInstitutionUpdateCommand_WithDescription_ShouldReturnANonGenericTypeResponse()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;
            string description = "New_Description";

            DTOEducationalInstitutionUpdateCommand request = new()
            {
                EduInstitutionID = educationalInstitutionID,
                UpdateName = false,
                Name = string.Empty,
                UpdateDescription = true,
                Description = description
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository
                                .Setup(r => r.UpdateDescriptionAsync(educationalInstitutionID, description, dependenciesHelper.cancellationToken))
                                .ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<Response>(result);
        }

        [Fact]
        public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            UpdateEducationalInstitutionCommandHandler handler = new(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, dependenciesHelper.cancellationToken));
        }

        [Fact]
        public void GivenANullArgumentRepositoryToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionCommandHandler(null, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionCommandHandler(mockUnitOfWork.Object, null));
        }
    }
}