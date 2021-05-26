using EducationalInstitutionAPI.DTOs.Commands;
using FluentValidation;

namespace EducationalInstitutionAPI.DTOs.Validators.Commands_Validators
{
    public class DTOEducationalInstitutionParentUpdateCommandValidator : AbstractValidator<DTOEducationalInstitutionParentUpdateCommand>
    {
        /// <summary>
        /// Initializes the rules based on which the validation is made
        /// </summary>
        public DTOEducationalInstitutionParentUpdateCommandValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(v => v.EducationalInstitutionID)
                             .NotEmpty()
                             .WithMessage("{PropertyName} was empty or null!");

            RuleFor(v => v.ParentInstitutionID)
                             .NotEqual(v => v.EducationalInstitutionID)
                             .WithMessage("Parent Institution ID was the same as Educational Institution ID!");
        }
    }
}