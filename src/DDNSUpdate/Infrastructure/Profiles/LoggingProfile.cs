using Microsoft.Extensions.Hosting;
using Serilog;

namespace DDNSUpdate.Infrastructure.Profiles;

internal sealed class LoggingProfile : IHostBuilderProfile
{
    public HostApplicationBuilder Add(HostApplicationBuilder builder)
    {
        builder.Services
            .AddSerilog(configuration =>
            {
                configuration.ReadFrom.Configuration(builder.Configuration);
            });
        return builder;
    }
}