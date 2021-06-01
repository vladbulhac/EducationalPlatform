using System;

namespace EducationalInstitutionAPI.Utils.Custom_Exceptions
{
    /// <summary>
    /// Defines an Exception type that is thrown when a validator has not been found for a Data Transfer Object
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