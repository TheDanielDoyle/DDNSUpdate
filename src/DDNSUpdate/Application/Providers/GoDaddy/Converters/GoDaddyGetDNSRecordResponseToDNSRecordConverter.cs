using AutoMapper;
using DDNSUpdate.Application.Providers.GoDaddy.Response;
using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.Providers.GoDaddy.Converters;

public class GoDaddyGetDNSRecordResponseToDNSRecordConverter : ITypeConverter<GoDaddyGetDNSRecordResponse, DNSRecord>
{
    public DNSRecord Convert(GoDaddyGetDNSRecordResponse source, DNSRecord? dnsRecord, ResolutionContext context)
    {
        dnsRecord ??= new DNSRecord();
        dnsRecord = dnsRecord with
        {
            Data = source.Data,
            Name = source.Name,
            Port = source.Port,
            Priority = source.Priority,
            TTL = source.Ttl,
            Type = DNSRecordType.FromValue(source.Type),
            Weight = source.Weight,
        };
        return dnsRecord;
    }
}