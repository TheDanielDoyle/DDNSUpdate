using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.Records;

internal interface IRecordReader<TRecord>
{
    Task<ReadRecordsResult<TRecord>> ReadAsync(CancellationToken cancellationToken);
}