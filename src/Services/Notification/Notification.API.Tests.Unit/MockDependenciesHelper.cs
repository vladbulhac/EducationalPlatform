using DataValidation.Abstractions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Notification.API.Hubs;
using Notification.Application.Services;
using Notification.Infrastructure.Repositories;

namespace Notification.API.Tests.Unit;

/// <summary>
/// Contains a collection of mocked types used in unit testing.
/// </summary>
public class MockDependenciesHelper<T> where T : class
{
    public readonly Mock<ILogger<T>> mockLogger;

    public Mock<INotificationHub> mockClientOne;
    public Mock<INotificationHub> mockClientTwo;
    public Mock<HubCallerContext> mockHubContext;
    public Mock<IHubCallerClients<INotificationHub>> mockHubClients;

    public Mock<IValidationHandler> mockValidationHandler;

    public readonly Mock<INotificationService> mockNotificationService;

    public readonly Mock<INotificationRepository> mockNotificationRepository;

    public MockDependenciesHelper()
    {
        mockLogger = new();

        mockClientOne = new();
        mockClientTwo = new();
        mockHubContext = new();
        mockHubClients = new();

        mockValidationHandler = new();

        mockNotificationService = new();

        mockNotificationRepository = new();
    }
}