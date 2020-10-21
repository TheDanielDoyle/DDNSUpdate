using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using DDNSUpdate.Application.Providers.GoDaddy.Request;
using DDNSUpdate.Domain;
using FluentResults;

namespace DDNSUpdate.Application.Providers.GoDaddy
{
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
            if (records.Any())
            {
                IEnumerable<GoDaddyUpdateDNSRecordRequest> requests = _mapper.Map<IEnumerable<GoDaddyUpdateDNSRecordRequest>>(records);
                GoDaddyUpdateDNSRecordsRequest request = new GoDaddyUpdateDNSRecordsRequest(authentication.ApiKey, authentication.ApiSecret, DNSRecordType.A, domainName, requests);
                return await _client.UpdateDNSRecordsAsync(request, cancellation);
            }
            return Result.Ok();
        }
    }
}