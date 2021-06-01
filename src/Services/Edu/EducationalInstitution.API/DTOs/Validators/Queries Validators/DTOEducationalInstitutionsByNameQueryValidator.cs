﻿using EducationalInstitutionAPI.DTOs.Queries;
using FluentValidation;

namespace EducationalInstitutionAPI.DTOs.Validators.Queries_Validators
{
    public class DTOEducationalInstitutionsByNameQueryValidator : AbstractValidator<DTOEducationalInstitutionsByNameQuery>
    {
        public DTOEducationalInstitutionsByNameQueryValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(dto => dto.Name)
                              .NotEmpty()
                                .WithMessage("{PropertyName} was empty or null!")
                              .Length(2, 128)
                                .WithMessage("{PropertyName}'s length was not between 2-128 characters!");

            RuleFor(dto => dto.OffsetValue)
                .NotNull()
                .InclusiveBetween(0, 150)
                    .WithMessage("{PropertyName} was not between 0 and 150!");

            RuleFor(dto => dto.ResultsCount)
                .NotNull()
                .InclusiveBetween(1, 100)
                    .WithMessage("{PropertyName} was not between 1 and 100!");
        }
    }
}