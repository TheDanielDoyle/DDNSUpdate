using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using DDNSUpdate.Domain;
using FluentResults;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public class DigitalOceanAccountProcessor : IDigitalOceanAccountProcessor
    {
        private readonly IDigitalOceanDomainProcessor _domainProcessor;

        public DigitalOceanAccountProcessor(IDigitalOceanDomainProcessor domainProcessor)
        {
            _domainProcessor = domainProcessor;
        }

        public async Task<Result> ProcessAsync(DigitalOceanAccount account, ExternalAddress externalAddress, CancellationToken cancellation)
        {
            Result result = Result.Ok();
            foreach (DigitalOceanDomain domain in account.Domains)
            {
                result = Result.Merge(await _domainProcessor.ProcessAsync(domain, externalAddress, account.Token, cancellation));
            }
            return result;
        }
    }
}