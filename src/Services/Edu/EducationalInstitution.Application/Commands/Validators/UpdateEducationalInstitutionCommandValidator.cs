using FluentValidation;

namespace EducationalInstitution.Application.Commands.Validators;

public class UpdateEducationalInstitutionCommandValidator : AbstractValidator<UpdateEducationalInstitutionCommand>
{
    public UpdateEducationalInstitutionCommandValidator()
    {
        CascadeMode = CascadeMode.Stop;
        RuleFor(dto => dto.EducationalInstitutionID)
                          .NotEmpty()
                          .WithMessage("{PropertyName} was empty or null!");

        When(dto => dto.UpdateName == true, () =>
        {
            RuleFor(dto => dto.Name)
                           .NotEmpty()
                            .WithMessage("{PropertyName} was empty or null!")
                           .Length(2, 128)
                            .WithMessage("{PropertyName}'s length was not between 2-128 characters!");
        }).Otherwise(() =>
        {
            RuleFor(dto => dto.UpdateDescription)
                    .Equal(true)
                        .WithMessage("Both update fields are set to false!");
        });

        When(dto => dto.UpdateDescription == true, () =>
        {
            RuleFor(v => v.Description)
                         .NotEmpty()
                            .WithMessage("{PropertyName} was empty or null!")
                         .Length(2, 500)
                            .WithMessage("{PropertyName}'s length was not between 2-500 characters!");
        });
    }
}