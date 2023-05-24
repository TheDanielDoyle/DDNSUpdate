using DDNSUpdate.Application.Records;
using DDNSUpdate.Infrastructure.Extensions;
using DDNSUpdate.Infrastructure.Profiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DDNSUpdate.Application.Providers.Cloudflare;

internal sealed class CloudflareProfile : HostApplicationBuilderProfile
{
    protected override void Add(HostApplicationBuilder builder)
    {
        builder
            .AddSettings<CloudflareSettings>("Cloudflare")
            .Services
            .AddTransient<IUpdateService, CloudflareUpdateService>()
            .AddTransient<IRecordFilter<CloudflareRecord>, CloudflareRecordFilter>()
            .AddTransient<IRecordReader<CloudflareRecord>, CloudflareRecordReader>()
            .AddTransient<IRecordWriter<CloudflareRecord>, CloudflareRecordWriter>();
    }
}