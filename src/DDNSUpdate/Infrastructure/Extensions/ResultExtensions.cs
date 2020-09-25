using FluentResults;

namespace DDNSUpdate.Infrastructure.Extensions
{
    public static class ResultExtensions
    {
        public static Result Merge(this Result result, Result otherResult)
        {
            return Result.Merge(result, otherResult);
        }
    }
}