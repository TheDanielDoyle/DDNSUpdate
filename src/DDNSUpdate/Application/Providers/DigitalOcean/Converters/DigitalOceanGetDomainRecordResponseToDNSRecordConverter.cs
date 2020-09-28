using AutoMapper;
using DDNSUpdate.Application.Providers.DigitalOcean.Responses;
using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.Providers.DigitalOcean.Converters
{
    public class DigitalOceanGetDomainRecordResponseToDNSRecordConverter : ITypeConverter<DigitalOceanGetDomainRecordResponse, DNSRecord>
    {
        public DNSRecord Convert(DigitalOceanGetDomainRecordResponse response, DNSRecord? record, ResolutionContext context)
        {
            record ??= new DNSRecord();
            record.Data = response.Data;
            record.Flags = response.Flags;
            record.Id = response.Id.ToString();
            record.Name = response.Name;
            record.Port = response.Port;
            record.Priority = response.Priority;
            record.Tag = response.Tag;
            record.TTL = response.Ttl;
            record.Type = DNSRecordType.FromValue(response.Type);
            record.Weight = response.Weight;
            return record;
        }
    }
}
