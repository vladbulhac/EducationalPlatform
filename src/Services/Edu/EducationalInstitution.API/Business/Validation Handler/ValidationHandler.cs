using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;

namespace EducationalInstitutionAPI.Business.Validation_Handler
{
    public class ValidationHandler : HandlerBase<ValidationHandler>, IValidationHandler
    {
        /// <inheritdoc cref="HandlerBase{THandler}.HandlerBase"/>
        public ValidationHandler(ILogger<ValidationHandler> logger) : base(logger) { }

        public bool IsDataTransferObjectValid<T>(T dto, out string validationErrors)
        {
            try
            {
                var validator = ValidatorFactory.CreateValidator<T>();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                    validationErrors = GetValidationErrors(validationResult);
                else
                    validationErrors = string.Empty;

                return validationResult.IsValid;
            }
            catch (Exception e)
            {
                validationErrors = "An error occurred while trying to validate the request!";

                return HandleException(
                            error_message: "Could not validate the request: {0} with the type: {1}, error details => {2}",
                                            JsonConvert.SerializeObject(dto),
                                            dto.GetType(),
                                            e.Message);
            }
        }

        /// <summary>
        /// Iterates over a list of errors and appends each to a <see cref="StringBuilder"/>
        /// </summary>
        /// <param name="validationResult">The object that contains information about the validation result</param>
        /// <returns>All the validation errors</returns>
        private static string GetValidationErrors(ValidationResult validationResult)
        {
            StringBuilder validationErrorInfo = new();
            foreach (var failure in validationResult.Errors)
                validationErrorInfo.Append(" Property ")
                                   .Append(failure.PropertyName)
                                   .Append(" failed validation. Error was: ")
                                   .Append(failure.ErrorMessage);

            return validationErrorInfo.ToString();
        }
    }
}