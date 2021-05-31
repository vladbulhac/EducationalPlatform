using EducationalInstitutionAPI.DTOs.Queries;
using FluentValidation;

namespace EducationalInstitutionAPI.DTOs.Validators.Queries_Validators
{
    public class DTOEducationalInstitutionByIDQueryValidator : AbstractValidator<DTOEducationalInstitutionByIDQuery>
    {
        /// <summary>
        /// Initializes the rules based on which the validation is made
        /// </summary>
        public DTOEducationalInstitutionByIDQueryValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(dto => dto.EducationalInstitutionID)
                              .NotEmpty()
                                .WithMessage("{PropertyName} was empty or null!");
        }
    }
}