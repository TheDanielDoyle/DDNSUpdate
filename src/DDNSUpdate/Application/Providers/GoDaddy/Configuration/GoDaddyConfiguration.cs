using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using System.Collections.Generic;

namespace DDNSUpdate.Application.Providers.GoDaddy.Configuration;

public record GoDaddyConfiguration
{
    public IList<GoDaddyAccount> Accounts { get; set; } = new List<GoDaddyAccount>();
}