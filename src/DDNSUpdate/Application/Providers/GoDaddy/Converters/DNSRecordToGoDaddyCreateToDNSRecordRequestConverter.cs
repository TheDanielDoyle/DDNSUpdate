using AutoMapper;
using DDNSUpdate.Application.Providers.GoDaddy.Request;
using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.Providers.GoDaddy.Converters
{
    public class DNSRecordToGoDaddyCreateToDNSRecordRequestConverter : ITypeConverter<DNSRecord, GoDaddyCreateDNSRecordRequest>
    {
        public GoDaddyCreateDNSRecordRequest Convert(DNSRecord dnsRecord, GoDaddyCreateDNSRecordRequest request, ResolutionContext context)
        {
            request ??= new GoDaddyCreateDNSRecordRequest();

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
