using DDNSUpdate.Application.Providers.GoDaddy.Configuration;
using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using DDNSUpdate.Domain;
using FluentValidation;
using FluentValidation.Results;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.GoDaddy.Configuration;

public class GoDaddyDomainValidatorTests
{
    [Fact]
    public void Validate_MissingName_ReturnIsNotValid()
    {
        GoDaddyDomain domain = new()
        {
            Records = CreateValidDNSRecordCollection()
        };

        IValidator<GoDaddyDomain> validator = new GoDaddyDomainValidator();

        ValidationResult result = validator.Validate(domain);

        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validate_MissingRecords_ReturnIsNotValid()
    {
        GoDaddyDomain domain = new()
        {
            Name = "GoDaddy Domain",
        };

        IValidator<GoDaddyDomain> validator = new GoDaddyDomainValidator();

        ValidationResult result = validator.Validate(domain);

        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validate_ValidDomain_ReturnIsValid()
    {
        GoDaddyDomain domain = new()
        {
            Name = "GoDaddy Domain",
            Records = CreateValidDNSRecordCollection()
        };

        IValidator<GoDaddyDomain> validator = new GoDaddyDomainValidator();

        ValidationResult result = validator.Validate(domain);

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
}