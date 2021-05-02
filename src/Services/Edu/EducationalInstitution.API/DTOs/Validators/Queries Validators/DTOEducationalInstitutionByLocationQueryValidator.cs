﻿using EducationalInstitutionAPI.DTOs.Queries;
using FluentValidation;

namespace EducationalInstitutionAPI.DTOs.Validators.Queries_Validators
{
    public class DTOEducationalInstitutionByLocationQueryValidator : AbstractValidator<DTOEducationalInstitutionByLocationQuery>
    {
        /// <summary>
        /// Initializes the rules based on which the validation is made
        /// </summary>
        public DTOEducationalInstitutionByLocationQueryValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(v => v.LocationID)
                                .NotEmpty()
                                    .WithMessage("{PropertyName} was empty or null!")
                                .Matches(@"\b[a-fA-F0-9]{24}$")
                                    .WithMessage("{PropertyName} contains characters that are not supported and/or the length is not exactly 24!");
        }
    }
}