using EducationaInstitutionAPI.Business;
using EducationaInstitutionAPI.DTOs.Commands;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.In;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Commands;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
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

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionByIDQuery_ShouldReturnTrue()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByIDQuery(Guid.NewGuid());
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionByIDQuery_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByIDQuery(Guid.NewGuid());
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionByIDQuery_WithDefaultID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByIDQuery(default);
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionByIDQuery_WithDefaultID_ShouldReturnAStringWithTheErrorsFound()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByIDQuery(default);
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property EduInstitutionID failed validation. Error was: Edu Institution ID was empty or null!", validationErrors);
        }

        #endregion Request of type DTOEducationalInstitutionByIDQuery TESTS

        #region Request of type DTOEducationalInstitutionByNameQuery TESTS

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionByNameQuery_ShouldReturnTrue()
        {
            //Arrange
            var request = new DTOEducationalInstitutionsByNameQuery() { Name = "University", OffsetValue = 0, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionByNameQuery_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionsByNameQuery() { Name = "University", OffsetValue = 0, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionByNameQuery_WithNullName_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionsByNameQuery() { Name = null, OffsetValue = 0, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionByNameQuery_WithEmptyName_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionsByNameQuery() { Name = string.Empty, OffsetValue = 0, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionByNameQuery_WithEmptyName_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionsByNameQuery() { Name = string.Empty, OffsetValue = 0, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property Name failed validation. Error was: Name was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionByNameQuery_WithNullName_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionsByNameQuery() { Name = null, OffsetValue = 0, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property Name failed validation. Error was: Name was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionByNameQuery_WithNameOfLengthOne_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionsByNameQuery() { Name = "a", OffsetValue = 0, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property Name failed validation. Error was: Name's length was not between 2-128 characters!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionByNameQuery_WithNegativeOffsetValue_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionsByNameQuery() { Name = "University", OffsetValue = -1, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionByNameQuery_WithNegativeOffsetValue_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionsByNameQuery() { Name = "University", OffsetValue = -1, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property OffsetValue failed validation. Error was: Offset Value was not between 0 and 150!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionByNameQuery_WithZeroResultsCount_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionsByNameQuery() { Name = "University", OffsetValue = 0, ResultsCount = 0 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionByNameQuery_WithZeroResultsCount_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionsByNameQuery() { Name = "University", OffsetValue = 0, ResultsCount = 0 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property ResultsCount failed validation. Error was: Results Count was not between 1 and 100!", validationErrors);
        }

        #endregion Request of type DTOEducationalInstitutionByNameQuery TESTS

        #region Request of type DTOEducationalInstitutionByLocationQuery TESTS

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionByLocationQuery_ShouldReturnTrue()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByLocationQuery("6050efcd87e2647ab7ac443e");
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionByLocationQuery_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByLocationQuery("6050efcd87e2647ab7ac443e");
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionByLocationQuery_WithEmptyID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByLocationQuery(string.Empty);
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionByLocationQuery_WithEmptyID_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByLocationQuery(string.Empty);
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property LocationID failed validation. Error was: Location ID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAanInvalidRequestOfTypeDTOEducationalInstitutionByLocationQuery_WithIDOfLengthNot24_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByLocationQuery("eIdL14F9");
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionByLocationQuery_WithIDOfLengthNot24_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByLocationQuery("eIdL14F9");
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property LocationID failed validation. Error was: Location ID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionByLocationQuery_WithIDThatContainsProhibitedCharacters_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByLocationQuery("6050efcd87e2647ab7ac443~");
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionByLocationQuery_WithIDThatContainsProhibitedCharacters_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByLocationQuery("6050efcd87e2647ab7ac443~");
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property LocationID failed validation. Error was: Location ID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        #endregion Request of type DTOEducationalInstitutionByLocationQuery TESTS

        #region Request of type DTOEducationalInstitutionCreateCommand TESTS

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionCreateCommand_ShouldReturnTrue()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionCreateCommand_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionCreateCommand_WithEmptyName_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = string.Empty,
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionCreateCommand_WithEmptyName_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = string.Empty,
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property Name failed validation. Error was: Name was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionCreateCommand_WithOutOfBoundsLengthName_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "N",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionCreateCommand_WithOutOfBoundsLengthName_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "N",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property Name failed validation. Error was: Name's length was not between 2-128 characters!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionCreateCommand_WithEmptyDescription_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = string.Empty,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionCreateCommand_WithEmptyDescription_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = string.Empty,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property Description failed validation. Error was: Description was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionCreateCommand_WithOutOfBoundsLengthDescription_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "D",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionCreateCommand_WithOutOfBoundsLengthDescription_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "D",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property Description failed validation. Error was: Description's length was not between 2-500 characters!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionCreateCommand_WithEmptyBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { string.Empty }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionCreateCommand_WithEmptyBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { string.Empty }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingsIDs[0] failed validation. Error was: BuildingID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionCreateCommand_WithOutOfBoundsLengthBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fa3A" }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionCreateCommand_WithOutOfBoundsLengthBuildingID_ShouldReturnValdationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fa3A" }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingsIDs[0] failed validation. Error was: BuildingID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionCreateCommand_WithOutOfBoundsAlphabetBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fa3AQ" }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionCreateCommand_WithOutOfBoundsAlphabetBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fa3AQ" }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingsIDs[0] failed validation. Error was: BuildingID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionCreateCommand_WithBuildingIDEqualsLocationID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00f1BDebAe4509" }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionCreateCommand_WithBuildingIDEqualsLocationID_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00f1BDebAe4509" }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingsIDs[0] failed validation. Error was: BuildingID was the same as LocationID!", validationErrors);
        }

        #endregion Request of type DTOEducationalInstitutionCreateCommand TESTS

        #region Request of type DTOEducationalInstitutionWithParentCreateCommand TESTS

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionWithParentCreateCommand_ShouldReturnTrue()
        {
            //Arrange
            var request = new DTOEducationalInstitutionWithParentCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                ParentInstitutionID = Guid.NewGuid()
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionWithParentCreateCommand_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionWithParentCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                ParentInstitutionID = Guid.NewGuid()
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        //->
        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionWithParentCreateCommand_WithEmptyName_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionWithParentCreateCommand()
            {
                Name = string.Empty,
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                ParentInstitutionID = Guid.NewGuid()
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionWithParentCreateCommand_WithEmptyName_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionWithParentCreateCommand()
            {
                Name = string.Empty,
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                ParentInstitutionID = Guid.NewGuid()
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property Name failed validation. Error was: Name was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionWithParentCreateCommand_WithOutOfBoundsLengthName_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionWithParentCreateCommand()
            {
                Name = "N",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                ParentInstitutionID = Guid.NewGuid()
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionWithParentCreateCommand_WithOutOfBoundsLengthName_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionWithParentCreateCommand()
            {
                Name = "N",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                ParentInstitutionID = Guid.NewGuid()
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property Name failed validation. Error was: Name's length was not between 2-128 characters!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionWithParentCreateCommand_WithEmptyDescription_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionWithParentCreateCommand()
            {
                Name = "Name",
                Description = string.Empty,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                ParentInstitutionID = Guid.NewGuid()
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionWithParentCreateCommand_WithEmptyDescription_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionWithParentCreateCommand()
            {
                Name = "Name",
                Description = string.Empty,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                ParentInstitutionID = Guid.NewGuid()
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property Description failed validation. Error was: Description was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionWithParentCreateCommand_WithOutOfBoundsLengthDescription_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionWithParentCreateCommand()
            {
                Name = "Name",
                Description = "D",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                ParentInstitutionID = Guid.NewGuid()
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionWithParentCreateCommand_WithOutOfBoundsLengthDescription_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionWithParentCreateCommand()
            {
                Name = "Name",
                Description = "D",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                ParentInstitutionID = Guid.NewGuid()
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property Description failed validation. Error was: Description's length was not between 2-500 characters!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionWithParentCreateCommand_WithEmptyBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionWithParentCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { string.Empty },
                ParentInstitutionID = Guid.NewGuid()
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionWithParentCreateCommand_WithEmptyBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionWithParentCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { string.Empty },
                ParentInstitutionID = Guid.NewGuid()
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingsIDs[0] failed validation. Error was: BuildingID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionWithParentCreateCommand_WithOutOfBoundsLengthBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionWithParentCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fa3A" },
                ParentInstitutionID = Guid.NewGuid()
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionWithParentCreateCommand_WithOutOfBoundsLengthBuildingID_ShouldReturnValdationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionWithParentCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fa3A" },
                ParentInstitutionID = Guid.NewGuid()
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingsIDs[0] failed validation. Error was: BuildingID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionWithParentCreateCommand_WithOutOfBoundsAlphabetBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionWithParentCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fa3AQ" },
                ParentInstitutionID = Guid.NewGuid()
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionWithParentCreateCommand_WithOutOfBoundsAlphabetBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionWithParentCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fa3AQ" },
                ParentInstitutionID = Guid.NewGuid()
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingsIDs[0] failed validation. Error was: BuildingID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionWithParentCreateCommand_WithBuildingIDEqualsLocationID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionWithParentCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00f1BDebAe4509" },
                ParentInstitutionID = Guid.NewGuid()
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionWithParentCreateCommand_WithBuildingIDEqualsLocationID_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionWithParentCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00f1BDebAe4509" },
                ParentInstitutionID = Guid.NewGuid()
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingsIDs[0] failed validation. Error was: BuildingID was the same as LocationID!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionWithParentCreateCommand_WithDefaultParentInstitutionID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionWithParentCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00f1BDebAe4503" },
                ParentInstitutionID = default
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionWithParentCreateCommand_WithDefaultParentInstitutionID_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionWithParentCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00f1BDebAe4503" },
                ParentInstitutionID = default
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property ParentInstitutionID failed validation. Error was: Parent Institution ID was empty or null!", validationErrors);
        }

        #endregion Request of type DTOEducationalInstitutionWithParentCreateCommand TESTS

        #region Request of type DTOEducationalInstitutionUpdateCommand TESTS

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionUpdateCommand_ShouldReturnTrue()
        {
            //Arrange
            var request = new DTOEducationalInstitutionUpdateCommand()
            {
                EduInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = true,
                Description = "New_Description",
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionUpdateCommand_WithFalseUpdateNameAndEmptyName_ShouldReturnTrue()
        {
            //Arrange
            var request = new DTOEducationalInstitutionUpdateCommand()
            {
                EduInstitutionID = Guid.NewGuid(),
                UpdateName = false,
                Name = string.Empty,
                UpdateDescription = true,
                Description = "New_Description",
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionUpdateCommand_WithFalseUpdateDescriptionAndEmptyDescription_ShouldReturnTrue()
        {
            //Arrange
            var request = new DTOEducationalInstitutionUpdateCommand()
            {
                EduInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = false,
                Description = string.Empty,
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionUpdateCommand_WithFalseUpdateDescriptionAndFalseUpdateName_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionUpdateCommand()
            {
                EduInstitutionID = Guid.NewGuid(),
                UpdateName = false,
                Name = string.Empty,
                UpdateDescription = false,
                Description = string.Empty,
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionUpdateCommand_WithFalseUpdateDescriptionAndFalseUpdateName_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionUpdateCommand()
            {
                EduInstitutionID = Guid.NewGuid(),
                UpdateName = false,
                Name = string.Empty,
                UpdateDescription = false,
                Description = string.Empty,
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property UpdateDescription failed validation. Error was: Both update fields are set to false!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionUpdateCommand_WithDefaultEducationalInstituionID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionUpdateCommand()
            {
                EduInstitutionID = default,
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = true,
                Description = "New_Description",
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionUpdateCommand_WithDefaultEducationalInstituionID_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionUpdateCommand()
            {
                EduInstitutionID = default,
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = true,
                Description = "New_Description",
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property EduInstitutionID failed validation. Error was: Edu Institution ID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionUpdateCommand_WithEmptyName_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionUpdateCommand()
            {
                EduInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = string.Empty,
                UpdateDescription = true,
                Description = "New_Description",
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionUpdateCommand_WithEmptyName_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionUpdateCommand()
            {
                EduInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = string.Empty,
                UpdateDescription = true,
                Description = "New_Description",
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property Name failed validation. Error was: Name was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionUpdateCommand_WithOutOfBoundsNameLength_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionUpdateCommand()
            {
                EduInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "N",
                UpdateDescription = true,
                Description = "New_Description",
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionUpdateCommand_WithOutOfBoundsNameLength_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionUpdateCommand()
            {
                EduInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "N",
                UpdateDescription = true,
                Description = "New_Description",
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property Name failed validation. Error was: Name's length was not between 2-128 characters!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionUpdateCommand_WithEmptyDescription_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionUpdateCommand()
            {
                EduInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = true,
                Description = string.Empty,
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionUpdateCommand_WithEmptyDescription_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionUpdateCommand()
            {
                EduInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = true,
                Description = string.Empty,
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property Description failed validation. Error was: Description was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionUpdateCommand_WithOutOfBoundsDescriptionLength_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionUpdateCommand()
            {
                EduInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = true,
                Description = "N",
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionUpdateCommand_WithOutOfBoundsDescriptionLength_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionUpdateCommand()
            {
                EduInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = true,
                Description = "N",
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property Description failed validation. Error was: Description's length was not between 2-500 characters!", validationErrors);
        }

        #endregion Request of type DTOEducationalInstitutionUpdateCommand TESTS

        #region Request of type DTOEducationalInstitutionLocationUpdateCommand TESTS

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_ShouldReturnTrue()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EduInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithDefaultEduInstitutionID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EduInstitutionID = default,
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                BuildingsIDs = new List<string>() { "10Fc4a7f1E00f1BDebAe4509" }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithDefaultEduInstitutionID_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EduInstitutionID = default,
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                BuildingsIDs = new List<string>() { "10Fc4a7f1E00f1BDebAe4509" }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property EduInstitutionID failed validation. Error was: Edu Institution ID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithEmptyBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EduInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                BuildingsIDs = new List<string>() { string.Empty }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithEmptyBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EduInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                BuildingsIDs = new List<string>() { string.Empty }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingsIDs[0] failed validation. Error was: BuildingID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithEmptyLocationID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EduInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = string.Empty,
                UpdateBuildings = true,
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00f1BDebAe4503" }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithEmptyLocationID_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EduInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = string.Empty,
                UpdateBuildings = true,
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00f1BDebAe4503" }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property LocationID failed validation. Error was: Location ID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithEmptyBuildings_ShouldReturnTrue()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EduInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = false,
                BuildingsIDs = default
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.True(validationResult);
        }

        /* ---THROWS Index was out of range. Must be non-negative and less than the size of the collection. (Parameter 'index')---
         * ---WHEN UpdateLocation is false---
        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithEmptyLocationID_ShouldReturnTrue()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EduInstitutionID = Guid.NewGuid(),
                UpdateLocation = false,
                LocationID = string.Empty,
                UpdateBuildings = true,
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00f1BDebAe4509" }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.True(validationResult);
        }*/

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithDuplicateBuildingsIDs_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EduInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00f1BDebAe4509", "10Fc4a7f1e00f1BDebAe4509" }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithDuplicateBuildingsIDs_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EduInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00f1BDebAe4509", "10Fc4a7f1e00f1BDebAe4509" }
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingsIDs failed validation. Error was: BuildingsIDs can't contain duplicates!", validationErrors);
        }

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithFalseUpdateLocationAndUpdateBuildings_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EduInstitutionID = Guid.NewGuid(),
                UpdateLocation = false,
                LocationID = string.Empty,
                UpdateBuildings = false,
                BuildingsIDs = default
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithFalseUpdateLocationAndUpdateBuildings_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EduInstitutionID = Guid.NewGuid(),
                UpdateLocation = false,
                LocationID = string.Empty,
                UpdateBuildings = false,
                BuildingsIDs = default
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property UpdateBuildings failed validation. Error was: Both location and buildings update fields are false!", validationErrors);
        }

        #endregion Request of type DTOEducationalInstitutionLocationUpdateCommand TESTS

        #region Request of Type DTOEducationalInstiutionDeleteCommand TESTS

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionDeleteCommand_ShouldReturnTrue()
        {
            //Arrange
            var request = new DTOEducationalInstitutionDeleteCommand() { EduInstitutionID = Guid.NewGuid() };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionDeleteCommand_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionDeleteCommand() { EduInstitutionID = Guid.NewGuid() };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionDeleteCommand_WithDefaultID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionDeleteCommand() { EduInstitutionID = default };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionDeleteCommand_WithDefaultID_ShouldReturnAStringWithTheErrorsFound()
        {
            //Arrange
            var request = new DTOEducationalInstitutionDeleteCommand() { EduInstitutionID = default };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property EduInstitutionID failed validation. Error was: Edu Institution ID was empty or null!", validationErrors);
        }

        #endregion Request of Type DTOEducationalInstiutionDeleteCommand TESTS
    }
}