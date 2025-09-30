using DDNSUpdate.Infrastructure.Profiles;
using Microsoft.Extensions.Hosting;

namespace DDNSUpdate.Application.Providers.Cloudflare;

internal sealed class CloudflareProfile : HostApplicationBuilderProfile
{
    protected override void Add(HostApplicationBuilder builder)
    {
    }
}