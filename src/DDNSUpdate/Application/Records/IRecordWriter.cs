using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.Records;

internal interface IRecordWriter<in TRecord>
{
    Task<WriteRecordsResult> WriteAsync(IReadOnlyCollection<TRecord> newRecords, IReadOnlyCollection<TRecord> updatedRecords, CancellationToken cancellationToken);
}