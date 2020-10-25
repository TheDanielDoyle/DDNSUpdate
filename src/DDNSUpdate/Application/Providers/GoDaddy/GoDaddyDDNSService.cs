using DDNSUpdate.Application.Providers.GoDaddy.Configuration;
using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using DDNSUpdate.Domain;
using DDNSUpdate.Infrastructure.Extensions;
using FluentResults;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.Providers.GoDaddy
{
    public class GoDaddyDDNSService : IDDNSService
    {
        private readonly IGoDaddyAccountProcessor _accountProcessor;
        private readonly GoDaddyConfiguration _configuration;

        public GoDaddyDDNSService(IGoDaddyAccountProcessor accountProcessor, IOptionsSnapshot<GoDaddyConfiguration> configuration)
        {
            _accountProcessor = accountProcessor;
            _configuration = configuration.Value;
        }

        public async Task<Result> ProcessAsync(ExternalAddress externalAddress, CancellationToken cancellation)
        {
            Result result = Result.Ok();
            foreach (GoDaddyAccount account in _configuration.Accounts)
            {
                result = result.Merge(await _accountProcessor.ProcessAsync(account, externalAddress, cancellation));
            }
            return result;
        }
    }
}
