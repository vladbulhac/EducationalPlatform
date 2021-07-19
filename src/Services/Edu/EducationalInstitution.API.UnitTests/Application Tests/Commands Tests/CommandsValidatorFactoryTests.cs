using DataValidation;
using EducationalInstitution.Application.Commands;
using EducationalInstitution.Application.Commands.Validators;
using Xunit;

namespace EducationalInstitution.API.UnitTests.Application_Tests.Commands_Tests
{
    public class CommandsValidatorFactoryTests
    {
        private readonly ValidatorFactory validatorFactory = new(typeof(CreateEducationalInstitutionCommandValidator).Assembly);

        [Fact]
        public void GivenAValidTypeCreateEducationalInstitutionCommand_ShouldReturnAnInstanceOfCreateEducationalInstitutionCommandValidator()
        {
            //Act
            var validator = validatorFactory.CreateValidator<CreateEducationalInstitutionCommand>();

            //Assert
            Assert.IsType<CreateEducationalInstitutionCommandValidator>(validator);
        }

        [Fact]
        public void GivenAValidTypeDisableEducationalInstitutionCommand_ShouldReturnAnInstanceOfDisableEducationalInstitutionCommandValidator()
        {
            //Act
            var validator = validatorFactory.CreateValidator<DisableEducationalInstitutionCommand>();

            //Assert
            Assert.IsType<DisableEducationalInstitutionCommandValidator>(validator);
        }

        [Fact]
        public void GivenAValidTypeUpdateEducationalInstitutionLocationCommand_ShouldReturnAnInstanceOfUpdateEducationalInstitutionLocationCommandValidator()
        {
            //Act
            var validator = validatorFactory.CreateValidator<UpdateEducationalInstitutionLocationCommand>();

            //Assert
            Assert.IsType<UpdateEducationalInstitutionLocationCommandValidator>(validator);
        }

        [Fact]
        public void GivenAValidTypeUpdateEducationalInstitutionParentCommand_ShouldReturnAnInstanceOfUpdateEducationalInstitutionParentCommandValidator()
        {
            //Act
            var validator = validatorFactory.CreateValidator<UpdateEducationalInstitutionParentCommand>();

            //Assert
            Assert.IsType<UpdateEducationalInstitutionParentCommandValidator>(validator);
        }

        [Fact]
        public void GivenAValidTypeUpdateEducationalInstitutionCommand_ShouldReturnAnInstanceOfUpdateEducationalInstitutionCommandValidator()
        {
            //Act
            var validator = validatorFactory.CreateValidator<UpdateEducationalInstitutionCommand>();

            //Assert
            Assert.IsType<UpdateEducationalInstitutionCommandValidator>(validator);
        }

        [Fact]
        public void GivenAValidTypeUpdateEducationalInstitutionAdminsCommand_ShouldReturnAnInstanceOfUpdateEducationalInstitutionAdminsCommandValidator()
        {
            //Act
            var validator = validatorFactory.CreateValidator<UpdateEducationalInstitutionAdminsCommand>();

            //Assert
            Assert.IsType<UpdateEducationalInstitutionAdminsCommandValidator>(validator);
        }
    }
}