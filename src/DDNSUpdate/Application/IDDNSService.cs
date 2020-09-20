using System.Threading;
using System.Threading.Tasks;

public interface IDDNSService
{
    Task ProcessAsync(CancellationToken cancellation);
}