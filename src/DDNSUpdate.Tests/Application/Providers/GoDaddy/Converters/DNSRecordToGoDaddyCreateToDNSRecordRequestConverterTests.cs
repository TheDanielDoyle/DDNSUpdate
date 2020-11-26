using AutoMapper;
using DDNSUpdate.Application.Providers.GoDaddy.Converters;
using DDNSUpdate.Application.Providers.GoDaddy.Request;
using DDNSUpdate.Domain;
using DDNSUpdate.Tests.Helpers;
using Xunit;

namespace DDNSCreate.Tests.Application.Providers.GoDaddy.Converters
{
    public class DNSRecordToGoDaddyCreateToDNSRecordRequestConverterTests : TestBase
    {
        private readonly ResolutionContext _resolutionContext;

        public DNSRecordToGoDaddyCreateToDNSRecordRequestConverterTests()
        {
            _resolutionContext = new MappingHelper().ResolutionContext;
        }

        [Fact]
        public void Convert_PassedNullRequest_ReturnsNewRequest()
        {
            DNSRecordToGoDaddyCreateToDNSRecordRequestConverter converter = new();
            DNSRecord record = new()
            {
                Data = "recordData",
                Name = "recordName",
                Port = 1,
                Priority = 1,
                TTL = 1,
                Weight = 1,
                Tag = "recordTag",
                Type = DNSRecordType.A
            };

            GoDaddyCreateDNSRecordRequest result = converter.Convert(record, null, _resolutionContext);
            Assert.NotNull(result);
            Assert.Equal(record.Data, result.Data);
            Assert.Equal(record.Name, result.Name);
            Assert.Equal(record.Port, result.Port);
            Assert.Equal(record.Priority, result.Priority);
            Assert.Null(result.Protocol);
            Assert.Null(result.Service);
            Assert.Equal(record.TTL, result.Ttl);
            Assert.Equal(record.Weight, result.Weight);
        }

        [Fact]
        public void Convert_PassedNonNullRequests_OverwritesValues()
        {
            DNSRecordToGoDaddyCreateToDNSRecordRequestConverter converter = new();
            DNSRecord record = new()
            {
                Data = "recordData",
                Name = "recordName",
                Port = 1,
                Priority = 1,
                TTL = 1,
                Weight = 1,
                Tag = "recordTag",
                Type = DNSRecordType.A
            };

            GoDaddyCreateDNSRecordRequest createRequest = new()
            {
                Data = "createRequestData",
                Name = "createRequestName",
                Port = 2,
                Priority = 2,
                Protocol = "createRequestProtocol",
                Service = "createRequestService",
                Ttl = 2,
                Weight = 2
            };

            GoDaddyCreateDNSRecordRequest result = converter.Convert(record, createRequest, _resolutionContext);

            Assert.Equal(record.Data, result.Data);
            Assert.Equal(record.Name, result.Name);
            Assert.Equal(record.Port, result.Port);
            Assert.Equal(record.Priority, result.Priority);
            Assert.Equal(createRequest.Protocol, result.Protocol);
            Assert.Equal(createRequest.Service, result.Service);
            Assert.Equal(record.TTL, result.Ttl);
            Assert.Equal(record.Weight, result.Weight);
        }
    }
}
