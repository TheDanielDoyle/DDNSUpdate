using System.Collections.Generic;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

internal sealed record DigitalOceanDomain
{
    public string? Name { get; set; }
    
    public IList<DigitalOceanDomainRecord>? Records { get; set; }
}