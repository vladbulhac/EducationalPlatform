using EducationalInstitutionAPI.DTOs.Commands;
using FluentValidation;

namespace EducationalInstitutionAPI.DTOs.Validators.Commands_Validators
{
    public class DTOEducationalInstitutionUpdateCommandValidator : AbstractValidator<DTOEducationalInstitutionUpdateCommand>
    {
        /// <summary>
        /// Initializes the rules based on which the validation is made
        /// </summary>
        public DTOEducationalInstitutionUpdateCommandValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(v => v.EducationalInstitutionID)
                              .NotEmpty()
                              .WithMessage("{PropertyName} was empty or null!");

            When(v => v.UpdateName == true, () =>
            {
                RuleFor(v => v.Name)
                               .NotEmpty()
                                .WithMessage("{PropertyName} was empty or null!")
                               .Length(2, 128)
                                .WithMessage("{PropertyName}'s length was not between 2-128 characters!");
            }).Otherwise(() =>
            {
                RuleFor(v => v.UpdateDescription)
                        .Equal(true)
                            .WithMessage("Both update fields are set to false!");
            });

            When(v => v.UpdateDescription == true, () =>
            {
                RuleFor(v => v.Description)
                             .NotEmpty()
                                .WithMessage("{PropertyName} was empty or null!")
                             .Length(2, 500)
                                .WithMessage("{PropertyName}'s length was not between 2-500 characters!");
            });
        }
    }
}