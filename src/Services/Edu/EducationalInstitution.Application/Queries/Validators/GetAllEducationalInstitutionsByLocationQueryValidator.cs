using FluentValidation;

namespace EducationalInstitution.Application.Queries.Validators;

public class GetAllEducationalInstitutionsByLocationQueryValidator : AbstractValidator<GetAllEducationalInstitutionsByLocationQuery>
{
    public GetAllEducationalInstitutionsByLocationQueryValidator()
    {
        CascadeMode = CascadeMode.Stop;
        RuleFor(dto => dto.LocationID)
                            .NotEmpty()
                                .WithMessage("{PropertyName} was empty or null!")
                            .Matches(@"\b[a-fA-F0-9]{24}$")
                                .WithMessage("{PropertyName} contains characters that are not supported and/or the length is not exactly 24!");
    }
}