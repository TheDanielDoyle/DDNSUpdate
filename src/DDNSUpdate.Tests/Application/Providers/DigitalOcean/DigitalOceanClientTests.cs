using DDNSUpdate.Application.Providers.DigitalOcean;
using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using DDNSUpdate.Application.Providers.DigitalOcean.Requests;
using DDNSUpdate.Application.Providers.DigitalOcean.Responses;
using DDNSUpdate.Domain;
using DDNSUpdate.Tests.Helpers;
using FluentResults;
using Flurl.Http.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.DigitalOcean;

public class DigitalOceanClientTests : TestBase, IDisposable
{
    private const string _createResponse = @"
{
  ""domain_record"": {
    ""id"": 28448433,
    ""type"": ""A"",
    ""name"": ""test"",
    ""data"": ""100.100.100.100"",
    ""priority"": null,
    ""port"": null,
    ""ttl"": 1800,
    ""weight"": null,
    ""flags"": null,
    ""tag"": null
  }
}
";

    private const string _readResponse = @"
{
  ""domain_records"": [
    {
      ""id"": 28448432,
      ""type"": ""A"",
      ""name"": ""test"",
      ""data"": ""100.100.100.100"",
      ""priority"": null,
      ""port"": null,
      ""ttl"": 1800,
      ""weight"": null,
      ""flags"": null,
      ""tag"": null
    }
  ],
  ""links"": {
  },
  ""meta"": {
    ""total"": 1
  }
}
";

    private const string _updateResponse = @"
{
  ""domain_record"": {
    ""id"": 3352896,
    ""type"": ""A"",
    ""name"": ""test"",
    ""data"": ""100.100.100.100"",
    ""priority"": null,
    ""port"": null,
    ""ttl"": 1800,
    ""weight"": null,
    ""flags"": null,
    ""tag"": null
  }
}
";

    private readonly HttpClient _httpClient;
    private readonly HttpTest _httpTest;

    public DigitalOceanClientTests()
    {
        _httpClient = new HttpClient();
        _httpTest = new HttpTest();
    }

    [Fact]
    public async Task Create_Failure_Is_Fail_Result()
    {
        _httpTest.RespondWith(string.Empty, (int)HttpStatusCode.BadRequest);
        IDigitalOceanClient client = new DigitalOceanClient(_httpClient);

        DigitalOceanCreateDomainRecordRequest request = new()
        {
            Data = "100.100.100.100",
            Name = "test",
            Ttl = 1800,
            Type = DNSRecordType.A
        };

        Result response = await client.CreateDNSRecordAsync("test.com", request, string.Empty, CancellationToken.None);
        Assert.True(response.IsFailed);
    }

    [Fact]
    public async Task Create_Success_Is_Success_Result()
    {
        _httpTest.RespondWith(_createResponse, (int)HttpStatusCode.Created);
        IDigitalOceanClient client = new DigitalOceanClient(_httpClient);

        DigitalOceanCreateDomainRecordRequest request = new()
        {
            Data = "100.100.100.100",
            Name = "test",
            Ttl = 1800,
            Type = DNSRecordType.A
        };

        Result response = await client.CreateDNSRecordAsync("test.com", request, string.Empty, CancellationToken.None);
        Assert.True(response.IsSuccess);
    }

    public void Dispose()
    {
        _httpClient.Dispose();
        _httpTest.Dispose();
    }

    [Fact]
    public async Task Read_Failure_Is_Fail_Result()
    {
        _httpTest.RespondWith(string.Empty, (int)HttpStatusCode.NotFound);
        IDigitalOceanClient client = new DigitalOceanClient(_httpClient);

        DigitalOceanDomain domain = new()
        {
            Name = "test.com"
        };

        Result<DigitalOceanGetDomainRecordsResponse> response = await client.GetDNSRecordsAsync(domain, string.Empty, CancellationToken.None);
        Assert.True(response.IsFailed);
    }

    [Fact]
    public async Task Read_Success_Is_Success_Result_And_Contains_DNSRecord()
    {
        _httpTest.RespondWith(_readResponse);
        IDigitalOceanClient client = new DigitalOceanClient(_httpClient);

        DigitalOceanDomain domain = new()
        {
            Name = "test.com"
        };

        Result<DigitalOceanGetDomainRecordsResponse> response = await client.GetDNSRecordsAsync(domain, string.Empty, CancellationToken.None);
        Assert.True(response.IsSuccess);

        IList<DigitalOceanGetDomainRecordResponse> domainRecords = response.Value.DomainRecords.ToList();
        Assert.True(domainRecords.Count() == 1);

        DigitalOceanGetDomainRecordResponse dnsRecord = domainRecords.First();
        Assert.Equal("100.100.100.100", dnsRecord.Data);
        Assert.Equal(28448432, dnsRecord.Id);
        Assert.Equal("test", dnsRecord.Name);
        Assert.Equal(DNSRecordType.A, dnsRecord.Type);
    }

    [Fact]
    public async Task Update_Failure_Is_Fail_Result()
    {
        _httpTest.RespondWith(string.Empty, (int)HttpStatusCode.BadRequest);
        IDigitalOceanClient client = new DigitalOceanClient(_httpClient);

        DigitalOceanUpdateDomainRecordRequest request = new()
        {
            Data = "",
            Id = "3352896",
            Name = "test",
            Ttl = 1800,
            Type = DNSRecordType.A
        };

        Result<DigitalOceanGetDomainRecordsResponse> response = await client.UpdateDNSRecordAsync("test.com", request, string.Empty, CancellationToken.None);
        Assert.True(response.IsFailed);
    }

    [Fact]
    public async Task Update_Success_Is_Success_Result()
    {
        _httpTest.RespondWith(_updateResponse);
        IDigitalOceanClient client = new DigitalOceanClient(_httpClient);

        DigitalOceanUpdateDomainRecordRequest request = new()
        {
            Data = "",
            Id = "3352896",
            Name = "test",
            Ttl = 1800,
            Type = DNSRecordType.A
        };

        Result<DigitalOceanGetDomainRecordsResponse> response = await client.UpdateDNSRecordAsync("test.com", request, string.Empty, CancellationToken.None);
        Assert.True(response.IsSuccess);
    }
}