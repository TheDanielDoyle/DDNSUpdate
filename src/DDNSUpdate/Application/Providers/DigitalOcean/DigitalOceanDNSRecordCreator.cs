using AutoMapper;
using DDNSUpdate.Application.Providers.DigitalOcean.Requests;
using DDNSUpdate.Domain;
using DDNSUpdate.Infrastructure.Extensions;
using FluentResults;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public class DigitalOceanDNSRecordCreator : IDigitalOceanDNSRecordCreator
    {
        private readonly IDigitalOceanClient _digitalOceanClient;
        private readonly IMapper _mapper;

        public DigitalOceanDNSRecordCreator(IDigitalOceanClient digitalOceanClient, IMapper mapper)
        {
            _digitalOceanClient = digitalOceanClient;
            _mapper = mapper;
        }

        public async Task<Result> CreateAsync(string domainName, DNSRecordCollection dnsRecords, string token, CancellationToken cancellation)
        {
            Result result = Result.Ok();
            IEnumerable<DigitalOceanCreateDomainRecordRequest> requests = _mapper.Map<IEnumerable<DigitalOceanCreateDomainRecordRequest>>(dnsRecords);
            foreach (DigitalOceanCreateDomainRecordRequest request in requests)
            {
                Result createResult = await _digitalOceanClient.CreateDNSRecordAsync(domainName, request, token, cancellation);
                result = result.Merge(createResult);
            }
            return result;
        }
    }
}
