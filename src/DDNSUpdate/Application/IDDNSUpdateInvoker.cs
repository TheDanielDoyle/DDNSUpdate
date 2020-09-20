using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application
{
    public interface IDDNSUpdateInvoker
    {
        Task InvokeAsync(CancellationToken cancellation);
    }
}