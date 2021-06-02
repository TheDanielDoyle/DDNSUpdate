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
            record = record with
            {
                Data = response.Data,
                Flags = response.Flags,
                Id = response.Id.ToString(),
                Name = response.Name,
                Port = response.Port,
                Priority = response.Priority,
                Tag = response.Tag,
                TTL = response.Ttl,
                Type = DNSRecordType.FromValue(response.Type),
                Weight = response.Weight
            };
            return record;
        }
    }
}
