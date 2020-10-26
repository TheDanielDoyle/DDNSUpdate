using AutoMapper;
using DDNSUpdate.Application.Providers.GoDaddy.Converters;
using DDNSUpdate.Application.Providers.GoDaddy.Response;
using DDNSUpdate.Domain;
using DDNSUpdate.Tests.Helpers;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.GoDaddy.Converters
{
    public class GoDaddyGetDNSRecordResponseToDNSRecordConverterTests : TestBase
    {

        private readonly ResolutionContext _resolutionContext;

        public GoDaddyGetDNSRecordResponseToDNSRecordConverterTests()
        {
            _resolutionContext = new MappingHelper().ResolutionContext;
        }

        [Fact]
        public void Given_Null_DNS_Record_Returns_New_DNS_Record()
        {
            GoDaddyGetDNSRecordResponseToDNSRecordConverter converter = new GoDaddyGetDNSRecordResponseToDNSRecordConverter();
            GoDaddyGetDNSRecordResponse response = new GoDaddyGetDNSRecordResponse()
            {
                Data = "Data",
                Name = "Name",
                Port = 0,
                Priority = 0,
                Protocol = "Protocol",
                Service = "Service",
                Ttl = 0,
                Type = "A",
                Weight = 0
            };

            DNSRecord result = converter.Convert(response, null, _resolutionContext);

            Assert.NotNull(result);
            Assert.Equal(response.Data, result.Data);
            Assert.Equal(response.Name, result.Name);
            Assert.Equal(response.Port, result.Port);
            Assert.Equal(response.Priority, result.Priority);
            Assert.Equal(response.Ttl, result.TTL);
            Assert.Equal(response.Type, result.Type);
            Assert.Equal(response.Weight, result.Weight);
        }

        [Fact]
        public void Given_Non_Null_DNS_Record_Overwrites_Values()
        {
            GoDaddyGetDNSRecordResponseToDNSRecordConverter converter = new GoDaddyGetDNSRecordResponseToDNSRecordConverter();
            GoDaddyGetDNSRecordResponse response = new GoDaddyGetDNSRecordResponse()
            {
                Data = "Data",
                Name = "Name",
                Port = 0,
                Priority = 0,
                Protocol = "Protocol",
                Service = "Service",
                Ttl = 0,
                Type = "A",
                Weight = 0
            };

            DNSRecord record = new DNSRecord()
            {
                Data = "RecordData",
                Flags = 42,
                Id = "A-RecordName",
                Name = "RecordName",
                Port = 42,
                Priority = 42,
                Tag = "RecordTag",
                TTL = 42,
                Type = DNSRecordType.CERT,
                Weight = 42
            };

            DNSRecord result = converter.Convert(response, record, _resolutionContext);

            Assert.NotNull(result);
            Assert.Equal(record.Id, result.Id);
            Assert.Equal(record.Flags, result.Flags);
            Assert.Equal(response.Data, result.Data);
            Assert.Equal(response.Name, result.Name);
            Assert.Equal(response.Port, result.Port);
            Assert.Equal(response.Priority, result.Priority);
            Assert.Equal(response.Ttl, result.TTL);
            Assert.Equal(response.Type, result.Type);
            Assert.Equal(response.Weight, result.Weight);
        }
    }
}