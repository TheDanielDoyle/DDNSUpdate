using System;
using System.Collections.Generic;
using System.Linq;
using DDNSUpdate.Infrastructure.Settings;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

internal sealed record DigitalOceanSettings : ISettings, IAccounts<DigitalOceanAccount>
{
    public IList<DigitalOceanAccount>? Accounts { get; set; }
    
    public Uri? ApiUrl { get; set; }

    public bool HasAccounts()
    {
        return Accounts is not null && Accounts.Any();
    }
}