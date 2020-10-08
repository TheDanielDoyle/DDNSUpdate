using AutoMapper;
using DDNSUpdate.Application.Providers.GoDaddy.Converters;
using DDNSUpdate.Application.Providers.GoDaddy.Request;
using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.Providers.GoDaddy
{
    public class GoDaddyProfile : Profile
    {
        public GoDaddyProfile()
        {
            CreateMap<DNSRecord, GoDaddyUpdateDNSRecordRequest>().ConvertUsing<DNSRecordToGoDaddyUpdateDNSRecordRequestConverter>();
        }
    }
}