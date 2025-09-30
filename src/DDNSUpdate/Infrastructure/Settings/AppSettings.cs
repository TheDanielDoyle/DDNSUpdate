using System;

namespace DDNSUpdate.Infrastructure.Settings;

internal sealed record AppSettings : ISettings
{
    public ProviderSettings? Providers { get; set; }
    
    public TimeSpan? UpdateInterval { get; set; }
}