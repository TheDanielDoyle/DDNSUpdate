using DDNSUpdate.Application.Providers.DigitalOcean.Configuration;
using DDNSUpdate.Infrastructure;
using DDNSUpdate.Infrastructure.Configuration;
using DDNSUpdate.Infrastructure.Extensions;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.Configuration
{
    public class ConfigurationValidator : IConfigurationValidator
    {
        private readonly ServiceFactory _serviceFactory;
        private readonly string _successMessage = "Configuration Validated Successfully.";

        public ConfigurationValidator(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public async Task<Result> ValidateAsync(CancellationToken cancellation)
        {
            IList<ValidationResult> validationResults = new List<ValidationResult>();
            await foreach (ValidationResult validationResult in ConfigurationsAsync(cancellation))
            {
                validationResults.Add(validationResult);
            }
            ValidationResultCollection results = new ValidationResultCollection(validationResults);
            if (results.IsValid)
            {
                return Result.Ok().WithSuccess(_successMessage);
            }
            return results.ToResults();
        }

        private async IAsyncEnumerable<ValidationResult> ConfigurationsAsync([EnumeratorCancellation] CancellationToken cancellation)
        {
            IValidator<ApplicationConfiguration> applicationConfigurationValidator = GetService<IValidator<ApplicationConfiguration>>();
            IOptionsSnapshot<ApplicationConfiguration> applicationConfiguration = GetService<IOptionsSnapshot<ApplicationConfiguration>>();
            yield return await applicationConfigurationValidator.ValidateAsync(applicationConfiguration.Value, cancellation);

            IValidator<DigitalOceanConfiguration> digitalOceanConfigurationValidator = GetService<IValidator<DigitalOceanConfiguration>>();
            IOptionsSnapshot<DigitalOceanConfiguration> digitalOceanConfiguration = GetService<IOptionsSnapshot<DigitalOceanConfiguration>>();
            yield return await digitalOceanConfigurationValidator.ValidateAsync(digitalOceanConfiguration.Value, cancellation);
        }

        private T GetService<T>()
        {
            return (T)_serviceFactory(typeof(T));
        }
    }
}
