using EducationalInstitutionAPI.DTOs.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.DTOs.Validators.Queries_Validators
{
    public class DTOEducationalInstitutionsByBuildingQueryValidator : AbstractValidator<DTOEducationalInstitutionsByBuildingQuery>
    {
        public DTOEducationalInstitutionsByBuildingQueryValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(dto => dto.BuildingID)
                                .NotEmpty()
                                    .WithMessage("{PropertyName} was empty or null!")
                                .Matches(@"\b[a-fA-F0-9]{24}$")
                                    .WithMessage("{PropertyName} contains characters that are not supported and/or the length is not exactly 24!");
        }
    }
}