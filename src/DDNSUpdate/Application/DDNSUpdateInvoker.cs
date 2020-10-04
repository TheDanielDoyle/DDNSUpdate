using DDNSUpdate.Application.Configuration;
using DDNSUpdate.Application.ExternalAddresses;
using DDNSUpdate.Infrastructure;
using DDNSUpdate.Infrastructure.Extensions;
using FluentResults;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application
{
    public class DDNSUpdateInvoker : IDDNSUpdateInvoker
    {
        private readonly IScopeBuilder _scopeBuilder;

        public DDNSUpdateInvoker(IScopeBuilder scopeBuilder)
        {
            _scopeBuilder = scopeBuilder;
        }

        public async Task<Result> InvokeAsync(CancellationToken cancellation)
        {
            using (IServiceScope scope = _scopeBuilder.Build())
            {
                IConfigurationValidator configurationValidator = GetService<IConfigurationValidator>(scope);
                Result validateConfigurationResult = await configurationValidator.ValidateAsync(cancellation);
                if (validateConfigurationResult.IsFailed)
                {
                    return validateConfigurationResult;
                }

                IExternalAddressClient externalAddressClient = GetService<IExternalAddressClient>(scope);
                Result<IExternalAddressResponse> externalAddressResult = await externalAddressClient.GetAsync(cancellation);
                if (externalAddressResult.IsFailed)
                {
                    return externalAddressResult.Merge(validateConfigurationResult);
                }

                IEnumerable<IDDNSService> dnsServices = GetService<IEnumerable<IDDNSService>>(scope);
                Result processResult = await ProcessAsync(dnsServices, externalAddressResult.Value, cancellation);
                return validateConfigurationResult.Merge(externalAddressResult, processResult);
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
                result = result.Merge(await dnsService.ProcessAsync(value.ExternalAddress, cancellation));
            }
            return result;
        }
    }
}
