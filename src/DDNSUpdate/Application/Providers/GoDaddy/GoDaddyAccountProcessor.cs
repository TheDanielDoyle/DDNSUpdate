using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using DDNSUpdate.Domain;
using DDNSUpdate.Infrastructure.Extensions;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.Providers.GoDaddy
{
    public class GoDaddyAccountProcessor : IGoDaddyAccountProcessor
    {
        private readonly IGoDaddyDomainProcessor _domainProcessor;

        public GoDaddyAccountProcessor(IGoDaddyDomainProcessor domainProcessor)
        {
            _domainProcessor = domainProcessor;
        }

        public async Task<Result> ProcessAsync(GoDaddyAccount account, ExternalAddress externalAddress, CancellationToken cancellation)
        {
            Result result = Result.Ok();
            GoDaddyAuthenticationDetails authenticationDetails = new GoDaddyAuthenticationDetails(account.ApiKey, account.ApiSecret);
            foreach(GoDaddyDomain domain in account.Domains)
            {
                result = result.Merge(await _domainProcessor.ProcessAsync(domain, externalAddress, authenticationDetails, cancellation));
            }
            return result;
        }
    }
}
