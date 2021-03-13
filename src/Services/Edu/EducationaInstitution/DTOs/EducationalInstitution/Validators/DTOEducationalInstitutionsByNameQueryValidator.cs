using EducationaInstitutionAPI.DTOs.EducationalInstitution.In;
using FluentValidation;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.Validators
{
    public class DTOEducationalInstitutionsByNameQueryValidator : AbstractValidator<DTOEducationalInstitutionsByNameQuery>
    {
        public DTOEducationalInstitutionsByNameQueryValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(v => v.Name)
                              .NotEmpty()
                              .WithMessage("Property {PropertyName} cannot be empty or null!")
                              .Length(2, 128)
                              .WithMessage("Property {PropertyName}'s length must be between 2-128 characters!");

            RuleFor(v => v.OffsetValue)
                .NotNull()
                .InclusiveBetween(0, 150)
                .WithMessage("Property {PropertyName} must be between 0 and 150!");

            RuleFor(v => v.ResultsCount)
                .NotNull()
                .InclusiveBetween(1, 100)
                .WithMessage("Property {PropertyName} must be between 1 and 100!");
        }
    }
}