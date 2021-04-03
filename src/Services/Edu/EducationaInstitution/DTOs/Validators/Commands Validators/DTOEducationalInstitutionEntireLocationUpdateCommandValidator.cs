using EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Commands;
using FluentValidation;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.Validators.Commands_Validators
{
    /// <summary>
    /// Contains the validation rules for <see cref="DTOEducationalInstitutionEntireLocationUpdateCommand"/>'s fields
    /// </summary>
    public class DTOEducationalInstitutionEntireLocationUpdateCommandValidator : AbstractValidator<DTOEducationalInstitutionEntireLocationUpdateCommand>
    {
        /// <summary>
        /// Initializes the rules based on which the validation is made
        /// </summary>
        public DTOEducationalInstitutionEntireLocationUpdateCommandValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(v => v.EduInstitutionID)
                              .NotEmpty()
                              .WithMessage("{PropertyName} was empty or null!");
            RuleFor(v => v.LocationID)
                                .NotEmpty()
                                .WithMessage("{PropertyName} was empty or null!")
                                .Matches(@"[a-fA-F0-9]{24}")
                                .WithMessage("{PropertyName} contains characters that are not supported and/or the length is not exactly 24!");
            RuleForEach(v => v.BuildingsIDs)
                                .NotEmpty()
                                .WithMessage("BuildingID was empty or null!")
                                .Matches(@"[a-fA-F0-9]{24}")
                                .WithMessage("BuildingID contains characters that are not supported and/or the length is not exactly 24!")
                                .NotEqual(req => req.LocationID)
                                .WithMessage("BuildingID was the same as LocationID!");
        }
    }
}