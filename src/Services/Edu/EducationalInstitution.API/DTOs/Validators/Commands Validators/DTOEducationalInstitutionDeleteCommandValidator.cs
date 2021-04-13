using EducationalInstitutionAPI.DTOs.Commands;
using FluentValidation;

namespace EducationalInstitutionAPI.DTOs.Validators.Commands_Validators
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
            RuleFor(v => v.EducationalInstitutionID)
                              .NotEmpty()
                              .WithMessage("{PropertyName} was empty or null!");
        }
    }
}