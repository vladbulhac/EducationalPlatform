using EducationalInstitutionAPI.DTOs.Queries;
using FluentValidation;

namespace EducationalInstitutionAPI.DTOs.Validators.Queries_Validators
{
    public class DTOAdminsByEducationalInstitutionIDQueryValidator : AbstractValidator<DTOAdminsByEducationalInstitutionIDQuery>
    {
        public DTOAdminsByEducationalInstitutionIDQueryValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(dto => dto.EducationalInstitutionID)
                        .NotEmpty()
                            .WithMessage(dto => $"{nameof(dto.EducationalInstitutionID)} is empty, null or default!");
        }
    }
}