using DataValidation;
using DataValidation.Abstractions;
using EducationalInstitution.Application.Queries;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EducationalInstitution.API.IntegrationTests.Application_Tests.Validation;

public class QueriesValidationTests
{
    private readonly Mock<ILogger<ValidationHandler>> mockLogger;
    private readonly IValidationHandler validationHandler;

    /// <remarks>Called before each test</remarks>
    public QueriesValidationTests()
    {
        mockLogger = new();

        var applicationAssembly = typeof(GetEducationalInstitutionByIDQuery).Assembly;
        validationHandler = new ValidationHandler(mockLogger.Object, new ValidatorFactory(applicationAssembly));
    }

    #region GetEducationalInstitutionByIDQuery TESTS

    [Fact]
    public void GivenAValidGetEducationalInstitutionByIDQuery_ShouldReturnTrue()
    {
        //Arrange
        var dto = new GetEducationalInstitutionByIDQuery { EducationalInstitutionID = Guid.NewGuid() };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

        //Assert
        Assert.True(validationResult);
    }

    [Fact]
    public void GivenAValidGetEducationalInstitutionByIDQuery_ShouldReturnAnEmptyValidationErrorsString()
    {
        //Arrange
        var dto = new GetEducationalInstitutionByIDQuery { EducationalInstitutionID = Guid.NewGuid() };
        //Act
        validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Empty(validationErrors);
    }

    [Fact]
    public void GivenAnInvalidGetEducationalInstitutionByIDQuery_WithDefaultEducationalInstitutionID_ShouldReturnFalse()
    {
        //Arrange
        var dto = new GetEducationalInstitutionByIDQuery { EducationalInstitutionID = default };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

        //Assert
        Assert.False(validationResult);
    }

    [Fact]
    public void GivenAnInvalidGetEducationalInstitutionByIDQuery_WithDefaultEducationalInstitutionID_ShouldReturnAStringWithTheErrorsFound()
    {
        //Arrange
        var dto = new GetEducationalInstitutionByIDQuery { EducationalInstitutionID = default };

        //Act
        validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Equal("Property EducationalInstitutionID failed validation. Error was: Educational Institution ID was empty or null!", validationErrors);
    }

    #endregion GetEducationalInstitutionByIDQuery TESTS

    #region GetAllEducationalInstitutionsByNameQuery TESTS

    [Fact]
    public void GivenAValidGetAllEducationalInstitutionsByNameQuery_ShouldReturnTrue()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByNameQuery { Name = "University", OffsetValue = 0, ResultsCount = 1 };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

