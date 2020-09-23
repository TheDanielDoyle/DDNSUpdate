using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Domain;
using DnsZone;
using FluentResults;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public interface IDigitalOceanClient
    {
        Task<Result> CreateDNSRecordAsync(DNSRecord dnsRecord, string token, CancellationToken cancellation);

        Task<Result<DnsZoneFile>> GetDNSZoneAsync(DigitalOceanDomain domain, string token, CancellationToken cancellation);

        Task<Result> UpdateDNSRecordAsync(DNSRecord dnsRecord, string token, CancellationToken cancellation);
    }
}