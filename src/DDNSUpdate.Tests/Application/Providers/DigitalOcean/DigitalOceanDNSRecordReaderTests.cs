using DDNSUpdate.Application.Providers.DigitalOcean;
using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using DDNSUpdate.Application.Providers.DigitalOcean.Responses;
using DDNSUpdate.Domain;
using DDNSUpdate.Tests.Helpers;
using FakeItEasy;
using FluentResults;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.DigitalOcean;

public class DigitalOceanDNSRecordReaderTests : TestBase
{
    private readonly MappingHelper _mappingHelper;

    public DigitalOceanDNSRecordReaderTests()
    {
        _mappingHelper = new MappingHelper(AssembliesUnderTest);
    }

    [Fact]
    public async Task Record_Is_Successfully_Retrieved()
    {
        string ipAddress = "100.100.100.100";
        string name = "test";
        int TTL = 1800;
        DNSRecordType type = DNSRecordType.A;

        DigitalOceanDomain domain = new();
        DigitalOceanGetDomainRecordsResponse clientResponse = new()
        {
            DomainRecords = new List<DigitalOceanGetDomainRecordResponse>
            {
                new DigitalOceanGetDomainRecordResponse
                {
                    Data = ipAddress,
                    Name = name,
                    Ttl = TTL,
                    Type = type
                }
            }
        };
        Result<DigitalOceanGetDomainRecordsResponse> clientResponeResult = Result.Ok(clientResponse);

        IDigitalOceanClient client = A.Fake<IDigitalOceanClient>();
        IDigitalOceanDNSRecordReader reader = new DigitalOceanDNSRecordReader(client, _mappingHelper.Mapper);

        A.CallTo(() => client.GetDNSRecordsAsync(A<DigitalOceanDomain>.Ignored, A<string>.Ignored, A<CancellationToken>.Ignored)).Returns(clientResponeResult);

        Result<DNSRecordCollection> result = await reader.ReadAsync(domain, string.Empty, CancellationToken.None);
        Assert.True(result.IsSuccess);

        DNSRecordCollection dnsRecords = result.Value;
        Assert.True(dnsRecords.Count == 1);

        DNSRecord dnsRecord = result.Value.First();
        Assert.Equal(dnsRecord.Data, ipAddress);
        Assert.Equal(dnsRecord.Name, name);
        Assert.Equal(dnsRecord.TTL, TTL);
        Assert.Equal(dnsRecord.Type, type);
    }

    [Fact]
    public async Task Record_Retrieval_Failed()
    {
        DigitalOceanDomain domain = new();
        IDigitalOceanClient client = A.Fake<IDigitalOceanClient>();
        IDigitalOceanDNSRecordReader reader = new DigitalOceanDNSRecordReader(client, _mappingHelper.Mapper);

        A.CallTo(() => client.GetDNSRecordsAsync(A<DigitalOceanDomain>.Ignored, A<string>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Fail("Error"));

        Result<DNSRecordCollection> result = await reader.ReadAsync(domain, string.Empty, CancellationToken.None);

        Assert.True(result.IsFailed);
    }
}