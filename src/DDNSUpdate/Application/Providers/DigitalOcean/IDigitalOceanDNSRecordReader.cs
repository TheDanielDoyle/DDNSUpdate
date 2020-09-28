using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using DDNSUpdate.Domain;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public interface IDigitalOceanDNSRecordReader
    {
        Task<Result<DNSRecordCollection>> ReadAsync(DigitalOceanDomain domain, string token, CancellationToken cancellation);
    }
}
