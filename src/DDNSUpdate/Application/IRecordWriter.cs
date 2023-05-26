using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Results;

namespace DDNSUpdate.Application;

internal interface IRecordWriter<in TRecord, in TAccount>
{
    Task<WriteRecordsResult> WriteAsync(TAccount account, IReadOnlyCollection<TRecord> newRecords, IReadOnlyCollection<TRecord> updatedRecords, CancellationToken cancellationToken);
}