using DDNSUpdate.Infrastructure.Profiles;
using Microsoft.Extensions.Hosting;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

internal sealed class DigitalOceanProfile : HostApplicationBuilderProfile
{
    protected override void Add(HostApplicationBuilder builder)
    {
    }
}