using EducationaInstitutionAPI.DTOs.Commands;
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
            RuleFor(v => v.Name)
                             .NotEmpty()
                             .WithMessage("{PropertyName} was empty or null!")
                             .Length(2, 128)
                             .WithMessage("{PropertyName}'s length was not between 2-128 characters!");

            RuleFor(v => v.Description)
                              .NotEmpty()
                              .WithMessage("{PropertyName} was empty or null!")
                              .Length(2, 500)
                              .WithMessage("{PropertyName}'s length was not between 2-500 characters!");
        }
    }
}