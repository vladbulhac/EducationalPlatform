using Notification.API.Tests.Shared;
using Notification.Application.DTOs;

namespace Notification.API.Tests.Unit.Application.DTOs;

public class NotificationBodyTests : IClassFixture<JSONDataParser>
{
    private readonly JSONDataParser testDataHelper;

    public NotificationBodyTests(JSONDataParser testDataHelper)
    {
        this.testDataHelper = testDataHelper;
    }

    [Fact]
    public void GivenAnEventAndId_ShouldReturnSameIdAsTheEvent()
    {
        //Arrange
        var @event = testDataHelper.Events[0];
        var id = @event.Recipients.First().Id;

        //Act
        var notification = new NotificationBody(@event, id);

        //Arrange
        Assert.Equal(@event.Id, notification.Id);
    }

    [Fact]
    public void GivenAnEventAndId_ShouldReturnAsTitleTheEventNameWithSpacesAfterWords()
    {
        //Arrange
        var @event = testDataHelper.Events[0];
        var id = @event.Recipients.First().Id;

        //Act
        var notification = new NotificationBody(@event, id);

        //Arrange
        Assert.Equal("Test Event One", notification.Title);
    }

    [Fact]
    public void GivenAnEventAndId_ShouldReturnSameFromAsEventIssuer()
    {
        //Arrange
        var @event = testDataHelper.Events[0];
        var id = @event.Recipients.First().Id;

        //Act
        var notification = new NotificationBody(@event, id);

        //Arrange
        Assert.Equal(@event.TriggerDetails.Issuer, notification.From);
    }

    [Fact]
    public void GivenAnEventAndId_ShouldReturnSameToAsId()
    {
        //Arrange
        var @event = testDataHelper.Events[0];
        var id = @event.Recipients.First().Id;

        //Act
        var notification = new NotificationBody(@event, id);

        //Arrange
        Assert.Equal(id, notification.To);
    }

    [Fact]
    public void GivenAnEventAndId_ShouldReturnSameMessageAsEventMessage()
    {
        //Arrange
        var @event = testDataHelper.Events[0];
        var id = @event.Recipients.First().Id;

        //Act
        var notification = new NotificationBody(@event, id);

        //Arrange
        Assert.Equal(@event.Message, notification.Message);
    }

    [Fact]
    public void GivenAnEventAndId_ShouldReturnSameUriAsEventUri()
    {
        //Arrange
        var @event = testDataHelper.Events[0];
        var id = @event.Recipients.First().Id;

        //Act
        var notification = new NotificationBody(@event, id);

        //Arrange
        Assert.Equal(@event.Uri, notification.Uri);
    }

    [Fact]
    public void GivenAnEventAndId_ShouldReturnSameIssuedAtAsEventTimeIssued()
    {
        //Arrange
        var @event = testDataHelper.Events[0];
        var id = @event.Recipients.First().Id;

        //Act
        var notification = new NotificationBody(@event, id);

        //Arrange
        Assert.Equal(@event.TriggerDetails.TimeIssued, notification.IssuedAt);
    }

    [Fact]
    public void GivenAnEventAndId_ShouldReturnForRecipientTheSameSeenValue()
    {
        //Arrange
        var @event = testDataHelper.Events[0];
        var id = @event.Recipients.First().Id;

        //Act
        var notification = new NotificationBody(@event, id);

        //Arrange
        Assert.Equal(@event.Recipients.First().Seen, notification.Seen);
    }
}