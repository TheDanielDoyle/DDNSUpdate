using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.Core;

internal interface IUpdateService
{
    Task UpdateAsync(CancellationToken cancellationToken);
}