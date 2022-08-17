using AutoMapper;
using DDNSUpdate.Application.Providers.DigitalOcean.Requests;
using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.Providers.DigitalOcean.Converters;

public class DNSRecordToDigitalOceanUpdateDomainRecordRequestConverter : ITypeConverter<DNSRecord, DigitalOceanUpdateDomainRecordRequest>
{
    public DigitalOceanUpdateDomainRecordRequest Convert(DNSRecord record, DigitalOceanUpdateDomainRecordRequest? request, ResolutionContext context)
    {
        request ??= new DigitalOceanUpdateDomainRecordRequest();
        request.Data = record.Data;
        request.Flags = record.Flags;
        request.Id = int.Parse(record.Id!);
        request.Name = record.Name;
        request.Port = record.Port;
        request.Priority = record.Priority;
        request.Tag = record.Tag;
        request.Ttl = record.TTL;
        request.Type = record.Type.Value;
        request.Weight = record.Weight;
        return request;
    }
}