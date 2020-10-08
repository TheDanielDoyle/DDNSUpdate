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
            destination.Port = (long)source.Port!;
            destination.Priority = (long)source.Priority!;
            destination.Ttl = (long)source.TTL!;
            destination.Weight = (long)source.Weight!;

            return destination;
        }
    }
}