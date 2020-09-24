using AutoMapper;
using DDNSUpdate.Application.Providers.DigitalOcean.Requests;
using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.Providers.DigitalOcean.Converters
{
    public class DNSRecordToDigitalOceanUpdateDomainRecordRequestConverter : ITypeConverter<DNSRecord, DigitalOceanUpdateDomainRecordRequest>
    {
        public DigitalOceanUpdateDomainRecordRequest Convert(DNSRecord record, DigitalOceanUpdateDomainRecordRequest request, ResolutionContext context)
        {
            request ??= new DigitalOceanUpdateDomainRecordRequest();
            return request;
        }
    }
}
