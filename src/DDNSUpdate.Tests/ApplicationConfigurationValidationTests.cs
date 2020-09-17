using DDNSUpdate.Application;
using DDNSUpdate.Infrastructure.Configuration;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DDNSUpdate.Tests
{
    public class ApplicationConfigurationValidationTests
    {
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
                UpdateInterval = ApplicationConfiguration.MinimumUpdateInterval
            };
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
