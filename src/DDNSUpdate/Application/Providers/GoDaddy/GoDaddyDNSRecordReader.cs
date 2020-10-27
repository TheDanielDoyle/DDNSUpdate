using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using DDNSUpdate.Application.Providers.GoDaddy.Request;
using DDNSUpdate.Application.Providers.GoDaddy.Response;
using DDNSUpdate.Domain;
using DDNSUpdate.Infrastructure.Extensions;
using FluentResults;

namespace DDNSUpdate.Application.Providers.GoDaddy
{
    public class GoDaddyDNSRecordReader : IGoDaddyDNSRecordReader
    {
        private readonly IGoDaddyClient _client;
        private readonly IMapper _mapper;

        public GoDaddyDNSRecordReader(IGoDaddyClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }
        public async Task<Result<DNSRecordCollection>> ReadAsync(string domainName, GoDaddyAuthenticationDetails authentication, CancellationToken cancellation)
        {
            GoDaddyGetDNSRecordsRequest request = new GoDaddyGetDNSRecordsRequest(authentication.ApiKey, authentication.ApiSecret, DNSRecordType.A, domainName);
            Result<GoDaddyGetDNSRecordsResponse> result = await _client.GetDNSRecordsAsync(request, cancellation);
            if (result.IsSuccess)
            {
                IList<DNSRecord> records = _mapper.Map<IList<DNSRecord>>(result.Value.Records);
                return Result.Ok(new DNSRecordCollection(records)).Merge(result);
            }
            return result.ToResult();
        }
    }
}