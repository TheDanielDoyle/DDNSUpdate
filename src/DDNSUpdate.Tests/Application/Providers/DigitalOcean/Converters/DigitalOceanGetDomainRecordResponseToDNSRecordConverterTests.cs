using DDNSUpdate.Application.Providers.DigitalOcean.Converters;
using DDNSUpdate.Application.Providers.DigitalOcean.Responses;
using DDNSUpdate.Domain;
using DDNSUpdate.Tests.Helpers;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.DigitalOcean.Converters
{
    public class DigitalOceanGetDomainRecordResponseToDNSRecordConverterTests : MappingSetupBase
    {
        [Fact]
        public void ReturnsNewDNSRecordWhenPassedNull()
        {
            DigitalOceanGetDomainRecordResponseToDNSRecordConverter converter = new DigitalOceanGetDomainRecordResponseToDNSRecordConverter();
            DigitalOceanGetDomainRecordResponse response = new DigitalOceanGetDomainRecordResponse()
            {
                Data = "data",
                Flags = 1,
                Id = 1,
                Name = "name",
                Port = 1,
                Priority = 1,
                Tag = "tag",
                Ttl = 1,
                Type = "A",
                Weight = 1
            };

            DNSRecord actual = converter.Convert(response, null, ResolutionContext);

            Assert.Equal(response.Data, actual.Data);
            Assert.Equal(response.Flags, actual.Flags);
            Assert.Equal(response.Id.ToString(), actual.Id);
            Assert.Equal(response.Name, actual.Name);
            Assert.Equal(response.Port, actual.Port);
            Assert.Equal(response.Priority, actual.Priority);
            Assert.Equal(response.Tag, actual.Tag);
            Assert.Equal(response.Ttl, actual.TTL);
            Assert.Equal(DNSRecordType.A, actual.Type);
            Assert.Equal(response.Weight, actual.Weight);
        }

        [Fact]
        public void OverwritesDNSRecordWhenPassedARecord()
        {
            DigitalOceanGetDomainRecordResponseToDNSRecordConverter converter = new DigitalOceanGetDomainRecordResponseToDNSRecordConverter();
            DigitalOceanGetDomainRecordResponse response = new DigitalOceanGetDomainRecordResponse()
            {
                Data = "data",
                Flags = 1,
                Id = 1,
                Name = "name",
                Port = 1,
                Priority = 1,
                Tag = "tag",
                Ttl = 1,
                Type = "A",
                Weight = 1
            };
            DNSRecord record = new DNSRecord()
            {
                Data = "dnsData",
                Flags = 987,
                Id = "aDNSRecordId",
                Name = "aDNSRecordName",
                Port = 852,
                Priority = 42,
                Tag = "aDNSTag",
                TTL = 987,
                Type = DNSRecordType.CERT,
                Weight = 42
            };

            DNSRecord actual = converter.Convert(response, record, ResolutionContext);

            Assert.Equal(response.Data, actual.Data);
            Assert.Equal(response.Flags, actual.Flags);
            Assert.Equal(response.Id.ToString(), actual.Id);
            Assert.Equal(response.Name, actual.Name);
            Assert.Equal(response.Port, actual.Port);
            Assert.Equal(response.Priority, actual.Priority);
            Assert.Equal(response.Tag, actual.Tag);
            Assert.Equal(response.Ttl, actual.TTL);
            Assert.Equal(DNSRecordType.A, actual.Type);
            Assert.Equal(response.Weight, actual.Weight);
        }
    }
}
