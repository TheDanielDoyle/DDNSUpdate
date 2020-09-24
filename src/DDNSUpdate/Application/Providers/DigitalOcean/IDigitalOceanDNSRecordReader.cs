using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Domain;
using FluentResults;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public interface IDigitalOceanDNSRecordReader
    {
        Task<Result<DNSRecordCollection>> ReadAsync(DigitalOceanDomain domain, string token, CancellationToken cancellation);
    }
}
