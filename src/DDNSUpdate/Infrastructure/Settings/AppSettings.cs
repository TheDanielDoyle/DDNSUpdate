using System;
using DDNSUpdate.Application.Providers.DigitalOcean;
using DDNSUpdate.Application.Providers.GoDaddy;

namespace DDNSUpdate.Infrastructure.Settings;

internal sealed record AppSettings : ISettings
{
    public DigitalOceanSettings? DigitalOcean { get; set; }
    
    public GoDaddySettings? GoDaddy { get; set; }
    
    public TimeSpan? UpdateInterval { get; set; }

    public bool HasGoDaddyAccounts()
    {
        return GoDaddy is not null && GoDaddy.HasAccounts();
    }

    public bool HasDigitalOceanAccounts()
    {
        return DigitalOcean is not null && DigitalOcean.HasAccounts();
    }
}