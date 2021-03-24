using EducationaInstitutionAPI.DTOs.EducationalInstitution.In;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Commands;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Queries;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.Validators;
using EducationaInstitutionAPI.Utils.Custom_Exceptions;
using FluentValidation;

namespace EducationaInstitutionAPI.Business.Validation_Handler
{
    /// <summary>
    /// Defines a method that creates a validator object based on the type of a Data Transfer Object
    /// </summary>
    public abstract class ValidationFactory
    {
        /// <summary>
        /// Instantiates, based on <typeparamref name="T"/>, a concrete validator class that extends the generic AbstractValidator class from the FluentValidation package
        /// </summary>
        /// <remarks>The project must have the FluentValidation package installed</remarks>
        /// <typeparam name="T">A Data Transfer Object type whose fields you want to validate</typeparam>
        /// <returns>A validator object of a class that extends the generic AbstractValidator class from the FluentValidation package</returns>
        /// <exception cref="RequestTypeNotSupportedException">Thrown when a validator of <typeparamref name="T"/> has not been declared</exception>
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
            if (typeof(T) == typeof(DTOEducationalInstitutionByLocationQuery))
            {
                return new DTOEducationalInstitutionByLocationQueryValidator() as AbstractValidator<T>;
            }
            if (typeof(T) == typeof(DTOEducationalInstitutionCreateCommand))
            {
                return new DTOEducationalInstitutionCreateCommandValidator() as AbstractValidator<T>;
            }

            throw new RequestTypeNotSupportedException(nameof(T));
        }
    }
}