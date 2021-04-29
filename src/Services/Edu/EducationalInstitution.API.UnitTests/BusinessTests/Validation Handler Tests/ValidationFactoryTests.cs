using EducationalInstitutionAPI.Business.Validation_Handler;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.DTOs.Queries;
using EducationalInstitutionAPI.DTOs.Validators.Commands_Validators;
using EducationalInstitutionAPI.DTOs.Validators.Queries_Validators;
using EducationalInstitutionAPI.Utils.Custom_Exceptions;
using System.Text;
using Xunit;

namespace EducationalInstitution.API.UnitTests.BusinessTests.Validation_Handler_Tests
{
    public class ValidationFactoryTests
    {
        [Fact]
        public void GivenAValidTypeDTOEducationalInstitutionByIDQuery_ShouldReturnAnInstanceOfDTOEducationalInstitutionByIDQueryValidator()
        {
            //Act
            var validator = ValidationFactory.CreateValidator<DTOEducationalInstitutionByIDQuery>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionByIDQueryValidator>(validator);
        }

        [Fact]
        public void GivenAValidTypeDTOEducationalInstitutionByLocationQuery_ShouldReturnAnInstanceOfDTOEducationalInstitutionByLocationQueryValidator()
        {
            //Act
            var validator = ValidationFactory.CreateValidator<DTOEducationalInstitutionByLocationQuery>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionByLocationQueryValidator>(validator);
        }

        [Fact]
        public void GivenAValidTypeDTOEducationalInstitutionsByNameQuery_ShouldReturnAnInstanceOfDTOEducationalInstitutionsByNameQueryValidator()
        {
            //Act
            var validator = ValidationFactory.CreateValidator<DTOEducationalInstitutionsByNameQuery>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionsByNameQueryValidator>(validator);
        }

        [Fact]
        public void GivenAValidTypeDTOEducationalInstitutionsFromCollectionOfIDsQuery_ShouldReturnAnInstanceOfDTOEducationalInstitutionsFromCollectionOfIDsQueryValidator()
        {
            //Act
            var validator = ValidationFactory.CreateValidator<DTOEducationalInstitutionsFromCollectionOfIDsQuery>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionsFromCollectionOfIDsQueryValidator>(validator);
        }

        [Fact]
        public void GivenAValidTypeDTOEducationalInstitutionCreateCommand_ShouldReturnAnInstanceOfDTOEducationalInstitutionCreateCommandValidator()
        {
            //Act
            var validator = ValidationFactory.CreateValidator<DTOEducationalInstitutionCreateCommand>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionCreateCommandValidator>(validator);
        }

        [Fact]
        public void GivenAValidTypeDTOEducationalInstitutionDeleteCommand_ShouldReturnAnInstanceOfDTOEducationalInstitutionDeleteCommandValidator()
        {
            //Act
            var validator = ValidationFactory.CreateValidator<DTOEducationalInstitutionDeleteCommand>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionDeleteCommandValidator>(validator);
        }

        [Fact]
        public void GivenAValidTypeDTOEducationalInstitutionLocationUpdateCommand_ShouldReturnAnInstanceOfDTOEducationalInstitutionLocationUpdateCommandValidator()
        {
            //Act
            var validator = ValidationFactory.CreateValidator<DTOEducationalInstitutionLocationUpdateCommand>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionLocationUpdateCommandValidator>(validator);
        }

        [Fact]
        public void GivenAValidTypeDTOEducationalInstitutionParentUpdateCommand_ShouldReturnAnInstanceOfDTOEducationalInstitutionParentUpdateCommandValidator()
        {
            //Act
            var validator = ValidationFactory.CreateValidator<DTOEducationalInstitutionParentUpdateCommand>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionParentUpdateCommandValidator>(validator);
        }

        [Fact]
        public void GivenAValidTypeDTOEducationalInstitutionUpdateCommand_ShouldReturnAnInstanceOfDTOEducationalInstitutionUpdateCommandValidator()
        {
            //Act
            var validator = ValidationFactory.CreateValidator<DTOEducationalInstitutionUpdateCommand>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionUpdateCommandValidator>(validator);
        }

        [Fact]
        public void GivenAnInvalidType_ShouldThrowAnRequestTypeNotSupportedException()
        {
            //Assert
            Assert.Throws<RequestTypeNotSupportedException>(() => ValidationFactory.CreateValidator<StringBuilder>());
        }
    }
}