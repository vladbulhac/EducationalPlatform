using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;

namespace DataValidation
{
    /// <inheritdoc cref="IValidationHandler">
    public class ValidationHandler : IValidationHandler
    {
        private readonly ILogger<ValidationHandler> logger;
        private readonly ValidatorFactory validatorFactory;

        /// <exception cref="ArgumentNullException"/>
        public ValidationHandler(ILogger<ValidationHandler> logger, ValidatorFactory validatorFactory)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.validatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
        }

        public bool IsDataTransferObjectValid<Tdto>(Tdto dto, out string validationErrors) where Tdto : class
        {
            try
            {
                var validator = validatorFactory.CreateValidator<Tdto>();
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

                logger.LogError("Could not validate the request: {0} with the type: {1}, error details => {2}",
                                JsonConvert.SerializeObject(dto),
                                dto.GetType(),
                                e.Message);
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
                validationErrorInfo.Append(" Property ")
                                   .Append(failure.PropertyName)
                                   .Append(" failed validation. Error was: ")
                                   .Append(failure.ErrorMessage);

            return validationErrorInfo.ToString();
        }
    }
}