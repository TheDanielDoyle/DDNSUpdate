using DDNSUpdate.Application.Configuration;
using DDNSUpdate.Infrastructure.Configuration;
using DDNSUpdate.Tests.Helpers;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DDNSUpdate.Tests.Application.Configuration
{
    public class ApplicationConfigurationValidationTests : TestBase
    {
        [Theory]
        [ClassData(typeof(ApplicationConfigurationInvalidExternalAddressProviders))]
        public void ExternalAddressProvidersInvalid(IEnumerable<ExternalAddressProvider> externalAddressProviders)
        {
            IValidator<ApplicationConfiguration> validator = new ApplicationConfigurationValidator();
            ApplicationConfiguration configuration = CreateValidApplicationConfiguration();

            configuration.ExternalAddressProviders = externalAddressProviders.ToList();

            ValidationResult result = validator.Validate(configuration);

            Assert.False(result.IsValid);
            Assert.True(result.Errors.All(m => !string.IsNullOrWhiteSpace(m.ErrorMessage)));
        }

        [Theory]
        [ClassData(typeof(ApplicationConfigurationUpdateIntervalInvalidTimeSpans))]
        public void UpdateIntervalInvalid(TimeSpan updateInterval)
        {
            IValidator<ApplicationConfiguration> validator = new ApplicationConfigurationValidator();
            ApplicationConfiguration configuration = CreateValidApplicationConfiguration();

            configuration.UpdateInterval = updateInterval;

            ValidationResult result = validator.Validate(configuration);

            Assert.False(result.IsValid);
            Assert.True(result.Errors.All(m => m.ErrorMessage.Equals(ApplicationConfigurationValidator.UpdateIntervalErrorMessage)));
        }

        [Theory]
        [ClassData(typeof(ApplicationConfigurationUpdateIntervalValidTimeSpans))]
        public void UpdateIntervalValid(TimeSpan updateInterval)
        {
            IValidator<ApplicationConfiguration> validator = new ApplicationConfigurationValidator();
            ApplicationConfiguration configuration = CreateValidApplicationConfiguration();

            configuration.UpdateInterval = updateInterval;

            Assert.True(validator.Validate(configuration).IsValid);
        }

        private ApplicationConfiguration CreateValidApplicationConfiguration()
        {
            return new ApplicationConfiguration()
            {
                ExternalAddressProviders = new List<ExternalAddressProvider>()
                {
                    new ExternalAddressProvider() { Uri = new Uri("https://test.com/") }
                },
                UpdateInterval = ApplicationConfiguration.MinimumUpdateInterval
            };
        }

        private class ApplicationConfigurationInvalidExternalAddressProviders : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { null };
                yield return new object[] { new List<ExternalAddressProvider>() };
                yield return new object[] { new List<ExternalAddressProvider>() { new ExternalAddressProvider() } };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class ApplicationConfigurationUpdateIntervalInvalidTimeSpans : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { TimeSpan.FromMilliseconds(900) };
                yield return new object[] { TimeSpan.FromSeconds(1) };
                yield return new object[] { TimeSpan.FromSeconds(30) };
                yield return new object[] { TimeSpan.FromSeconds(59) };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class ApplicationConfigurationUpdateIntervalValidTimeSpans : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { TimeSpan.FromMinutes(1) };
                yield return new object[] { TimeSpan.FromMinutes(60) };
                yield return new object[] { TimeSpan.FromHours(1) };
                yield return new object[] { TimeSpan.FromHours(24) };
                yield return new object[] { TimeSpan.FromDays(1) };
                yield return new object[] { TimeSpan.FromDays(7) };
                yield return new object[] { TimeSpan.FromDays(30) };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
