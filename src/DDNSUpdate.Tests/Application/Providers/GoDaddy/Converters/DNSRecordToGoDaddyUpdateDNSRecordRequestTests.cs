using AutoMapper;
using DDNSUpdate.Application.Providers.GoDaddy.Converters;
using DDNSUpdate.Application.Providers.GoDaddy.Request;
using DDNSUpdate.Domain;
using DDNSUpdate.Tests.Helpers;
using FakeItEasy;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.GoDaddy.Converters
{
    public class DNSRecordToGoDaddyUpdateDNSRecordRequestTests : TestBase
    {

        private readonly ResolutionContext _resolutionContext;

        public DNSRecordToGoDaddyUpdateDNSRecordRequestTests()
        {
            _resolutionContext = new MappingHelper().ResolutionContext;
        }

        [Fact]
        public void Returns_Non_Null_Update_Request_When_Passed_Null()
        {
            DNSRecordToGoDaddyUpdateDNSRecordRequestConverter converter = new DNSRecordToGoDaddyUpdateDNSRecordRequestConverter();
            DNSRecord record = new DNSRecord()
            {
                Data = "recordData",
                Name = "recordName",
                Port = 1,
                Priority = 1,
                TTL = 1,
                Weight = 1,
                Tag = "something",
                Type = DNSRecordType.A
            };

            GoDaddyUpdateDNSRecordRequest result = converter.Convert(record, null, _resolutionContext);
            Assert.NotNull(result);
            Assert.Equal(record.Data, result.Data);
            Assert.Equal(record.Name, result.Name);
            Assert.Equal((int)record.Port, result.Port);
            Assert.Equal((int)record.Priority, result.Priority);
            Assert.Null(result.Protocol);
            Assert.Null(result.Service);
            Assert.Equal(record.TTL, (int)result.Ttl);
            Assert.Equal(record.Weight, (int)result.Weight);
        }

        [Fact]
        public void Overwrites_Values_When_Passed_Non_Null()
        {
            DNSRecordToGoDaddyUpdateDNSRecordRequestConverter converter = new DNSRecordToGoDaddyUpdateDNSRecordRequestConverter();
            DNSRecord record = new DNSRecord()
            {
                Data = "recordData",
                Name = "recordName",
                Port = 1,
                Priority = 1,
                TTL = 1,
                Weight = 1,
                Tag = "something",
                Type = DNSRecordType.A
            };

            GoDaddyUpdateDNSRecordRequest updateRequest = new GoDaddyUpdateDNSRecordRequest()
            {
                Data = "updateRequestData",
                Name = "updateRequestName",
                Port = 2,
                Priority = 2,
                Protocol = "updateRequestProtocol",
                Service = "updateRequestService",
                Ttl = 2,
                Weight = 2
            };

            GoDaddyUpdateDNSRecordRequest result = converter.Convert(record, updateRequest, _resolutionContext);

            Assert.Equal(record.Data, result.Data);
            Assert.Equal(record.Name, result.Name);
            Assert.Equal((int)record.Port, result.Port);
            Assert.Equal((int)record.Priority, result.Priority);
            Assert.Equal(updateRequest.Protocol, result.Protocol);
            Assert.Equal(updateRequest.Service, result.Service);
            Assert.Equal(record.TTL, (int)result.Ttl);
            Assert.Equal(record.Weight, (int)result.Weight);
        }
    }
}