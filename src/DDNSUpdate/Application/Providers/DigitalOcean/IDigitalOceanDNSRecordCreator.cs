﻿using DDNSUpdate.Domain;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public interface IDigitalOceanDNSRecordCreator
    {
        Task<Result> CreateAsync(DNSRecordCollection dnsRecords, string token, CancellationToken cancellation);
    }
}
