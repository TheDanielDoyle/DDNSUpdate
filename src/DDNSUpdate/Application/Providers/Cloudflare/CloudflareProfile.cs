using DDNSUpdate.Infrastructure.Profiles;
using Microsoft.Extensions.Hosting;

namespace DDNSUpdate.Application.Providers.Cloudflare;

internal sealed class CloudflareProfile : IHostBuilderProfile
{
    public HostApplicationBuilder Add(HostApplicationBuilder builder)
    {
        return builder;
    }
}