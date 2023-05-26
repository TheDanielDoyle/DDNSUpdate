using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Results;
using Microsoft.Extensions.Logging;

namespace DDNSUpdate.Application;

internal abstract class UpdateService<TRecord, TAccount> : IUpdateService<TAccount>
{
    private readonly ILogger _logger;
    private readonly IRecordFilter<TRecord, TAccount> _filter;
    private readonly IRecordReader<TRecord, TAccount> _reader;
    private readonly IRecordWriter<TRecord, TAccount> _writer;

    protected UpdateService(
        ILogger logger, 
        IRecordFilter<TRecord, TAccount> filter, 
        IRecordReader<TRecord, TAccount> reader, 
        IRecordWriter<TRecord, TAccount> writer)
    {
        _logger = logger;
        _filter = filter;
        _reader = reader;
        _writer = writer;
    }

    public async Task<UpdateResult> UpdateAsync(TAccount account, CancellationToken cancellationToken)
    {
        ReadRecordsResult<TRecord> read = await _reader.ReadAsync(account, cancellationToken);
        return await read
            .Match<Task<UpdateResult>>(
                async readSuccess =>
                {
                    _logger.LogInformation("{Message}", readSuccess.Message);
                    IReadOnlyCollection<TRecord> newRecords = _filter.FilterNew(account, readSuccess.Records);
                    IReadOnlyCollection<TRecord> updatedRecords = _filter.FilterUpdated(account, readSuccess.Records);

                    WriteRecordsResult writes = await _writer.WriteAsync(account, newRecords, updatedRecords, cancellationToken);
                    return writes
                        .Match<UpdateResult>(
                            writeSuccess => new UpdateSuccess(writeSuccess.RecordsCreated, writeSuccess.RecordsUpdated, writeSuccess.Message),
                            writeFailed => new UpdateFailed(writeFailed.ErrorMessages),
                            writeCancelled => new UpdateCancelled(writeCancelled.Message));
                },
                readFailed => Task.FromResult<UpdateResult>(new UpdateFailed(readFailed.ErrorMessages)),
                readCancelled => Task.FromResult<UpdateResult>(new UpdateCancelled(readCancelled.Message)));
    }
}