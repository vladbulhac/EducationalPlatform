using EducationaInstitutionAPI.DTOs.EducationalInstitution.In;
using FluentValidation;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.Validators
{
    /// <summary>
    /// Contains the validation logic for <see cref="DTOEducationalInstitutionsByNameQuery"/>'s fields
    /// </summary>
    public class DTOEducationalInstitutionsByNameQueryValidator : AbstractValidator<DTOEducationalInstitutionsByNameQuery>
    {
        /// <summary>
        /// Initializes the rules based on which the validation is made
        /// </summary>
        public DTOEducationalInstitutionsByNameQueryValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(v => v.Name)
                              .NotEmpty()
                              .WithMessage("{PropertyName} was empty or null!")
                              .Length(2, 128)
                              .WithMessage("{PropertyName}'s length was not between 2-128 characters!");

            RuleFor(v => v.OffsetValue)
                .NotNull()
                .InclusiveBetween(0, 150)
                .WithMessage("{PropertyName} was not between 0 and 150!");

            RuleFor(v => v.ResultsCount)
                .NotNull()
                .InclusiveBetween(1, 100)
                .WithMessage("{PropertyName} was not between 1 and 100!");
        }
    }
}