using EducationalInstitutionAPI.Business.Validation_Handler;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.DTOs.Queries;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace EducationalInstitution.API.IntegrationTests.BusinessTests
{
    public class ValidationHandlerTests
    {
        private readonly Mock<ILogger<ValidationHandler>> mockLogger;
        private readonly IValidationHandler validationHandler;

        /// <remarks>Called before each test</remarks>
        public ValidationHandlerTests()
        {
            mockLogger = new();
            validationHandler = new ValidationHandler(mockLogger.Object);
        }

        #region DTOEducationalInstitutionByIDQuery TESTS

        [Fact]
        public void GivenAValidDTOEducationalInstitutionByIDQuery_ShouldReturnTrue()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionByIDQuery { EducationalInstitutionID = Guid.NewGuid() };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionByIDQuery_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionByIDQuery { EducationalInstitutionID = Guid.NewGuid() };
            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionByIDQuery_WithDefaultEducationalInstitutionID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionByIDQuery { EducationalInstitutionID = default };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionByIDQuery_WithDefaultEducationalInstitutionID_ShouldReturnAStringWithTheErrorsFound()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionByIDQuery { EducationalInstitutionID = default };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property EducationalInstitutionID failed validation. Error was: Educational Institution ID was empty or null!", validationErrors);
        }

        #endregion DTOEducationalInstitutionByIDQuery TESTS

        #region DTOEducationalInstitutionByNameQuery TESTS

        [Fact]
        public void GivenAValidDTOEducationalInstitutionByNameQuery_ShouldReturnTrue()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByNameQuery { Name = "University", OffsetValue = 0, ResultsCount = 1 };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionByNameQuery_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByNameQuery { Name = "University", OffsetValue = 0, ResultsCount = 1 };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionByNameQuery_WithNullName_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByNameQuery { Name = null, OffsetValue = 0, ResultsCount = 1 };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionByNameQuery_WithEmptyName_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByNameQuery { Name = string.Empty, OffsetValue = 0, ResultsCount = 1 };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionByNameQuery_WithEmptyName_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByNameQuery { Name = string.Empty, OffsetValue = 0, ResultsCount = 1 };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property Name failed validation. Error was: Name was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionByNameQuery_WithNullName_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByNameQuery { Name = null, OffsetValue = 0, ResultsCount = 1 };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property Name failed validation. Error was: Name was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionByNameQuery_WithNameOfLengthOne_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByNameQuery { Name = "a", OffsetValue = 0, ResultsCount = 1 };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property Name failed validation. Error was: Name's length was not between 2-128 characters!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionByNameQuery_WithNegativeOffsetValue_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByNameQuery { Name = "University", OffsetValue = -1, ResultsCount = 1 };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionByNameQuery_WithNegativeOffsetValue_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByNameQuery { Name = "University", OffsetValue = -1, ResultsCount = 1 };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property OffsetValue failed validation. Error was: Offset Value was not between 0 and 150!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionByNameQuery_WithZeroResultsCount_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByNameQuery { Name = "University", OffsetValue = 0, ResultsCount = 0 };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionByNameQuery_WithZeroResultsCount_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByNameQuery { Name = "University", OffsetValue = 0, ResultsCount = 0 };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property ResultsCount failed validation. Error was: Results Count was not between 1 and 100!", validationErrors);
        }

        #endregion DTOEducationalInstitutionByNameQuery TESTS

        #region DTOEducationalInstitutionByLocationQuery TESTS

        [Fact]
        public void GivenAValidDTOEducationalInstitutionByLocationQuery_ShouldReturnTrue()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByLocationQuery { LocationID = "6050efcd87e2647ab7ac443e" };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionByLocationQuery_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByLocationQuery { LocationID = "6050efcd87e2647ab7ac443e" };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionByLocationQuery_WithEmptyLocationID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByLocationQuery { LocationID = string.Empty };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionByLocationQuery_WithEmptyLocationID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByLocationQuery { LocationID = string.Empty };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property LocationID failed validation. Error was: Location ID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAanInvalidDTOEducationalInstitutionByLocationQuery_WithLocationIDOfLengthNot24_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByLocationQuery { LocationID = "eIdL14F9" };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionByLocationQuery_WithLocationIDOfLengthNot24_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByLocationQuery { LocationID = "eIdL14F9" };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property LocationID failed validation. Error was: Location ID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionByLocationQuery_WithLocationIDThatContainsProhibitedCharacters_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByLocationQuery { LocationID = "6050efcd87e2647ab7ac443~" };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionByLocationQuery_WithLocationIDThatContainsProhibitedCharacters_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByLocationQuery { LocationID = "6050efcd87e2647ab7ac443~" };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property LocationID failed validation. Error was: Location ID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        #endregion DTOEducationalInstitutionByLocationQuery TESTS

        #region DTOAdminsByEducationalInstitutionIDQuery TESTS

        [Fact]
        public void GivenAValidDTOAdminsByEducationalInstitutionIDQuery_ShouldReturnTrue()
        {
            //Arrange
            var dto = new DTOAdminsByEducationalInstitutionIDQuery { EducationalInstitutionID = Guid.NewGuid() };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidDTOAdminsByEducationalInstitutionIDQuery_ShouldReturnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new DTOAdminsByEducationalInstitutionIDQuery { EducationalInstitutionID = Guid.NewGuid() };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOAdminsByEducationalInstitutionIDQuery_WithDefaultID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOAdminsByEducationalInstitutionIDQuery { EducationalInstitutionID = default };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOAdminsByEducationalInstitutionIDQuery_WithDefaultID_ShouldReturnAStringWithValidationError()
        {
            //Arrange
            var dto = new DTOAdminsByEducationalInstitutionIDQuery { EducationalInstitutionID = default };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property EducationalInstitutionID failed validation. Error was: EducationalInstitutionID is empty, null or default!", validationErrors);
        }

        #endregion DTOAdminsByEducationalInstitutionIDQuery TESTS

        #region DTOEducationalInstitutionsByBuildingQuery TEST

        [Fact]
        public void GivenAValidDTOEducationalInstitutionsByBuildingQuery_ShouldReturnTrue()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByBuildingQuery { BuildingID = "12AEDa09344151BbdDefF134" };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionsByBuildingQuery_ShouldReturnEmptyValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByBuildingQuery { BuildingID = "12AEDa09344151BbdDefF134" };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionsByBuildingQuery_WithEmptyBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByBuildingQuery { BuildingID = string.Empty };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionsByBuildingQuery_WithEmptyBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByBuildingQuery { BuildingID = string.Empty };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingID failed validation. Error was: Building ID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionsByBuildingQuery_WithBuildingIDLengthNot24_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByBuildingQuery { BuildingID = "a1234b" };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionsByBuildingQuery_WithBuildingIDLengthNot24_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByBuildingQuery { BuildingID = "a1234b" };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingID failed validation. Error was: Building ID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionsByBuildingQuery_WithBuildingIDContainingCharactersOutOfTheAlphabet_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByBuildingQuery { BuildingID = "12AEDa09344151BbdDefF13Q" };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionsByBuildingQuery_WithBuildingIDContainingCharactersOutOfTheAlphabet_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionsByBuildingQuery { BuildingID = "12AEDa09344151BbdDefF13Q" };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingID failed validation. Error was: Building ID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        #endregion DTOEducationalInstitutionsByBuildingQuery TEST

        #region DTOEducationalInstitutionCreateCommand TESTS

        [Fact]
        public void GivenAValidDTOEducationalInstitutionCreateCommand_ShouldReturnTrue()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionCreateCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<Guid>() { Guid.NewGuid() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionCreateCommand_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionCreateCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<Guid>() { Guid.NewGuid() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionCreateCommand_WithEmptyName_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionCreateCommand
            {
                Name = string.Empty,
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<Guid>() { Guid.NewGuid() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionCreateCommand_WithEmptyName_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionCreateCommand
            {
                Name = string.Empty,
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<Guid>() { Guid.NewGuid() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property Name failed validation. Error was: Name was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionCreateCommand_WithOutOfBoundsLengthName_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionCreateCommand
            {
                Name = "N",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<Guid>() { Guid.NewGuid() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionCreateCommand_WithOutOfBoundsLengthName_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionCreateCommand
            {
                Name = "N",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<Guid>() { Guid.NewGuid() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property Name failed validation. Error was: Name's length was not between 2-128 characters!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionCreateCommand_WithEmptyDescription_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionCreateCommand
            {
                Name = "Name",
                Description = string.Empty,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<Guid>() { Guid.NewGuid() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionCreateCommand_WithEmptyDescription_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionCreateCommand
            {
                Name = "Name",
                Description = string.Empty,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<Guid>() { Guid.NewGuid() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property Description failed validation. Error was: Description was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionCreateCommand_WithOutOfBoundsLengthDescription_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionCreateCommand
            {
                Name = "Name",
                Description = "D",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<Guid>() { Guid.NewGuid() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionCreateCommand_WithOutOfBoundsLengthDescription_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionCreateCommand
            {
                Name = "Name",
                Description = "D",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<Guid>() { Guid.NewGuid() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property Description failed validation. Error was: Description's length was not between 2-500 characters!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionCreateCommand_WithEmptyBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionCreateCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { string.Empty },
                AdminsIDs = new List<Guid>() { Guid.NewGuid() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionCreateCommand_WithEmptyBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionCreateCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { string.Empty },
                AdminsIDs = new List<Guid>() { Guid.NewGuid() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingsIDs[0] failed validation. Error was: BuildingID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionCreateCommand_WithOutOfBoundsLengthBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionCreateCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fa3A" },
                AdminsIDs = new List<Guid>() { Guid.NewGuid() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionCreateCommand_WithOutOfBoundsLengthBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionCreateCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fa3A" },
                AdminsIDs = new List<Guid>() { Guid.NewGuid() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingsIDs[0] failed validation. Error was: BuildingID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionCreateCommand_WithOutOfBoundsAlphabetBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionCreateCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fa3AQ" },
                AdminsIDs = new List<Guid>() { Guid.NewGuid() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionCreateCommand_WithOutOfBoundsAlphabetBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionCreateCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fa3AQ" },
                AdminsIDs = new List<Guid>() { Guid.NewGuid() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingsIDs[0] failed validation. Error was: BuildingID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionCreateCommand_WithBuildingIDEqualsLocationID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionCreateCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00f1BDebAe4509" },
                AdminsIDs = new List<Guid>() { Guid.NewGuid() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionCreateCommand_WithBuildingIDEqualsLocationID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionCreateCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00f1BDebAe4509" },
                AdminsIDs = new List<Guid>() { Guid.NewGuid() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingsIDs[0] failed validation. Error was: BuildingID was the same as LocationID!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionCreateCommand_WithEmptyBuildingsIDs_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionCreateCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>(0),
                AdminsIDs = new List<Guid>() { Guid.NewGuid() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionCreateCommand_WithEmptyBuildingsIDs_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionCreateCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>(0),
                AdminsIDs = new List<Guid>() { Guid.NewGuid() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingsIDs failed validation. Error was: BuildingsIDs was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionCreateCommand_WithEmptyAdminsIDs_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionCreateCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<Guid>(0),
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionCreateCommand_WithEmptyAdminsIDs_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionCreateCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<Guid>(0),
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property AdminsIDs failed validation. Error was: AdminsIDs was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionCreateCommand_WithDefaultAdminID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionCreateCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<Guid>() { default },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionCreateCommand_WithDefaultAdminID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionCreateCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<Guid>() { default },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property AdminsIDs[0] failed validation. Error was: AdminID was empty or null!", validationErrors);
        }

        #endregion DTOEducationalInstitutionCreateCommand TESTS

        #region DTOEducationalInstitutionUpdateCommand TESTS

        [Fact]
        public void GivenAValidDTOEducationalInstitutionUpdateCommand_ShouldReturnTrue()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = true,
                Description = "New_Description",
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionUpdateCommand_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = true,
                Description = "New_Description",
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionUpdateCommand_WithFalseUpdateNameAndEmptyName_ShouldReturnTrue()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = false,
                Name = string.Empty,
                UpdateDescription = true,
                Description = "New_Description",
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionUpdateCommand_WithFalseUpdateNameAndEmptyName_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = false,
                Name = string.Empty,
                UpdateDescription = true,
                Description = "New_Description",
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionUpdateCommand_WithFalseUpdateDescriptionAndEmptyDescription_ShouldReturnTrue()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = false,
                Description = string.Empty,
            };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionUpdateCommand_WithFalseUpdateDescriptionAndEmptyDescription_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = false,
                Description = string.Empty,
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionUpdateCommand_WithFalseUpdateDescriptionAndFalseUpdateName_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = false,
                Name = string.Empty,
                UpdateDescription = false,
                Description = string.Empty,
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionUpdateCommand_WithFalseUpdateDescriptionAndFalseUpdateName_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = false,
                Name = string.Empty,
                UpdateDescription = false,
                Description = string.Empty,
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property UpdateDescription failed validation. Error was: Both update fields are set to false!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionUpdateCommand_WithDefaultEducationalInstitutionID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionUpdateCommand
            {
                EducationalInstitutionID = default,
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = true,
                Description = "New_Description",
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionUpdateCommand_WithDefaultEducationalInstitutionID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionUpdateCommand
            {
                EducationalInstitutionID = default,
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = true,
                Description = "New_Description",
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property EducationalInstitutionID failed validation. Error was: Educational Institution ID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionUpdateCommand_WithEmptyName_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = string.Empty,
                UpdateDescription = true,
                Description = "New_Description",
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionUpdateCommand_WithEmptyName_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = string.Empty,
                UpdateDescription = true,
                Description = "New_Description",
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property Name failed validation. Error was: Name was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionUpdateCommand_WithOutOfBoundsNameLength_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "N",
                UpdateDescription = true,
                Description = "New_Description",
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionUpdateCommand_WithOutOfBoundsNameLength_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "N",
                UpdateDescription = true,
                Description = "New_Description",
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property Name failed validation. Error was: Name's length was not between 2-128 characters!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionUpdateCommand_WithEmptyDescription_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = true,
                Description = string.Empty,
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionUpdateCommand_WithEmptyDescription_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = true,
                Description = string.Empty,
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

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
            var validationResult = validationHandler.IsDataTransferObjectValid(request, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionUpdateCommand_WithOutOfBoundsDescriptionLength_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = true,
                Description = "N",
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property Description failed validation. Error was: Description's length was not between 2-500 characters!", validationErrors);
        }

        #endregion DTOEducationalInstitutionUpdateCommand TESTS

        #region DTOEducationalInstitutionParentUpdateCommand TESTS

        [Fact]
        public void GivenAValidDTOEducationalInstitutionParentUpdateCommand_ShouldReturnTrue()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionParentUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var result = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionParentUpdateCommand_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionParentUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var result = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionParentUpdateCommand_WithDefaultEducationalInstitutionID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionParentUpdateCommand
            {
                EducationalInstitutionID = default,
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var result = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionParentUpdateCommand_WithDefaultEducationalInstitutionID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionParentUpdateCommand
            {
                EducationalInstitutionID = default,
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var result = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property EducationalInstitutionID failed validation. Error was: Educational Institution ID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionParentUpdateCommand_WithParentInstitutionIDSameAsEducationalInstitutionID_ShouldReturnFalse()
        {
            //Arrange
            var id = Guid.NewGuid();
            var dto = new DTOEducationalInstitutionParentUpdateCommand
            {
                EducationalInstitutionID = id,
                ParentInstitutionID = id
            };

            //Act
            var result = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionParentUpdateCommand_WithParentInstitutionIDSameAsEducationalInstitutionID_ShouldReturnValidationErrors()
        {
            //Arrange
            var id = Guid.NewGuid();
            var dto = new DTOEducationalInstitutionParentUpdateCommand
            {
                EducationalInstitutionID = id,
                ParentInstitutionID = id
            };

            //Act
            var result = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property ParentInstitutionID failed validation. Error was: ParentInstitutionID was the same as EducationalInstitutionID!", validationErrors);
        }

        #endregion DTOEducationalInstitutionParentUpdateCommand TESTS

        #region DTOEducationalInstitutionLocationUpdateCommand TESTS

        [Fact]
        public void GivenAValidDTOEducationalInstitutionLocationUpdateCommand_ShouldReturnTrue()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionLocationUpdateCommand_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionLocationUpdateCommand_WithDefaultEducationalInstitutionID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = default,
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { "10Fc4a7f1E00f1BDebAe4509" }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionLocationUpdateCommand_WithDefaultEducationalInstitutionID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = default,
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { "10Fc4a7f1E00f1BDebAe4509" }
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property EducationalInstitutionID failed validation. Error was: Educational Institution ID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionLocationUpdateCommand_WithEmptyBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { string.Empty }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionLocationUpdateCommand_WithEmptyBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { string.Empty }
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property AddBuildingsIDs[0] failed validation. Error was: BuildingID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionLocationUpdateCommand_WithEmptyLocationID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = string.Empty,
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { "10Fc4a7f1e00f1BDebAe4503" }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionLocationUpdateCommand_WithEmptyLocationID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = string.Empty,
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { "10Fc4a7f1e00f1BDebAe4503" }
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property LocationID failed validation. Error was: Location ID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionLocationUpdateCommand_WithEmptyBuildings_ShouldReturnTrue()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = false,
                AddBuildingsIDs = default
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionLocationUpdateCommand_WithEmptyBuildings_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = false,
                AddBuildingsIDs = default
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionLocationUpdateCommand_WithEmptyLocationID_ShouldReturnTrue()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = false,
                LocationID = string.Empty,
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { "10Fc4a7f1e00f1BDebAe4509" }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionLocationUpdateCommand_WithDuplicateBuildingsIDs_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { "10Fc4a7f1e00f1BDebAe4509", "10Fc4a7f1e00f1BDebAe4509" }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionLocationUpdateCommand_WithDuplicateBuildingsIDs_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { "10Fc4a7f1e00f1BDebAe4509", "10Fc4a7f1e00f1BDebAe4509" }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property AddBuildingsIDs failed validation. Error was: AddBuildingsIDs can't contain duplicates!", validationErrors);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionLocationUpdateCommand_WithFalseUpdateLocationAndUpdateBuildings_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = false,
                LocationID = string.Empty,
                UpdateBuildings = false,
                AddBuildingsIDs = default
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionLocationUpdateCommand_WithFalseUpdateLocationAndUpdateBuildings_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = false,
                LocationID = string.Empty,
                UpdateBuildings = false,
                AddBuildingsIDs = default
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property UpdateBuildings failed validation. Error was: Both location and buildings update fields are false!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionLocationUpdateCommand_WithDuplicateBuildingsIDs_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { "10Fc4a7f1E00f1BDebAe4509", "10Fc4a7f1E00f1BDebAe4509" }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionLocationUpdateCommand_WithDuplicateBuildingsIDs_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { "10Fc4a7f1E00f1BDebAe4509", "10Fc4a7f1E00f1BDebAe4509" }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property AddBuildingsIDs failed validation. Error was: AddBuildingsIDs can't contain duplicates!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionLocationUpdateCommand_WithBothAddBuiildingsIDsAndRemoveBuildingsIDsEmpty_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>(0),
                RemoveBuildingsIDs = new List<string>(0)
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionLocationUpdateCommand_WithBothAddBuiildingsIDsAndRemoveBuildingsIDsEmpty_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>(0),
                RemoveBuildingsIDs = new List<string>(0)
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property AddBuildingsIDs failed validation. Error was: Both AddBuildingsIDs and RemoveBuildingsIDs collections are empty!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionLocationUpdateCommand_WithEmptyRemoveBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                RemoveBuildingsIDs = new List<string>() { string.Empty }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionLocationUpdateCommand_WithEmptyRemoveBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                RemoveBuildingsIDs = new List<string>() { string.Empty }
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property RemoveBuildingsIDs[0] failed validation. Error was: BuildingID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionLocationUpdateCommand_WithOutOfBoundsCharactersInRemoveBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>(),
                RemoveBuildingsIDs = new List<string>() { "Q10Fc47f1e00f1BDebAe4509" }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionLocationUpdateCommand_WithOutOfBoundsCharactersInRemoveBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>(),
                RemoveBuildingsIDs = new List<string>() { "Q10Fc47f1e00f1BDebAe4509" }
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property RemoveBuildingsIDs[0] failed validation. Error was: BuildingID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionLocationUpdateCommand_WithOutOfBoundsCharactersInAddBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                RemoveBuildingsIDs = new List<string>(),
                AddBuildingsIDs = new List<string>() { "Q10Fc47f1e00f1BDebAe4509" }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionLocationUpdateCommand_WithOutOfBoundsCharactersInAddBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                RemoveBuildingsIDs = new List<string>(),
                AddBuildingsIDs = new List<string>() { "Q10Fc47f1e00f1BDebAe4509" }
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property AddBuildingsIDs[0] failed validation. Error was: BuildingID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionLocationUpdateCommand_WithLengthGreaterThan24InRemoveBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>(),
                RemoveBuildingsIDs = new List<string>() { "a10Fc47f1e00f1BDebAe4509A" }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionLocationUpdateCommand_WithLengthGreaterThan24InRemoveBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>(),
                RemoveBuildingsIDs = new List<string>() { "a10Fc47f1e00f1BDebAe4509A" }
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property RemoveBuildingsIDs[0] failed validation. Error was: BuildingID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionLocationUpdateCommand_WithLengthGreaterThan24InAddBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                RemoveBuildingsIDs = new List<string>(),
                AddBuildingsIDs = new List<string>() { "AA10Fc47f1e00f1BDebAe4509" }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionLocationUpdateCommand_WithLengthGreaterThan24InAddBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                RemoveBuildingsIDs = new List<string>(),
                AddBuildingsIDs = new List<string>() { "a10Fc47f1e00f1BDebAe4509AE" }
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property AddBuildingsIDs[0] failed validation. Error was: BuildingID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionLocationUpdateCommand_WithSameIDOnAddBuildingsIDsAndRemoveBuildingsIDs_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                RemoveBuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionLocationUpdateCommand_WithSameIDOnAddBuildingsIDsAndRemoveBuildingsIDs_ShouldReturnAStringWithValidationErrors()
        {
            //Arrange
            var buildingID = "10Fc4a7f1e00F1BDebAe4501";
            var dto = new DTOEducationalInstitutionLocationUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateLocation = true,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                UpdateBuildings = true,
                AddBuildingsIDs = new List<string>() { buildingID },
                RemoveBuildingsIDs = new List<string>() { buildingID }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal($" Property AddBuildingsIDs[0] failed validation. Error was: AddBuildingsIDs' {buildingID} was also found in RemoveBuildingsIDs!", validationErrors);
        }

        #endregion DTOEducationalInstitutionLocationUpdateCommand TESTS

        #region DTOEducationalInstitutionAdminUpdateCommand TESTS

        [Fact]
        public void GivenAValidDTOEducationalInstitutionAdminUpdateCommand_ShouldReturnTrue()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionAdminUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<Guid>() { Guid.NewGuid() },
                RemoveAdminsIDs = new List<Guid>() { Guid.NewGuid() }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionAdminUpdateCommand_ShouldReturnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionAdminUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<Guid>() { Guid.NewGuid() },
                RemoveAdminsIDs = new List<Guid>() { Guid.NewGuid() }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionAdminUpdateCommand_WithNullAddAdminsIDsCollection_ShouldReturnTrue()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionAdminUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = null,
                RemoveAdminsIDs = new List<Guid>() { Guid.NewGuid() }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionAdminUpdateCommand_WithNullAddAdminsIDsCollection_ShouldReturnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionAdminUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = null,
                RemoveAdminsIDs = new List<Guid>() { Guid.NewGuid() }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionAdminUpdateCommand_WithNullRemoveAdminsIDsCollection_ShouldReturnTrue()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionAdminUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<Guid>() { Guid.NewGuid() },
                RemoveAdminsIDs = null
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionAdminUpdateCommand_WithNullRemoveAdminsIDsCollection_ShouldReturnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionAdminUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<Guid>() { Guid.NewGuid() },
                RemoveAdminsIDs = null
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionAdminUpdateCommand_WithDefaultID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionAdminUpdateCommand
            {
                EducationalInstitutionID = default,
                AddAdminsIDs = new List<Guid>() { Guid.NewGuid() },
                RemoveAdminsIDs = new List<Guid>() { Guid.NewGuid() }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionAdminUpdateCommand_WithDefaultID_ShouldReturnAStringWithValidationError()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionAdminUpdateCommand
            {
                EducationalInstitutionID = default,
                AddAdminsIDs = new List<Guid>() { Guid.NewGuid() },
                RemoveAdminsIDs = new List<Guid>() { Guid.NewGuid() }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property EducationalInstitutionID failed validation. Error was: Educational Institution ID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionAdminUpdateCommand_WithBothCollectionsNull_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionAdminUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = null,
                RemoveAdminsIDs = null
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionAdminUpdateCommand_WithBothCollectionsNull_ShouldReturnAStringWithValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionAdminUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = null,
                RemoveAdminsIDs = null
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property RemoveAdminsIDs failed validation. Error was: Both AddAdminsIDs and RemoveAdminsIDs collections are empty!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionAdminUpdateCommand_WithSameAdminIDOnBothCollections_ShouldReturnFalse()
        {
            //Arrange
            var adminID = Guid.NewGuid();
            var dto = new DTOEducationalInstitutionAdminUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<Guid>() { adminID },
                RemoveAdminsIDs = new List<Guid>() { adminID }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionAdminUpdateCommand_WithSameAdminIDOnBothCollections_ShouldReturnAStringWithValidationErrors()
        {
            //Arrange
            var adminID = Guid.NewGuid();
            var dto = new DTOEducationalInstitutionAdminUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<Guid>() { adminID },
                RemoveAdminsIDs = new List<Guid>() { adminID }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal($" Property RemoveAdminsIDs[0] failed validation. Error was: RemoveAdminsIDs' {adminID} was also found in AddAdminsIDs!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionAdminUpdateCommand_WithDuplicateIDInAddAdminsIDsCollection_ShouldReturnFalse()
        {
            //Arrange
            var adminID = Guid.NewGuid();
            var dto = new DTOEducationalInstitutionAdminUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<Guid>() { adminID, adminID },
                RemoveAdminsIDs = new List<Guid>() { Guid.NewGuid() }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionAdminUpdateCommand_WithDuplicateIDInAddAdminsIDsCollection_ShouldReturnAStringWithValidationErrors()
        {
            //Arrange
            var adminID = Guid.NewGuid();
            var dto = new DTOEducationalInstitutionAdminUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<Guid>() { adminID, adminID },
                RemoveAdminsIDs = new List<Guid>() { Guid.NewGuid() }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property AddAdminsIDs failed validation. Error was: AddAdminsIDs contains duplicate values!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionAdminUpdateCommand_WithDuplicateIDInRemoveAdminsIDsCollection_ShouldReturnFalse()
        {
            //Arrange
            var adminID = Guid.NewGuid();
            var dto = new DTOEducationalInstitutionAdminUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<Guid>() { Guid.NewGuid() },
                RemoveAdminsIDs = new List<Guid>() { adminID, adminID }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionAdminUpdateCommand_WithDuplicateIDInRemoveAdminsIDsCollection_ShouldReturnAStringWithValidationErrors()
        {
            //Arrange
            var adminID = Guid.NewGuid();
            var dto = new DTOEducationalInstitutionAdminUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<Guid>() { Guid.NewGuid() },
                RemoveAdminsIDs = new List<Guid>() { adminID, adminID }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property RemoveAdminsIDs failed validation. Error was: RemoveAdminsIDs contains duplicate values!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionAdminUpdateCommand_WithDefaultIDInAddAdminsIDs_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionAdminUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<Guid>() { default },
                RemoveAdminsIDs = new List<Guid>() { Guid.NewGuid() }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionAdminUpdateCommand_WithDefaultIDInAddAdminsIDs_ShouldReturnAStringWithValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionAdminUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<Guid>() { default },
                RemoveAdminsIDs = new List<Guid>() { Guid.NewGuid() }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property AddAdminsIDs[0] failed validation. Error was: AddAdminsIDs contains an invalid ID!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionAdminUpdateCommand_WithDefaultIDInRemoveAdminsIDs_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionAdminUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<Guid>() { Guid.NewGuid() },
                RemoveAdminsIDs = new List<Guid>() { default }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionAdminUpdateCommand_WithDefaultIDInRemoveAdminsIDs_ShouldReturnAStringWithValidationErrors()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionAdminUpdateCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<Guid>() { Guid.NewGuid() },
                RemoveAdminsIDs = new List<Guid>() { default }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property RemoveAdminsIDs[0] failed validation. Error was: RemoveAdminsIDs contains an invalid ID!", validationErrors);
        }

        #endregion DTOEducationalInstitutionAdminUpdateCommand TESTS

        #region DTOEducationalInstiutionDeleteCommand TESTS

        [Fact]
        public void GivenAValidDTOEducationalInstitutionDeleteCommand_ShouldReturnTrue()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionDeleteCommand { EducationalInstitutionID = Guid.NewGuid() };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidDTOEducationalInstitutionDeleteCommand_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionDeleteCommand { EducationalInstitutionID = Guid.NewGuid() };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionDeleteCommand_WithDefaultEducationalInstitutionID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionDeleteCommand { EducationalInstitutionID = default };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDTOEducationalInstitutionDeleteCommand_WithDefaultEducationalInstitutionID_ShouldReturnAStringWithTheErrorsFound()
        {
            //Arrange
            var dto = new DTOEducationalInstitutionDeleteCommand { EducationalInstitutionID = default };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property EducationalInstitutionID failed validation. Error was: Educational Institution ID was empty or null!", validationErrors);
        }

        #endregion DTOEducationalInstiutionDeleteCommand TESTS
    }
}