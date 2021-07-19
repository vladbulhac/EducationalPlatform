using FluentValidation;

namespace EducationalInstitution.Application.Queries.Validators
{
    public class GetEducationalInstitutionByIDQueryValidator : AbstractValidator<GetEducationalInstitutionByIDQuery>
    {
        public GetEducationalInstitutionByIDQueryValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(dto => dto.EducationalInstitutionID)
                              .NotEmpty()
                                .WithMessage("{PropertyName} was empty or null!");
        }
    }
}