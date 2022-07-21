using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using DDNSUpdate.Domain;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.Providers.GoDaddy;

public interface IGoDaddyDomainProcessor
{
    Task<Result> ProcessAsync(GoDaddyDomain domain, ExternalAddress externalAddress, GoDaddyAuthenticationDetails authentication, CancellationToken cancellation);
}