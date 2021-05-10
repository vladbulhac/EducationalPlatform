using EducationalInstitutionAPI.Business.Validation_Handler;
using EducationalInstitutionAPI.Repositories.EducationalInstitution_Repository.Command_Repository;
using EducationalInstitutionAPI.Repositories.EducationalInstitution_Repository.Query_Repository;
using EducationalInstitutionAPI.Repositories.EducationalInstitutionAdmin_Repository.Command_Repository;
using EducationalInstitutionAPI.Repositories.EducationalInstitutionAdmin_Repository.Query_Repostiory;
using EducationalInstitutionAPI.Repositories.EducationalInstitutionBuilding_Repository.Command_Repository;
using EducationalInstitutionAPI.Repositories.EducationalInstitutionBuilding_Repository.Query_Repository;
using EducationalInstitutionAPI.Unit_of_Work.Command_Unit_of_Work;
using EducationalInstitutionAPI.Unit_of_Work.Query_Unit_of_Work;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;

namespace EducationalInstitution.API.UnitTests
{
    /// <summary>
    /// Contains a collection of mocked types used in unit testing
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MockDependenciesHelper<T>
    {
        public readonly Mock<IEducationalInstitutionCommandRepository> mockEducationalInstitutionCommandRepository;
        public readonly Mock<IEducationalInstitutionQueryRepository> mockEducationalInstitutionQueryRepository;
        public readonly Mock<IEducationalInstitutionBuildingCommandRepository> mockBuildingCommandRepository;
        public readonly Mock<IEducationalInstitutionBuildingQueryRepository> mockBuildingQueryRepository;
        public readonly Mock<IEducationalInstitutionAdminCommandRepository> mockAdminCommandRepository;
        public readonly Mock<IEducationalInstitutionAdminQueryRepository> mockAdminQueryRepository;

        public readonly Mock<IUnitOfWorkForCommands> mockUnitOfWorkCommand;
        public readonly Mock<IUnitOfWorkForQueries> mockUnitOfWorkQuery;

        public readonly Mock<ILogger<T>> mockLogger;
        public readonly Mock<IMediator> mockMediator;
        public readonly Mock<IValidationHandler> mockValidationHandler;
        public readonly Mock<ServerCallContext> mockServerCallContext;

        public MockDependenciesHelper()
        {
            mockEducationalInstitutionCommandRepository = new();
            mockEducationalInstitutionQueryRepository = new();
            mockBuildingCommandRepository = new();
            mockBuildingQueryRepository = new();
            mockAdminCommandRepository = new();
            mockAdminQueryRepository = new();

            mockUnitOfWorkCommand = new();
            mockUnitOfWorkQuery = new();

            mockLogger = new();
            mockMediator = new();
            mockValidationHandler = new();
            mockServerCallContext = new();
        }
    }
}