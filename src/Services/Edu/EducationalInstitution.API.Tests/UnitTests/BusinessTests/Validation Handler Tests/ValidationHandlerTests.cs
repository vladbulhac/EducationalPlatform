using EducationaInstitutionAPI.Business;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.In;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Commands;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace EducationalInstitution.API.Tests.UnitTests.BusinessTests.Validation_Handler_Tests
{
    public class ValidationHandlerTests
    {
        private readonly Mock<ILogger<ValidationHandler>> mockLogger;

        public ValidationHandlerTests()
        {
            mockLogger = new();
        }

        #region Request of type DTOEducationalInstitutionByIDQuery TESTS

        #region Input: ID = Guid | Expect: Result to be true

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByIDQuery_ShouldReturnTrue()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByIDQuery(Guid.NewGuid());
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.True(validationResult);
        }

        #endregion Input: ID = Guid | Expect: Result to be true

        #region Input: ID = Guid | Expect: Result's validationError output to be empty

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByIDQuery_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByIDQuery(Guid.NewGuid());
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        #endregion Input: ID = Guid | Expect: Result's validationError output to be empty

        #region Input: ID = default Guid | Expect: Result to be false

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByIDQuery_WithDefaultID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByIDQuery(default);
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.False(validationResult);
        }

        #endregion Input: ID = default Guid | Expect: Result to be false

        #region Input: ID = default Guid | Expect: Result's validationError output to contain the validation errors

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByIDQuery_WithDefaultID_ShouldReturnAStringWithTheErrorsFound()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByIDQuery(default);
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property EduInstitutionID failed validation. Error was: Edu Institution ID was empty or null!", validationErrors);
        }

        #endregion Input: ID = default Guid | Expect: Result's validationError output to contain the validation errors

        #endregion Request of type DTOEducationalInstitutionByIDQuery TESTS

        #region Request of type DTOEducationalInstitutionByNameQuery TESTS

        #region Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result to be true

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByNameQuery_ShouldReturnTrue()
        {
            //Arrange
            var request = new DTOEducationalInstitutionsByNameQuery() { Name = "University", OffsetValue = 0, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.True(validationResult);
        }

        #endregion Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result to be true

        #region Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's validationErrors output to be empty

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByNameQuery_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionsByNameQuery() { Name = "University", OffsetValue = 0, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        #endregion Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's validationErrors output to be empty

        #region Input: Name = null string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result to be false

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByNameQuery_WithNullName_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionsByNameQuery() { Name = null, OffsetValue = 0, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.False(validationResult);
        }

        #endregion Input: Name = null string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result to be false

        #region Input: Name = empty string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result to be false

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByNameQuery_WithEmptyName_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionsByNameQuery() { Name = string.Empty, OffsetValue = 0, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.False(validationResult);
        }

        #endregion Input: Name = empty string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result to be false

        #region Input: Name = empty string, OffsetValue = int in [0,150], ResultsCounts = int in [1,100] | Expect: Result's validationErrors output to contain the validation errors

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByNameQuery_WithEmptyName_ShouldReturnMessage()
        {
            //Arrange
            var request = new DTOEducationalInstitutionsByNameQuery() { Name = string.Empty, OffsetValue = 0, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property Name failed validation. Error was: Name was empty or null!", validationErrors);
        }

        #endregion Input: Name = empty string, OffsetValue = int in [0,150], ResultsCounts = int in [1,100] | Expect: Result's validationErrors output to contain the validation errors

        #region Input: Name = null string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's validationErrors output to contain the validation errors

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByNameQuery_WithNullName_ShouldReturnMessage()
        {
            //Arrange
            var request = new DTOEducationalInstitutionsByNameQuery() { Name = null, OffsetValue = 0, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property Name failed validation. Error was: Name was empty or null!", validationErrors);
        }

        #endregion Input: Name = null string, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's validationErrors output to contain the validation errors

        #region Input: Name = string of length 1, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's validationErrors output to contain the validation errors

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByNameQuery_WithNameOfLengthOne_ShouldReturnMessage()
        {
            //Arrange
            var request = new DTOEducationalInstitutionsByNameQuery() { Name = "a", OffsetValue = 0, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property Name failed validation. Error was: Name's length was not between 2-128 characters!", validationErrors);
        }

        #endregion Input: Name = string of length 1, OffsetValue = int in [0,150], ResultsCount = int in [1,100] | Expect: Result's validationErrors output to contain the validation errors

        #region Input: Name = string, OffsetValue = int < 0, ResultsCount = int in [1,100] | Expect: Result to be false

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByNameQuery_WithNegativeOffsetValue_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionsByNameQuery() { Name = "University", OffsetValue = -1, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        #endregion Input: Name = string, OffsetValue = int < 0, ResultsCount = int in [1,100] | Expect: Result to be false

        #region Input: Name = string, OffsetValue = int < 0, ResultsCount = int in [1,100] | Expect: Result's validationErrors output to contain the validation errors

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByNameQuery_WithNegativeOffsetValue_ShouldReturnMessage()
        {
            //Arrange
            var request = new DTOEducationalInstitutionsByNameQuery() { Name = "University", OffsetValue = -1, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property OffsetValue failed validation. Error was: Offset Value was not between 0 and 150!", validationErrors);
        }

        #endregion Input: Name = string, OffsetValue = int < 0, ResultsCount = int in [1,100] | Expect: Result's validationErrors output to contain the validation errors

        #region Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int == 0 | Expect: Result to be false

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByNameQuery_WithZeroResultsCount_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionsByNameQuery() { Name = "University", OffsetValue = 0, ResultsCount = 0 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        #endregion Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int == 0 | Expect: Result to be false

        #region Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int == 0 | Expect: Result's validationErrors output to contain the validation errors

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByNameQuery_WithZeroResultsCount_ShouldReturnMessage()
        {
            //Arrange
            var request = new DTOEducationalInstitutionsByNameQuery() { Name = "University", OffsetValue = 0, ResultsCount = 0 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property ResultsCount failed validation. Error was: Results Count was not between 1 and 100!", validationErrors);
        }

        #endregion Input: Name = string, OffsetValue = int in [0,150], ResultsCount = int == 0 | Expect: Result's validationErrors output to contain the validation errors

        #endregion Request of type DTOEducationalInstitutionByNameQuery TESTS

        #region Request of type DTOEducationalInstitutionByLocationQuery TESTS

        #region Input: ID = string of length 24 | Expect: Result to be true

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByLocationQuery_ShouldReturnTrue()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByLocationQuery("6050efcd87e2647ab7ac443e");
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.True(validationResult);
        }

        #endregion Input: ID = string of length 24 | Expect: Result to be true

        #region Input: ID = string of length 24 | Expect: Result's validationError output to be empty

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByLocationQuery_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByLocationQuery("6050efcd87e2647ab7ac443e");
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        #endregion Input: ID = string of length 24 | Expect: Result's validationError output to be empty

        #region Input: ID = empty string | Expect: Result to be false

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByLocationQuery_WithEmptyID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByLocationQuery(string.Empty);
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.False(validationResult);
        }

        #endregion Input: ID = empty string | Expect: Result to be false

        #region Input: ID = empty string | Expect: Result's validationError output to contain the validation errors

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByLocationQuery_WithEmptyID_ShouldReturnAStringWithTheErrorsFound()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByLocationQuery(string.Empty);
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property LocationID failed validation. Error was: Location ID was empty or null!", validationErrors);
        }

        #endregion Input: ID = empty string | Expect: Result's validationError output to contain the validation errors

        #region Input: ID = string of length != 24 | Expect: Result should be false

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByLocationQuery_WithIDOfLengthNot24_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByLocationQuery("eIdL14F9");
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        #endregion Input: ID = string of length != 24 | Expect: Result should be false

        #region Input: ID = string of length != 24 | Expect: Result's validationError output to contain the validation errors

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByLocationQuery_WithIDOfLengthNot24_ShouldReturnAStringWithTheErrorsFound()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByLocationQuery("eIdL14F9");
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property LocationID failed validation. Error was: Location ID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        #endregion Input: ID = string of length != 24 | Expect: Result's validationError output to contain the validation errors

        #region Input: ID = string of length 24 with prohibited characters  | Expect: Result should be false

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByLocationQuery_WithIDThatContainsProhibitedCharacters_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByLocationQuery("6050efcd87e2647ab7ac443~");
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        #endregion Input: ID = string of length 24 with prohibited characters  | Expect: Result should be false

        #region Input: ID = string of length 24 with prohibited characters | Expect: Result's validationError output to contain the validation errors

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByLocationQuery_WithIDThatContainsProhibitedCharacters_ShouldReturnAStringWithTheErrorsFound()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByLocationQuery("6050efcd87e2647ab7ac443~");
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property LocationID failed validation. Error was: Location ID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        #endregion Input: ID = string of length 24 with prohibited characters | Expect: Result's validationError output to contain the validation errors

        #endregion Request of type DTOEducationalInstitutionByLocationQuery TESTS

        #region Request of type DTOEducationalInstitutionCreateCommand TESTS

        #region Input: Name = string of length in [2,128], Description = string of length in [2,500], BuildingID & LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result to be true

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionCreateCommand_ShouldReturnTrue()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingID = "10Fc4a7f1e00F1BDebAe4501"
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.True(validationResult);
        }

        #endregion Input: Name = string of length in [2,128], Description = string of length in [2,500], BuildingID & LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result to be true

        #region Input: Name = string of length in [2,128], Description = string of length in [2,500], BuildingID & LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result's validationError output to be empty

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionCreateCommand_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingID = "10Fc4a7f1e00F1BDebAe4501"
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        #endregion Input: Name = string of length in [2,128], Description = string of length in [2,500], BuildingID & LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result's validationError output to be empty

        #region Input: Name = empty string , Description = string of length in [2,500], BuildingID & LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result to be false

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionCreateCommand_WithEmptyName_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = string.Empty,
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingID = "10Fc4a7f1e00F1BDebAe4501"
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        #endregion Input: Name = empty string , Description = string of length in [2,500], BuildingID & LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result to be false

        #region Input: Name = empty string, Description = string of length in [2,500], BuildingID & LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result's validationError to contain the validation errors

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionCreateCommand_WithEmptyName_ShouldReturnAStringWithTheErrorsFound()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = string.Empty,
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingID = "10Fc4a7f1e00F1BDebAe4501"
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property Name failed validation. Error was: Name was empty or null!", validationErrors);
        }

        #endregion Input: Name = empty string, Description = string of length in [2,500], BuildingID & LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result's validationError to contain the validation errors

        #region Input: Name = string of length NOT in [2,128], Description = string of length in [2,500], BuildingID & LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result to be false

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionCreateCommand_WithOutOfBoundsLengthName_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "N",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingID = "10Fc4a7f1e00F1BDebAe4501"
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        #endregion Input: Name = string of length NOT in [2,128], Description = string of length in [2,500], BuildingID & LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result to be false

        #region Input: Name = string of length NOT in [2,128], Description = string of length in [2,500], BuildingID & LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result's validationError to contain the validation errors

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionCreateCommand_WithOutOfBoundsLengthName_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "N",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingID = "10Fc4a7f1e00F1BDebAe4501"
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property Name failed validation. Error was: Name's length was not between 2-128 characters!", validationErrors);
        }

        #endregion Input: Name = string of length NOT in [2,128], Description = string of length in [2,500], BuildingID & LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result's validationError to contain the validation errors

        #region Input: Name = string of length in [2,128], Description = empty string, BuildingID & LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result to be false

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionCreateCommand_WithEmptyDescription_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = string.Empty,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingID = "10Fc4a7f1e00F1BDebAe4501"
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        #endregion Input: Name = string of length in [2,128], Description = empty string, BuildingID & LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result to be false

        #region Input: Name = string of length in [2,128], Description = empty string, BuildingID & LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result's validationError to contain the validation errors

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionCreateCommand_WithEmptyDescription_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = string.Empty,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingID = "10Fc4a7f1e00F1BDebAe4501"
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property Description failed validation. Error was: Description was empty or null!", validationErrors);
        }

        #endregion Input: Name = string of length in [2,128], Description = empty string, BuildingID & LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result's validationError to contain the validation errors

        #region Input: Name = string of length in [2,128], Description = string of length NOT in [2,500], BuildingID & LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result to be false

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionCreateCommand_WithOutOfBoundsLengthDescription_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "D",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingID = "10Fc4a7f1e00F1BDebAe4501"
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        #endregion Input: Name = string of length in [2,128], Description = string of length NOT in [2,500], BuildingID & LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result to be false

        #region Input: Name = string of length in [2,128], Description = string of length NOT in [2,500], BuildingID & LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result's validationError to contain the validation errors

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionCreateCommand_WithOutOfBoundsLengthDescription_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "D",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingID = "10Fc4a7f1e00F1BDebAe4501"
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property Description failed validation. Error was: Description's length was not between 2-500 characters!", validationErrors);
        }

        #endregion Input: Name = string of length in [2,128], Description = string of length NOT in [2,500], BuildingID & LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result's validationError to contain the validation errors

        #region Input: Name = string of length in [2,128], Description = string of length in [2,500], BuildingID = empty string, LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result to be false

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionCreateCommand_WithEmptyBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingID = string.Empty
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        #endregion Input: Name = string of length in [2,128], Description = string of length in [2,500], BuildingID = empty string, LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result to be false

        #region Input: Name = string of length in [2,128], Description = string of length NOT in [2,500], BuildingID = empty string, LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result's validationError to contain the validation errors

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionCreateCommand_WithEmptyBuildingID_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingID = string.Empty
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingID failed validation. Error was: Building ID was empty or null!", validationErrors);
        }

        #endregion Input: Name = string of length in [2,128], Description = string of length NOT in [2,500], BuildingID = empty string, LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result's validationError to contain the validation errors

        #region Input: Name = string of length in [2,128], Description = string of length in [2,500], BuildingID = string NOT of length 24, LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result to be false

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionCreateCommand_WithOutOfBoundsLengthBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingID = "10Fa3A"
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        #endregion Input: Name = string of length in [2,128], Description = string of length in [2,500], BuildingID = string NOT of length 24, LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result to be false

        #region Input: Name = string of length in [2,128], Description = string of length in [2,500], BuildingID = string NOT of length 24, LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result's validationError to contain the validation errors

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionCreateCommand_WithOutOfBoundsLengthBuildingID_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingID = "10Fa3A"
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingID failed validation. Error was: Building ID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        #endregion Input: Name = string of length in [2,128], Description = string of length in [2,500], BuildingID = string NOT of length 24, LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result's validationError to contain the validation errors

        #region Input: Name = string of length in [2,128], Description = string of length in [2,500], BuildingID = string of length 24 with alphabet NOT [a-fA-F0-9], LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result to be false

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionCreateCommand_WithOutOfBoundsAlphabetBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingID = "10Fa3AQ"
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        #endregion Input: Name = string of length in [2,128], Description = string of length in [2,500], BuildingID = string of length 24 with alphabet NOT [a-fA-F0-9], LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result to be false

        #region Input: Name = string of length in [2,128], Description = string of length in [2,500], BuildingID = string of length 24 with alphabet NOT [a-fA-F0-9], LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result's validationError to contain the validation errors

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionCreateCommand_WithOutOfBoundsAlphabetBuildingID_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingID = "10Fa3AQ"
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingID failed validation. Error was: Building ID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        #endregion Input: Name = string of length in [2,128], Description = string of length in [2,500], BuildingID = string of length 24 with alphabet NOT [a-fA-F0-9], LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result's validationError to contain the validation errors

        #region Input: Name = string of length in [2,128], Description = string of length in [2,500], BuildingID == LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result to be false

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionCreateCommand_WithBuildingIDEqualsLocationID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingID = "10Fc4a7f1e00f1BDebAe4509"
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        #endregion Input: Name = string of length in [2,128], Description = string of length in [2,500], BuildingID == LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result to be false

        #region Input: Name = string of length in [2,128], Description = string of length in [2,500], BuildingID == LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result's validationError to contain the validation errors

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionCreateCommand_WithBuildingIDEqualsLocationID_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingID = "10Fc4a7f1e00f1BDebAe4509"
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingID failed validation. Error was: Building ID was the same as LocationID!", validationErrors);
        }

        #endregion Input: Name = string of length in [2,128], Description = string of length in [2,500], BuildingID == LocationID = string of length 24 with alphabet [a-fA-F0-9] | Expect: Result's validationError to contain the validation errors

        #endregion Request of type DTOEducationalInstitutionCreateCommand TESTS
    }
}