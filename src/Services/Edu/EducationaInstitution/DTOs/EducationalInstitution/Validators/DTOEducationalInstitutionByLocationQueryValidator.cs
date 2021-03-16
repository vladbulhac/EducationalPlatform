using EducationaInstitutionAPI.DTOs.EducationalInstitution.In;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.Validators
{
    public class DTOEducationalInstitutionByLocationQueryValidator:AbstractValidator<DTOEducationalInstitutionByLocationQuery>
    {
        public DTOEducationalInstitutionByLocationQueryValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(v => v.LocationID)
                                .NotEmpty()
                                .WithMessage("{PropertyName} must not be empty and be of type string!")
                                .Matches(@"[a-fA-F0-9]{24}")
                                .WithMessage("{PropertyName} contains characters that are not supported and must be exactly of length 24!");
        }
    }
}
