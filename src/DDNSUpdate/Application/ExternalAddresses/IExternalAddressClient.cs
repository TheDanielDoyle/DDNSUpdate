using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.ExternalAddresses
{
    public interface IExternalAddressClient
    {
        Task<Result<IExternalAddressResponse>> GetAsync(CancellationToken cancellation);
    }
}
