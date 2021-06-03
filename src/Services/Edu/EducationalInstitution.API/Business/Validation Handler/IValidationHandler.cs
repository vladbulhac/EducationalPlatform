namespace EducationalInstitutionAPI.Business.Validation_Handler
{
    /// <summary>
    /// Defines a method that instantiates a validator class that validates all the fields of a given request
    /// </summary>
    public interface IValidationHandler
    {
        /// <summary>
        /// Validates all the fields of a Data Transfer Object
        /// </summary>
        /// <typeparam name="T">A Data Transfer Object type for which a Validator class has been declared</typeparam>
        /// <param name="dto">Contains the data that has to be validated</param>
        /// <param name="validationErrors">A string that contains the information about the violated constraints ONLY when the return type is False</param>
        /// <returns>True if the request is valid, False and a string with the error in case the validation of a field fails</returns>
        public bool IsDataTransferObjectValid<T>(T dto, out string validationErrors);
    }
}