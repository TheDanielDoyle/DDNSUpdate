using Microsoft.Extensions.Hosting;
using Serilog;

namespace DDNSUpdate.Infrastructure.Profiles;

internal sealed class LoggingProfile : HostApplicationBuilderProfile
{
    protected override void Add(HostApplicationBuilder builder)
    {
        builder.Services
            .AddSerilog(configuration =>
            {
                configuration.ReadFrom.Configuration(builder.Configuration);
            });
    }
}