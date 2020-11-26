using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DDNSUpdate.Application.Providers.GoDaddy;
using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using DDNSUpdate.Application.Providers.GoDaddy.Request;
using DDNSUpdate.Application.Providers.GoDaddy.Response;
using DDNSUpdate.Domain;
using DDNSUpdate.Tests.Helpers;
using FakeItEasy;
using FluentResults;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.GoDaddy
{

    public class GoDadyDNSRecordReaderTests : TestBase
    {
        private readonly IMapper _mapper;

        public GoDadyDNSRecordReaderTests()
        {
            _mapper = new MappingHelper(AssembliesUnderTest).Mapper;
        }

        [Fact]
        public async Task Returns_Failure_When_Getting_Record_Fails()
        {

            IGoDaddyClient fakeClient = A.Fake<IGoDaddyClient>();

            A.CallTo(() => fakeClient.GetDNSRecordsAsync(A<GoDaddyGetDNSRecordsRequest>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Fail("failed"));

            GoDaddyDNSRecordReader reader = new(fakeClient, _mapper);
            GoDaddyAuthenticationDetails authicationDetails = new("apiKey", "apiSecret");

            Result<DNSRecordCollection> result = await reader.ReadAsync(string.Empty, authicationDetails, CancellationToken.None);

            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task Returns_Success_With_DNS_Records_When_Successfully_Retrieved()
        {
            IGoDaddyClient fakeClient = A.Fake<IGoDaddyClient>();

            List<GoDaddyGetDNSRecordResponse> records = new()
            {
                CreateValidGoDaddyGetDNSRecordResponse(1),
                CreateValidGoDaddyGetDNSRecordResponse(2),
                CreateValidGoDaddyGetDNSRecordResponse(3)
            };

            GoDaddyGetDNSRecordsResponse clientResponse = new(records);

            A.CallTo(() => fakeClient.GetDNSRecordsAsync(A<GoDaddyGetDNSRecordsRequest>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Ok(clientResponse));
            GoDaddyDNSRecordReader reader = new(fakeClient, _mapper);
            GoDaddyAuthenticationDetails authicationDetails = new("apiKey", "apiSecret");

            Result<DNSRecordCollection> result = await reader.ReadAsync(string.Empty, authicationDetails, CancellationToken.None);
            DNSRecord firstRecord = result.Value.First();

            Assert.True(result.IsSuccess);
            Assert.Equal(3, result.Value.Count);

            Assert.Equal("Data-1", firstRecord.Data);
            Assert.Equal("Name-1", firstRecord.Name);
            Assert.Equal(1, firstRecord.Port);
        }

        private GoDaddyGetDNSRecordResponse CreateValidGoDaddyGetDNSRecordResponse(int num)
        {
            return new GoDaddyGetDNSRecordResponse()
            {
                Data = $"Data-{num}",
                Name = $"Name-{num}",
                Port = num,
                Priority = num,
                Protocol = $"Protocol-{num}",
                Service = $"Service-{num}",
                Ttl = num,
                Type = "A",
                Weight = num
            };
        }
    }
}