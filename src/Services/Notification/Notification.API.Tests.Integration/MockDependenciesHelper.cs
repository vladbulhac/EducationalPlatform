using Microsoft.Extensions.Logging;

namespace Notification.API.Tests.Integration;

/// <summary>
/// Contains a collection of mocked types used in unit testing.
/// </summary>
public class MockDependenciesHelper<T> where T : class
{
    public readonly Mock<ILogger<T>> mockLogger;

    public MockDependenciesHelper()
    {
        mockLogger = new();
    }
}