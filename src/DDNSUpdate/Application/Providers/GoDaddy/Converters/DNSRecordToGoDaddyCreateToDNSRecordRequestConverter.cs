using AutoMapper;
using DDNSUpdate.Application.Providers.GoDaddy.Request;
using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.Providers.GoDaddy.Converters
{
    public class DNSRecordToGoDaddyCreateToDNSRecordRequestConverter : ITypeConverter<DNSRecord, GoDaddyCreateDNSRecordRequest>
    {
        public GoDaddyCreateDNSRecordRequest Convert(DNSRecord dnsRecord, GoDaddyCreateDNSRecordRequest? request, ResolutionContext context)
        {
            request ??= new GoDaddyCreateDNSRecordRequest();

            request.Data = dnsRecord.Data;
            request.Name = dnsRecord.Name;
            request.Port = dnsRecord.Port;
            request.Priority = dnsRecord.Priority;
            request.Ttl = dnsRecord.TTL.GetValueOrDefault();
            request.Type = dnsRecord.Type.Value;
            request.Weight = dnsRecord.Weight;

            return request;
        }
    }
}
