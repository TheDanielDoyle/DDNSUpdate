using System.Linq;
using DDNSUpdate.Application.Results;
using DDNSUpdate.Infrastructure.Settings;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace DDNSUpdate.Infrastructure.Extensions;

internal static class ILoggerExtensions
{
    public static void LogUpdateFailed(this ILogger logger, UpdateFailed updateFailed)
    {
        foreach (string errorMessage in updateFailed.ErrorMessages.ToList())
        {
            logger.LogError("Update error {ErrorMessage}", errorMessage);
        }
    }
    
    public static void LogValidationErrors(this ILogger logger, ValidationResults validationResults)
    {
        foreach (ValidationFailure error in validationResults.Errors().ToList())
        {
            logger.LogError("Setting {ErrorMessage}", error.ErrorMessage);
        }
    }
}