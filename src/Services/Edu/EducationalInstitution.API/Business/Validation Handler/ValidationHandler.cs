using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace EducationalInstitutionAPI.Business.Validation_Handler
{
    /// <summary>
    /// Instantiates a validator class that validates all the fields of a given request
    /// </summary>
    public class ValidationHandler : ValidationFactory, IValidationHandler
    {
        /// <summary>
        /// Outputs to a file information about the state of the machine when an error/exception occurs during an operation
        /// </summary>
        private readonly ILogger<ValidationHandler> logger;

        public ValidationHandler(ILogger<ValidationHandler> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public bool IsRequestValid<T>(T request, out string validationErrors)
        {
            try
            {
                var validator = CreateValidator<T>();
                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                    validationErrors = GetValidationErrors(validationResult);
                else
                    validationErrors = string.Empty;

                return validationResult.IsValid;
            }
            catch (Exception e)
            {
                logger.LogError("Could not validate the request: {0} with the type: {1}, error details => {2}", nameof(request), request.GetType().ToString(), e.Message);
                validationErrors = e.Message;
                return false;
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
                validationErrorInfo.Append(" Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);

            return validationErrorInfo.ToString();
        }
    }
}