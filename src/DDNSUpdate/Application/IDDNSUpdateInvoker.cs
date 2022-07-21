using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application;

public interface IDDNSUpdateInvoker
{
    Task<Result> InvokeAsync(CancellationToken cancellation);
}