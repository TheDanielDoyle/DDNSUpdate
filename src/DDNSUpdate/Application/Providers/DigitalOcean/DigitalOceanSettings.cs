using System.Collections.Generic;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

internal sealed record DigitalOceanSettings
{
    public List<DigitalOceanAccount>? Accounts { get; set; } = [];

    public bool HasAccounts()
    {
        return Accounts is not null && Accounts.Count != 0;
    }
}