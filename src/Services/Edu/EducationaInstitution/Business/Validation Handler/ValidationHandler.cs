using EducationaInstitutionAPI.Business.Validation_Handler;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace EducationaInstitutionAPI.Business
{
    public class ValidationHandler : ValidationFactory
    {
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
                {
                    validationErrors = GetValidationErrors(validationResult);
                    return false;
                }

                validationErrors = string.Empty;
                return true;
            }
            catch (Exception e)
            {
                logger.LogError("Could not validate the request:{0} with the type:{1}, error details=>{2}", nameof(request), request.GetType().ToString(), e.Message);
                validationErrors = e.Message;
                return false;
            }
        }

        private static string GetValidationErrors(ValidationResult validationResult)
        {
            StringBuilder validationErrorInfo = new();
            foreach (var failure in validationResult.Errors)
                validationErrorInfo.Append(" Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);

            return validationErrorInfo.ToString();
        }
    }
}