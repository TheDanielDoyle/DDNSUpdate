using DDNSUpdate.Application.Providers.Cloudflare;
using DDNSUpdate.Application.Providers.DigitalOcean;
using DDNSUpdate.Application.Providers.GoDaddy;
using DDNSUpdate.Infrastructure.Extensions;
using Microsoft.Extensions.Hosting;

namespace DDNSUpdate.Infrastructure.Profiles;

internal sealed class ServiceProfile : HostApplicationBuilderProfile
{
    protected override void Add(HostApplicationBuilder builder)
    {
        builder
            .AddProfile<CloudflareProfile>()
            .AddProfile<DigitalOceanProfile>()
            .AddProfile<GoDaddyProfile>();
    }
}