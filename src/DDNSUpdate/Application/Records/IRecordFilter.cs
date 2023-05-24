using System.Collections.Generic;

namespace DDNSUpdate.Application.Records;

internal interface IRecordFilter<TRecord>
{
    IReadOnlyCollection<TRecord> FilterNew(IReadOnlyCollection<TRecord> records);
    IReadOnlyCollection<TRecord> FilterUpdated(IReadOnlyCollection<TRecord> records);
}