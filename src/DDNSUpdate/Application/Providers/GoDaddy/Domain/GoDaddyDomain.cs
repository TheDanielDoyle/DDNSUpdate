using DDNSUpdate.Domain;
using System.Collections.Generic;

namespace DDNSUpdate.Application.Providers.GoDaddy.Domain;

public record GoDaddyDomain
{
    public string Name { get; set; } = default!;

    public IList<DNSRecord> Records { get; set; } = new List<DNSRecord>();
}