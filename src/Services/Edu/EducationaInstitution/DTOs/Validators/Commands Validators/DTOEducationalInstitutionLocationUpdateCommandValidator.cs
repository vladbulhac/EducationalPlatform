using EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Commands;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.Validators.Commands_Validators
{
    /// <summary>
    /// Contains the validation rules for <see cref="DTOEducationalInstitutionLocationUpdateCommand"/>'s fields
    /// </summary>
    public class DTOEducationalInstitutionLocationUpdateCommandValidator : AbstractValidator<DTOEducationalInstitutionLocationUpdateCommand>
    {
        /// <summary>
        /// Initializes the rules based on which the validation is made
        /// </summary>
        public DTOEducationalInstitutionLocationUpdateCommandValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(v => v.EduInstitutionID)
                              .NotEmpty()
                              .WithMessage("{PropertyName} was empty or null!");

            When(v => v.UpdateBuildings == true, () =>
            {
                RuleFor(v => v.BuildingsIDs)
                                .Must((i, x) => NotContainsDuplicates(x))
                                .WithMessage("BuildingsIDs can't contain duplicates!");

                RuleForEach(v => v.BuildingsIDs)
                            .NotEmpty()
                                .WithMessage("BuildingID was empty or null!")
                            .Matches(@"[a-fA-F0-9]{24}")
                                .WithMessage("BuildingID contains characters that are not supported and/or the length is not exactly 24!")
                            .NotEqual(req => req.LocationID)
                                .When(v => v.UpdateLocation == true)
                                .WithMessage("BuildingID was the same as LocationID!");
            });

            When(v => v.UpdateLocation == true, () =>
            {
                RuleFor(v => v.LocationID)
                                   .NotEmpty()
                                    .WithMessage("{PropertyName} was empty or null!")
                                   .Matches(@"[a-fA-F0-9]{24}")
                                    .WithMessage("{PropertyName} contains characters that are not supported and/or the length is not exactly 24!");
            }).Otherwise(() =>
            {
                RuleFor(v => v.UpdateBuildings)
                            .Equal(true)
                            .WithMessage("Both location and buildings update fields are false!");
            });
        }

        private static bool NotContainsDuplicates<TElement>(ICollection<TElement> elements)
        {
            HashSet<TElement> visitedElements = new(elements.Count);
            foreach (var element in elements)
            {
                if (visitedElements.Contains(element))
                    return false;

                visitedElements.Add(element);
            }

            return true;
        }
    }
}