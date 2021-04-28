using EducationalInstitutionAPI.Business.Validation_Handler;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.DTOs.Queries;
using EducationalInstitutionAPI.DTOs.Validators.Commands_Validators;
using EducationalInstitutionAPI.DTOs.Validators.Queries_Validators;
using EducationalInstitutionAPI.Utils.Custom_Exceptions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.UnitTests.BusinessTests.Validation_Handler_Tests
{
    public class ValidationFactoryTests
    {
        [Fact]
        public async Task GivenAValidTypeDTOEducationalInstitutionByIDQuery_ShouldReturnAnInstanceOfDTOEducationalInstitutionByIDQueryValidator()
        {
            //Act
            var validator = ValidationFactory.CreateValidator<DTOEducationalInstitutionByIDQuery>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionByIDQueryValidator>(validator);
        }

        [Fact]
        public async Task GivenAValidTypeDTOEducationalInstitutionByLocationQuery_ShouldReturnAnInstanceOfDTOEducationalInstitutionByLocationQueryValidator()
        {
            //Act
            var validator = ValidationFactory.CreateValidator<DTOEducationalInstitutionByLocationQuery>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionByLocationQueryValidator>(validator);
        }

        [Fact]
        public async Task GivenAValidTypeDTOEducationalInstitutionsByNameQuery_ShouldReturnAnInstanceOfDTOEducationalInstitutionsByNameQueryValidator()
        {
            //Act
            var validator = ValidationFactory.CreateValidator<DTOEducationalInstitutionsByNameQuery>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionsByNameQueryValidator>(validator);
        }

        [Fact]
        public async Task GivenAValidTypeDTOEducationalInstitutionsFromCollectionOfIDsQuery_ShouldReturnAnInstanceOfDTOEducationalInstitutionsFromCollectionOfIDsQueryValidator()
        {
            //Act
            var validator = ValidationFactory.CreateValidator<DTOEducationalInstitutionsFromCollectionOfIDsQuery>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionsFromCollectionOfIDsQueryValidator>(validator);
        }

        [Fact]
        public async Task GivenAValidTypeDTOEducationalInstitutionCreateCommand_ShouldReturnAnInstanceOfDTOEducationalInstitutionCreateCommandValidator()
        {
            //Act
            var validator = ValidationFactory.CreateValidator<DTOEducationalInstitutionCreateCommand>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionCreateCommandValidator>(validator);
        }

        [Fact]
        public async Task GivenAValidTypeDTOEducationalInstitutionDeleteCommand_ShouldReturnAnInstanceOfDTOEducationalInstitutionDeleteCommandValidator()
        {
            //Act
            var validator = ValidationFactory.CreateValidator<DTOEducationalInstitutionDeleteCommand>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionDeleteCommandValidator>(validator);
        }

        [Fact]
        public async Task GivenAValidTypeDTOEducationalInstitutionLocationUpdateCommand_ShouldReturnAnInstanceOfDTOEducationalInstitutionLocationUpdateCommandValidator()
        {
            //Act
            var validator = ValidationFactory.CreateValidator<DTOEducationalInstitutionLocationUpdateCommand>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionLocationUpdateCommandValidator>(validator);
        }

        [Fact]
        public async Task GivenAValidTypeDTOEducationalInstitutionParentUpdateCommand_ShouldReturnAnInstanceOfDTOEducationalInstitutionParentUpdateCommandValidator()
        {
            //Act
            var validator = ValidationFactory.CreateValidator<DTOEducationalInstitutionParentUpdateCommand>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionParentUpdateCommandValidator>(validator);
        }

        [Fact]
        public async Task GivenAValidTypeDTOEducationalInstitutionUpdateCommand_ShouldReturnAnInstanceOfDTOEducationalInstitutionUpdateCommandValidator()
        {
            //Act
            var validator = ValidationFactory.CreateValidator<DTOEducationalInstitutionUpdateCommand>();

            //Assert
            Assert.IsType<DTOEducationalInstitutionUpdateCommandValidator>(validator);
        }

        [Fact]
        public async Task GivenAnInvalidType_ShouldThrowAnRequestTypeNotSupportedException()
        {
            //Assert
            Assert.Throws<RequestTypeNotSupportedException>(() => ValidationFactory.CreateValidator<StringBuilder>());
        }
    }
}