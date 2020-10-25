using AutoMapper;
using DDNSUpdate.Application.Providers.GoDaddy.Request;
using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.Providers.GoDaddy.Converters
{
    public class DNSRecordToGoDaddyUpdateDNSRecordRequestConverter : ITypeConverter<DNSRecord, GoDaddyUpdateDNSRecord>
    {
        public GoDaddyUpdateDNSRecord Convert(DNSRecord dnsRecord, GoDaddyUpdateDNSRecord? request, ResolutionContext context)
        {
            request ??= new GoDaddyUpdateDNSRecord();

            request.Data = dnsRecord.Data;
            request.Port = dnsRecord.Port;
            request.Priority = dnsRecord.Priority;
            request.Ttl = dnsRecord.TTL.GetValueOrDefault();
            request.Weight = dnsRecord.Weight;

            return request;
        }
    }
}