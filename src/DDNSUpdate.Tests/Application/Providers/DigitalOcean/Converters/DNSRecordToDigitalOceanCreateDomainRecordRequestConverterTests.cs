using DDNSUpdate.Application.Providers.DigitalOcean.Converters;
using DDNSUpdate.Application.Providers.DigitalOcean.Requests;
using DDNSUpdate.Domain;
using DDNSUpdate.Tests.Helpers;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.DigitalOcean.Converters
{
    public class DNSRecordToDigitalOceanCreateDomainRecordRequestConverterTests : TestBase
    {
        private readonly MappingHelper _mappingHelper;

        public DNSRecordToDigitalOceanCreateDomainRecordRequestConverterTests()
        {
            _mappingHelper = new MappingHelper(base.AssembliesUnderTest);
        }

        [Fact]
        public void OverwirtesCreateDomainRecordRequestValues()
        {
            DNSRecordToDigitalOceanCreateDomainRecordRequestConverter converter = new();
            DNSRecord record = new()
            {
                Data = "DNSData",
                Flags = 1,
                Name = "DNSName",
                Port = 1,
                Priority = 1,
                Tag = "DNSTag",
                TTL = 1,
                Weight = 1,
                Type = DNSRecordType.A
            };

            DigitalOceanCreateDomainRecordRequest createRecord = new()
            {
                Data = "CreatData",
                Flags = 123,
                Name = "CreateName",
                Port = 123,
                Priority = 123,
                Tag = "DNSTag",
                Ttl = 123,
                Weight = 123,
                Type = "Some other type"
            };

            DigitalOceanCreateDomainRecordRequest actual = converter.Convert(record, createRecord, _mappingHelper.ResolutionContext);

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

        [Fact]
        public void ReturnsNewDomainRecordRequestWhenNull()
        {
            DNSRecordToDigitalOceanCreateDomainRecordRequestConverter converter = new();
            DNSRecord record = new()
            {
                Data = "DNSData",
                Flags = 1,
                Name = "DNSName",
                Port = 1,
                Priority = 1,
                Tag = "DNSTag",
                TTL = 1,
                Weight = 1,
                Type = DNSRecordType.A
            };

            DigitalOceanCreateDomainRecordRequest actual = converter.Convert(record, null, _mappingHelper.ResolutionContext);

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
}
