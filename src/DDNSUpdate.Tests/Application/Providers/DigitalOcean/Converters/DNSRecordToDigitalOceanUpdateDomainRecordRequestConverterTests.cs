﻿using DDNSUpdate.Application.Providers.DigitalOcean.Converters;
using DDNSUpdate.Application.Providers.DigitalOcean.Requests;
using DDNSUpdate.Domain;
using DDNSUpdate.Tests.Helpers;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.DigitalOcean.Converters;

public class DNSRecordToDigitalOceanUpdateDomainRecordRequestConverterTests : TestBase
{
    private readonly MappingHelper _mappingHelper;

    public DNSRecordToDigitalOceanUpdateDomainRecordRequestConverterTests()
    {
        _mappingHelper = new MappingHelper(base.AssembliesUnderTest);
    }

    [Fact]
    public void OverwritesUpdateDomainRecordRequestValues()
    {
        DNSRecordToDigitalOceanUpdateDomainRecordRequestConverter converter = new();
        DNSRecord record = new()
        {
            Data = "DNSData",
            Flags = 1,
            Id = "12345",
            Name = "DNSName",
            Port = 1,
            Priority = 1,
            Tag = "DNSTag",
            TTL = 1,
            Weight = 1,
            Type = DNSRecordType.A
        };

        DigitalOceanUpdateDomainRecordRequest updateRecord = new()
        {
            Data = "CreatData",
            Flags = 123,
            Id = 12345,
            Name = "CreateName",
            Port = 123,
            Priority = 123,
            Tag = "DNSTag",
            Ttl = 123,
            Weight = 123,
            Type = "Some other type"
        };

        DigitalOceanUpdateDomainRecordRequest actual = converter.Convert(record, updateRecord, _mappingHelper.ResolutionContext);

        Assert.Equal(record.Data, actual.Data);
        Assert.Equal(record.Flags, actual.Flags);
        Assert.Equal(record.Id, actual.Id.ToString());
        Assert.Equal(record.Name, actual.Name);
        Assert.Equal(record.Port, actual.Port);
        Assert.Equal(record.Priority, actual.Priority);
        Assert.Equal(record.Tag, actual.Tag);
        Assert.Equal(record.TTL, actual.Ttl);
        Assert.Equal(record.Weight, actual.Weight);
        Assert.Equal(DNSRecordType.A.Value, actual.Type);
    }

    [Fact]
    public void ReturnsNewUpdateRequestWhenNull()
    {
        DNSRecordToDigitalOceanUpdateDomainRecordRequestConverter converter = new();

        DNSRecord record = new()
        {
            Data = "DNSData",
            Flags = 1,
            Id = "12345",
            Name = "DNSName",
            Port = 1,
            Priority = 1,
            Tag = "DNSTag",
            TTL = 1,
            Weight = 1,
            Type = DNSRecordType.A
        };

        DigitalOceanUpdateDomainRecordRequest actual = converter.Convert(record, null, _mappingHelper.ResolutionContext);

        Assert.Equal(record.Data, actual.Data);
        Assert.Equal(record.Flags, actual.Flags);
        Assert.Equal(record.Name, actual.Name);
        Assert.Equal(record.Port, actual.Port);
        Assert.Equal(record.Priority, actual.Priority);
        Assert.Equal(record.Tag, actual.Tag);
        Assert.Equal(record.TTL, actual.Ttl);
        Assert.Equal(record.Weight, actual.Weight);
        Assert.Equal(DNSRecordType.A.Value, actual.Type);
    }
}