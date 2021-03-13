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

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByIDQuery_ShouldReturnTrue()
        {
            #region Arrange

            var request = new DTOEducationalInstitutionByIDQuery(Guid.NewGuid());
            var validationHandler = new ValidationHandler(mockLogger.Object);

            #endregion Arrange

            #region Act

            var validationResult = validationHandler.IsRequestValid(request, out _);

            #endregion Act

            #region Assert

            Assert.True(validationResult);

            #endregion Assert
        }

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByIDQuery_ShouldReturnAnEmptyValidationErrorsString()
        {
            #region Arrange

            var request = new DTOEducationalInstitutionByIDQuery(Guid.NewGuid());
            var validationHandler = new ValidationHandler(mockLogger.Object);

            #endregion Arrange

            #region Act

            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            #endregion Act

            #region Assert

            Assert.Empty(validationErrors);

            #endregion Assert
        }

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByIDQuery_ShouldReturnFalse()
        {
            #region Arrange

            var request = new DTOEducationalInstitutionByIDQuery(default);
            var validationHandler = new ValidationHandler(mockLogger.Object);

            #endregion Arrange

            #region Act

            var validationResult = validationHandler.IsRequestValid(request, out _);

            #endregion Act

            #region Assert

            Assert.False(validationResult);

            #endregion Assert
        }

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByIDQuery_WithDefaultGuidID_ShouldReturnAStringWithTheErrorsFound()
        {
            #region Arrange

            var request = new DTOEducationalInstitutionByIDQuery(default);
            var validationHandler = new ValidationHandler(mockLogger.Object);

            #endregion Arrange

            #region Act

            validationHandler.IsRequestValid(request, out string validationErrors);

            #endregion Act

            #region Assert

            Assert.Equal(" Property EduInstitutionID failed validation. Error was: Edu Institution ID must not be empty and be of type GUID: https://docs.microsoft.com/en-us/dotnet/api/system.guid", validationErrors);

            #endregion Assert
        }

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByNameQuery_ShouldReturnTrue()
        {
            #region Arrange

            var request = new DTOEducationalInstitutionsByNameQuery() { Name = "University", OffsetValue = 0, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            #endregion Arrange

            #region Act

            var validationResult = validationHandler.IsRequestValid(request, out _);

            #endregion Act

            #region Assert

            Assert.True(validationResult);

            #endregion Assert
        }

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByNameQuery_ShouldReturnAnEmptyValidationErrorsString()
        {
            #region Arrange

            var request = new DTOEducationalInstitutionsByNameQuery() { Name = "University", OffsetValue = 0, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            #endregion Arrange

            #region Act

            validationHandler.IsRequestValid(request, out string validationErrors);

            #endregion Act

            #region Assert

            Assert.Empty(validationErrors);

            #endregion Assert
        }

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByNameQuery_WithNullName_ShouldReturnFalse()
        {
            #region Arrange

            var request = new DTOEducationalInstitutionsByNameQuery() { Name = null, OffsetValue = 0, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            #endregion Arrange

            #region Act

            var validationResult = validationHandler.IsRequestValid(request, out _);

            #endregion Act

            #region Assert

            Assert.False(validationResult);

            #endregion Assert
        }

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByNameQuery_WithEmptyName_ShouldReturnFalse()
        {
            #region Arrange

            var request = new DTOEducationalInstitutionsByNameQuery() { Name = string.Empty, OffsetValue = 0, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            #endregion Arrange

            #region Act

            var validationResult = validationHandler.IsRequestValid(request, out _);

            #endregion Act

            #region Assert

            Assert.False(validationResult);

            #endregion Assert
        }

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByNameQuery_WithEmptyName_ShouldReturnMessage()
        {
            #region Arrange

            var request = new DTOEducationalInstitutionsByNameQuery() { Name = string.Empty, OffsetValue = 0, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            #endregion Arrange

            #region Act

            validationHandler.IsRequestValid(request, out string validationErrors);

            #endregion Act

            #region Assert

            Assert.Equal(" Property Name failed validation. Error was: Property Name cannot be empty or null!", validationErrors);

            #endregion Assert
        }

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByNameQuery_WithNullName_ShouldReturnMessage()
        {
            #region Arrange

            var request = new DTOEducationalInstitutionsByNameQuery() { Name = null, OffsetValue = 0, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            #endregion Arrange

            #region Act

            validationHandler.IsRequestValid(request, out string validationErrors);

            #endregion Act

            #region Assert

            Assert.Equal(" Property Name failed validation. Error was: Property Name cannot be empty or null!", validationErrors);

            #endregion Assert
        }

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByNameQuery_WithNameOfLength1_ShouldReturnMessage()
        {
            #region Arrange

            var request = new DTOEducationalInstitutionsByNameQuery() { Name = "a", OffsetValue = 0, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            #endregion Arrange

            #region Act

            validationHandler.IsRequestValid(request, out string validationErrors);

            #endregion Act

            #region Assert

            Assert.Equal(" Property Name failed validation. Error was: Property Name's length must be between 2-128 characters!", validationErrors);

            #endregion Assert
        }

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByNameQuery_WithOffsetValueNegative_ShouldReturnFalse()
        {
            #region Arrange

            var request = new DTOEducationalInstitutionsByNameQuery() { Name = "University", OffsetValue = -1, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            #endregion Arrange

            #region Act

            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            #endregion Act

            #region Assert

            Assert.False(validationResult);

            #endregion Assert
        }

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByNameQuery_WithNegativeOffsetValue_ShouldReturnMessage()
        {
            #region Arrange

            var request = new DTOEducationalInstitutionsByNameQuery() { Name = "University", OffsetValue = -1, ResultsCount = 1 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            #endregion Arrange

            #region Act

            validationHandler.IsRequestValid(request, out string validationErrors);

            #endregion Act

            #region Assert

            Assert.Equal(" Property OffsetValue failed validation. Error was: Property Offset Value must be between 0 and 150!", validationErrors);

            #endregion Assert
        }

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByNameQuery_WithZeroResultsCount_ShouldReturnFalse()
        {
            #region Arrange

            var request = new DTOEducationalInstitutionsByNameQuery() { Name = "University", OffsetValue = 0, ResultsCount = 0 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            #endregion Arrange

            #region Act

            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            #endregion Act

            #region Assert

            Assert.False(validationResult);

            #endregion Assert
        }

        [Fact]
        public void GivenARequestOfTypeDTOEducationalInstitutionByNameQuery_WithZeroResultsCount_ShouldReturnMessage()
        {
            #region Arrange

            var request = new DTOEducationalInstitutionsByNameQuery() { Name = "University", OffsetValue = 0, ResultsCount = 0 };
            var validationHandler = new ValidationHandler(mockLogger.Object);

            #endregion Arrange

            #region Act

            var validationResult = validationHandler.IsRequestValid(request, out string validationErrors);

            #endregion Act

            #region Assert

            Assert.Equal(" Property ResultsCount failed validation. Error was: Property Results Count must be between 1 and 100!", validationErrors);

            #endregion Assert
        }
    }
}