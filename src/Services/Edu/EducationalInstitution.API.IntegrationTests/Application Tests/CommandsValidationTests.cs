﻿using DataValidation;
using DataValidation.Abstractions;
using EducationalInstitution.Application.Commands;
using EducationalInstitution.Application.Commands.Validators;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace EducationalInstitution.API.IntegrationTests.Application_Tests
{
    public class CommandsValidationTests
    {
        private readonly Mock<ILogger<ValidationHandler>> mockLogger;
        private readonly IValidationHandler validationHandler;

        /// <remarks>Called before each test</remarks>
        public CommandsValidationTests()
        {
            mockLogger = new();

            var applicationAssembly = typeof(CreateEducationalInstitutionCommandValidator).Assembly;
            validationHandler = new ValidationHandler(mockLogger.Object, new ValidatorFactory(applicationAssembly));
        }

        #region CreateEducationalInstitutionCommand TESTS

        [Fact]
        public void GivenAValidCreateEducationalInstitutionCommand_ShouldReturnTrue()
        {
            //Arrange
            var dto = new CreateEducationalInstitutionCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidCreateEducationalInstitutionCommand_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new CreateEducationalInstitutionCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidCreateEducationalInstitutionCommand_WithEmptyName_ShouldReturnFalse()
        {
            //Arrange
            var dto = new CreateEducationalInstitutionCommand
            {
                Name = string.Empty,
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidCreateEducationalInstitutionCommand_WithEmptyName_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new CreateEducationalInstitutionCommand
            {
                Name = string.Empty,
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property Name failed validation. Error was: Name was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidCreateEducationalInstitutionCommand_WithOutOfBoundsLengthName_ShouldReturnFalse()
        {
            //Arrange
            var dto = new CreateEducationalInstitutionCommand
            {
                Name = "N",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidCreateEducationalInstitutionCommand_WithOutOfBoundsLengthName_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new CreateEducationalInstitutionCommand
            {
                Name = "N",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property Name failed validation. Error was: Name's length was not between 2-128 characters!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidCreateEducationalInstitutionCommand_WithEmptyDescription_ShouldReturnFalse()
        {
            //Arrange
            var dto = new CreateEducationalInstitutionCommand
            {
                Name = "Name",
                Description = string.Empty,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidCreateEducationalInstitutionCommand_WithEmptyDescription_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new CreateEducationalInstitutionCommand
            {
                Name = "Name",
                Description = string.Empty,
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property Description failed validation. Error was: Description was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidCreateEducationalInstitutionCommand_WithOutOfBoundsLengthDescription_ShouldReturnFalse()
        {
            //Arrange
            var dto = new CreateEducationalInstitutionCommand
            {
                Name = "Name",
                Description = "D",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidCreateEducationalInstitutionCommand_WithOutOfBoundsLengthDescription_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new CreateEducationalInstitutionCommand
            {
                Name = "Name",
                Description = "D",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property Description failed validation. Error was: Description's length was not between 2-500 characters!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidCreateEducationalInstitutionCommand_WithEmptyBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new CreateEducationalInstitutionCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { string.Empty },
                AdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidCreateEducationalInstitutionCommand_WithEmptyBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new CreateEducationalInstitutionCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { string.Empty },
                AdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingsIDs[0] failed validation. Error was: BuildingID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidCreateEducationalInstitutionCommand_WithOutOfBoundsLengthBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new CreateEducationalInstitutionCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fa3A" },
                AdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidCreateEducationalInstitutionCommand_WithOutOfBoundsLengthBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new CreateEducationalInstitutionCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fa3A" },
                AdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingsIDs[0] failed validation. Error was: BuildingID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidCreateEducationalInstitutionCommand_WithOutOfBoundsAlphabetBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new CreateEducationalInstitutionCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fa3AQ" },
                AdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidCreateEducationalInstitutionCommand_WithOutOfBoundsAlphabetBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new CreateEducationalInstitutionCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fa3AQ" },
                AdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingsIDs[0] failed validation. Error was: BuildingID contains characters that are not supported and/or the length is not exactly 24!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidCreateEducationalInstitutionCommand_WithBuildingIDEqualsLocationID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new CreateEducationalInstitutionCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00f1BDebAe4509" },
                AdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidCreateEducationalInstitutionCommand_WithBuildingIDEqualsLocationID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new CreateEducationalInstitutionCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00f1BDebAe4509" },
                AdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingsIDs[0] failed validation. Error was: BuildingID was the same as LocationID!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidCreateEducationalInstitutionCommand_WithEmptyBuildingsIDs_ShouldReturnFalse()
        {
            //Arrange
            var dto = new CreateEducationalInstitutionCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>(0),
                AdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidCreateEducationalInstitutionCommand_WithEmptyBuildingsIDs_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new CreateEducationalInstitutionCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>(0),
                AdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property BuildingsIDs failed validation. Error was: BuildingsIDs was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidCreateEducationalInstitutionCommand_WithEmptyAdminsIDs_ShouldReturnFalse()
        {
            //Arrange
            var dto = new CreateEducationalInstitutionCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<string>(0),
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidCreateEducationalInstitutionCommand_WithEmptyAdminsIDs_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new CreateEducationalInstitutionCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<string>(0),
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property AdminsIDs failed validation. Error was: AdminsIDs was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidCreateEducationalInstitutionCommand_WithDefaultAdminID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new CreateEducationalInstitutionCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<string>() { default },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidCreateEducationalInstitutionCommand_WithDefaultAdminID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new CreateEducationalInstitutionCommand
            {
                Name = "Name",
                Description = "Description",
                LocationID = "10Fc4a7f1e00f1BDebAe4509",
                BuildingsIDs = new List<string>() { "10Fc4a7f1e00F1BDebAe4501" },
                AdminsIDs = new List<string>() { default },
                ParentInstitutionID = Guid.NewGuid()
            };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property AdminsIDs[0] failed validation. Error was: AdminID was empty or null!", validationErrors);
        }

        #endregion CreateEducationalInstitutionCommand TESTS

        #region UpdateEducationalInstitutionCommand TESTS

        [Fact]
        public void GivenAValidUpdateEducationalInstitutionCommand_ShouldReturnTrue()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionCommand
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
        public void GivenAValidUpdateEducationalInstitutionCommand_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionCommand
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
        public void GivenAValidUpdateEducationalInstitutionCommand_WithFalseUpdateNameAndEmptyName_ShouldReturnTrue()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionCommand
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
        public void GivenAValidUpdateEducationalInstitutionCommand_WithFalseUpdateNameAndEmptyName_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionCommand
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
        public void GivenAValidUpdateEducationalInstitutionCommand_WithFalseUpdateDescriptionAndEmptyDescription_ShouldReturnTrue()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                UpdateName = true,
                Name = "New_Name",
                UpdateDescription = false,
                Description = string.Empty,
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidUpdateEducationalInstitutionCommand_WithFalseUpdateDescriptionAndEmptyDescription_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionCommand_WithFalseUpdateDescriptionAndFalseUpdateName_ShouldReturnFalse()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionCommand_WithFalseUpdateDescriptionAndFalseUpdateName_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionCommand_WithDefaultEducationalInstitutionID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionCommand_WithDefaultEducationalInstitutionID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionCommand_WithEmptyName_ShouldReturnFalse()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionCommand_WithEmptyName_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionCommand_WithOutOfBoundsNameLength_ShouldReturnFalse()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionCommand_WithOutOfBoundsNameLength_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionCommand_WithEmptyDescription_ShouldReturnFalse()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionCommand_WithEmptyDescription_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionCommand
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
        public void GivenAnInvalidRequestOfTypeUpdateEducationalInstitutionCommand_WithOutOfBoundsDescriptionLength_ShouldReturnFalse()
        {
            //Arrange
            var request = new UpdateEducationalInstitutionCommand()
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
        public void GivenAnInvalidUpdateEducationalInstitutionCommand_WithOutOfBoundsDescriptionLength_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionCommand
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

        #endregion UpdateEducationalInstitutionCommand TESTS

        #region UpdateEducationalInstitutionParentCommand TESTS

        [Fact]
        public void GivenAValidUpdateEducationalInstitutionParentCommand_ShouldReturnTrue()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionParentCommand
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
        public void GivenAValidUpdateEducationalInstitutionParentCommand_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionParentCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionParentCommand_WithDefaultEducationalInstitutionID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionParentCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionParentCommand_WithDefaultEducationalInstitutionID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionParentCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionParentCommand_WithParentInstitutionIDSameAsEducationalInstitutionID_ShouldReturnFalse()
        {
            //Arrange
            var id = Guid.NewGuid();
            var dto = new UpdateEducationalInstitutionParentCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionParentCommand_WithParentInstitutionIDSameAsEducationalInstitutionID_ShouldReturnValidationErrors()
        {
            //Arrange
            var id = Guid.NewGuid();
            var dto = new UpdateEducationalInstitutionParentCommand
            {
                EducationalInstitutionID = id,
                ParentInstitutionID = id
            };

            //Act
            var result = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property ParentInstitutionID failed validation. Error was: ParentInstitutionID was the same as EducationalInstitutionID!", validationErrors);
        }

        #endregion UpdateEducationalInstitutionParentCommand TESTS

        #region UpdateEducationalInstitutionLocationCommand TESTS

        [Fact]
        public void GivenAValidUpdateEducationalInstitutionLocationCommand_ShouldReturnTrue()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAValidUpdateEducationalInstitutionLocationCommand_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionLocationCommand_WithDefaultEducationalInstitutionID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionLocationCommand_WithDefaultEducationalInstitutionID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionLocationCommand_WithEmptyBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionLocationCommand_WithEmptyBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionLocationCommand_WithEmptyLocationID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionLocationCommand_WithEmptyLocationID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAValidUpdateEducationalInstitutionLocationCommand_WithEmptyBuildings_ShouldReturnTrue()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAValidUpdateEducationalInstitutionLocationCommand_WithEmptyBuildings_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAValidUpdateEducationalInstitutionLocationCommand_WithEmptyLocationID_ShouldReturnTrue()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAValidUpdateEducationalInstitutionLocationCommand_WithDuplicateBuildingsIDs_ShouldReturnFalse()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAValidUpdateEducationalInstitutionLocationCommand_WithDuplicateBuildingsIDs_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAValidUpdateEducationalInstitutionLocationCommand_WithFalseUpdateLocationAndUpdateBuildings_ShouldReturnFalse()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAValidUpdateEducationalInstitutionLocationCommand_WithFalseUpdateLocationAndUpdateBuildings_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionLocationCommand_WithDuplicateBuildingsIDs_ShouldReturnFalse()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionLocationCommand_WithDuplicateBuildingsIDs_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionLocationCommand_WithBothAddBuiildingsIDsAndRemoveBuildingsIDsEmpty_ShouldReturnFalse()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionLocationCommand_WithBothAddBuiildingsIDsAndRemoveBuildingsIDsEmpty_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionLocationCommand_WithEmptyRemoveBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionLocationCommand_WithEmptyRemoveBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionLocationCommand_WithOutOfBoundsCharactersInRemoveBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionLocationCommand_WithOutOfBoundsCharactersInRemoveBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionLocationCommand_WithOutOfBoundsCharactersInAddBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionLocationCommand_WithOutOfBoundsCharactersInAddBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionLocationCommand_WithLengthGreaterThan24InRemoveBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionLocationCommand_WithLengthGreaterThan24InRemoveBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionLocationCommand_WithLengthGreaterThan24InAddBuildingID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionLocationCommand_WithLengthGreaterThan24InAddBuildingID_ShouldReturnValidationErrors()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAValidUpdateEducationalInstitutionLocationCommand_WithSameIDOnAddBuildingsIDsAndRemoveBuildingsIDs_ShouldReturnFalse()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionLocationCommand
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
        public void GivenAValidUpdateEducationalInstitutionLocationCommand_WithSameIDOnAddBuildingsIDsAndRemoveBuildingsIDs_ShouldReturnAStringWithValidationErrors()
        {
            //Arrange
            var buildingID = "10Fc4a7f1e00F1BDebAe4501";
            var dto = new UpdateEducationalInstitutionLocationCommand
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

        #endregion UpdateEducationalInstitutionLocationCommand TESTS

        #region UpdateEducationalInstitutionAdminsCommand TESTS

        [Fact]
        public void GivenAValidUpdateEducationalInstitutionAdminsCommand_ShouldReturnTrue()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionAdminsCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                RemoveAdminsIDs = new List<string>() { Guid.NewGuid().ToString() }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidUpdateEducationalInstitutionAdminsCommand_ShouldReturnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionAdminsCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                RemoveAdminsIDs = new List<string>() { Guid.NewGuid().ToString() }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAValidUpdateEducationalInstitutionAdminsCommand_WithNullAddAdminsIDsCollection_ShouldReturnTrue()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionAdminsCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = null,
                RemoveAdminsIDs = new List<string>() { Guid.NewGuid().ToString() }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidUpdateEducationalInstitutionAdminsCommand_WithNullAddAdminsIDsCollection_ShouldReturnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionAdminsCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = null,
                RemoveAdminsIDs = new List<string>() { Guid.NewGuid().ToString() }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAValidUpdateEducationalInstitutionAdminsCommand_WithNullRemoveAdminsIDsCollection_ShouldReturnTrue()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionAdminsCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                RemoveAdminsIDs = null
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidUpdateEducationalInstitutionAdminsCommand_WithNullRemoveAdminsIDsCollection_ShouldReturnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionAdminsCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                RemoveAdminsIDs = null
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidUpdateEducationalInstitutionAdminsCommand_WithDefaultID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionAdminsCommand
            {
                EducationalInstitutionID = default,
                AddAdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                RemoveAdminsIDs = new List<string>() { Guid.NewGuid().ToString() }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidUpdateEducationalInstitutionAdminsCommand_WithDefaultID_ShouldReturnAStringWithValidationError()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionAdminsCommand
            {
                EducationalInstitutionID = default,
                AddAdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                RemoveAdminsIDs = new List<string>() { Guid.NewGuid().ToString() }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property EducationalInstitutionID failed validation. Error was: Educational Institution ID was empty or null!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidUpdateEducationalInstitutionAdminsCommand_WithBothCollectionsNull_ShouldReturnFalse()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionAdminsCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionAdminsCommand_WithBothCollectionsNull_ShouldReturnAStringWithValidationErrors()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionAdminsCommand
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
        public void GivenAnInvalidUpdateEducationalInstitutionAdminsCommand_WithSameAdminIDOnBothCollections_ShouldReturnFalse()
        {
            //Arrange
            var adminID = Guid.NewGuid().ToString();
            var dto = new UpdateEducationalInstitutionAdminsCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<string>() { adminID },
                RemoveAdminsIDs = new List<string>() { adminID }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidUpdateEducationalInstitutionAdminsCommand_WithSameAdminIDOnBothCollections_ShouldReturnAStringWithValidationErrors()
        {
            //Arrange
            var adminID = Guid.NewGuid().ToString();
            var dto = new UpdateEducationalInstitutionAdminsCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<string>() { adminID },
                RemoveAdminsIDs = new List<string>() { adminID }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal($" Property RemoveAdminsIDs[0] failed validation. Error was: RemoveAdminsIDs' {adminID} was also found in AddAdminsIDs!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidUpdateEducationalInstitutionAdminsCommand_WithDuplicateIDInAddAdminsIDsCollection_ShouldReturnFalse()
        {
            //Arrange
            var adminID = Guid.NewGuid().ToString();
            var dto = new UpdateEducationalInstitutionAdminsCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<string>() { adminID, adminID },
                RemoveAdminsIDs = new List<string>() { Guid.NewGuid().ToString() }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidUpdateEducationalInstitutionAdminsCommand_WithDuplicateIDInAddAdminsIDsCollection_ShouldReturnAStringWithValidationErrors()
        {
            //Arrange
            var adminID = Guid.NewGuid().ToString();
            var dto = new UpdateEducationalInstitutionAdminsCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<string>() { adminID, adminID },
                RemoveAdminsIDs = new List<string>() { Guid.NewGuid().ToString() }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property AddAdminsIDs failed validation. Error was: AddAdminsIDs contains duplicate values!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidUpdateEducationalInstitutionAdminsCommand_WithDuplicateIDInRemoveAdminsIDsCollection_ShouldReturnFalse()
        {
            //Arrange
            var adminID = Guid.NewGuid().ToString();
            var dto = new UpdateEducationalInstitutionAdminsCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                RemoveAdminsIDs = new List<string>() { adminID, adminID }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidUpdateEducationalInstitutionAdminsCommand_WithDuplicateIDInRemoveAdminsIDsCollection_ShouldReturnAStringWithValidationErrors()
        {
            //Arrange
            var adminID = Guid.NewGuid().ToString();
            var dto = new UpdateEducationalInstitutionAdminsCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                RemoveAdminsIDs = new List<string>() { adminID, adminID }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property RemoveAdminsIDs failed validation. Error was: RemoveAdminsIDs contains duplicate values!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidUpdateEducationalInstitutionAdminsCommand_WithDefaultIDInAddAdminsIDs_ShouldReturnFalse()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionAdminsCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<string>() { default },
                RemoveAdminsIDs = new List<string>() { Guid.NewGuid().ToString() }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidUpdateEducationalInstitutionAdminsCommand_WithDefaultIDInAddAdminsIDs_ShouldReturnAStringWithValidationErrors()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionAdminsCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<string>() { default },
                RemoveAdminsIDs = new List<string>() { Guid.NewGuid().ToString() }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property AddAdminsIDs[0] failed validation. Error was: AddAdminsIDs contains an invalid ID!", validationErrors);
        }

        [Fact]
        public void GivenAnInvalidUpdateEducationalInstitutionAdminsCommand_WithDefaultIDInRemoveAdminsIDs_ShouldReturnFalse()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionAdminsCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                RemoveAdminsIDs = new List<string>() { default }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidUpdateEducationalInstitutionAdminsCommand_WithDefaultIDInRemoveAdminsIDs_ShouldReturnAStringWithValidationErrors()
        {
            //Arrange
            var dto = new UpdateEducationalInstitutionAdminsCommand
            {
                EducationalInstitutionID = Guid.NewGuid(),
                AddAdminsIDs = new List<string>() { Guid.NewGuid().ToString() },
                RemoveAdminsIDs = new List<string>() { default }
            };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property RemoveAdminsIDs[0] failed validation. Error was: RemoveAdminsIDs contains an invalid ID!", validationErrors);
        }

        #endregion UpdateEducationalInstitutionAdminsCommand TESTS

        #region DisableEducationalInstitutionCommand TESTS

        [Fact]
        public void GivenAValidDisableEducationalInstitutionCommand_ShouldReturnTrue()
        {
            //Arrange
            var dto = new DisableEducationalInstitutionCommand { EducationalInstitutionID = Guid.NewGuid() };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

            //Assert
            Assert.True(validationResult);
        }

        [Fact]
        public void GivenAValidDisableEducationalInstitutionCommand_ShouldReturnAnEmptyValidationErrorsString()
        {
            //Arrange
            var dto = new DisableEducationalInstitutionCommand { EducationalInstitutionID = Guid.NewGuid() };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void GivenAnInvalidDisableEducationalInstitutionCommand_WithDefaultEducationalInstitutionID_ShouldReturnFalse()
        {
            //Arrange
            var dto = new DisableEducationalInstitutionCommand { EducationalInstitutionID = default };

            //Act
            var validationResult = validationHandler.IsDataTransferObjectValid(dto, out _);

            //Assert
            Assert.False(validationResult);
        }

        [Fact]
        public void GivenAnInvalidDisableEducationalInstitutionCommand_WithDefaultEducationalInstitutionID_ShouldReturnAStringWithTheErrorsFound()
        {
            //Arrange
            var dto = new DisableEducationalInstitutionCommand { EducationalInstitutionID = default };

            //Act
            validationHandler.IsDataTransferObjectValid(dto, out string validationErrors);

            //Assert
            Assert.Equal(" Property EducationalInstitutionID failed validation. Error was: Educational Institution ID was empty or null!", validationErrors);
        }

        #endregion DisableEducationalInstitutionCommand TESTS
    }
}