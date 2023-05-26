using System.Collections.Generic;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

internal sealed record DigitalOceanAccount
{
    public IList<DigitalOceanDomain>? Domains { get; set; }
    
    public string? Token { get; set; }
}