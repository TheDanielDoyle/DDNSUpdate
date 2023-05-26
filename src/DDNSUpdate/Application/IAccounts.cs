using System.Collections.Generic;

namespace DDNSUpdate.Application;

internal interface IAccounts<TAccount>
{
    IList<TAccount>? Accounts { get; }
}