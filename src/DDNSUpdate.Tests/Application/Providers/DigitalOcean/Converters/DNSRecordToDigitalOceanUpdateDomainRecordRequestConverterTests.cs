using DDNSUpdate.Application.Providers.DigitalOcean.Converters;
using DDNSUpdate.Application.Providers.DigitalOcean.Requests;
using DDNSUpdate.Domain;
using DDNSUpdate.Tests.Helpers;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.DigitalOcean.Converters
{
    public class DNSRecordToDigitalOceanUpdateDomainRecordRequestConverterTests : MappingSetupBase
    {
        [Fact]
        public void ReturnsNewUpdateRequestWhenNull()
        {
            //Given
            DNSRecordToDigitalOceanUpdateDomainRecordRequestConverter converter = new DNSRecordToDigitalOceanUpdateDomainRecordRequestConverter();

            DNSRecord record = new DNSRecord()
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
            //When
            DigitalOceanUpdateDomainRecordRequest actual = converter.Convert(record, null, ResolutionContext);
            //Then
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
        public void OverwirtesUpdateDomainRecordRequestValues()
        {
            //Given
            DNSRecordToDigitalOceanUpdateDomainRecordRequestConverter converter = new DNSRecordToDigitalOceanUpdateDomainRecordRequestConverter();
            DNSRecord record = new DNSRecord()
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

            DigitalOceanUpdateDomainRecordRequest updateRecord = new DigitalOceanUpdateDomainRecordRequest()
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
            //When
            DigitalOceanUpdateDomainRecordRequest actual = converter.Convert(record, updateRecord, ResolutionContext);
            //Then
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
