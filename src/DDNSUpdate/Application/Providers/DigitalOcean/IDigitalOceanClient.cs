using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

internal interface IDigitalOceanClient
{
    Task<DigitalOceanReadRecordsResult> GetRecordsAsync(string domain, string bearerToken, CancellationToken cancellation);
}