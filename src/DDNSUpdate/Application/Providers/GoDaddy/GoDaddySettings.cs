using System.Collections.Generic;

namespace DDNSUpdate.Application.Providers.GoDaddy;

internal sealed record GoDaddySettings
{
    public List<GoDaddyAccount>? Accounts { get; set; } = [];

    public bool HasAccounts()
    {
        return Accounts is not null && Accounts.Count != 0;
    }
}