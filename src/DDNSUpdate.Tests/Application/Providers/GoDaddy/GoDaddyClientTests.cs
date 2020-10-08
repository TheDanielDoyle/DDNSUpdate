using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Providers.GoDaddy;
using DDNSUpdate.Application.Providers.GoDaddy.Request;
using DDNSUpdate.Application.Providers.GoDaddy.Response;
using DDNSUpdate.Domain;
using FluentResults;
using Flurl.Http.Testing;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.GoDaddy
{
    public class GoDaddyClientTests : IDisposable
    {

        private readonly string _domainsResult = @"
        [
  {
    ""data"": ""testData"",
    ""name"": ""testName"",
    ""port"": 0,
    ""priority"": 0,
    ""protocol"": ""testProtocol"",
    ""service"": ""testService"",
    ""ttl"": 0,
    ""type"": ""A"",
    ""weight"": 0
  }
]";

        private readonly HttpClient _httpClient;
        private readonly HttpTest _httpTest;

        public GoDaddyClientTests()
        {
            _httpClient = new HttpClient();
            _httpTest = new HttpTest();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
            _httpTest.Dispose();
        }

        [Fact]
        public async Task Create_DNS_Record_Success_Is_Success_Result()
        {
            _httpTest.RespondWith(string.Empty, (int)HttpStatusCode.OK);
            GoDaddyClient client = new GoDaddyClient(_httpClient);
            IEnumerable<DNSRecord> records = new List<DNSRecord>() { new DNSRecord() };
            GoDaddyCreateDNSRecordRequest request = new GoDaddyCreateDNSRecordRequest("AnApiKey", "AnApiSecret", records, "test.com");

            Result result = await client.CreateDNSRecordAsync(request, new CancellationToken());

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Create_DNS_Record_Failure_Is_Failure_Result()
        {
            _httpTest.RespondWith(string.Empty, (int)HttpStatusCode.InternalServerError);
            GoDaddyClient client = new GoDaddyClient(_httpClient);
            IEnumerable<DNSRecord> records = new List<DNSRecord>() { new DNSRecord() };
            GoDaddyCreateDNSRecordRequest request = new GoDaddyCreateDNSRecordRequest("AnApiKey", "AnApiSecret", records, "test.com");

            Result result = await client.CreateDNSRecordAsync(request, new CancellationToken());

            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task Get_DNS_Record_Success_Is_Success_Result_And_Contains_Records()
        {
            _httpTest.RespondWith(_domainsResult);
            GoDaddyClient client = new GoDaddyClient(_httpClient);

            GoDaddyGetDNSRecordsRequest request = new GoDaddyGetDNSRecordsRequest("ApiKey", "ApiSecret", DNSRecordType.A, "test.com");

            Result<GoDaddyGetDNSRecordsResponse> result = await client.GetDNSRecordsAsync(request, new CancellationToken());

            Assert.True(result.IsSuccess);

            var records = result.Value.Records.ToList();
            var record = records.First();

            Assert.Equal("testData", record.Data);
            Assert.Equal("testName", record.Name);
            Assert.Equal(0, record.Port);
            Assert.Equal(0, record.Priority);
            Assert.Equal("testProtocol", record.Protocol);
            Assert.Equal("testService", record.Service);
            Assert.Equal(0, record.Ttl);
            Assert.Equal("A", record.Type);
            Assert.Equal(0, record.Weight);
        }

        [Fact]
        public async Task Get_DNS_Record_Failure_Is_Failure_Result()
        {
            _httpTest.RespondWith(string.Empty, (int)HttpStatusCode.NotFound);
            GoDaddyClient client = new GoDaddyClient(_httpClient);

            GoDaddyGetDNSRecordsRequest request = new GoDaddyGetDNSRecordsRequest("ApiKey", "ApiSecret", DNSRecordType.A, "test.com");

            Result<GoDaddyGetDNSRecordsResponse> result = await client.GetDNSRecordsAsync(request, new CancellationToken());

            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task Update_DNS_Record_Success_Returns_Success_Result()
        {
            _httpTest.RespondWith(string.Empty);
            GoDaddyClient client = new GoDaddyClient(_httpClient);
            IEnumerable<GoDaddyUpdateDNSRecordRequest> recordRequests = new List<GoDaddyUpdateDNSRecordRequest>();
            GoDaddyUpdateDNSRecordsRequest request = new GoDaddyUpdateDNSRecordsRequest("ApiKey", "ApiSecret", DNSRecordType.A, "test.com", recordRequests);

            Result actual = await client.UpdateDNSRecordsAsync(request, new CancellationToken());

            Assert.True(actual.IsSuccess);
        }

        [Fact]
        public async Task Update_DNS_Record_Failure_Returns_Failure_Result()
        {
            _httpTest.RespondWith(string.Empty, (int)HttpStatusCode.InternalServerError);
            GoDaddyClient client = new GoDaddyClient(_httpClient);
            IEnumerable<GoDaddyUpdateDNSRecordRequest> recordRequests = new List<GoDaddyUpdateDNSRecordRequest>();
            GoDaddyUpdateDNSRecordsRequest request = new GoDaddyUpdateDNSRecordsRequest("ApiKey", "ApiSecret", DNSRecordType.A, "test.com", recordRequests);

            Result actual = await client.UpdateDNSRecordsAsync(request, new CancellationToken());

            Assert.True(actual.IsFailed);
        }
    }
}
