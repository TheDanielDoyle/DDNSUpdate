using Microsoft.Extensions.Hosting;

namespace DDNSUpdate.Infrastructure.Profiles;

internal interface IHostApplicationBuilderProfile
{
    public HostApplicationBuilder Add(HostApplicationBuilder builder);
}