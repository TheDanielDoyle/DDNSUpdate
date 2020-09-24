using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Providers.DigitalOcean.Requests;
using DDNSUpdate.Application.Providers.DigitalOcean.Responses;
using FluentResults;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public interface IDigitalOceanClient
    {
        Task<Result> CreateDNSRecordAsync(DigitalOceanCreateDomainRecordRequest request, string token, CancellationToken cancellation);

        Task<Result<DigitalOceanGetDomainRecordsResponse>> GetDNSRecordsAsync(DigitalOceanDomain domain, string token, CancellationToken cancellation);
        
        Task<Result> UpdateDNSRecordAsync(DigitalOceanUpdateDomainRecordRequest request, string token, CancellationToken cancellation);
    }
}