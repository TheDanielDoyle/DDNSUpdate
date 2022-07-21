using System.Linq;
using FluentResults;

namespace DDNSUpdate.Infrastructure.Extensions;

public static class ResultExtensions
{
    public static Result Merge(this Result source, params Result[] results)
    {
        foreach (Result result in results)
        {
            source = Result.Merge(source, result);
        }
        return source;
    }

    public static Result<T> Merge<T>(this Result<T> result, params Result[] results)
    {
        return result.WithReasons(results.SelectMany(r => r.Reasons));
    }
}