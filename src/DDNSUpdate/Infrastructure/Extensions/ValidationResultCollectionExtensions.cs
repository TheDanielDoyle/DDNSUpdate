using DDNSUpdate.Application.Configuration;
using FluentResults;

namespace DDNSUpdate.Infrastructure.Extensions
{
    public static class ValidationResultCollectionExtensions
    {
        public static Result ToResults(this ValidationResultCollection validationResults)
        {
            if (!validationResults.IsValid)
            {
                Result failure = new Result();
                foreach (string errorMessage in validationResults.ErrorMessages)
                {
                    failure = failure.WithError(errorMessage);
                }
                return failure;
            }
            return Result.Ok();
        }
    }
}
