using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Domain;
using FluentResults;

namespace DDNSUpdate.Application
{
    public interface IDDNSService
    {
        Task<Result> ProcessAsync(ExternalAddress externalAddress, CancellationToken cancellation);
    }
}
