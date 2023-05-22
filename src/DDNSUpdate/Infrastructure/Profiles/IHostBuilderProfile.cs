using Microsoft.Extensions.Hosting;

namespace DDNSUpdate.Infrastructure.Profiles;

internal interface IHostBuilderProfile
{
    public HostApplicationBuilder Add(HostApplicationBuilder builder);
}