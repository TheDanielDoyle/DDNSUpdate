using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Results;

namespace DDNSUpdate.Application.Providers.GoDaddy;

internal sealed class GoDaddyRecordWriter : IRecordWriter<GoDaddyRecord, GoDaddyAccount>
{
    public async Task<WriteRecordsResult> WriteAsync(
        GoDaddyAccount account,
        IReadOnlyCollection<GoDaddyRecord> newRecords,
        IReadOnlyCollection<GoDaddyRecord> updatedRecords,
        CancellationToken cancellationToken)
    {
        //TODO: Implement
        return await Task.FromResult(new WriteSuccess(1, 2, "GoDaddy write success"));
    }
}