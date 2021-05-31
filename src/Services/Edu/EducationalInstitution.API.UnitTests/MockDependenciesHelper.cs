using EducationalInstitutionAPI.Business.Validation_Handler;
using EducationalInstitutionAPI.Repositories.Command_Repository;
using EducationalInstitutionAPI.Repositories.Query_Repository;
using EducationalInstitutionAPI.Unit_of_Work.Command_Unit_of_Work;
using EducationalInstitutionAPI.Unit_of_Work.Query_Unit_of_Work;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using RabbitMQEventBus.Abstractions;

namespace EducationalInstitution.API.UnitTests
{
    /// <summary>
    /// Contains a collection of mocked types used in unit testing
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MockDependenciesHelper<T> where T : class
    {
        public readonly Mock<IEducationalInstitutionCommandRepository> mockEducationalInstitutionCommandRepository;
        public readonly Mock<IEducationalInstitutionQueryRepository> mockEducationalInstitutionQueryRepository;

        public readonly Mock<IUnitOfWorkForCommands> mockUnitOfWorkCommand;
        public readonly Mock<IUnitOfWorkForQueries> mockUnitOfWorkQuery;

        public readonly Mock<IEventBus> mockEventBus;

        public readonly Mock<ILogger<T>> mockLogger;
        public readonly Mock<IMediator> mockMediator;
        public readonly Mock<IValidationHandler> mockValidationHandler;
        public readonly Mock<ServerCallContext> mockServerCallContext;

        public MockDependenciesHelper()
        {
            mockEducationalInstitutionCommandRepository = new();
            mockEducationalInstitutionQueryRepository = new();

            mockUnitOfWorkCommand = new();
            mockUnitOfWorkQuery = new();

            mockEventBus = new();

            mockLogger = new();
            mockMediator = new();
            mockValidationHandler = new();
            mockServerCallContext = new();
        }
    }
}