using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using DDNSUpdate.Domain;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

public interface IDigitalOceanAccountProcessor
{
    Task<Result> ProcessAsync(DigitalOceanAccount account, ExternalAddress externalAddress, CancellationToken cancellation);
}