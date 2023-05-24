using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Records;
using Microsoft.Extensions.Logging;

namespace DDNSUpdate.Application;

internal abstract class UpdateService<TRecord> : IUpdateService
{
    private readonly ILogger _logger;
    private readonly IRecordFilter<TRecord> _filter;
    private readonly IRecordReader<TRecord> _reader;
    private readonly IRecordWriter<TRecord> _writer;

    protected UpdateService(ILogger logger, IRecordFilter<TRecord> filter, IRecordReader<TRecord> reader, IRecordWriter<TRecord> writer)
    {
        _logger = logger;
        _filter = filter;
        _reader = reader;
        _writer = writer;
    }

    public async Task<UpdateResult> UpdateAsync(CancellationToken cancellationToken)
    {
        ReadRecordsResult<TRecord> read = await _reader.ReadAsync(cancellationToken);
        return await read
            .Match<Task<UpdateResult>>(
                async readSuccess =>
                {
                    _logger.LogInformation("{Message}", readSuccess.Message);
                    IReadOnlyCollection<TRecord> newRecords = _filter.FilterNew(readSuccess.Records);
                    IReadOnlyCollection<TRecord> updatedRecords = _filter.FilterUpdated(readSuccess.Records);

                    WriteRecordsResult writes = await _writer.WriteAsync(newRecords, updatedRecords, cancellationToken);
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