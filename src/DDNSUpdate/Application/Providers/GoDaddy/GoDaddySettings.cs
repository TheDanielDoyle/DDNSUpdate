using System.Collections.Generic;
using System.Linq;
using DDNSUpdate.Infrastructure.Settings;

namespace DDNSUpdate.Application.Providers.GoDaddy;

internal sealed record GoDaddySettings : ISettings, IAccounts<GoDaddyAccount>
{
    public IList<GoDaddyAccount>? Accounts { get; set; }

    public bool HasAccounts()
    {
        return Accounts is not null && Accounts.Any();
    }
}