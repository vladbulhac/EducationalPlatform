using EducationaInstitutionAPI.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;

namespace EducationalInstitution.API.Tests.UnitTests.BusinessTests.QueriesTests.EducationalInstitutionTests
{
    public class MockDependenciesHelper<T>
    {
        public readonly Mock<ILogger<T>> mockLogger;
        public readonly Mock<IEducationalInstitutionRepository> mockRepository;
        public readonly CancellationToken cancellationToken;
        public readonly T requestHandler;

        public MockDependenciesHelper()
        {
            mockLogger = new();
            mockRepository = new();
            cancellationToken = new();
            requestHandler = default;
        }
    }
}