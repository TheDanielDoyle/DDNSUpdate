using DDNSUpdate.Domain;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application
{
    public interface IDDNSService
    {
        Task<Result> ProcessAsync(ExternalAddress externalAddress, CancellationToken cancellation);
    }
}
