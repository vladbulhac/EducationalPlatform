using EducationalInstitutionAPI.Business.Commands_Handlers;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Commands_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Utils;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.Tests.UnitTests.BusinessTests.Commands_Handlers_Tests
{
    public class DeleteEducationalInstitutionCommandHandlerTests : IClassFixture<MockDependenciesHelper<DeleteEducationalInstitutionCommandHandler>>, IClassFixture<TestDataFromJSONParser>
    {
        private readonly MockDependenciesHelper<DeleteEducationalInstitutionCommandHandler> dependenciesHelper;
        private readonly TestDataFromJSONParser testDataHelper;

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

            dependenciesHelper.mockUnitOfWork.Setup(muk => muk.UsingEducationalInstitutionRepository())
                                                            .Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(educationalInstitutionID, dependenciesHelper.cancellationToken))
                                              .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

            DeleteEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(HttpStatusCode.Accepted, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnID_ShouldReturnAResponseThatIncludesAnEmptyMessage()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            DTOEducationalInstitutionDeleteCommand request = new() { EducationalInstitutionID = educationalInstitutionID };

            dependenciesHelper.mockUnitOfWork.Setup(muk => muk.UsingEducationalInstitutionRepository())
                                                            .Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(educationalInstitutionID, dependenciesHelper.cancellationToken))
                                              .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

            DeleteEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnID_ShouldReturnARecordTypeResponse()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            DTOEducationalInstitutionDeleteCommand request = new() { EducationalInstitutionID = educationalInstitutionID };

            dependenciesHelper.mockUnitOfWork.Setup(muk => muk.UsingEducationalInstitutionRepository())
                                                            .Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(educationalInstitutionID, dependenciesHelper.cancellationToken))
                                              .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

            DeleteEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<Response<DeleteEducationalInstitutionCommandResult>>(result);
        }

        [Fact]
        public async Task GivenAnID_ShouldReturnAResponseThatIncludesAResponseObjectOfType_DeleteEducationalInstitutionCommandResult()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            DTOEducationalInstitutionDeleteCommand request = new() { EducationalInstitutionID = educationalInstitutionID };

            dependenciesHelper.mockUnitOfWork.Setup(muk => muk.UsingEducationalInstitutionRepository())
                                                            .Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(educationalInstitutionID, dependenciesHelper.cancellationToken))
                                              .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

            DeleteEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<DeleteEducationalInstitutionCommandResult>(result.Data);
        }

        [Fact]
        public async Task GivenAnID_ShouldReturnAResponseThatIncludesAResponseObjectWithA_DateForPermanentDeletionSetToADayInTheFutureField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            DTOEducationalInstitutionDeleteCommand request = new() { EducationalInstitutionID = educationalInstitutionID };

            dependenciesHelper.mockUnitOfWork.Setup(muk => muk.UsingEducationalInstitutionRepository())
                                                            .Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(educationalInstitutionID, dependenciesHelper.cancellationToken))
                                              .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

            DeleteEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            var daysFromConfigFile = ConfigurationHelper.GetCurrentSettings("DaysUntilDeletion");
            int days = int.Parse(daysFromConfigFile);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(DateTime.UtcNow.Date.AddDays(days), result.Data.DateForPermanentDeletion);
        }

        [Fact]
        public async Task GivenAnID_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            Guid educationalInstitutionID = testDataHelper.EducationalInstitutions[0].EducationalInstitutionID;

            DTOEducationalInstitutionDeleteCommand request = new() { EducationalInstitutionID = educationalInstitutionID };

            dependenciesHelper.mockUnitOfWork.Setup(muk => muk.UsingEducationalInstitutionRepository())
                                                            .Returns(dependenciesHelper.mockRepository.Object);
            dependenciesHelper.mockRepository.Setup(mr => mr.GetEntityByIDAsync(educationalInstitutionID, dependenciesHelper.cancellationToken))
                                              .ReturnsAsync(testDataHelper.EducationalInstitutions[0]);

            DeleteEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.True(result.OperationStatus);
        }

        #endregion One object contains the input id TESTS

        [Fact]
        public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            DeleteEducationalInstitutionCommandHandler handler = new(dependenciesHelper.mockUnitOfWork.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, dependenciesHelper.cancellationToken));
        }

        [Fact]
        public void GivenANullArgumentRepositoryToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new DeleteEducationalInstitutionCommandHandler(null, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new DeleteEducationalInstitutionCommandHandler(dependenciesHelper.mockUnitOfWork.Object, null));
        }
    }
}