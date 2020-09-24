using AutoMapper;
using DDNSUpdate.Application.Providers.DigitalOcean.Requests;
using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.Providers.DigitalOcean.Converters
{
    public class DNSRecordToDigitalOceanCreateDomainRecordRequestConverter : ITypeConverter<DNSRecord, DigitalOceanCreateDomainRecordRequest>
    {
        public DigitalOceanCreateDomainRecordRequest Convert(DNSRecord record, DigitalOceanCreateDomainRecordRequest request, ResolutionContext context)
        {
            request ??= new DigitalOceanCreateDomainRecordRequest();
            return request;
        }
    }
}