using DDNSUpdate.Application.Providers.GoDaddy.Configuration;
using DDNSUpdate.Domain;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.GoDaddy.Configuration
{
    public class GoDaddyDNSRecordValidatorTest
    {
        [Fact]
        public void Validate_InvalidDNSRecord_ReturnsIsNotValid()
        {
            DNSRecord dnsRecord = new DNSRecord
            {
                Data = "Data",
                Flags = 0,
                Id = "Id",
                Priority = 0,
                Name = string.Empty,
                Tag = "Tag",
                TTL = 0,
                Type = DNSRecordType.SRV
            };

            IValidator<DNSRecord> validator = new GoDaddyDNSRecordValidator();
            ValidationResult validationResult = validator.Validate(dnsRecord);

            Assert.False(validationResult.IsValid);

            Assert.Contains(validationResult.Errors, f => f.PropertyName == nameof(DNSRecord.Data));
            Assert.Contains(validationResult.Errors, f => f.PropertyName == nameof(DNSRecord.Flags));
            Assert.Contains(validationResult.Errors, f => f.PropertyName == nameof(DNSRecord.Priority));
            Assert.Contains(validationResult.Errors, f => f.PropertyName == nameof(DNSRecord.Name));
            Assert.Contains(validationResult.Errors, f => f.PropertyName == nameof(DNSRecord.Tag));
            Assert.Contains(validationResult.Errors, f => f.PropertyName == nameof(DNSRecord.TTL));
            Assert.Contains(validationResult.Errors, f => f.PropertyName == nameof(DNSRecord.Type));
        }

        [Fact]
        public void Validate_ValidDNSRecord_ReturnsIsValid()
        {
            DNSRecord dnsRecord = new DNSRecord
            {
                Name = "Valid",
                TTL = 1800,
                Type = DNSRecordType.A
            };

            IValidator<DNSRecord> validator = new GoDaddyDNSRecordValidator();
            ValidationResult validationResult = validator.Validate(dnsRecord);

            Assert.True(validationResult.IsValid);
        }
    }
}
