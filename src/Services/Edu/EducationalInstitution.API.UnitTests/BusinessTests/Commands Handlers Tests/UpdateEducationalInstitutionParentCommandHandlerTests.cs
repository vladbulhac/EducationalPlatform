using EducationalInstitutionAPI.Business.Commands_Handlers;
using EducationalInstitutionAPI.Data.Repositories_results;
using EducationalInstitutionAPI.DTOs.Commands;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.UnitTests.BusinessTests.Commands_Handlers_Tests
{
    public class UpdateEducationalInstitutionParentCommandHandlerTests : IClassFixture<MockDependenciesHelper<UpdateEducationalInstitutionParentCommandHandler>>,
                                                                         IClassFixture<TestDataFromJSONParser>
    {
        private readonly MockDependenciesHelper<UpdateEducationalInstitutionParentCommandHandler> dependenciesHelper;
        private readonly TestDataFromJSONParser testDataHelper;

        /// <remarks>Called before each test</remarks>
        public UpdateEducationalInstitutionParentCommandHandlerTests(MockDependenciesHelper<UpdateEducationalInstitutionParentCommandHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
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

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(r => r.UpdateParentInstitutionAsync(request.EducationalInstitutionID, testDataHelper.EducationalInstitutions[1].EducationalInstitutionID, It.IsAny<CancellationToken>()))
                                                .ReturnsAsync(new CommandRepositoryResult(new List<Guid>() { Guid.NewGuid() }));

            var handler = new UpdateEducationalInstitutionParentCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                                dependenciesHelper.mockEventBus.Object,
                                                                                dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

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

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(r => r.UpdateParentInstitutionAsync(request.EducationalInstitutionID, testDataHelper.EducationalInstitutions[1].EducationalInstitutionID, It.IsAny<CancellationToken>()))
                                                .ReturnsAsync(new CommandRepositoryResult(new List<Guid>() { Guid.NewGuid() }));

            var handler = new UpdateEducationalInstitutionParentCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                                dependenciesHelper.mockEventBus.Object,
                                                                                dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

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

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                     .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(r => r.UpdateParentInstitutionAsync(request.EducationalInstitutionID, testDataHelper.EducationalInstitutions[1].EducationalInstitutionID, It.IsAny<CancellationToken>()))
                                                .ReturnsAsync(new CommandRepositoryResult(new List<Guid>() { Guid.NewGuid() }));

            var handler = new UpdateEducationalInstitutionParentCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                                dependenciesHelper.mockEventBus.Object,
                                                                                dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAnInvalidRequestOfTypeDTOEducationalInstitutionParentUpdateCommand_WithEducationalInstitutionOrParentIDNotInDatabase_ShouldReturnAResponseThatIncludesAStatusCodeNotFoundField()
        {
            //Arrange
            DTOEducationalInstitutionParentUpdateCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID,
                ParentInstitutionID = Guid.NewGuid()
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                    .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(r => r.UpdateParentInstitutionAsync(request.EducationalInstitutionID, testDataHelper.EducationalInstitutions[1].EducationalInstitutionID, It.IsAny<CancellationToken>()))
                                                           .ReturnsAsync((CommandRepositoryResult)default);
            var handler = new UpdateEducationalInstitutionParentCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                                dependenciesHelper.mockEventBus.Object,
                                                                                dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnInvalidRequestOfTypeDTOEducationalInstitutionParentUpdateCommand_WithEducationalInstitutionOrParentIDNotInDatabase_ShouldReturnAResponseThatIncludesAMessageField()
        {
            //Arrange
            DTOEducationalInstitutionParentUpdateCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID,
                ParentInstitutionID = Guid.NewGuid()
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(r => r.UpdateParentInstitutionAsync(request.EducationalInstitutionID, testDataHelper.EducationalInstitutions[1].EducationalInstitutionID, It.IsAny<CancellationToken>()))
                                                           .ReturnsAsync((CommandRepositoryResult)default);
            var handler = new UpdateEducationalInstitutionParentCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                                dependenciesHelper.mockEventBus.Object,
                                                                                dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal($"The Educational Institution with the ID: {request.EducationalInstitutionID} or the Parent Educational Institution with the ID: {request.ParentInstitutionID} has not been found!", result.Message);
        }

        [Fact]
        public async Task GivenAnInvalidRequestOfTypeDTOEducationalInstitutionParentUpdateCommand_WithEducationalInstitutionOrParentIDNotInDatabase_ShouldReturnAResponseThatIncludesAFalseOperationStatusField()
        {
            //Arrange
            DTOEducationalInstitutionParentUpdateCommand request = new()
            {
                EducationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID,
                ParentInstitutionID = Guid.NewGuid()
            };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(uok => uok.UsingEducationalInstitutionCommandRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);

            dependenciesHelper.mockEducationalInstitutionCommandRepository.Setup(r => r.UpdateParentInstitutionAsync(request.EducationalInstitutionID, testDataHelper.EducationalInstitutions[1].EducationalInstitutionID, It.IsAny<CancellationToken>()))
                                                           .ReturnsAsync((CommandRepositoryResult)default);
            var handler = new UpdateEducationalInstitutionParentCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                                dependenciesHelper.mockEventBus.Object,
                                                                                dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.False(result.OperationStatus);
        }
    }
}