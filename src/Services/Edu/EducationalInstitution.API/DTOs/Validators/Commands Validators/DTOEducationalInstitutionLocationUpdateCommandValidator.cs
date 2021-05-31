using EducationalInstitutionAPI.DTOs.Commands;
using FluentValidation;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.DTOs.Validators.Commands_Validators
{
    public class DTOEducationalInstitutionLocationUpdateCommandValidator : AbstractValidator<DTOEducationalInstitutionLocationUpdateCommand>
    {
        /// <summary>
        /// Initializes the rules based on which the validation is made
        /// </summary>
        public DTOEducationalInstitutionLocationUpdateCommandValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(dto => dto.EducationalInstitutionID)
                              .NotEmpty()
                              .WithMessage("{PropertyName} was empty or null!");

            When(dto => dto.UpdateBuildings == true, () =>
            {
                When(dto => dto.AddBuildingsIDs is not null && dto.AddBuildingsIDs.Count > 0, () =>
                {
                    RuleFor(dto => dto.AddBuildingsIDs)
                                   .Must((dto, collection) => NotContainDuplicates(collection))
                                    .WithMessage(dto => $"{nameof(dto.AddBuildingsIDs)} can't contain duplicates!");

                    RuleForEach(dto => dto.AddBuildingsIDs)
                                    .NotEmpty()
                                        .WithMessage("BuildingID was empty or null!")
                                    .Matches(@"\b[a-fA-F0-9]{24}$")
                                        .WithMessage("BuildingID contains characters that are not supported and/or the length is not exactly 24!")
                                    .NotEqual(dto => dto.LocationID)
                                        .When(dto => dto.UpdateLocation == true)
                                        .WithMessage(dto => $"BuildingID was the same as {nameof(dto.LocationID)}!");
                });

                When(dto => dto.RemoveBuildingsIDs is not null && dto.RemoveBuildingsIDs.Count > 0, () =>
                {
                    RuleFor(dto => dto.RemoveBuildingsIDs)
                                 .Must((dto, collection) => NotContainDuplicates(collection))
                                  .WithMessage(dto => $"{nameof(dto.RemoveBuildingsIDs)} can't contain duplicates!");

                    RuleForEach(dto => dto.RemoveBuildingsIDs)
                                .NotEmpty()
                                    .WithMessage("BuildingID was empty or null!")
                                .Matches(@"\b[a-fA-F0-9]{24}$")
                                    .WithMessage("BuildingID contains characters that are not supported and/or the length is not exactly 24!")
                                .NotEqual(dto => dto.LocationID)
                                    .When(dto => dto.UpdateLocation == true)
                                    .WithMessage(dto => $"BuildingID was the same as {nameof(dto.LocationID)}!");
                }).Otherwise(() =>
                {
                    RuleFor(dto => dto.AddBuildingsIDs)
                                      .NotEmpty()
                                          .WithMessage(dto => $"Both {nameof(dto.AddBuildingsIDs)} and {nameof(dto.RemoveBuildingsIDs)} collections are empty!");
                });

                When(dto => dto.AddBuildingsIDs is not null && dto.AddBuildingsIDs.Count > 0 && dto.RemoveBuildingsIDs is not null && dto.RemoveBuildingsIDs.Count > 0, () =>
                {
                    RuleForEach(dto => dto.AddBuildingsIDs)
                            .Must((dto, element) => !dto.RemoveBuildingsIDs.Contains(element))
                                .WithMessage((dto, element) => $"{nameof(dto.AddBuildingsIDs)}' {element} was also found in {nameof(dto.RemoveBuildingsIDs)}!");
                });
            });

            When(dto => dto.UpdateLocation == true, () =>
            {
                RuleFor(dto => dto.LocationID)
                                   .NotEmpty()
                                    .WithMessage("{PropertyName} was empty or null!")
                                   .Matches(@"\b[a-fA-F0-9]{24}$")
                                    .WithMessage("{PropertyName} contains characters that are not supported and/or the length is not exactly 24!");
            }).Otherwise(() =>
            {
                RuleFor(dto => dto.UpdateBuildings)
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