using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using DDNSUpdate.Application.Providers.DigitalOcean.Responses;
using DDNSUpdate.Domain;
using FluentResults;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public class DigitalOceanDNSRecordReader : IDigitalOceanDNSRecordReader
    {
        private readonly IDigitalOceanClient _client;
        private readonly IMapper _mapper;

        public DigitalOceanDNSRecordReader(IDigitalOceanClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<Result<DNSRecordCollection>> ReadAsync(DigitalOceanDomain domain, string token, CancellationToken cancellation)
        {
            Result<DigitalOceanGetDomainRecordsResponse> result = await _client.GetDNSRecordsAsync(domain, token, cancellation);
            if (result.IsFailed)
            {
                return result.ToResult();
            }

            IEnumerable<DNSRecord> records = _mapper.Map<IEnumerable<DNSRecord>>(result.Value.DomainRecords);
            return Result.Ok(new DNSRecordCollection(records));
        }
    }
}