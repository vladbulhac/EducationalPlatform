using EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Queries;
using FluentValidation;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.Validators
{
    public class DTOEducationalInstitutionsFromCollectionOfIDsValidator : AbstractValidator<DTOEducationalInstitutionsFromCollectionOfIDsQuery>
    {
        public DTOEducationalInstitutionsFromCollectionOfIDsValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleForEach(v => v.EducationalInstitutionsIDs)
                                 .NotEmpty()
                                 .WithMessage("{PropertyName} must not be empty and be of type GUID: https://docs.microsoft.com/en-us/dotnet/api/system.guid")
                                 .NotNull()
                                 .WithMessage("{PropertyName} must not be null and be of type GUID: https://docs.microsoft.com/en-us/dotnet/api/system.guid");
        }
    }
}