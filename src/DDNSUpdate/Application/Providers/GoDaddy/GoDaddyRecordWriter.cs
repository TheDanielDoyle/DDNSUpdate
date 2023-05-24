using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Records;

namespace DDNSUpdate.Application.Providers.GoDaddy;

internal sealed class GoDaddyRecordWriter : IRecordWriter<GoDaddyRecord>
{
    public async Task<WriteRecordsResult> WriteAsync(
        IReadOnlyCollection<GoDaddyRecord> newRecords,
        IReadOnlyCollection<GoDaddyRecord> updatedRecords,
        CancellationToken cancellationToken)
    {
        //TODO: Implement
        return await Task.FromResult(new WriteSuccess(1, 2, "GoDaddy write success"));
    }
}