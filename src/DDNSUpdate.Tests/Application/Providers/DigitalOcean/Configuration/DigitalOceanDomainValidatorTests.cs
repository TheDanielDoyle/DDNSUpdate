using DDNSUpdate.Application.Providers.DigitalOcean.Configuration;
using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using DDNSUpdate.Domain;
using DDNSUpdate.Tests.Helpers;
using FluentValidation;
using FluentValidation.Results;
using System.Linq;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.DigitalOcean.Configuration
{
    public class DigitalOceanDomainValidatorTests : TestBase
    {
        [Fact]
        public void InvalidDigitalOceanDomain()
        {
            DigitalOceanDomain domain = new()
            {
                Name = string.Empty,
                Records = ValidDNSRecordCollection()
            };

            IValidator<DigitalOceanDomain> validator = new DigitalOceanDomainValidator();
            ValidationResult validationResult = validator.Validate(domain);

            Assert.False(validationResult.IsValid);
            Assert.True(validationResult.Errors.All(m => m.ErrorMessage.Equals(DigitalOceanDomainValidator.NameErrorMessage)));
        }

        [Fact]
        public void ValidateValidDigitalOceanDomain()
        {
            DigitalOceanDomain domain = new()
            {
                Name = "test.com",
                Records = ValidDNSRecordCollection()
            };

            IValidator<DigitalOceanDomain> validator = new DigitalOceanDomainValidator();
            ValidationResult validationResult = validator.Validate(domain);

            Assert.True(validationResult.IsValid);
        }

        private DNSRecord ValidDNSRecord()
        {
            return new DNSRecord
            {
                Name = "validate",
                TTL = 1800,
                Type = DNSRecordType.A
            };
        }

        private DNSRecordCollection ValidDNSRecordCollection()
        {
            return new DNSRecordCollection(new[]
            {
                ValidDNSRecord()
            });
        }
    }
}
