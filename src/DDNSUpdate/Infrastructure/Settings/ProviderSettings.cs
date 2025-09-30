using DDNSUpdate.Application.Providers.Cloudflare;
using DDNSUpdate.Application.Providers.DigitalOcean;
using DDNSUpdate.Application.Providers.GoDaddy;

namespace DDNSUpdate.Infrastructure.Settings;

internal sealed record ProviderSettings
{
    public CloudflareSettings? Cloudflare { get; set; }
    
    public DigitalOceanSettings? DigitalOcean { get; set; }
    
    public GoDaddySettings? GoDaddy { get; set; }
    
    public bool HasCloudflareAccounts()
    {
        return Cloudflare is not null  &&  Cloudflare.HasAccounts();
    }
    
    public bool HasDigitalOceanAccounts()
    {
        return DigitalOcean is not null  &&  DigitalOcean.HasAccounts();
    }
    
    public bool HasGoDaddyAccounts()
    {
        return GoDaddy is not null  && GoDaddy.HasAccounts();
    }
}