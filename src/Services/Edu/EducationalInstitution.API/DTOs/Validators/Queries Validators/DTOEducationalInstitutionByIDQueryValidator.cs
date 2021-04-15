using EducationalInstitutionAPI.DTOs.Queries;
using FluentValidation;

namespace EducationalInstitutionAPI.DTOs.Validators.Queries_Validators
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
            RuleFor(v => v.EducationalInstitutionID)
                              .NotEmpty()
                              .WithMessage("{PropertyName} was empty or null!");
        }
    }
}