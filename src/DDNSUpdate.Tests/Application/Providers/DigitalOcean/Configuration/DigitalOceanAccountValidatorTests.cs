using DDNSUpdate.Application.Providers.DigitalOcean.Configuration;
using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using DDNSUpdate.Domain;
using DDNSUpdate.Tests.Helpers;
using FluentValidation;
using FluentValidation.Results;
using System.Linq;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.DigitalOcean.Configuration;

public class DigitalOceanAccountValidatorTests : TestBase
{
    [Fact]
    public void InvalidDigitalOceanAccount()
    {
        DigitalOceanAccount account = new()
        {
            Domains = new[]
            {
                ValidDigitalOceanDomain()
            },
            Token = string.Empty
        };

        IValidator<DigitalOceanAccount> validator = new DigitalOceanAccountValidator();
        ValidationResult validationResult = validator.Validate(account);

        Assert.False(validationResult.IsValid);
        Assert.True(validationResult.Errors.All(m => m.ErrorMessage.Equals(DigitalOceanAccountValidator.TokenErrorMessage)));
    }

    [Fact]
    public void ValidDigitalOceanAccount()
    {
        DigitalOceanAccount account = new()
        {
            Domains = new[]
            {
                ValidDigitalOceanDomain()
            },
            Token = "QSA5VXQSWMH3L8MYX2XF"
        };

        IValidator<DigitalOceanAccount> validator = new DigitalOceanAccountValidator();
        ValidationResult validationResult = validator.Validate(account);

        Assert.True(validationResult.IsValid);
    }

    private DigitalOceanDomain ValidDigitalOceanDomain()
    {
        return new DigitalOceanDomain
        {
            Name = "test.com",
            Records = ValidDNSRecordCollection()
        };
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