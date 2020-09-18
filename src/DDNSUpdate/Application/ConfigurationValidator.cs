using DDNSUpdate.Infrastructure.Configuration;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace DDNSUpdate.Application
{
    public class ConfigurationValidator : IConfigurationValidator
    {
        private readonly IOptionsSnapshot<ApplicationConfiguration> _applicationConfiguration;
        private readonly IValidator<ApplicationConfiguration> _applicationConfigurationValidator;
        private readonly ILogger _logger;

        public ConfigurationValidator(
            IOptionsSnapshot<ApplicationConfiguration> applicationConfiguration, 
            IValidator<ApplicationConfiguration> applicationConfigurationValidator,
            ILogger<ConfigurationValidator> logger)
        {
            _applicationConfiguration = applicationConfiguration;
            _applicationConfigurationValidator = applicationConfigurationValidator;
            _logger = logger;
        }

        public bool IsValid()
        {
            return Validate().IsValid;
        }

        private void LogErrors(ValidationResultCollection results)
        {
            foreach (string errorMessage in results.ErrorMessages)
            {
                _logger.LogError(errorMessage);
            }
        }

        private ValidationResultCollection Validate()
        {
            ValidationResultCollection results = new ValidationResultCollection(GetValidationResults());
            LogErrors(results);
            return results;
        }
        
        private IEnumerable<ValidationResult> GetValidationResults()
        {
            yield return _applicationConfigurationValidator.Validate(_applicationConfiguration.Value);
        }
    }
}
