using EducationalInstitutionAPI.Business.Validation_Handler;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.DTOs.Queries;
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
        private readonly IValidationHandler validationHandler;

        public ValidationHandlerTests()
        {
            mockLogger = new();
            validationHandler = new ValidationHandler(mockLogger.Object);
        }

        #region Request of type DTOEducationalInstitutionByIDQuery TESTS

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionByIDQuery_ShouldReturnTrue()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByIDQuery() { EducationalInstitutionID = Guid.NewGuid() };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionByIDQuery_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByIDQuery() { EducationalInstitutionID = Guid.NewGuid() };
            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionByIDQuery_WithDefaultEducationalInstitutionID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByIDQuery() { EducationalInstitutionID = default };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionByIDQuery_WithDefaultEducationalInstitutionID_ShouldReturnAStringWithTheErrorsFound()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByIDQuery() { EducationalInstitutionID = default };

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property EducationalInstitutionID failed validation. Error was: Educational Institution ID was empty or null!", validationErrors);
        }

        #endregion Request of type DTOEducationalInstitutionByIDQuery TESTS

        #region Request of type DTOEducationalInstitutionByNameQuery TESTS

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionByNameQuery_ShouldReturnTrue()
        {
            //Arrange
            var request = new DTOEducationalInstitutionsByNameQuery() { Name = "University", OffsetValue = 0, ResultsCount = 1 };

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
            var request = new DTOEducationalInstitutionByLocationQuery() { LocationID = "6050efcd87e2647ab7ac443e" };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionByLocationQuery_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByLocationQuery() { LocationID = "6050efcd87e2647ab7ac443e" };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionByLocationQuery_WithEmptyLocationID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByLocationQuery() { LocationID = string.Empty };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionByLocationQuery_WithEmptyLocationID_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByLocationQuery() { LocationID = string.Empty };

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property LocationID failed validation. Error was: Location ID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAanInvalidRequestOfTypeDTOEducationalInstitutionByLocationQuery_WithLocationIDOfLengthNot24_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByLocationQuery() { LocationID = "eIdL14F9" };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionByLocationQuery_WithLocationIDOfLengthNot24_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByLocationQuery() { LocationID = "eIdL14F9" };

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property LocationID failed validation. Error was: Location ID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionByLocationQuery_WithLocationIDThatContainsProhibitedCharacters_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByLocationQuery() { LocationID = "6050efcd87e2647ab7ac443~" };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionByLocationQuery_WithLocationIDThatContainsProhibitedCharacters_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionByLocationQuery() { LocationID = "6050efcd87e2647ab7ac443~" };

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

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionCreateCommand_WithOutOfBoundsLengthBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionCreateCommand()
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fa3A" }
            };

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

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionWithParentCreateCommand_WithOutOfBoundsLengthBuildingID_ShouldReturnValidationErrors()
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
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = true,
                Description = "New_Description",
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionUpdateCommand_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = true,
                Description = "New_Description",
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionUpdateCommand_WithFalseUpdateNameAndEmptyName_ShouldReturnTrue()
        {
            //Arrange
            var request = new DTOEducationalInstitutionUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = false,
                Name = string.Empty,
                UpdateDescription = true,
                Description = "New_Description",
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionUpdateCommand_WithFalseUpdateNameAndEmptyName_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = false,
                Name = string.Empty,
                UpdateDescription = true,
                Description = "New_Description",
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionUpdateCommand_WithFalseUpdateDescriptionAndEmptyDescription_ShouldReturnTrue()
        {
            //Arrange
            var request = new DTOEducationalInstitutionUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
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
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionUpdateCommand_WithFalseUpdateDescriptionAndEmptyDescription_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = false,
                Description = string.Empty,
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionUpdateCommand_WithFalseUpdateDescriptionAndFalseUpdateName_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = false,
                Name = string.Empty,
                UpdateDescription = false,
                Description = string.Empty,
            };

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
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = false,
                Name = string.Empty,
                UpdateDescription = false,
                Description = string.Empty,
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property UpdateDescription failed validation. Error was: Both update fields are set to false!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionUpdateCommand_WithDefaultEducationalInstitutionID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionUpdateCommand()
            {
                EducationalInstitutionID = default,
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = true,
                Description = "New_Description",
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionUpdateCommand_WithDefaultEducationalInstitutionID_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionUpdateCommand()
            {
                EducationalInstitutionID = default,
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = true,
                Description = "New_Description",
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property EducationalInstitutionID failed validation. Error was: Educational Institution ID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionUpdateCommand_WithEmptyName_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = string.Empty,
                UpdateDescription = true,
                Description = "New_Description",
            };

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
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = string.Empty,
                UpdateDescription = true,
                Description = "New_Description",
            };

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
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "N",
                UpdateDescription = true,
                Description = "New_Description",
            };

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
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "N",
                UpdateDescription = true,
                Description = "New_Description",
            };

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
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = true,
                Description = string.Empty,
            };

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
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = true,
                Description = string.Empty,
            };

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
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = true,
                Description = "N",
            };

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
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = true,
                Description = "N",
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property Description failed validation. Error was: Description's length was not between 2-500 characters!", validationErrors);
        }

        #endregion Request of type DTOEducationalInstitutionUpdateCommand TESTS

        #region Request of type DTOEducationalInstitutionParentUpdateCommand TESTS

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionParentUpdateCommand_ShouldReturnTrue()
        {
            //Arrange
            var request = new DTOEducationalInstitutionParentUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var result = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionParentUpdateCommand_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionParentUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var result = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionParentUpdateCommand_WithDefaultEducationalInstitutionID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionParentUpdateCommand()
            {
                EducationalInstitutionID = default,
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var result = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionParentUpdateCommand_WithDefaultEducationalInstitutionID_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionParentUpdateCommand()
            {
                EducationalInstitutionID = default,
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var result = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property EducationalInstitutionID failed validation. Error was: Educational Institution ID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionParentUpdateCommand_WithDefaultParentInstitutionID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionParentUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                ParentInstitutionID = default
            };

            //Act
            var result = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionParentUpdateCommand_WithDefaultParentInstitutionID_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionParentUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                ParentInstitutionID = default
            };

            //Act
            var result = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property ParentInstitutionID failed validation. Error was: Parent Institution ID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionParentUpdateCommand_WithParentInstitutionIDSameAsEducationalInstitutionID_ShouldReturnFalse()
        {
            //Arrange
            var id = Guid.NewGuid();
            var request = new DTOEducationalInstitutionParentUpdateCommand()
            {
                EducationalInstitutionID = id,
                ParentInstitutionID = id
            };

            //Act
            var result = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionParentUpdateCommand_WithParentInstitutionIDSameAsEducationalInstitutionID_ShouldReturnValidationErrors()
        {
            //Arrange
            var id = Guid.NewGuid();
            var request = new DTOEducationalInstitutionParentUpdateCommand()
            {
                EducationalInstitutionID = id,
                ParentInstitutionID = id
            };

            //Act
            var result = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property ParentInstitutionID failed validation. Error was: Parent Institution ID was the same as Educational Institution ID!", validationErrors);
        }

        #endregion Request of type DTOEducationalInstitutionParentUpdateCommand TESTS

        #region Request of type DTOEducationalInstitutionLocationUpdateCommand TESTS

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_ShouldReturnTrue()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithDefaultEducationalInstitutionID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EducationalInstitutionID = default,
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { "10Fc4a7f1E00f1BDebAe4509" }
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithDefaultEducationalInstitutionID_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EducationalInstitutionID = default,
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { "10Fc4a7f1E00f1BDebAe4509" }
            };

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property EducationalInstitutionID failed validation. Error was: Educational Institution ID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithEmptyBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { string.Empty }
            };

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
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { string.Empty }
            };

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property AddBuildingsIDs[0] failed validation. Error was: BuildingID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithEmptyLocationID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = string.Empty,
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { "10Fc4a7f1e00f1BDebAe4503" }
            };

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
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = string.Empty,
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { "10Fc4a7f1e00f1BDebAe4503" }
            };

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
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = false,
                AddBuildingsIDs = default
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithEmptyBuildings_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = false,
                AddBuildingsIDs = default
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
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
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { "10Fc4a7f1e00f1BDebAe4509", "10Fc4a7f1e00f1BDebAe4509" }
            };

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
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { "10Fc4a7f1e00f1BDebAe4509", "10Fc4a7f1e00f1BDebAe4509" }
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property AddBuildingsIDs failed validation. Error was: AddBuildingsIDs can't contain duplicates!", validationErrors);
        }

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithFalseUpdateLocationAndUpdateBuildings_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = false,
                LocationID = string.Empty,
                UpdateBuildings = false,
                AddBuildingsIDs = default
            };

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
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = false,
                LocationID = string.Empty,
                UpdateBuildings = false,
                AddBuildingsIDs = default
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property UpdateBuildings failed validation. Error was: Both location and buildings update fields are false!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithDuplicateBuildingsIDs_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { "10Fc4a7f1E00f1BDebAe4509", "10Fc4a7f1E00f1BDebAe4509" }
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithDuplicateBuildingsIDs_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { "10Fc4a7f1E00f1BDebAe4509", "10Fc4a7f1E00f1BDebAe4509" }
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property AddBuildingsIDs failed validation. Error was: AddBuildingsIDs can't contain duplicates!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithBothAddBuiildingsIDsAndRemoveBuildingsIDsEmpty_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>(0),
                RemoveBuildingsIDs = new List<string>(0)
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithBothAddBuiildingsIDsAndRemoveBuildingsIDsEmpty_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>(0),
                RemoveBuildingsIDs = new List<string>(0)
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property AddBuildingsIDs failed validation. Error was: Both AddBuildingsIDs and RemoveBuildingsIDs collections are empty!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithEmptyRemoveBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                RemoveBuildingsIDs = new List<string>() { string.Empty }
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithEmptyRemoveBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                RemoveBuildingsIDs = new List<string>() { string.Empty }
            };

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property RemoveBuildingsIDs[0] failed validation. Error was: BuildingID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithOutOfBoundsCharactersInRemoveBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>(),
                RemoveBuildingsIDs = new List<string>() { "Q10Fc47f1e00f1BDebAe4509" }
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithOutOfBoundsCharactersInRemoveBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>(),
                RemoveBuildingsIDs = new List<string>() { "Q10Fc47f1e00f1BDebAe4509" }
            };

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property RemoveBuildingsIDs[0] failed validation. Error was: BuildingID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithOutOfBoundsCharactersInAddBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                RemoveBuildingsIDs = new List<string>(),
                AddBuildingsIDs = new List<string>() { "Q10Fc47f1e00f1BDebAe4509" }
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithOutOfBoundsCharactersInAddBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                RemoveBuildingsIDs = new List<string>(),
                AddBuildingsIDs = new List<string>() { "Q10Fc47f1e00f1BDebAe4509" }
            };

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property AddBuildingsIDs[0] failed validation. Error was: BuildingID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithLengthGreaterThan24InRemoveBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>(),
                RemoveBuildingsIDs = new List<string>() { "a10Fc47f1e00f1BDebAe4509A" }
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithLengthGreaterThan24InRemoveBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>(),
                RemoveBuildingsIDs = new List<string>() { "a10Fc47f1e00f1BDebAe4509A" }
            };

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property RemoveBuildingsIDs[0] failed validation. Error was: BuildingID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithLengthGreaterThan24InAddBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                RemoveBuildingsIDs = new List<string>(),
                AddBuildingsIDs = new List<string>() { "AA10Fc47f1e00f1BDebAe4509" }
            };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionLocationUpdateCommand_WithLengthGreaterThan24InAddBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var request = new DTOEducationalInstitutionLocationUpdateCommand()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                RemoveBuildingsIDs = new List<string>(),
                AddBuildingsIDs = new List<string>() { "a10Fc47f1e00f1BDebAe4509AE" }
            };

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property AddBuildingsIDs[0] failed validation. Error was: BuildingID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        #endregion Request of type DTOEducationalInstitutionLocationUpdateCommand TESTS

        #region Request of Type DTOEducationalInstiutionDeleteCommand TESTS

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionDeleteCommand_ShouldReturnTrue()
        {
            //Arrange
            var request = new DTOEducationalInstitutionDeleteCommand() { EducationalInstitutionID = Guid.NewGuid() };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidRequestOfTypeDTOEducationalInstitutionDeleteCommand_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var request = new DTOEducationalInstitutionDeleteCommand() { EducationalInstitutionID = Guid.NewGuid() };

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionDeleteCommand_WithDefaultEducationalInstitutionID_ShouldReturnFalse()
        {
            //Arrange
            var request = new DTOEducationalInstitutionDeleteCommand() { EducationalInstitutionID = default };

            //Act
            var validationResult = validationHandler.IsRequestValid(request, out _);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidRequestOfTypeDTOEducationalInstitutionDeleteCommand_WithDefaultEducationalInstitutionID_ShouldReturnAStringWithTheErrorsFound()
        {
            //Arrange
            var request = new DTOEducationalInstitutionDeleteCommand() { EducationalInstitutionID = default };

            //Act
            validationHandler.IsRequestValid(request, out string validationErrors);

            //Assert
            Assert.Equal(" Property EducationalInstitutionID failed validation. Error was: Educational Institution ID was empty or null!", validationErrors);
        }

        #endregion Request of Type DTOEducationalInstiutionDeleteCommand TESTS
    }
}