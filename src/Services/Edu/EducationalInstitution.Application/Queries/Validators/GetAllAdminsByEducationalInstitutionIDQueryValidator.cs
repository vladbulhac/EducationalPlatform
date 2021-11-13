using FluentValidation;

namespace EducationalInstitution.Application.Queries.Validators;

public class GetAllAdminsByEducationalInstitutionIDQueryValidator : AbstractValidator<GetAllAdminsByEducationalInstitutionIDQuery>
{
    public GetAllAdminsByEducationalInstitutionIDQueryValidator()
    {
        CascadeMode = CascadeMode.Stop;
        RuleFor(dto => dto.EducationalInstitutionID)
                    .NotEmpty()
                        .WithMessage(dto => $"{nameof(dto.EducationalInstitutionID)} is empty, null or default!");
    }
}