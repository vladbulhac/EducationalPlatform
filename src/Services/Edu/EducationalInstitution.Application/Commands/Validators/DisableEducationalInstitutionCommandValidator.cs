using FluentValidation;

namespace EducationalInstitution.Application.Commands.Validators
{
    public class DisableEducationalInstitutionCommandValidator : AbstractValidator<DisableEducationalInstitutionCommand>
    {
        public DisableEducationalInstitutionCommandValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(dto => dto.EducationalInstitutionID)
                              .NotEmpty()
                                .WithMessage("{PropertyName} was empty or null!");
        }
    }
}