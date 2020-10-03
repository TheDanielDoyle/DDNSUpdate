using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using DDNSUpdate.Application.Providers.DigitalOcean.Requests;
using DDNSUpdate.Application.Providers.DigitalOcean.Responses;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public interface IDigitalOceanClient
    {
        Task<Result> CreateDNSRecordAsync(string domainName, DigitalOceanCreateDomainRecordRequest request, string token, CancellationToken cancellation);

        Task<Result<DigitalOceanGetDomainRecordsResponse>> GetDNSRecordsAsync(DigitalOceanDomain domain, string token, CancellationToken cancellation);

        Task<Result> UpdateDNSRecordAsync(string domainName, DigitalOceanUpdateDomainRecordRequest request, string token, CancellationToken cancellation);
    }
}
