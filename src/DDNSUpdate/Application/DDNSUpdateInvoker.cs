using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Infrastructure;
using DDNSUpdate.Infrastructure.Extensions;
using FluentResults;
using DDNSUpdate.Application.ExternalAddresses;
using Microsoft.Extensions.Logging;

namespace DDNSUpdate.Application
{
    public class DDNSUpdateInvoker : IDDNSUpdateInvoker
    {
        private readonly ILogger _logger;
        private readonly IScopeBuilder _scopeBuilder;

        public DDNSUpdateInvoker(ILogger<DDNSUpdateInvoker> logger, IScopeBuilder scopeBuilder)
        {
            _logger = logger;
            _scopeBuilder = scopeBuilder;
        }

        public async Task InvokeAsync(CancellationToken cancellation)
        {
            using(IServiceScope scope = _scopeBuilder.Build())
            {
                IExternalAddressClient externalAddressClient = GetService<IExternalAddressClient>(scope);
                Result<IExternalAddressResponse> externalAddressResult = await externalAddressClient.GetAsync(cancellation);
                if (externalAddressResult.IsSuccess)
                {
                    IEnumerable<IDDNSService> services = GetService<IEnumerable<IDDNSService>>(scope);
                    IEnumerable<Task> updateTasks = services.Select(async service => service.ProcessAsync(cancellation));
                    await Task.WhenAll(updateTasks).WithAggregatedExceptions();
                }
                else
                {
                    _logger.LogError("Cannot process DNS records - Unable to get external IP address.");
                }
            }
        }

        private static T GetService<T>(IServiceScope scope)
        {
            return (T)scope.ServiceProvider.GetService(typeof(T));
        }
    }
}
