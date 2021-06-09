using DataValidation.Exceptions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DataValidation
{
    /// <summary>
    /// Exposes a method that instantiates a validator class based on a given type
    /// </summary>
    public class ValidatorFactory
    {
        private readonly IDictionary<Type, Type> dtoToValidatorMap;
        private readonly Assembly assembly;

        public ValidatorFactory(Assembly appAssembly)
        {
            dtoToValidatorMap = new Dictionary<Type, Type>();
            assembly = appAssembly;
            MapDTOsToValidators();
        }

        /// <summary>
        /// Searches in the given <see cref="assembly">Assembly</see> for classes that inherit <see cref="AbstractValidator{T}"/> and maps them to the class dictionary
        /// </summary>
        private void MapDTOsToValidators()
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsClass && !type.IsAbstract && type.IsSubclassOfGeneric(typeof(AbstractValidator<>)))
                    dtoToValidatorMap.TryAdd(type.BaseType.GetGenericArguments()[0], type);
            }
        }

        /// <summary>
        /// Instantiates, based on <typeparamref name="T"/>, a concrete validator class that extends <see cref="AbstractValidator{T}"/> from the <see cref="FluentValidation"/> package
        /// </summary>
        /// <typeparam name="T">A Data Transfer Object type whose fields you want to validate</typeparam>
        /// <returns>A validator object of a class that extends <see cref="AbstractValidator{T}"/> from the <see cref="FluentValidation"/> package</returns>
        /// <exception cref="RequestTypeNotSupportedException">Thrown when a validator of <typeparamref name="T"/> has not been declared</exception>
        public AbstractValidator<T> CreateValidator<T>()
        {
            if (!dtoToValidatorMap.TryGetValue(typeof(T), out Type validatorClass))
                throw new RequestTypeNotSupportedException(nameof(T));

            return Activator.CreateInstance(validatorClass) as AbstractValidator<T>;
        }
    }

    public static class TypeExtensionMethods
    {
        /// <summary>
        /// Searches for <paramref name="lookedUpGenericType"/> in the inherited classes by <paramref name="type"/>
        /// </summary>
        public static bool IsSubclassOfGeneric(this Type type, Type lookedUpGenericType)
        {
            while (type is not null && type != typeof(object))
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == lookedUpGenericType)
                    return true;

                type = type.BaseType;
            }
            return false;
        }
    }
}