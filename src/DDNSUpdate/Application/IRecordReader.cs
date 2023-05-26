using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Results;

namespace DDNSUpdate.Application;

internal interface IRecordReader<TRecord, in TAccount>
{
    Task<ReadRecordsResult<TRecord>> ReadAsync(TAccount account, CancellationToken cancellationToken);
}