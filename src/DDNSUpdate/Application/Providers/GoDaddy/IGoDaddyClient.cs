using DDNSUpdate.Application.Providers.GoDaddy.Request;
using DDNSUpdate.Application.Providers.GoDaddy.Response;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.Providers.GoDaddy
{
    public interface IGoDaddyClient
    {
        Task<Result> CreateDNSRecordAsync(GoDaddyCreateDNSRecordRequest request, CancellationToken cancellation);

        Task<Result<GoDaddyGetDNSRecordsResponse>> GetDNSRecordsAsync(GoDaddyGetDNSRecordsRequest request, CancellationToken cancellation);

        Task<Result> UpdateDNSRecordsAsync(GoDaddyUpdateDNSRecordsRequest request, CancellationToken cancellation);
    }
}
