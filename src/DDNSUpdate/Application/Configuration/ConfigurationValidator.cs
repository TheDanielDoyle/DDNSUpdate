using DDNSUpdate.Application.Providers.DigitalOcean.Configuration;
using DDNSUpdate.Application.Providers.GoDaddy.Configuration;
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
            yield return await ValidateResultAsync<ApplicationConfiguration>(cancellation);
            yield return await ValidateResultAsync<DigitalOceanConfiguration>(cancellation);
            yield return await ValidateResultAsync<GoDaddyConfiguration>(cancellation);
        }

        private async Task<ValidationResult> ValidateResultAsync<TConfiguration>(CancellationToken cancellation)
            where TConfiguration : class, new()
        {
            IValidator<TConfiguration> validator = GetService<IValidator<TConfiguration>>();
            IOptionsSnapshot<TConfiguration> configuration = GetService<IOptionsSnapshot<TConfiguration>>();
            return await validator.ValidateAsync(configuration.Value, cancellation);
        }

        private T GetService<T>()
        {
            return (T)_serviceFactory(typeof(T))!;
        }
    }
}
