using Microsoft.Extensions.DependencyInjection;
using System;

namespace DDNSUpdate.Infrastructure
{
    public class ScopeBuilder : IScopeBuilder
    {
        private readonly ServiceFactory _serviceFactory;

        public ScopeBuilder(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public IServiceScope Build()
        {
            IServiceProvider serviceProvider = (IServiceProvider)_serviceFactory(typeof(IServiceProvider));
            return serviceProvider.CreateScope();
        }
    }
}
