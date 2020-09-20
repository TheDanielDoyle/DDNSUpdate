using Microsoft.Extensions.DependencyInjection;

namespace DDNSUpdate.Infrastructure
{
    public interface IScopeBuilder
    {
        IServiceScope Build();
    }
}
