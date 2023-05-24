using DDNSUpdate.Infrastructure.Extensions;
using Microsoft.Extensions.Hosting;

namespace DDNSUpdate.Infrastructure.Profiles;

internal sealed class ApplicationProfile : HostApplicationBuilderProfile
{
    protected override void Add(HostApplicationBuilder builder)
    {
        builder
            .AddProfile<LoggingProfile>()
            .AddProfile<ProviderProfile>()
            .AddProfile<SettingsProfile>()
            .AddProfile<ValidationProfile>();
    }
}