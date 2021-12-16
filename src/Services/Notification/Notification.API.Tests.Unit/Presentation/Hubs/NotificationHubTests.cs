using Notification.API.Hubs;
using Notification.API.Tests.Shared;
using Notification.Application.DTOs;

namespace Notification.API.Tests.Unit.Presentation.Hubs;

public class NotificationHubTests : IClassFixture<MockDependenciesHelper<NotificationHub>>, IClassFixture<JSONDataParser>
{
    private JSONDataParser testDataHelper;
    private MockDependenciesHelper<NotificationHub> dependenciesHelper;

    public NotificationHubTests(MockDependenciesHelper<NotificationHub> dependenciesHelper, JSONDataParser testDataHelper)
    {
        this.testDataHelper = testDataHelper;
        this.dependenciesHelper = dependenciesHelper;
    }

    [Fact]
    public async Task GivenARecipientAndACollectionOfNotifications_ToSendNotificationsMethod_ShouldCallReceiveNotificationsOfRecipientOnce()
    {
        //Arrange
        var @event = testDataHelper.Events[0];
        var to = @event.Recipients.First().Id;
        var notifications = new NotificationBody[1] { new(@event, to) };

        SetupMockedClients(to);

        var hub = new NotificationHub(dependenciesHelper.mockNotificationService.Object, dependenciesHelper.mockLogger.Object);
        hub.Clients = dependenciesHelper.mockHubClients.Object;

        //Act
        await hub.SendNotifications(to, notifications);

        //Assert
        dependenciesHelper.mockClientOne.Verify(mco => mco.ReceiveNotifications(notifications), Times.Once);
    }

    [Fact]
    public async Task GivenARecipientAndACollectionOfNotifications_ToSendNotificationsMethod_ShouldNotCallReceiveNotificationsOfOtherRecipients()
    {
        //Arrange
        var @event = testDataHelper.Events[0];
        var to = @event.Recipients.First().Id;
        var notifications = new NotificationBody[1] { new(@event, to) };

        SetupMockedClients(to);

        var hub = new NotificationHub(dependenciesHelper.mockNotificationService.Object, dependenciesHelper.mockLogger.Object);
        hub.Clients = dependenciesHelper.mockHubClients.Object;

        //Act
        await hub.SendNotifications(to, notifications);

        //Assert
        dependenciesHelper.mockClientTwo.Verify(mco => mco.ReceiveNotifications(notifications), Times.Never);
    }

    private void SetupMockedClients(string testUserId)
    {
        dependenciesHelper.mockClientOne.Setup(mco => mco.ReceiveNotifications(It.IsAny<ICollection<NotificationBody>>()))
                                        .Verifiable();
        dependenciesHelper.mockClientTwo.Setup(mct => mct.ReceiveNotifications(It.IsAny<ICollection<NotificationBody>>()))
                                        .Verifiable();

        dependenciesHelper.mockHubClients.Setup(mc => mc.User(testUserId))
                                                   .Returns(dependenciesHelper.mockClientOne.Object);
        dependenciesHelper.mockHubClients.Setup(mc => mc.User("user-two"))
                                                   .Returns(dependenciesHelper.mockClientTwo.Object);
    }
}