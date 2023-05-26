using Microsoft.Extensions.Logging;

namespace DDNSUpdate.Application.Providers.GoDaddy;

internal sealed class GoDaddyUpdateService : UpdateService<GoDaddyRecord, GoDaddyAccount>
{
    public GoDaddyUpdateService(
        ILogger<GoDaddyUpdateService> logger,
        IRecordFilter<GoDaddyRecord, GoDaddyAccount> filter,
        IRecordReader<GoDaddyRecord, GoDaddyAccount> reader,
        IRecordWriter<GoDaddyRecord, GoDaddyAccount> writer) : base(logger, filter, reader, writer)
    {
    }
}