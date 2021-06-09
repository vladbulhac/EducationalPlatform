using DataValidation;
using DataValidation.Exceptions;
using EducationalInstitutionAPI;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.DTOs.Queries;
using EducationalInstitutionAPI.DTOs.Validators.Commands_Validators;
using EducationalInstitutionAPI.DTOs.Validators.Queries_Validators;
using System.Text;
using Xunit;

namespace EducationalInstitution.API.UnitTests.BusinessTests.Validation_Handler_Tests
{
    public class ValidatorFactoryTests
    {
        private readonly ValidatorFactory validatorFactory = new(typeof(Startup).Assembly);

        [Fact]
        public void GivenAValidTypeDTOEducationalInstitutionByIDQuery_ShouldReturnAnInstanceOfDTOEducationalInstitutionByIDQueryValidator()
        {
            //Act
            var validator = validatorFactory.CreateValidator<DTOEducationalInstitutionByIDQuery>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionByIDQueryValidator>(validator);
        }

        [Fact]
        public void GivenAValidTypeDTOEducationalInstitutionByLocationQuery_ShouldReturnAnInstanceOfDTOEducationalInstitutionByLocationQueryValidator()
        {
            //Act
            var validator = validatorFactory.CreateValidator<DTOEducationalInstitutionsByLocationQuery>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionByLocationQueryValidator>(validator);
        }

        [Fact]
        public void GivenAValidTypeDTOEducationalInstitutionsByNameQuery_ShouldReturnAnInstanceOfDTOEducationalInstitutionsByNameQueryValidator()
        {
            //Act
            var validator = validatorFactory.CreateValidator<DTOEducationalInstitutionsByNameQuery>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionsByNameQueryValidator>(validator);
        }

        [Fact]
        public void GivenAValidTypeDTOAdminsByEducationalInstitutionIdQuery_ShouldReturnAnInstanceOfDTOAdminsByEducationalInstitutionIDQueryValidator()
        {
            //Act
            var validator = validatorFactory.CreateValidator<DTOAdminsByEducationalInstitutionIDQuery>();

            //Assert
            Assert.IsType<DTOAdminsByEducationalInstitutionIDQueryValidator>(validator);
        }

        [Fact]
        public void GivenAValidTypeDTOEducationalInstitutionsByBuildingQuery_ShouldReturnAnInstanceOfDTOEducationalInstitutionsByBuildingQueryValidator()
        {
            //Act
            var validator = validatorFactory.CreateValidator<DTOEducationalInstitutionsByBuildingQuery>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionsByBuildingQueryValidator>(validator);
        }

        [Fact]
        public void GivenAValidTypeDTOEducationalInstitutionCreateCommand_ShouldReturnAnInstanceOfDTOEducationalInstitutionCreateCommandValidator()
        {
            //Act
            var validator = validatorFactory.CreateValidator<DTOEducationalInstitutionCreateCommand>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionCreateCommandValidator>(validator);
        }

        [Fact]
        public void GivenAValidTypeDTOEducationalInstitutionDeleteCommand_ShouldReturnAnInstanceOfDTOEducationalInstitutionDeleteCommandValidator()
        {
            //Act
            var validator = validatorFactory.CreateValidator<DTOEducationalInstitutionDeleteCommand>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionDeleteCommandValidator>(validator);
        }

        [Fact]
        public void GivenAValidTypeDTOEducationalInstitutionLocationUpdateCommand_ShouldReturnAnInstanceOfDTOEducationalInstitutionLocationUpdateCommandValidator()
        {
            //Act
            var validator = validatorFactory.CreateValidator<DTOEducationalInstitutionLocationUpdateCommand>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionLocationUpdateCommandValidator>(validator);
        }

        [Fact]
        public void GivenAValidTypeDTOEducationalInstitutionParentUpdateCommand_ShouldReturnAnInstanceOfDTOEducationalInstitutionParentUpdateCommandValidator()
        {
            //Act
            var validator = validatorFactory.CreateValidator<DTOEducationalInstitutionParentUpdateCommand>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionParentUpdateCommandValidator>(validator);
        }

        [Fact]
        public void GivenAValidTypeDTOEducationalInstitutionUpdateCommand_ShouldReturnAnInstanceOfDTOEducationalInstitutionUpdateCommandValidator()
        {
            //Act
            var validator = validatorFactory.CreateValidator<DTOEducationalInstitutionUpdateCommand>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionUpdateCommandValidator>(validator);
        }

        [Fact]
        public void GivenAValidTypeDTOEducationalInstitutionAdminUpdateCommand_ShouldReturnAnInstanceOfDTOEducationalInstitutionAdminUpdateCommandValidator()
        {
            //Act
            var validator = validatorFactory.CreateValidator<DTOEducationalInstitutionAdminUpdateCommand>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionAdminUpdateCommandValidator>(validator);
        }

        [Fact]
        public void GivenAnInvalidType_ShouldThrowAnRequestTypeNotSupportedException()
        {
            //Assert
            Assert.Throws<RequestTypeNotSupportedException>(() => validatorFactory.CreateValidator<StringBuilder>());
        }
    }
}