        //Assert
        Assert.True(validationResult);
    }

    [Fact]
    public void GivenAValidGetAllEducationalInstitutionsByNameQuery_ShouldReturnAnEmptyValidationErrorsString()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByNameQuery { Name = "University", OffsetValue = 0, ResultsCount = 1 };

        //Act
        validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Empty(validationErrors);
    }

    [Fact]
    public void GivenAnInvalidGetAllEducationalInstitutionsByNameQuery_WithNullName_ShouldReturnFalse()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByNameQuery { Name = null, OffsetValue = 0, ResultsCount = 1 };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

        //Assert
        Assert.False(validationResult);
    }

    [Fact]
    public void GivenAnInvalidGetAllEducationalInstitutionsByNameQuery_WithEmptyName_ShouldReturnFalse()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByNameQuery { Name = string.Empty, OffsetValue = 0, ResultsCount = 1 };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

        //Assert
        Assert.False(validationResult);
    }

    [Fact]
    public void GivenAnInvalidGetAllEducationalInstitutionsByNameQuery_WithEmptyName_ShouldReturnValidationErrors()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByNameQuery { Name = string.Empty, OffsetValue = 0, ResultsCount = 1 };

        //Act
        validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Equal("Property Name failed validation. Error was: Name was empty or null!", validationErrors);
    }

    [Fact]
    public void GivenAnInvalidGetAllEducationalInstitutionsByNameQuery_WithNullName_ShouldReturnValidationErrors()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByNameQuery { Name = null, OffsetValue = 0, ResultsCount = 1 };

        //Act
        validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Equal("Property Name failed validation. Error was: Name was empty or null!", validationErrors);
    }

    [Fact]
    public void GivenAnInvalidGetAllEducationalInstitutionsByNameQuery_WithNameOfLengthOne_ShouldReturnValidationErrors()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByNameQuery { Name = "a", OffsetValue = 0, ResultsCount = 1 };

        //Act
        validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Equal("Property Name failed validation. Error was: Name's length was not between 2-128 characters!", validationErrors);
    }

    [Fact]
    public void GivenAnInvalidGetAllEducationalInstitutionsByNameQuery_WithNegativeOffsetValue_ShouldReturnFalse()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByNameQuery { Name = "University", OffsetValue = -1, ResultsCount = 1 };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.False(validationResult);
    }

    [Fact]
    public void GivenAnInvalidGetAllEducationalInstitutionsByNameQuery_WithNegativeOffsetValue_ShouldReturnValidationErrors()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByNameQuery { Name = "University", OffsetValue = -1, ResultsCount = 1 };

        //Act
        validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Equal("Property OffsetValue failed validation. Error was: Offset Value was not between 0 and 150!", validationErrors);
    }

    [Fact]
    public void GivenAnInvalidGetAllEducationalInstitutionsByNameQuery_WithZeroResultsCount_ShouldReturnFalse()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByNameQuery { Name = "University", OffsetValue = 0, ResultsCount = 0 };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.False(validationResult);
    }

    [Fact]
    public void GivenAnInvalidGetAllEducationalInstitutionsByNameQuery_WithZeroResultsCount_ShouldReturnValidationErrors()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByNameQuery { Name = "University", OffsetValue = 0, ResultsCount = 0 };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Equal("Property ResultsCount failed validation. Error was: Results Count was not between 1 and 100!", validationErrors);
    }

    #endregion GetAllEducationalInstitutionsByNameQuery TESTS

    #region GetAllEducationalInstitutionsByLocationQuery TESTS

    [Fact]
    public void GivenAValidGetAllEducationalInstitutionsByLocationQuery_ShouldReturnTrue()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByLocationQuery { LocationID = "6050efcd87e2647ab7ac443e" };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

        //Assert
        Assert.True(validationResult);
    }

    [Fact]
    public void GivenAValidGetAllEducationalInstitutionsByLocationQuery_ShouldReturnAnEmptyValidationErrorsString()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByLocationQuery { LocationID = "6050efcd87e2647ab7ac443e" };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Empty(validationErrors);
    }

    [Fact]
    public void GivenAnInvalidGetAllEducationalInstitutionsByLocationQuery_WithEmptyLocationID_ShouldReturnFalse()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByLocationQuery { LocationID = string.Empty };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

        //Assert
        Assert.False(validationResult);
    }

    [Fact]
    public void GivenAnInvalidGetAllEducationalInstitutionsByLocationQuery_WithEmptyLocationID_ShouldReturnValidationErrors()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByLocationQuery { LocationID = string.Empty };

        //Act
        validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Equal("Property LocationID failed validation. Error was: Location ID was empty or null!", validationErrors);
    }

    [Fact]
    public void GivenAanInvalidGetAllEducationalInstitutionsByLocationQuery_WithLocationIDOfLengthNot24_ShouldReturnFalse()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByLocationQuery { LocationID = "eIdL14F9" };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.False(validationResult);
    }

    [Fact]
    public void GivenAnInvalidGetAllEducationalInstitutionsByLocationQuery_WithLocationIDOfLengthNot24_ShouldReturnValidationErrors()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByLocationQuery { LocationID = "eIdL14F9" };

        //Act
        validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Equal("Property LocationID failed validation. Error was: Location ID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
    }

    [Fact]
    public void GivenAnInvalidGetAllEducationalInstitutionsByLocationQuery_WithLocationIDThatContainsProhibitedCharacters_ShouldReturnFalse()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByLocationQuery { LocationID = "6050efcd87e2647ab7ac443~" };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.False(validationResult);
    }

    [Fact]
    public void GivenAnInvalidGetAllEducationalInstitutionsByLocationQuery_WithLocationIDThatContainsProhibitedCharacters_ShouldReturnValidationErrors()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByLocationQuery { LocationID = "6050efcd87e2647ab7ac443~" };

        //Act
        validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Equal("Property LocationID failed validation. Error was: Location ID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
    }

    #endregion GetAllEducationalInstitutionsByLocationQuery TESTS

    #region GetAllAdminsByEducationalInstitutionIDQuery TESTS

    [Fact]
    public void GivenAValidGetAllAdminsByEducationalInstitutionIDQuery_ShouldReturnTrue()
    {
        //Arrange
        var dto = new GetAllAdminsByEducationalInstitutionIDQuery { EducationalInstitutionID = Guid.NewGuid() };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

        //Assert
        Assert.True(validationResult);
    }

    [Fact]
    public void GivenAValidGetAllAdminsByEducationalInstitutionIDQuery_ShouldReturnEmptyValidationErrorsString()
    {
        //Arrange
        var dto = new GetAllAdminsByEducationalInstitutionIDQuery { EducationalInstitutionID = Guid.NewGuid() };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Empty(validationErrors);
    }

    [Fact]
    public void GivenAnInvalidGetAllAdminsByEducationalInstitutionIDQuery_WithDefaultID_ShouldReturnFalse()
    {
        //Arrange
        var dto = new GetAllAdminsByEducationalInstitutionIDQuery { EducationalInstitutionID = default };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

        //Assert
        Assert.False(validationResult);
    }

    [Fact]
    public void GivenAnInvalidGetAllAdminsByEducationalInstitutionIDQuery_WithDefaultID_ShouldReturnAStringWithValidationError()
    {
        //Arrange
        var dto = new GetAllAdminsByEducationalInstitutionIDQuery { EducationalInstitutionID = default };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Equal("Property EducationalInstitutionID failed validation. Error was: EducationalInstitutionID is empty, null or default!", validationErrors);
    }

    #endregion GetAllAdminsByEducationalInstitutionIDQuery TESTS

    #region GetAllEducationalInstitutionsByBuildingQuery TEST

    [Fact]
    public void GivenAValidGetAllEducationalInstitutionsByBuildingQuery_ShouldReturnTrue()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByBuildingQuery { BuildingID = "12AEDa09344151BbdDefF134" };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.True(validationResult);
    }

    [Fact]
    public void GivenAValidGetAllEducationalInstitutionsByBuildingQuery_ShouldReturnEmptyValidationErrors()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByBuildingQuery { BuildingID = "12AEDa09344151BbdDefF134" };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Empty(validationErrors);
    }

    [Fact]
    public void GivenAnInvalidGetAllEducationalInstitutionsByBuildingQuery_WithEmptyBuildingID_ShouldReturnFalse()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByBuildingQuery { BuildingID = string.Empty };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.False(validationResult);
    }

    [Fact]
    public void GivenAnInvalidGetAllEducationalInstitutionsByBuildingQuery_WithEmptyBuildingID_ShouldReturnValidationErrors()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByBuildingQuery { BuildingID = string.Empty };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Equal("Property BuildingID failed validation. Error was: Building ID was empty or null!", validationErrors);
    }

    [Fact]
    public void GivenAnInvalidGetAllEducationalInstitutionsByBuildingQuery_WithBuildingIDLengthNot24_ShouldReturnFalse()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByBuildingQuery { BuildingID = "a1234b" };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.False(validationResult);
    }

    [Fact]
    public void GivenAnInvalidGetAllEducationalInstitutionsByBuildingQuery_WithBuildingIDLengthNot24_ShouldReturnValidationErrors()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByBuildingQuery { BuildingID = "a1234b" };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Equal("Property BuildingID failed validation. Error was: Building ID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
    }

    [Fact]
    public void GivenAnInvalidGetAllEducationalInstitutionsByBuildingQuery_WithBuildingIDContainingCharactersOutOfTheAlphabet_ShouldReturnFalse()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByBuildingQuery { BuildingID = "12AEDa09344151BbdDefF13Q" };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.False(validationResult);
    }

    [Fact]
    public void GivenAnInvalidGetAllEducationalInstitutionsByBuildingQuery_WithBuildingIDContainingCharactersOutOfTheAlphabet_ShouldReturnValidationErrors()
    {
        //Arrange
        var dto = new GetAllEducationalInstitutionsByBuildingQuery { BuildingID = "12AEDa09344151BbdDefF13Q" };

        //Act
        var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

        //Assert
        Assert.Equal("Property BuildingID failed validation. Error was: Building ID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
    }

    #endregion GetAllEducationalInstitutionsByBuildingQuery TEST
}