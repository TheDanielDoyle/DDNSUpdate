using DDNSUpdate.Application.Providers.DigitalOcean.Configuration;
using DDNSUpdate.Domain;
using DDNSUpdate.Tests.Helpers;
using FluentValidation;
using FluentValidation.Results;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.DigitalOcean.Configuration;

public class DigitalOceanDNSRecordValidatorTests : TestBase
{
    [Fact]
    public void InvalidDNSRecord()
    {
        DNSRecord dnsRecord = new()
        {
            Data = "boom",
            Flags = 0,
            Id = "goes",
            Priority = 0,
            Name = string.Empty,
            Tag = "the dynamite",
            TTL = 0,
            Type = DNSRecordType.SRV
        };

        IValidator<DNSRecord> validator = new DigitalOceanDNSRecordValidator();
        ValidationResult validationResult = validator.Validate(dnsRecord);

        Assert.False(validationResult.IsValid);

        Assert.Contains(validationResult.Errors, f => f.PropertyName == nameof(DNSRecord.Data));
        Assert.Contains(validationResult.Errors, f => f.PropertyName == nameof(DNSRecord.Flags));
        Assert.Contains(validationResult.Errors, f => f.PropertyName == nameof(DNSRecord.Id));
        Assert.Contains(validationResult.Errors, f => f.PropertyName == nameof(DNSRecord.Priority));
        Assert.Contains(validationResult.Errors, f => f.PropertyName == nameof(DNSRecord.Name));
        Assert.Contains(validationResult.Errors, f => f.PropertyName == nameof(DNSRecord.Tag));
        Assert.Contains(validationResult.Errors, f => f.PropertyName == nameof(DNSRecord.TTL));
        Assert.Contains(validationResult.Errors, f => f.PropertyName == nameof(DNSRecord.Type));
    }

    [Fact]
    public void ValidDNSRecord()
    {
        DNSRecord dnsRecord = new()
        {
            Name = "validate",
            TTL = 1800,
            Type = DNSRecordType.A
        };

        IValidator<DNSRecord> validator = new DigitalOceanDNSRecordValidator();
        ValidationResult validationResult = validator.Validate(dnsRecord);

        Assert.True(validationResult.IsValid);
    }
}