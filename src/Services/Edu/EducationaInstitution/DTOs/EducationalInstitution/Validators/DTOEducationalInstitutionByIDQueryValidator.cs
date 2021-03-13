using EducationaInstitutionAPI.DTOs.EducationalInstitution.In;
using FluentValidation;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.Validators
{
    public class DTOEducationalInstitutionByIDQueryValidator : AbstractValidator<DTOEducationalInstitutionByIDQuery>
    {
        public DTOEducationalInstitutionByIDQueryValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(v => v.EduInstitutionID)
                              .NotEmpty()
                              .WithMessage("{PropertyName} must not be empty and be of type GUID: https://docs.microsoft.com/en-us/dotnet/api/system.guid")
                              .NotNull()
                              .WithMessage("{PropertyName} must not be null and be of type GUID: https://docs.microsoft.com/en-us/dotnet/api/system.guid");
        }
    }
}