using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.DTOs.Queries;
using EducationalInstitutionAPI.DTOs.Validators.Commands_Validators;
using EducationalInstitutionAPI.DTOs.Validators.Queries_Validators;
using EducationalInstitutionAPI.Utils.Custom_Exceptions;
using FluentValidation;

namespace EducationalInstitutionAPI.Business.Validation_Handler
{
    /// <summary>
    /// Defines a method that creates a validator object based on the type of a Data Transfer Object
    /// </summary>
    public abstract class ValidationFactory
    {
        /// <summary>
        /// Instantiates, based on <typeparamref name="T"/>, a concrete validator class that extends <see cref="AbstractValidator{T}"/> from the <see cref="FluentValidation"/> package
        /// </summary>
        /// <remarks>The project must have the FluentValidation package installed</remarks>
        /// <typeparam name="T">A Data Transfer Object type whose fields you want to validate</typeparam>
        /// <returns>A validator object of a class that extends <see cref="AbstractValidator{T}"/> from the <see cref="FluentValidation"/> package</returns>
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
            else
            if (typeof(T) == typeof(DTOEducationalInstitutionWithParentCreateCommand))
            {
                return new DTOEducationalInstitutionWithParentCreateCommandValidator() as AbstractValidator<T>;
            }
            else
             if (typeof(T) == typeof(DTOEducationalInstitutionUpdateCommand))
            {
                return new DTOEducationalInstitutionUpdateCommandValidator() as AbstractValidator<T>;
            }
            else
            if (typeof(T) == typeof(DTOEducationalInstitutionParentUpdateCommand))
            {
                return new DTOEducationalInstitutionParentUpdateCommandValidator() as AbstractValidator<T>;
            }
            else
            if (typeof(T) == typeof(DTOEducationalInstitutionLocationUpdateCommand))
            {
                return new DTOEducationalInstitutionLocationUpdateCommandValidator() as AbstractValidator<T>;
            }
            else
            if (typeof(T) == typeof(DTOEducationalInstitutionDeleteCommand))
            {
                return new DTOEducationalInstitutionDeleteCommandValidator() as AbstractValidator<T>;
            }

            throw new RequestTypeNotSupportedException(nameof(T));
        }
    }
}