using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Results;

namespace DDNSUpdate.Application;

internal interface IUpdateService<in TAccount>
{
    Task<UpdateResult> UpdateAsync(TAccount account, CancellationToken cancellationToken);
}