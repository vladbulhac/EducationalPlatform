using EducationaInstitutionAPI.Business.Commands_Handlers.EducationalInstitution_Commands;
using EducationaInstitutionAPI.Data.Helpers.Queries_and_Commands_Results.Commands_Results;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Commands;
using EducationaInstitutionAPI.Utils;
using EducationalInstitution.API.Tests.UnitTests.BusinessTests.QueriesTests.EducationalInstitutionTests;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.Tests.UnitTests.BusinessTests.Commands_Handlers_Tests
{
    public class UpdateEducationalInstitutionEntireLocationCommandHandlerTests : IClassFixture<MockDependenciesHelper<UpdateEducationalInstitutionEntireLocationCommandHandler>>, IClassFixture<TestDataFromJSONParser>
    {
        private readonly MockDependenciesHelper<UpdateEducationalInstitutionEntireLocationCommandHandler> dependenciesHelper;
        private readonly TestDataFromJSONParser testDataHelper;

        public UpdateEducationalInstitutionEntireLocationCommandHandlerTests(MockDependenciesHelper<UpdateEducationalInstitutionEntireLocationCommandHandler> dependenciesHelper, TestDataFromJSONParser testDataHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
            this.testDataHelper = testDataHelper;
        }

        [Fact]
        public async Task GivenAnEduInstitutionID_LocationID_BuildingsIDs_ShouldReturnAResponseThatIncludesAStatusCodeOkField()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";
            ICollection<string> buildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };

            DTOEducationalInstitutionEntireLocationUpdateCommand request = new()
            {
                EduInstitutionID = eduInstitutionID,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs
            };
            dependenciesHelper.mockRepository.Setup(mr => mr.UpdateAsync(eduInstitutionID, locationID, buildingsIDs, dependenciesHelper.cancellationToken)).ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionEntireLocationCommandHandler(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task GivenAnEduInstitutionID_LocationID_BuildingsIDs_ShouldReturnAResponseThatIncludesAnEmptyMessageField()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";
            ICollection<string> buildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };

            DTOEducationalInstitutionEntireLocationUpdateCommand request = new()
            {
                EduInstitutionID = eduInstitutionID,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs
            };
            dependenciesHelper.mockRepository.Setup(mr => mr.UpdateAsync(eduInstitutionID, locationID, buildingsIDs, dependenciesHelper.cancellationToken)).ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionEntireLocationCommandHandler(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAnEduInstitutionID_LocationID_BuildingsIDs_ShouldReturnARecordTypeResponse()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";
            ICollection<string> buildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };

            DTOEducationalInstitutionEntireLocationUpdateCommand request = new()
            {
                EduInstitutionID = eduInstitutionID,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs
            };
            dependenciesHelper.mockRepository.Setup(mr => mr.UpdateAsync(eduInstitutionID, locationID, buildingsIDs, dependenciesHelper.cancellationToken)).ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionEntireLocationCommandHandler(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<Response<EducationalInstitutionCommandResult>>(result);
        }

        [Fact]
        public async Task GivenAnEduInstitutionID_LocationID_BuildingsIDs_ShouldReturnAResponseThatIncludesAResponseObjectWithTheEducationalInstitutionID()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";
            ICollection<string> buildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };

            DTOEducationalInstitutionEntireLocationUpdateCommand request = new()
            {
                EduInstitutionID = eduInstitutionID,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs
            };
            dependenciesHelper.mockRepository.Setup(mr => mr.UpdateAsync(eduInstitutionID, locationID, buildingsIDs, dependenciesHelper.cancellationToken)).ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionEntireLocationCommandHandler(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<Guid>(result.ResponseObject.EduInstitutionID);
        }

        [Fact]
        public async Task GivenAnEduInstitutionID_LocationID_BuildingsIDs_ShouldReturnAResponseThatIncludesAResponseObjectOfTypeCreateEducationalInstitutionCommandResult()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";
            ICollection<string> buildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };

            DTOEducationalInstitutionEntireLocationUpdateCommand request = new()
            {
                EduInstitutionID = eduInstitutionID,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs
            };
            dependenciesHelper.mockRepository.Setup(mr => mr.UpdateAsync(eduInstitutionID, locationID, buildingsIDs, dependenciesHelper.cancellationToken)).ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionEntireLocationCommandHandler(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.IsType<EducationalInstitutionCommandResult>(result.ResponseObject);
        }

        [Fact]
        public async Task GivenAnEduInstitutionID_LocationID_BuildingsIDs_ShouldReturnAResponseThatIncludesAnOperationStatusTrueField()
        {
            //Arrange
            Guid eduInstitutionID = testDataHelper.EduInstitutions[0].EduInstitutionID;
            string locationID = "10Fc4a7f1e00f1BDebAe4509";
            ICollection<string> buildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" };

            DTOEducationalInstitutionEntireLocationUpdateCommand request = new()
            {
                EduInstitutionID = eduInstitutionID,
                LocationID = locationID,
                BuildingsIDs = buildingsIDs
            };
            dependenciesHelper.mockRepository.Setup(mr => mr.UpdateAsync(eduInstitutionID, locationID, buildingsIDs, dependenciesHelper.cancellationToken)).ReturnsAsync(true);
            var handler = new UpdateEducationalInstitutionEntireLocationCommandHandler(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request, dependenciesHelper.cancellationToken);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            UpdateEducationalInstitutionEntireLocationCommandHandler handler = new(dependenciesHelper.mockRepository.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, dependenciesHelper.cancellationToken));
        }

        [Fact]
        public void GivenANullArgumentRepositoryToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionEntireLocationCommandHandler(null, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new UpdateEducationalInstitutionEntireLocationCommandHandler(dependenciesHelper.mockRepository.Object, null));
        }
    }
}