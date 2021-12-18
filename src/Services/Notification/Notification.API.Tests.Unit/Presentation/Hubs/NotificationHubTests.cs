using Notification.API.Hubs;
using Notification.API.Hubs.DataTransferObjects;
using Notification.API.Tests.Shared;
using Notification.Application.DTOs;

namespace Notification.API.Tests.Unit.Presentation.Hubs;

public class NotificationHubTests : IClassFixture<MockDependenciesHelper<NotificationHub>>, IClassFixture<JSONDataParser>
{
    private JSONDataParser testDataHelper;
    private MockDependenciesHelper<NotificationHub> dependenciesHelper;

    private NotificationHub hub;

    public NotificationHubTests(MockDependenciesHelper<NotificationHub> dependenciesHelper, JSONDataParser testDataHelper)
    {
        this.testDataHelper = testDataHelper;
        this.dependenciesHelper = dependenciesHelper;

        hub = new(dependenciesHelper.mockNotificationService.Object, dependenciesHelper.mockLogger.Object);
        hub.Clients = dependenciesHelper.mockHubClients.Object;
        hub.Context = dependenciesHelper.mockHubContext.Object;
    }

    [Fact]
    public async Task GivenAGetNotificationsOfRecipientDTO_ToGetNotificationsMethod_ShouldCallReceiveNotificationsOfRecipientOnce()
    {
        //Arrange
        var dto = new GetNotificationsOfRecipientDTO() { Offset = 0, ResultsCount = 10 };
        var @event = testDataHelper.Events[0];
        var to = @event.Recipients.First().Id;
        var notifications = new NotificationBody[1] { new(@event, to) };

        SetupMockedClients(to);
        dependenciesHelper.mockNotificationService.Setup(mns => mns.GetNotificationsAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                                                  .ReturnsAsync(notifications);
        //Act
        await hub.GetNotifications(dto);

        //Assert
        dependenciesHelper.mockClientOne.Verify(mco => mco.ReceiveNotifications(notifications), Times.Once);
    }

    [Fact]
    public async Task GivenAGetNotificationsOfRecipientDTO_ToGetNotificationsMethod_ShouldNotCallReceiveNotificationsOfOtherRecipients()
    {
        //Arrange
        var dto = new GetNotificationsOfRecipientDTO() { Offset = 0, ResultsCount = 10 };
        var @event = testDataHelper.Events[0];
        var to = @event.Recipients.First().Id;
        var notifications = new NotificationBody[1] { new(@event, to) };

        SetupMockedClients(to);
        dependenciesHelper.mockNotificationService.Setup(mns => mns.GetNotificationsAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                                                  .ReturnsAsync(notifications);
        //Act
        await hub.GetNotifications(dto);

        //Assert
        dependenciesHelper.mockClientTwo.Verify(mct => mct.ReceiveNotifications(notifications), Times.Never);
    }

    private void SetupMockedClients(string testUserId = "user-one")
    {
        dependenciesHelper.mockClientOne.Setup(mco => mco.ReceiveNotifications(It.IsAny<ICollection<NotificationBody>>()))
                                        .Verifiable();
        dependenciesHelper.mockClientTwo.Setup(mct => mct.ReceiveNotifications(It.IsAny<ICollection<NotificationBody>>()))
                                        .Verifiable();

        dependenciesHelper.mockHubClients.Setup(mc => mc.User(testUserId))
                                                   .Returns(dependenciesHelper.mockClientOne.Object);
        dependenciesHelper.mockHubClients.Setup(mc => mc.User("user-two"))
                                                   .Returns(dependenciesHelper.mockClientTwo.Object);

        dependenciesHelper.mockHubContext.SetupGet(mhc => mhc.UserIdentifier)
                                         .Returns(testUserId);
        dependenciesHelper.mockHubContext.SetupGet(mhc => mhc.ConnectionId)
                                         .Returns("mockConnectionId");

        dependenciesHelper.mockHubClients.SetupGet(mhc => mhc.Caller)
                                         .Returns(dependenciesHelper.mockHubClients.Object.User(testUserId));
    }
}