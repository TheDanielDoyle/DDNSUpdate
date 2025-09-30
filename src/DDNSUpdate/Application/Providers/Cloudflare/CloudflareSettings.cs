using System.Collections.Generic;

namespace DDNSUpdate.Application.Providers.Cloudflare;

internal sealed record CloudflareSettings
{
    public List<CloudflareAccount>? Accounts { get; set; } = [];

    public bool HasAccounts()
    {
        return Accounts is not null && Accounts.Count != 0;
    }
}