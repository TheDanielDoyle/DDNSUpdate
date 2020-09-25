using DDNSUpdate.Application.Providers.DigitalOcean.Configuration;
using DDNSUpdate.Infrastructure;
using DDNSUpdate.Infrastructure.Configuration;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace DDNSUpdate.Application.Configuration
{
    public class ConfigurationValidator : IConfigurationValidator
    {
        private readonly ILogger _logger;
        private readonly ServiceFactory _serviceFactory;

        public ConfigurationValidator(ServiceFactory serviceFactory, ILogger<ConfigurationValidator> logger)
        {
            _logger = logger;
            _serviceFactory = serviceFactory;
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
            IValidator<ApplicationConfiguration> applicationConfigurationValidator = GetService<IValidator<ApplicationConfiguration>>();
            IOptionsSnapshot<ApplicationConfiguration> applicationConfiguration = GetService<IOptionsSnapshot<ApplicationConfiguration>>();
            yield return applicationConfigurationValidator.Validate(applicationConfiguration.Value);

            IValidator<DigitalOceanConfiguration> digitalOceanConfigurationValidator = GetService<IValidator<DigitalOceanConfiguration>>();
            IOptionsSnapshot<DigitalOceanConfiguration> digitalOceanConfiguration = GetService<IOptionsSnapshot<DigitalOceanConfiguration>>();
            yield return digitalOceanConfigurationValidator.Validate(digitalOceanConfiguration.Value);
        }

        private T GetService<T>()
        {
            return (T)_serviceFactory(typeof(T));
        }
    }
}
