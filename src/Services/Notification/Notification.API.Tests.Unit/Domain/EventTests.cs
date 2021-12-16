using Notification.Domain.Models.Aggregates;

namespace Notification.API.Tests.Unit.Domain;

public class EventTests
{
    [Fact]
    public void GivenNoId_ToContructor_ShouldGenerateOne()
    {
        //Act
        var @event = new Event("test_nameIntegrationEvent",
                                "test_message",
                                "test_uri",
                                DateTime.UtcNow,
                                "test_action",
                                "test_issuer",
                                "test_recipient");

        //Assert
        Assert.NotNull(@event.Id);
    }

    [Fact]
    public void GivenAnId_ToContructor_ShouldReturnTheGivenOne()
    {
        //Arrange
        var id = "test_id";

        //Act
        var @event = new Event("test_nameIntegrationEvent",
                                "test_message",
                                "test_uri",
                                DateTime.UtcNow,
                                "test_action",
                                "test_issuer",
                                "test_recipient",
                                id);

        //Assert
        Assert.Equal(id, @event.Id);
    }

    [Fact]
    public void GivenOneRecipient_ToContructor_ShouldHaveOnlyOneRecipientInCollection()
    {
        //Arrange
        var recipient = "test_recipient";

        //Act
        var @event = new Event("test_nameIntegrationEvent",
                                "test_message",
                                "test_uri",
                                DateTime.UtcNow,
                                "test_action",
                                "test_issuer",
                                recipient);

        //Assert
        Assert.Single(@event.Recipients);
    }

    [Fact]
    public void GivenACollectionOfRecipients_ToContructor_ShouldHaveTheSameNumberOfElementsInRecipientsCollection()
    {
        //Arrange
        var recipients = new string[2] { "test_recipient_1", "test_recipient_2" };

        //Act
        var @event = new Event("test_nameIntegrationEvent",
                                "test_message",
                                "test_uri",
                                DateTime.UtcNow,
                                "test_action",
                                "test_issuer",
                                recipients);

        //Assert
        Assert.Equal(recipients.Length, @event.Recipients.Count);
    }

    [Fact]
    public void GivenACollectionOfRecipients_ToContructor_ShouldContainTheFirstRecipientId()
    {
        //Arrange
        var recipients = new string[2] { "test_recipient_1", "test_recipient_2" };

        //Act
        var @event = new Event("test_nameIntegrationEvent",
                                "test_message",
                                "test_uri",
                                DateTime.UtcNow,
                                "test_action",
                                "test_issuer",
                                recipients);

        //Assert
        Assert.Contains(@event.Recipients, r => r.Id == recipients[0]);
    }

    [Fact]
    public void GivenACollectionOfRecipients_ToContructor_ShouldContainTheSecondRecipientId()
    {
        //Arrange
        var recipients = new string[2] { "test_recipient_1", "test_recipient_2" };

        //Act
        var @event = new Event("test_nameIntegrationEvent",
                                "test_message",
                                "test_uri",
                                DateTime.UtcNow,
                                "test_action",
                                "test_issuer",
                                recipients);

        //Assert
        Assert.Contains(@event.Recipients, r => r.Id == recipients[1]);
    }

    [Fact]
    public void GivenANullName_ToContructor_ShouldThrowArgumentNullException()
    {
        //Arrange
        string name = null;

        //Act && Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            var @event = new Event(name,
                                   "test_message",
                                   "test_uri",
                                   DateTime.UtcNow,
                                   "test_action",
                                   "test_issuer",
                                   "test_recipient");
        });
    }

    [Fact]
    public void GivenANullMessage_ToContructor_ShouldReturn_NO_MESSAGE_SPECIFIED()
    {
        //Arrange
        string message = null;

        //Act
        var @event = new Event("test_nameIntegrationEvent",
                               message,
                               "test_uri",
                               DateTime.UtcNow,
                               "test_action",
                               "test_issuer",
                               "test_recipient");

        //Assert
        Assert.Equal("NO_MESSAGE_SPECIFIED", @event.Message);
    }

    [Fact]
    public void GivenANullUri_ToContructor_ShouldReturn_NO_URI_SPECIFIED()
    {
        //Arrange
        string uri = null;

        //Act
        var @event = new Event("test_nameIntegrationEvent",
                               "test_message",
                               uri,
                               DateTime.UtcNow,
                               "test_action",
                               "test_issuer",
                               "test_recipient");

        //Assert
        Assert.Equal("NO_URI_SPECIFIED", @event.Uri);
    }

    [Fact]
    public void GivenARecipientId_ToRecipientSawEventNotificationMethod_ShouldChangeStatusOfRecipientToTrue()
    {
        //Arrange
        string recipientId = "test_recipient_1";

        var @event = new Event("test_nameIntegrationEvent",
                               "test_message",
                               "test_uri",
                               DateTime.UtcNow,
                               "test_action",
                               "test_issuer",
                               new string[2] { "test_recipient_1", "test_recipient_2" });

        //Act
        @event.RecipientSawEventNotification(recipientId);

        //Assert
        Assert.True(@event.Recipients.Where(r => r.Id == recipientId).Select(r => r.Seen).Single());
    }

    [Fact]
    public void GivenARecipientId_ToRecipientSawEventNotificationMethod_TheRecipientDoesntExist_ShouldThrowKeyNotFoundException()
    {
        //Arrange
        string recipientId = "not_found_id";

        var @event = new Event("test_nameIntegrationEvent",
                               "test_message",
                               "test_uri",
                               DateTime.UtcNow,
                               "test_action",
                               "test_issuer",
                               new string[2] { "test_recipient_1", "test_recipient_2" });

        //Act && Assert
        Assert.Throws<KeyNotFoundException>(() => { @event.RecipientSawEventNotification(recipientId); });
    }
}