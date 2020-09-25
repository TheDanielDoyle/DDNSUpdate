using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using DDNSUpdate.Domain;
using FluentResults;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public interface IDigitalOceanDomainProcessor
    {
        Task<Result> ProcessAsync(DigitalOceanDomain domain, ExternalAddress externalAddress, string token, CancellationToken cancellation);
    }
}
