using EducationalInstitutionAPI.Business.Queries_Handlers;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Queries;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.UnitTests.BusinessTests.Queries_Handlers_Tests
{
    public class GetAllAdminsByEducationalInstitutionIDQueryHandlerTests : IClassFixture<MockDependenciesHelper<GetAllAdminsByEducationalInstitutionIDQueryHandler>>
    {
        private readonly MockDependenciesHelper<GetAllAdminsByEducationalInstitutionIDQueryHandler> dependenciesHelper;

        /// <remarks>Called before each test</remarks>
        public GetAllAdminsByEducationalInstitutionIDQueryHandlerTests(MockDependenciesHelper<GetAllAdminsByEducationalInstitutionIDQueryHandler> dependenciesHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
        }

        [Fact]
        public async Task GivenAValidDTOAdminsByEducationalInstitutionIDQuery_ShouldReturnResponseOfGetAllAdminsOfEducationalInstitutionQueryResultType()
        {
            //Arrange
            DTOAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<Guid>() { Guid.NewGuid() } };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uokq => uokq.UsingEducationalInstitutionQueryRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(eiqr => eiqr.GetAllAdminsForEducationalInstitutionAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                        .ReturnsAsync(expectedQueryResult);

            var handler = new GetAllAdminsByEducationalInstitutionIDQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.IsType<Response<GetAllAdminsOfEducationalInstitutionQueryResult>>(result);
        }

        [Fact]
        public async Task GivenAValidDTOAdminsByEducationalInstitutionIDQuery_ShouldReturnTrueOperationStatus()
        {
            //Arrange
            DTOAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<Guid>() { Guid.NewGuid() } };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uokq => uokq.UsingEducationalInstitutionQueryRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(eiqr => eiqr.GetAllAdminsForEducationalInstitutionAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                        .ReturnsAsync(expectedQueryResult);

            var handler = new GetAllAdminsByEducationalInstitutionIDQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.True(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidDTOAdminsByEducationalInstitutionIDQuery_ShouldReturnEmptyMessage()
        {
            //Arrange
            DTOAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<Guid>() { Guid.NewGuid() } };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uokq => uokq.UsingEducationalInstitutionQueryRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(eiqr => eiqr.GetAllAdminsForEducationalInstitutionAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                        .ReturnsAsync(expectedQueryResult);

            var handler = new GetAllAdminsByEducationalInstitutionIDQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Empty(result.Message);
        }

        [Fact]
        public async Task GivenAValidDTOAdminsByEducationalInstitutionIDQuery_ShouldReturnStatusCodeOK()
        {
            //Arrange
            DTOAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<Guid>() { Guid.NewGuid() } };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uokq => uokq.UsingEducationalInstitutionQueryRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(eiqr => eiqr.GetAllAdminsForEducationalInstitutionAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                        .ReturnsAsync(expectedQueryResult);

            var handler = new GetAllAdminsByEducationalInstitutionIDQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidDTOAdminsByEducationalInstitutionIDQuery_ShouldReturnExpectedData()
        {
            //Arrange
            DTOAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<Guid>() { Guid.NewGuid() } };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uokq => uokq.UsingEducationalInstitutionQueryRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(eiqr => eiqr.GetAllAdminsForEducationalInstitutionAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                        .ReturnsAsync(expectedQueryResult);

            var handler = new GetAllAdminsByEducationalInstitutionIDQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(expectedQueryResult, result.Data);
        }

        [Fact]
        public async Task GivenAValidDTOAdminsByEducationalInstitutionIDQuery_WithIDThatDoesntExistInDatabase_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            DTOAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<Guid>() };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uokq => uokq.UsingEducationalInstitutionQueryRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(eiqr => eiqr.GetAllAdminsForEducationalInstitutionAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                        .ReturnsAsync(expectedQueryResult);

            var handler = new GetAllAdminsByEducationalInstitutionIDQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidDTOAdminsByEducationalInstitutionIDQuery_WithIDThatDoesntExistInDatabase_ShouldReturnStatusCodeNotFound()
        {
            //Arrange
            DTOAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<Guid>() };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uokq => uokq.UsingEducationalInstitutionQueryRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(eiqr => eiqr.GetAllAdminsForEducationalInstitutionAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                        .ReturnsAsync(expectedQueryResult);

            var handler = new GetAllAdminsByEducationalInstitutionIDQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidDTOAdminsByEducationalInstitutionIDQuery_WithIDThatDoesntExistInDatabase_ShouldReturnNullData()
        {
            //Arrange
            DTOAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<Guid>() };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uokq => uokq.UsingEducationalInstitutionQueryRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(eiqr => eiqr.GetAllAdminsForEducationalInstitutionAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                        .ReturnsAsync(expectedQueryResult);

            var handler = new GetAllAdminsByEducationalInstitutionIDQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GivenAValidDTOAdminsByEducationalInstitutionIDQuery_WithIDThatDoesntExistInDatabase_ShouldReturnExpectedMessage()
        {
            //Arrange
            DTOAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<Guid>() };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uokq => uokq.UsingEducationalInstitutionQueryRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(eiqr => eiqr.GetAllAdminsForEducationalInstitutionAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                        .ReturnsAsync(expectedQueryResult);

            var handler = new GetAllAdminsByEducationalInstitutionIDQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal($"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!", result.Message);
        }

        [Fact]
        public async Task GivenAValidDTOAdminsByEducationalInstitutionIDQuery_AnExceptionIsCaught_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            DTOAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<Guid>() };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uokq => uokq.UsingEducationalInstitutionQueryRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(eiqr => eiqr.GetAllAdminsForEducationalInstitutionAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                        .ThrowsAsync(new Exception());

            var handler = new GetAllAdminsByEducationalInstitutionIDQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public async Task GivenAValidDTOAdminsByEducationalInstitutionIDQuery_AnExceptionIsCaught_ShouldReturnStatusCodeInternalServerError()
        {
            //Arrange
            DTOAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<Guid>() };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uokq => uokq.UsingEducationalInstitutionQueryRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(eiqr => eiqr.GetAllAdminsForEducationalInstitutionAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                        .ThrowsAsync(new Exception());

            var handler = new GetAllAdminsByEducationalInstitutionIDQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task GivenAValidDTOAdminsByEducationalInstitutionIDQuery_AnExceptionIsCaught_ShouldReturnNullData()
        {
            //Arrange
            DTOAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<Guid>() };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uokq => uokq.UsingEducationalInstitutionQueryRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(eiqr => eiqr.GetAllAdminsForEducationalInstitutionAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                        .ThrowsAsync(new Exception());

            var handler = new GetAllAdminsByEducationalInstitutionIDQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GivenAValidDTOAdminsByEducationalInstitutionIDQuery_AnExceptionIsCaught_ShouldReturnExpectedMessage()
        {
            //Arrange
            DTOAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<Guid>() };

            dependenciesHelper.mockUnitOfWorkQuery.Setup(uokq => uokq.UsingEducationalInstitutionQueryRepository())
                                                   .Returns(dependenciesHelper.mockEducationalInstitutionQueryRepository.Object);
            dependenciesHelper.mockEducationalInstitutionQueryRepository.Setup(eiqr => eiqr.GetAllAdminsForEducationalInstitutionAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                                                        .ThrowsAsync(new Exception());

            var handler = new GetAllAdminsByEducationalInstitutionIDQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Act
            var result = await handler.Handle(request);

            //Assert
            Assert.Equal($"An error occurred while searching for the admins of Educational Institution with the following ID: {request.EducationalInstitutionID}!", result.Message);
        }

        [Fact]
        public async Task GivenANullArgumentRequestToTheRequestHandlerHandleMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            GetAllAdminsByEducationalInstitutionIDQueryHandler handler = new(dependenciesHelper.mockUnitOfWorkQuery.Object, dependenciesHelper.mockLogger.Object);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null));
        }

        [Fact]
        public void GivenANullArgumentRepositoryToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new GetAllAdminsByEducationalInstitutionIDQueryHandler(null, dependenciesHelper.mockLogger.Object));
        }

        [Fact]
        public void GivenANullArgumentLoggerToTheRequestHandlerConstructor_ShouldThrowArgumentNullException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new GetAllAdminsByEducationalInstitutionIDQueryHandler(dependenciesHelper.mockUnitOfWorkQuery.Object, null));
        }
    }
}