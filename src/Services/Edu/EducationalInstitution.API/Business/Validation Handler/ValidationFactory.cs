using EducationalInstitutionAPI.Utils.Custom_Exceptions;
using FluentValidation;
using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace EducationalInstitutionAPI.Business.Validation_Handler
{
    /// <summary>
    /// Defines a method that creates a validator object based on the type of a Data Transfer Object
    /// </summary>
    public static class ValidationFactory
    {
        private static readonly ConcurrentDictionary<Type, Type> dtoToValidatorMap;

        static ValidationFactory()
        {
            dtoToValidatorMap = new();
            MapDTOsToValidators();
        }

        /// <summary>
        /// Searches in the current executing <see cref="Assembly"/> for classes that inherit <see cref="AbstractValidator{T}"/> and maps them to the class dictionary
        /// </summary>
        private static void MapDTOsToValidators()
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsClass && !type.IsAbstract && IsSubclassOfGeneric(type, typeof(AbstractValidator<>)))
                    dtoToValidatorMap.TryAdd(type.BaseType.GetGenericArguments()[0], type);
            }
        }

        /// <summary>
        /// Searches for <paramref name="genericType"/> in the collection of inherited classes by <paramref name="typeToCheck"/>
        /// </summary>
        /// <see cref="https://stackoverflow.com/questions/457676/check-if-a-class-is-derived-from-a-generic-class"/>
        private static bool IsSubclassOfGeneric(Type typeToCheck, Type genericType)
        {
            while (typeToCheck != null && typeToCheck != typeof(object))
            {
                var currentType = typeToCheck.IsGenericType ? typeToCheck.GetGenericTypeDefinition() : typeToCheck;
                if (genericType == currentType) return true;

                typeToCheck = typeToCheck.BaseType;
            }
            return false;
        }

        /// <summary>
        /// Instantiates, based on <typeparamref name="T"/>, a concrete validator class that extends <see cref="AbstractValidator{T}"/> from the <see cref="FluentValidation"/> package
        /// </summary>
        /// <remarks>The project must have the FluentValidation package installed</remarks>
        /// <typeparam name="T">A Data Transfer Object type whose fields you want to validate</typeparam>
        /// <returns>A validator object of a class that extends <see cref="AbstractValidator{T}"/> from the <see cref="FluentValidation"/> package</returns>
        /// <exception cref="RequestTypeNotSupportedException">Thrown when a validator of <typeparamref name="T"/> has not been declared</exception>
        public static AbstractValidator<T> CreateValidator<T>()
        {
            if (!dtoToValidatorMap.TryGetValue(typeof(T), out Type validatorClass))
                throw new RequestTypeNotSupportedException(nameof(T));

            return Activator.CreateInstance(validatorClass) as AbstractValidator<T>;
        }
    }
}