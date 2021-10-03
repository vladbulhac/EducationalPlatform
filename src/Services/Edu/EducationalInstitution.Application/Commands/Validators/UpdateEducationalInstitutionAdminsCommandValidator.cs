using FluentValidation;
using System.Collections.Generic;

namespace EducationalInstitution.Application.Commands.Validators
{
    public class UpdateEducationalInstitutionAdminsCommandValidator : AbstractValidator<UpdateEducationalInstitutionAdminsCommand>
    {
        public UpdateEducationalInstitutionAdminsCommandValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(dto => dto.EducationalInstitutionID)
                              .NotEmpty()
                              .WithMessage("{PropertyName} was empty or null!");

            When(dto => dto.NewAdmins is not null && dto.NewAdmins.Count > 0, () =>
            {
                RuleForEach(dto => dto.NewAdmins).SetValidator(dto => new AdminDetailsValidator(nameof(dto.NewAdmins)));
            });

            When(dto => dto.AdminsWithNewPermissions is not null && dto.AdminsWithNewPermissions.Count > 0, () =>
            {
                RuleForEach(dto => dto.AdminsWithNewPermissions).SetValidator(dto => new AdminDetailsValidator(nameof(dto.AdminsWithNewPermissions)));
            });

            When(dto => dto.AdminsWithRevokedPermissions is not null && dto.AdminsWithRevokedPermissions.Count > 0, () =>
            {
                RuleForEach(dto => dto.AdminsWithRevokedPermissions).SetValidator(dto => new AdminDetailsValidator(nameof(dto.AdminsWithRevokedPermissions)));
            })
            .Otherwise(() =>
            {
                //rule for the case when all collections are empty
                RuleFor(dto => dto.NewAdmins)
                        .NotEmpty().When(dto => dto.AdminsWithNewPermissions is null || dto.AdminsWithNewPermissions.Count == 0)
                            .WithMessage("All collections are empty!");
            });
        }

        public class AdminDetailsValidator : AbstractValidator<AdminDetails>
        {
            public AdminDetailsValidator(string fromCollection)
            {
                RuleFor(dto => dto.Identity)
                            .NotEmpty()
                                .WithMessage($"{fromCollection} contains an invalid ID!");

                RuleFor(dto => dto.Permissions)
                           .Must((collection) => NotContainDuplicates(collection))
                               .WithMessage($"{fromCollection} contains duplicate permission values!")
                           .NotEmpty()
                               .WithMessage($"{fromCollection} permissions collection is empty!");
            }
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