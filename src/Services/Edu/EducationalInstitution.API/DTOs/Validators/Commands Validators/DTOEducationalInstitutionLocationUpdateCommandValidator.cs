using EducationalInstitutionAPI.DTOs.Commands;
using FluentValidation;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.DTOs.Validators.Commands_Validators
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
            RuleFor(v => v.EducationalInstitutionID)
                              .NotEmpty()
                              .WithMessage("{PropertyName} was empty or null!");

            When(v => v.UpdateBuildings == true, () =>
            {
                When(v => v.AddBuildingsIDs is not null && v.AddBuildingsIDs.Count > 0, () =>
                {
                    RuleFor(v => v.AddBuildingsIDs)
                                   .Must((i, x) => NotContainDuplicates(x))
                                    .WithMessage("AddBuildingsIDs can't contain duplicates!");

                    RuleForEach(v => v.AddBuildingsIDs)
                                    .NotEmpty()
                                        .WithMessage("BuildingID was empty or null!")
                                    .Matches(@"\b[a-fA-F0-9]{24}$")
                                        .WithMessage("BuildingID contains characters that are not supported and/or the length is not exactly 24!")
                                    .NotEqual(req => req.LocationID)
                                        .When(v => v.UpdateLocation == true)
                                        .WithMessage("BuildingID was the same as LocationID!");
                });

                When(v => v.RemoveBuildingsIDs is not null && v.RemoveBuildingsIDs.Count > 0, () =>
                {
                    RuleFor(v => v.RemoveBuildingsIDs)
                                 .Must((i, x) => NotContainDuplicates(x))
                                  .WithMessage("BuildingsIDs can't contain duplicates!");

                    RuleForEach(v => v.RemoveBuildingsIDs)
                                .NotEmpty()
                                    .WithMessage("BuildingID was empty or null!")
                                .Matches(@"\b[a-fA-F0-9]{24}$")
                                    .WithMessage("BuildingID contains characters that are not supported and/or the length is not exactly 24!")
                                .NotEqual(req => req.LocationID)
                                    .When(v => v.UpdateLocation == true)
                                    .WithMessage("BuildingID was the same as LocationID!");
                }).Otherwise(() =>
                {
                    RuleFor(v => v.AddBuildingsIDs)
                                      .NotEmpty()
                                          .WithMessage("Both AddBuildingsIDs and RemoveBuildingsIDs collections are empty!");
                });
            });

            When(v => v.UpdateLocation == true, () =>
            {
                RuleFor(v => v.LocationID)
                                   .NotEmpty()
                                    .WithMessage("{PropertyName} was empty or null!")
                                   .Matches(@"\b[a-fA-F0-9]{24}$")
                                    .WithMessage("{PropertyName} contains characters that are not supported and/or the length is not exactly 24!");
            }).Otherwise(() =>
            {
                RuleFor(v => v.UpdateBuildings)
                            .Equal(true)
                                .WithMessage("Both location and buildings update fields are false!");
            });
        }

        private static bool NotContainDuplicates<TElement>(ICollection<TElement> elements)
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