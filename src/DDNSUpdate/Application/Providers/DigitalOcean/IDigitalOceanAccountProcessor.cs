using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Domain;
using FluentResults;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public interface IDigitalOceanAccountProcessor
    {
        Task<Result> ProcessAsync(DigitalOceanAccount account, ExternalAddress externalAddress, CancellationToken cancellation);
    }
}