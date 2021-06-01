using EducationalInstitutionAPI.DTOs.Commands;
using FluentValidation;

namespace EducationalInstitutionAPI.DTOs.Validators.Commands_Validators
{
    public class DTOEducationalInstitutionDeleteCommandValidator : AbstractValidator<DTOEducationalInstitutionDeleteCommand>
    {
        public DTOEducationalInstitutionDeleteCommandValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(dto => dto.EducationalInstitutionID)
                              .NotEmpty()
                                .WithMessage("{PropertyName} was empty or null!");
        }
    }
}