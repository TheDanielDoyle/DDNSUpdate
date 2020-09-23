using DDNSUpdate.Domain;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public class DigitalOceanDDNSService : IDDNSService
    {
        private readonly DigitalOceanConfiguration _configuration;
        private readonly IDigitalOceanAccountProcessor _accountProcessor;

        public DigitalOceanDDNSService(DigitalOceanConfiguration configuration, IDigitalOceanAccountProcessor accountProcessor)
        {
            _configuration = configuration;
            _accountProcessor = accountProcessor;
        }

        public async Task<Result> ProcessAsync(ExternalAddress externalAddress, CancellationToken cancellation)
        {
            Result result = Result.Ok();
            foreach (DigitalOceanAccount account in _configuration.Accounts)
            {
                result = Result.Merge(await _accountProcessor.ProcessAsync(account, externalAddress, cancellation));
            }
            return result;
        }
    }
}