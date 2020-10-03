using DDNSUpdate.Application.Providers.DigitalOcean.Configuration;
using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using DDNSUpdate.Domain;
using DDNSUpdate.Infrastructure.Extensions;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public class DigitalOceanDDNSService : IDDNSService
    {
        private readonly IDigitalOceanAccountProcessor _accountProcessor;
        private readonly DigitalOceanConfiguration _configuration;

        public DigitalOceanDDNSService(IOptionsSnapshot<DigitalOceanConfiguration> configuration, IDigitalOceanAccountProcessor accountProcessor)
        {
            _configuration = configuration.Value;
            _accountProcessor = accountProcessor;
        }

        public async Task<Result> ProcessAsync(ExternalAddress externalAddress, CancellationToken cancellation)
        {
            Result result = Result.Ok();
            foreach (DigitalOceanAccount account in _configuration.Accounts)
            {
                result = result.Merge(await _accountProcessor.ProcessAsync(account, externalAddress, cancellation));
            }
            return result;
        }
    }
}
