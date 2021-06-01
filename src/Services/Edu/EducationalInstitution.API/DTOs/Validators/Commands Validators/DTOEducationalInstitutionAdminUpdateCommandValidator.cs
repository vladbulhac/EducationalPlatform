using EducationalInstitutionAPI.DTOs.Commands;
using FluentValidation;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.DTOs.Validators.Commands_Validators
{
    public class DTOEducationalInstitutionAdminUpdateCommandValidator : AbstractValidator<DTOEducationalInstitutionAdminUpdateCommand>
    {
        public DTOEducationalInstitutionAdminUpdateCommandValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(dto => dto.EducationalInstitutionID)
                              .NotEmpty()
                              .WithMessage("{PropertyName} was empty or null!");

            When(dto => dto.AddAdminsIDs is not null && dto.AddAdminsIDs.Count > 0, () =>
            {
                RuleFor(dto => dto.AddAdminsIDs)
                           .Must((dto, collection) => NotContainDuplicates(collection))
                               .WithMessage(dto => $"{nameof(dto.AddAdminsIDs)} contains duplicate values!");

                RuleForEach(dto => dto.AddAdminsIDs)
                            .NotEmpty()
                                .WithMessage(dto => $"{nameof(dto.AddAdminsIDs)} contains an invalid ID!");
            })
            .Otherwise(() =>
            {
                RuleFor(dto => dto.RemoveAdminsIDs)
                        .NotEmpty()
                            .WithMessage("Both AddAdminsIDs and RemoveAdminsIDs collections are empty!");
            });

            When(dto => dto.RemoveAdminsIDs is not null && dto.RemoveAdminsIDs.Count > 0, () =>
            {
                RuleFor(dto => dto.RemoveAdminsIDs)
                        .Must((dto, collection) => NotContainDuplicates(collection))
                            .WithMessage("RemoveAdminsIDs contains duplicate values!");

                RuleForEach(dto => dto.RemoveAdminsIDs)
                           .NotEmpty()
                               .WithMessage(dto => $"{nameof(dto.RemoveAdminsIDs)} contains an invalid ID!");
            });

            When(dto => dto.RemoveAdminsIDs is not null && dto.RemoveAdminsIDs.Count > 0 && dto.AddAdminsIDs is not null && dto.AddAdminsIDs.Count > 0, () =>
            {
                RuleForEach(dto => dto.RemoveAdminsIDs)
                                        .Must((dto, element) => !dto.AddAdminsIDs.Contains(element))
                                            .WithMessage((dto, element) => $"{nameof(dto.RemoveAdminsIDs)}' {element} was also found in {nameof(dto.AddAdminsIDs)}!");
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