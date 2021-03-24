using System;

namespace EducationaInstitutionAPI.Utils.Custom_Exceptions
{
    /// <summary>
    /// Defines an Exception type that is thrown when a method can't handle a class type
    /// </summary>
    public class RequestTypeNotSupportedException : Exception
    {
        public RequestTypeNotSupportedException()
        {
        }

        public RequestTypeNotSupportedException(string message) : base(message)
        {
        }

        public RequestTypeNotSupportedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}