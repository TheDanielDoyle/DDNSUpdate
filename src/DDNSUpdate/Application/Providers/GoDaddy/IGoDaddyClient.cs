using DDNSUpdate.Application.Providers.GoDaddy.Request;
using DDNSUpdate.Application.Providers.GoDaddy.Response;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.Providers.GoDaddy;

public interface IGoDaddyClient
{
    Task<Result> CreateDNSRecordsAsync(GoDaddyCreateDNSRecordsRequest request, CancellationToken cancellation);

    Task<Result<GoDaddyGetDNSRecordsResponse>> GetDNSRecordsAsync(GoDaddyGetDNSRecordsRequest request, CancellationToken cancellation);

    Task<Result> UpdateDNSRecordAsync(GoDaddyUpdateDNSRecordsRequest request, CancellationToken cancellation);
}