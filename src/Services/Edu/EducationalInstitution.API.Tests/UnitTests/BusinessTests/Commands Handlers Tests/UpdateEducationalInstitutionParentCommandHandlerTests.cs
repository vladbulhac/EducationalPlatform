using EducationalInstitutionAPI.Business.Commands_Handlers;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Unit_of_Work;
using Moq;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.Tests.UnitTests.BusinessTests.Commands_Handlers_Tests
{
    public class UpdateEducationalInstitutionParentCommandHandlerTests : IClassFixture<MockDependenciesHelper<UpdateEducationalInstitutionParentCommandHandler>>, IClassFixture<TestDataFromJSONParser>
    {
        private readonly MockDependenciesHelper<UpdateEducationalInstitutionParentCommandHandler> dependenciesHelper;
        private readonly TestDataFromJSONParser testDataHelper;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;

        public UpdateEducationalInstitutionParentCommandHandlerTests(MockDependenciesHelper<UpdateEducationalInstitutionParentCommandHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
            mockUnitOfWork = new();
        }

        [Fact]
        public async Task GivenAValidRequestOfTypeDTOEducationalInstitutionParentUpdateCommand_ShouldReturnAResponseThatIncludesAStatusCodeNoContentField()
        {
            //Arrange
            DTOEducationalInstitutionParentUpdateCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID,
                ParentInstitutionID = testDataHelper.EducationalInstitutions[1].EducationalInstitutionID
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(r => r.GetEntityByIDAsync(request.ParentInstitutionID, It.IsAny<CancellationToken>()))
                                                .ReturnsAsync(testDataHelper.EducationalInstitutions[1]);
            dependenciesHelper.mockRepository.Setup(r => r.UpdateParentInstitutionAsync(request.EducationalInstitutionID, testDataHelper.EducationalInstitutions[1], It.IsAny<CancellationToken>()))
                                                .ReturnsAsync(true);

            var handler = new UpdateEducationalInstitutionParentCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidRequestOfTypeDTOEducationalInstitutionParentUpdateCommand_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            DTOEducationalInstitutionParentUpdateCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID,
                ParentInstitutionID = testDataHelper.EducationalInstitutions[1].EducationalInstitutionID
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(r => r.GetEntityByIDAsync(request.ParentInstitutionID, It.IsAny<CancellationToken>()))
                                                .ReturnsAsync(testDataHelper.EducationalInstitutions[1]);
            dependenciesHelper.mockRepository.Setup(r => r.UpdateParentInstitutionAsync(request.EducationalInstitutionID, testDataHelper.EducationalInstitutions[1], It.IsAny<CancellationToken>()))
                                                .ReturnsAsync(true);

            var handler = new UpdateEducationalInstitutionParentCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidRequestOfTypeDTOEducationalInstitutionParentUpdateCommand_ShouldReturnAResponseThatIncludesATrueOperationStatusField()
        {
            //Arrange
            DTOEducationalInstitutionParentUpdateCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID,
                ParentInstitutionID = testDataHelper.EducationalInstitutions[1].EducationalInstitutionID
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(r => r.GetEntityByIDAsync(request.ParentInstitutionID, It.IsAny<CancellationToken>()))
                                                .ReturnsAsync(testDataHelper.EducationalInstitutions[1]);
            dependenciesHelper.mockRepository.Setup(r => r.UpdateParentInstitutionAsync(request.EducationalInstitutionID, testDataHelper.EducationalInstitutions[1], It.IsAny<CancellationToken>()))
                                                .ReturnsAsync(true);

            var handler = new UpdateEducationalInstitutionParentCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidRequestOfTypeDTOEducationalInstitutionParentUpdateCommand_WithParentInstitutionIDNotInDatabase_ShouldReturnAResponseThatIncludesAStatusCodeNotFoundField()
        {
            //Arrange
            DTOEducationalInstitutionParentUpdateCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID,
                ParentInstitutionID = Guid.NewGuid()
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(r => r.GetEntityByIDAsync(request.ParentInstitutionID, It.IsAny<CancellationToken>()))
                                                .Returns(Task.FromResult<EducationalInstitutionAPI.Data.EducationalInstitution>(null));

            var handler = new UpdateEducationalInstitutionParentCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnInvalidRequestOfTypeDTOEducationalInstitutionParentUpdateCommand_WithParentInstitutionIDNotInDatabase_ShouldReturnAResponseThatIncludesAMessageField()
        {
            //Arrange
            DTOEducationalInstitutionParentUpdateCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID,
                ParentInstitutionID = Guid.NewGuid()
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(r => r.GetEntityByIDAsync(request.ParentInstitutionID, It.IsAny<CancellationToken>()))
                                                .Returns(Task.FromResult<EducationalInstitutionAPI.Data.EducationalInstitution>(null));

            var handler = new UpdateEducationalInstitutionParentCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal($"The Parent Educational Institution with the following ID: {request.ParentInstitutionID} has not been found!", result.Message);
        }

        [Fact]
        public async Task GivenAnInvalidRequestOfTypeDTOEducationalInstitutionParentUpdateCommand_WithParentInstitutionIDNotInDatabase_ShouldReturnAResponseThatIncludesAFalseOperationStatusField()
        {
            //Arrange
            DTOEducationalInstitutionParentUpdateCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID,
                ParentInstitutionID = Guid.NewGuid()
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(r => r.GetEntityByIDAsync(request.ParentInstitutionID, It.IsAny<CancellationToken>()))
                                                .Returns(Task.FromResult<EducationalInstitutionAPI.Data.EducationalInstitution>(null));

            var handler = new UpdateEducationalInstitutionParentCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidRequestOfTypeDTOEducationalInstitutionParentUpdateCommand_WithEducationalInstitutionIDNotInDatabase_ShouldReturnAResponseThatIncludesAStatusCodeNotFoundField()
        {
            //Arrange
            DTOEducationalInstitutionParentUpdateCommand request = new()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                ParentInstitutionID = testDataHelper.EducationalInstitutions[1].EducationalInstitutionID
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(r => r.GetEntityByIDAsync(request.ParentInstitutionID, It.IsAny<CancellationToken>()))
                                                .ReturnsAsync(testDataHelper.EducationalInstitutions[1]);
            dependenciesHelper.mockRepository.Setup(r => r.UpdateParentInstitutionAsync(request.EducationalInstitutionID, testDataHelper.EducationalInstitutions[1], It.IsAny<CancellationToken>()))
                                                .ReturnsAsync(false);

            var handler = new UpdateEducationalInstitutionParentCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnInvalidRequestOfTypeDTOEducationalInstitutionParentUpdateCommand_WithEducationalInstitutionIDNotInDatabase_ShouldReturnAResponseThatIncludesAFalseOperationStatusField()
        {
            //Arrange
            DTOEducationalInstitutionParentUpdateCommand request = new()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                ParentInstitutionID = testDataHelper.EducationalInstitutions[1].EducationalInstitutionID
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(r => r.GetEntityByIDAsync(request.ParentInstitutionID, It.IsAny<CancellationToken>()))
                                                .ReturnsAsync(testDataHelper.EducationalInstitutions[1]);
            dependenciesHelper.mockRepository.Setup(r => r.UpdateParentInstitutionAsync(request.EducationalInstitutionID, testDataHelper.EducationalInstitutions[1], It.IsAny<CancellationToken>()))
                                                .ReturnsAsync(false);

            var handler = new UpdateEducationalInstitutionParentCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidRequestOfTypeDTOEducationalInstitutionParentUpdateCommand_WithEducationalInstitutionIDNotInDatabase_ShouldReturnAResponseThatIncludesAMessageField()
        {
            //Arrange
            DTOEducationalInstitutionParentUpdateCommand request = new()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                ParentInstitutionID = testDataHelper.EducationalInstitutions[1].EducationalInstitutionID
            };

            mockUnitOfWork.Setup(uok => uok.UsingEducationalInstitutionRepository()).Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(r => r.GetEntityByIDAsync(request.ParentInstitutionID, It.IsAny<CancellationToken>()))
                .ReturnsAsync(testDataHelper.EducationalInstitutions[1]);
            dependenciesHelper.mockRepository.Setup(r => r.UpdateParentInstitutionAsync(request.EducationalInstitutionID, testDataHelper.EducationalInstitutions[1], It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var handler = new UpdateEducationalInstitutionParentCommandHandler(mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal($"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!", result.Message);
        }
    }
}