using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using DDNSUpdate.Domain;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.Providers.GoDaddy;

public interface IGoDaddyAccountProcessor
{
    Task<Result> ProcessAsync(GoDaddyAccount account, ExternalAddress externalAddress, CancellationToken cancellation);
}