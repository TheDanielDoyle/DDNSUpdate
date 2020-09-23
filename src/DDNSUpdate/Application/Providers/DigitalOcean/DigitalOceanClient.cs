using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Domain;
using DnsZone;
using FluentResults;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public class DigitalOceanClient : IDigitalOceanClient
    {
        public Task<Result> CreateDNSRecordAsync(DNSRecord dnsRecord, string token, CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<DnsZoneFile>> GetDNSZoneAsync(DigitalOceanDomain domain, string token, CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result> UpdateDNSRecordAsync(DNSRecord dnsRecord, string token, CancellationToken cancellation)
        {
            throw new System.NotImplementedException();
        }
    }
}