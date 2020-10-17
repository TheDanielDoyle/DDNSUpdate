using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using DDNSUpdate.Application.Providers.GoDaddy.Response;
using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.Providers.GoDaddy.Converters
{
    public class GoDaddyGetDNSRecordResponseToDNSRecordConverter : ITypeConverter<GoDaddyGetDNSRecordResponse, DNSRecord>
    {
        public DNSRecord Convert(GoDaddyGetDNSRecordResponse source, DNSRecord dnsRecord, ResolutionContext context)
        {
            dnsRecord ??= new DNSRecord();

            dnsRecord.Data = source.Data;
            dnsRecord.Name = source.Name;
            dnsRecord.Port = source.Port;
            dnsRecord.Priority = source.Priority;
            dnsRecord.TTL = source.Ttl;
            dnsRecord.Type = DNSRecordType.FromValue(source.Type);
            dnsRecord.Weight = source.Weight;

            return dnsRecord;
        }
    }
}