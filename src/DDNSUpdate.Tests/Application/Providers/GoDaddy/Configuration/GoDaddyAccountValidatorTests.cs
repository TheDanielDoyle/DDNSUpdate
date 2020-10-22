using DDNSUpdate.Application.Providers.GoDaddy.Configuration;
using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using DDNSUpdate.Domain;
using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.GoDaddy.Configuration
{
    public class GoDaddyAccountValidatorTests
    {
        [Fact]
        public void Validate_MissingApiKey_ReturnsIsNotValid()
        {
            GoDaddyAccount account = new GoDaddyAccount()
            {
                ApiSecret = "ApiSecret",
                Domains = CreateValidGoDaddyDomains()
            };

            IValidator<GoDaddyAccount> validator = new GoDaddyAccountValidator();

            ValidationResult result = validator.Validate(account);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_MissingApiSecret_ReturnsIsNotValid()
        {
            GoDaddyAccount account = new GoDaddyAccount()
            {
                ApiKey = "ApiKey",
                Domains = CreateValidGoDaddyDomains()
            };

            IValidator<GoDaddyAccount> validator = new GoDaddyAccountValidator();

            ValidationResult result = validator.Validate(account);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_MissingDomains_ReturnsIsNotValid()
        {
            GoDaddyAccount account = new GoDaddyAccount()
            {
                ApiKey = "ApiKey",
                ApiSecret = "ApiSecret"
            };

            IValidator<GoDaddyAccount> validator = new GoDaddyAccountValidator();

            ValidationResult result = validator.Validate(account);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_ValidAccount_ReturnsIsValid()
        {
            GoDaddyAccount account = new GoDaddyAccount()
            {
                ApiKey = "ApiKey",
                ApiSecret = "ApiSecret",
                Domains = CreateValidGoDaddyDomains()
            };

            IValidator<GoDaddyAccount> validator = new GoDaddyAccountValidator();

            ValidationResult result = validator.Validate(account);

            Assert.True(result.IsValid);
        }

        private DNSRecord CreateValidDNSRecord()
        {
            return new DNSRecord
            {
                Name = "Valid",
                TTL = 1800,
                Type = DNSRecordType.A
            };
        }

        private DNSRecordCollection CreateValidDNSRecordCollection()
        {
            return new DNSRecordCollection(new[]
            {
                CreateValidDNSRecord()
            });
        }

        private GoDaddyDomain CreateValidGoDaddyDomain()
        {
            return new GoDaddyDomain
            {
                Name = "test.com",
                Records = CreateValidDNSRecordCollection()
            };
        }

        private IEnumerable<GoDaddyDomain> CreateValidGoDaddyDomains()
        {
            return new List<GoDaddyDomain>(new[]
            {
                CreateValidGoDaddyDomain()
            });
        }
    }
}
