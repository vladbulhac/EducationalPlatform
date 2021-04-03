using EducationaInstitutionAPI.DTOs.EducationalInstitution.In;
using FluentValidation;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.Validators
{
    /// <summary>
    /// Contains the validation rules for <see cref="DTOEducationalInstitutionByLocationQuery"/>'s fields
    /// </summary>
    public class DTOEducationalInstitutionByLocationQueryValidator : AbstractValidator<DTOEducationalInstitutionByLocationQuery>
    {
        /// <summary>
        /// Initializes the rules based on which the validation is made
        /// </summary>
        public DTOEducationalInstitutionByLocationQueryValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(v => v.LocationID)
                                .NotEmpty()
                                .WithMessage("{PropertyName} was empty or null!")
                                .Matches(@"[a-fA-F0-9]{24}")
                                .WithMessage("{PropertyName} contains characters that are not supported and/or the length is not exactly 24!");
        }
    }
}