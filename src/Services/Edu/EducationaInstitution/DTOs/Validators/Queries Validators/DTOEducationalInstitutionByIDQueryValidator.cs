using EducationaInstitutionAPI.DTOs.EducationalInstitution.In;
using FluentValidation;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.Validators
{
    /// <summary>
    /// Contains the validation rules for <see cref="DTOEducationalInstitutionByIDQuery"/>'s fields
    /// </summary>
    public class DTOEducationalInstitutionByIDQueryValidator : AbstractValidator<DTOEducationalInstitutionByIDQuery>
    {
        /// <summary>
        /// Initializes the rules based on which the validation is made
        /// </summary>
        public DTOEducationalInstitutionByIDQueryValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(v => v.EduInstitutionID)
                              .NotEmpty()
                              .WithMessage("{PropertyName} was empty or null!");
        }
    }
}