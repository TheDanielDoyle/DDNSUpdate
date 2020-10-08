using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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

        public async Task<Result> UpdateAsync(string domainName, DNSRecordCollection records, string apiKey, string apiSecret, CancellationToken cancellation)
        {
            IEnumerable<GoDaddyUpdateDNSRecordRequest> requests = _mapper.Map<IEnumerable<GoDaddyUpdateDNSRecordRequest>>(records);
            GoDaddyUpdateDNSRecordsRequest request = new GoDaddyUpdateDNSRecordsRequest(apiKey, apiSecret, DNSRecordType.A, domainName, requests);
            return await _client.UpdateDNSRecordsAsync(request, cancellation);
        }
    }
}