using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using DDNSUpdate.Application.Providers.GoDaddy.Request;
using DDNSUpdate.Domain;
using DDNSUpdate.Infrastructure.Extensions;
using FluentResults;

namespace DDNSUpdate.Application.Providers.GoDaddy;

public class GoDaddyDNSRecordUpdater : IGoDaddyDNSRecordUpdater
{
    private readonly IGoDaddyClient _client;
    private readonly IMapper _mapper;

    public GoDaddyDNSRecordUpdater(IGoDaddyClient client, IMapper mapper)
    {
        _client = client;
        _mapper = mapper;
    }

    public async Task<Result> UpdateAsync(string domainName, DNSRecordCollection records, GoDaddyAuthenticationDetails authentication, CancellationToken cancellation)
    {
        Result result = Result.Ok();
        foreach (DNSRecord record in records)
        {
            GoDaddyUpdateDNSRecord mappedRecord = _mapper.Map<GoDaddyUpdateDNSRecord>(record);
            GoDaddyUpdateDNSRecordsRequest request = new GoDaddyUpdateDNSRecordsRequest(authentication.ApiKey, authentication.ApiSecret, domainName, record.Type, record.Name, mappedRecord);
            Result updateResult = await _client.UpdateDNSRecordAsync(request, cancellation);
            result = result.Merge(updateResult);
        }
        return result;
    }
}