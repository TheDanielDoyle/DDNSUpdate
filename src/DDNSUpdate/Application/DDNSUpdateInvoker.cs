using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Infrastructure;
using System;
using DDNSUpdate.Infrastructure.Extensions;

namespace DDNSUpdate.Application
{
    public class DDNSUpdateInvoker : IDDNSUpdateInvoker
    {
        private readonly IScopeBuilder _scopeBuilder;

        public DDNSUpdateInvoker(IScopeBuilder scopeBuilder)
        {
            _scopeBuilder = scopeBuilder;
        }

        public async Task InvokeAsync(CancellationToken cancellation)
        {
            using(IServiceScope scope = _scopeBuilder.Build())
            {
                IEnumerable<IDDNSService> services = (IEnumerable<IDDNSService>)scope.ServiceProvider.GetService(typeof(IEnumerable<IDDNSService>));
                IEnumerable<Task> updateTasks = services.Select(async service => service.ProcessAsync(cancellation));
                await Task.WhenAll(updateTasks).WithAggregatedExceptions();
            }
        }
    }
}
