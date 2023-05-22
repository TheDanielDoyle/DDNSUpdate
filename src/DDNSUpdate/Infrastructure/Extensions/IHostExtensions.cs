using DDNSUpdate.Infrastructure.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StartupSettingsValidationResult = DDNSUpdate.Infrastructure.Settings.StartupSettingsValidationResult;

namespace DDNSUpdate.Infrastructure.Extensions;

internal static class IHostExtensions
{
    public static StartupSettingsValidationResult ValidateSettings(this IHost host)
    {
        ISettingsValidator validator = host.Services
            .GetRequiredService<ISettingsValidator>();
        
        return validator
            .Validate()
            .Match<StartupSettingsValidationResult>(
                valid => (valid, host), 
                invalid => invalid);
    }
}