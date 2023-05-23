using DDNSUpdate.Infrastructure.Profiles;
using Microsoft.Extensions.Hosting;

namespace DDNSUpdate.Application.Providers.GoDaddy;

internal sealed class GoDaddyProfile : IHostApplicationBuilderProfile
{
    public HostApplicationBuilder Add(HostApplicationBuilder builder)
    {
        return builder;
    }
}