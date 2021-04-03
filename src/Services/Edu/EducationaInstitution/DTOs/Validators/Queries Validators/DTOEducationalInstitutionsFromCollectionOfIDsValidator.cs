using EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Queries;
using FluentValidation;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.Validators
{
    /// <summary>
    /// Contains the validation rules for <see cref="DTOEducationalInstitutionsByNameQuery"/>'s fields
    /// </summary>
    public class DTOEducationalInstitutionsFromCollectionOfIDsValidator : AbstractValidator<DTOEducationalInstitutionsFromCollectionOfIDsQuery>
    {
        /// <summary>
        /// Initializes the rules based on which the validation is made
        /// </summary>
        public DTOEducationalInstitutionsFromCollectionOfIDsValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleForEach(v => v.EducationalInstitutionsIDs)
                                 .NotEmpty()
                                 .WithMessage("{PropertyName} was empty or null!");
        }
    }
}