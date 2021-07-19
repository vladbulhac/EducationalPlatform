using FluentValidation;

namespace EducationalInstitution.Application.Commands.Validators
{
    public class UpdateEducationalInstitutionParentCommandValidator : AbstractValidator<UpdateEducationalInstitutionParentCommand>
    {
        public UpdateEducationalInstitutionParentCommandValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(dto => dto.EducationalInstitutionID)
                             .NotEmpty()
                             .WithMessage("{PropertyName} was empty or null!");

            RuleFor(dto => dto.ParentInstitutionID)
                             .NotEqual(v => v.EducationalInstitutionID)
                             .WithMessage(dto => $"{nameof(dto.ParentInstitutionID)} was the same as {nameof(dto.EducationalInstitutionID)}!");
        }
    }
}