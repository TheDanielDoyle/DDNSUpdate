using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Configuration;
using DDNSUpdate.Infrastructure;
using FluentResults;
using DDNSUpdate.Application.ExternalAddresses;

namespace DDNSUpdate.Application
{
    public class DDNSUpdateInvoker : IDDNSUpdateInvoker
    {
        private readonly IConfigurationValidator _configurationValidator;
        private readonly IScopeBuilder _scopeBuilder;

        public DDNSUpdateInvoker(IConfigurationValidator configurationValidator, IScopeBuilder scopeBuilder)
        {
            _configurationValidator = configurationValidator;
            _scopeBuilder = scopeBuilder;
        }

        public async Task<Result> InvokeAsync(CancellationToken cancellation)
        {
            using(IServiceScope scope = _scopeBuilder.Build())
            {
                Result validateConfigurationResult = await _configurationValidator.ValidateAsync(cancellation);
                if (validateConfigurationResult.IsFailed)
                {
                    return validateConfigurationResult;
                }

                IExternalAddressClient externalAddressClient = GetService<IExternalAddressClient>(scope);
                Result<IExternalAddressResponse> externalAddressResult = await externalAddressClient.GetAsync(cancellation);
                if (externalAddressResult.IsFailed)
                {
                    return externalAddressResult;
                }

                IEnumerable<IDDNSService> dnsServices = GetService<IEnumerable<IDDNSService>>(scope);
                return await ProcessAsync(dnsServices, externalAddressResult.Value, cancellation);
                
            }
        }

        private static T GetService<T>(IServiceScope scope)
        {
            return (T)scope.ServiceProvider.GetService(typeof(T));
        }
        
        private async Task<Result> ProcessAsync(IEnumerable<IDDNSService> dnsServices, IExternalAddressResponse value, CancellationToken cancellation)
        {
            Result result = Result.Ok();
            foreach (IDDNSService dnsService in dnsServices)
            {
                result = Result.Merge(result, await dnsService.ProcessAsync(value.ExternalAddress, cancellation));
            }
            return result;
        }
    }
}
