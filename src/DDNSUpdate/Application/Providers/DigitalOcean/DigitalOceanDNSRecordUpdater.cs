﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DDNSUpdate.Application.Providers.DigitalOcean.Requests;
using DDNSUpdate.Domain;
using FluentResults;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public class DigitalOceanDNSRecordUpdater : IDigitalOceanDNSRecordUpdater
    {
        private readonly IDigitalOceanClient _digitalOceanClient;
        private readonly IMapper _mapper;

        public DigitalOceanDNSRecordUpdater(IDigitalOceanClient digitalOceanClient, IMapper mapper)
        {
            _digitalOceanClient = digitalOceanClient;
            _mapper = mapper;
        }

        public async Task<Result> UpdateAsync(DNSRecordCollection dnsRecords, string token, CancellationToken cancellation)
        {
            Result result = Result.Ok();
            IEnumerable<DigitalOceanUpdateDomainRecordRequest> requests = _mapper.Map<IEnumerable<DigitalOceanUpdateDomainRecordRequest>>(dnsRecords);
            foreach (DigitalOceanUpdateDomainRecordRequest request in requests)
            {
                Result updateResult = await _digitalOceanClient.UpdateDNSRecordAsync(request, token, cancellation);
                Result.Merge(result, updateResult);
            }
            return result;
        }
    }
}