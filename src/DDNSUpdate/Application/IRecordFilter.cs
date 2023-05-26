using System.Collections.Generic;

namespace DDNSUpdate.Application;

internal interface IRecordFilter<TRecord, in TAccount>
{
    IReadOnlyCollection<TRecord> FilterNew(TAccount account, IReadOnlyCollection<TRecord> records);
    IReadOnlyCollection<TRecord> FilterUpdated(TAccount account, IReadOnlyCollection<TRecord> records);
}