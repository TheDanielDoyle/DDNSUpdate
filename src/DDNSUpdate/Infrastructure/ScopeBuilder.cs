using System;
using Microsoft.Extensions.DependencyInjection;

namespace DDNSUpdate.Infrastructure
{
    public class ScopeBuilder : IScopeBuilder
    {
        private readonly IServiceProvider _serviceProvider;

        public ScopeBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IServiceScope Build()
        {
            return _serviceProvider.CreateScope();
        }
    }
}
