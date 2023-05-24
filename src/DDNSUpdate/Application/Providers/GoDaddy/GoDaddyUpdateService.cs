using DDNSUpdate.Application.Records;
using Microsoft.Extensions.Logging;

namespace DDNSUpdate.Application.Providers.GoDaddy;

internal sealed class GoDaddyUpdateService : UpdateService<GoDaddyRecord>
{
    public GoDaddyUpdateService(
        ILogger<GoDaddyUpdateService> logger,
        IRecordFilter<GoDaddyRecord> filter,
        IRecordReader<GoDaddyRecord> reader,
        IRecordWriter<GoDaddyRecord> writer) : base(logger, filter, reader, writer)
    {
    }
}