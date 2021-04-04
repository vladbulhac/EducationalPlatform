using EducationaInstitutionAPI.DTOs.Commands;
using FluentValidation;

namespace EducationaInstitutionAPI.DTOs.Validators.Commands_Validators
{
    /// <summary>
    /// Contains the validation rules for <see cref="DTOEducationalInstitutionDeleteCommand"/>'s fields
    /// </summary>
    public class DTOEducationalInstitutionDeleteCommandValidator : AbstractValidator<DTOEducationalInstitutionDeleteCommand>
    {
        /// <summary>
        /// Initializes the rules based on which the validation is made
        /// </summary>
        public DTOEducationalInstitutionDeleteCommandValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(v => v.EduInstitutionID)
                              .NotEmpty()
                              .WithMessage("{PropertyName} was empty or null!");
        }
    }
}