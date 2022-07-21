using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using DDNSUpdate.Application.Providers.GoDaddy.Request;
using DDNSUpdate.Domain;
using FluentResults;

namespace DDNSUpdate.Application.Providers.GoDaddy;

public class GoDaddyDNSRecordCreator : IGoDaddyDNSRecordCreator
{
    private readonly IGoDaddyClient _client;
    private readonly IMapper _mapper;

    public GoDaddyDNSRecordCreator(IGoDaddyClient client, IMapper mapper)
    {
        _client = client;
        _mapper = mapper;
    }

    public async Task<Result> CreateAsync(string domainName, DNSRecordCollection records, GoDaddyAuthenticationDetails authentication, CancellationToken cancellation)
    {
        if (records.Any())
        {
            IEnumerable<GoDaddyCreateDNSRecordRequest> recordRequests = _mapper.Map<IEnumerable<GoDaddyCreateDNSRecordRequest>>(records);
            GoDaddyCreateDNSRecordsRequest request = new GoDaddyCreateDNSRecordsRequest(authentication.ApiKey, authentication.ApiSecret, recordRequests, domainName);
            return await _client.CreateDNSRecordsAsync(request, cancellation);
        }
        return Result.Ok();
    }
}