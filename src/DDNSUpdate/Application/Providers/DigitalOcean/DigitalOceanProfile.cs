using AutoMapper;
using DDNSUpdate.Application.Providers.DigitalOcean.Converters;
using DDNSUpdate.Application.Providers.DigitalOcean.Requests;
using DDNSUpdate.Application.Providers.DigitalOcean.Responses;
using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public class DigitalOceanProfile : Profile
    {
        public DigitalOceanProfile()
        {
            CreateMap<DigitalOceanGetDomainRecordResponse, DNSRecord>().ConvertUsing<DigitalOceanGetDomainRecordResponseToDNSRecordConverter>();
            CreateMap<DNSRecord, DigitalOceanCreateDomainRecordRequest>().ConvertUsing<DNSRecordToDigitalOceanCreateDomainRecordRequestConverter>();
            CreateMap<DNSRecord, DigitalOceanUpdateDomainRecordRequest>().ConvertUsing<DNSRecordToDigitalOceanUpdateDomainRecordRequestConverter>();
        }
    }
}
