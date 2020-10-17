using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using DDNSUpdate.Application.Providers.GoDaddy.Request;
using DDNSUpdate.Application.Providers.GoDaddy.Response;
using DDNSUpdate.Domain;
using FluentResults;

namespace DDNSUpdate.Application.Providers.GoDaddy
{
    public class GoDadyDNSRecordReader : IGoDaddyDNSRecordReader
    {
        private readonly IGoDaddyClient _client;
        private readonly IMapper _mapper;

        public GoDadyDNSRecordReader(IGoDaddyClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }
        public async Task<Result<DNSRecordCollection>> ReadAsync(GoDaddyDomain domain, string apiKey, string apiSecret, CancellationToken cancellation)
        {
            GoDaddyGetDNSRecordsRequest request = new GoDaddyGetDNSRecordsRequest(apiKey, apiSecret, DNSRecordType.A, domain.Name);
            Result<GoDaddyGetDNSRecordsResponse> result = await _client.GetDNSRecordsAsync(request, cancellation);
            if (result.IsSuccess)
            {
                IList<DNSRecord> records = _mapper.Map<IList<DNSRecord>>(result.Value.Records);
                return Result.Ok(new DNSRecordCollection(records));
            }
            return Result.Fail($"Unable to get Records for domain {domain.Name}");
        }
    }
}