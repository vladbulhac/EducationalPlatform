using DataValidation.Exceptions;
using FluentValidation;
using System.Reflection;

namespace DataValidation;

/// <summary>
/// <para>Exposes a method that instantiates a validator class based on a given type.</para>
/// <para>On instantiation it searches in the given <see cref="Assembly"/> for validators.</para>
/// </summary>
/// <remarks><i>
/// Register as a Singleton service in the Dependency Injection Container so that it iterates through the assembly only once.
/// </i></remarks>
public class ValidatorFactory
{
    private readonly Dictionary<Type, Type> dtoToValidatorMap;

    public ValidatorFactory(Assembly appAssembly)
    {
        if (appAssembly is null) throw new ArgumentNullException(nameof(appAssembly));

        dtoToValidatorMap = new();

        MapDTOsToValidators(appAssembly);
    }

    /// <summary>
    /// Searches in the given <see cref="assembly">Assembly</see> for classes that inherit <see cref="AbstractValidator{T}"/> and maps them to the class dictionary.
    /// </summary>
    private void MapDTOsToValidators(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            if (type.IsClass && !type.IsAbstract && type.IsSubclassOfGeneric(typeof(AbstractValidator<>)))
                dtoToValidatorMap.Add(type.BaseType.GetGenericArguments()[0], type);
        }
    }

    /// <summary>
    /// Instantiates, based on <typeparamref name="Tdto"/>, a concrete validator class that extends <see cref="AbstractValidator{T}"/> from the <see cref="FluentValidation"/> package.
    /// </summary>
    /// <typeparam name="Tdto">A Data Transfer Object type whose fields you want to validate.</typeparam>
    /// <returns>A validator object of a class that extends <see cref="AbstractValidator{T}"/> from the <see cref="FluentValidation"/> package.</returns>
    /// <exception cref="RequestTypeNotSupportedException">Thrown when a validator of <typeparamref name="Tdto"/> has not been declared.</exception>
    public AbstractValidator<Tdto> CreateValidator<Tdto>() where Tdto : class
    {
        var Ttype = typeof(Tdto);
        if (!dtoToValidatorMap.ContainsKey(Ttype))
            throw new RequestTypeNotSupportedException(nameof(Tdto));

        Type validatorClass = dtoToValidatorMap[Ttype];
        return Activator.CreateInstance(validatorClass) as AbstractValidator<Tdto>;
    }
}