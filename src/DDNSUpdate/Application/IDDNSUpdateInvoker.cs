using System.Threading;
using System.Threading.Tasks;
using FluentResults;

namespace DDNSUpdate.Application
{
    public interface IDDNSUpdateInvoker
    {
        Task<Result> InvokeAsync(CancellationToken cancellation);
    }
}
