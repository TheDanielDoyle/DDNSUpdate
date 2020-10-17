using AutoMapper;
using DDNSUpdate.Application.Providers.GoDaddy.Request;
using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.Providers.GoDaddy.Converters
{
    public class DNSRecordToGoDaddyUpdateDNSRecordRequestConverter : ITypeConverter<DNSRecord, GoDaddyUpdateDNSRecordRequest>
    {
        public GoDaddyUpdateDNSRecordRequest Convert(DNSRecord source, GoDaddyUpdateDNSRecordRequest destination, ResolutionContext context)
        {
            destination ??= new GoDaddyUpdateDNSRecordRequest();
            destination.Data = source.Data;
            destination.Name = source.Name;
            destination.Port = source.Port.GetValueOrDefault();
            destination.Priority = source.Priority.GetValueOrDefault();
            destination.Ttl = source.TTL.GetValueOrDefault();
            destination.Weight = source.Weight.GetValueOrDefault();
            return destination;
        }
    }
}