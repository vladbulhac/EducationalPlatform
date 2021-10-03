using FluentValidation;
using System;
using System.Collections.Generic;

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
                                    .WithMessage("Building ID was empty or null!")
                                .Matches(@"\b[a-fA-F0-9]{24}$")
                                    .WithMessage("BuildingID contains characters that are not supported and/or the length is not exactly 24!")
                                .NotEqual(req => req.LocationID)
                                    .WithMessage(dto => $"Building ID was the same as {nameof(dto.LocationID)}!");
            RuleFor(dto => dto.BuildingsIDs)
                                .NotEmpty()
                                    .WithMessage("Buildings IDs collection was empty or null!");

            RuleFor(dto => dto.AdminId)
                                .NotEmpty()
                                    .WithMessage("{PropertyName} was empty or null!")
                                .NotEqual(Guid.Empty.ToString())
                                    .WithMessage("{PropertyName} does not have a valid value!");

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