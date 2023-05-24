using DDNSUpdate.Application.Records;
using Microsoft.Extensions.Logging;

namespace DDNSUpdate.Application.Providers.Cloudflare;

internal class CloudflareUpdateService : UpdateService<CloudflareRecord>
{
    public CloudflareUpdateService(
        ILogger<CloudflareUpdateService> logger,
        IRecordFilter<CloudflareRecord> filter, 
        IRecordReader<CloudflareRecord> reader, 
        IRecordWriter<CloudflareRecord> writer) : base(logger, filter, reader, writer)
    {
    }
}