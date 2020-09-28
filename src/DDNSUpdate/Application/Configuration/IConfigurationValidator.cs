using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.Configuration
{
    public interface IConfigurationValidator
    {
        Task<Result> ValidateAsync(CancellationToken cancellation);
    }
}
