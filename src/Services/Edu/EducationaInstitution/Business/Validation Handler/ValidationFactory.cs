using EducationaInstitutionAPI.DTOs.EducationalInstitution.In;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Queries;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.Validators;
using FluentValidation;
using System;

namespace EducationaInstitutionAPI.Business.Validation_Handler
{
    public abstract class ValidationFactory
    {
        protected AbstractValidator<T> CreateValidator<T>()
        {
            if (typeof(T) == typeof(DTOEducationalInstitutionByIDQuery))
            {
                return new DTOEducationalInstitutionByIDQueryValidator() as AbstractValidator<T>;
            }
            else
            if (typeof(T) == typeof(DTOEducationalInstitutionsFromCollectionOfIDsQuery))
            {
                return new DTOEducationalInstitutionsFromCollectionOfIDsValidator() as AbstractValidator<T>;
            }
            else
            if (typeof(T) == typeof(DTOEducationalInstitutionsByNameQuery))
            {
                return new DTOEducationalInstitutionsByNameQueryValidator() as AbstractValidator<T>;
            }
            else
            if(typeof(T)==typeof(DTOEducationalInstitutionByLocationQuery))
            {
                return new DTOEducationalInstitutionByLocationQueryValidator() as AbstractValidator<T>;
            }

            throw new Exception("Object Type not supported!");
        }
    }
}