using System.Linq;
using DDNSUpdate.Application.Providers.DigitalOcean.Configuration;
using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using DDNSUpdate.Domain;
using DDNSUpdate.Tests.Helpers;
using FluentValidation;
using FluentValidation.Results;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.DigitalOcean.Configuration
{
    public class DigitalOceanDomainValidatorTests : TestBase
    {
        [Fact]
        public void ValidDigitalOceanDomain()
        {
            DigitalOceanDomain domain = new DigitalOceanDomain
            {
                Name = "test.com",
                Records = ValidDNSRecordCollection()
            };

            IValidator<DigitalOceanDomain> validator = new DigitalOceanDomainValidator();
            ValidationResult validationResult = validator.Validate(domain);

            Assert.True(validationResult.IsValid);
        }

        [Fact]
        public void InvalidDigitalOceanDomain()
        {
            DigitalOceanDomain domain = new DigitalOceanDomain
            {
                Name = string.Empty,
                Records = ValidDNSRecordCollection()
            };

            IValidator<DigitalOceanDomain> validator = new DigitalOceanDomainValidator();
            ValidationResult validationResult = validator.Validate(domain);

            Assert.False(validationResult.IsValid);
            Assert.True(validationResult.Errors.All(m => m.ErrorMessage.Equals(DigitalOceanDomainValidator.NameErrorMessage)));
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
