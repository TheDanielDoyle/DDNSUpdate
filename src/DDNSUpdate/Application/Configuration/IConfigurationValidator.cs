using System.Threading;
using System.Threading.Tasks;
using FluentResults;

namespace DDNSUpdate.Application.Configuration
{
    public interface IConfigurationValidator
    {
        Task<Result> ValidateAsync(CancellationToken cancellation);
    }
}
