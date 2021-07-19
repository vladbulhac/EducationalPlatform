using FluentValidation;
using System;

namespace EducationalInstitution.Application.Commands.Validators
{
    public class CreateEducationalInstitutionCommandValidator : AbstractValidator<CreateEducationalInstitutionCommand>
    {
        public CreateEducationalInstitutionCommandValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(dto => dto.LocationID)
                                .NotEmpty()
                                    .WithMessage("{PropertyName} was empty or null!")
                                .Matches(@"\b[a-fA-F0-9]{24}$")
                                    .WithMessage("{PropertyName} contains characters that are not supported and/or the length is not exactly 24!");
            RuleForEach(dto => dto.BuildingsIDs)
                                .NotEmpty()
                                    .WithMessage("BuildingID was empty or null!")
                                .Matches(@"\b[a-fA-F0-9]{24}$")
                                    .WithMessage("BuildingID contains characters that are not supported and/or the length is not exactly 24!")
                                .NotEqual(req => req.LocationID)
                                    .WithMessage(dto => $"BuildingID was the same as {nameof(dto.LocationID)}!");
            RuleFor(dto => dto.BuildingsIDs)
                                .NotEmpty()
                                    .WithMessage(dto => $"{nameof(dto.BuildingsIDs)} was empty or null!");

            RuleForEach(dto => dto.AdminsIDs)
                                .NotEmpty()
                                    .WithMessage("AdminID was empty or null!")
                                .NotEqual(Guid.Empty)
                                    .WithMessage("AdminID was default!");
            RuleFor(dto => dto.AdminsIDs)
                                .NotEmpty()
                                    .WithMessage("AdminsIDs was empty or null!");

            RuleFor(dto => dto.Name)
                              .NotEmpty()
                                .WithMessage("{PropertyName} was empty or null!")
                              .Length(2, 128)
                                .WithMessage("{PropertyName}'s length was not between 2-128 characters!");

            RuleFor(dto => dto.Description)
                              .NotEmpty()
                                .WithMessage("{PropertyName} was empty or null!")
                              .Length(2, 500)
                                .WithMessage("{PropertyName}'s length was not between 2-500 characters!");
        }
    }
}