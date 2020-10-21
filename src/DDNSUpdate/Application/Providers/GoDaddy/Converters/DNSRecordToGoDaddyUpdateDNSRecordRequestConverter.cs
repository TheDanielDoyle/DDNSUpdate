using AutoMapper;
using DDNSUpdate.Application.Providers.GoDaddy.Request;
using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.Providers.GoDaddy.Converters
{
    public class DNSRecordToGoDaddyUpdateDNSRecordRequestConverter : ITypeConverter<DNSRecord, GoDaddyUpdateDNSRecordRequest>
    {
        public GoDaddyUpdateDNSRecordRequest Convert(DNSRecord dnsRecord, GoDaddyUpdateDNSRecordRequest request, ResolutionContext context)
        {
            request ??= new GoDaddyUpdateDNSRecordRequest();

            request.Data = dnsRecord.Data;
            request.Name = dnsRecord.Name;
            request.Port = dnsRecord.Port.GetValueOrDefault();
            request.Priority = dnsRecord.Priority.GetValueOrDefault();
            request.Ttl = dnsRecord.TTL.GetValueOrDefault();
            request.Weight = dnsRecord.Weight.GetValueOrDefault();

            return request;
        }
    }
}