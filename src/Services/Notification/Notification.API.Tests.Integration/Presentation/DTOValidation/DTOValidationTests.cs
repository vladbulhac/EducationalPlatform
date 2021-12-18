using DataValidation;
using DataValidation.Abstractions;
using Notification.API.Hubs.DataTransferObjects;
using Notification.API.Hubs.DataTransferObjects.Validators;

namespace Notification.API.Tests.Integration.Presentation.DTOValidation;

public class DTOValidationTests : IClassFixture<MockDependenciesHelper<ValidationHandler>>
{
    private IValidationHandler validationHandler;
    private MockDependenciesHelper<ValidationHandler> dependenciesHelper;

    public DTOValidationTests(MockDependenciesHelper<ValidationHandler> dependenciesHelper)
    {
        this.dependenciesHelper = dependenciesHelper ?? throw new ArgumentNullException(nameof(dependenciesHelper));
        validationHandler = new ValidationHandler(this.dependenciesHelper.mockLogger.Object, new(typeof(GetNotificationDTOValidator).Assembly));
    }

    #region GetNotificationDTO validation tests

    [Fact]
    public void GivenAValidGetNotificationDTO_ShoulReturnTrue()
    {
        //Arrange
        var dto = new GetNotificationDTO() { EventId = "mockEventId" };

        //Act
        var result = validationHandler.IsDataTransferObjectValid(dto, out _);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void GivenAValidGetNotificationDTO_ShoulReturnEmptyValidationErrors()
    {
        //Arrange
        var dto = new GetNotificationDTO() { EventId = "mockEventId" };

        //Act
        validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Empty(validationErrors);
    }

    [Fact]
    public void GivenAnInvalidGetNotificationDTO_WithEventIdNull_ShoulReturnFalse()
    {
        //Arrange
        var dto = new GetNotificationDTO() { EventId = null };

        //Act
        var result = validationHandler.IsDataTransferObjectValid(dto, out _);

        //Assert
        Assert.False(result);
    }

    [Fact]
    public void GivenAnInvalidGetNotificationDTO_WithEventIdNull_ShoulReturnValidationErrors()
    {
        //Arrange
        var dto = new GetNotificationDTO() { EventId = null };

        //Act
        validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Equal("Property EventId failed validation. Error was: Event Id was empty or null!", validationErrors);
    }

    [Fact]
    public void GivenAnInvalidGetNotificationDTO_WithEventIdEmpty_ShoulReturnFalse()
    {
        //Arrange
        var dto = new GetNotificationDTO() { EventId = string.Empty };

        //Act
        var result = validationHandler.IsDataTransferObjectValid(dto, out _);

        //Assert
        Assert.False(result);
    }

    [Fact]
    public void GivenAnInvalidGetNotificationDTO_WithEventIdEmpty_ShoulReturnValidationErrors()
    {
        //Arrange
        var dto = new GetNotificationDTO() { EventId = string.Empty };

        //Act
        validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Equal("Property EventId failed validation. Error was: Event Id was empty or null!", validationErrors);
    }

    #endregion GetNotificationDTO validation tests

    #region SeenNotificationsDTO validation tests

    [Fact]
    public void GivenAValidSeenNotificationsDTO_ShouldReturnTrue()
    {
        //Arrange
        var dto = new SeenNotificationsDTO() { EventsIds = new string[2] { "eventOne", "eventTwo" } };

        //Act
        var result = validationHandler.IsDataTransferObjectValid(dto, out _);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void GivenAValidSeenNotificationsDTO_ShouldReturnEmptyValidationErrors()
    {
        //Arrange
        var dto = new SeenNotificationsDTO() { EventsIds = new string[2] { "eventOne", "eventTwo" } };

        //Act
        validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Empty(validationErrors);
    }

    [Fact]
    public void GivenAnInvalidSeenNotificationsDTO_WithEmptyCollection_ShouldReturnFalse()
    {
        //Arrange
        var dto = new SeenNotificationsDTO() { EventsIds = Array.Empty<string>() };

        //Act
        var result = validationHandler.IsDataTransferObjectValid(dto, out _);

        //Assert
        Assert.False(result);
    }

    [Fact]
    public void GivenAnInvalidSeenNotificationsDTO_WithEmptyCollection_ShouldReturnValidationErrors()
    {
        //Arrange
        var dto = new SeenNotificationsDTO() { EventsIds = Array.Empty<string>() };

        //Act
        validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Equal("Property EventsIds failed validation. Error was: Events Ids was empty or null!", validationErrors);
    }

    [Fact]
    public void GivenAnInvalidSeenNotificationsDTO_WithAnEmptyId_ShouldReturnFalse()
    {
        //Arrange
        var dto = new SeenNotificationsDTO() { EventsIds = new string[2] { "eventOne", string.Empty } };

        //Act
        var result = validationHandler.IsDataTransferObjectValid(dto, out _);

        //Assert
        Assert.False(result);
    }

    [Fact]
    public void GivenAnInvalidSeenNotificationsDTO_WithAnEmptyId_ShouldReturnValidationErrors()
    {
        //Arrange
        var dto = new SeenNotificationsDTO() { EventsIds = new string[2] { "eventOne", string.Empty } };

        //Act
        validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Equal("Property EventsIds[1] failed validation. Error was: An id was empty or null!", validationErrors);
    }

    #endregion SeenNotificationsDTO validation tests

    #region GetNotificationsOfRecipientDTO validation tests

    [Fact]
    public void GivenAValidGetNotificationsOfRecipientDTO_ShouldReturnTrue()
    {
        //Arrange
        var dto = new GetNotificationsOfRecipientDTO() { Offset = 0, ResultsCount = 10 };

        //Act
        var result = validationHandler.IsDataTransferObjectValid(dto, out _);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void GivenAValidGetNotificationsOfRecipientDTO_ShouldReturnEmptyValidationErrors()
    {
        //Arrange
        var dto = new GetNotificationsOfRecipientDTO() { Offset = 0, ResultsCount = 10 };

        //Act
        validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Empty(validationErrors);
    }

    [Fact]
    public void GivenAnInvalidGetNotificationsOfRecipientDTO_WithOffsetValueNegative_ShouldReturnFalse()
    {
        //Arrange
        var dto = new GetNotificationsOfRecipientDTO() { Offset = -1, ResultsCount = 10 };

        //Act
        var result = validationHandler.IsDataTransferObjectValid(dto, out _);

        //Assert
        Assert.False(result);
    }

    [Fact]
    public void GivenAnInvalidGetNotificationsOfRecipientDTO_WithOffsetValueNegative_ShouldReturnValidationErrors()
    {
        //Arrange
        var dto = new GetNotificationsOfRecipientDTO() { Offset = -1, ResultsCount = 10 };

        //Act
        validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Equal("Property Offset failed validation. Error was: Offset was an incorrect value! It must be a value greater or equal to 0!", validationErrors);
    }

    [Fact]
    public void GivenAnInvalidGetNotificationsOfRecipientDTO_WithResultsCountValueZero_ShouldReturnFalse()
    {
        //Arrange
        var dto = new GetNotificationsOfRecipientDTO() { Offset = 0, ResultsCount = 0 };

        //Act
        var result = validationHandler.IsDataTransferObjectValid(dto, out _);

        //Assert
        Assert.False(result);
    }

    [Fact]
    public void GivenAnInvalidGetNotificationsOfRecipientDTO_WithResultsCountValueZero_ShouldReturnValidationErrors()
    {
        //Arrange
        var dto = new GetNotificationsOfRecipientDTO() { Offset = 0, ResultsCount = 0 };

        //Act
        validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Equal("Property ResultsCount failed validation. Error was: Results Count was an incorrect value! It must be a value greater or equal to 1!", validationErrors);
    }

    #endregion GetNotificationsOfRecipientDTO validation tests
}