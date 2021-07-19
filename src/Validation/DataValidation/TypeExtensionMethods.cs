using System;

namespace DataValidation
{
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