using System.Collections.Generic;
using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.Providers.DigitalOcean.Domain
{
    public record DigitalOceanDomain
    {
        public string Name { get; set; } = default!;

        public IList<DNSRecord> Records { get; set; } = new List<DNSRecord>();
    }
}
