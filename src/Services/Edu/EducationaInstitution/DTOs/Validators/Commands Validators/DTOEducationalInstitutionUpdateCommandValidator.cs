﻿using EducationaInstitutionAPI.DTOs.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationaInstitutionAPI.DTOs.Validators.Commands_Validators
{
    public class DTOEducationalInstitutionUpdateCommandValidator : AbstractValidator<DTOEducationalInstitutionUpdateCommand>
    {
        public DTOEducationalInstitutionUpdateCommandValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(v => v.EduInstitutionID)
                              .NotEmpty()
                              .WithMessage("{PropertyName} was empty or null!");

            When(v => v.UpdateName == true, () =>
            {
                RuleFor(v => v.Name)
                               .NotEmpty()
                               .WithMessage("{PropertyName} was empty or null!")
                               .Length(2, 128)
                               .WithMessage("{PropertyName}'s length was not between 2-128 characters!");
            }).Otherwise(() =>
            {
                RuleFor(v => v.UpdateDescription)
                        .Equal(true)
                        .WithMessage("Both update fields are set to false!");
            });

            When(v => v.UpdateDescription == true, () =>
            {
                RuleFor(v => v.Description)
                             .NotEmpty()
                             .WithMessage("{PropertyName} was empty or null!")
                             .Length(2, 500)
                             .WithMessage("{PropertyName}'s length was not between 2-500 characters!");
            });
        }
    }
}