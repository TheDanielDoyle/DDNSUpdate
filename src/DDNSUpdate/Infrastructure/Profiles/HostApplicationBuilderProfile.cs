using Microsoft.Extensions.Hosting;

namespace DDNSUpdate.Infrastructure.Profiles;

internal abstract class HostApplicationBuilderProfile : IHostApplicationBuilderProfile
{
    protected abstract void Add(HostApplicationBuilder builder);
    
    HostApplicationBuilder IHostApplicationBuilderProfile.Add(HostApplicationBuilder builder)
    {
        Add(builder);
        return builder;
    }
}