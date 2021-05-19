using EducationalInstitutionAPI.Business.Commands_Handlers;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Commands_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Utils;
using Moq;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.UnitTests.BusinessTests.Commands_Handlers_Tests
{
    public class DeleteEducationalInstitutionCommandHandlerTests : IClassFixture<MockDependenciesHelper<DeleteEducationalInstitutionCommandHandler>>,
                                                                   IClassFixture<TestDataFromJSONParser>
    {
        private readonly MockDependenciesHelper<DeleteEducationalInstitutionCommandHandler> dependenciesHelper;
        private readonly TestDataFromJSONParser testDataHelper;

        /// <remarks>Called before each test</remarks>
        public DeleteEducationalInstitutionCommandHandlerTests(MockDependenciesHelper<DeleteEducationalInstitutionCommandHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
        }

        #region One object contains the input id TESTS

        [Fact]
        public async Task GivenAnID_ShouldReturnAResponseThatIncludesAStatusCodeAcceptedField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            DTOEducationalInstitutionDeleteCommand request = new() { EducationalInstitutionID = educationalInstitutionID };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(muk => muk.UsingEducationalInstitutionCommandRepository())
                                                            .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockUnitOfWorkQuery.Setup(muk => muk.UsingEducationalInstitutionQueryRepository())
                                                            .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);

            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetEntityByIDAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                              .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

            DeleteEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                    dependenciesHelper.mockUnitOfWorkQuery.Object,
                                                                    dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.Accepted, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnID_ShouldReturnAResponseThatIncludesAnEmptyMessage()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            DTOEducationalInstitutionDeleteCommand request = new() { EducationalInstitutionID = educationalInstitutionID };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(muk => muk.UsingEducationalInstitutionCommandRepository())
                                                            .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockUnitOfWorkQuery.Setup(muk => muk.UsingEducationalInstitutionQueryRepository())
                                                            .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);

            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetEntityByIDAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                              .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

            DeleteEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                    dependenciesHelper.mockUnitOfWorkQuery.Object,
                                                                    dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnID_ShouldReturnARecordTypeResponse()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            DTOEducationalInstitutionDeleteCommand request = new() { EducationalInstitutionID = educationalInstitutionID };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(muk => muk.UsingEducationalInstitutionCommandRepository())
                                                            .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockUnitOfWorkQuery.Setup(muk => muk.UsingEducationalInstitutionQueryRepository())
                                                            .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);

            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetEntityByIDAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                              .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

            DeleteEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                    dependenciesHelper.mockUnitOfWorkQuery.Object,
                                                                    dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.IsType<Response<DeleteEducationalInstitutionCommandResult>>(result);
        }

        [Fact]
        public async Task GivenAnID_ShouldReturnAResponseThatIncludesDataOfType_DeleteEducationalInstitutionCommandResult()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            DTOEducationalInstitutionDeleteCommand request = new() { EducationalInstitutionID = educationalInstitutionID };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(muk => muk.UsingEducationalInstitutionCommandRepository())
                                                            .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockUnitOfWorkQuery.Setup(muk => muk.UsingEducationalInstitutionQueryRepository())
                                                            .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);

            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetEntityByIDAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                              .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

            DeleteEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                    dependenciesHelper.mockUnitOfWorkQuery.Object,
                                                                    dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.IsType<DeleteEducationalInstitutionCommandResult>(result.Data);
        }

        [Fact]
        public async Task GivenAnID_ShouldReturnAResponseThatIncludesDataWithA_DateForPermanentDeletionSetToADayInTheFutureField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            DTOEducationalInstitutionDeleteCommand request = new() { EducationalInstitutionID = educationalInstitutionID };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(muk => muk.UsingEducationalInstitutionCommandRepository())
                                                            .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockUnitOfWorkQuery.Setup(muk => muk.UsingEducationalInstitutionQueryRepository())
                                                            .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);

            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetEntityByIDAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                              .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

            DeleteEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                    dependenciesHelper.mockUnitOfWorkQuery.Object,
                                                                    dependenciesHelper.mockLogger.Object);

            var daysFromConfigFile = ConfigurationHelper.GetCurrentSettings("DaysUntilDeletion");
            int days = int.Parse(daysFromConfigFile);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(DateTime.UtcNow.Date.AddDays(days), result.Data.DateForPermanentDeletion);
        }

        [Fact]
        public async Task GivenAnID_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;
            DTOEducationalInstitutionDeleteCommand request = new() { EducationalInstitutionID = educationalInstitutionID };

            dependenciesHelper.mockUnitOfWorkCommand.Setup(muk => muk.UsingEducationalInstitutionCommandRepository())
                                                             .Returns(dependenciesHelper.mockEducationalInstitutionCommandRepository.Object);
            dependenciesHelper.mockUnitOfWorkQuery.Setup(muk => muk.UsingEducationalInstitutionQueryRepository())
                                                            .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);

            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(mr => mr.GetEntityByIDAsync(educationalInstitutionID, It.IsAny<CancellationToken>()))
                                              .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

            DeleteEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                    dependenciesHelper.mockUnitOfWorkQuery.Object,
                                                                    dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.True(result.OperationStatus);
        }

        #endregion One object contains the input id TESTS

        [Fact]
        public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            DeleteEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                        dependenciesHelper.mockUnitOfWorkQuery.Object,
                                                                        dependenciesHelper.mockLogger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null));
        }

        [Fact]
        public void GivenANullArgumentUnitOfWorkForCommandsToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new DeleteEducationalInstitutionCommandHandler(null,
                                                                                                    dependenciesHelper.mockUnitOfWorkQuery.Object,
                                                                                                    dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentUnitOfWorkForQueriesToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new DeleteEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                                                    null,
                                                                                                    dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new DeleteEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWorkCommand.Object,
                                                                                                        dependenciesHelper.mockUnitOfWorkQuery.Object,
                                                                                                        null));
        }
    }
}