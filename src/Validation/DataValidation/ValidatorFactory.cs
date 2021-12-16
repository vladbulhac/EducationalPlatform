using DataValidation.Exceptions;
using FluentValidation;
using System.Reflection;

namespace DataValidation;

/// <summary>
/// <para>Exposes a method that instantiates a validator class based on a given type</para>
/// <para>On instantiation it searches in the given <see cref="Assembly"/> for validators</para>
/// </summary>
/// <remarks><i>
/// Must be registered as a Singleton service in the Dependency Injection Container
/// </i></remarks>
public class ValidatorFactory
{
    private readonly Dictionary<Type, Type> dtoToValidatorMap;
    private readonly Assembly assembly;

    public ValidatorFactory(Assembly appAssembly)
    {
        assembly = appAssembly ?? throw new ArgumentNullException(nameof(appAssembly));
        dtoToValidatorMap = new();

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
                dtoToValidatorMap.Add(type.BaseType.GetGenericArguments()[0], type);
        }
    }

    /// <summary>
    /// Instantiates, based on <typeparamref name="Tdto"/>, a concrete validator class that extends <see cref="AbstractValidator{T}"/> from the <see cref="FluentValidation"/> package
    /// </summary>
    /// <typeparam name="Tdto">A Data Transfer Object type whose fields you want to validate</typeparam>
    /// <returns>A validator object of a class that extends <see cref="AbstractValidator{T}"/> from the <see cref="FluentValidation"/> package</returns>
    /// <exception cref="RequestTypeNotSupportedException">Thrown when a validator of <typeparamref name="Tdto"/> has not been declared</exception>
    public AbstractValidator<Tdto> CreateValidator<Tdto>() where Tdto : class
    {
        var Ttype = typeof(Tdto);
        if (!dtoToValidatorMap.ContainsKey(Ttype))
            throw new RequestTypeNotSupportedException(nameof(Tdto));

        Type validatorClass = dtoToValidatorMap[Ttype];
        return Activator.CreateInstance(validatorClass) as AbstractValidator<Tdto>;
    }
}