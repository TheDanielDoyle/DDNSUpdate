using DDNSUpdate.Infrastructure.Extensions;
using DDNSUpdate.Infrastructure.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DDNSUpdate.Infrastructure.Profiles;

internal sealed class SettingsProfile : IHostBuilderProfile
{
    public HostApplicationBuilder Add(HostApplicationBuilder builder)
    {
        builder
            .AddSettings<AppSettings>()
            .Services
            .AddTransient<ISettingsValidator, SettingsValidator>();
        return builder;
    }
}