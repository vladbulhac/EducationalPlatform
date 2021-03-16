using EducationaInstitutionAPI.Business;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.In;
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
        #endregion

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
        #endregion

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
        #endregion

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
            Assert.Equal(" Property EduInstitutionID failed validation. Error was: Edu Institution ID must not be empty and be of type GUID: https://docs.microsoft.com/en-us/dotnet/api/system.guid", validationErrors);
        }
        #endregion

        #endregion

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
        #endregion

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
        #endregion

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
        #endregion

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
        #endregion

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
            Assert.Equal(" Property Name failed validation. Error was: Property Name cannot be empty or null!", validationErrors);
        }
        #endregion

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
            Assert.Equal(" Property Name failed validation. Error was: Property Name cannot be empty or null!", validationErrors);
        }
        #endregion

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
            Assert.Equal(" Property Name failed validation. Error was: Property Name's length must be between 2-128 characters!", validationErrors);
        }
        #endregion

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
        #endregion

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
            Assert.Equal(" Property OffsetValue failed validation. Error was: Property Offset Value must be between 0 and 150!", validationErrors);
        }
        #endregion

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
        #endregion

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
            Assert.Equal(" Property ResultsCount failed validation. Error was: Property Results Count must be between 1 and 100!", validationErrors);
        }
        #endregion

        #endregion

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
        #endregion

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
        #endregion

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
        #endregion

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
            Assert.Equal(" Property LocationID failed validation. Error was: Location ID must not be empty and be of type string!", validationErrors);
        }
        #endregion

        #region Input: ID = string of length != 24 | Expect: Result should be false
        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByLocationQuery_WithIDOfLengthNot24_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByLocationQuery("eIdL14F9");
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult=validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }
        #endregion

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
            Assert.Equal(" Property LocationID failed validation. Error was: Location ID contains characters that are not supported and must be exactly of length 24!", validationErrors);
        }
        #endregion

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
        #endregion

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
            Assert.Equal(" Property LocationID failed validation. Error was: Location ID contains characters that are not supported and must be exactly of length 24!", validationErrors);
        }
        #endregion
        #endregion
    }
}