using EducationalInstitutionAPI.DTOs.Commands;
using FluentValidation;

namespace EducationalInstitutionAPI.DTOs.Validators.Commands_Validators
{
    public class DTOEducationalInstitutionParentUpdateCommandValidator : AbstractValidator<DTOEducationalInstitutionParentUpdateCommand>
    {
        public DTOEducationalInstitutionParentUpdateCommandValidator()
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