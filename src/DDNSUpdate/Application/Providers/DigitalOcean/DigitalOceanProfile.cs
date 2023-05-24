using DDNSUpdate.Application.Records;
using DDNSUpdate.Infrastructure.Extensions;
using DDNSUpdate.Infrastructure.Profiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

internal sealed class DigitalOceanProfile : HostApplicationBuilderProfile
{
    protected override void Add(HostApplicationBuilder builder)
    {
        builder
            .AddSettings<DigitalOceanSettings>("DigitalOcean")
            .Services
            .AddTransient<IUpdateService, DigitalOceanUpdateService>()
            .AddTransient<IRecordFilter<DigitalOceanRecord>, DigitalOceanRecordFilter>()
            .AddTransient<IRecordReader<DigitalOceanRecord>, DigitalOceanRecordReader>()
            .AddTransient<IRecordWriter<DigitalOceanRecord>, DigitalOceanRecordWriter>();
    }
}