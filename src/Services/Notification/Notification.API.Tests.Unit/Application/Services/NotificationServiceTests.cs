using Notification.API.Tests.Shared;
using Notification.Application.Services;
using Notification.Domain.Models.Aggregates;

namespace Notification.API.Tests.Unit.Application.Services;

public class NotificationServiceTests : IClassFixture<MockDependenciesHelper<NotificationService>>, IClassFixture<JSONDataParser>
{
    private readonly MockDependenciesHelper<NotificationService> dependenciesHelper;
    private readonly NotificationService notificationService;
    private readonly JSONDataParser testDataHelper;

    public NotificationServiceTests(MockDependenciesHelper<NotificationService> dependenciesHelper, JSONDataParser testDataHelper)
    {
        this.testDataHelper = testDataHelper;
        this.dependenciesHelper = dependenciesHelper;
        notificationService = new(dependenciesHelper.mockNotificationRepository.Object, dependenciesHelper.mockLogger.Object);
    }

    #region GetNotificationsAsync method tests

    [Fact]
    public async Task GivenAnUserId_OffsetValue_ResultsCount_ToGetNotificationsAsyncMethod_ShouldReturnOneNotification()
    {
        //Arrange
        var userId = "fixed-recipient-id";
        int offset = 0, resultsCount = 10;

        dependenciesHelper.mockNotificationRepository.Setup(mnr => mnr.GetAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                                                     .ReturnsAsync(new Event[2] { testDataHelper.Events[0], testDataHelper.Events[1] });

        //Act
        var result = await notificationService.GetNotificationsAsync(userId, offset, resultsCount);

        //Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GivenAnUserId_OffsetValue_ResultsCount_ToGetNotificationsAsyncMethod_ShouldHaveExpectedEventIdInCollection()
    {
        //Arrange
        var userId = "fixed-recipient-id";
        int offset = 0, resultsCount = 10;

        dependenciesHelper.mockNotificationRepository.Setup(mnr => mnr.GetAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                                                     .ReturnsAsync(new Event[2] { testDataHelper.Events[0], testDataHelper.Events[1] });

        //Act
        var result = await notificationService.GetNotificationsAsync(userId, offset, resultsCount);

        //Assert
        Assert.Contains(result, n => n.Id == testDataHelper.Events[0].Id);
    }

    [Fact]
    public async Task GivenAnUserId_OffsetValue_ResultsCount_ToGetNotificationsAsyncMethod_AnExceptionCaught_ShouldReturnAnEmptyCollection()
    {
        //Arrange
        var userId = "fixed-recipient-id";
        int offset = 0, resultsCount = 10;

        dependenciesHelper.mockNotificationRepository.Setup(mnr => mnr.GetAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                                                     .Throws<ArgumentException>();

        //Act
        var result = await notificationService.GetNotificationsAsync(userId, offset, resultsCount);

        //Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GivenAnEmptyUserId_OffsetValue_ResultsCount_ToGetNotificationsAsyncMethod_ShouldThrowArgumentException()
    {
        //Arrange
        string userId = string.Empty;
        int offset = 0, resultsCount = 1;

        //Act && Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await notificationService.GetNotificationsAsync(userId, offset, resultsCount));
    }

    [Fact]
    public async Task GivenAnUserId_NegativeOffsetValue_ResultsCount_ToGetNotificationsAsyncMethod_ShouldThrowArgumentOutOfRangeException()
    {
        //Arrange
        string userId = "fixed-recipient-id";
        int offset = -1, resultsCount = 1;

        //Act && Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await notificationService.GetNotificationsAsync(userId, offset, resultsCount));
    }

    [Fact]
    public async Task GivenAnUserId_OffsetValue_NegativeResultsCount_ToGetNotificationsAsyncMethod_ShouldThrowArgumentOutOfRangeException()
    {
        //Arrange
        string userId = "fixed-recipient-id";
        int offset = 0, resultsCount = -1;

        //Act && Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await notificationService.GetNotificationsAsync(userId, offset, resultsCount));
    }

    #endregion GetNotificationsAsync method tests
}