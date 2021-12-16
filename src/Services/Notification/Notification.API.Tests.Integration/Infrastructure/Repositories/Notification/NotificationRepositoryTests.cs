using Notification.Domain.Models.Aggregates;
using Notification.Infrastructure.Repositories;

namespace Notification.API.Tests.Integration.Infrastructure.Repositories.Notification;

[Collection("Database collection")]
public class NotificationRepositoryTests
{
    private readonly DatabaseFixture dbHelper;
    private readonly INotificationRepository notificationRepository;

    public NotificationRepositoryTests(DatabaseFixture dbHelper)
    {
        this.dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));

        this.notificationRepository = new NotificationRepository(dbHelper.Context);
    }

    [IgnoreWhenDatabaseIsNotLoaded]
    public async Task GivenAnEventIdAndRecipientId_ToGetByIdAsyncMethod_ShouldReturnAnEvent()
    {
        //Arrange
        var eventId = dbHelper.testDataHelper.Events[0].Id;
        var recipientId = dbHelper.testDataHelper.Events[0].Recipients.First().Id;

        //Act
        var result = await notificationRepository.GetByIdAsync(eventId, recipientId);

        //Assert
        Assert.NotNull(result);
    }

    [IgnoreWhenDatabaseIsNotLoaded]
    public async Task GivenAnEventIdAndRecipientId_ToGetByIdAsyncMethod_ShouldReturnExpectedId()
    {
        //Arrange
        var eventId = dbHelper.testDataHelper.Events[0].Id;
        var recipientId = dbHelper.testDataHelper.Events[0].Recipients.First().Id;

        //Act
        var result = await notificationRepository.GetByIdAsync(eventId, recipientId);

        //Assert
        Assert.Equal(eventId, result.Id);
    }

    [IgnoreWhenDatabaseIsNotLoaded]
    public async Task GivenAnEventIdAndRecipientId_ToGetByIdAsyncMethod_TheEventIsNotFound_ShouldReturnNull()
    {
        //Arrange
        var eventId = "not-found-id";
        var recipientId = dbHelper.testDataHelper.Events[0].Recipients.First().Id;

        //Act
        var result = await notificationRepository.GetByIdAsync(eventId, recipientId);

        //Assert
        Assert.Null(result);
    }

    [IgnoreWhenDatabaseIsNotLoaded]
    public async Task GivenAnEventIdAndRecipientId_ToGetByIdAsyncMethod_TheRecipientIsNotFound_ShouldReturnNull()
    {
        //Arrange
        var eventId = dbHelper.testDataHelper.Events[0].Id;
        var recipientId = "not-found-id";

        //Act
        var result = await notificationRepository.GetByIdAsync(eventId, recipientId);

        //Assert
        Assert.Null(result);
    }

    [IgnoreWhenDatabaseIsNotLoaded]
    public async Task GivenAnEvent_ToAddAsyncMethod_ShouldReturnExistInDatabase()
    {
        //Arrange
        var entity = new Event("test_nameIntegrationEvent",
                               "test_message",
                               "test_uri",
                               new DateTime(),
                               "add",
                               "test",
                               "test_recipient");

        //Act
        await notificationRepository.AddAsync(entity);
        await notificationRepository.SaveChangesAsync();

        //Assert
        var @event = await notificationRepository.GetByIdAsync(entity.Id, "test_recipient");
        Assert.NotNull(@event);
    }

    [IgnoreWhenDatabaseIsNotLoaded]
    public async Task GivenARecipientId_ToGetAsyncMethod_ShouldReturnThreeEvents()
    {
        //Arrange
        var recipientId = "fixed-recipient-id";

        //Act
        var result = await notificationRepository.GetAsync(recipientId);

        //Assert
        Assert.Equal(3, result.Count);
    }

    [IgnoreWhenDatabaseIsNotLoaded]
    public async Task GivenARecipientIdAndResultsCountValueOfTwo_ToGetAsyncMethod_ShouldReturnTwoEvents()
    {
        //Arrange
        var recipientId = "fixed-recipient-id";
        int resultsCount = 2;

        //Act
        var result = await notificationRepository.GetAsync(recipientId, resultsCount: resultsCount);

        //Assert
        Assert.Equal(2, result.Count);
    }

    [IgnoreWhenDatabaseIsNotLoaded]
    public async Task GivenARecipientIdAndOffsetValueOfTwo_ToGetAsyncMethod_ShouldReturnOneEvent()
    {
        //Arrange
        var recipientId = "fixed-recipient-id";
        int offset = 2;

        //Act
        var result = await notificationRepository.GetAsync(recipientId, offset);

        //Assert
        Assert.Single(result);
    }

    [IgnoreWhenDatabaseIsNotLoaded]
    public async Task GivenARecipientIdAndOffsetValueOfThree_ToGetAsyncMethod_ShouldReturnAnEmptyCollection()
    {
        //Arrange
        var recipientId = "fixed-recipient-id";
        int offset = 3;

        //Act
        var result = await notificationRepository.GetAsync(recipientId, offset);

        //Assert
        Assert.Empty(result);
    }

    [IgnoreWhenDatabaseIsNotLoaded]
    public async Task GivenARecipientId_ToGetAsyncMethod_TheRecipientIsNotFound_ShouldReturnAnEmptyCollection()
    {
        //Arrange
        var recipientId = "not-found-id";

        //Act
        var result = await notificationRepository.GetAsync(recipientId);

        //Assert
        Assert.Empty(result);
    }

    [IgnoreWhenDatabaseIsNotLoaded]
    public async Task GivenARecipientIdAndEventsIdsCollection_ToGetEventsFromCollectionMethod_ShouldReturnTwoEvents()
    {
        //Arrange
        var recipientId = "fixed-recipient-id";
        var eventsIds = new string[2] { dbHelper.testDataHelper.Events[0].Id, dbHelper.testDataHelper.Events[1].Id };

        //Act
        var result = await notificationRepository.GetEventsFromCollectionAsync(eventsIds, recipientId);

        //Assert
        Assert.Equal(2, result.Count);
    }

    [IgnoreWhenDatabaseIsNotLoaded]
    public async Task GivenARecipientIdAndEventsIdsCollection_ToGetEventsFromCollectionMethod_RecipientIdIsNotFound_ShouldReturnEmptyEventsCollection()
    {
        //Arrange
        var recipientId = "not-found-id";
        var eventsIds = new string[2] { dbHelper.testDataHelper.Events[0].Id, dbHelper.testDataHelper.Events[1].Id };

        //Act
        var result = await notificationRepository.GetEventsFromCollectionAsync(eventsIds, recipientId);

        //Assert
        Assert.Empty(result);
    }
}