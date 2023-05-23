using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application;

internal interface IUpdateService
{
    Task<UpdateResult> UpdateAsync(CancellationToken cancellationToken);
}