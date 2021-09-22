using EducationalInstitution.Application;
using EducationalInstitution.Application.Queries;
using EducationalInstitution.Application.Queries.Handlers;
using EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.UnitTests.Application_Tests.Queries_Tests.Handlers_Tests
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
        public async Task GivenAValidGetAllAdminsByEducationalInstitutionIDQuery_ShouldReturnResponseOfGetAllAdminsOfEducationalInstitutionQueryResultType()
        {
            //Arrange
            GetAllAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<string>() { Guid.NewGuid().ToString() } };

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
        public async Task GivenAValidGetAllAdminsByEducationalInstitutionIDQuery_ShouldReturnTrueOperationStatus()
        {
            //Arrange
            GetAllAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<string>() { Guid.NewGuid().ToString() } };

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
        public async Task GivenAValidGetAllAdminsByEducationalInstitutionIDQuery_ShouldReturnEmptyMessage()
        {
            //Arrange
            GetAllAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<string>() { Guid.NewGuid().ToString() } };

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
        public async Task GivenAValidGetAllAdminsByEducationalInstitutionIDQuery_ShouldReturnStatusCodeOK()
        {
            //Arrange
            GetAllAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<string>() { Guid.NewGuid().ToString() } };

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
        public async Task GivenAValidGetAllAdminsByEducationalInstitutionIDQuery_ShouldReturnExpectedData()
        {
            //Arrange
            GetAllAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<string>() { Guid.NewGuid().ToString() } };

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
        public async Task GivenAValidGetAllAdminsByEducationalInstitutionIDQuery_WithIDThatDoesntExistInDatabase_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            GetAllAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult() { AdminsIDs = new List<string>() };

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
        public async Task GivenAValidGetAllAdminsByEducationalInstitutionIDQuery_WithIDThatDoesntExistInDatabase_ShouldReturnStatusCodeNotFound()
        {
            //Arrange
            GetAllAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<string>() };

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
        public async Task GivenAValidGetAllAdminsByEducationalInstitutionIDQuery_WithIDThatDoesntExistInDatabase_ShouldReturnNullData()
        {
            //Arrange
            GetAllAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<string>() };

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
        public async Task GivenAValidGetAllAdminsByEducationalInstitutionIDQuery_WithIDThatDoesntExistInDatabase_ShouldReturnExpectedMessage()
        {
            //Arrange
            GetAllAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<string>() };

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
        public async Task GivenAValidGetAllAdminsByEducationalInstitutionIDQuery_AnExceptionIsCaught_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            GetAllAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<string>() };

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
        public async Task GivenAValidGetAllAdminsByEducationalInstitutionIDQuery_AnExceptionIsCaught_ShouldReturnStatusCodeInternalServerError()
        {
            //Arrange
            GetAllAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<string>() };

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
        public async Task GivenAValidGetAllAdminsByEducationalInstitutionIDQuery_AnExceptionIsCaught_ShouldReturnNullData()
        {
            //Arrange
            GetAllAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<string>() };

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
        public async Task GivenAValidGetAllAdminsByEducationalInstitutionIDQuery_AnExceptionIsCaught_ShouldReturnExpectedMessage()
        {
            //Arrange
            GetAllAdminsByEducationalInstitutionIDQuery request = new() { EducationalInstitutionID = Guid.NewGuid() };

            var expectedQueryResult = new GetAllAdminsOfEducationalInstitutionQueryResult()
            { AdminsIDs = new List<string>() };

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