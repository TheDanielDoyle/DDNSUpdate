using AutoMapper;
using DDNSUpdate.Application.Providers.DigitalOcean.Requests;
using DDNSUpdate.Domain;
using DDNSUpdate.Infrastructure.Extensions;
using FluentResults;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

public class DigitalOceanDNSRecordUpdater : IDigitalOceanDNSRecordUpdater
{
    private readonly IDigitalOceanClient _digitalOceanClient;
    private readonly IMapper _mapper;

    public DigitalOceanDNSRecordUpdater(IDigitalOceanClient digitalOceanClient, IMapper mapper)
    {
        _digitalOceanClient = digitalOceanClient;
        _mapper = mapper;
    }

    public async Task<Result> UpdateAsync(string domainName, DNSRecordCollection dnsRecords, string token, CancellationToken cancellation)
    {
        Result result = Result.Ok();
        IEnumerable<DigitalOceanUpdateDomainRecordRequest> requests = _mapper.Map<IEnumerable<DigitalOceanUpdateDomainRecordRequest>>(dnsRecords);
        foreach (DigitalOceanUpdateDomainRecordRequest request in requests)
        {
            Result updateResult = await _digitalOceanClient.UpdateDNSRecordAsync(domainName, request, token, cancellation);
            result = result.Merge(updateResult);
        }

        return result;
    }
}