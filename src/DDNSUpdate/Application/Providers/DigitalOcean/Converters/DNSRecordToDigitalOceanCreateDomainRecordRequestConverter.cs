using AutoMapper;
using DDNSUpdate.Application.Providers.DigitalOcean.Requests;
using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.Providers.DigitalOcean.Converters
{
    public class DNSRecordToDigitalOceanCreateDomainRecordRequestConverter : ITypeConverter<DNSRecord, DigitalOceanCreateDomainRecordRequest>
    {
        public DigitalOceanCreateDomainRecordRequest Convert(DNSRecord record, DigitalOceanCreateDomainRecordRequest? request, ResolutionContext context)
        {
            request ??= new DigitalOceanCreateDomainRecordRequest();
            request.Data = record.Data;
            request.Flags = record.Flags;
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
}
