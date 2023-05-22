using DDNSUpdate.Infrastructure.Settings;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace DDNSUpdate.Infrastructure.Extensions;

internal static class ILoggerExtensions
{
    public static void LogValidationErrors(this ILogger logger, ValidationResults validationResults)
    {
        foreach (ValidationFailure error in validationResults.Errors())
        {
            logger.LogError("Setting {ErrorMessage}", error.ErrorMessage);
        }
    }
}