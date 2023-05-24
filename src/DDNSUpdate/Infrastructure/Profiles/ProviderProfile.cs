using DDNSUpdate.Application.Providers.DigitalOcean;
using DDNSUpdate.Application.Providers.GoDaddy;
using DDNSUpdate.Infrastructure.Extensions;
using Microsoft.Extensions.Hosting;

namespace DDNSUpdate.Infrastructure.Profiles;

internal sealed class ProviderProfile : HostApplicationBuilderProfile
{
    protected override void Add(HostApplicationBuilder builder)
    {
        builder
            .AddProfile<DigitalOceanProfile>()
            .AddProfile<GoDaddyProfile>();
    }
}