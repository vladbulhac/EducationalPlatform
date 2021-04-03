using EducationaInstitutionAPI.DTOs.Commands;
using FluentValidation;

namespace EducationaInstitutionAPI.DTOs.Validators.Commands_Validators
{
    /// <summary>
    /// Contains the validation rules for <see cref="DTOEducationalInstitutionWithParentCreateCommand"/>'s fields
    /// </summary>
    public class DTOEducationalInstitutionWithParentCommandCommandValidator : AbstractValidator<DTOEducationalInstitutionWithParentCreateCommand>
    {
        /// <summary>
        /// Initializes the rules based on which the validation is made
        /// </summary>
        public DTOEducationalInstitutionWithParentCommandCommandValidator()
        {
            CascadeMode = CascadeMode.Stop;
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
            RuleFor(v => v.Name)
                              .NotEmpty()
                              .WithMessage("{PropertyName} was empty or null!")
                              .Length(2, 128)
                              .WithMessage("{PropertyName}'s length was not between 2-128 characters!");

            RuleFor(v => v.Description)
                              .NotEmpty()
                              .WithMessage("{PropertyName} was empty or null!")
                              .Length(2, 500)
                              .WithMessage("{PropertyName}'s length was not between 2-500 characters!");
            RuleFor(v => v.ParentInstitutionID)
                              .NotEmpty()
                              .WithMessage("{PropertyName} was empty or null!");
        }
    }
}